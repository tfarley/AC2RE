using Dia2Lib;
using System;
using System.Collections.Generic;

namespace AC2RE.PdbTool {

    public class UDTInfo {

        public IDiaSymbol udtSymbol;
        public readonly List<IDiaSymbol> baseClassSymbols = new();
        public readonly List<IDiaSymbol> typedefSymbols = new();
        public readonly List<IDiaSymbol> constantSymbols = new();
        public readonly List<IDiaSymbol> memberSymbols = new();
        public readonly List<FunctionInfo> functionInfos = new();
        public readonly List<UDTInfo> nestedUdtInfos = new();
        public readonly bool isEnum;
        public readonly string namePrefix;

        public UDTInfo(PdbParser pdbParser, IDiaSymbol udtSymbol, bool isEnum, string namePrefix = "") {
            this.udtSymbol = udtSymbol;
            this.isEnum = isEnum;
            this.namePrefix = namePrefix;

            IDiaEnumSymbols childSymbols = pdbParser.getChildSymbols(udtSymbol, SymTagEnum.SymTagNull);
            foreach (IDiaSymbol childSymbol in childSymbols) {
                SymTagEnum tag = (SymTagEnum)childSymbol.symTag;
                switch (tag) {
                    case SymTagEnum.SymTagBaseClass:
                        baseClassSymbols.Add(childSymbol);
                        break;
                    case SymTagEnum.SymTagTypedef:
                        typedefSymbols.Add(childSymbol);
                        break;
                    case SymTagEnum.SymTagData:
                        DataKind dataKind = (DataKind)childSymbol.dataKind;
                        switch (dataKind) {
                            case DataKind.DataIsConstant:
                                constantSymbols.Add(childSymbol);
                                break;
                            case DataKind.DataIsStaticMember:
                            case DataKind.DataIsMember:
                                memberSymbols.Add(childSymbol);
                                break;
                            default:
                                throw new NotImplementedException(dataKind.ToString());
                        }
                        break;
                    case SymTagEnum.SymTagFunction:
                        if (!pdbParser.functionInfoById.TryGetValue(childSymbol.symIndexId, out FunctionInfo? functionInfo)) {
                            functionInfo = new(pdbParser, childSymbol);
                            pdbParser.functionInfoById[childSymbol.symIndexId] = functionInfo;
                        }
                        functionInfos.Add(functionInfo);
                        break;
                    case SymTagEnum.SymTagUDT:
                        if (!pdbParser.udtInfoById.TryGetValue(childSymbol.symIndexId, out UDTInfo? udtInfo)) {
                            udtInfo = new(pdbParser, childSymbol, false, namePrefix + udtSymbol.name + "::");
                            pdbParser.udtInfoById[childSymbol.symIndexId] = udtInfo;
                        }
                        nestedUdtInfos.Add(udtInfo);
                        break;
                    case SymTagEnum.SymTagEnum:
                        nestedUdtInfos.Add(new(pdbParser, childSymbol, true, namePrefix + udtSymbol.name + "::"));
                        break;
                    case SymTagEnum.SymTagVTable:
                        // TODO: Implement
                        break;
                    default:
                        throw new NotImplementedException(tag.ToString());
                }
            }
        }
    }
}
