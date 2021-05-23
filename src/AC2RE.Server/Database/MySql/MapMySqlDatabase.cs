using AC2RE.Definitions;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;

namespace AC2RE.Server.Database {

    internal class MapMySqlDatabase : BaseMySqlDatabase, IMapDatabase {

        protected override string? databaseName => "ac2re_map";

        public List<MapObject> getMapObjectsInLandblock(LandblockId landblockId) {
            List<MapObject> mapObjects = new();
            using (MySqlCommand cmd = new($"SELECT * FROM map_obj WHERE landblockId = {landblockId.id};", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult)) {
                    while (reader.Read()) {
                        Position position = mapPosition(reader);
                        StringInfo? nameStringInfo = !reader.IsDBNull("nameStringId")
                            ? new StringInfo(new(reader.GetUInt32("nameTableDid")), reader.GetUInt32("nameStringId"))
                            : null;
                        mapObjects.Add(new(new(reader.GetUInt64("id")), new(reader.GetUInt32("entityDid")), position, reader.GetFloat("scale"), nameStringInfo));
                    }
                }
            }
            return mapObjects;
        }
    }
}
