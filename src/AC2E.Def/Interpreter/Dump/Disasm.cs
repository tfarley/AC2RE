using System;
using System.Collections.Generic;
using System.IO;
using static AC2E.Def.ByteStream;

namespace AC2E.Def {

    public class Disasm {

        public class Instruction {

            public uint offset;
            public uint raw;
            public bool dwordFlag;
            public Opcode opcode;
            public uint immediate;
            public bool valIsDec;
            public bool val2IsDec;
            public long? val;
            public long? val2;
            public ExportData targetPackage;
            public ExportFunctionData targetFunc;
            public string targetString;
        }

        public readonly ByteStream byteStream;
        public readonly Dictionary<uint, string> funcLocToName = new Dictionary<uint, string>();
        public readonly Dictionary<string, FrameDebugInfo> nameToFrame = new Dictionary<string, FrameDebugInfo>();
        public readonly Dictionary<FunctionId, KeyValuePair<ExportData, ExportFunctionData>> addrToTarget = new Dictionary<FunctionId, KeyValuePair<ExportData, ExportFunctionData>>();
        public readonly Dictionary<PackageType, ExportData> packageTypeToExport = new Dictionary<PackageType, ExportData>();

        public readonly List<Instruction> instructions = new List<Instruction>();

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
                    addrToTarget[func.funcId] = new KeyValuePair<ExportData, ExportFunctionData>(export, func);
                }
            }

            for (uint i = 0; i < byteStream.opcodeStream.opcodeBytes.Length; i += 4) {
                uint rawInstruction = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                Instruction instruction = new Instruction {
                    offset = i,
                    raw = rawInstruction,
                    dwordFlag = (rawInstruction & (uint)Opcode.DWORD_FLAG) != 0,
                    opcode = (Opcode)(rawInstruction & 0x7F),
                    immediate = ((rawInstruction & 0x7FFF0000) >> 16),
                };
                // TODO: Decode more opcodes - some may be multi-word like the ones below, and may be some with embedded immediates
                switch (instruction.opcode) {
                    case Opcode.PUSH:
                        instruction.valIsDec = true;
                        if (instruction.dwordFlag) {
                            i += 4;
                            instruction.val = BitConverter.ToInt64(byteStream.opcodeStream.opcodeBytes, (int)i);
                            i += 4;
                        } else {
                            i += 4;
                            instruction.val = BitConverter.ToInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
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
                        FunctionId targetFuncId = new FunctionId(BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i));
                        // TODO: There is still a lurking case where the key isn't found in the dictionary (6990)
                        KeyValuePair<ExportData, ExportFunctionData> target = addrToTarget.GetValueOrDefault(new FunctionId(targetFuncId.funcAddr), new KeyValuePair<ExportData, ExportFunctionData>());
                        instruction.targetPackage = target.Key;
                        instruction.targetFunc = target.Value;
                        break;
                    case Opcode.IF:
                    case Opcode.RJMP:
                        instruction.val = i + instruction.immediate + 4;
                        break;
                    case Opcode.LOAD:
                    case Opcode.POPN:
                    case Opcode.PUSH_FRAME:
                    case Opcode.PUSHV:
                    case Opcode.SADDR:
                        instruction.valIsDec = true;
                        instruction.val = instruction.immediate;
                        break;
                    case Opcode.RLOAD:
                        instruction.val = instruction.immediate;
                        break;
                    case Opcode.SWITCH: {
                            i += 4;
                            uint val1 = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                            i += 4;
                            uint numCases = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                            instruction.targetString = $"{val1} {numCases}:";
                            uint switchEndOffset = i + numCases * 2 * 4 + 4;
                            for (int j = 0; j < numCases; j++) {
                                i += 4;
                                uint caseValue = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                                i += 4;
                                uint caseOffset = switchEndOffset + BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                                instruction.targetString += $" {caseValue} 0x{caseOffset:X8}";
                            }
                            break;
                        }
                    default:
                        if (instruction.opcode >= Opcode.FADD && instruction.opcode <= Opcode.FDEC) {
                            i += 4;
                            instruction.val = BitConverter.ToInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                            if (instruction.dwordFlag) {
                                i += 4;
                                instruction.val2 = BitConverter.ToInt64(byteStream.opcodeStream.opcodeBytes, (int)i);
                                i += 4;
                            } else {
                                i += 4;
                                instruction.val2 = BitConverter.ToInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                            }
                        }
                        break;
                }
                instructions.Add(instruction);
            }
        }

        private void writeFunction(StreamWriter data, string functionName) {
            data.Write(functionName);
            if (nameToFrame.TryGetValue(functionName, out FrameDebugInfo frame)) {
                bool firstMember = true;
                data.Write('(');
                foreach (FrameMemberDebugInfo frameMember in frame.members) {
                    if (!firstMember) {
                        data.Write(", ");
                    }
                    data.Write($"{frameMember.typeName} {frameMember.name}");
                    firstMember = false;
                }
                data.Write(')');
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
                    writeFunction(data, functionName);
                    data.WriteLine();
                }
                data.Write($"0x{instruction.offset:X8} {instruction.opcode}");
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
                if (instruction.val2.HasValue) {
                    if (instruction.val2IsDec) {
                        data.Write($" {instruction.val2.Value}");
                    } else if (instruction.dwordFlag) {
                        data.Write($" 0x{instruction.val2.Value:X16}");
                    } else {
                        data.Write($" 0x{instruction.val2.Value:X8}");
                    }
                }
                if (instruction.targetPackage != null) {
                    data.Write($" {instruction.targetPackage.args.name}");
                    if (instruction.targetFunc == null) {
                        data.Write($" (0x{(uint)instruction.targetPackage.args.packageType:X8})");
                    }
                }
                if (instruction.targetFunc != null) {
                    writeFunction(data, $"::{instruction.targetFunc.name}");
                    FunctionId funcId = instruction.targetFunc.funcId;
                    funcId.isAbs = true;
                    data.Write($" [pkg 0x{funcId.packageNum:X4} func 0x{funcId.funcNum:X4} full id 0x{funcId.id:X8}]");
                }
                if (instruction.targetString != null) {
                    data.Write($" \"{instruction.targetString}\"");
                }
                //data.Write($" {instruction.raw:X8}");
                data.WriteLine();
            }
        }
    }
}
