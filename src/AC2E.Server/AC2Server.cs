using Serilog;
using System;
using System.Diagnostics;

namespace AC2E.Server {

    internal class AC2Server {

        public ClientManager clientManager = new ClientManager();

        public float serverTime => (DateTime.Now.Ticks - Process.GetCurrentProcess().StartTime.Ticks) / TimeSpan.TicksPerSecond;

        private PacketHandler packetHandler;
        private ServerListener logonServerListener = new ServerListener();
        private ServerListener gameServerListener = new ServerListener();

        public AC2Server() {
            packetHandler = new PacketHandler(this);
        }

        ~AC2Server() {
            if (logonServerListener != null || gameServerListener != null) {
                Log.Warning($"Didn't disconnect AC2Server before destruction!");
                disconnect();
            }
        }

        public void start(int port = 0) {
            logonServerListener.runAsync(packetHandler.processReceive, port);
            gameServerListener.runAsync(packetHandler.processReceive, logonServerListener.netInterface.port + 1);

            Log.Debug($"Initialized AC2Server.");
        }

        public void disconnect() {
            // TODO: Disconnect and clear all connections
            if (logonServerListener != null) {
                logonServerListener.disconnect();
                logonServerListener = null;
            }
            if (gameServerListener != null) {
                gameServerListener.disconnect();
                gameServerListener = null;
            }
        }

        public async void sendAsync() {
            await clientManager.flushSendAsync(gameServerListener.netInterface, serverTime);
        }
    }
}
