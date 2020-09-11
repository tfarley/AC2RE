﻿using AC2E.Def;
using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class ServerListener {

        public delegate Task ProcessReceiveDelegate(NetInterface netInterface, byte[] rawData, int dataLen, IPEndPoint receiveEndpoint);

        public readonly NetInterface netInterface;
        private bool stopped;

        private readonly byte[] receiveBuffer = new byte[NetPacket.MAX_SIZE];

        public ServerListener(NetInterface netInterface, ProcessReceiveDelegate processReceive) {
            this.netInterface = netInterface;
            runAsync(processReceive);
            Log.Debug($"Initialized server listener with interface {netInterface}.");
        }

        ~ServerListener() {
            if (!stopped) {
                Log.Warning($"Didn't stop server listener with interface {netInterface} before destruction!");
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
                } catch (ObjectDisposedException e) {
                    // Socket closed (stop was called)
                    Log.Debug($"Server listener interface {netInterface} closed.");
                    continue;
                } catch (Exception e) {
                    Log.Error(e, "Bad receive.");
                    continue;
                }

                try {
                    await processReceive(netInterface, receiveBuffer, receivedInfo.ReceivedBytes, (IPEndPoint)receivedInfo.RemoteEndPoint);
                } catch (Exception e) {
                    Log.Error(e, $"Exception when reading packet on interface {netInterface}, discarded.");
                    continue;
                }
            }
        }

        public void stop() {
            stopped = true;
        }
    }
}
