using System.Collections.Generic;

namespace AC2RE.Definitions;

public class GMRaceSexInfo : IHeapObject {

    public NativeType nativeType => NativeType.gmRaceSexInfo;

    public DataId physObjDid; // _physObjDID
    public Dictionary<uint, uint> objInLocHash; // _objInLocHash
    public DataId aprDid; // _aprFileDID
    public DataId musicDid; // _musicFileDID

    public GMRaceSexInfo(AC2Reader data) {
        physObjDid = data.ReadDataId();
        aprDid = data.ReadDataId();
        musicDid = data.ReadDataId();
        objInLocHash = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
    }
}
