using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public static class PackageManager {

        private static PackageTypes packageTypes;

        public static void loadPackageTypes(DatReader datReader) {
            DataId wlibDid = new DataId(0x56000005);
            using (AC2Reader data = datReader.getFileReader(wlibDid)) {
                var wlib = new WLib(data);
                packageTypes = new PackageTypes();
                foreach (ByteStream.ExportData export in wlib.byteStream.exports) {
                    packageTypes.add(export.args.packageTypeId, export.args.parentIndex);
                }
                packageTypes.calculate();
            }
        }

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
                case NativeType.BEHAVIORPARAMS:
                    return new BehaviorParams(data);
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
                case NativeType.LARRAY:
                    return new LArray(data);
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
                case NativeType.SKILLUINODE:
                    return new SkillUINode(data);
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
            IPackage package = readInternal(data, packageType);

            // Deserialize as the most derived "known" package type
            if (package == null && packageTypes != null) {
                List<PackageTypeId> packageTypeHierarchy = packageTypes.getPackageTypeHierarchy(new PackageTypeId((uint)packageType));
                foreach (PackageTypeId packageTypeId in packageTypeHierarchy) {
                    package = readInternal(data, (PackageType)packageTypeId.id);
                    if (package != null) {
                        break;
                    }
                }
            }

            if (package == null) {
                throw new NotImplementedException($"Unhandled read for package type {packageType}.");
            }

            return package;
        }

        private static IPackage readInternal(AC2Reader data, PackageType packageType) {
            switch (packageType) {
                case PackageType.Act:
                    return new Act(data);
                case PackageType.ActiveSkill:
                    return new ActiveSkill(data);
                case PackageType.ActRegistry:
                    return new ActRegistry(data);
                case PackageType.ActTemplate:
                    return new ActTemplate(data);
                case PackageType.AdvancementTable:
                    return new AdvancementTable(data);
                case PackageType.Agent:
                    return new Agent(data);
                case PackageType.AIPetEffect:
                    return new AIPetEffect(data);
                case PackageType.AITauntDetauntEffect:
                    return new AITauntDetauntEffect(data);
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
                case PackageType.AttributeSkill:
                    return new AttributeSkill(data);
                case PackageType.AuraEffect:
                    return new AuraEffect(data);
                case PackageType.BiasProfile:
                    return new BiasProfile(data);
                case PackageType.BindRecipeAction:
                    return new BindRecipeAction(data);
                case PackageType.CAExcavationPoint:
                    return new CAExcavationPoint(data);
                case PackageType.ChainedInstantEffect:
                    return new ChainedInstantEffect(data);
                case PackageType.ChainedNumericEffect:
                    return new ChainedNumericEffect(data);
                case PackageType.ChannelData:
                    return new ChannelData(data);
                case PackageType.ClassFilter:
                    return new ClassFilter(data);
                case PackageType.Clothing:
                    return new Clothing(data);
                case PackageType.ComboEffect:
                    return new ComboEffect(data);
                case PackageType.ConsignerDesc:
                    return new ConsignerDesc(data);
                case PackageType.Consignment:
                    return new Consignment(data);
                case PackageType.Container:
                    return new Container(data);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptor(data);
                case PackageType.CountdownEffect:
                    return new CountdownEffect(data);
                case PackageType.CraftCheckEntry:
                    return new CraftCheckEntry(data);
                case PackageType.CraftRandomEntry:
                    return new CraftRandomEntry(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistry(data);
                case PackageType.CraftSkill:
                    return new CraftSkill(data);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecord(data);
                case PackageType.CustomFailureRecipeAction:
                    return new CustomFailureRecipeAction(data);
                case PackageType.CustomSuccessRecipeAction:
                    return new CustomSuccessRecipeAction(data);
                case PackageType.DestroyRecipeAction:
                    return new DestroyRecipeAction(data);
                case PackageType.DefaultPermissionBlob:
                    return new DefaultPermissionBlob(data);
                case PackageType.Door:
                    return new Door(data);
                case PackageType.DurabilityFilter:
                    return new DurabilityFilter(data);
                case PackageType.Eff_UseXPStone:
                    return new Eff_UseXPStone(data);
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
                case PackageType.ExperienceEffect:
                    return new ExperienceEffect(data);
                case PackageType.Fellow:
                    return new Fellow(data);
                case PackageType.Fellowship:
                    return new Fellowship(data);
                case PackageType.FellowVitals:
                    return new FellowVitals(data);
                case PackageType.FineItemClassFilter:
                    return new FineItemClassFilter(data);
                case PackageType.FlagRecipeAction:
                    return new FlagRecipeAction(data);
                case PackageType.FloatScaleDuple:
                    return new FloatScaleDuple(data);
                case PackageType.GameEventEffect:
                    return new GameEventEffect(data);
                case PackageType.GameSaleProfile:
                    return new GameSaleProfile(data);
                case PackageType.gmCEntity:
                    return new gmCEntity(data);
                case PackageType.gmEntity:
                    return new gmEntity(data);
                case PackageType.GrantRecipeEffect:
                    return new GrantRecipeEffect(data);
                case PackageType.HarmonizeRecipeAction:
                    return new HarmonizeRecipeAction(data);
                case PackageType.HotspotEffect:
                    return new HotspotEffect(data);
                case PackageType.Ingredient:
                    return new Ingredient(data);
                case PackageType.InstantBehaviorEffect:
                    return new InstantBehaviorEffect(data);
                case PackageType.InstantVitalEffect:
                    return new InstantVitalEffect(data);
                case PackageType.InteractionTable:
                    return new InteractionTable(data);
                case PackageType.Inventory:
                    return new Inventory(data);
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
                case PackageType.ItemEffectRecipeAction:
                    return new ItemEffectRecipeAction(data);
                case PackageType.ItemInteractionOutcome:
                    return new ItemInteractionOutcome(data);
                case PackageType.LevelFilter:
                    return new LevelFilter(data);
                case PackageType.LevelMappingTable:
                    return new LevelMappingTable(data);
                case PackageType.LinearAttackHook:
                    return new LinearAttackHook(data);
                case PackageType.LogInfo:
                    return new LogInfo(data);
                case PackageType.LoreFilter:
                    return new LoreFilter(data);
                case PackageType.MasterDIDList:
                    return new MasterDIDList(data);
                case PackageType.MasterDIDListMember:
                    return new MasterDIDListMember(data);
                case PackageType.MasterList:
                    return new MasterList(data);
                case PackageType.MasterListMember:
                    return new MasterListMember(data);
                case PackageType.MineGenesisEffect:
                    return new MineGenesisEffect(data);
                case PackageType.MountEffect:
                    return new MountEffect(data);
                case PackageType.MutateRecipeAction:
                    return new MutateRecipeAction(data);
                case PackageType.OrderedDIDEntryTable:
                    return new OrderedDIDEntryTable(data);
                case PackageType.ParameterizedNumericEffect:
                    return new ParameterizedNumericEffect(data);
                case PackageType.PetData:
                    return new PetData(data);
                case PackageType.PerkSkill:
                    return new PerkSkill(data);
                case PackageType.PhaseInfo:
                    return new PhaseInfo(data);
                case PackageType.PlayerEffectRecipeAction:
                    return new PlayerEffectRecipeAction(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfile(data);
                case PackageType.ProduceRecipeAction:
                    return new ProduceRecipeAction(data);
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
                case PackageType.QuestVaultTemplate:
                    return new QuestVaultTemplate(data);
                case PackageType.RaceFilter:
                    return new RaceFilter(data);
                case PackageType.RandomRecallEffect:
                    return new RandomRecallEffect(data);
                case PackageType.RankBoard:
                    return new RankBoard(data);
                case PackageType.Recipe:
                    return new Recipe(data);
                case PackageType.RecipeDifficultyTable:
                    return new RecipeDifficultyTable(data);
                case PackageType.RecipeNameColoringTable:
                    return new RecipeNameColoringTable(data);
                case PackageType.RecipeRecord:
                    return new RecipeRecord(data);
                case PackageType.RecipeTrainingTable:
                    return new GenericPackage(packageType);
                case PackageType.ReflectiveEffect:
                    return new ReflectiveEffect(data);
                case PackageType.ReflectiveVitalEffect:
                    return new ReflectiveVitalEffect(data);
                case PackageType.RemoveRecipeEffect:
                    return new RemoveRecipeEffect(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequest(data);
                case PackageType.SaleProfile:
                    return new SaleProfile(data);
                case PackageType.SaleTemplate:
                    return new SaleTemplate(data);
                case PackageType.SecretRecipe:
                    return new SecretRecipe(data);
                case PackageType.Skill:
                    return new Skill(data);
                case PackageType.SkillCheck:
                    return new SkillCheck(data);
                case PackageType.SkillInfo:
                    return new SkillInfo(data);
                case PackageType.SkillPanel:
                    return new SkillPanel(data);
                case PackageType.SkillRepository:
                    return new SkillRepository(data);
                case PackageType.SlayerEffect:
                    return new SlayerEffect(data);
                case PackageType.StampRecipeAction:
                    return new StampRecipeAction(data);
                case PackageType.StaticAttackHook:
                    return new StaticAttackHook(data);
                case PackageType.SRFormula:
                    return new SRFormula(data);
                case PackageType.StoreGroup:
                    return new StoreGroup(data);
                case PackageType.StoreSorter:
                    return new StoreSorter(data);
                case PackageType.StoreTemplate:
                    return new StoreTemplate(data);
                case PackageType.StoreView:
                    return new StoreView(data);
                case PackageType.StoryQuestTriggerEffect:
                    return new StoryQuestTriggerEffect(data);
                case PackageType.TargetInteraction:
                    return new TargetInteraction(data);
                case PackageType.TargetLevelFilter:
                    return new TargetLevelFilter(data);
                case PackageType.TargetLoreFilter:
                    return new TargetLoreFilter(data);
                case PackageType.TextEffect:
                    return new TextEffect(data);
                case PackageType.TextRecipeAction:
                    return new TextRecipeAction(data);
                case PackageType.Trade:
                    return new Trade(data);
                case PackageType.TransactionBlob:
                    return new TransactionBlob(data);
                case PackageType.UsageBlob:
                    return new UsageBlob(data);
                case PackageType.UsageDesc:
                    return new UsageDesc(data);
                case PackageType.VisualDescEffect:
                    return new VisualDescEffect(data);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfo(data);
                case PackageType.VitalOverTimeEffect:
                    return new VitalOverTimeEffect(data);
                case PackageType.VitalTransferEffect:
                    return new VitalTransferEffect(data);
                case PackageType.WeaponTemplate:
                    return new WeaponTemplate(data);
                default:
                    return null;
            }
        }
    }
}
