using System;
using System.Collections.Generic;
using System.IO;
using static AC2RE.Definitions.ByteStream;

namespace AC2RE.Definitions;

public class Disasm {

    public class Instruction {

        public static readonly int TAG_SHIFT = 8; // OPCODE_TAG_SHIFT
        public static readonly int IMMEDIATE_SHIFT = 16; // OPCODE_VALUE_SHIFT

        public uint offset;
        public uint raw;
        public Opcode opcode;
        public bool dwordFlag;
        public StackType tag;
        public short immediate;
        public bool valIsDec;
        public bool val2IsDec;
        public long? val;
        public double? valDouble;
        public string valString;
        public long? val2;
        public ExportData targetPackage;
        public ExportFunctionData targetFunc;
        public string targetString;
    }

    public readonly ByteStream byteStream;
    public readonly Dictionary<uint, string> funcLocToName = new();
    public readonly Dictionary<string, FrameDebugInfo> nameToFrame = new();
    public readonly Dictionary<FunctionId, KeyValuePair<ExportData, ExportFunctionData>> addrToTarget = new();
    public readonly Dictionary<PackageType, ExportData> packageTypeToExport = new();

    public readonly List<Instruction> instructions = new();

    public Disasm(ByteStream byteStream) {
        this.byteStream = byteStream;

        foreach (FunctionLocationInfo funcLoc in byteStream.funcLocs) {
            funcLocToName[funcLoc.offset] = funcLoc.functionName;
        }

        foreach (FrameDebugInfo frame in byteStream.frames) {
            nameToFrame[frame.name] = frame;
        }

        foreach (ExportData export in byteStream.exports) {
            packageTypeToExport[export.args.packageType] = export;
            foreach (ExportFunctionData func in export.funcs) {
                addrToTarget[func.funcId] = new(export, func);
            }
        }

        FrameDebugInfo curFrame = null;

        for (uint i = 0; i < byteStream.opcodeStream.opcodeBytes.Length; i += 4) {
            if (funcLocToName.TryGetValue(i, out string functionName)) {
                curFrame = nameToFrame[functionName];
            }

            uint rawInstruction = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
            Instruction instruction = new() {
                offset = i,
                raw = rawInstruction,
                opcode = (Opcode)(rawInstruction & 0x7F),
                dwordFlag = (rawInstruction & (uint)Opcode.DWORD_FLAG) != 0,
                tag = (StackType)(byte)((rawInstruction & 0x0F00) >> Instruction.TAG_SHIFT),
                immediate = (short)((rawInstruction & 0xFFFF0000) >> Instruction.IMMEDIATE_SHIFT),
            };
            // TODO: Decode more opcodes via Interp::Handle* - some may have embedded immediates
            switch (instruction.opcode) {
                case Opcode.PUSH:
                    instruction.valIsDec = true;
                    if (instruction.dwordFlag) {
                        i += 4;
                        byte[] swappedBytes = new byte[8];
                        for (int j = 0; j < 4; j++) {
                            swappedBytes[j] = byteStream.opcodeStream.opcodeBytes[i + 4 + j];
                            swappedBytes[j + 4] = byteStream.opcodeStream.opcodeBytes[i + j];
                        }
                        switch (instruction.tag) {
                            case StackType.Float:
                                instruction.valDouble = BitConverter.ToDouble(swappedBytes);
                                break;
                            default:
                                instruction.val = BitConverter.ToInt64(swappedBytes);
                                break;
                        }
                        i += 4;
                    } else {
                        i += 4;
                        switch (instruction.tag) {
                            case StackType.Float:
                                instruction.valDouble = BitConverter.ToSingle(byteStream.opcodeStream.opcodeBytes, (int)i);
                                break;
                            default:
                                instruction.val = BitConverter.ToInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                                break;
                        }
                    }
                    break;
                case Opcode.NEW:
                case Opcode.NEW_NATIVE:
                    i += 4;
                    PackageType packageType = (PackageType)(BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i));
                    instruction.targetPackage = packageTypeToExport[packageType];
                    break;
                case Opcode.NEW_STRING:
                    i += 4;
                    int stringIndex = BitConverter.ToInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                    instruction.targetString = byteStream.stringLitTable.strings[stringIndex];
                    break;
                case Opcode.CALL:
                case Opcode.CALL_EFUN:
                    i += 4;
                    FunctionId targetFuncId = new(BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i));
                    // TODO: There is still a lurking case where the key isn't found in the dictionary (6990)
                    KeyValuePair<ExportData, ExportFunctionData> target = addrToTarget.GetValueOrDefault(new(targetFuncId.funcAddr), new());
                    instruction.targetPackage = target.Key;
                    instruction.targetFunc = target.Value;
                    break;
                case Opcode.OPTIMIZED_THIS_CALL:
                case Opcode.OPTIMIZED_THIS_CALLEFUN:
                case Opcode.OPTIMIZED_LOAD_CALL:
                case Opcode.OPTIMIZED_LOAD_CALLEFUN:
                case Opcode.OPTIMIZED_RLOAD_CALL:
                case Opcode.OPTIMIZED_RLOAD_CALLEFUN:
                case Opcode.OPTIMIZED_THIS_RLOAD:
                case Opcode.CALLSTATIC:
                    i += 4;
                    // TODO: Parse
                    break;
                case Opcode.IF:
                case Opcode.RJMP:
                    instruction.val = i + instruction.immediate + 4;
                    break;
                case Opcode.LOAD:
                    instruction.valString = getFrameMemberNameByOffset(curFrame, instruction.immediate);
                    break;
                case Opcode.PUSHV:
                    switch (instruction.tag) {
                        case StackType.Reference:
                            if (instruction.immediate == -1) {
                                instruction.valString = "null";
                            } else {
                                instruction.val = instruction.immediate;
                            }
                            break;
                        default:
                            instruction.valIsDec = true;
                            instruction.val = instruction.immediate;
                            break;
                    }
                    break;
                case Opcode.POPN:
                case Opcode.PUSH_FRAME:
                case Opcode.SADDR:
                    instruction.valIsDec = true;
                    instruction.val = instruction.immediate;
                    break;
                case Opcode.RLOAD:
                case Opcode.RSTORE:
                    instruction.val = instruction.immediate;
                    break;
                case Opcode.SWITCH: {
                        i += 4;
                        uint defaultOffset = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                        i += 4;
                        uint numCases = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                        uint switchEndOffset = i + numCases * 2 * 4 + 4;
                        instruction.targetString = $"default: 0x{switchEndOffset + defaultOffset:X8}";
                        for (int j = 0; j < numCases; j++) {
                            i += 4;
                            uint caseValue = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                            i += 4;
                            uint caseOffset = switchEndOffset + BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                            instruction.targetString += $" {caseValue}: 0x{caseOffset:X8}";
                        }
                        break;
                    }
                default:
                    break;
            }
            instructions.Add(instruction);
        }
    }

    private string getFrameMemberNameByOffset(FrameDebugInfo frame, int offset) {
        foreach (FrameMemberDebugInfo member in frame.members) {
            if (member.offset == offset) {
                return $"{member.name} [{offset}]";
            }
        }
        return offset.ToString();
    }

    private void writeFunction(StreamWriter data, string functionName, bool printStackVars) {
        data.Write(functionName);
        if (nameToFrame.TryGetValue(functionName, out FrameDebugInfo frame)) {
            bool firstMember = true;
            data.Write('(');
            foreach (FrameMemberDebugInfo frameMember in frame.members) {
                if (frameMember.offset >= 0) {
                    continue;
                }

                if (!firstMember) {
                    data.Write(", ");
                }
                data.Write($"{frameMember.typeName} {frameMember.name}");
                firstMember = false;
            }
            data.Write(')');

            if (printStackVars) {
                firstMember = true;
                data.Write(" [");
                foreach (FrameMemberDebugInfo frameMember in frame.members) {
                    if (frameMember.offset < 0) {
                        continue;
                    }

                    if (!firstMember) {
                        data.Write(", ");
                    }
                    data.Write($"{frameMember.typeName} {frameMember.name}");
                    firstMember = false;
                }
                data.Write(']');

            }
        }
    }

    public void write(StreamWriter data) {
        foreach (Instruction instruction in instructions) {
            if (funcLocToName.TryGetValue(instruction.offset, out string functionName)) {
                string[] fullNameSplit = functionName.Split("::");
                string packageName = fullNameSplit[0];
                string funcName = fullNameSplit[1];
                PackageType packageType = byteStream.vTable.packageNameToType[packageName];
                FunctionId funcId = packageTypeToExport[packageType].funcs.Find(f => f.name == funcName).funcId;
                funcId.isAbs = true;
                data.WriteLine();
                data.Write($"0x{funcId.id:X8} func ");
                writeFunction(data, functionName, true);
                data.Write($" pkg 0x{funcId.packageNum:X4} func 0x{funcId.funcNum:X4}");
                data.WriteLine();
            }
            data.Write($"0x{instruction.offset:X8} {instruction.raw:X8} {instruction.opcode}");
            if (instruction.dwordFlag) {
                data.Write("(L)");
            }
            if (instruction.val.HasValue) {
                if (instruction.valIsDec) {
                    data.Write($" {instruction.val.Value}");
                } else if (instruction.dwordFlag) {
                    data.Write($" 0x{instruction.val.Value:X16}");
                } else {
                    data.Write($" 0x{instruction.val.Value:X8}");
                }
            }
            if (instruction.valDouble.HasValue) {
                data.Write($" {instruction.valDouble}");
            }
            if (instruction.val2.HasValue) {
                if (instruction.val2IsDec) {
                    data.Write($" {instruction.val2.Value}");
                } else if (instruction.dwordFlag) {
                    data.Write($" 0x{instruction.val2.Value:X16}");
                } else {
                    data.Write($" 0x{instruction.val2.Value:X8}");
                }
            }
            if (instruction.targetPackage != null && instruction.targetFunc == null) {
                data.Write($" {instruction.targetPackage.args.name} (0x{(uint)instruction.targetPackage.args.packageType:X8})");
            }
            if (instruction.targetFunc != null) {
                data.Write(' ');
                writeFunction(data, $"{instruction.targetPackage.args.name}::{instruction.targetFunc.name}", false);
                FunctionId funcId = instruction.targetFunc.funcId;
                funcId.isAbs = true;
                data.Write($" [0x{funcId.id:X8}]");
            }
            if (instruction.valString != null) {
                data.Write($" {instruction.valString}");
            }
            if (instruction.targetString != null) {
                data.Write($" \"{instruction.targetString}\"");
            }
            if (instruction.tag != 0) {
                data.Write($" Tag: {instruction.tag}");
            }
            data.WriteLine();
        }
    }
}
