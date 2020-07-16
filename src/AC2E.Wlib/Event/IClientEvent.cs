using System;
using System.IO;

namespace AC2E.WLib {

    public interface IClientEvent {

        ClientEventFunctionId funcId { get; } // _fid

        void write(BinaryWriter data);

        public static IClientEvent read(ClientEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ClientEventFunctionId.Allegiance__UpdateAllegianceChatDone:
                    return new UpdateAllegianceChatDoneCEvt(data);
                case ClientEventFunctionId.Allegiance__UpdateAllegianceProfile:
                    return new UpdateAllegianceProfileCEvt(data);

                case ClientEventFunctionId.Combat__Combat_BasicAttacksFailed:
                    return new BasicAttacksFailedCEvt(data);
                case ClientEventFunctionId.Combat__UpdateAttackState:
                    return new UpdateAttackStateCEvt(data);

                case ClientEventFunctionId.Communication__DisplayStringInfo:
                    return new DisplayStringInfoCEvt(data);
                case ClientEventFunctionId.Communication__CDoSay:
                    return new DoSayCEvt(data);
                case ClientEventFunctionId.Communication__CHearTell:
                    return new HearTellCEvt(data);
                case ClientEventFunctionId.Communication__RefreshChannels:
                    return new RefreshChannelsCEvt(data);

                case ClientEventFunctionId.Craft__CraftRefresh:
                    return new CraftRefreshCEvt(data);
                case ClientEventFunctionId.Craft__UpdateCraftSkill_Done:
                    return new UpdateCraftSkillDoneCEvt(data);
                case ClientEventFunctionId.Craft__UpdateCraftSkills_Done:
                    return new UpdateCraftSkillsDoneCEvt(data);

                case ClientEventFunctionId.Effect__ClientAddEffect:
                    return new ClientAddEffectCEvt(data);
                case ClientEventFunctionId.Effect__ClientRemoveEffect:
                    return new ClientRemoveEffectCEvt(data);

                case ClientEventFunctionId.Examination__UpdateExaminationProfile:
                    return new UpdateExaminationProfileCEvt(data);

                case ClientEventFunctionId.Fellowship__UpdateFellowCellID:
                    return new UpdateFellowCellIdCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowHealth:
                    return new UpdateFellowHealthCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowLevel:
                    return new UpdateFellowLevelCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth:
                    return new UpdateFellowMaxHealthCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor:
                    return new UpdateFellowMaxVigorCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowVigor:
                    return new UpdateFellowVigorCEvt(data);

                case ClientEventFunctionId.Inventory__CloseContainer_Done:
                    return new CloseContainerDoneCEvt(data);
                case ClientEventFunctionId.Inventory__DiscreteDestroyContainedItem:
                    return new DiscreteDestroyContainedItemCEvt(data);
                case ClientEventFunctionId.Inventory__MoveItem_Done:
                    return new MoveItemDoneCEvt(data);
                case ClientEventFunctionId.Inventory__Inventory_ResetContents:
                    return new ResetContentsCEvt(data);
                case ClientEventFunctionId.Inventory__TransmuteAllFromContainer_Done:
                    return new TransmuteAllFromContainerDoneCEvt(data);

                case ClientEventFunctionId.Player__EnterPortalSpace:
                    return new EnterPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__ExitPortalSpace:
                    return new ExitPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__HandleCharacterSessionStart:
                    return new HandleCharacterSessionStartCEvt(data);
                case ClientEventFunctionId.Player__SetAnimationFrozen:
                    return new SetAnimationFrozenCEvt(data);
                case ClientEventFunctionId.Player__SetMovementFrozen:
                    return new SetMovementFrozenCEvt(data);
                case ClientEventFunctionId.Player__UpdateSelectionInfo:
                    return new UpdateSelectionInfoCEvt(data);

                case ClientEventFunctionId.Store__LeaveCatalog:
                    return new LeaveCatalogCEvt(data);

                case ClientEventFunctionId.Usage__TryToUseItem_Done:
                    return new TryToUseItemDoneCEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled client event: {funcId}.");
            }
        }
    }
}
