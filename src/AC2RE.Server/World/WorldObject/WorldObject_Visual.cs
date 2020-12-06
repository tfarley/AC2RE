using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Numerics;

namespace AC2RE.Server {

    internal partial class WorldObject {

        [DbPersist]
        public VisualDesc visual;

        private bool visualDirty;

        private void initVisual() {
            visual = new();
        }

        public Vector3 visualScale {
            get => visual.scale;
            set {
                visual.scale = value;
                visualDirty = true;
            }
        }

        public PartGroupDataDesc globalAppearanceModifiers {
            get => visual.globalAppearanceModifiers;
            set {
                visual.globalAppearanceModifiers = value;
                visualDirty = true;
            }
        }

        public void broadcastVisualUpdate() {
            if (visualDirty) {
                if (inWorld) {
                    world.playerManager.sendAllVisible(id, new UpdateVisualDescMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), visual));
                }

                visualDirty = false;
            }
        }

        public void doFx(FxId fxId, float scalar) {
            world.playerManager.sendAllVisible(id, new DoFxMsg {
                senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
                fxId = fxId,
                scalar = scalar,
            });
        }

        public void stopFx(FxId fxId) {
            world.playerManager.sendAllVisible(id, new StopFxMsg {
                senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
                fxId = fxId,
            });
        }
    }
}
