namespace AC2RE.Server.Migration {

    internal class CreateInitialSchemaMigration : SqlMigration {

        protected override string statement => @"CREATE DATABASE ac2re_account;
USE ac2re_account;

CREATE TABLE account (
    id CHAR(36) NOT NULL PRIMARY KEY,
    userName VARCHAR(32) NOT NULL,
    password VARCHAR(32) NOT NULL
);
CREATE TABLE account_del LIKE account;

CREATE DATABASE ac2re_map;
USE ac2re_map;

CREATE TABLE map_obj (
    id BIGINT UNSIGNED NOT NULL,
    entityDid INT UNSIGNED NOT NULL,
    landblockId SMALLINT UNSIGNED NOT NULL,
    cellId SMALLINT UNSIGNED NOT NULL,
    posX FLOAT NOT NULL,
    posY FLOAT NOT NULL,
    posZ FLOAT NOT NULL,
    rotX FLOAT NOT NULL,
    rotY FLOAT NOT NULL,
    rotZ FLOAT NOT NULL,
    rotW FLOAT NOT NULL,
    scale FLOAT NOT NULL,
    nameStringId INT UNSIGNED,
    nameTableDid INT UNSIGNED
);

CREATE DATABASE ac2re_world;
USE ac2re_world;

CREATE TABLE characters (
    id CHAR(36) NOT NULL PRIMARY KEY,
    sequence TINYINT UNSIGNED NOT NULL,
    accountId CHAR(36) NOT NULL,
    objectId BIGINT UNSIGNED NOT NULL
);
CREATE TABLE characters_del LIKE characters;

CREATE TABLE id_gen (
    type VARCHAR(64) NOT NULL PRIMARY KEY,
    idCounter BIGINT UNSIGNED NOT NULL
);

CREATE TABLE world_obj (
    id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    entityDid INT UNSIGNED NOT NULL,
    physicsEntityDid INT UNSIGNED NOT NULL
);
CREATE TABLE world_obj_del LIKE world_obj;

CREATE TABLE world_obj_stat_int (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value INT NOT NULL
);
CREATE TABLE world_obj_stat_int_del LIKE world_obj_stat_int;

CREATE TABLE world_obj_stat_long (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value BIGINT NOT NULL
);
CREATE TABLE world_obj_stat_long_del LIKE world_obj_stat_long;

CREATE TABLE world_obj_stat_bool (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value BOOL NOT NULL
);
CREATE TABLE world_obj_stat_bool_del LIKE world_obj_stat_bool;

CREATE TABLE world_obj_stat_float (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value FLOAT NOT NULL
);
CREATE TABLE world_obj_stat_float_del LIKE world_obj_stat_float;

CREATE TABLE world_obj_stat_double (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value DOUBLE NOT NULL
);
CREATE TABLE world_obj_stat_double_del LIKE world_obj_stat_double;

CREATE TABLE world_obj_stat_id (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value BIGINT UNSIGNED NOT NULL
);
CREATE TABLE world_obj_stat_id_del LIKE world_obj_stat_id;

CREATE TABLE world_obj_stat_did (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value INT UNSIGNED NOT NULL
);
CREATE TABLE world_obj_stat_did_del LIKE world_obj_stat_did;

CREATE TABLE world_obj_stat_str (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value VARCHAR(1024) NOT NULL
);
CREATE TABLE world_obj_stat_str_del LIKE world_obj_stat_str;

CREATE TABLE world_obj_stat_strinfo (
    PRIMARY KEY(objectId, stat),
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    stringId INT UNSIGNED,
    tableDid INT UNSIGNED,
    literalValue VARCHAR(1024)
);
CREATE TABLE world_obj_stat_strinfo_del LIKE world_obj_stat_strinfo;

CREATE TABLE world_obj_phys (
    objectId BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    landblockId SMALLINT UNSIGNED NOT NULL,
    cellId SMALLINT UNSIGNED NOT NULL,
    posX FLOAT NOT NULL,
    posY FLOAT NOT NULL,
    posZ FLOAT NOT NULL,
    rotX FLOAT NOT NULL,
    rotY FLOAT NOT NULL,
    rotZ FLOAT NOT NULL,
    rotW FLOAT NOT NULL,
    headingX FLOAT NOT NULL,
    headingZ FLOAT NOT NULL,
    parentId BIGINT UNSIGNED NOT NULL,
    parentInstanceStamp SMALLINT UNSIGNED NOT NULL,
    locationId INT UNSIGNED NOT NULL,
    orientationId INT UNSIGNED NOT NULL,
    instanceStamp SMALLINT UNSIGNED NOT NULL
);
CREATE TABLE world_obj_phys_del LIKE world_obj_phys;

CREATE TABLE world_obj_visual (
    objectId BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    scaleX FLOAT NOT NULL,
    scaleY FLOAT NOT NULL,
    scaleZ FLOAT NOT NULL
);
CREATE TABLE world_obj_visual_del LIKE world_obj_visual;

CREATE TABLE world_obj_apr (
    PRIMARY KEY(objectId, partDid, aprKey),
    objectId BIGINT UNSIGNED NOT NULL,
    partDid INT UNSIGNED NOT NULL,
    aprKey INT UNSIGNED NOT NULL,
    value FLOAT NOT NULL
);
CREATE TABLE world_obj_apr_del LIKE world_obj_apr;";

    }
}
