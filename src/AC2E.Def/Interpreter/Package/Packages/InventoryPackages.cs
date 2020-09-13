using System.Collections.Generic;

namespace AC2E.Def {

    public static class InventoryPackages {

        public static readonly HashSet<PackageType> CHEST_ADMIN_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.ChestAdminHigh,
            PackageType.ChestAdminLow,
            PackageType.ChestAdminMagicHigh,
            PackageType.ChestAdminMed,
            PackageType.ChestCommonMotes,
            PackageType.ChestElderMotes,
            PackageType.ChestHumanHigh,
            PackageType.ChestHumanLow,
            PackageType.ChestHumanMedHoard,
            PackageType.ChestLugianHigh,
            PackageType.ChestLugianLow,
            PackageType.ChestLugianMed,
            PackageType.ChestTier1,
            PackageType.ChestTier2,
            PackageType.ChestTier3,
            PackageType.ChestTier4,
            PackageType.ChestTier5,
            PackageType.ChestTier6,
            PackageType.ChestTier7,
            PackageType.ChestTier8,
            PackageType.ChestTumerokHigh,
            PackageType.ChestTumerokLow,
            PackageType.ChestTumerokMed,
            PackageType.ChestUncommonMotes,
        };

        public static readonly HashSet<PackageType> CHEST_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.ChestHuman,
            PackageType.ChestLugian,
            PackageType.ChestMedallionPiece,
            PackageType.ChestTreasuryEerukHuman,
            PackageType.ChestTreasuryEerukLugian,
            PackageType.ChestTreasuryEerukTumerok,
            PackageType.ChestTumerok,
            PackageType.ChestTyrant,
            PackageType.DeleteOnCloseChestTemplate,
        };
    }
}
