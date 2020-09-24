using AC2E.Server.Database;
using Serilog;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AC2E.Server {

    internal class AC2Server {

        private static readonly string MONGODB_CONNECTION_ENDPOINT = "mongodb://localhost:27017?replicaSet=rs0";

        private static readonly double TICK_DELTA_TIME = 1.0 / 20.0;
        private static readonly double MAX_DELTA_TIME = TICK_DELTA_TIME * 3.0;

        private ServerTime time = new ServerTime(TICK_DELTA_TIME, MAX_DELTA_TIME);

        private bool active;

        private AccountDatabase accountDb;
        private WorldDatabase worldDb;

        private AccountManager accountManager;
        private ClientManager clientManager;
        private ContentManager contentManager;

        private PacketHandler packetHandler;

        private World world;

        private NetInterface logonNetInterface;
        private NetInterface gameNetInterface;
        private List<ServerListener> serverListeners = new List<ServerListener>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        ~AC2Server() {
            if (active) {
                Log.Warning($"Didn't stop AC2Server before destruction!");
                stop();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void start(int port = 0) {
            if (active) {
                stop();
            }

            time.restart();

            accountDb = new AccountDatabase(MONGODB_CONNECTION_ENDPOINT);
            worldDb = new WorldDatabase(MONGODB_CONNECTION_ENDPOINT);

            accountManager = new AccountManager(accountDb);
            clientManager = new ClientManager();
            contentManager = new ContentManager();

            packetHandler = new PacketHandler(accountManager, clientManager, time);

            world = new World(worldDb, time, packetHandler, contentManager);

            logonNetInterface = new NetInterface(port);
            gameNetInterface = new NetInterface(logonNetInterface.port + 1);
            serverListeners.Add(new ServerListener(logonNetInterface, packetHandler.processReceive));
            serverListeners.Add(new ServerListener(gameNetInterface, packetHandler.processReceive));

            Log.Debug($"Initialized AC2Server.");

            active = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void stop() {
            if (!active) {
                return;
            }

            world.save();

            world.disconnectAll();

            lock (clientManager) {
                foreach (ClientConnection client in clientManager.clients) {
                    client.flushSend(gameNetInterface, time.time, time.elapsedTime);
                }
            }

            // TODO: Disconnect and clear all connections

            foreach (ServerListener serverListener in serverListeners) {
                serverListener.stop();
            }
            serverListeners.Clear();

            logonNetInterface.close();
            gameNetInterface.close();

            world = null;

            packetHandler = null;

            accountManager = null;
            clientManager = null;
            contentManager.Dispose();
            contentManager = null;

            accountDb = null;
            worldDb = null;

            active = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void tick() {
            if (!active) {
                return;
            }

            time.beginFrame();

            while (time.tryTick()) {
                world.tick();

                lock (clientManager) {
                    foreach (ClientConnection client in clientManager.clients) {
                        client.flushSend(gameNetInterface, time.time, time.elapsedTime);
                    }
                }
            }
        }
    }
}
