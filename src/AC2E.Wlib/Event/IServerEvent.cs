using System;
using System.IO;

namespace AC2E.WLib {

    public interface IServerEvent {

        ServerEventFunctionId funcId { get; } // _fid

        public static IServerEvent read(ServerEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ServerEventFunctionId.Allegiance__AcceptVassal:
                    return new AcceptVassalSEvt(data);
                case ServerEventFunctionId.Allegiance__QueryAllegianceProfile:
                    return new QueryAllegianceProfileSEvt(data);
                case ServerEventFunctionId.Allegiance__QueryAllegiance:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Allegiance__SwearAllegiance:
                    return new SwearAllegianceSEvt(data);

                case ServerEventFunctionId.Combat__DoAttack:
                    return new DoAttackSEvt(data);
                case ServerEventFunctionId.Combat__SetNextTarget:
                    return new SetNextTargetSEvt(data);
                case ServerEventFunctionId.Combat__StartAttack:
                    return new StartAttackSEvt(data);
                case ServerEventFunctionId.Combat__StopAttacking:
                    return new StopAttackingSEvt(data);
                case ServerEventFunctionId.Combat__StopAttack:
                    return new EmptySEvt(funcId);

                case ServerEventFunctionId.Communication__PrefabEmote:
                    return new PrefabEmoteSEvt(data);
                case ServerEventFunctionId.Communication__Say:
                    return new SaySEvt(data);
                case ServerEventFunctionId.Communication__Tell:
                    return new TellSEvt(data);

                case ServerEventFunctionId.Craft__RequestAddIngredient:
                    return new RequestAddIngredientSEvt(data);
                case ServerEventFunctionId.Craft__RequestExecuteCraft:
                    return new RequestExecuteCraftSEvt(data);
                case ServerEventFunctionId.Craft__RequestRaiseCraftSkill:
                    return new RequestRaiseCraftSkillSEvt(data);

                case ServerEventFunctionId.Death__RequestResurrect:
                    return new EmptySEvt(funcId);

                case ServerEventFunctionId.Examination__QueryExaminationProfile:
                    return new QueryExaminationProfileSEvt(data);

                case ServerEventFunctionId.Fellowship__AcceptRecruitment:
                    return new AcceptRecruitmentSEvt(data);
                case ServerEventFunctionId.Fellowship__CreateFellowship:
                    return new CreateFellowshipSEvt(data);
                case ServerEventFunctionId.Fellowship__DisbandFellowship:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Fellowship__LeaveFellowship:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Fellowship__RecruitFellow:
                    return new RecruitFellowSEvt(data);

                case ServerEventFunctionId.Inventory__CloseContainer:
                    return new CloseContainerSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveEquipItem:
                    return new DirectiveEquipItemSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveMoveItem:
                    return new DirectiveMoveItemSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveReorganizeContents:
                    return new DirectiveReorganizeContentsSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveTakeAllFromContainer:
                    return new DirectiveTakeAllFromContainerSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveTransmuteAllFromContainer:
                    return new DirectiveTransmuteAllFromContainerSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveUnEquipItem:
                    return new DirectiveUnequipItemSEvt(data);

                case ServerEventFunctionId.Money__DragFromMoneyBag:
                    return new DragFromMoneyBagSEvt(data);
                case ServerEventFunctionId.Money__DragToMoneyBag:
                    return new DragToMoneyBagSEvt(data);

                case ServerEventFunctionId.Player__Dismount:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Player__Follow:
                    return new FollowSEvt(data);
                case ServerEventFunctionId.Player__InscribeItem:
                    return new InscribeItemSEvt(data);
                case ServerEventFunctionId.Player__ListCurrentlyTrainedSkills:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Player__RequestPetAttack:
                    return new RequestPetAttackSEvt(data);
                case ServerEventFunctionId.Player__RequestPetDie:
                    return new RequestPetDieSEvt(data);
                case ServerEventFunctionId.Player__RequestPetBuff:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Player__RequestPetHeal:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Player__RequestPetMode:
                    return new RequestPetModeSEvt(data);
                case ServerEventFunctionId.Player__RequestPetReturn:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Player__RequestSetPetName:
                    return new RequestSetPetNameSEvt(data);
                case ServerEventFunctionId.Player__SetAlias:
                    return new SetAliasSEvt(data);
                case ServerEventFunctionId.Player__SetSelection:
                    return new SetSelectionSEvt(data);
                case ServerEventFunctionId.Player__SetShortcut:
                    return new SetShortcutSEvt(data);
                case ServerEventFunctionId.Player__UploadChatOptions:
                    return new UploadChatOptionsSEvt(data);
                case ServerEventFunctionId.Player__UploadGameOptions:
                    return new UploadGameOptionsSEvt(data);
                case ServerEventFunctionId.Player__UploadUISettings:
                    return new UploadUISettingsSEvt(data);

                case ServerEventFunctionId.Quest__RequestCancelQuest:
                    return new RequestCancelQuestSEvt(data);

                case ServerEventFunctionId.Skill__RequestRaiseSkill:
                    return new RequestRaiseSkillSEvt(data);
                case ServerEventFunctionId.Skill__RequestTrainSkill:
                    return new RequestTrainSkillSEvt(data);
                case ServerEventFunctionId.Skill__RequestUntrainSkill:
                    return new RequestUntrainSkillSEvt(data);
                case ServerEventFunctionId.Skill__RequestUntrainSkillCancel:
                    return new EmptySEvt(funcId);

                case ServerEventFunctionId.Store__PurchaseItemFromStore:
                    return new PurchaseItemFromStoreSEvt(data);
                case ServerEventFunctionId.Store__RequestEnterStore:
                    return new RequestEnterStoreSEvt(data);
                case ServerEventFunctionId.Store__RequestLeaveStore:
                    return new RequestLeaveStoreSEvt(data);
                case ServerEventFunctionId.Store__RequestNextSales:
                    return new RequestNextSalesSEvt(data);
                case ServerEventFunctionId.Store__RequestPrevSales:
                    return new RequestPrevSalesSEvt(data);

                case ServerEventFunctionId.Trade__AcceptTrade:
                    return new AcceptTradeSEvt(data);
                case ServerEventFunctionId.Trade__OfferTradeItem:
                    return new OfferTradeItemSEvt(data);
                case ServerEventFunctionId.Trade__CloseTradeNegotiations:
                    return new EmptySEvt(funcId);
                case ServerEventFunctionId.Trade__OpenTradeNegotiations:
                    return new OpenTradeNegotiationsSEvt(data);
                case ServerEventFunctionId.Trade__RevokeTradeItem:
                    return new RevokeTradeItemSEvt(data);

                case ServerEventFunctionId.Usage__Usage_TryToUseItem:
                    return new TryToUseItemSEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled server event: {funcId}.");
            }
        }
    }
}
