using Dia2Lib;
using System;
using System.Text;

namespace AC2RE.PdbTool {

    public static class PdbPrinter {

        private static readonly string[] REG_TO_NAME = {
            "None",         // 0   CV_REG_NONE
            "a",            // 1   CV_REG_AL
            "c",            // 2   CV_REG_CL
            "d",            // 3   CV_REG_DL
            "b",            // 4   CV_REG_BL
            "ah",           // 5   CV_REG_AH
            "ch",           // 6   CV_REG_CH
            "dh",           // 7   CV_REG_DH
            "bh",           // 8   CV_REG_BH
            "ax",           // 9   CV_REG_AX
            "cx",           // 10  CV_REG_CX
            "dx",           // 11  CV_REG_DX
            "bx",           // 12  CV_REG_BX
            "sp",           // 13  CV_REG_SP
            "bp",           // 14  CV_REG_BP
            "si",           // 15  CV_REG_SI
            "di",           // 16  CV_REG_DI
            "eax",          // 17  CV_REG_EAX
            "ecx",          // 18  CV_REG_ECX
            "edx",          // 19  CV_REG_EDX
            "ebx",          // 20  CV_REG_EBX
            "esp",          // 21  CV_REG_ESP
            "ebp",          // 22  CV_REG_EBP
            "esi",          // 23  CV_REG_ESI
            "edi",          // 24  CV_REG_EDI
            "es",           // 25  CV_REG_ES
            "cs",           // 26  CV_REG_CS
            "ss",           // 27  CV_REG_SS
            "ds",           // 28  CV_REG_DS
            "fs",           // 29  CV_REG_FS
            "gs",           // 30  CV_REG_GS
            "IP",           // 31  CV_REG_IP
            "FLAGS",        // 32  CV_REG_FLAGS
            "EIP",          // 33  CV_REG_EIP
            "EFLAGS",       // 34  CV_REG_EFLAG
            "???",          // 35
            "???",          // 36
            "???",          // 37
            "???",          // 38
            "???",          // 39
            "TEMP",         // 40  CV_REG_TEMP
            "TEMPH",        // 41  CV_REG_TEMPH
            "QUOTE",        // 42  CV_REG_QUOTE
            "PCDR3",        // 43  CV_REG_PCDR3
            "PCDR4",        // 44  CV_REG_PCDR4
            "PCDR5",        // 45  CV_REG_PCDR5
            "PCDR6",        // 46  CV_REG_PCDR6
            "PCDR7",        // 47  CV_REG_PCDR7
            "???",          // 48
            "???",          // 49
            "???",          // 50
            "???",          // 51
            "???",          // 52
            "???",          // 53
            "???",          // 54
            "???",          // 55
            "???",          // 56
            "???",          // 57
            "???",          // 58
            "???",          // 59
            "???",          // 60
            "???",          // 61
            "???",          // 62
            "???",          // 63
            "???",          // 64
            "???",          // 65
            "???",          // 66
            "???",          // 67
            "???",          // 68
            "???",          // 69
            "???",          // 70
            "???",          // 71
            "???",          // 72
            "???",          // 73
            "???",          // 74
            "???",          // 75
            "???",          // 76
            "???",          // 77
            "???",          // 78
            "???",          // 79
            "cr0",          // 80  CV_REG_CR0
            "cr1",          // 81  CV_REG_CR1
            "cr2",          // 82  CV_REG_CR2
            "cr3",          // 83  CV_REG_CR3
            "cr4",          // 84  CV_REG_CR4
            "???",          // 85
            "???",          // 86
            "???",          // 87
            "???",          // 88
            "???",          // 89
            "dr0",          // 90  CV_REG_DR0
            "dr1",          // 91  CV_REG_DR1
            "dr2",          // 92  CV_REG_DR2
            "dr3",          // 93  CV_REG_DR3
            "dr4",          // 94  CV_REG_DR4
            "dr5",          // 95  CV_REG_DR5
            "dr6",          // 96  CV_REG_DR6
            "dr7",          // 97  CV_REG_DR7
            "???",          // 98
            "???",          // 99
            "???",          // 10
            "???",          // 101
            "???",          // 102
            "???",          // 103
            "???",          // 104
            "???",          // 105
            "???",          // 106
            "???",          // 107
            "???",          // 108
            "???",          // 109
            "GDTR",         // 110 CV_REG_GDTR
            "GDT",          // 111 CV_REG_GDTL
            "IDTR",         // 112 CV_REG_IDTR
            "IDT",          // 113 CV_REG_IDTL
            "LDTR",         // 114 CV_REG_LDTR
            "TR",           // 115 CV_REG_TR
            "???",          // 116
            "???",          // 117
            "???",          // 118
            "???",          // 119
            "???",          // 120
            "???",          // 121
            "???",          // 122
            "???",          // 123
            "???",          // 124
            "???",          // 125
            "???",          // 126
            "???",          // 127
            "st(0)",        // 128 CV_REG_ST0
            "st(1)",        // 129 CV_REG_ST1
            "st(2)",        // 130 CV_REG_ST2
            "st(3)",        // 131 CV_REG_ST3
            "st(4)",        // 132 CV_REG_ST4
            "st(5)",        // 133 CV_REG_ST5
            "st(6)",        // 134 CV_REG_ST6
            "st(7)",        // 135 CV_REG_ST7
            "CTR",          // 136 CV_REG_CTRL
            "STAT",         // 137 CV_REG_STAT
            "TAG",          // 138 CV_REG_TAG
            "FPIP",         // 139 CV_REG_FPIP
            "FPCS",         // 140 CV_REG_FPCS
            "FPDO",         // 141 CV_REG_FPDO
            "FPDS",         // 142 CV_REG_FPDS
            "ISEM",         // 143 CV_REG_ISEM
            "FPEIP",        // 144 CV_REG_FPEIP
            "FPED0",        // 145 CV_REG_FPEDO
        };

        public static void print(PdbParser pdbParser) {
            Console.WriteLine($"Guid: {pdbParser.guid}");

            Console.WriteLine();

            foreach (IDiaSymbol dataSymbol in pdbParser.dataById.Values) {
                Console.WriteLine(dataToString(dataSymbol, "", 0));
            }

            foreach (IDiaSymbol typedefSymbol in pdbParser.typedefById.Values) {
                Console.WriteLine(typedefToString(typedefSymbol));
            }

            Console.WriteLine();

            foreach (UDTInfo udtInfo in pdbParser.udtInfoById.Values) {
                if (udtInfo.udtSymbol.unmodifiedTypeId == 0 && udtInfo.namePrefix.Length == 0) {
                    Console.WriteLine(udtToString(udtInfo));
                    Console.WriteLine();
                }
            }

            foreach (FunctionInfo functionInfo in pdbParser.functionInfoById.Values) {
                if (functionInfo.functionSymbol.classParentId == 0) {
                    Console.WriteLine(functionToString(functionInfo));
                }
            }
        }

        private static string typedefToString(IDiaSymbol typedefSymbol, int indentLevel = 0) {
            StringBuilder sb = new StringBuilder()
                .Append(' ', indentLevel * 2)
                .Append("typedef ")
                .Append(typeToString(typedefSymbol.type))
                .Append(' ')
                .Append(typedefSymbol.name);
            return sb.ToString();
        }

        private static string udtToString(UDTInfo udtInfo, int indentLevel = 0) {
            StringBuilder sb = new StringBuilder()
                .Append(' ', indentLevel * 2)
                .Append(udtKindToString(udtInfo))
                .Append(' ')
                .Append(udtInfo.namePrefix)
                .Append(udtInfo.udtSymbol.name);
            if (udtInfo.baseClassSymbols.Count > 0) {
                sb.Append(" : ");
                for (int i = 0; i < udtInfo.baseClassSymbols.Count; i++) {
                    IDiaSymbol baseClassSymbol = udtInfo.baseClassSymbols[i];
                    if (i != 0) {
                        sb.Append(", ");
                    }
                    sb.Append(baseClassSymbol.name);
                }
            }
            foreach (IDiaSymbol typedefSymbol in udtInfo.typedefSymbols) {
                sb.AppendLine()
                    .Append(typedefToString(typedefSymbol, indentLevel + 1));
            }
            foreach (IDiaSymbol constantSymbol in udtInfo.constantSymbols) {
                sb.AppendLine()
                    .Append(dataToString(constantSymbol, "const", indentLevel + 1));
            }
            foreach (IDiaSymbol memberSymbol in udtInfo.memberSymbols) {
                sb.AppendLine()
                    .Append(dataToString(memberSymbol, "", indentLevel + 1));
            }
            foreach (FunctionInfo functionInfo in udtInfo.functionInfos) {
                sb.AppendLine()
                    .Append(functionToString(functionInfo, indentLevel + 1));
            }
            foreach (UDTInfo nestedUdtInfo in udtInfo.nestedUdtInfos) {
                sb.AppendLine()
                    .Append(udtToString(nestedUdtInfo, indentLevel + 1));
            }
            return sb.ToString();
        }

        private static string udtKindToString(UDTInfo udtInfo) {
            if (udtInfo.isEnum) {
                return "enum";
            }

            UdtKind udtKind = (UdtKind)udtInfo.udtSymbol.udtKind;
            switch (udtKind) {
                case UdtKind.UdtStruct:
                    return "struct";
                case UdtKind.UdtClass:
                    return "class";
                case UdtKind.UdtUnion:
                    return "union";
                case UdtKind.UdtInterface:
                    return "interface";
                default:
                    throw new NotImplementedException(udtKind.ToString());
            }
        }

        private static string functionToString(FunctionInfo functionInfo, int indentLevel = 0) {
            StringBuilder sb = new();
            sb.Append(' ', indentLevel * 2);
            string locationStr = locationToString(functionInfo.functionSymbol);
            if (locationStr.Length > 0) {
                sb.Append(locationStr)
                    .Append(' ');
            }
            CV_access_e access = (CV_access_e)functionInfo.functionSymbol.access;
            if (access != 0) {
                sb.Append(accessToString(access))
                    .Append(' ');
            }
            sb.Append(functionInfo.functionSymbol.name)
                .Append('(');
            for (int i = 0; i < functionInfo.argSymbols.Count; i++) {
                IDiaSymbol argSymbol = functionInfo.argSymbols[i];
                if (i != 0) {
                    sb.Append(", ");
                }
                sb.Append(typeToString(argSymbol.type))
                    .Append(' ')
                    .Append(argSymbol.name);
            }
            sb.Append(')');
            foreach (IDiaSymbol constantSymbol in functionInfo.constantSymbols) {
                sb.AppendLine()
                    .Append(dataToString(constantSymbol, "const", indentLevel + 1));
            }
            foreach (IDiaSymbol ptrSymbol in functionInfo.ptrSymbols) {
                sb.AppendLine()
                    .Append(dataToString(ptrSymbol, "ptr", indentLevel + 1));
            }
            foreach (IDiaSymbol argSymbol in functionInfo.argSymbols) {
                sb.AppendLine()
                    .Append(dataToString(argSymbol, "arg", indentLevel + 1));
            }
            foreach (IDiaSymbol localSymbol in functionInfo.localSymbols) {
                sb.AppendLine()
                    .Append(dataToString(localSymbol, "local", indentLevel + 1));
            }
            return sb.ToString();
        }

        private static string accessToString(CV_access_e access) {
            switch (access) {
                case 0:
                    return "";
                case CV_access_e.CV_private:
                    return "private";
                case CV_access_e.CV_protected:
                    return "protected";
                case CV_access_e.CV_public:
                    return "public";
                default:
                    throw new NotImplementedException(access.ToString());
            }
        }

        private static string dataToString(IDiaSymbol dataSymbol, string descriptor, int indentLevel) {
            StringBuilder sb = new StringBuilder()
                .Append(' ', indentLevel * 2);
            string locationString = locationToString(dataSymbol);
            if (locationString.Length > 0) {
                sb.Append(locationString)
                    .Append(' ');
            }
            CV_access_e access = (CV_access_e)dataSymbol.access;
            if (access != 0) {
                sb.Append(accessToString(access))
                    .Append(' ');
            }
            if (descriptor.Length > 0) {
                sb.Append('(')
                    .Append(descriptor)
                    .Append(") ");
            }
            if (dataSymbol.type != null) {
                sb.Append(typeToString(dataSymbol.type))
                    .Append(' ');
            }
            sb.Append(dataSymbol.name);
            if ((LocationType)dataSymbol.locationType == LocationType.LocIsConstant) {
                sb.Append(" = ")
                    .Append(dataSymbol.value);
            }
            return sb.ToString();
        }

        private static string typeToString(IDiaSymbol typeSymbol) {
            SymTagEnum tag = (SymTagEnum)typeSymbol.symTag;

            StringBuilder sb = new();
            if (tag != SymTagEnum.SymTagPointerType) {
                if (typeSymbol.constType != 0) {
                    sb.Append("const ");
                }

                if (typeSymbol.volatileType != 0) {
                    sb.Append("volatile ");
                }

                if (typeSymbol.unalignedType != 0) {
                    sb.Append("__unaligned ");
                }
            }

            switch (tag) {
                case SymTagEnum.SymTagUDT:
                case SymTagEnum.SymTagEnum:
                    sb.Append(typeSymbol.name);
                    break;
                case SymTagEnum.SymTagFunctionType:
                    sb.Append("function ");
                    break;
                case SymTagEnum.SymTagPointerType:
                    sb.Append(typeToString(typeSymbol.type));

                    if (typeSymbol.reference != 0) {
                        sb.Append('&');
                    } else {
                        sb.Append('*');
                    }

                    if (typeSymbol.constType != 0) {
                        sb.Append(" const");
                    }

                    if (typeSymbol.volatileType != 0) {
                        sb.Append(" volatile");
                    }

                    if (typeSymbol.unalignedType != 0) {
                        sb.Append(" __unaligned");
                    }
                    break;

                case SymTagEnum.SymTagArrayType:
                    IDiaSymbol baseTypeSymbol = typeSymbol.type;
                    sb.Append(typeToString(baseTypeSymbol));
                    if (typeSymbol.rank != 0) {
                        throw new NotImplementedException(typeSymbol.rank.ToString());
                    } else if (typeSymbol.count != 0) {
                        sb.Append('[')
                            .Append(typeSymbol.count)
                            .Append(']');
                    } else if (baseTypeSymbol.length == 0) {
                        sb.Append('[')
                            .Append(typeSymbol.length)
                            .Append(']');
                    } else {
                        sb.Append('[')
                            .Append(typeSymbol.length / baseTypeSymbol.length)
                            .Append(']');
                    }
                    break;

                case SymTagEnum.SymTagBaseType:
                    BasicType baseType = (BasicType)typeSymbol.baseType;

                    switch (baseType) {
                        case BasicType.btUInt:
                            sb.Append("unsigned ");
                            goto case BasicType.btInt;
                        case BasicType.btInt:
                            switch (typeSymbol.length) {
                                case 1:
                                    if (baseType == BasicType.btInt) {
                                        sb.Append("signed ");
                                    }

                                    sb.Append("char");
                                    break;

                                case 2:
                                    sb.Append("short");
                                    break;

                                case 4:
                                    sb.Append("int");
                                    break;

                                case 8:
                                    sb.Append("__int64");
                                    break;
                            }
                            break;
                        case BasicType.btFloat:
                            switch (typeSymbol.length) {
                                case 4:
                                    sb.Append("float");
                                    break;

                                case 8:
                                    sb.Append("double");
                                    break;
                            }
                            break;
                        case BasicType.btULong:
                            sb.Append("unsigned ");
                            goto case BasicType.btLong;
                        case BasicType.btLong:
                            sb.Append("long");
                            break;
                        case BasicType.btBool:
                            sb.Append("boo");
                            break;
                        case BasicType.btChar:
                            sb.Append("char");
                            break;
                        case BasicType.btVoid:
                            sb.Append("void");
                            break;
                        case BasicType.btHresult:
                            sb.Append("HRESULT");
                            break;
                        case BasicType.btWChar:
                            sb.Append("WCHAR");
                            break;
                        default:
                            sb.Append(baseType);
                            break;
                    }
                    break;

                case SymTagEnum.SymTagTypedef:
                    sb.Append(typeSymbol.name);
                    break;
                case SymTagEnum.SymTagData: // This really is member data, just print its location
                    sb.Append(locationToString(typeSymbol));
                    break;
                default:
                    throw new NotImplementedException(tag.ToString());
            }

            return sb.ToString();
        }

        private static string locationToString(IDiaSymbol symbol) {
            LocationType locationType = (LocationType)symbol.locationType;
            switch (locationType) {
                case LocationType.LocIsStatic:
                case LocationType.LocIsTLS:
                case LocationType.LocInMetaData:
                case LocationType.LocIsIlRel:
                    return $"[{symbol.relativeVirtualAddress:X}]";
                case LocationType.LocIsRegRel:
                    return $"[{registerToString((CV_HREG_e)symbol.registerId)}{offsetToString(symbol)}]";
                case LocationType.LocIsThisRel:
                    return $"[this{offsetToString(symbol)}]";
                case LocationType.LocIsBitField:
                    return $"[this{offsetToString(symbol)}]:bits_{symbol.bitPosition}_{symbol.bitPosition + symbol.length - 1}";
                case LocationType.LocIsEnregistered:
                    return $"[{registerToString((CV_HREG_e)symbol.registerId)}]";
                case LocationType.LocIsSlot:
                    return $"slot[{symbol.slot:X}]";
                case LocationType.LocIsConstant:
                case LocationType.LocIsNull:
                    return "";
                default:
                    throw new NotImplementedException(locationType.ToString());
            }
        }

        private static string offsetToString(IDiaSymbol symbol) {
            if (symbol.offset == 0) {
                return "";
            }

            return $"{(symbol.offset >= 0 ? '+' : '-')}{(symbol.offset >= 0 ? symbol.offset : -symbol.offset):X}";
        }

        private static string registerToString(CV_HREG_e register) {
            if (register == CV_HREG_e.CV_ALLREG_VFRAME) {
                register = CV_HREG_e.CV_REG_ESP;
            }

            return (int)register < REG_TO_NAME.Length
                ? REG_TO_NAME[(int)register]
                : register.ToString();
        }
    }
}
