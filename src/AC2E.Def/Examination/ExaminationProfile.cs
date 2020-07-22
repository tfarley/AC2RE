﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class ExaminationProfile : IPackage {

        public NativeType nativeType => NativeType.EXAMINATIONPROFILE;

        public ExaminationRequest _request;
        public List<ExaminationDataNode> _nodeList;
        public uint unk1;

        public ExaminationProfile() {

        }

        public ExaminationProfile(AC2Reader data) {
            _request = new ExaminationRequest(data);
            _nodeList = data.ReadList(() => new ExaminationDataNode(data));
            unk1 = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            _request.write(data);
            data.Write(_nodeList, v => v.write(data));
            data.Write(unk1);
        }
    }
}