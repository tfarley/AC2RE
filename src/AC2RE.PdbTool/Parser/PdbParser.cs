using Dia2Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AC2RE.PdbTool {

    public class PdbParser {

        private static readonly int REGDB_E_CLASSNOTREG = unchecked((int)0x80040154);

        public Guid guid;
        public readonly Dictionary<uint, IDiaSymbol> dataById = new();
        public readonly Dictionary<uint, IDiaSymbol> typedefById = new();
        public readonly Dictionary<uint, UDTInfo> udtInfoById = new();
        public readonly Dictionary<uint, FunctionInfo> functionInfoById = new();

        public PdbParser(string pdbFilePath) {
            // NOTE: If this does not build due to: Warning MSB3284 Cannot get the file path for type library "106173a0-0173-4e5c-84e7-e915422be997" version 2.0. Library not registered. (Exception from HRESULT: 0x8002801D(TYPE_E_LIBNOTREGISTERED))
            // Then need to perform the regsvr32 command below before building
            DiaSource diaSrc;
            try {
                diaSrc = new();
            } catch (COMException e) {
                if (e.HResult == REGDB_E_CLASSNOTREG) {
                    Console.WriteLine("ERROR: DIA dll not registered. Run as Admin:\nregsvr32 \"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\DIA SDK\\bin\\amd64\\msdia140.dll\"");
                }
                throw;
            }

            diaSrc.loadDataFromPdb(pdbFilePath);

            diaSrc.openSession(out IDiaSession diaSession);

            guid = diaSession.globalScope.guid;

            parseChildSymbols(diaSession.globalScope);

            IDiaEnumSymbols compilandSymbols = getChildSymbols(diaSession.globalScope, SymTagEnum.SymTagCompiland);
            if (compilandSymbols != null) {
                foreach (IDiaSymbol compilandSymbol in compilandSymbols) {
                    parseChildSymbols(compilandSymbol);
                }
            }
        }

        private void parseChildSymbols(IDiaSymbol parentSymbol) {
            IDiaEnumSymbols dataSymbols = getChildSymbols(parentSymbol, SymTagEnum.SymTagData);
            if (dataSymbols != null) {
                foreach (IDiaSymbol dataSymbol in dataSymbols) {
                    if (!dataById.ContainsKey(dataSymbol.symIndexId)) {
                        dataById[dataSymbol.symIndexId] = dataSymbol;
                    }
                }
            }

            IDiaEnumSymbols typedefSymbols = getChildSymbols(parentSymbol, SymTagEnum.SymTagTypedef);
            if (typedefSymbols != null) {
                foreach (IDiaSymbol typedefSymbol in typedefSymbols) {
                    if (!typedefById.ContainsKey(typedefSymbol.symIndexId)) {
                        typedefById[typedefSymbol.symIndexId] = typedefSymbol;
                    }
                }
            }

            IDiaEnumSymbols udtSymbols = getChildSymbols(parentSymbol, SymTagEnum.SymTagUDT);
            if (udtSymbols != null) {
                foreach (IDiaSymbol udtSymbol in udtSymbols) {
                    if (!udtInfoById.ContainsKey(udtSymbol.symIndexId)) {
                        udtInfoById[udtSymbol.symIndexId] = new UDTInfo(this, udtSymbol, false);
                    }
                }
            }

            IDiaEnumSymbols enumSymbols = getChildSymbols(parentSymbol, SymTagEnum.SymTagEnum);
            if (enumSymbols != null) {
                foreach (IDiaSymbol enumSymbol in enumSymbols) {
                    if (!udtInfoById.ContainsKey(enumSymbol.symIndexId)) {
                        udtInfoById[enumSymbol.symIndexId] = new UDTInfo(this, enumSymbol, true);
                    }
                }
            }

            IDiaEnumSymbols functionSymbols = getChildSymbols(parentSymbol, SymTagEnum.SymTagFunction);
            if (functionSymbols != null) {
                foreach (IDiaSymbol functionSymbol in functionSymbols) {
                    if (!functionInfoById.ContainsKey(functionSymbol.symIndexId)) {
                        functionInfoById[functionSymbol.symIndexId] = new FunctionInfo(this, functionSymbol);
                    }
                }
            }
        }

        public IDiaEnumSymbols getChildSymbols(IDiaSymbol parentSymbol, SymTagEnum symTag = SymTagEnum.SymTagNull) {
            parentSymbol.findChildren(symTag, null, 0, out IDiaEnumSymbols symbols);
            return symbols;
        }

        private List<IDiaSymbol>? findSymbolPath(IDiaSymbol rootSymbol, Predicate<IDiaSymbol> predicate) {
            // Iterative DFS in order to use heap memory and avoid stack overflows, but reduce memory usage by tracking the next child index and re-add/pop the parent for each child visit, instead of pushing all children onto the stack at once
            Stack<(IDiaSymbol, int)> symbolAndNextChildIndexStack = new();
            Dictionary<uint, int> symbolIdToChildCount = new();

            symbolAndNextChildIndexStack.Push((rootSymbol, -1));

            while (symbolAndNextChildIndexStack.TryPop(out (IDiaSymbol, int) symbolAndNextChildIndex)) {
                (IDiaSymbol symbol, int childIndex) = symbolAndNextChildIndex;
                if (childIndex == -1) {
                    SymTagEnum symbolTag = (SymTagEnum)symbol.symTag;
                    // Don't process these since they appear to have cyclical symbol references or something
                    if (symbolTag == SymTagEnum.SymTagFuncDebugStart || symbolTag == SymTagEnum.SymTagFuncDebugEnd) {
                        continue;
                    }

                    if (predicate.Invoke(symbol)) {
                        List<IDiaSymbol> symbolPath = new();
                        // NOTE: Stack enumerator is LIFO (reverse of List)
                        foreach ((IDiaSymbol parentSymbol, int _) in symbolAndNextChildIndexStack) {
                            symbolPath.Add(parentSymbol);
                        }
                        symbolPath.Add(symbol);
                        return symbolPath;
                    }
                }

                if (childIndex == -1) {
                    symbolAndNextChildIndexStack.Push((symbol, childIndex + 1));
                } else {
                    IDiaEnumSymbols? childSymbols = null;
                    if (!symbolIdToChildCount.TryGetValue(symbol.symIndexId, out int childCount)) {
                        childSymbols = getChildSymbols(symbol);
                        childCount = childSymbols?.count ?? 0;
                        symbolIdToChildCount[symbol.symIndexId] = childCount;
                    }
                    if (childIndex < childCount) {
                        if (childSymbols == null) {
                            childSymbols = getChildSymbols(symbol);
                        }
                        symbolAndNextChildIndexStack.Push((symbol, childIndex + 1));
                        IDiaSymbol childSymbol = childSymbols.Item((uint)childIndex);
                        symbolAndNextChildIndexStack.Push((childSymbol, -1));
                    }
                }
            }

            return null;
        }
    }
}
