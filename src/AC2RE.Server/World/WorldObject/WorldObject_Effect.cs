using AC2RE.Definitions;

namespace AC2RE.Server;

internal partial class WorldObject {

    public readonly EffectRegistry effectRegistry = new() {
        appliedFx = new(),
        effectInfo = new(),
        appliedAppearances = new(),
    };

    public void initEffect() {

    }

    public EffectId addEffect(EffectRecord effectRecord) {
        EffectId effectId = new(++effectRegistry.effectIdCounter);
        effectRecord.effectId = effectId;

        effectRegistry.effectInfo[effectId] = effectRecord;

        if (player != null) {
            world.playerManager.send(player, new ClientAddEffectCEvt {
                effectRecord = effectRecord,
                effectId = effectId,
            });
        }

        return effectId;
    }

    public void removeEffect(EffectId effectId) {
        effectRegistry.effectInfo.Remove(effectId);

        if (player != null) {
            world.playerManager.send(player, new ClientRemoveEffectCEvt {
                effectId = effectId,
            });
        }
    }
}
