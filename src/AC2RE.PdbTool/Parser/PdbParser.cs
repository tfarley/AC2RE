using Dia2Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AC2RE.PdbTool {

    public class PdbParser {

        private static readonly int REGDB_E_CLASSNOTREG = unchecked((int)0x80040154);

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
                    Console.WriteLine("ERROR: DIA dll not registered. Run as Admin:\nregsvr32 \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\DIA SDK\\bin\\amd64\\msdia140.dll\"");
                }
                throw;
            }

            diaSrc.loadDataFromPdb(pdbFilePath);

            diaSrc.openSession(out IDiaSession diaSession);

            IDiaEnumSymbols typedefSymbols = getChildSymbols(diaSession.globalScope, SymTagEnum.SymTagTypedef);
            foreach (IDiaSymbol typedefSymbol in typedefSymbols) {
                if (!typedefById.ContainsKey(typedefSymbol.symIndexId)) {
                    typedefById[typedefSymbol.symIndexId] = typedefSymbol;
                }
            }

            IDiaEnumSymbols udtSymbols = getChildSymbols(diaSession.globalScope, SymTagEnum.SymTagUDT);
            foreach (IDiaSymbol udtSymbol in udtSymbols) {
                if (!udtInfoById.TryGetValue(udtSymbol.symIndexId, out UDTInfo? udtInfo)) {
                    udtInfo = new UDTInfo(this, udtSymbol, false);
                    udtInfoById[udtSymbol.symIndexId] = udtInfo;
                }
            }

            IDiaEnumSymbols enumSymbols = getChildSymbols(diaSession.globalScope, SymTagEnum.SymTagEnum);
            foreach (IDiaSymbol enumSymbol in enumSymbols) {
                if (!udtInfoById.TryGetValue(enumSymbol.symIndexId, out UDTInfo? udtInfo)) {
                    udtInfo = new UDTInfo(this, enumSymbol, true);
                    udtInfoById[enumSymbol.symIndexId] = udtInfo;
                }
            }

            IDiaEnumSymbols functionSymbols = getChildSymbols(diaSession.globalScope, SymTagEnum.SymTagFunction);
            foreach (IDiaSymbol functionSymbol in functionSymbols) {
                if (!functionInfoById.TryGetValue(functionSymbol.symIndexId, out FunctionInfo? functionInfo)) {
                    functionInfo = new FunctionInfo(this, functionSymbol);
                    functionInfoById[functionSymbol.symIndexId] = functionInfo;
                }
            }
        }

        public IDiaEnumSymbols getChildSymbols(IDiaSymbol parentSymbol, SymTagEnum symTag) {
            parentSymbol.findChildren(symTag, null, 0, out IDiaEnumSymbols symbols);
            return symbols;
        }
    }
}
