namespace AC2RE.Server.Migration;

internal class LoadMapMigration : SqlMigration {

    protected override string statement => @"USE ac2re_map;

LOAD DATA INFILE './map_obj.csv' INTO TABLE map_obj
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\r\n'
IGNORE 1 LINES
(@dummy, id, entityDid, landblockId, cellId, posX, posY, posZ, rotX, rotY, rotZ, rotW, scale, @nameStringId, @nameTableDid)
SET nameStringId = if(@nameStringId = 'NULL', NULL, @nameStringId),
nameTableDid = if(@nameTableDid = 'NULL', NULL, @nameTableDid);";

}
