namespace AC2RE.Definitions {

    // Dat file 23000048 + 23000026 / Const - globals
    public enum ModeId : uint {
        Undef = 0,

        peace = 0x40000001, // peace
        dive = 0x40000002, // dive
        riding = 0x40000003, // riding

        sit = 0x41000001, // sit
        open = 0x41000002, // open
        closed = 0x41000003, // closed
        looted = 0x41000004, // looted
        dead = 0x41000005, // dead
        chargen = 0x41000006, // chargen
        chargenMO = 0x41000007, // chargenMO
        avgen = 0x41000008, // avgen
        combat = 0x41000009, // combat
        combat_1h = 0x4100000A, // combat_1h
        combat_1h_shield = 0x4100000B, // combat_1h_shield
        combat_2h = 0x4100000C, // combat_2h
        combat_polearm = 0x4100000D, // combat_polearm
        combat_dualwield = 0x4100000E, // combat_dualwield
        combat_martialarts = 0x4100000F, // combat_martialarts
        combat_missile = 0x41000010, // combat_missile
        combat_magic = 0x41000011, // combat_magic
        combat_monster = 0x41000012, // combat_monster
        combat_flail = 0x41000013, // combat_flail
        combat_turret = 0x41000014, // combat_turret
        combat_boulder = 0x41000015, // combat_boulder
        music_lute = 0x41000016, // music_lute
        music_wind = 0x41000017, // music_wind
        music_drum = 0x41000018, // music_drum
        music_string = 0x41000019, // music_string
        mounted_dead = 0x4100001A, // mounted_dead
    }
}
