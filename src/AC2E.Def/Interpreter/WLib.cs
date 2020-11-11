﻿namespace AC2E.Def {

    public class WLib {

        public DataId did; // m_DID
        public ByteStream byteStream; // m_bstream

        public WLib(AC2Reader data) {
            did = data.ReadDataId();
            byteStream = new(data);
        }
    }
}
