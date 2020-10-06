using System.Collections.Generic;

namespace AC2E.Def {

    public class MaterialModifierAtom {

        public float priority; // m_priority
        public DataId modifierDid; // m_modifierDID
        public DataId templateDid; // m_templateDID
        public uint meshType; // m_meshType
        public uint materialType; // m_materialType

        public MaterialModifierAtom(AC2Reader data) {
            priority = data.ReadSingle();
            templateDid = data.ReadDataId();
            modifierDid = data.ReadDataId();
            meshType = data.ReadUInt32();
            materialType = data.ReadUInt32();
        }
    }

    public class OpacityAtom {

        public float priority; // m_priority
        public float opacity; // m_opacity

        public OpacityAtom(AC2Reader data) {
            priority = data.ReadSingle();
            opacity = data.ReadSingle();
        }
    }

    public class MeshReplacementAtom {

        public float priority; // m_priority
        public DataId newMeshDid; // m_newMeshDID
        public uint meshType; // m_meshType

        public MeshReplacementAtom(AC2Reader data) {
            priority = data.ReadSingle();
            meshType = data.ReadUInt32();
            newMeshDid = data.ReadDataId();
        }
    }

    public class PGDReplacementAtom {

        public float priority; // m_priority
        public PartGroupDataDesc newPgd; // m_newPGD

        public PGDReplacementAtom(AC2Reader data) {
            priority = data.ReadSingle();
            newPgd = new PartGroupDataDesc(data);
        }
    }

    public class FXAtom {

        public List<FxId> fxIds; // m_fx_ids
        public List<FXData> fxDatas; // m_fx_data

        public FXAtom(AC2Reader data) {
            fxIds = data.ReadList(() => (FxId)data.ReadUInt32());
            fxDatas = data.ReadList(() => new FXData(data));
        }
    }

    public class AtomCollection {

        public float intensity; // m_intensity
        public List<MaterialModifierAtom> materialModifiers; // m_materialModifiers
        public List<OpacityAtom> opacities; // m_opacities
        public List<MeshReplacementAtom> meshReplacements; // m_meshReplacements
        public List<PGDReplacementAtom> pgdReplacements; // m_PGDReplacements
        public List<FXAtom> fx; // m_fx
        public List<IconLayerDesc> iconModifiers; // m_iconModifiers

        public AtomCollection(AC2Reader data) {
            intensity = data.ReadSingle();
            materialModifiers = data.ReadList(() => new MaterialModifierAtom(data));
            opacities = data.ReadList(() => new OpacityAtom(data));
            meshReplacements = data.ReadList(() => new MeshReplacementAtom(data));
            pgdReplacements = data.ReadList(() => new PGDReplacementAtom(data));
            fx = data.ReadList(() => new FXAtom(data));
            iconModifiers = data.ReadList(() => new IconLayerDesc(data));
        }
    }
}
