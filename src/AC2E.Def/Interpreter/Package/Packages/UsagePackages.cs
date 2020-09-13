using System.Collections.Generic;

namespace AC2E.Def {

    public static class UsagePackages {

        public static readonly HashSet<PackageType> USAGE_ACTION_PACKAGES = new HashSet<PackageType> {
            PackageType.ActivatorUsageAction,
            PackageType.AllegianceHallBindingStoneUsageAction,
            PackageType.BallUsageAction,
            PackageType.BookUsageAction,
            PackageType.ButcheryToolUsageAction,
            PackageType.CoinUsageAction,
            PackageType.ContainerUsageAction,
            PackageType.DoNothingUsageAction,
            PackageType.DoorUsageAction,
            PackageType.EquippableUsageAction,
            PackageType.ExamineUsageAction,
            PackageType.InscriptionControlledPortalUsageAction,
            PackageType.ItemInteractionUsageAction,
            PackageType.KeyUsageAction,
            PackageType.LifestoneUsageAction,
            PackageType.MineUsageAction,
            PackageType.MonsterUsageAction,
            PackageType.NPCUsageAction,
            PackageType.PKRankBoardUsageAction,
            PackageType.PortalDoorUsageAction,
            PackageType.PortalUsageAction,
            PackageType.PotionUsageAction,
            PackageType.SaddleUsageAction,
            PackageType.TotemUsageAction,
            PackageType.VendorUsageAction,
        };
    }
}
