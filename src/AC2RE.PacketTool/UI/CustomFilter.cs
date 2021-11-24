using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace AC2RE.PacketTool.UI {

    internal abstract class CustomFilter {

        public static readonly Dictionary<string, Func<CustomFilter>> FILTERS = new() {
            { nameof(CreatePlayer), () => new CreatePlayer() },
            { nameof(CreateNonzeroInstanceStamp), () => new CreateNonzeroInstanceStamp() },
            { nameof(MoveItemActualSlotMismatch), () => new MoveItemActualSlotMismatch() },
        };

        public virtual GridViewColumn createColumn(string name, double width) {
            GridViewColumn column = new();
            column.Header = name;
            column.DisplayMemberBinding = new Binding($"matchResultValues[{name}]");
            column.Width = width;
            return column;
        }

        public virtual List<GridViewColumn>? resultColumns => null;

        public abstract bool matches(NetBlobRow netBlobRow, Dictionary<string, object> matchResultValues);

        private class CreatePlayer : CustomFilter {

            private static readonly string NAME_COLUMN = "Name";
            private static readonly string SCALE_COLUMN = "Scale";

            public override List<GridViewColumn>? resultColumns => new() {
                createColumn(NAME_COLUMN, 150),
                createColumn(SCALE_COLUMN, 220),
            };

            public override bool matches(NetBlobRow netBlobRow, Dictionary<string, object> matchResultValues) {
                if (netBlobRow.opcode != MessageOpcode.Physics__CreateObject) {
                    return false;
                }

                var msg = (CreateObjectMsg?)netBlobRow.netBlobRecord.message;

                if (msg == null) {
                    return false;
                }

                if (msg.weenieDesc.packageType != PackageType.PlayerAvatar) {
                    return false;
                }

                matchResultValues[NAME_COLUMN] = msg.weenieDesc.name.literalValue;
                matchResultValues[SCALE_COLUMN] = msg.visualDesc.scale;

                return true;
            }
        }

        private class CreateNonzeroInstanceStamp : CustomFilter {

            private static readonly string PACKAGE_TYPE_COLUMN = "Package Type";
            private static readonly string INSTANCE_ID_COLUMN = "Instance Id";
            private static readonly string STAMP_COLUMN = "Stamp";

            public override List<GridViewColumn>? resultColumns => new() {
                createColumn(PACKAGE_TYPE_COLUMN, 150),
                createColumn(INSTANCE_ID_COLUMN, 150),
                createColumn(STAMP_COLUMN, 150),
            };

            public override bool matches(NetBlobRow netBlobRow, Dictionary<string, object> matchResultValues) {
                if (netBlobRow.opcode != MessageOpcode.Physics__CreateObject) {
                    return false;
                }

                var msg = (CreateObjectMsg?)netBlobRow.netBlobRecord.message;

                if (msg == null) {
                    return false;
                }

                if (msg.physicsDesc.instanceStamp == 0) {
                    return false;
                }

                matchResultValues[PACKAGE_TYPE_COLUMN] = msg.weenieDesc.packageType;
                matchResultValues[INSTANCE_ID_COLUMN] = msg.id.id;
                matchResultValues[STAMP_COLUMN] = msg.physicsDesc.instanceStamp;

                return true;
            }
        }

        private class MoveItemActualSlotMismatch : CustomFilter {

            private static readonly string FROM_COLUMN = "From";
            private static readonly string ACTUAL_FROM_COLUMN = "Actual From";
            private static readonly string TARGET_COLUMN = "Target";
            private static readonly string ACTUAL_TARGET_COLUMN = "Actual Target";

            public override List<GridViewColumn>? resultColumns => new() {
                createColumn(FROM_COLUMN, 100),
                createColumn(ACTUAL_FROM_COLUMN, 100),
                createColumn(TARGET_COLUMN, 100),
                createColumn(ACTUAL_TARGET_COLUMN, 100),
            };

            public override bool matches(NetBlobRow netBlobRow, Dictionary<string, object> matchResultValues) {
                if (netBlobRow.clientEvent != ClientEventFunctionId.Inventory__MoveItem_Done) {
                    return false;
                }

                var msg = (InterpCEventPrivateMsg?)netBlobRow.netBlobRecord.message;

                if (msg == null) {
                    return false;
                }

                var cEvent = (MoveItemDoneCEvt)msg.netEvent;

                if (cEvent.moveDesc.fromSlot == cEvent.moveDesc.actualFromSlot && cEvent.moveDesc.targetSlot == cEvent.moveDesc.actualTargetSlot) {
                    return false;
                }

                matchResultValues[FROM_COLUMN] = cEvent.moveDesc.fromSlot;
                matchResultValues[ACTUAL_FROM_COLUMN] = cEvent.moveDesc.actualFromSlot;
                matchResultValues[TARGET_COLUMN] = cEvent.moveDesc.targetSlot;
                matchResultValues[ACTUAL_TARGET_COLUMN] = cEvent.moveDesc.actualTargetSlot;

                return true;
            }
        }
    }
}
