using System;

namespace AC2RE.Definitions;

public interface IServerEvent {

    public ServerEventFunctionId funcId { get; } // _fid

    public static IServerEvent read(ServerEventFunctionId funcId, AC2Reader data) {
        return funcId switch {
            ServerEventFunctionId.Allegiance__AcceptVassal => new AcceptVassalSEvt(data),
            ServerEventFunctionId.Allegiance__AllegianceTreeExport => new AllegianceTreeExportSEvt(data),
            ServerEventFunctionId.Allegiance__AllegianceMemberSearch => new AllegianceMemberSearchSEvt(data),
            ServerEventFunctionId.Allegiance__AllegianceRename => new AllegianceRenameSEvt(data),
            ServerEventFunctionId.Allegiance__AllegianceSetKingdomRestrictions => new AllegianceSetKingdomRestrictionsSEvt(data),
            ServerEventFunctionId.Allegiance__BreakAllegiance => new BreakAllegianceSEvt(data),
            ServerEventFunctionId.Allegiance__QueryAllegianceProfile => new QueryAllegianceProfileSEvt(data),
            ServerEventFunctionId.Allegiance__QueryAllegiance => new EmptySEvt(funcId),
            ServerEventFunctionId.Allegiance__SwearAllegiance => new SwearAllegianceSEvt(data),

            ServerEventFunctionId.Combat__DoAttack => new DoAttackSEvt(data),
            ServerEventFunctionId.Combat__SetNextTarget => new SetNextTargetSEvt(data),
            ServerEventFunctionId.Combat__StartAttack => new StartAttackSEvt(data),
            ServerEventFunctionId.Combat__StopAttacking => new StopAttackingSEvt(data),
            ServerEventFunctionId.Combat__StopAttack => new EmptySEvt(funcId),

            ServerEventFunctionId.Communication__CustomEmote => new CustomEmoteSEvt(data),
            ServerEventFunctionId.Communication__PrefabEmote => new PrefabEmoteSEvt(data),
            ServerEventFunctionId.Communication__Say => new SaySEvt(data),
            ServerEventFunctionId.Communication__Tell => new TellSEvt(data),

            ServerEventFunctionId.Craft__RequestAddIngredient => new RequestAddIngredientSEvt(data),
            ServerEventFunctionId.Craft__RequestExecuteCraft => new RequestExecuteCraftSEvt(data),
            ServerEventFunctionId.Craft__RequestRaiseCraftSkill => new RequestRaiseCraftSkillSEvt(data),

            ServerEventFunctionId.Death__RequestResurrect => new EmptySEvt(funcId),

            ServerEventFunctionId.Examination__QueryExaminationProfile => new QueryExaminationProfileSEvt(data),

            ServerEventFunctionId.Fellowship__AcceptRecruitment => new AcceptRecruitmentSEvt(data),
            ServerEventFunctionId.Fellowship__CreateFellowship => new CreateFellowshipSEvt(data),
            ServerEventFunctionId.Fellowship__DisbandFellowship => new EmptySEvt(funcId),
            ServerEventFunctionId.Fellowship__LeaveFellowship => new EmptySEvt(funcId),
            ServerEventFunctionId.Fellowship__RecruitFellow => new RecruitFellowSEvt(data),

            ServerEventFunctionId.Inventory__CloseContainer => new CloseContainerSEvt(data),
            ServerEventFunctionId.Inventory__DirectiveEquipItem => new DirectiveEquipItemSEvt(data),
            ServerEventFunctionId.Inventory__DirectiveMoveItem => new DirectiveMoveItemSEvt(data),
            ServerEventFunctionId.Inventory__DirectiveReorganizeContents => new DirectiveReorganizeContentsSEvt(data),
            ServerEventFunctionId.Inventory__DirectiveTakeAllFromContainer => new DirectiveTakeAllFromContainerSEvt(data),
            ServerEventFunctionId.Inventory__DirectiveTransmuteAllFromContainer => new DirectiveTransmuteAllFromContainerSEvt(data),
            ServerEventFunctionId.Inventory__DirectiveUnEquipItem => new DirectiveUnequipItemSEvt(data),

            ServerEventFunctionId.Money__DragFromMoneyBag => new DragFromMoneyBagSEvt(data),
            ServerEventFunctionId.Money__DragToMoneyBag => new DragToMoneyBagSEvt(data),

            ServerEventFunctionId.Player__CharacterRename => new CharacterRenameSEvt(data),
            ServerEventFunctionId.Player__Dismount => new EmptySEvt(funcId),
            ServerEventFunctionId.Player__Follow => new FollowSEvt(data),
            ServerEventFunctionId.Player__InscribeItem => new InscribeItemSEvt(data),
            ServerEventFunctionId.Player__ListCurrentlyTrainedSkills => new EmptySEvt(funcId),
            ServerEventFunctionId.Player__RequestPetAttack => new RequestPetAttackSEvt(data),
            ServerEventFunctionId.Player__RequestPetDie => new RequestPetDieSEvt(data),
            ServerEventFunctionId.Player__RequestPetBuff => new EmptySEvt(funcId),
            ServerEventFunctionId.Player__RequestPetHeal => new EmptySEvt(funcId),
            ServerEventFunctionId.Player__RequestPetMode => new RequestPetModeSEvt(data),
            ServerEventFunctionId.Player__RequestPetReturn => new EmptySEvt(funcId),
            ServerEventFunctionId.Player__RequestSetPetName => new RequestSetPetNameSEvt(data),
            ServerEventFunctionId.Player__SetAlias => new SetAliasSEvt(data),
            ServerEventFunctionId.Player__SetSelection => new SetSelectionSEvt(data),
            ServerEventFunctionId.Player__SetShortcut => new SetShortcutSEvt(data),
            ServerEventFunctionId.Player__UploadChatOptions => new UploadChatOptionsSEvt(data),
            ServerEventFunctionId.Player__UploadGameOptions => new UploadGameOptionsSEvt(data),
            ServerEventFunctionId.Player__UploadUISettings => new UploadUISettingsSEvt(data),

            ServerEventFunctionId.Quest__HandlePlayScenesDone => new HandlePlayScenesDoneSEvt(data),
            ServerEventFunctionId.Quest__RequestCancelQuest => new RequestCancelQuestSEvt(data),

            ServerEventFunctionId.Skill__RequestRaiseSkill => new RequestRaiseSkillSEvt(data),
            ServerEventFunctionId.Skill__RequestTrainSkill => new RequestTrainSkillSEvt(data),
            ServerEventFunctionId.Skill__RequestUntrainSkill => new RequestUntrainSkillSEvt(data),
            ServerEventFunctionId.Skill__RequestUntrainSkillCancel => new EmptySEvt(funcId),

            ServerEventFunctionId.Store__PurchaseItemFromStore => new PurchaseItemFromStoreSEvt(data),
            ServerEventFunctionId.Store__RequestCollect => new RequestCollectSEvt(data),
            ServerEventFunctionId.Store__RequestEnterConsignment => new RequestEnterConsignmentSEvt(data),
            ServerEventFunctionId.Store__RequestEnterStore => new RequestEnterStoreSEvt(data),
            ServerEventFunctionId.Store__RequestLeaveCatalog => new RequestLeaveCatalogSEvt(data),
            ServerEventFunctionId.Store__RequestLeaveConsignment => new RequestLeaveConsignmentSEvt(data),
            ServerEventFunctionId.Store__RequestLeaveStore => new RequestLeaveStoreSEvt(data),
            ServerEventFunctionId.Store__RequestNextSales => new RequestNextSalesSEvt(data),
            ServerEventFunctionId.Store__RequestPrevSales => new RequestPrevSalesSEvt(data),
            ServerEventFunctionId.Store__RequestSales => new RequestSalesSEvt(data),
            ServerEventFunctionId.Store__RequestSell => new RequestSellSEvt(data),

            ServerEventFunctionId.Trade__AcceptTrade => new AcceptTradeSEvt(data),
            ServerEventFunctionId.Trade__DeclineTrade => new EmptySEvt(funcId),
            ServerEventFunctionId.Trade__OfferTradeItem => new OfferTradeItemSEvt(data),
            ServerEventFunctionId.Trade__CloseTradeNegotiations => new EmptySEvt(funcId),
            ServerEventFunctionId.Trade__OpenTradeNegotiations => new OpenTradeNegotiationsSEvt(data),
            ServerEventFunctionId.Trade__RevokeTradeItem => new RevokeTradeItemSEvt(data),

            ServerEventFunctionId.Usage__TryToUseItem => new TryToUseItemSEvt(data),

            _ => throw new NotImplementedException($"Unhandled server event: {funcId}."),
        };
    }
}
