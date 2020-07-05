﻿using AC2E.Protocol;
using System;
using System.IO;

namespace AC2E.PacketTool {

    public class NetBlobRecord {

        public enum MessageErrorType {
            UNDETERMINED,
            NONE,
            PARTIAL_READ,
            INCOMPLETE_BLOB,
            UNHANDLED_OPCODE,
            PARSE_FAILURE,
        }

        public bool isClientToServer;
        public int startPacketNum;
        public float startTimestamp;
        public int endPacketNum;
        public float endTimestamp;
        public NetBlob netBlob;

        private bool attemptedMessageParse;

        private INetMessage _message;
        public INetMessage message {
            get {
                attemptParseMessage();
                return _message;
            }
        }

        private MessageErrorType _messageErrorType;
        public MessageErrorType messageErrorType {
            get {
                attemptParseMessage();
                return _messageErrorType;
            }
        }
        public MessageErrorType messageErrorTypeOptional => _messageErrorType;

        private Exception _messageException;
        public Exception messageException {
            get {
                attemptParseMessage();
                return _messageException;
            }
        }

        private void attemptParseMessage() {
            if (attemptedMessageParse) {
                return;
            }

            attemptedMessageParse = true;

            try {
                if (netBlob.payload != null) {
                    using (BinaryReader data = new BinaryReader(new MemoryStream(netBlob.payload))) {
                        _message = MessageReader.read(data);
                        if (_message != null) {
                            if (data.BaseStream.Position < data.BaseStream.Length) {
                                _messageErrorType = MessageErrorType.PARTIAL_READ;
                                _messageException = new NotImplementedException($"NetBlob was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                            } else {
                                _messageErrorType = MessageErrorType.NONE;
                            }
                        } else {
                            _messageErrorType = MessageErrorType.UNHANDLED_OPCODE;
                            _messageException = new NotImplementedException("Unhandled message opcode.");
                        }
                    }
                } else {
                    _messageErrorType = MessageErrorType.INCOMPLETE_BLOB;
                    _messageException = new Exception("Incomplete NetBlob due to missing fragments.");
                }
            } catch (Exception e) {
                _messageErrorType = MessageErrorType.PARSE_FAILURE;
                _messageException = e;
            }
        }
    }
}
