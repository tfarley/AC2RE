using AC2RE.Definitions;
using System;
using System.IO;

namespace AC2RE.PacketTool {

    internal class NetBlobRecord {

        public bool isClientToServer;
        public int startPacketNum;
        public double startTimestamp;
        public int endPacketNum;
        public double endTimestamp;
        public NetBlob netBlob;

        private bool attemptedMessageParse;

        private INetMessage? _message;
        public INetMessage? message {
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

        private Exception? _messageException;
        public Exception? messageException {
            get {
                attemptParseMessage();
                return _messageException;
            }
        }

        public long parseFailurePos { get; private set; }

        public NetBlobRecord(NetBlob netBlob) {
            this.netBlob = netBlob;
        }

        private void attemptParseMessage() {
            if (attemptedMessageParse) {
                return;
            }

            attemptedMessageParse = true;

            if (netBlob.payload != null) {
                using (AC2Reader data = new(new MemoryStream(netBlob.payload))) {
                    try {
                        MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
                        try {
                            _message = INetMessage.read(opcode, data, isClientToServer);
                            try {
                                if (data.BaseStream.Position < data.BaseStream.Length) {
                                    _messageErrorType = MessageErrorType.PARTIAL_READ;
                                    _messageException = new NotImplementedException($"NetBlob was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                                    parseFailurePos = data.BaseStream.Position;
                                } else {
                                    _messageErrorType = MessageErrorType.NONE;
                                }
                            } catch (NotImplementedException e) {
                                _messageErrorType = MessageErrorType.NOT_IMPLEMENTED;
                                _messageException = e;
                                parseFailurePos = data.BaseStream.Position;
                            }
                        } catch (NotImplementedException e) {
                            _messageErrorType = MessageErrorType.UNHANDLED_OPCODE;
                            _messageException = e;
                            parseFailurePos = data.BaseStream.Position;
                        }
                    } catch (Exception e) {
                        _messageErrorType = MessageErrorType.PARSE_FAILURE;
                        _messageException = e;
                        parseFailurePos = data.BaseStream.Position;
                    }
                }
            } else {
                _messageErrorType = MessageErrorType.INCOMPLETE_BLOB;
                _messageException = new Exception("Incomplete NetBlob due to missing fragments.");
            }
        }
    }
}
