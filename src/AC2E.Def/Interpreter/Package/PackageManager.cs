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
            if (AgentPackages.NPC_IMMOBILE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.NPCImmobileTemplate;
            } else if (AgentPackages.NPC_TABLE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.NPCTable;
            } else if (AgentPackages.TEACHING_STONE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.TeachingStoneTemplate;
            } else if (ClothingPackages.HUMAN_ARMOR_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.HumanArmorTemplate;
            } else if (ClothingPackages.LUGIAN_ARMOR_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.LugianArmorTemplate;
            } else if (ClothingPackages.RING_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.RingTemplate;
            } else if (ClothingPackages.TALISMAN_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.TalismanTemplate;
            } else if (ClothingPackages.TUMEROK_ARMOR_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.TumerokArmorTemplate;
            } else if (CraftPackages.REFINING_RECIPE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.RefiningRecipe;
            } else if (EffectPackages.AI_BEHAVIOR_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AIBehaviorEffect;
            } else if (EffectPackages.AI_CONCEAL_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AIConcealEffect;
            } else if (EffectPackages.AI_PET_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AIPetEffect;
            } else if (EffectPackages.AI_VOTER_SWAP_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AIVoterSwapEffect;
            } else if (EffectPackages.APPLY_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ApplyEffect;
            } else if (EffectPackages.ATTUNEMENT_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AttunementEffect;
            } else if (EffectPackages.COMBO_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ComboEffect;
            } else if (EffectPackages.COUNTDOWN_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.CountdownEffect;
            } else if (EffectPackages.CHAINED_INSTANT_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ChainedInstantEffect;
            } else if (EffectPackages.CHAINED_NUMERIC_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ChainedNumericEffect;
            } else if (EffectPackages.DISPEL_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.DispelEffect;
            } else if (EffectPackages.EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.Effect;
            } else if (EffectPackages.EXPERIENCE_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ExperienceEffect;
            } else if (EffectPackages.GAME_EVENT_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.GameEventEffect;
            } else if (EffectPackages.GENESIS_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.GenesisEffect;
            } else if (EffectPackages.GRANT_RECIPE_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.GrantRecipeEffect;
            } else if (EffectPackages.IMPULSE_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ImpulseEffect;
            } else if (EffectPackages.INSTANT_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.InstantEffect;
            } else if (EffectPackages.INSTANT_NUMERIC_QUALITIES_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.InstantNumericQualitiesEffect;
            } else if (EffectPackages.INSTANT_VITAL_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.InstantVitalEffect;
            } else if (EffectPackages.MOUNT_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.MountEffect;
            } else if (EffectPackages.NULL_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.Eff_NullEffect;
            } else if (EffectPackages.PERK_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.PerkTemplate;
            } else if (EffectPackages.POPUP_EFFECT_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.PopupEffectTemplate;
            } else if (EffectPackages.PROTECTION_EFFECT_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ProtectionEffect;
            } else if (EffectPackages.RANDOM_RECALL_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.RandomRecallEffect;
            } else if (EffectPackages.REFLECTIVE_VITAL_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ReflectiveVitalEffect;
            } else if (EffectPackages.REMOVE_RECIPE_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.RemoveRecipeEffect;
            } else if (EffectPackages.SKILL_TRAINING_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.SkillTrainingEffect;
            } else if (EffectPackages.SLAYER_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.SlayerEffect;
            } else if (EffectPackages.STORY_QUEST_TRIGGER_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.StoryQuestTriggerEffect;
            } else if (EffectPackages.TRANSPARENT_NUMERIC_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.TransparentNumericEffect;
            } else if (EffectPackages.USE_XP_STONE_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.Eff_UseXPStone;
            } else if (EffectPackages.VISUAL_DESC_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.VisualDescEffect;
            } else if (EffectPackages.VITAL_OVER_TIME_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.VitalOverTimeEffect;
            } else if (EffectPackages.VITAL_TRANSFER_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.VitalTransferEffect;
            } else if (InventoryPackages.CHEST_ADMIN_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ChestAdminTemplate;
            } else if (InventoryPackages.CHEST_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ChestTemplate;
            } else if (ItemPackages.ALCHEMY_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AlchemyTemplate;
            } else if (ItemPackages.AXE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AxeTemplate;
            } else if (ItemPackages.CESTAS_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.CestasTemplate;
            } else if (ItemPackages.DEATH_PORTAL_PACKAGES.Contains(packageType)) {
                packageType = PackageType.DeathPortal;
            } else if (ItemPackages.DOOR_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.DoorTemplate;
            } else if (ItemPackages.DRUM_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.DrumTemplate;
            } else if (ItemPackages.FROGSTAVE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.FrogstaveTemplate;
            } else if (ItemPackages.GENERIC_PORTAL_PACKAGES.Contains(packageType)) {
                packageType = PackageType.GenericPortal;
            } else if (ItemPackages.GLYPH_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.GlyphTemplate;
            } else if (ItemPackages.KEY_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.KeyTemplate;
            } else if (ItemPackages.MAP_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.MapTemplate;
            } else if (ItemPackages.MINE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.MineTemplate;
            } else if (ItemPackages.MINING_TOOL_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.MiningToolTemplate;
            } else if (ItemPackages.MOTE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.MoteTemplate;
            } else if (ItemPackages.POTION_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.PotionTemplate;
            } else if (ItemPackages.ROAD_SIGN_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.RoadSignTemplate;
            } else if (ItemPackages.SCEPTER_LUGIAN_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ScepterLugianTemplate;
            } else if (ItemPackages.SHARD_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ShardTemplate;
            } else if (ItemPackages.SPEAR_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.SpearTemplate;
            } else if (ItemPackages.STAVE_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.StaveTemplate;
            } else if (ItemPackages.SWORD_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.SwordTemplate;
            } else if (ItemPackages.TOOL_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ToolTemplate;
            } else if (ItemPackages.TROPHY_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.TrophyTemplate;
            } else if (ItemPackages.WASPNEST_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.WaspnestTemplate;
            } else if (ItemPackages.WRENCH_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.WrenchTemplate;
            } else if (QuestPackages.ACT_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ActTemplate;
            } else if (QuestPackages.QUEST_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.QuestTemplate;
            } else if (QuestPackages.QUEST_VAULT_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.QuestVaultTemplate;
            } else if (QuestPackages.QUEST_TRIGGER_EFFECT_PACKAGES.Contains(packageType)) {
                packageType = PackageType.QuestTriggerEffect;
            } else if (SkillPackages.ACTIVE_SKILL_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.ActiveSkillTemplate;
            } else if (SkillPackages.ATTRIBUTE_SKILL_PACKAGES.Contains(packageType)) {
                packageType = PackageType.AttributeSkill;
            } else if (SkillPackages.PASSIVE_SKILL_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.PassiveSkillTemplate;
            } else if (SkillPackages.SKILL_PANEL_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.SkillPanelTemplate;
            } else if (SkillPackages.SR_FORMULA_TEMPLATE_PACKAGES.Contains(packageType)) {
                packageType = PackageType.SRFormulaTemplate;
            } else if (UsagePackages.USAGE_ACTION_PACKAGES.Contains(packageType)) {
                packageType = PackageType.UsageAction;
            }

            switch (packageType) {
                case PackageType.AbilityCalculator:
                    return new AbilityCalculator(data);
                case PackageType.Act:
                    return new Act(data);
                case PackageType.ActiveSkill:
                    return new ActiveSkill(data);
                case PackageType.ActiveSkillTemplate:
                    return new ActiveSkillTemplate(data);
                case PackageType.ActRegistry:
                    return new ActRegistry(data);
                case PackageType.ActTemplate:
                    return new ActTemplate(data);
                case PackageType.Agent:
                    return new Agent(data);
                case PackageType.AIBehaviorEffect:
                    return new AIBehaviorEffect(data);
                case PackageType.AIConcealEffect:
                    return new AIConcealEffect(data);
                case PackageType.AIPetEffect:
                    return new AIPetEffect(data);
                case PackageType.AIVoterSwapEffect:
                    return new AIVoterSwapEffect(data);
                case PackageType.AlchemyTemplate:
                    return new AlchemyTemplate(data);
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
                case PackageType.ArmorTemplate:
                    return new ArmorTemplate(data);
                case PackageType.AttackHook:
                    return new AttackHook(data);
                case PackageType.AttributeSkill:
                    return new AttributeSkill(data);
                case PackageType.AttunementEffect:
                    return new AttunementEffect(data);
                case PackageType.AuraEffect:
                    return new AuraEffect(data);
                case PackageType.AxeTemplate:
                    return new AxeTemplate(data);
                case PackageType.BiasProfile:
                    return new BiasProfile(data);
                case PackageType.BindRecipeAction:
                    return new BindRecipeAction(data);
                case PackageType.Book:
                    return new Book(data);
                case PackageType.BookTemplate:
                    return new BookTemplate(data);
                case PackageType.ButcheryProfile:
                    return new ButcheryProfile(data);
                case PackageType.CAExcavationPoint:
                    return new CAExcavationPoint(data);
                case PackageType.CAgent:
                    return new CAgent(data);
                case PackageType.CContainer:
                    return new CContainer(data);
                case PackageType.CestasTemplate:
                    return new CestasTemplate(data);
                case PackageType.ChainedInstantEffect:
                    return new ChainedInstantEffect(data);
                case PackageType.ChainedNumericEffect:
                    return new ChainedNumericEffect(data);
                case PackageType.ChannelData:
                    return new ChannelData(data);
                case PackageType.ChestAdminTemplate:
                    return new ChestAdminTemplate(data);
                case PackageType.ChestTemplate:
                    return new ChestTemplate(data);
                case PackageType.CItem:
                    return new CItem(data);
                case PackageType.ClassFilter:
                    return new ClassFilter(data);
                case PackageType.Clothing:
                    return new Clothing(data);
                case PackageType.ClothingTemplate:
                    return new ClothingTemplate(data);
                case PackageType.CMoneySystem:
                    return new CMoneySystem(data);
                case PackageType.CMonster:
                    return new CMonster(data);
                case PackageType.Coin:
                    return new Coin(data);
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
                case PackageType.ContainerTemplate:
                    return new ContainerTemplate(data);
                case PackageType.Corpse:
                    return new Corpse(data);
                case PackageType.CountdownEffect:
                    return new CountdownEffect(data);
                case PackageType.CraftCheckEntry:
                    return new CraftCheckEntry(data);
                case PackageType.CraftNumericEffect:
                    return new CraftNumericEffect(data);
                case PackageType.CraftRandomEntry:
                    return new CraftRandomEntry(data);
                case PackageType.CraftRegistry:
                    return new CraftRegistry(data);
                case PackageType.CraftSkill:
                    return new CraftSkill(data);
                case PackageType.CraftSkillRecord:
                    return new CraftSkillRecord(data);
                case PackageType.CustomCorpseTemplate:
                    return new CustomCorpseTemplate(data);
                case PackageType.CustomFailureRecipeAction:
                    return new CustomFailureRecipeAction(data);
                case PackageType.CustomSuccessRecipeAction:
                    return new CustomSuccessRecipeAction(data);
                case PackageType.CWeapon:
                    return new CWeapon(data);
                case PackageType.DeathPortal:
                    return new DeathPortal(data);
                case PackageType.DestroyRecipeAction:
                    return new DestroyRecipeAction(data);
                case PackageType.DefaultPermissionBlob:
                    return new DefaultPermissionBlob(data);
                case PackageType.DispelEffect:
                    return new DispelEffect(data);
                case PackageType.Door:
                    return new Door(data);
                case PackageType.DoorTemplate:
                    return new DoorTemplate(data);
                case PackageType.DrudgeArmorTemplate:
                    return new DrudgeArmorTemplate(data);
                case PackageType.DrumTemplate:
                    return new DrumTemplate(data);
                case PackageType.DurabilityFilter:
                    return new DurabilityFilter(data);
                case PackageType.EarringsToadfang:
                    return new JewelryTemplate(data);
                case PackageType.Eff_AIPetIgnore:
                    return new AIBehaviorEffect(data);
                case PackageType.Eff_NullEffect:
                    return new Eff_NullEffect(data);
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
                case PackageType.EmpyreanArmorTemplate:
                    return new EmpyreanArmorTemplate(data);
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
                case PackageType.FrogstaveTemplate:
                    return new FrogstaveTemplate(data);
                case PackageType.GameEventEffect:
                    return new GameEventEffect(data);
                case PackageType.GameplayContainer:
                    return new GameplayContainer(data);
                case PackageType.GameSaleProfile:
                    return new GameSaleProfile(data);
                case PackageType.GemTemplate:
                    return new GemTemplate(data);
                case PackageType.Generator:
                    return new Generator(data);
                case PackageType.GenericPortal:
                    return new GenericPortal(data);
                case PackageType.GenesisEffect:
                    return new GenesisEffect(data);
                case PackageType.GlyphTemplate:
                    return new GlyphTemplate(data);
                case PackageType.gmCEntity:
                    return new gmCEntity(data);
                case PackageType.gmEntity:
                    return new gmEntity(data);
                case PackageType.GrantRecipeEffect:
                    return new GrantRecipeEffect(data);
                case PackageType.GRSystemTemplate:
                    return new GRSystemTemplate(data);
                case PackageType.HandTemplate:
                    return new HandTemplate(data);
                case PackageType.HarmonizeRecipeAction:
                    return new HarmonizeRecipeAction(data);
                case PackageType.HotspotEffect:
                    return new HotspotEffect(data);
                case PackageType.HumanArmorTemplate:
                    return new HumanArmorTemplate(data);
                case PackageType.IClothing:
                    return new IClothing(data);
                case PackageType.IItem:
                    return new IItem(data);
                case PackageType.ImbuingStone:
                    return new Interactor(data);
                case PackageType.ImpulseEffect:
                    return new ImpulseEffect(data);
                case PackageType.Ingredient:
                    return new Ingredient(data);
                case PackageType.InstantAuraEffect:
                    return new InstantAuraEffect(data);
                case PackageType.InstantEffect:
                    return new InstantEffect(data);
                case PackageType.InstantNumericQualitiesEffect:
                    return new InstantNumericQualitiesEffect(data);
                case PackageType.InstantVitalEffect:
                    return new InstantVitalEffect(data);
                case PackageType.InteractionTable:
                    return new InteractionTable(data);
                case PackageType.Interactor:
                    return new Interactor(data);
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
                case PackageType.Item:
                    return new Item(data);
                case PackageType.ItemEffectRecipeAction:
                    return new ItemEffectRecipeAction(data);
                case PackageType.ItemInteractionOutcome:
                    return new ItemInteractionOutcome(data);
                case PackageType.IWeapon:
                    return new IWeapon(data);
                case PackageType.Jewelry:
                    return new Jewelry(data);
                case PackageType.JewelryTemplate:
                    return new JewelryTemplate(data);
                case PackageType.Key:
                    return new Key(data);
                case PackageType.KeyTemplate:
                    return new KeyTemplate(data);
                case PackageType.LevelFilter:
                    return new LevelFilter(data);
                case PackageType.LevelMappingTable:
                    return new LevelMappingTable(data);
                case PackageType.LinearAttackHook:
                    return new LinearAttackHook(data);
                case PackageType.LinkerEffect:
                    return new LinkerEffect(data);
                case PackageType.LogInfo:
                    return new LogInfo(data);
                case PackageType.LoreFilter:
                    return new LoreFilter(data);
                case PackageType.LugianArmorTemplate:
                    return new LugianArmorTemplate(data);
                case PackageType.MajorSpellbindingRecipe:
                    return new MajorSpellbindingRecipe(data);
                case PackageType.MapTemplate:
                    return new MapTemplate(data);
                case PackageType.MasterDIDListMember:
                    return new MasterDIDListMember(data);
                case PackageType.MasterList:
                    return new MasterList(data);
                case PackageType.MasterListMember:
                    return new MasterListMember(data);
                case PackageType.MineGenesisEffect:
                    return new MineGenesisEffect(data);
                case PackageType.MineTemplate:
                    return new MineTemplate(data);
                case PackageType.MiningToolTemplate:
                    return new MiningToolTemplate(data);
                case PackageType.MinorSpellbindingRecipe:
                    return new MinorSpellbindingRecipe(data);
                case PackageType.MoneySystem:
                    return new MoneySystem(data);
                case PackageType.Monster:
                    return new Monster(data);
                case PackageType.MonsterTemplate:
                    return new MonsterTemplate(data);
                case PackageType.MoteTemplate:
                    return new MoteTemplate(data);
                case PackageType.MountEffect:
                    return new MountEffect(data);
                case PackageType.MutateRecipeAction:
                    return new MutateRecipeAction(data);
                case PackageType.NPC:
                    return new NPC(data);
                case PackageType.NPCGuardTemplate:
                    return new NPCGuardTemplate(data);
                case PackageType.NPCImmobileTemplate:
                    return new NPCImmobileTemplate(data);
                case PackageType.NPCTable:
                    return new NPCTable(data);
                case PackageType.ParameterizedNumericEffect:
                    return new ParameterizedNumericEffect(data);
                case PackageType.PassiveSkillTemplate:
                    return new PassiveSkillTemplate(data);
                case PackageType.PetData:
                    return new PetData(data);
                case PackageType.PerkSkill:
                    return new PerkSkill(data);
                case PackageType.PerkTemplate:
                    return new PerkTemplate(data);
                case PackageType.PhaseInfo:
                    return new PhaseInfo(data);
                case PackageType.PlayerEffectRecipeAction:
                    return new PlayerEffectRecipeAction(data);
                case PackageType.PlayerSaleProfile:
                    return new PlayerSaleProfile(data);
                case PackageType.PopupEffectTemplate:
                    return new PopupEffectTemplate(data);
                case PackageType.Portal:
                    return new Portal(data);
                case PackageType.PortalDoorTemplate:
                    return new PortalDoorTemplate(data);
                case PackageType.PortalSummoned:
                    return new PortalTemplate(data);
                case PackageType.PortalTemplate:
                    return new PortalTemplate(data);
                case PackageType.PotionTemplate:
                    return new PotionTemplate(data);
                case PackageType.ProduceRecipeAction:
                    return new ProduceRecipeAction(data);
                case PackageType.ProtectionEffect:
                    return new ProtectionEffect(data);
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
                case PackageType.Recipe:
                    return new Recipe(data);
                case PackageType.RecipeAction:
                    return new RecipeAction(data);
                case PackageType.RecipeNameColoringTable:
                    return new RecipeNameColoringTable(data);
                case PackageType.RecipeRecord:
                    return new RecipeRecord(data);
                case PackageType.RefiningRecipe:
                    return new RefiningRecipe(data);
                case PackageType.ReflectiveVitalEffect:
                    return new ReflectiveVitalEffect(data);
                case PackageType.RemoveRecipeEffect:
                    return new RemoveRecipeEffect(data);
                case PackageType.ResurrectionRequest:
                    return new ResurrectionRequest(data);
                case PackageType.RingTemplate:
                    return new RingTemplate(data);
                case PackageType.RoadSignTemplate:
                    return new RoadSignTemplate(data);
                case PackageType.SaddleTemplate:
                    return new SaddleTemplate(data);
                case PackageType.SaddleTest:
                    return new SaddleTemplate(data);
                case PackageType.SaleProfile:
                    return new SaleProfile(data);
                case PackageType.SaleTemplate:
                    return new SaleTemplate(data);
                case PackageType.ScepterLugianTemplate:
                    return new ScepterLugianTemplate(data);
                case PackageType.SceneryObject:
                    return new SceneryObject(data);
                case PackageType.Shard:
                    return new Shard(data);
                case PackageType.ShardTemplate:
                    return new ShardTemplate(data);
                case PackageType.Skill:
                    return new Skill(data);
                case PackageType.SkillCheck:
                    return new SkillCheck(data);
                case PackageType.SkillInfo:
                    return new SkillInfo(data);
                case PackageType.SkillPanel:
                    return new SkillPanel(data);
                case PackageType.SkillPanelTemplate:
                    return new SkillPanelTemplate(data);
                case PackageType.SkillRepository:
                    return new SkillRepository(data);
                case PackageType.SkillTrainingEffect:
                    return new SkillTrainingEffect(data);
                case PackageType.SlayerEffect:
                    return new SlayerEffect(data);
                case PackageType.SpearTemplate:
                    return new SpearTemplate(data);
                case PackageType.SpellbindingRecipe:
                    return new SpellbindingRecipe(data);
                case PackageType.StampRecipeAction:
                    return new StampRecipeAction(data);
                case PackageType.StaticAttackHook:
                    return new StaticAttackHook(data);
                case PackageType.SRFormula:
                    return new SRFormula(data);
                case PackageType.SRFormulaTemplate:
                    return new SRFormulaTemplate(data);
                case PackageType.StaveTemplate:
                    return new StaveTemplate(data);
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
                case PackageType.SwordTemplate:
                    return new SwordTemplate(data);
                case PackageType.TalismanTemplate:
                    return new TalismanTemplate(data);
                case PackageType.TargetInteraction:
                    return new TargetInteraction(data);
                case PackageType.TargetLevelFilter:
                    return new TargetLevelFilter(data);
                case PackageType.TargetLoreFilter:
                    return new TargetLoreFilter(data);
                case PackageType.TeachingStoneTemplate:
                    return new TeachingStoneTemplate(data);
                case PackageType.TextEffect:
                    return new TextEffect(data);
                case PackageType.TextRecipeAction:
                    return new TextRecipeAction(data);
                case PackageType.Tool:
                    return new Tool(data);
                case PackageType.ToolTemplate:
                    return new ToolTemplate(data);
                case PackageType.Trade:
                    return new Trade(data);
                case PackageType.TransactionBlob:
                    return new TransactionBlob(data);
                case PackageType.TransparentNumericEffect:
                    return new TransparentNumericEffect(data);
                case PackageType.TrophyTemplate:
                    return new TrophyTemplate(data);
                case PackageType.TrumpSceptersTemplate:
                    return new TrumpSceptersTemplate(data);
                case PackageType.TrumpSwordsTemplate:
                    return new TrumpSwordsTemplate(data);
                case PackageType.TrumpTemplate:
                    return new TrumpTemplate(data);
                case PackageType.TumerokArmorTemplate:
                    return new TumerokArmorTemplate(data);
                case PackageType.UsageBlob:
                    return new UsageBlob(data);
                case PackageType.UsageAction:
                    return new UsageAction(data);
                case PackageType.UsageDesc:
                    return new UsageDesc(data);
                case PackageType.UsagePermission:
                    return new UsagePermission(data);
                case PackageType.VisualDescEffect:
                    return new VisualDescEffect(data);
                case PackageType.VisualDescInfo:
                    return new VisualDescInfo(data);
                case PackageType.VitalOverTimeEffect:
                    return new VitalOverTimeEffect(data);
                case PackageType.VitalTransferEffect:
                    return new VitalTransferEffect(data);
                case PackageType.WaspnestTemplate:
                    return new WaspnestTemplate(data);
                case PackageType.Weapon:
                    return new Weapon(data);
                case PackageType.WeaponTemplate:
                    return new WeaponTemplate(data);
                case PackageType.WrenchTemplate:
                    return new WrenchTemplate(data);
                default:
                    throw new NotImplementedException($"Unhandled read for package type {packageType}.");
            }
        }
    }
}
