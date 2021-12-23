namespace AC2RE.Server.Migration;

internal class CreateInitialSchemaMigration : SqlMigration {

    protected override string statement => @"CREATE DATABASE ac2re_account;
USE ac2re_account;

CREATE TABLE account (
    INDEX (userName, password),
    id CHAR(36) NOT NULL PRIMARY KEY,
    deleted BOOL NOT NULL,
    userName VARCHAR(32) NOT NULL UNIQUE,
    password VARCHAR(32) NOT NULL,
    banned BOOL NOT NULL
);

CREATE DATABASE ac2re_map;
USE ac2re_map;

CREATE TABLE map_obj (
    id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
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

CREATE TABLE id_gen (
    type VARCHAR(64) NOT NULL PRIMARY KEY,
    idCounter BIGINT UNSIGNED NOT NULL
);

CREATE TABLE world_obj (
    id BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    entityDid INT UNSIGNED NOT NULL,
    physicsEntityDid INT UNSIGNED NOT NULL
);
CREATE TABLE del_world_obj LIKE world_obj;

CREATE TABLE world_obj_stat_int (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value INT NOT NULL
);
CREATE TABLE del_world_obj_stat_int LIKE world_obj_stat_int;
ALTER TABLE del_world_obj_stat_int ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_long (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value BIGINT NOT NULL
);
CREATE TABLE del_world_obj_stat_long LIKE world_obj_stat_long;
ALTER TABLE del_world_obj_stat_long ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_bool (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value BOOL NOT NULL
);
CREATE TABLE del_world_obj_stat_bool LIKE world_obj_stat_bool;
ALTER TABLE del_world_obj_stat_bool ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_float (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value FLOAT NOT NULL
);
CREATE TABLE del_world_obj_stat_float LIKE world_obj_stat_float;
ALTER TABLE del_world_obj_stat_float ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_double (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value DOUBLE NOT NULL
);
CREATE TABLE del_world_obj_stat_double LIKE world_obj_stat_double;
ALTER TABLE del_world_obj_stat_double ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_id (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value BIGINT UNSIGNED NOT NULL
);
CREATE TABLE del_world_obj_stat_id LIKE world_obj_stat_id;
ALTER TABLE del_world_obj_stat_id ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_did (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value INT UNSIGNED NOT NULL
);
CREATE TABLE del_world_obj_stat_did LIKE world_obj_stat_did;
ALTER TABLE del_world_obj_stat_did ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_str (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    value VARCHAR(1024) NOT NULL
);
CREATE TABLE del_world_obj_stat_str LIKE world_obj_stat_str;
ALTER TABLE del_world_obj_stat_str ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_stat_strinfo (
    PRIMARY KEY(objectId, stat),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    stat INT UNSIGNED NOT NULL,
    stringId INT UNSIGNED,
    tableDid INT UNSIGNED,
    literalValue VARCHAR(1024)
);
CREATE TABLE del_world_obj_stat_strinfo LIKE world_obj_stat_strinfo;
ALTER TABLE del_world_obj_stat_strinfo ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_character (
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    skillCredits INT UNSIGNED NOT NULL,
    heroSkillCredits INT UNSIGNED NOT NULL,
    skillIdUntraining INT UNSIGNED NOT NULL,
    untrainingXp BIGINT UNSIGNED NOT NULL
);
CREATE TABLE del_world_obj_character LIKE world_obj_character;
ALTER TABLE del_world_obj_character ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_phys (
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
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
CREATE TABLE del_world_obj_phys LIKE world_obj_phys;
ALTER TABLE del_world_obj_phys ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_visual (
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    scaleX FLOAT NOT NULL,
    scaleY FLOAT NOT NULL,
    scaleZ FLOAT NOT NULL
);
CREATE TABLE del_world_obj_visual LIKE world_obj_visual;
ALTER TABLE del_world_obj_visual ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_apr (
    PRIMARY KEY(objectId, partDid, aprKey),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    partDid INT UNSIGNED NOT NULL,
    aprKey INT UNSIGNED NOT NULL,
    value FLOAT NOT NULL
);
CREATE TABLE del_world_obj_apr LIKE world_obj_apr;
ALTER TABLE del_world_obj_apr ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE world_obj_skill (
    PRIMARY KEY(objectId, skillId),
    FOREIGN KEY (objectId) REFERENCES world_obj (id) ON DELETE CASCADE,
    objectId BIGINT UNSIGNED NOT NULL,
    skillId INT UNSIGNED NOT NULL,
    flags INT UNSIGNED NOT NULL,
    xpAllocated BIGINT UNSIGNED NOT NULL,
    lastUsedTime DOUBLE NOT NULL
);
CREATE TABLE del_world_obj_skill LIKE world_obj_skill;
ALTER TABLE del_world_obj_skill ADD FOREIGN KEY (objectId) REFERENCES del_world_obj (id) ON DELETE CASCADE;

CREATE TABLE characters (
    FOREIGN KEY (accountId) REFERENCES ac2re_account.account (id),
    FOREIGN KEY (objectId) REFERENCES world_obj (id),
    id CHAR(36) NOT NULL PRIMARY KEY,
    accountId CHAR(36) NOT NULL,
    sequence TINYINT UNSIGNED NOT NULL,
    objectId BIGINT UNSIGNED NOT NULL UNIQUE
);
CREATE TABLE del_characters LIKE characters;
ALTER TABLE del_characters ADD FOREIGN KEY (accountId) REFERENCES ac2re_account.account (id) ON DELETE CASCADE;
ALTER TABLE characters ADD UNIQUE (accountId, sequence);";

}
