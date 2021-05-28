using AC2RE.Definitions;
using AC2RE.Server.Database;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace AC2RE.Server {

    internal class World {

        public static readonly Position TUTORIAL_START_POS = new() {
            cell = new(0x02, 0x98, 0x01, 0x09),
            frame = new(new(59.060577f, 240.199f, -44.894524f), new(0.70710677f, 0.0f, 0.0f, 0.70710677f)),
        };

        public static readonly Position ARWIC_START_POS = new() {
            cell = new(0x75, 0xB9, 0x00, 0x31),
            frame = new(new(131.13126f, 13.535009f, 127.25996f), Quaternion.Identity),
        };

        private readonly List<IMessageProcessor> MESSAGE_PROCESSORS;

        public readonly IMapDatabase mapDb;
        public readonly IWorldDatabase worldDb;
        public readonly ServerTime serverTime;
        private readonly PacketHandler packetHandler;
        public readonly ContentManager contentManager;

        public readonly PlayerManager playerManager;
        public readonly CharacterManager characterManager;
        public readonly WorldObjectManager objectManager;
        public readonly LandblockManager landblockManager;

        public World(IMapDatabase mapDb, IWorldDatabase worldDb, ServerTime serverTime, PacketHandler packetHandler, ContentManager contentManager) {
            this.mapDb = mapDb;
            this.worldDb = worldDb;
            this.serverTime = serverTime;
            this.packetHandler = packetHandler;
            this.contentManager = contentManager;
            playerManager = new(packetHandler);
            characterManager = new(this);
            objectManager = new(this, contentManager);
            landblockManager = new(this);
            MESSAGE_PROCESSORS = new() {
                new CharacterMessageProcessor(this),
                new CombatMessageProcessor(this),
                new CommunicationMessageProcessor(this),
                new DataMessageProcessor(this),
                new EquipMessageProcessor(this),
                new ExaminationMessageProcessor(this),
                new SkillMessageProcessor(this),
            };
        }

        public void addPlayerIfNecessary(ClientConnection client, Account account) {
            if (!playerManager.exists(client.id)) {
                playerManager.add(client.id, account);
            }
        }

        public bool processMessage(ClientConnection client, INetMessage genericMsg) {
            if (!playerManager.tryGet(client.id, out Player? player)) {
                throw new InvalidDataException($"Received message from client {client} with no player.");
            }

            bool handled = false;
            foreach (IMessageProcessor messageProcessor in MESSAGE_PROCESSORS) {
                handled = messageProcessor.processMessage(client, player, genericMsg);
                if (handled) {
                    break;
                }
            }

            return handled;
        }

        public void tick() {
            packetHandler.processNetBlobs(this);

            landblockManager.update();

            objectManager.broadcastUpdates();
        }

        public void save() {
            WorldSave worldSave = new();
            characterManager.contributeToSave(worldSave);
            objectManager.contributeToSave(worldSave);
            worldDb.save(worldSave);
        }

        public void disconnectAll() {
            playerManager.disconnectAll();
        }
    }
}
