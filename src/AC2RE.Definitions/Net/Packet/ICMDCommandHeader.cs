namespace AC2RE.Definitions;

public class ICMDCommandHeader {

    // CICMDCommandStruct
    public ICMDCommand cmd; // Cmd
    public uint param; // Param

    public ICMDCommandHeader() {

    }

    public ICMDCommandHeader(AC2Reader data) {
        cmd = data.ReadEnum<ICMDCommand>();
        param = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(cmd);
        data.Write(param);
    }
}
