using Dia2Lib;
using System;
using System.Collections.Generic;

namespace AC2RE.PdbTool;

public class FunctionInfo {

    public IDiaSymbol functionSymbol;
    public readonly List<IDiaSymbol> constantSymbols = new();
    public readonly List<IDiaSymbol> ptrSymbols = new();
    public readonly List<IDiaSymbol> argSymbols = new();
    public readonly List<IDiaSymbol> localSymbols = new();

    public FunctionInfo(PdbParser pdbParser, IDiaSymbol functionSymbol) {
        this.functionSymbol = functionSymbol;

        IDiaEnumSymbols dataSymbols = pdbParser.getChildSymbols(functionSymbol, SymTagEnum.SymTagData);
        foreach (IDiaSymbol dataSymbol in dataSymbols) {
            DataKind dataKind = (DataKind)dataSymbol.dataKind;
            switch (dataKind) {
                case DataKind.DataIsConstant:
                    constantSymbols.Add(dataSymbol);
                    break;
                case DataKind.DataIsObjectPtr:
                    ptrSymbols.Add(dataSymbol);
                    break;
                case DataKind.DataIsParam:
                    argSymbols.Add(dataSymbol);
                    break;
                case DataKind.DataIsStaticLocal:
                case DataKind.DataIsLocal:
                    localSymbols.Add(dataSymbol);
                    break;
                default:
                    throw new NotImplementedException(dataKind.ToString());
            }
        }
    }
}
