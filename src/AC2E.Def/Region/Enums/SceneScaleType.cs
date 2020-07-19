namespace AC2E.Def {

    // Enum SceneScaleType::Type
    public enum SceneScaleType : uint {
        SCENE_SCALE_RANDOM = 0,
        SCENE_SCALE_XY_RELATED = 1,
        SCENE_SCALE_XZ_RELATED = 2,
        SCENE_SCALE_YZ_RELATED = 3,
        SCENE_SCALE_UNIFORM = 4,
        DEPTH_SCALE_HEIGHT_XY_RANDOM = 5,
        DEPTH_SCALE_HEIGHT_XY_UNIFORM = 6,
        DEPTH_SCALE_SIZE = 7,
        SLOPE_SCALE_XY = 8,
        SLOPE_SCALE_SIZE = 9,
        INVALID = 10,
    }
}
