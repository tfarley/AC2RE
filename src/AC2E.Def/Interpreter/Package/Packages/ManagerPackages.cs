using System.Collections.Generic;

namespace AC2E.Def {

    public static class ManagerPackages {

        public static readonly HashSet<PackageType> SYSTEM_PACKAGES = new HashSet<PackageType> {
            PackageType.ExperienceSystem,
        };

        public static readonly HashSet<PackageType> WM_PACKAGES = new HashSet<PackageType> {
            PackageType.WM_Admin,
            PackageType.WM_AI,
            PackageType.WM_Allegiance,
            PackageType.WM_Combat,
            PackageType.WM_Communication,
            PackageType.WM_Craft,
            PackageType.WM_Death,
            PackageType.WM_Effect,
            PackageType.WM_Examination,
            PackageType.WM_Fellowship,
            PackageType.WM_Inventory,
            PackageType.WM_Money,
            PackageType.WM_PK,
            PackageType.WM_Player,
            PackageType.WM_Quest,
            PackageType.WM_Skill,
            PackageType.WM_Store,
            PackageType.WM_Trade,
            PackageType.WM_Usage,
            PackageType.WM_Vendor,
        };
    }
}
