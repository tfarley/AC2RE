using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CSetup {

    public class BoneInfo {

        // BoneInfo
        public BoneType id; // id
        public uint parentIndex; // parent_index

        public BoneInfo(AC2Reader data) {
            parentIndex = data.ReadUInt32();
            id = data.ReadEnum<BoneType>();
        }
    }

    public class PlacementFrame {

        // PlacementFrame
        public List<TransformBase> keyframes; // keyFrame

        public PlacementFrame(AC2Reader data) {
            keyframes = data.ReadList(() => new TransformBase(data));
        }
    }

    public class LocationType {

        // LocationType
        public uint boneIndex; // bone_index
        public TransformBase transform; // transform

        public LocationType(AC2Reader data) {
            boneIndex = data.ReadUInt32();
            transform = new(data);
        }
    }

    public class DegradeInfo {

        public class DLDegradeInfo {

            // DLDegradeInfo
            public float visibleDistance; // m_fVisibleDistance
            public float nearDegradeDistance; // m_fNearDegradeDistance
            public float idealDegradeDistance; // m_fIdealDegradeDistance
            public float farDegradeDistance; // m_fFarDegradeDistance
            public float highDetailDistance; // m_fHighDetailDistance
            public float mediumDetailDistance; // m_fMediumDetailDistance

            public DLDegradeInfo(AC2Reader data) {
                visibleDistance = data.ReadSingle();
                nearDegradeDistance = data.ReadSingle();
                idealDegradeDistance = data.ReadSingle();
                farDegradeDistance = data.ReadSingle();
                highDetailDistance = data.ReadSingle();
                mediumDetailDistance = data.ReadSingle();
            }
        }

        // DegradeInfo
        public DLDegradeInfo[] degradeInfos; // m_DLDegradeInfo

        public DegradeInfo(AC2Reader data) {
            degradeInfos = new DLDegradeInfo[3];
            for (int i = 0; i < degradeInfos.Length; i++) {
                degradeInfos[i] = new(data);
            }
        }
    }

    // Const - globals
    public enum Flag : uint {
        NONE = 0,

        ALLOW_FREE_HEADING = 1 << 2, // ALLOW_FREE_HEADING 0x00000004
        HAS_PHYSICS_BSP = 1 << 3, // HAS_PHYSICS_BSP 0x00000008
    }

    // Enum ShadowType
    public enum ShadowType : uint {
        NONE, // SHADOWTYPE_NONE
        STATIC, // SHADOWTYPE_STATIC
        VOLUMETRIC, // SHADOWTYPE_VOLUMETRIC

        INVALID = 0x7FFFFFFF, // SHADOWTYPE_INVALID
    }

    // CSetup
    public DataId did; // m_DID
    public List<DataId> meshes; // num_mesh + mesh_info
    public List<BoneInfo> bones; // num_bones + bone_info
    public List<Cylsphere> cylspheres; // num_cylsphere + cylsphere
    public List<Sphere> spheres; // num_sphere + sphere
    public float stepDownHeight; // step_down_height
    public float stepUpHeight; // step_up_height
    public Flag flags; // has_physics_bsp + allow_free_heading
    public float height; // height
    public float radius; // radius
    public Sphere boundingSphere; // bounding_sphere
    public Sphere selectionSphere; // selection_sphere
    public Dictionary<HoldingLocation, LocationType> holdingLocations; // holding_locations
    public Dictionary<ConnectionPoint, LocationType> connectionPoints; // connection_points
    public Dictionary<uint, PlacementFrame> placementFrames; // placement_frames
    public DegradeInfo degradeInfo; // degrade_info
    public ShadowType shadowType; // render_info
    public float physicsContainmentRadius; // m_physics_containment_radius
    public BBox physicsBoundingBox; // m_physics_bounding_box

    public CSetup(AC2Reader data) {
        did = data.ReadDataId();
        flags = data.ReadEnum<Flag>();
        meshes = data.ReadList(data.ReadDataId);
        bones = data.ReadList(() => new BoneInfo(data));
        placementFrames = data.ReadDictionary(data.ReadUInt32, () => new PlacementFrame(data));
        holdingLocations = data.ReadDictionary(data.ReadEnum<HoldingLocation>, () => new LocationType(data));
        connectionPoints = data.ReadDictionary(data.ReadEnum<ConnectionPoint>, () => new LocationType(data));
        cylspheres = data.ReadList(() => new Cylsphere(data));
        spheres = data.ReadList(() => new Sphere(data));
        height = data.ReadSingle();
        radius = data.ReadSingle();
        stepUpHeight = data.ReadSingle();
        stepDownHeight = data.ReadSingle();
        boundingSphere = new(data);
        selectionSphere = new(data);
        degradeInfo = new(data);
        shadowType = data.ReadEnum<ShadowType>();
        physicsContainmentRadius = data.ReadSingle();
        physicsBoundingBox = new(data);
    }
}
