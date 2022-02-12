using System;

namespace AC2RE.Definitions;

public class NetAuthenticator {

    // Enum DEFAULT_AUTHFLAGS
    [Flags]
    public enum AuthFlag : uint {
        NONE = 0,
        ENABLECRYPTO = 1 << 0, // AUTHFLAGS_ENABLECRYPTO 0x00000001
        ADMINACCTOVERRIDE = 1 << 1, // AUTHFLAGS_ADMINACCTOVERRIDE 0x00000002
        EXTRADATA = 1 << 2, // AUTHFLAGS_EXTRADATA + AUTHFLAGS_LASTDEFAULT 0x00000004
    }

    // NetAuthenticator
    public AuthType authType; // m_dwAuthType
    public AuthFlag authFlags; // m_dwAuthFlags
    public uint connectionSeq; // m_dwConnectionSequenceNumber
    public string accountName; // m_Account
    public string accountToLogonAs; // m_AccountToLogonAs
    public byte[] cryptoData; // m_CryptoData
    public byte[] extraData; // m_ExtraData

    public NetAuthenticator(AC2Reader data) {
        authType = data.ReadEnum<AuthType>();
        authFlags = data.ReadEnum<AuthFlag>();
        connectionSeq = data.ReadUInt32();
        accountName = data.ReadString();
        if (authFlags.HasFlag(AuthFlag.ADMINACCTOVERRIDE)) {
            accountToLogonAs = data.ReadString();
        }
        int cryptoDataLength = data.ReadInt32();
        cryptoData = data.ReadBytes(cryptoDataLength);
        int extraDataLength = data.ReadInt32();
        extraData = data.ReadBytes(extraDataLength);
    }
}
