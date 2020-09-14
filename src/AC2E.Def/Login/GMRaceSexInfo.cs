using System.Collections.Generic;

namespace AC2E.Def {

    public class GMRaceSexInfo : IPackage {

        public NativeType nativeType => NativeType.GMRACESEXINFO;

        public DataId physObjDid; // _physObjDID
        public Dictionary<uint, uint> objInLocHash; // _objInLocHash
        public DataId aprFileDid; // _aprFileDID
        public DataId musicFileDid; // _musicFileDID

        public GMRaceSexInfo(AC2Reader data) {
            physObjDid = data.ReadDataId();
            aprFileDid = data.ReadDataId();
            musicFileDid = data.ReadDataId();
            objInLocHash = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
        }
    }
}
