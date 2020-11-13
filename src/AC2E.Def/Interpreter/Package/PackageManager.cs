﻿using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public static class PackageManager {

        private static readonly DataId CLIENT_WLIB_DID = new(0x56000005);

        private static PackageTypes packageTypes;

        public static void loadPackageTypes(DatReader datReader) {
            if (packageTypes == null) {
                using (AC2Reader data = datReader.getFileReader(CLIENT_WLIB_DID)) {
                    WLib wlib = new(data);
                    packageTypes = new();
                    foreach (ByteStream.ExportData export in wlib.byteStream.exports) {
                        packageTypes.add(export.args.packageType, export.args.parentIndex);
                    }
                    packageTypes.calculate();
                }
            }
        }

        public static InterpReferenceMeta getReferenceMeta(Type type) {
            InterpReferenceMeta.Flag flags = InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SingletonPkg<>)) {
                flags |= InterpReferenceMeta.Flag.SINGLETON;
            }

            return new(flags, ReferenceType.HEAPOBJECT);
        }

        public static IPackage read(AC2Reader data, NativeType nativeType) {
            return nativeType switch {
                NativeType.AAHASH => new AAHash(data),
                NativeType.AAMULTIHASH => new AAMultiHash(data),
                NativeType.AARRAY => new AArray(data),
                NativeType.AHASHSET => new AHashSet(data),
                NativeType.ALHASH => new ALHash(data),
                NativeType.ALIST => new AList(data),
                NativeType.APPINFOHASH => new AppInfoHash(data),
                NativeType.ARHASH => new ARHash(data),
                NativeType.BEHAVIORPARAMS => new BehaviorParams(data),
                NativeType.EXAMINATIONPROFILE => new ExaminationProfile(data),
                NativeType.EXAMINATIONREQUEST => new ExaminationRequest(data),
                NativeType.GAMEPLAYOPTIONSPROFILE => new GameplayOptionsProfile(data),
                NativeType.GMKEYFRAME => new GMKeyframe(data),
                NativeType.GMQUESTINFOLIST => new GMQuestInfoList(data),
                NativeType.GMQUESTINFO => new GMQuestInfo(data),
                NativeType.GMRACESEXINFO => new GMRaceSexInfo(data),
                NativeType.GMSCENEINFO => new GMSceneInfo(data),
                NativeType.GMSCENEINFOLIST => new GMSceneInfoList(data),
                NativeType.ICONDESC => new IconDesc(data),
                NativeType.LAHASH => new LAHash(data),
                NativeType.LAHASHSET => new LAHashSet(data),
                NativeType.LAMULTIHASH => new LAMultiHash(data),
                NativeType.LARRAY => new LArray(data),
                NativeType.LLIST => new LList(data),
                NativeType.LRHASH => new LRHash(data),
                NativeType.MISSILEPARAMETERS => new MissileParameters(data),
                NativeType.NRHASH => new NRHash(data),
                NativeType.POSITION => new Position(data),
                NativeType.RANDOMSELECTIONTABLE_INT => new RandomSelectionTable(data),
                NativeType.RARRAY => new RArray(data),
                NativeType.RLIST => new RList(data),
                NativeType.SELECTIONINFO => new SelectionInfo(data),
                NativeType.SHORTCUTINFO => new ShortcutInfo(data),
                NativeType.SKILLUINODE => new SkillUINode(data),
                NativeType.STRINGINFO => new StringInfo(data),
                NativeType.WPSTRING => new WPString(data),
                NativeType.UISAVELOCATIONS => new UISaveLocations(data),
                NativeType.VECTOR => new VectorPkg(data.ReadVector()),
                NativeType.VISUALDESC => new VisualDesc(data),
                _ => throw new NotImplementedException($"Unhandled read for native package type {nativeType}."),
            };
        }

        public static IPackage read(AC2Reader data, PackageType packageType) {
            IPackage package = readInternal(data, packageType);

            // Deserialize as the most derived "known" package type
            if (package == null) {
                List<PackageType> packageTypeHierarchy = packageTypes.getPackageTypeHierarchy(packageType);
                foreach (PackageType inheritedPackageType in packageTypeHierarchy) {
                    package = readInternal(data, inheritedPackageType);
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
            return packageType switch {
                PackageType.ACFraction => new ACFraction(data),
                PackageType.Act => new Act(data),
                PackageType.ActiveSkill => new ActiveSkill(data),
                PackageType.ActRegistry => new ActRegistry(data),
                PackageType.ActTemplate => new ActTemplate(data),
                PackageType.AdvancementTable => new AdvancementTable(data),
                PackageType.Agent => new Agent(data),
                PackageType.AIAngerEffect => new AIAngerEffect(data),
                PackageType.AIPetEffect => new AIPetEffect(data),
                PackageType.AITauntDetauntEffect => new AITauntDetauntEffect(data),
                PackageType.AllegianceControl => new AllegianceControl(data),
                PackageType.AllegianceData => new AllegianceData(data),
                PackageType.AllegianceHallBindingStoneUsageBlob => new AllegianceHallBindingStoneUsageBlob(data),
                PackageType.AllegianceHierarchy => new AllegianceHierarchy(data),
                PackageType.AllegianceNode => new AllegianceHierarchy.AllegianceNode(data),
                PackageType.AllegianceProfile => new AllegianceProfile(data),
                PackageType.AllegianceRankTable => new AllegianceRankTable(data),
                PackageType.AnimationRecipeAction => new AnimationRecipeAction(data),
                PackageType.AppearanceModRecipeAction => new AppearanceModRecipeAction(data),
                PackageType.AppearanceProfile => new AppearanceProfile(data),
                PackageType.ApplyEffect => new ApplyEffect(data),
                PackageType.AttackHook => new AttackHook(data),
                PackageType.AttackHookData => new AttackHookData(data),
                PackageType.AttributeProfile => new AttributeProfile(data),
                PackageType.AttributeSkill => new AttributeSkill(data),
                PackageType.AttuneRecipeAction => new AttuneRecipeAction(data),
                PackageType.AuraEffect => new AuraEffect(data),
                PackageType.BallUsageBlob => new BallUsageBlob(data),
                PackageType.BiasProfile => new BiasProfile(data),
                PackageType.BindRecipeAction => new BindRecipeAction(data),
                PackageType.BookEffect => new BookEffect(data),
                PackageType.BookUsageBlob => new BookUsageBlob(data),
                PackageType.ButcheryToolUsageBlob => new ButcheryToolUsageBlob(data),
                PackageType.CAdmin => new CAdmin(data),
                PackageType.CAExcavationPoint => new CAExcavationPoint(data),
                PackageType.ChainedInstantEffect => new ChainedInstantEffect(data),
                PackageType.ChainedNumericEffect => new ChainedNumericEffect(data),
                PackageType.ChannelData => new ChannelData(data),
                PackageType.CharacterGenSystem => new CharacterGenSystem(data),
                PackageType.CharGenMatrix or PackageType.CharGenMatrixData => new CharGenMatrix(data),
                PackageType.ChatChannelControl => new ChatChannelControl(data),
                PackageType.ClassFilter => new ClassFilter(data),
                PackageType.Clothing => new Clothing(data),
                PackageType.ComboEffect => new ComboEffect(data),
                PackageType.CommunicationControl => new CommunicationControl(data),
                PackageType.ConsignerDesc => new ConsignerDesc(data),
                PackageType.Consignment => new Consignment(data),
                PackageType.ConsignmentDesc => new ConsignmentDesc(data),
                PackageType.Container => new Container(data),
                PackageType.ContainerSegmentDescriptor => new ContainerSegmentDescriptor(data),
                PackageType.ContentProfile => new ContentProfile(data),
                PackageType.CorpsePermissionBlob => new CorpsePermissionBlob(data),
                PackageType.CountdownEffect => new CountdownEffect(data),
                PackageType.CPetState => new CPetState(data),
                PackageType.CPlayer => new CPlayer(data),
                PackageType.CraftBlob => new CraftBlob(data),
                PackageType.CraftCheckEntry => new CraftCheckEntry(data),
                PackageType.CraftRandomEntry => new CraftRandomEntry(data),
                PackageType.CraftRegistry => new CraftRegistry(data),
                PackageType.CraftSkill => new CraftSkill(data),
                PackageType.CraftSkillRecord => new CraftSkillRecord(data),
                PackageType.CraftSkillTitleScore => new CraftSkillTitleScore(data),
                PackageType.CShopperContext => new CShopperContext(data),
                PackageType.CUsageSystem => new CUsageSystem(data),
                PackageType.CustomFailureRecipeAction => new CustomFailureRecipeAction(data),
                PackageType.CustomSuccessRecipeAction => new CustomSuccessRecipeAction(data),
                PackageType.DamageDisplayInfo => new DamageDisplayInfo(data),
                PackageType.DestroyRecipeAction => new DestroyRecipeAction(data),
                PackageType.DefaultPermissionBlob => new DefaultPermissionBlob(data),
                PackageType.DefaultTakePermissionBlob => new DefaultTakePermissionBlob(data),
                PackageType.Door => new Door(data),
                PackageType.DoorUsageBlob => new DoorUsageBlob(data),
                PackageType.DurabilityFilter => new DurabilityFilter(data),
                PackageType.Eff_Com_Hero_FickleFate_HealthHealDecrease => new Eff_Com_Hero_FickleFate_HealthHealDecrease(data),
                PackageType.Eff_Com_Hero_FickleFate_HealthHealIncrease => new Eff_Com_Hero_FickleFate_HealthHealIncrease(data),
                PackageType.Eff_Com_Hero_FickleFate_VigorHealDecrease => new Eff_Com_Hero_FickleFate_VigorHealDecrease(data),
                PackageType.Eff_Com_Hero_FickleFate_VigorHealIncrease => new Eff_Com_Hero_FickleFate_VigorHealIncrease(data),
                PackageType.Eff_Com_Hero_Perk_WildMagic1 => new Eff_Com_Hero_Perk_WildMagic1(data),
                PackageType.Eff_DayCounterLostArtifacts => new Eff_DayCounterLostArtifacts(data),
                PackageType.Eff_Emp_Me_Templar_SigilOfValor => new Eff_Emp_Me_Templar_SigilOfValor(data),
                PackageType.Eff_FlagAccountCanCreateDrudges => new Eff_FlagAccountCanCreateDrudges(data),
                PackageType.Eff_HealthArchon2 => new Eff_HealthArchon2(data),
                PackageType.Eff_Hum_Ma_Sorcerer_Hero_BlightingGaze => new Eff_Hum_Ma_Sorcerer_Hero_BlightingGaze(data),
                PackageType.Eff_Mn_Doppelganger => new Eff_Mn_Doppelganger(data),
                PackageType.Eff_Mn_Golem_Clone => new Eff_Mn_Golem_Clone(data),
                PackageType.Eff_Mn_Ma_CorruptorsTouch => new Eff_Mn_Ma_CorruptorsTouch(data),
                PackageType.Eff_Popup_FirstCharacterSession => new Eff_Popup_FirstCharacterSession(data),
                PackageType.Eff_PortalBeacon_PortalDeflectionGem => new Eff_PortalBeacon_PortalDeflectionGem(data),
                PackageType.Eff_RashanDrudgeBane => new Eff_RashanDrudgeBane(data),
                PackageType.Eff_RemoveMonsterWeaponsFromPlayersMay05 => new Eff_RemoveMonsterWeaponsFromPlayersMay05(data),
                PackageType.Eff_ResetLinvakResetTimers => new Eff_ResetLinvakResetTimers(data),
                PackageType.Eff_ResetOmishanResetTimers => new Eff_ResetOmishanResetTimers(data),
                PackageType.Eff_ResetOstethResetTimers => new Eff_ResetOstethResetTimers(data),
                PackageType.Eff_SetLinvakResetTimers => new Eff_SetLinvakResetTimers(data),
                PackageType.Eff_SetOmishanResetTimers => new Eff_SetOmishanResetTimers(data),
                PackageType.Eff_SetOstethResetTimers => new Eff_SetOstethResetTimers(data),
                PackageType.Eff_Tsys_SoulDefractor => new Eff_Tsys_SoulDefractor(data),
                PackageType.Eff_UseXPStone => new Eff_UseXPStone(data),
                PackageType.Eff_WildMagic4_EepEars => new Eff_WildMagic4_EepEars(data),
                PackageType.Effect => new Effect(data),
                PackageType.EffectDesc => new EffectDesc(data),
                PackageType.EffectRecord => new EffectRecord(data),
                PackageType.EffectRegistry => new EffectRegistry(data),
                PackageType.EffectTypeFilter => new EffectTypeFilter(data),
                PackageType.EquipItemProfile => new EquipItemProfile(data),
                PackageType.EmoteInfo => new EmoteInfo(data),
                PackageType.EmoteTable => new EmoteTable(data),
                PackageType.Entity => new Entity(data),
                PackageType.EntityFilter => new EntityFilter(data),
                PackageType.ExperienceEffect => new ExperienceEffect(data),
                PackageType.ExportToXMLOp => new ExportToXMLOp(data),
                PackageType.ExportToXMLCleanupOp => new ExportToXMLCleanupOp(data),
                PackageType.FactionChangeEffect => new FactionChangeEffect(data),
                PackageType.FactionEffectEntry => new FactionEffectEntry(data),
                PackageType.FactionGlobals => new FactionGlobals(data),
                PackageType.Fellow => new Fellow(data),
                PackageType.Fellowship => new Fellowship(data),
                PackageType.FellowshipControl => new FellowshipControl(data),
                PackageType.FellowVitals => new FellowVitals(data),
                PackageType.FineItemClassFilter => new FineItemClassFilter(data),
                PackageType.FlagRecipeAction => new FlagRecipeAction(data),
                PackageType.FloatScaleDuple => new FloatScaleDuple(data),
                PackageType.GameEventEffect => new GameEventEffect(data),
                PackageType.GameSaleProfile => new GameSaleProfile(data),
                PackageType.gmCEntity => new gmCEntity(data),
                PackageType.gmEntity => new gmEntity(data),
                PackageType.GrantRecipeEffect => new GrantRecipeEffect(data),
                PackageType.HarmonizeRecipeAction => new HarmonizeRecipeAction(data),
                PackageType.HeroControl => new HeroControl(data),
                PackageType.HistoryList => new HistoryList(data),
                PackageType.HookData => new HookData(data),
                PackageType.HotspotEffect => new HotspotEffect(data),
                PackageType.Ingredient => new Ingredient(data),
                PackageType.InstantBehaviorEffect => new InstantBehaviorEffect(data),
                PackageType.InstantVitalEffect => new InstantVitalEffect(data),
                PackageType.InteractionSystem => new InteractionSystem(data),
                PackageType.InteractionTable => new InteractionTable(data),
                PackageType.Inventory => new Inventory(data),
                PackageType.InventoryGlobals => new InventoryGlobals(data),
                PackageType.InventProfile => new InventProfile(data),
                PackageType.InvEquipDesc => new InvEquipDesc(data),
                PackageType.InvLocCategory => new InvLocCategory(data),
                PackageType.InvMoveDesc => new InvMoveDesc(data),
                PackageType.InvTakeAllDesc => new InvTakeAllDesc(data),
                PackageType.InvTransmuteAllDesc => new InvTransmuteAllDesc(data),
                PackageType.ItemEffectRecipeAction => new ItemEffectRecipeAction(data),
                PackageType.ItemInteractionOutcome => new ItemInteractionOutcome(data),
                PackageType.ItemInteractionUsageBlob => new ItemInteractionUsageBlob(data),
                PackageType.ItemProfile => new ItemProfile(data),
                PackageType.KeyUsageBlob => new KeyUsageBlob(data),
                PackageType.LevelData => new LevelData(data),
                PackageType.LevelFilter => new LevelFilter(data),
                PackageType.LevelMappingTable => new LevelMappingTable(data),
                PackageType.LevelTable => new LevelTable(data),
                PackageType.LinearAttackHook => new LinearAttackHook(data),
                PackageType.LogInfo => new LogInfo(data),
                PackageType.LogSystem => new LogSystem(data),
                PackageType.LoreFilter => new LoreFilter(data),
                PackageType.MasterDIDList => new MasterDIDList(data),
                PackageType.MasterDIDListMember => new MasterDIDListMember(data),
                PackageType.MasterList => new MasterList(data),
                PackageType.MasterListMember => new MasterListMember(data),
                PackageType.MineCraftBlob => new MineCraftBlob(data),
                PackageType.MineGenesisEffect => new MineGenesisEffect(data),
                PackageType.MineUsageAction => new MineUsageAction(data),
                PackageType.MineUsageBlob => new MineUsageBlob(data),
                PackageType.ModifyHierarchyHashesOp => new ModifyHierarchyHashesOp(data),
                PackageType.MountEffect => new MountEffect(data),
                PackageType.MountTable => new MountTable(data),
                PackageType.MutateRecipeAction => new MutateRecipeAction(data),
                PackageType.Operation => new Operation(data),
                PackageType.OperationQueue => new OperationQueue(data),
                PackageType.OrderedDIDEntryTable => new OrderedDIDEntryTable(data),
                PackageType.PackageIDTable => new PackageIDTable(data),
                PackageType.ParameterizedNumericEffect => new ParameterizedNumericEffect(data),
                PackageType.PetData => new PetData(data),
                PackageType.PetGenesisInfo => new PetGenesisInfo(data),
                PackageType.PerkSkill => new PerkSkill(data),
                PackageType.PhaseInfo => new PhaseInfo(data),
                PackageType.PKStatus => new PKStatus(data),
                PackageType.Player => new Player(data),
                PackageType.PlayerEffectRecipeAction => new PlayerEffectRecipeAction(data),
                PackageType.PlayerSaleProfile => new PlayerSaleProfile(data),
                PackageType.PortalSummonEffect => new PortalSummonEffect(data),
                PackageType.PotionUsageBlob => new PotionUsageBlob(data),
                PackageType.ProduceRecipeAction => new ProduceRecipeAction(data),
                PackageType.PropertyMapper => new PropertyMapper(data),
                PackageType.PublicVendorProfile => new PublicVendorProfile(data),
                PackageType.QualitiesEffect => new QualitiesEffect(data),
                PackageType.Quest => new Quest(data),
                PackageType.QuestGGWDreamT => new QuestTemplate(data),
                PackageType.QuestGlobals => new QuestGlobals(data),
                PackageType.QuestTemplate => new QuestTemplate(data),
                PackageType.QuestTriggerEffect => new QuestTriggerEffect(data),
                PackageType.QuestVaultTemplate => new QuestVaultTemplate(data),
                PackageType.RaceFilter => new RaceFilter(data),
                PackageType.RandomAttackHook => new RandomAttackHook(data),
                PackageType.RandomRecallEffect => new RandomRecallEffect(data),
                PackageType.RankBoard => new RankBoard(data),
                PackageType.Recipe => new Recipe(data),
                PackageType.RecipeContext => new RecipeContext(data),
                PackageType.RecipeCostData => new RecipeCostData(data),
                PackageType.RecipeCostTable => new RecipeCostTable(data),
                PackageType.RecipeDifficultyTable => new RecipeDifficultyTable(data),
                PackageType.RecipeNameColoringTable => new RecipeNameColoringTable(data),
                PackageType.RecipeRecord => new RecipeRecord(data),
                PackageType.RecipeTrainingTable => new GenericPackage(packageType),
                PackageType.ReflectiveEffect => new ReflectiveEffect(data),
                PackageType.ReflectiveVitalEffect => new ReflectiveVitalEffect(data),
                PackageType.RemoveRecipeEffect => new RemoveRecipeEffect(data),
                PackageType.ResurrectEffect => new ResurrectEffect(data),
                PackageType.ResurrectionRequest => new ResurrectionRequest(data),
                PackageType.SaleProfile => new SaleProfile(data),
                PackageType.SaleTemplate => new SaleTemplate(data),
                PackageType.SecretProduct => new SecretProduct(data),
                PackageType.SecretRecipe => new SecretRecipe(data),
                PackageType.ShardUsageBlob => new ShardUsageBlob(data),
                PackageType.SimpleMasterList => new SimpleMasterList(data),
                PackageType.Skill => new Skill(data),
                PackageType.SkillCheck => new SkillCheck(data),
                PackageType.SkillInfo => new SkillInfo(data),
                PackageType.SkillPanel => new SkillPanel(data),
                PackageType.SkillProfile => new SkillProfile(data),
                PackageType.SkillRepository => new SkillRepository(data),
                PackageType.SlayerEffect => new SlayerEffect(data),
                PackageType.StampRecipeAction => new StampRecipeAction(data),
                PackageType.StartArea => new StartArea(data),
                PackageType.StartInvData => new StartInvData(data),
                PackageType.StaticAttackHook => new StaticAttackHook(data),
                PackageType.SRFormula => new SRFormula(data),
                PackageType.StoreGlobals => new StoreGlobals(data),
                PackageType.StoreGroup => new StoreGroup(data),
                PackageType.StoreSorter => new StoreSorter(data),
                PackageType.StoreTemplate => new StoreTemplate(data),
                PackageType.StoreView => new StoreView(data),
                PackageType.StoryQuestTriggerEffect => new StoryQuestTriggerEffect(data),
                PackageType.StrictAliasControl => new StrictAliasControl(data),
                PackageType.TargetInteraction => new TargetInteraction(data),
                PackageType.TargetLevelFilter => new TargetLevelFilter(data),
                PackageType.TargetLoreFilter => new TargetLoreFilter(data),
                PackageType.TeleportEffect => new TeleportEffect(data),
                PackageType.TextEffect => new TextEffect(data),
                PackageType.TextRecipeAction => new TextRecipeAction(data),
                PackageType.ToolPermissionBlob => new ToolPermissionBlob(data),
                PackageType.TotemUsageBlob => new TotemUsageBlob(data),
                PackageType.Trade => new Trade(data),
                PackageType.TradeSystem => new TradeSystem(data),
                PackageType.TransactInfo => new TransactInfo(data),
                PackageType.TransactionBlob => new TransactionBlob(data),
                PackageType.TransactResult => new TransactResult(data),
                PackageType.TravelRecallEffect => new TravelRecallEffect(data),
                PackageType.TravelTieEffect => new TravelTieEffect(data),
                PackageType.TravelUsageBlob => new TravelUsageBlob(data),
                PackageType.UIDamageControl => new UIDamageControl(data),
                PackageType.UsageBlob => new UsageBlob(data),
                PackageType.UsageDesc => new UsageDesc(data),
                PackageType.VisualDescEffect => new VisualDescEffect(data),
                PackageType.VisualDescInfo => new VisualDescInfo(data),
                PackageType.VitalOverTimeEffect => new VitalOverTimeEffect(data),
                PackageType.VitalTransferEffect => new VitalTransferEffect(data),
                PackageType.WeaponTemplate => new WeaponTemplate(data),
                _ => null,
            };
        }
    }
}
