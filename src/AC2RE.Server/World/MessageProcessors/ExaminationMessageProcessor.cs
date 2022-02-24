using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server;

internal class ExaminationMessageProcessor : BaseMessageProcessor {

    public ExaminationMessageProcessor(World world) : base(world) {

    }

    public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
        switch (genericMsg.opcode) {
            case MessageOpcode.Interp__InterpSEvent: {
                    InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                    if (msg.netEvent.funcId == ServerEventFunctionId.Examination__QueryExaminationProfile) {
                        QueryExaminationProfileSEvt sEvent = (QueryExaminationProfileSEvt)msg.netEvent;
                        if (world.objectManager.tryGet(sEvent.request.dataId, out WorldObject? target)) {
                            List<ExaminationDataNode> nodes = new();
                            if (target.qualities.ints != null) {
                                foreach ((IntStat stat, int value) in target.qualities.ints) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Int,
                                        valInt = value,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.longs != null) {
                                foreach ((LongIntStat stat, long value) in target.qualities.longs) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.LongInt,
                                        valLongInt = value,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.bools != null) {
                                foreach ((BoolStat stat, bool value) in target.qualities.bools) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Bool,
                                        valBool = value,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.floats != null) {
                                foreach ((FloatStat stat, float value) in target.qualities.floats) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Float,
                                        valFloat = value,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.doubles != null) {
                                foreach ((TimestampStat stat, double value) in target.qualities.doubles) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Countdown,
                                        valTime = value,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.ids != null) {
                                foreach ((InstanceIdStat stat, InstanceId value) in target.qualities.ids) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(value.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.dids != null) {
                                foreach ((DataIdStat stat, DataId value) in target.qualities.dids) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(value.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.strings != null) {
                                foreach ((StringStat stat, string value) in target.qualities.strings) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(value),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            if (target.qualities.stringInfos != null) {
                                foreach ((StringInfoStat stat, Definitions.StringInfo value) in target.qualities.stringInfos) {
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = new(stat.ToString()),
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Tab,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.String,
                                        valString = value,
                                    });
                                    nodes.Add(new() {
                                        order = (uint)nodes.Count,
                                        type = ExaminationDataNode.DataType.Break,
                                    });
                                }
                            }
                            send(player, new UpdateExaminationProfileCEvt {
                                profile = new() {
                                    request = sEvent.request,
                                    nodes = nodes,
                                }
                            });
                        } else {
                            send(player, new UpdateExaminationProfileCEvt {
                                profile = new() {
                                    request = sEvent.request,
                                    nodes = new() {
                                        new() {
                                            order = 2,
                                            type = ExaminationDataNode.DataType.Int,
                                            valInt = 12345,
                                            appearanceId = 3193660691,
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
