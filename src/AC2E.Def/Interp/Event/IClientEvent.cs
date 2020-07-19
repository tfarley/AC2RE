using System;
using System.IO;

namespace AC2E.Def {

    public interface IClientEvent {

        ClientEventFunctionId funcId { get; } // _fid

        void write(BinaryWriter data);

        public static IClientEvent read(ClientEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ClientEventFunctionId.AI__PetAdd:
                    return new PetAddCEvt(data);
                case ClientEventFunctionId.AI__PetRemove:
                    return new PetRemoveCEvt(data);
                case ClientEventFunctionId.AI__UpdatePetMode:
                    return new UpdatePetModeCEvt(data);
                case ClientEventFunctionId.AI__UpdatePetName:
                    return new UpdatePetNameCEvt(data);

                case ClientEventFunctionId.Allegiance__HandleAllegianceHierarchyForExport:
                    return new HandleAllegianceHierarchyForExportCEvt(data);
                case ClientEventFunctionId.Allegiance__Patron_SwearDone:
                    return new PatronSwearDoneCEvt(data);
                case ClientEventFunctionId.Allegiance__RequestVassal:
                    return new RequestVassalCEvt(data);
                case ClientEventFunctionId.Allegiance__UpdateAllegianceChatDone:
                    return new UpdateAllegianceChatDoneCEvt(data);
                case ClientEventFunctionId.Allegiance__UpdateAllegianceProfile:
                    return new UpdateAllegianceProfileCEvt(data);
                case ClientEventFunctionId.Allegiance__Vassal_SwearDone:
                    return new VassalSwearDoneCEvt(data);

                case ClientEventFunctionId.Combat__Combat_AttackError:
                    return new AttackErrorCEvt(data);
                case ClientEventFunctionId.Combat__Combat_BasicAttacksFailed:
                    return new BasicAttacksFailedCEvt(data);
                case ClientEventFunctionId.Combat__EndSpecialAttack:
                    return new EndSpecialAttackCEvt(data);
                case ClientEventFunctionId.Combat__ToggleSpecialAttack:
                    return new ToggleSpecialAttackCEvt(data);
                case ClientEventFunctionId.Combat__UpdateAttackState:
                    return new UpdateAttackStateCEvt(data);

                case ClientEventFunctionId.Communication__CBroadcastStringInfoLocal:
                    return new CBroadcastStringInfoLocalCEvt(data);
                case ClientEventFunctionId.Communication__DisplayStringInfo:
                    return new DisplayStringInfoCEvt(data);
                case ClientEventFunctionId.Communication__CDoSay:
                    return new DoSayCEvt(data);
                case ClientEventFunctionId.Communication__HearCustomEmote:
                    return new HearCustomEmoteCEvt(data);
                case ClientEventFunctionId.Communication__CHearTell:
                    return new HearTellCEvt(data);
                case ClientEventFunctionId.Communication__RefreshChannels:
                    return new RefreshChannelsCEvt(data);

                case ClientEventFunctionId.Craft__AddIngredient_Done:
                    return new AddIngredientDoneCEvt(data);
                case ClientEventFunctionId.Craft__AddRecipe_Done:
                    return new AddRecipeDoneCEvt(data);
                case ClientEventFunctionId.Craft__CraftRefresh:
                    return new CraftRefreshCEvt(data);
                case ClientEventFunctionId.Craft__ExecuteCraft_Done:
                    return new ExecuteCraftDoneCEvt(data);
                case ClientEventFunctionId.Craft__RemoveRecipe_Done:
                    return new RemoveRecipeDoneCEvt(data);
                case ClientEventFunctionId.Craft__UpdateCraftSkill_Done:
                    return new UpdateCraftSkillDoneCEvt(data);
                case ClientEventFunctionId.Craft__UpdateCraftSkills_Done:
                    return new UpdateCraftSkillsDoneCEvt(data);
                case ClientEventFunctionId.Craft__UpdateRecipe_Done:
                    return new UpdateRecipeDoneCEvt(data);
                case ClientEventFunctionId.Craft__UpdateRecipes_Done:
                    return new UpdateRecipesDoneCEvt(data);

                case ClientEventFunctionId.Death__AnnounceWhoKilledMe:
                    return new AnnounceWhoKilledMeCEvt(data);
                case ClientEventFunctionId.Death__DisplayDeathMsg:
                    return new DisplayDeathMessageCEvt(data);
                case ClientEventFunctionId.Death__DisplayKillingMsg:
                    return new DisplayKillingMessageCEvt(data);
                case ClientEventFunctionId.Death__ResurrectionRequestedByOther:
                    return new ResurrectionRequestedByOtherCEvt(data);
                case ClientEventFunctionId.Death__UpdateDeathState:
                    return new UpdateDeathStateCEvt(data);

                case ClientEventFunctionId.Effect__ClientAddEffect:
                    return new ClientAddEffectCEvt(data);
                case ClientEventFunctionId.Effect__ClientRemoveEffect:
                    return new ClientRemoveEffectCEvt(data);
                case ClientEventFunctionId.Effect__PulseMissed:
                    return new EffectPulseMissedCEvt(data);

                case ClientEventFunctionId.Examination__UpdateExaminationProfile:
                    return new UpdateExaminationProfileCEvt(data);

                case ClientEventFunctionId.Fellowship__ClearFellow:
                    return new ClearFellowCEvt(data);
                case ClientEventFunctionId.Fellowship__ClearFellowship:
                    return new EmptyCEvt(funcId);
                case ClientEventFunctionId.Fellowship__ExpireRecruitment:
                    return new ExpireRecruitmentCEvt(data);
                case ClientEventFunctionId.Fellowship__RequestRecruitment:
                    return new RequestRecruitmentCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowCellID:
                    return new UpdateFellowCellIdCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellow:
                    return new UpdateFellowCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowHealth:
                    return new UpdateFellowHealthCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowLevel:
                    return new UpdateFellowLevelCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth:
                    return new UpdateFellowMaxHealthCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor:
                    return new UpdateFellowMaxVigorCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowship:
                    return new UpdateFellowshipCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowVigor:
                    return new UpdateFellowVigorCEvt(data);

                case ClientEventFunctionId.Inventory__CloseContainer_Done:
                    return new CloseContainerDoneCEvt(data);
                case ClientEventFunctionId.Inventory__DestroyContainedItem:
                    return new DestroyContainedItemCEvt(data);
                case ClientEventFunctionId.Inventory__DiscreteDestroyContainedItem:
                    return new DiscreteDestroyContainedItemCEvt(data);
                case ClientEventFunctionId.Inventory__EquipItem_Done:
                    return new EquipItemDoneCEvt(data);
                case ClientEventFunctionId.Inventory__MoveItem_Done:
                    return new MoveItemDoneCEvt(data);
                case ClientEventFunctionId.Inventory__OpenContainer_Done:
                    return new OpenContainerDoneCEvt(data);
                case ClientEventFunctionId.Inventory__ReorganizeContents_Done:
                    return new ReorganizeContentsDoneCEvt(data);
                case ClientEventFunctionId.Inventory__Inventory_ResetContents:
                    return new ResetContentsCEvt(data);
                case ClientEventFunctionId.Inventory__TakeAllFromContainer_Done:
                    return new TakeAllFromContainerDoneCEvt(data);
                case ClientEventFunctionId.Inventory__TransmuteAllFromContainer_Done:
                    return new TransmuteAllFromContainerDoneCEvt(data);
                case ClientEventFunctionId.Inventory__UnEquipItem_Done:
                    return new UnequipItemDoneCEvt(data);

                case ClientEventFunctionId.Money__DragFromMoneyBag_Done:
                    return new DragFromMoneyBagDoneCEvt(data);
                case ClientEventFunctionId.Money__DragToMoneyBag_Done:
                    return new DragToMoneyBagDoneCEvt(data);

                case ClientEventFunctionId.PK__Faction_UpdateStatus:
                    return new UpdateFactionStatusCEvt(data);

                case ClientEventFunctionId.Player__ClearMarkers:
                    return new ClearMarkersCEvt(data);
                case ClientEventFunctionId.Player__DisplayMarker:
                    return new DisplayMarkerCEvt(data);
                case ClientEventFunctionId.Player__DisplayMessage:
                    return new DisplayMessageCEvt(data);
                case ClientEventFunctionId.Player__EnterPortalScene:
                    return new EnterPortalSceneCEvt(data);
                case ClientEventFunctionId.Player__EnterPortalSpace:
                    return new EnterPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__ExitPortalScene:
                    return new ExitPortalSceneCEvt(data);
                case ClientEventFunctionId.Player__ExitPortalSpace:
                    return new ExitPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__ForceExamineItem:
                    return new ForceExamineItemCEvt(data);
                case ClientEventFunctionId.Player__HandleCharacterSessionStart:
                    return new HandleCharacterSessionStartCEvt(data);
                case ClientEventFunctionId.Player__ListCurrentlyTrainedSkills_Done:
                    return new ListCurrentlyTrainedSkillsDoneCEvt(data);
                case ClientEventFunctionId.Player__PortalStorm_Warning:
                    return new PortalStormWarningCEvt(data);
                case ClientEventFunctionId.Player__SetAnimationFrozen:
                    return new SetAnimationFrozenCEvt(data);
                case ClientEventFunctionId.Player__SetMovementFrozen:
                    return new SetMovementFrozenCEvt(data);
                case ClientEventFunctionId.Player__UpdateSelectionInfo:
                    return new UpdateSelectionInfoCEvt(data);

                case ClientEventFunctionId.Quest__PlayScenes:
                    return new PlayScenesCEvt(data);
                case ClientEventFunctionId.Quest__UpdateQuest_Done:
                    return new UpdateQuestCEvt(data);
                case ClientEventFunctionId.Quest__UpdateStoryQuest_Done:
                    return new UpdateStoryQuestDoneCEvt(data);

                case ClientEventFunctionId.Skill__RemoveInfo:
                    return new RemoveSkillInfoCEvt(data);
                case ClientEventFunctionId.Skill__UpdateEverything:
                    return new SkillUpdateEverythingCEvt(data);
                case ClientEventFunctionId.Skill__UpdateInfo:
                    return new UpdateSkillInfoCEvt(data);
                case ClientEventFunctionId.Skill__UpdateRepository:
                    return new UpdateSkillRepositoryCEvt(data);

                case ClientEventFunctionId.Store__EnterCatalog:
                    return new EnterCatalogCEvt(data);
                case ClientEventFunctionId.Store__EnterConsignment:
                    return new EnterConsignmentCEvt(data);
                case ClientEventFunctionId.Store__EnterStore:
                    return new EnterStoreCEvt(data);
                case ClientEventFunctionId.Store__InitSaleReminders:
                    return new InitSaleRemindersCEvt(data);
                case ClientEventFunctionId.Store__LeaveCatalog:
                    return new LeaveCatalogCEvt(data);
                case ClientEventFunctionId.Store__LeaveStore:
                    return new LeaveStoreCEvt(data);
                case ClientEventFunctionId.Store__Request_Done:
                    return new StoreRequestDoneCEvt(data);
                case ClientEventFunctionId.Store__UpdateCatalog:
                    return new UpdateCatalogCEvt(data);
                case ClientEventFunctionId.Store__UpdateConsignment:
                    return new UpdateConsignmentCEvt(data);
                case ClientEventFunctionId.Store__UpdateSale:
                    return new UpdateSaleCEvt(data);
                case ClientEventFunctionId.Store__UpdateStore:
                    return new UpdateStoreCEvt(data);

                case ClientEventFunctionId.Trade__BeAccepted:
                    return new TradeBeAcceptedCEvt(data);
                case ClientEventFunctionId.Trade__BeClosed:
                    return new TradeBeClosedCEvt(data);
                case ClientEventFunctionId.Trade__BeDeclined:
                    return new TradeBeDeclinedCEvt(data);
                case ClientEventFunctionId.Trade__BeFailed:
                    return new TradeBeFailedCEvt(data);
                case ClientEventFunctionId.Trade__BeOffered:
                    return new TradeBeOfferedCEvt(data);
                case ClientEventFunctionId.Trade__BeOpened:
                    return new EmptyCEvt(funcId);
                case ClientEventFunctionId.Trade__BeRefreshed:
                    return new TradeBeRefreshedCEvt(data);
                case ClientEventFunctionId.Trade__BeRegistered:
                    return new TradeBeRegisteredCEvt(data);
                case ClientEventFunctionId.Trade__BeReset:
                    return new EmptyCEvt(funcId);
                case ClientEventFunctionId.Trade__BeRevoked:
                    return new TradeBeRevokedCEvt(data);

                case ClientEventFunctionId.Usage__TryToUseItem_Done:
                    return new TryToUseItemDoneCEvt(data);
                case ClientEventFunctionId.Usage__UseBook:
                    return new UseBookCEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled client event: {funcId}.");
            }
        }
    }
}
