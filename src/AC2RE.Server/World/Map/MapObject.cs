using AC2RE.Definitions;

namespace AC2RE.Server;

internal class MapObject {

    public readonly InstanceId id;
    public readonly DataId entityDid;
    public readonly Position position;
    public readonly float scale;
    public readonly StringInfo? name;

    public MapObject(InstanceId id, DataId entityDid, Position position, float scale, StringInfo? name = null) {
        this.id = id;
        this.entityDid = entityDid;
        this.position = position;
        this.scale = scale;
        this.name = name;
    }
}
