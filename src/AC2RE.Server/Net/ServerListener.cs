using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AC2RE.Server;

internal class ServerListener {

    public delegate void ProcessReceiveDelegate(NetInterface netInterface, byte[] rawData, int dataLen, IPEndPoint receiveEndpoint);

    public readonly NetInterface netInterface;
    private bool stopped;

    private readonly byte[] receiveBuffer = new byte[NetPacket.MAX_SIZE];

    private readonly ALogger log;

    public ServerListener(NetInterface netInterface, ProcessReceiveDelegate processReceive) {
        this.netInterface = netInterface;
        log = Logs.NET.forContext("interface", netInterface);
        // Async call without await is intentional, so that IOCP thread pool handles the incoming requests
#pragma warning disable CS4014
        runAsync(processReceive);
#pragma warning restore CS4014
        log.debug("Initialized server listener");
    }

    ~ServerListener() {
        if (!stopped) {
            log.warn("Didn't stop server listener before destruction!");
            stop();
        }
    }

    private async Task runAsync(ProcessReceiveDelegate processReceive) {
        while (true) {
            if (stopped) {
                break;
            }

            Array.Clear(receiveBuffer, 0, receiveBuffer.Length);

            SocketReceiveFromResult receivedInfo;
            try {
                receivedInfo = await netInterface.receiveFromAsync(receiveBuffer);

                if (receivedInfo.ReceivedBytes <= 0) {
                    continue;
                }
            } catch (ObjectDisposedException) {
                // Socket closed (stop was called)
                log.debug("Server listener closed");
                continue;
            } catch (Exception e) {
                log.error(e, "Bad receive");
                continue;
            }

            try {
                processReceive(netInterface, receiveBuffer, receivedInfo.ReceivedBytes, (IPEndPoint)receivedInfo.RemoteEndPoint);
            } catch (Exception e) {
                log.error(e, "Exception when reading packet, discarded");
                continue;
            }
        }
    }

    public void stop() {
        stopped = true;
    }
}
