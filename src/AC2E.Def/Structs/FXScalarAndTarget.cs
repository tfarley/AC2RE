﻿using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Def.Structs {

    public class FXScalarAndTarget {

        public float scalar; // m_scalar
        public InstanceId targetId; // m_target_id

        public FXScalarAndTarget(BinaryReader data) {
            scalar = data.ReadSingle();
            targetId = data.ReadInstanceId();
        }
    }
}
