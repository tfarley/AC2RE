using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions;

public class MaterialProperty {

    // MaterialProperty
    public uint nameId; // nameID
    public RMDataType dataType; // dataType
    // data
    public Waveform valWaveform;
    public RGBAColor valColor;
    public DataId valTextureDid;
    public bool valBool;
    public List<MaterialField> fields; // fields

    public MaterialProperty(AC2Reader data) {
        nameId = data.ReadUInt32();
        dataType = (RMDataType)data.ReadUInt32();
        uint dataLength = data.ReadUInt32();
        long startPos = data.BaseStream.Position;
        switch (dataType) {
            case RMDataType.WAVEFORM:
                valWaveform = new(data);
                break;
            case RMDataType.COLOR:
                valColor = data.ReadRGBAColorFull();
                break;
            case RMDataType.TEXTURE:
                valTextureDid = data.ReadDataId();
                break;
            case RMDataType.BOOL:
                valBool = data.ReadBoolean();
                break;
            default:
                throw new InvalidDataException(dataType.ToString());
        }
        uint readLength = (uint)(data.BaseStream.Position - startPos);
        if (readLength != dataLength) {
            throw new InvalidDataException(readLength.ToString());
        }
        fields = data.ReadList(() => new MaterialField(data));
    }
}
