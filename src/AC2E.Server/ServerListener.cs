using AC2E.Def;
using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class ServerListener {

        public delegate Task ProcessReceiveDelegate(NetInterface netInterface, byte[] rawData, int dataLen, IPEndPoint receiveEndpoint);

        public NetInterface netInterface;

        private readonly byte[] receiveBuffer = new byte[NetPacket.MAX_SIZE];

        ~ServerListener() {
            if (netInterface != null) {
                Log.Warning($"Didn't disconnect server listener with interface {netInterface} before destruction!");
                disconnect();
            }
        }

        public async void runAsync(ProcessReceiveDelegate processReceive, int port = 0) {
            if (netInterface != null) {
                disconnect();
            }

            netInterface = port != -1 ? new NetInterface(port) : null;

            Log.Debug($"Initialized server listener with interface {netInterface}.");

            while (true) {
                Array.Clear(receiveBuffer, 0, receiveBuffer.Length);

                SocketReceiveFromResult receivedInfo;
                try {
                    receivedInfo = await netInterface.receiveFromAsync(receiveBuffer);

                    if (receivedInfo.ReceivedBytes <= 0) {
                        continue;
                    }
                } catch (Exception e) {
                    Log.Error(e, "Bad receive.");
                    continue;
                }

                try {
                    await processReceive(netInterface, receiveBuffer, receivedInfo.ReceivedBytes, (IPEndPoint)receivedInfo.RemoteEndPoint);
                } catch (Exception e) {
                    Log.Error(e, "Exception when reading packet, discarded.");
                    continue;
                }
            }
        }

        public void disconnect() {
            if (netInterface != null) {
                netInterface.close();
                netInterface = null;
            }
        }
    }
}
