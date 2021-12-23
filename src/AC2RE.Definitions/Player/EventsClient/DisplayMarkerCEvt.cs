namespace AC2RE.Definitions;

public class DisplayMarkerCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Player__DisplayMarker;

    // WM_Player::PostCEvt_DisplayMarker
    public StringInfo text; // _msg
    public DataId markerDid; // _marker
    public CellId cell; // _cell
    public InstanceId id; // _id
    public uint type; // _type // TODO: MarkerType (has enum mapper id 0x2300010C, but not in dat)

    public DisplayMarkerCEvt() {

    }

    public DisplayMarkerCEvt(AC2Reader data) {
        text = data.UnpackPackage<StringInfo>();
        markerDid = data.UnpackDataId();
        cell = new(data.UnpackUInt32());
        id = data.UnpackInstanceId();
        type = data.UnpackUInt32();
    }

    public void write(AC2Writer data) {
        data.Pack(text);
        data.Pack(markerDid);
        data.Pack(cell.id);
        data.Pack(id);
        data.Pack(type);
    }
}
