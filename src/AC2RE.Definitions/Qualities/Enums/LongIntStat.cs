namespace AC2RE.Definitions {

    // From RepositoryRepository::GetRawLongInt and RepositoryRepository::SetLongInt calls in wlib
    public enum LongIntStat : uint {
        UNDEF = 0,

        TOTALXP = 300,
        AVAILABLEXP = 301,
        DEATHXP = 312,
        XPTORAISEVITAE = 316,
        ALLEGIANCE_XPPOOL = 501,
        ALLEGIANCE_XPINHERITED = 503,
        TOTALCRAFTXP = 1000,
        AVAILABLECRAFTXP = 1001,
    }
}
