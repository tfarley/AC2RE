﻿using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Numerics;

namespace AC2RE.Server {

    internal partial class WorldObject {

        public VisualDesc visual;

        [DatabaseIgnore]
        private bool visualDirty;

        private void initVisual() {
            visual = new();
        }

        [DatabaseIgnore]
        public Vector3 visualScale {
            get => visual.scale;
            set {
                visual.scale = value;
                visualDirty = true;
            }
        }

        [DatabaseIgnore]
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
                    playerManager.sendAllVisible(id, new UpdateVisualDescMsg(getInstanceIdWithStamp(++physics.visualOrderStamp), visual));
                }

                visualDirty = false;
            }
        }

        public void doFx(FxId fxId, float scalar) {
            playerManager.sendAllVisible(id, new DoFxMsg {
                senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
                fxId = fxId,
                scalar = scalar,
            });
        }

        public void stopFx(FxId fxId) {
            playerManager.sendAllVisible(id, new StopFxMsg {
                senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
                fxId = fxId,
            });
        }
    }
}
