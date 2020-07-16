using System;
using System.IO;

namespace AC2E.WLib {

    public interface IServerEvent {

        ServerEventFunctionId funcId { get; } // _fid

        public static IServerEvent read(ServerEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ServerEventFunctionId.Allegiance__QueryAllegianceProfile:
                    return new QueryAllegianceProfileSEvt(data);
                case ServerEventFunctionId.Allegiance__QueryAllegiance:
                    return new QueryAllegianceSEvt();

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

                case ServerEventFunctionId.Examination__QueryExaminationProfile:
                    return new QueryExaminationProfileSEvt(data);

                case ServerEventFunctionId.Fellowship__CreateFellowship:
                    return new CreateFellowshipSEvt(data);
                case ServerEventFunctionId.Fellowship__RecruitFellow:
                    return new RecruitFellowSEvt(data);

                case ServerEventFunctionId.Inventory__CloseContainer:
                    return new CloseContainerSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveEquipItem:
                    return new DirectiveEquipItemSEvt(data);
                case ServerEventFunctionId.Inventory__DirectiveTransmuteAllFromContainer:
                    return new DirectiveTransmuteAllFromContainerSEvt(data);

                case ServerEventFunctionId.Money__DragToMoneyBag:
                    return new DragToMoneyBagSEvt(data);



                case ServerEventFunctionId.Player__Dismount:
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
                case ServerEventFunctionId.Player__SetSelection:
                    return new SetSelectionSEvt(data);
                case ServerEventFunctionId.Player__SetShortcut:
                    return new SetShortcutSEvt(data);
                case ServerEventFunctionId.Player__UploadUISettings:
                    return new UploadUISettingsSEvt(data);

                case ServerEventFunctionId.Skill__RequestRaiseSkill:
                    return new RequestRaiseSkillSEvt(data);
                case ServerEventFunctionId.Skill__RequestTrainSkill:
                    return new RequestTrainSkillSEvt(data);
                case ServerEventFunctionId.Skill__RequestUntrainSkill:
                    return new RequestUntrainSkillSEvt(data);

                case ServerEventFunctionId.Usage__Usage_TryToUseItem:
                    return new TryToUseItemSEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled server event: {funcId}.");
            }
        }
    }
}
