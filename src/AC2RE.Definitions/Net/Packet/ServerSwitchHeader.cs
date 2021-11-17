﻿namespace AC2RE.Definitions {

    public class ServerSwitchHeader {

        // CServerSwitchStruct
        public uint seq; // dwSeqNo
        public ServerSwitchType switchType; // Type

        public ServerSwitchHeader() {

        }

        public ServerSwitchHeader(AC2Reader data) {
            seq = data.ReadUInt32();
            switchType = (ServerSwitchType)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(seq);
            data.Write((uint)switchType);
        }
    }
}
