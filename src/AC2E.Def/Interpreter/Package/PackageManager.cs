using System;

namespace AC2E.Def {

    public static class PackageManager {

        public static InterpReferenceMeta getReferenceMeta(Type type) {
            InterpReferenceMeta.Flag flags = InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SingletonPkg<>)) {
                flags |= InterpReferenceMeta.Flag.SINGLETON;
            }

            return new InterpReferenceMeta(flags, ReferenceType.HEAPOBJECT);
        }

        public static IPackage read(AC2Reader data, NativeType nativeType) {
            switch (nativeType) {
                case NativeType.AAHASH:
                    return new AAHash(data);
                case NativeType.AAMULTIHASH:
                    return new AAMultiHash(data);
                case NativeType.AARRAY:
                    return new AArray(data);
                case NativeType.AHASHSET:
                    return new AHashSet(data);
                case NativeType.ALHASH:
                    return new ALHash(data);
                case NativeType.ALIST:
                    return new AList(data);
                case NativeType.APPINFOHASH:
                    return new AppInfoHash(data);
                case NativeType.ARHASH:
                    return new ARHash<IPackage>(data);
                case NativeType.EXAMINATIONPROFILE:
                    return new ExaminationProfile(data);
                case NativeType.EXAMINATIONREQUEST:
                    return new ExaminationRequest(data);
                case NativeType.GAMEPLAYOPTIONSPROFILE:
                    return new GameplayOptionsProfile(data);
                case NativeType.GMKEYFRAME:
                    return new GMKeyframe(data);
                case NativeType.GMQUESTINFOLIST:
                    return new GMQuestInfoList(data);
                case NativeType.GMQUESTINFO:
                    return new GMQuestInfo(data);
                case NativeType.GMSCENEINFO:
                    return new GMSceneInfo(data);
                case NativeType.GMSCENEINFOLIST:
                    return new GMSceneInfoList(data);
                case NativeType.ICONDESC:
                    return new IconDesc(data);
                case NativeType.LAHASH:
                    return new LAHash(data);
                case NativeType.LAHASHSET:
                    return new LAHashSet(data);
                case NativeType.LAMULTIHASH:
                    return new LAMultiHash(data);
                case NativeType.LLIST:
                    return new LList(data);
                case NativeType.LRHASH:
                    return new LRHash<IPackage>(data);
                case NativeType.MISSILEPARAMETERS:
                    return new MissileParameters(data);
                case NativeType.NRHASH:
                    return new NRHash<IPackage, IPackage>(data);
                case NativeType.POSITION:
                    return new Position(data);
                case NativeType.RARRAY:
                    return new RArray<IPackage>(data);
                case NativeType.RLIST:
                    return new RList<IPackage>(data);
                case NativeType.SELECTIONINFO:
                    return new SelectionInfo(data);
                case NativeType.SHORTCUTINFO:
                    return new ShortcutInfo(data);
                case NativeType.STRINGINFO:
                    return new StringInfo(data);
                case NativeType.WPSTRING:
                    return new WPString(data);
                case NativeType.UISAVELOCATIONS:
                    return new UISaveLocations(data);
                case NativeType.VECTOR:
                    return new VectorPkg(data.ReadVector());
                case NativeType.VISUALDESC:
                    return new VisualDesc(data);
                default:
                    throw new NotImplementedException($"Unhandled read for native package type {nativeType}.");
            }
        }

        public static IPackage read(AC2Reader data, PackageType packageType) {
            switch (packageType) {
                case PackageType.AbilityCalculator:
                    return new AbilityCalculator(data);
                case PackageType.ActiveSkill:
                    return new ActiveSkill(data);
                case PackageType.ActiveSkillTemplate:
                    return new ActiveSkillTemplate(data);
                case PackageType.ActRegistry:
                    return new ActRegistry(data);
                case PackageType.AIVoterSwapEffect:
                    return new AIVoterSwapEffect(data);
                case PackageType.AllegianceData:
                    return new AllegianceData(data);
                case PackageType.AllegianceHierarchy:
                    return new AllegianceHierarchy(data);
                case PackageType.AllegianceNode:
                    return new AllegianceHierarchy.AllegianceNode(data);
                case PackageType.AllegianceProfile:
                    return new AllegianceProfile(data);
                case PackageType.ApplyEffect:
                    return new ApplyEffect(data);
                case PackageType.AttackHook:
                    return new AttackHook(data);
                case PackageType.AuraEffect:
                    return new AuraEffect(data);
                case PackageType.BiasProfile:
                    return new BiasProfile(data);
                case PackageType.ChannelData:
                    return new ChannelData(data);
                case PackageType.CItem:
                    return new CItem(data);
                case PackageType.ConsignerDesc:
                    return new ConsignerDesc(data);
                case PackageType.Consignment:
                    return new Consignment(data);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptor(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistry(data);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecord(data);
                case PackageType.CWeapon:
                    return new CWeapon(data);
                case PackageType.DefaultPermissionBlob:
                    return new DefaultPermissionBlob(data);
                case PackageType.DispelEffect:
                    return new DispelEffect(data);
                case PackageType.Eff_AIPetIgnore:
                    return new AIBehaviorEffect(data);
                case PackageType.Eff_BestowQuestKillFaisiSclavusFangchief:
                    return new QuestTriggerEffect(data);
                case PackageType.Eff_CompleteQuestBarnesKillArmoredilloStonyUndead:
                    return new QuestTriggerEffect(data);
                case PackageType.Eff_RemoveRecipe_BreastplateArtefonLordsHuman:
                    return new RemoveRecipeEffect(data);
                case PackageType.Eff_SetPlayerMaxContainerSizeApr03:
                    return new Effect(data);
                case PackageType.Eff_Tum_Me_FeralIntendant_DispelLeaderOfThePack:
                    return new DispelEffect(data);
                case PackageType.Eff_Tum_Me_Zealot_SpineRipper:
                    return new VitalOverTimeEffect(data);
                case PackageType.Eff_Tum_Mi_HiveKeeper_Daze:
                    return new AIVoterSwapEffect(data);
                case PackageType.Eff_Tsys_Player_Apply_GiveLife:
                    return new ApplyEffect(data);
                case PackageType.Effect:
                    return new Effect(data);
                case PackageType.EffectRecord:
                    return new EffectRecord(data);
                case PackageType.EffectRegistry:
                    return new EffectRegistry(data);
                case PackageType.EffectTypeFilter:
                    return new EffectTypeFilter(data);
                case PackageType.EquipItemProfile:
                    return new EquipItemProfile(data);
                case PackageType.EntityFilter:
                    return new EntityFilter(data);
                case PackageType.Fellow:
                    return new Fellow(data);
                case PackageType.Fellowship:
                    return new Fellowship(data);
                case PackageType.FellowVitals:
                    return new FellowVitals(data);
                case PackageType.FloatScaleDuple:
                    return new FloatScaleDuple(data);
                case PackageType.GameSaleProfile:
                    return new GameSaleProfile(data);
                case PackageType.GenesisEffect:
                    return new GenesisEffect(data);
                case PackageType.gmCEntity:
                    return new gmCEntity(data);
                case PackageType.gmEntity:
                    return new gmEntity(data);
                case PackageType.Hum_Ma_Enchanter_BladeOfFire:
                    return new ActiveSkillTemplate(data);
                case PackageType.InstantAuraEffect:
                    return new InstantAuraEffect(data);
                case PackageType.InventProfile:
                    return new InventProfile(data);
                case PackageType.InvEquipDesc:
                    return new InvEquipDesc(data);
                case PackageType.InvMoveDesc:
                    return new InvMoveDesc(data);
                case PackageType.InvTakeAllDesc:
                    return new InvTakeAllDesc(data);
                case PackageType.InvTransmuteAllDesc:
                    return new InvTransmuteAllDesc(data);
                case PackageType.Item:
                    return new Item(data);
                case PackageType.IWeapon:
                    return new IWeapon(data);
                case PackageType.LinkerEffect:
                    return new LinkerEffect(data);
                case PackageType.LogInfo:
                    return new LogInfo(data);
                case PackageType.MasterDIDListMember:
                    return new MasterDIDListMember(data);
                case PackageType.MasterList:
                    return new MasterList(data);
                case PackageType.MasterListMember:
                    return new MasterListMember(data);
                case PackageType.ParameterizedNumericEffect:
                    return new ParameterizedNumericEffect(data);
                case PackageType.PetData:
                    return new PetData(data);
                case PackageType.PhaseInfo:
                    return new PhaseInfo(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfile(data);
                case PackageType.QualitiesEffect:
                    return new QualitiesEffect(data);
                case PackageType.Quest:
                    return new Quest(data);
                case PackageType.QuestGGWDreamT:
                    return new QuestTemplate(data);
                case PackageType.QuestGlobals:
                    return new QuestGlobals(data);
                case PackageType.QuestTemplate:
                    return new QuestTemplate(data);
                case PackageType.QuestTriggerEffect:
                    return new QuestTriggerEffect(data);
                case PackageType.RecipeRecord:
                    return new RecipeRecord(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequest(data);
                case PackageType.SaleProfile:
                    return new SaleProfile(data);
                case PackageType.Skill:
                    return new Skill(data);
                case PackageType.SkillInfo:
                    return new SkillInfo(data);
                case PackageType.SkillRepository:
                    return new SkillRepository(data);
                case PackageType.SpearTemplate:
                    return new SpearTemplate(data);
                case PackageType.SpearTumerokSpineJavelin:
                    return new SpearTemplate(data);
                case PackageType.StaticAttackHook:
                    return new StaticAttackHook(data);
                case PackageType.StoreSorter:
                    return new StoreSorter(data);
                case PackageType.StoreTemplate:
                    return new StoreTemplate(data);
                case PackageType.StoreView:
                    return new StoreView(data);
                case PackageType.TargetLevelFilter:
                    return new TargetLevelFilter(data);
                case PackageType.Trade:
                    return new Trade(data);
                case PackageType.TransactionBlob:
                    return new TransactionBlob(data);
                case PackageType.TransparentNumericEffect:
                    return new TransparentNumericEffect(data);
                case PackageType.UsageBlob:
                    return new UsageBlob(data);
                case PackageType.UsageAction:
                    return new UsageAction(data);
                case PackageType.UsageDesc:
                    return new UsageDesc(data);
                case PackageType.UsagePermission:
                    return new UsagePermission(data);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfo(data);
                case PackageType.VitalOverTimeEffect:
                    return new VitalOverTimeEffect(data);
                case PackageType.Weapon:
                    return new Weapon(data);
                case PackageType.WeaponTemplate:
                    return new WeaponTemplate(data);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}.");
            }
        }
    }
}
