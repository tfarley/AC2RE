namespace AC2RE.Definitions {

    // Const *_NativeType, updated with gmCNativeFactory::GetNativeTypeName
    public enum NativeType : uint {
        Undef = 0, // Undef_NativeType
        AAHash = 1, // AAHash_NativeType
        AAHashIter = 2, // AAHashIter_NativeType
        AAMultiHash = 3, // AAMultiHash_NativeType
        AAMultiHashIter = 4, // AAMultiHashIter_NativeType
        AArray = 5, // AArray_NativeType
        AHashSet = 6, // AHashSet_NativeType
        AHashSetIter = 7, // AHashSetIter_NativeType
        ALHash = 8, // ALHash_NativeType
        ALHashIter = 9, // ALHashIter_NativeType
        AList = 10, // AList_NativeType
        AListIter = 11, // AListIter_NativeType
        ALMultiHash = 12, // ALMultiHash_NativeType
        ALMultiHashIter = 13, // ALMultiHashIter_NativeType
        AppearanceTableAdaptor = 14, // AppearanceTableAdaptor_NativeType
        AppInfoHash = 15, // AppInfoHash_NativeType
        AppliedAppearanceKey = 16, // AppliedAppearanceKey_NativeType
        ARHash = 17, // ARHash_NativeType
        ARHashIter = 18, // ARHashIter_NativeType
        ARMultiHash = 19, // ARMultiHash_NativeType
        ARMultiHashIter = 20, // ARMultiHashIter_NativeType

        BaseProperty = 22, // BaseProperty_NativeType
        BehaviorParams = 23, // BehaviorParams_NativeType
        CDBForm = 24, // CDBForm_NativeType
        CollideProperty = 25, // CollideProperty_NativeType
        CollisionParameters = 26, // CollisionParameters_NativeType
        CollisionProfile = 27, // CollisionProfile_NativeType

        DetectionParameters = 29, // DetectionParameters_NativeType
        DetectionProfile = 30, // DetectionProfile_NativeType
        DetectionReportType = 31, // DetectionReportType_NativeType

        EntityDesc = 34, // EntityDesc_NativeType

        EtherealPhysicsType = 36, // EtherealPhysicsType_NativeType
        EtherealWeenieType = 37, // EtherealWeenieType_NativeType

        PFileNode = 40, // PFileNode_NativeType
        PFileParser = 41, // PFileParser_NativeType
        Frame = 42, // Frame_NativeType
        FXAndBehaviorInfo = 43, // FXAndBehaviorInfo_NativeType
        GameTime = 44, // GameTime_NativeType
        Heading = 45, // Heading_NativeType
        IconDesc = 46, // IconDesc_NativeType

        LAHash = 50, // LAHash_NativeType
        LAHashIter = 51, // LAHashIter_NativeType
        LAMultiHash = 52, // LAMultiHash_NativeType
        LAMultiHashIter = 53, // LAMultiHashIter_NativeType

        LArray = 55, // LArray_NativeType
        LinkRequirements = 56, // LinkRequirements_NativeType
        LLHash = 57, // LLHash_NativeType
        LLHashIter = 58, // LLHashIter_NativeType
        LList = 59, // LList_NativeType
        LListIter = 60, // LListIter_NativeType
        LLMultiHash = 61, // LLMultiHash_NativeType
        LLMultiHashIter = 62, // LLMultiHashIter_NativeType
        LOSParameters = 63, // LOSParameters_NativeType
        LRHash = 64, // LRHash_NativeType
        LRHashIter = 65, // LRHashIter_NativeType
        LRMultiHash = 66, // LRMultiHash_NativeType
        LRMultiHashIter = 67, // LRMultiHashIter_NativeType

        MissileInfo = 69, // MissileInfo_NativeType
        MissileParameters = 70, // MissileParameters_NativeType
        MissileTargetingParameters = 71, // MissileTargetingParameters_NativeType
        MotionValues = 72, // MotionValues_NativeType
        MovementParameters = 73, // MovementParameters_NativeType
        MovementReturn = 74, // MovementReturn_NativeType
        NAHash = 75, // NAHash_NativeType
        NAHashIter = 76, // NAHashIter_NativeType
        NRHash = 77, // NRHash_NativeType
        NRHashIter = 78, // NRHashIter_NativeType
        ObjCollisionProfile = 79, // ObjCollisionProfile_NativeType
        PackBuffer = 80, // PackBuffer_NativeType
        Path = 81, // Path_NativeType
        PathFinder = 82, // PathFinder_NativeType
        PathFinder_Normal = 83, // PathFinder_Normal_NativeType
        PathNodeData = 84, // PathNodeData_NativeType

        PhysicsStory = 87, // PhysicsStory_NativeType
        PlacesTable = 88, // PlacesTable_NativeType
        Plane = 89, // Plane_NativeType
        Position = 90, // Position_NativeType
        PropertyCollection = 91, // PropertyCollection_NativeType
        PropertyGroup = 92, // PropertyGroup_NativeType

        QualifiedDataIDArray = 94, // QualifiedDataIDArray_NativeType

        RandomSelectionTable_Int = 96, // RandomSelectionTable_Int_NativeType
        RArray = 97, // RArray_NativeType
        Ray = 98, // Ray_NativeType

        RGBAColor = 100, // RGBAColor_NativeType
        RList = 101, // RList_NativeType
        RListIter = 102, // RListIter_NativeType
        SetPositionStruct = 103, // SetPositionStruct_NativeType
        Sphere = 104, // Sphere_NativeType
        StoryHookData = 105, // StoryHookData_NativeType
        StringInfo = 106, // StringInfo_NativeType
        TabooTableAdaptor = 107, // TabooTableAdaptor_NativeType

        UISaveLocations = 111, // UISaveLocations_NativeType
        Vector = 112, // Vector_NativeType
        VisualDesc = 113, // VisualDesc_NativeType

        VMData = 115, // VMData_NativeType

        wpstring = 118, // wpstring_NativeType
        WState = 119, // WState_NativeType
        CRSData = 120, // CRSData_NativeType
        PathPlanManager = 121, // PathPlanManager_NativeType

        WBookKeeper = 125, // WBookKeeper_NativeType
        WDistributor = 126, // WDistributor_NativeType
        WInterface = 127, // WInterface_NativeType
        WPhysicsObject = 128, // WPhysicsObject_NativeType
        WRepository = 129, // WRepository_NativeType
        AllegianceDataAdaptor = 130, // AllegianceDataAdaptor_NativeType
        AllegianceProfileAdaptor = 131, // AllegianceProfileAdaptor_NativeType

        FellowAdaptor = 147, // FellowAdaptor_NativeType
        FellowshipAdaptor = 148, // FellowshipAdaptor_NativeType

        DamageTextBlob = 151, // DamageTextBlob_NativeType
        EffectUINode = 152, // EffectUINode_NativeType
        ExaminationProfile = 153, // ExaminationProfile_NativeType
        ExaminationRequest = 154, // ExaminationRequest_NativeType
        GameplayOptionsProfile = 155, // GameplayOptionsProfile_NativeType
        gmActInfo = 156, // gmActInfo_NativeType
        gmActInfoList = 157, // gmActInfoList_NativeType
        gmCharGenResult = 158, // gmCharGenResult_NativeType
        gmKeyframe = 159, // gmKeyframe_NativeType
        gmQuestInfo = 160, // gmQuestInfo_NativeType
        gmQuestInfoList = 161, // gmQuestInfoList_NativeType
        gmRaceSexInfo = 162, // gmRaceSexInfo_NativeType
        gmSceneInfo = 163, // gmSceneInfo_NativeType
        gmSceneInfoList = 164, // gmSceneInfoList_NativeType
        IngredientAdaptor = 165, // IngredientAdaptor_NativeType

        RecipeAdaptor = 170, // RecipeAdaptor_NativeType
        SelectionInfo = 171, // SelectionInfo_NativeType
        ShortcutInfo = 172, // ShortcutInfo_NativeType

        SkillUINode = 174, // SkillUINode_NativeType
        TurbineUserStatus = 175, // TurbineUserStatus_NativeType

        CliqueConstraints = 177, // CliqueConstraints_NativeType
        CliqueManager = 178, // CliqueManager_NativeType

        gmWInterface = 180, // gmWInterface_NativeType

        DBCache = 6848, // DBCache_NativeType

        CRSDataBase = 6850, // CRSDataBase_NativeType
        AAMultiMap = 6851, // AAMultiMap_NativeType was 13969
        AAMultiMapIter = 6852, // AAMultiMapIter_NativeType was 13970
        ALMultiMap = 6853, // ALMultiMap_NativeType was 13971
        ALMultiMapIter = 6854, // ALMultiMapIter_NativeType was 13972
        EntityLinkDesc = 6855, // EntityLinkDesc_NativeType was 14240
        LAHashSet = 6856, // LAHashSet_NativeType was 11787
        LAHashSetIter = 6857, // LAHashSetIter_NativeType was 11788
        LAMultiMap = 6858, // LAMultiMap_NativeType was 13973
        LAMultiMapIter = 6859, // LAMultiMapIter_NativeType was 13974
        LLMultiMap = 6860, // LLMultiMap_NativeType was 13975
        LLMultiMapIter = 6861, // LLMultiMapIter_NativeType was 13976

        CaseInsensitiveWPString = 6863, // CaseInsensitiveWPString_NativeType was 13631

        CraftSkillAdaptor = 6868, // CraftSkillAdaptor_NativeType was 13221

        UIShop = 6870, // UIShop_NativeType was 14346
        UIShopSale = 6871, // UIShopSale_NativeType was 14347

        GRSManager = 13174, // GRSManager_NativeType
    }
}
