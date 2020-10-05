using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public static class PackageManager {

        private static readonly DataId CLIENT_WLIB_DID = new DataId(0x56000005);

        private static PackageTypes packageTypes;

        public static void loadPackageTypes(DatReader datReader) {
            if (packageTypes == null) {
                using (AC2Reader data = datReader.getFileReader(CLIENT_WLIB_DID)) {
                    var wlib = new WLib(data);
                    packageTypes = new PackageTypes();
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
                    return new ARHash(data);
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
                case NativeType.GMRACESEXINFO:
                    return new GMRaceSexInfo(data);
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
                    return new LRHash(data);
                case NativeType.MISSILEPARAMETERS:
                    return new MissileParameters(data);
                case NativeType.NRHASH:
                    return new NRHash(data);
                case NativeType.POSITION:
                    return new Position(data);
                case NativeType.RANDOMSELECTIONTABLE_INT:
                    return new RandomSelectionTable(data);
                case NativeType.RARRAY:
                    return new RArray(data);
                case NativeType.RLIST:
                    return new RList(data);
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
            switch (packageType) {
                case PackageType.ACFraction:
                    return new ACFraction(data);
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
                case PackageType.AIAngerEffect:
                    return new AIAngerEffect(data);
                case PackageType.AIPetEffect:
                    return new AIPetEffect(data);
                case PackageType.AITauntDetauntEffect:
                    return new AITauntDetauntEffect(data);
                case PackageType.AllegianceControl:
                    return new AllegianceControl(data);
                case PackageType.AllegianceData:
                    return new AllegianceData(data);
                case PackageType.AllegianceHallBindingStoneUsageBlob:
                    return new AllegianceHallBindingStoneUsageBlob(data);
                case PackageType.AllegianceHierarchy:
                    return new AllegianceHierarchy(data);
                case PackageType.AllegianceNode:
                    return new AllegianceHierarchy.AllegianceNode(data);
                case PackageType.AllegianceProfile:
                    return new AllegianceProfile(data);
                case PackageType.AllegianceRankTable:
                    return new AllegianceRankTable(data);
                case PackageType.AnimationRecipeAction:
                    return new AnimationRecipeAction(data);
                case PackageType.AppearanceModRecipeAction:
                    return new AppearanceModRecipeAction(data);
                case PackageType.AppearanceProfile:
                    return new AppearanceProfile(data);
                case PackageType.ApplyEffect:
                    return new ApplyEffect(data);
                case PackageType.AttackHook:
                    return new AttackHook(data);
                case PackageType.AttackHookData:
                    return new AttackHookData(data);
                case PackageType.AttributeProfile:
                    return new AttributeProfile(data);
                case PackageType.AttributeSkill:
                    return new AttributeSkill(data);
                case PackageType.AttuneRecipeAction:
                    return new AttuneRecipeAction(data);
                case PackageType.AuraEffect:
                    return new AuraEffect(data);
                case PackageType.BallUsageBlob:
                    return new BallUsageBlob(data);
                case PackageType.BiasProfile:
                    return new BiasProfile(data);
                case PackageType.BindRecipeAction:
                    return new BindRecipeAction(data);
                case PackageType.BookEffect:
                    return new BookEffect(data);
                case PackageType.BookUsageBlob:
                    return new BookUsageBlob(data);
                case PackageType.ButcheryToolUsageBlob:
                    return new ButcheryToolUsageBlob(data);
                case PackageType.CAdmin:
                    return new CAdmin(data);
                case PackageType.CAExcavationPoint:
                    return new CAExcavationPoint(data);
                case PackageType.ChainedInstantEffect:
                    return new ChainedInstantEffect(data);
                case PackageType.ChainedNumericEffect:
                    return new ChainedNumericEffect(data);
                case PackageType.ChannelData:
                    return new ChannelData(data);
                case PackageType.CharacterGenSystem:
                    return new CharacterGenSystem(data);
                case PackageType.CharGenMatrix:
                case PackageType.CharGenMatrixData:
                    return new CharGenMatrix(data);
                case PackageType.ChatChannelControl:
                    return new ChatChannelControl(data);
                case PackageType.ClassFilter:
                    return new ClassFilter(data);
                case PackageType.Clothing:
                    return new Clothing(data);
                case PackageType.ComboEffect:
                    return new ComboEffect(data);
                case PackageType.CommunicationControl:
                    return new CommunicationControl(data);
                case PackageType.ConsignerDesc:
                    return new ConsignerDesc(data);
                case PackageType.Consignment:
                    return new Consignment(data);
                case PackageType.ConsignmentDesc:
                    return new ConsignmentDesc(data);
                case PackageType.Container:
                    return new Container(data);
                case PackageType.ContainerSegmentDescriptor:
                    return new ContainerSegmentDescriptor(data);
                case PackageType.ContentProfile:
                    return new ContentProfile(data);
                case PackageType.CorpsePermissionBlob:
                    return new CorpsePermissionBlob(data);
                case PackageType.CountdownEffect:
                    return new CountdownEffect(data);
                case PackageType.CPetState:
                    return new CPetState(data);
                case PackageType.CPlayer:
                    return new CPlayer(data);
                case PackageType.CraftBlob:
                    return new CraftBlob(data);
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
                case PackageType.CraftSkillTitleScore:
                    return new CraftSkillTitleScore(data);
                case PackageType.CShopperContext:
                    return new CShopperContext(data);
                case PackageType.CUsageSystem:
                    return new CUsageSystem(data);
                case PackageType.CustomFailureRecipeAction:
                    return new CustomFailureRecipeAction(data);
                case PackageType.CustomSuccessRecipeAction:
                    return new CustomSuccessRecipeAction(data);
                case PackageType.DamageDisplayInfo:
                    return new DamageDisplayInfo(data);
                case PackageType.DestroyRecipeAction:
                    return new DestroyRecipeAction(data);
                case PackageType.DefaultPermissionBlob:
                    return new DefaultPermissionBlob(data);
                case PackageType.DefaultTakePermissionBlob:
                    return new DefaultTakePermissionBlob(data);
                case PackageType.Door:
                    return new Door(data);
                case PackageType.DoorUsageBlob:
                    return new DoorUsageBlob(data);
                case PackageType.DurabilityFilter:
                    return new DurabilityFilter(data);
                case PackageType.Eff_Com_Hero_FickleFate_HealthHealDecrease:
                    return new Eff_Com_Hero_FickleFate_HealthHealDecrease(data);
                case PackageType.Eff_Com_Hero_FickleFate_HealthHealIncrease:
                    return new Eff_Com_Hero_FickleFate_HealthHealIncrease(data);
                case PackageType.Eff_Com_Hero_FickleFate_VigorHealDecrease:
                    return new Eff_Com_Hero_FickleFate_VigorHealDecrease(data);
                case PackageType.Eff_Com_Hero_FickleFate_VigorHealIncrease:
                    return new Eff_Com_Hero_FickleFate_VigorHealIncrease(data);
                case PackageType.Eff_Com_Hero_Perk_WildMagic1:
                    return new Eff_Com_Hero_Perk_WildMagic1(data);
                case PackageType.Eff_DayCounterLostArtifacts:
                    return new Eff_DayCounterLostArtifacts(data);
                case PackageType.Eff_Emp_Me_Templar_SigilOfValor:
                    return new Eff_Emp_Me_Templar_SigilOfValor(data);
                case PackageType.Eff_FlagAccountCanCreateDrudges:
                    return new Eff_FlagAccountCanCreateDrudges(data);
                case PackageType.Eff_HealthArchon2:
                    return new Eff_HealthArchon2(data);
                case PackageType.Eff_Hum_Ma_Sorcerer_Hero_BlightingGaze:
                    return new Eff_Hum_Ma_Sorcerer_Hero_BlightingGaze(data);
                case PackageType.Eff_Mn_Doppelganger:
                    return new Eff_Mn_Doppelganger(data);
                case PackageType.Eff_Mn_Golem_Clone:
                    return new Eff_Mn_Golem_Clone(data);
                case PackageType.Eff_Mn_Ma_CorruptorsTouch:
                    return new Eff_Mn_Ma_CorruptorsTouch(data);
                case PackageType.Eff_Popup_FirstCharacterSession:
                    return new Eff_Popup_FirstCharacterSession(data);
                case PackageType.Eff_PortalBeacon_PortalDeflectionGem:
                    return new Eff_PortalBeacon_PortalDeflectionGem(data);
                case PackageType.Eff_RashanDrudgeBane:
                    return new Eff_RashanDrudgeBane(data);
                case PackageType.Eff_RemoveMonsterWeaponsFromPlayersMay05:
                    return new Eff_RemoveMonsterWeaponsFromPlayersMay05(data);
                case PackageType.Eff_ResetLinvakResetTimers:
                    return new Eff_ResetLinvakResetTimers(data);
                case PackageType.Eff_ResetOmishanResetTimers:
                    return new Eff_ResetOmishanResetTimers(data);
                case PackageType.Eff_ResetOstethResetTimers:
                    return new Eff_ResetOstethResetTimers(data);
                case PackageType.Eff_SetLinvakResetTimers:
                    return new Eff_SetLinvakResetTimers(data);
                case PackageType.Eff_SetOmishanResetTimers:
                    return new Eff_SetOmishanResetTimers(data);
                case PackageType.Eff_SetOstethResetTimers:
                    return new Eff_SetOstethResetTimers(data);
                case PackageType.Eff_Tsys_SoulDefractor:
                    return new Eff_Tsys_SoulDefractor(data);
                case PackageType.Eff_UseXPStone:
                    return new Eff_UseXPStone(data);
                case PackageType.Eff_WildMagic4_EepEars:
                    return new Eff_WildMagic4_EepEars(data);
                case PackageType.Effect:
                    return new Effect(data);
                case PackageType.EffectDesc:
                    return new EffectDesc(data);
                case PackageType.EffectRecord:
                    return new EffectRecord(data);
                case PackageType.EffectRegistry:
                    return new EffectRegistry(data);
                case PackageType.EffectTypeFilter:
                    return new EffectTypeFilter(data);
                case PackageType.EquipItemProfile:
                    return new EquipItemProfile(data);
                case PackageType.EmoteInfo:
                    return new EmoteInfo(data);
                case PackageType.EmoteTable:
                    return new EmoteTable(data);
                case PackageType.Entity:
                    return new Entity(data);
                case PackageType.EntityFilter:
                    return new EntityFilter(data);
                case PackageType.ExperienceEffect:
                    return new ExperienceEffect(data);
                case PackageType.ExportToXMLOp:
                    return new ExportToXMLOp(data);
                case PackageType.ExportToXMLCleanupOp:
                    return new ExportToXMLCleanupOp(data);
                case PackageType.FactionChangeEffect:
                    return new FactionChangeEffect(data);
                case PackageType.FactionEffectEntry:
                    return new FactionEffectEntry(data);
                case PackageType.FactionGlobals:
                    return new FactionGlobals(data);
                case PackageType.Fellow:
                    return new Fellow(data);
                case PackageType.Fellowship:
                    return new Fellowship(data);
                case PackageType.FellowshipControl:
                    return new FellowshipControl(data);
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
                case PackageType.HeroControl:
                    return new HeroControl(data);
                case PackageType.HistoryList:
                    return new HistoryList(data);
                case PackageType.HookData:
                    return new HookData(data);
                case PackageType.HotspotEffect:
                    return new HotspotEffect(data);
                case PackageType.Ingredient:
                    return new Ingredient(data);
                case PackageType.InstantBehaviorEffect:
                    return new InstantBehaviorEffect(data);
                case PackageType.InstantVitalEffect:
                    return new InstantVitalEffect(data);
                case PackageType.InteractionSystem:
                    return new InteractionSystem(data);
                case PackageType.InteractionTable:
                    return new InteractionTable(data);
                case PackageType.Inventory:
                    return new Inventory(data);
                case PackageType.InventoryGlobals:
                    return new InventoryGlobals(data);
                case PackageType.InventProfile:
                    return new InventProfile(data);
                case PackageType.InvEquipDesc:
                    return new InvEquipDesc(data);
                case PackageType.InvLocCategory:
                    return new InvLocCategory(data);
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
                case PackageType.ItemInteractionUsageBlob:
                    return new ItemInteractionUsageBlob(data);
                case PackageType.ItemProfile:
                    return new ItemProfile(data);
                case PackageType.KeyUsageBlob:
                    return new KeyUsageBlob(data);
                case PackageType.LevelData:
                    return new LevelData(data);
                case PackageType.LevelFilter:
                    return new LevelFilter(data);
                case PackageType.LevelMappingTable:
                    return new LevelMappingTable(data);
                case PackageType.LevelTable:
                    return new LevelTable(data);
                case PackageType.LinearAttackHook:
                    return new LinearAttackHook(data);
                case PackageType.LogInfo:
                    return new LogInfo(data);
                case PackageType.LogSystem:
                    return new LogSystem(data);
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
                case PackageType.MineCraftBlob:
                    return new MineCraftBlob(data);
                case PackageType.MineGenesisEffect:
                    return new MineGenesisEffect(data);
                case PackageType.MineUsageAction:
                    return new MineUsageAction(data);
                case PackageType.MineUsageBlob:
                    return new MineUsageBlob(data);
                case PackageType.ModifyHierarchyHashesOp:
                    return new ModifyHierarchyHashesOp(data);
                case PackageType.MountEffect:
                    return new MountEffect(data);
                case PackageType.MountTable:
                    return new MountTable(data);
                case PackageType.MutateRecipeAction:
                    return new MutateRecipeAction(data);
                case PackageType.Operation:
                    return new Operation(data);
                case PackageType.OperationQueue:
                    return new OperationQueue(data);
                case PackageType.OrderedDIDEntryTable:
                    return new OrderedDIDEntryTable(data);
                case PackageType.PackageIDTable:
                    return new PackageIDTable(data);
                case PackageType.ParameterizedNumericEffect:
                    return new ParameterizedNumericEffect(data);
                case PackageType.PetData:
                    return new PetData(data);
                case PackageType.PetGenesisInfo:
                    return new PetGenesisInfo(data);
                case PackageType.PerkSkill:
                    return new PerkSkill(data);
                case PackageType.PhaseInfo:
                    return new PhaseInfo(data);
                case PackageType.PKStatus:
                    return new PKStatus(data);
                case PackageType.Player:
                    return new Player(data);
                case PackageType.PlayerEffectRecipeAction:
                    return new PlayerEffectRecipeAction(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfile(data);
                case PackageType.PortalSummonEffect:
                    return new PortalSummonEffect(data);
                case PackageType.PotionUsageBlob:
                    return new PotionUsageBlob(data);
                case PackageType.ProduceRecipeAction:
                    return new ProduceRecipeAction(data);
                case PackageType.PropertyMapper:
                    return new PropertyMapper(data);
                case PackageType.PublicVendorProfile:
                    return new PublicVendorProfile(data);
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
                case PackageType.RandomAttackHook:
                    return new RandomAttackHook(data);
                case PackageType.RandomRecallEffect:
                    return new RandomRecallEffect(data);
                case PackageType.RankBoard:
                    return new RankBoard(data);
                case PackageType.Recipe:
                    return new Recipe(data);
                case PackageType.RecipeContext:
                    return new RecipeContext(data);
                case PackageType.RecipeCostData:
                    return new RecipeCostData(data);
                case PackageType.RecipeCostTable:
                    return new RecipeCostTable(data);
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
                case PackageType.ResurrectEffect:
                    return new ResurrectEffect(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequest(data);
                case PackageType.SaleProfile:
                    return new SaleProfile(data);
                case PackageType.SaleTemplate:
                    return new SaleTemplate(data);
                case PackageType.SecretProduct:
                    return new SecretProduct(data);
                case PackageType.SecretRecipe:
                    return new SecretRecipe(data);
                case PackageType.ShardUsageBlob:
                    return new ShardUsageBlob(data);
                case PackageType.SimpleMasterList:
                    return new SimpleMasterList(data);
                case PackageType.Skill:
                    return new Skill(data);
                case PackageType.SkillCheck:
                    return new SkillCheck(data);
                case PackageType.SkillInfo:
                    return new SkillInfo(data);
                case PackageType.SkillPanel:
                    return new SkillPanel(data);
                case PackageType.SkillProfile:
                    return new SkillProfile(data);
                case PackageType.SkillRepository:
                    return new SkillRepository(data);
                case PackageType.SlayerEffect:
                    return new SlayerEffect(data);
                case PackageType.StampRecipeAction:
                    return new StampRecipeAction(data);
                case PackageType.StartArea:
                    return new StartArea(data);
                case PackageType.StartInvData:
                    return new StartInvData(data);
                case PackageType.StaticAttackHook:
                    return new StaticAttackHook(data);
                case PackageType.SRFormula:
                    return new SRFormula(data);
                case PackageType.StoreGlobals:
                    return new StoreGlobals(data);
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
                case PackageType.StrictAliasControl:
                    return new StrictAliasControl(data);
                case PackageType.TargetInteraction:
                    return new TargetInteraction(data);
                case PackageType.TargetLevelFilter:
                    return new TargetLevelFilter(data);
                case PackageType.TargetLoreFilter:
                    return new TargetLoreFilter(data);
                case PackageType.TeleportEffect:
                    return new TeleportEffect(data);
                case PackageType.TextEffect:
                    return new TextEffect(data);
                case PackageType.TextRecipeAction:
                    return new TextRecipeAction(data);
                case PackageType.ToolPermissionBlob:
                    return new ToolPermissionBlob(data);
                case PackageType.TotemUsageBlob:
                    return new TotemUsageBlob(data);
                case PackageType.Trade:
                    return new Trade(data);
                case PackageType.TradeSystem:
                    return new TradeSystem(data);
                case PackageType.TransactInfo:
                    return new TransactInfo(data);
                case PackageType.TransactionBlob:
                    return new TransactionBlob(data);
                case PackageType.TransactResult:
                    return new TransactResult(data);
                case PackageType.TravelRecallEffect:
                    return new TravelRecallEffect(data);
                case PackageType.TravelTieEffect:
                    return new TravelTieEffect(data);
                case PackageType.TravelUsageBlob:
                    return new TravelUsageBlob(data);
                case PackageType.UIDamageControl:
                    return new UIDamageControl(data);
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
