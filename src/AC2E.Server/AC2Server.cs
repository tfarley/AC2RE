using Serilog;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AC2E.Server {

    internal class AC2Server {

        private static readonly double TICK_DELTA_TIME = 1.0 / 20.0;
        private static readonly double MAX_DELTA_TIME = TICK_DELTA_TIME * 3.0;

        public AccountManager accountManager = new AccountManager();
        public ClientManager clientManager = new ClientManager();

        public ServerTime time = new ServerTime(TICK_DELTA_TIME, MAX_DELTA_TIME);
        public World world;

        private bool active;
        private PacketHandler packetHandler;
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

            packetHandler = new PacketHandler(accountManager, clientManager, time);

            world = new World(time, packetHandler);

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

            // TODO: Disconnect and clear all connections

            foreach (ServerListener serverListener in serverListeners) {
                serverListener.stop();
            }
            serverListeners.Clear();

            logonNetInterface.close();
            gameNetInterface.close();

            packetHandler = null;

            world = null;

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
