using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace AC2RE.PacketTool.UI {

    internal abstract class CustomFilter {

        public static readonly Dictionary<string, Func<CustomFilter>> FILTERS = new() {
            { "CreatePlayer", () => new CreatePlayerFilter() }
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

        private class CreatePlayerFilter : CustomFilter {

            private static readonly string NAME_COLUMN = "Name";
            private static readonly string SCALE_COLUMN = "Scale";

            public override List<GridViewColumn>? resultColumns => new() {
                createColumn(NAME_COLUMN, 150),
                createColumn(SCALE_COLUMN, 220),
            };

            public override bool matches(NetBlobRow netBlobRow, Dictionary<string, object> matchResultValues) {
                if (netBlobRow.opcode != MessageOpcode.Evt_Physics__CreateObject_ID) {
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
    }
}
