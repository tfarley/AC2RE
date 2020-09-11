using AC2E.Def;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AC2E.Server {

    internal class AC2Server {

        public ClientManager clientManager = new ClientManager();

        public float serverTime => (DateTime.Now.Ticks - Process.GetCurrentProcess().StartTime.Ticks) / TimeSpan.TicksPerSecond;

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

            packetHandler = new PacketHandler(this);

            logonNetInterface = new NetInterface(port);
            gameNetInterface = new NetInterface(logonNetInterface.port + 1);
            serverListeners.Add(new ServerListener(logonNetInterface, packetHandler.processReceiveAsync));
            serverListeners.Add(new ServerListener(gameNetInterface, packetHandler.processReceiveAsync));

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

            active = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void tick() {
            if (!active) {
                return;
            }

            lock (clientManager) {
                foreach (ClientConnection client in clientManager.clients) {
                    lock (client) {
                        while (client.incomingCompleteBlobs.TryDequeue(out NetBlob blob)) {
                            packetHandler.processNetBlob(client, blob);
                        }
                        client.flushSendAsync(gameNetInterface, serverTime);
                    }
                }
            }
        }
    }
}
