using System;

namespace AC2RE.Definitions;

public interface IClientEvent : IWritable {

    ClientEventFunctionId funcId { get; } // _fid

    public static IClientEvent read(ClientEventFunctionId funcId, AC2Reader data) {
        return funcId switch {
            ClientEventFunctionId.AI__PetAdd => new PetAddCEvt(data),
            ClientEventFunctionId.AI__PetRemove => new PetRemoveCEvt(data),
            ClientEventFunctionId.AI__UpdatePetMode => new UpdatePetModeCEvt(data),
            ClientEventFunctionId.AI__UpdatePetName => new UpdatePetNameCEvt(data),

            ClientEventFunctionId.Allegiance__HandleAllegianceHierarchyForExport => new HandleAllegianceHierarchyForExportCEvt(data),
            ClientEventFunctionId.Allegiance__Patron_SwearDone => new PatronSwearDoneCEvt(data),
            ClientEventFunctionId.Allegiance__RequestVassal => new RequestVassalCEvt(data),
            ClientEventFunctionId.Allegiance__UpdateAllegianceChatDone => new UpdateAllegianceChatDoneCEvt(data),
            ClientEventFunctionId.Allegiance__UpdateAllegianceProfile => new UpdateAllegianceProfileCEvt(data),
            ClientEventFunctionId.Allegiance__Vassal_SwearDone => new VassalSwearDoneCEvt(data),

            ClientEventFunctionId.Combat__AttackError => new AttackErrorCEvt(data),
            ClientEventFunctionId.Combat__BasicAttacksFailed => new BasicAttacksFailedCEvt(data),
            ClientEventFunctionId.Combat__EndSpecialAttack => new EndSpecialAttackCEvt(data),
            ClientEventFunctionId.Combat__ToggleSpecialAttack => new ToggleSpecialAttackCEvt(data),
            ClientEventFunctionId.Combat__UpdateAttackState => new UpdateAttackStateCEvt(data),

            ClientEventFunctionId.Communication__CBroadcastStringInfoLocal => new CBroadcastStringInfoLocalCEvt(data),
            ClientEventFunctionId.Communication__DisplayStringInfo => new DisplayStringInfoCEvt(data),
            ClientEventFunctionId.Communication__CDoSay => new DoSayCEvt(data),
            ClientEventFunctionId.Communication__HearCustomEmote => new HearCustomEmoteCEvt(data),
            ClientEventFunctionId.Communication__CHearTell => new HearTellCEvt(data),
            ClientEventFunctionId.Communication__RefreshChannels => new RefreshChannelsCEvt(data),

            ClientEventFunctionId.Craft__AddIngredient_Done => new AddIngredientDoneCEvt(data),
            ClientEventFunctionId.Craft__AddRecipe_Done => new AddRecipeDoneCEvt(data),
            ClientEventFunctionId.Craft__CraftRefresh => new CraftRefreshCEvt(data),
            ClientEventFunctionId.Craft__ExecuteCraft_Done => new ExecuteCraftDoneCEvt(data),
            ClientEventFunctionId.Craft__RemoveRecipe_Done => new RemoveRecipeDoneCEvt(data),
            ClientEventFunctionId.Craft__UpdateCraftSkill_Done => new UpdateCraftSkillDoneCEvt(data),
            ClientEventFunctionId.Craft__UpdateCraftSkills_Done => new UpdateCraftSkillsDoneCEvt(data),
            ClientEventFunctionId.Craft__UpdateRecipe_Done => new UpdateRecipeDoneCEvt(data),
            ClientEventFunctionId.Craft__UpdateRecipes_Done => new UpdateRecipesDoneCEvt(data),

            ClientEventFunctionId.Death__AnnounceWhoKilledMe => new AnnounceWhoKilledMeCEvt(data),
            ClientEventFunctionId.Death__DisplayDeathMsg => new DisplayDeathMessageCEvt(data),
            ClientEventFunctionId.Death__DisplayKillingMsg => new DisplayKillingMessageCEvt(data),
            ClientEventFunctionId.Death__ResurrectionRequestedByOther => new ResurrectionRequestedByOtherCEvt(data),
            ClientEventFunctionId.Death__UpdateDeathState => new UpdateDeathStateCEvt(data),

            ClientEventFunctionId.Effect__ClientAddEffect => new ClientAddEffectCEvt(data),
            ClientEventFunctionId.Effect__ClientRemoveEffect => new ClientRemoveEffectCEvt(data),
            ClientEventFunctionId.Effect__PulseMissed => new EffectPulseMissedCEvt(data),

            ClientEventFunctionId.Examination__UpdateExaminationProfile => new UpdateExaminationProfileCEvt(data),

            ClientEventFunctionId.Fellowship__ClearFellow => new ClearFellowCEvt(data),
            ClientEventFunctionId.Fellowship__ClearFellowship => new EmptyCEvt(funcId),
            ClientEventFunctionId.Fellowship__ExpireRecruitment => new ExpireRecruitmentCEvt(data),
            ClientEventFunctionId.Fellowship__RequestRecruitment => new RequestRecruitmentCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowCellID => new UpdateFellowCellIdCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellow => new UpdateFellowCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowHealth => new UpdateFellowHealthCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowLevel => new UpdateFellowLevelCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth => new UpdateFellowMaxHealthCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor => new UpdateFellowMaxVigorCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowship => new UpdateFellowshipCEvt(data),
            ClientEventFunctionId.Fellowship__UpdateFellowVigor => new UpdateFellowVigorCEvt(data),

            ClientEventFunctionId.Inventory__CloseContainer_Done => new CloseContainerDoneCEvt(data),
            ClientEventFunctionId.Inventory__DestroyContainedItem => new DestroyContainedItemCEvt(data),
            ClientEventFunctionId.Inventory__DiscreteDestroyContainedItem => new DiscreteDestroyContainedItemCEvt(data),
            ClientEventFunctionId.Inventory__EquipItem_Done => new EquipItemDoneCEvt(data),
            ClientEventFunctionId.Inventory__MoveItem_Done => new MoveItemDoneCEvt(data),
            ClientEventFunctionId.Inventory__OpenContainer_Done => new OpenContainerDoneCEvt(data),
            ClientEventFunctionId.Inventory__ReorganizeContents_Done => new ReorganizeContentsDoneCEvt(data),
            ClientEventFunctionId.Inventory__ResetContents => new ResetContentsCEvt(data),
            ClientEventFunctionId.Inventory__TakeAllFromContainer_Done => new TakeAllFromContainerDoneCEvt(data),
            ClientEventFunctionId.Inventory__TransmuteAllFromContainer_Done => new TransmuteAllFromContainerDoneCEvt(data),
            ClientEventFunctionId.Inventory__UnEquipItem_Done => new UnequipItemDoneCEvt(data),

            ClientEventFunctionId.Money__DragFromMoneyBag_Done => new DragFromMoneyBagDoneCEvt(data),
            ClientEventFunctionId.Money__DragToMoneyBag_Done => new DragToMoneyBagDoneCEvt(data),

            ClientEventFunctionId.PK__Faction_UpdateStatus => new UpdateFactionStatusCEvt(data),

            ClientEventFunctionId.Player__ClearMarkers => new ClearMarkersCEvt(data),
            ClientEventFunctionId.Player__DisplayMarker => new DisplayMarkerCEvt(data),
            ClientEventFunctionId.Player__DisplayMessage => new DisplayMessageCEvt(data),
            ClientEventFunctionId.Player__EnterPortalScene => new EnterPortalSceneCEvt(data),
            ClientEventFunctionId.Player__EnterPortalSpace => new EnterPortalSpaceCEvt(data),
            ClientEventFunctionId.Player__ExitPortalScene => new ExitPortalSceneCEvt(data),
            ClientEventFunctionId.Player__ExitPortalSpace => new ExitPortalSpaceCEvt(data),
            ClientEventFunctionId.Player__ForceExamineItem => new ForceExamineItemCEvt(data),
            ClientEventFunctionId.Player__HandleCharacterSessionStart => new HandleCharacterSessionStartCEvt(data),
            ClientEventFunctionId.Player__ListCurrentlyTrainedSkills_Done => new ListCurrentlyTrainedSkillsDoneCEvt(data),
            ClientEventFunctionId.Player__PortalStorm_Warning => new PortalStormWarningCEvt(data),
            ClientEventFunctionId.Player__SetAnimationFrozen => new SetAnimationFrozenCEvt(data),
            ClientEventFunctionId.Player__SetMovementFrozen => new SetMovementFrozenCEvt(data),
            ClientEventFunctionId.Player__UpdateSelectionInfo => new UpdateSelectionInfoCEvt(data),

            ClientEventFunctionId.Quest__PlayScenes => new PlayScenesCEvt(data),
            ClientEventFunctionId.Quest__UpdateQuest_Done => new UpdateQuestCEvt(data),
            ClientEventFunctionId.Quest__UpdateStoryQuest_Done => new UpdateStoryQuestDoneCEvt(data),

            ClientEventFunctionId.Skill__RemoveInfo => new RemoveSkillInfoCEvt(data),
            ClientEventFunctionId.Skill__UpdateEverything => new SkillUpdateEverythingCEvt(data),
            ClientEventFunctionId.Skill__UpdateInfo => new UpdateSkillInfoCEvt(data),
            ClientEventFunctionId.Skill__UpdateRepository => new UpdateSkillRepositoryCEvt(data),

            ClientEventFunctionId.Store__EnterCatalog => new EnterCatalogCEvt(data),
            ClientEventFunctionId.Store__EnterConsignment => new EnterConsignmentCEvt(data),
            ClientEventFunctionId.Store__EnterStore => new EnterStoreCEvt(data),
            ClientEventFunctionId.Store__InitSaleReminders => new InitSaleRemindersCEvt(data),
            ClientEventFunctionId.Store__LeaveCatalog => new LeaveCatalogCEvt(data),
            ClientEventFunctionId.Store__LeaveStore => new LeaveStoreCEvt(data),
            ClientEventFunctionId.Store__Request_Done => new StoreRequestDoneCEvt(data),
            ClientEventFunctionId.Store__UpdateCatalog => new UpdateCatalogCEvt(data),
            ClientEventFunctionId.Store__UpdateConsignment => new UpdateConsignmentCEvt(data),
            ClientEventFunctionId.Store__UpdateSale => new UpdateSaleCEvt(data),
            ClientEventFunctionId.Store__UpdateStore => new UpdateStoreCEvt(data),

            ClientEventFunctionId.Trade__BeAccepted => new TradeBeAcceptedCEvt(data),
            ClientEventFunctionId.Trade__BeClosed => new TradeBeClosedCEvt(data),
            ClientEventFunctionId.Trade__BeDeclined => new TradeBeDeclinedCEvt(data),
            ClientEventFunctionId.Trade__BeFailed => new TradeBeFailedCEvt(data),
            ClientEventFunctionId.Trade__BeOffered => new TradeBeOfferedCEvt(data),
            ClientEventFunctionId.Trade__BeOpened => new EmptyCEvt(funcId),
            ClientEventFunctionId.Trade__BeRefreshed => new TradeBeRefreshedCEvt(data),
            ClientEventFunctionId.Trade__BeRegistered => new TradeBeRegisteredCEvt(data),
            ClientEventFunctionId.Trade__BeReset => new EmptyCEvt(funcId),
            ClientEventFunctionId.Trade__BeRevoked => new TradeBeRevokedCEvt(data),

            ClientEventFunctionId.Usage__TryToUseItem_Done => new TryToUseItemDoneCEvt(data),
            ClientEventFunctionId.Usage__UseBook => new UseBookCEvt(data),

            _ => throw new NotImplementedException($"Unhandled client event: {funcId}."),
        };
    }
}
