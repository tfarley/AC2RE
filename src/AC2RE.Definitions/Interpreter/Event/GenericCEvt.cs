namespace AC2RE.Definitions;

public class GenericCEvt : IClientEvent {

    public ClientEventFunctionId funcId { get; set; }

    public byte[] payload;

    public void write(AC2Writer data) {
        data.Write(payload);
    }
}
