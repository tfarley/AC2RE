using System.Collections.Generic;

namespace AC2E.Def {

    public class GMRaceSexInfo : IPackage {

        public NativeType nativeType => NativeType.GMRACESEXINFO;

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
}
