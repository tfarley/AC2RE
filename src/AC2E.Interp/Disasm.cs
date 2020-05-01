using System;
using System.Collections.Generic;
using System.IO;
using static AC2E.Interp.ByteStream;

namespace AC2E.Interp {

    public class Disasm {

        public class Instruction {

            public uint offset;
            public uint raw;
            public bool dwordFlag;
            public Opcode opcode;
            public uint? val;
            public ExportData targetPackage;
            public ExportFunctionData targetFunc;
        }

        public readonly ByteStream byteStream;
        public readonly Dictionary<uint, string> funcLocToName = new Dictionary<uint, string>();
        public readonly Dictionary<string, FrameDebugInfo> nameToFrame = new Dictionary<string, FrameDebugInfo>();
        public readonly Dictionary<FunctionId, KeyValuePair<ExportData, ExportFunctionData>> addrToTarget = new Dictionary<FunctionId, KeyValuePair<ExportData, ExportFunctionData>>();
        public readonly Dictionary<uint, ExportData> packageIdToPackage = new Dictionary<uint, ExportData>();

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
                packageIdToPackage[export.args.packageId] = export;
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
                };
                // TODO: Decode more opcodes - some may be multi-word like the ones below, and may be some with embedded immediates
                switch (instruction.opcode) {
                    case Opcode.CALL:
                    case Opcode.CALL_EFUN:
                        i += 4;
                        FunctionId targetFuncId = new FunctionId(BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i));
                        KeyValuePair<ExportData, ExportFunctionData> target = addrToTarget.GetValueOrDefault(new FunctionId(targetFuncId.funcAddr), new KeyValuePair<ExportData, ExportFunctionData>());
                        instruction.targetPackage = target.Key;
                        instruction.targetFunc = target.Value;
                        break;
                    case Opcode.PUSH:
                        i += 4;
                        instruction.val = BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i);
                        break;
                    case Opcode.NEW:
                    case Opcode.NEW_NATIVE:
                        i += 4;
                        instruction.targetPackage = packageIdToPackage.GetValueOrDefault(BitConverter.ToUInt32(byteStream.opcodeStream.opcodeBytes, (int)i), null);
                        break;
                    default:
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
                    uint packageId = byteStream.vTable.packageIdStrMap[packageName];
                    FunctionId funcId = packageIdToPackage[packageId].funcs.Find(f => f.name == funcName).funcId;
                    funcId.isAbs = true;
                    data.WriteLine();
                    data.Write($"0x{funcId.id:X8} func ");
                    writeFunction(data, functionName);
                    data.WriteLine();
                }
                data.Write(instruction.opcode.ToString());
                if (instruction.val.HasValue) {
                    data.Write($" 0x{instruction.val.Value:X8}");
                }
                if (instruction.targetPackage != null) {
                    data.Write($" {instruction.targetPackage.args.name}");
                    if (instruction.targetFunc == null) {
                        data.Write($" (0x{instruction.targetPackage.args.packageId:X8})");
                    }
                }
                if (instruction.targetFunc != null) {
                    writeFunction(data, $"{instruction.targetPackage.args.name}::{instruction.targetFunc.name}");
                    FunctionId funcId = instruction.targetFunc.funcId;
                    funcId.isAbs = true;
                    data.Write($" [0x{funcId.id:X8}]");
                }
                data.WriteLine();
            }
        }
    }
}
