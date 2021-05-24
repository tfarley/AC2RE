using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal class ExaminationMessageProcessor : BaseMessageProcessor {

        public ExaminationMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.Evt_Interp__InterpSEvent_ID: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        if (msg.netEvent.funcId == ServerEventFunctionId.Examination__QueryExaminationProfile) {
                            QueryExaminationProfileSEvt sEvent = (QueryExaminationProfileSEvt)msg.netEvent;
                            if (sEvent.request.dataId != InstanceId.NULL && world.objectManager.tryGet(sEvent.request.dataId, out WorldObject? target) && target.inWorld) {
                                List<ExaminationDataNode> nodes = new();
                                if (target.qualities.ints != null) {
                                    foreach ((IntStat stat, int value) in target.qualities.ints) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.INT,
                                            valInt = value,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.longs != null) {
                                    foreach ((LongIntStat stat, long value) in target.qualities.longs) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.LONG_INT,
                                            valLongInt = value,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.bools != null) {
                                    foreach ((BoolStat stat, bool value) in target.qualities.bools) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BOOL,
                                            valBool = value,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.floats != null) {
                                    foreach ((FloatStat stat, float value) in target.qualities.floats) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.FLOAT,
                                            valFloat = value,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.doubles != null) {
                                    foreach ((TimestampStat stat, double value) in target.qualities.doubles) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.COUNTDOWN,
                                            valTime = value,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.ids != null) {
                                    foreach ((InstanceIdStat stat, InstanceId value) in target.qualities.ids) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(value.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.dids != null) {
                                    foreach ((DataIdStat stat, DataId value) in target.qualities.dids) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(value.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.strings != null) {
                                    foreach ((StringStat stat, string value) in target.qualities.strings) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(value),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                if (target.qualities.stringInfos != null) {
                                    foreach ((StringInfoStat stat, Definitions.StringInfo value) in target.qualities.stringInfos) {
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = new(stat.ToString()),
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.TAB,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.STRING,
                                            valString = value,
                                        });
                                        nodes.Add(new() {
                                            order = (uint)nodes.Count,
                                            type = ExaminationDataNode.DataType.BREAK,
                                        });
                                    }
                                }
                                send(player, new InterpCEventPrivateMsg {
                                    netEvent = new UpdateExaminationProfileCEvt {
                                        profile = new() {
                                            request = sEvent.request,
                                            nodes = nodes,
                                        }
                                    }
                                });
                            } else {
                                send(player, new InterpCEventPrivateMsg {
                                    netEvent = new UpdateExaminationProfileCEvt {
                                        profile = new() {
                                            request = sEvent.request,
                                            nodes = new() {
                                                new() {
                                                    order = 2,
                                                    type = ExaminationDataNode.DataType.INT,
                                                    valInt = 12345,
                                                    appearanceId = 3193660691,
                                                }
                                            }
                                        }
                                    }
                                });
                            }
                        } else {
                            return false;
                        }
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
    }
}
