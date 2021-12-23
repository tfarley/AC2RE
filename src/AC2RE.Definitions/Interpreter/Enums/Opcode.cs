namespace AC2RE.Definitions;

// Const O_*
public enum Opcode : byte {
    UNDEF = 0, // O_UNDEF
    NOOP = 0x01, // O_NOOP
    PUSH = 0x02, // O_PUSH
    PUSHV = 0x03, // O_PUSHV
    POP = 0x04, // O_POP
    POPN = 0x05, // O_POPN
    NEW = 0x06, // O_NEW
    NEW_NATIVE = 0x07, // O_NEW_NATIVE
    NEW_STRING = 0x08, // O_NEW_STRING
    PUSH_FRAME = 0x09, // O_PUSH_FRAME
    POP_FRAME = 0x0A, // O_POP_FRAME
    CALL = 0x0B, // O_CALL
    CALL_EFUN = 0x0C, // O_CALL_EFUN
    ADD = 0x0D, // O_ADD
    SUB = 0x0E, // O_SUB
    MULT = 0x0F, // O_MULT
    DIV = 0x10, // O_DIV
    MOD = 0x11, // O_MOD
    NEGATE = 0x12, // O_NEGATE
    INC = 0x13, // O_INC
    DEC = 0x14, // O_DEC
    FADD = 0x15, // O_FADD
    FSUB = 0x16, // O_FSUB
    FMULT = 0x17, // O_FMULT
    FDIV = 0x18, // O_FDIV
    FNEGATE = 0x19, // O_FNEGATE
    FINC = 0x1A, // O_FINC
    FDEC = 0x1B, // O_FDEC
    I2F = 0x1C, // O_I2F
    F2I = 0x1D, // O_F2I
    I2I = 0x1E, // O_I2I
    F2F = 0x1F, // O_F2F
    F2T = 0x20, // O_F2T

    LOAD = 0x22, // O_LOAD
    LOADTHIS = 0x23, // O_LOADTHIS
    STORE = 0x24, // O_STORE
    RLOAD = 0x25, // O_RLOAD
    RSTORE = 0x26, // O_RSTORE
    SADDR = 0x27, // O_SADDR
    RADDR = 0x28, // O_RADDR
    IF = 0x29, // O_IF
    RJMP = 0x2A, // O_RJMP
    SWITCH = 0x2B, // O_SWITCH
    L_NOT = 0x2C, // O_L_NOT
    L_OR = 0x2D, // O_L_OR
    L_AND = 0x2E, // O_L_AND
    L_EQ = 0x2F, // O_L_EQ
    L_NEQ = 0x30, // O_L_NEQ
    L_SLT = 0x31, // O_L_SLT
    L_SGT = 0x32, // O_L_SGT
    L_SLTE = 0x33, // O_L_SLTE
    L_SGTE = 0x34, // O_L_SGTE
    L_ULT = 0x35, // O_L_ULT
    L_UGT = 0x36, // O_L_UGT
    L_ULTE = 0x37, // O_L_ULTE
    L_UGTE = 0x38, // O_L_UGTE
    L_FNOT = 0x39, // O_L_FNOT
    L_FOR = 0x3A, // O_L_FOR
    L_FAND = 0x3B, // O_L_FAND
    L_FEQ = 0x3C, // O_L_FEQ
    L_FNEQ = 0x3D, // O_L_FNEQ
    L_FLT = 0x3E, // O_L_FLT
    L_FGT = 0x3F, // O_L_FGT
    L_FLTE = 0x40, // O_L_FLTE
    L_FGTE = 0x41, // O_L_FGTE
    B_NEGATE = 0x42, // O_B_NEGATE
    B_OR = 0x43, // O_B_OR
    B_XOR = 0x44, // O_B_XOR
    B_AND = 0x45, // O_B_AND
    B_LSHIFT = 0x46, // O_B_LSHIFT
    B_RSHIFT = 0x47, // O_B_RSHIFT
    L_REQ = 0x48, // O_L_REQ
    L_RNEQ = 0x49, // O_L_RNEQ
    END = 0x4A, // O_END
    OVERFLOW = 0x4B, // O_OVERFLOW
    DEAD_CODE = 0x4C, // O_DEAD_CODE
    OPTIMIZED_STORE_POPN = 0x4D, // O_OPTIMIZED_STORE_POPN

    OPTIMIZED_THIS_CALL = 0x4F, // O_OPTIMIZED_THIS_CALL
    OPTIMIZED_THIS_CALLEFUN = 0x50, // O_OPTIMIZED_THIS_CALLEFUN
    OPTIMIZED_LOAD_CALL = 0x51, // O_OPTIMIZED_LOAD_CALL
    OPTIMIZED_LOAD_CALLEFUN = 0x52, // O_OPTIMIZED_LOAD_CALLEFUN
    OPTIMIZED_RLOAD_CALL = 0x53, // O_OPTIMIZED_RLOAD_CALL
    OPTIMIZED_RLOAD_CALLEFUN = 0x54, // O_OPTIMIZED_RLOAD_CALLEFUN
    OPTIMIZED_THIS_RLOAD = 0x55, // O_OPTIMIZED_THIS_RLOAD
    CALLSTATIC = 0x56, // O_CALLSTATIC

    U2U = 0x60, // O_U2U
    B_URSHIFT = 0x61, // O_B_URSHIFT
    U2F = 0x62, // O_U2F / O_MAXIMUM_OPCODE_WITHOUT_FLAGS

    DWORD_FLAG = 0x80, // O_DWORD_FLAG

    MAX_OPCODES = 0xFF, // O_MAX_OPCODES / O_TOTAL_OPCODE
}
