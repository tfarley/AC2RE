public class CatTransport : ALog.Category {
    private CatTransport() { }
    public static readonly CatTransport i = new CatTransport();
    public string name => "NetTransport";
    public bool disabledByDefault => true;
}

public class CatProto : ALog.Category {
    private CatProto() { }
    public static readonly CatProto i = new CatProto();
    public string name => "NetProtocol";
    public bool disabledByDefault => true;
}
