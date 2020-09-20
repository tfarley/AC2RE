﻿namespace AC2E.Def {

    public class TravelUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.TravelUsageBlob;

        public IPackage travelRec; // m_travelRec
        public uint scene; // m_siScene

        public TravelUsageBlob() : base() {

        }

        public TravelUsageBlob(AC2Reader data) : base(data) {
            data.ReadPkg<IPackage>(v => travelRec = v); // TODO: TravelRecord
            scene = data.ReadUInt32();
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.WritePkg(travelRec);
            data.Write(scene);
        }
    }
}