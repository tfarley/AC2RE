﻿using AC2E.Def;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class ExaminationProfilePkg : IPackage {

        public NativeType nativeType => NativeType.EXAMINATIONPROFILE;

        public ExaminationRequestPkg _request;
        public List<ExaminationDataNode> _nodeList;
        public uint unk1;

        public ExaminationProfilePkg() {

        }

        public ExaminationProfilePkg(BinaryReader data) {
            _request = new ExaminationRequestPkg(data);
            _nodeList = data.ReadList(() => new ExaminationDataNode(data));
            unk1 = data.ReadUInt32();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            _request.write(data, references);
            data.Write(_nodeList, v => v.write(data));
            data.Write(unk1);
        }
    }
}
