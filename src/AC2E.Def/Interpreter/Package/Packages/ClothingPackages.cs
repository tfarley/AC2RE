using System.Collections.Generic;

namespace AC2E.Def {

    public static class ClothingPackages {

        public static readonly HashSet<PackageType> ARMOR_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.BootsOpalHuman,
            PackageType.BootsOpalLugian,
            PackageType.BootsOpalTumerok,
        };

        public static readonly HashSet<PackageType> CLOTHING_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.BootsHumanStarter,
            PackageType.BootsLugianStarter,
            PackageType.GauntletsHumanStarter,
            PackageType.GlovesLugianStarter,
            PackageType.HumanQuiver,
            PackageType.JerkinTumerokStarter,
            PackageType.KiltLugianStarter,
            PackageType.LeggingsTumerokStarter,
            PackageType.PantsHumanStarter,
            PackageType.RareArmorTemplate,
            PackageType.SashLugianStarter,
            PackageType.ShirtHumanStarter,
            PackageType.Toga,
            PackageType.TogaLugianBarbs,
        };

        public static readonly HashSet<PackageType> HUMAN_ARMOR_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.BootsHumanLeather,
            PackageType.BootsHumanLeatherHard,
            PackageType.BootsHumanPlate,
            PackageType.BootsHumanReinforced,
            PackageType.GlovesHumanLeatherHard,
            PackageType.GlovesHumanPadded,
            PackageType.GlovesHumanPlate,
            PackageType.GlovesHumanReinforced,
            PackageType.GreavesHumanLeatherHard,
            PackageType.GreavesHumanPadded,
            PackageType.GreavesHumanPlate,
            PackageType.GreavesHumanReinforced,
            PackageType.HauberkHumanPlate,
            PackageType.JacketHumanLeatherHard,
            PackageType.JacketHumanPadded,
            PackageType.JacketHumanReinforced,
            PackageType.RobeHuman,

            PackageType.BreastplateHumanDarkfoe,
            PackageType.GauntletsGhostfireHuman,
        };

        public static readonly HashSet<PackageType> LUGIAN_ARMOR_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.CuirassLugianMalok,
            PackageType.GauntletsLugianGuardian,
            PackageType.GauntletsLugianMalok,
            PackageType.GauntletsLugianQuilted,
            PackageType.GauntletsLugianRaider,
            PackageType.GauntletsLugianStudded,
            PackageType.KiltLugianGuardian,
            PackageType.KiltLugianMalok,
            PackageType.KiltLugianQuilted,
            PackageType.KiltLugianRaider,
            PackageType.KiltLugianStudded,
            PackageType.PauldronLugianGuardian,
            PackageType.PauldronLugianQuilted,
            PackageType.PauldronLugianRaider,
            PackageType.PauldronLugianStudded,
            PackageType.RobeLugian,
            PackageType.SandalsLugianGuardian,
            PackageType.SandalsLugianMalok,
            PackageType.SandalsLugianQuilted,
            PackageType.SandalsLugianRaider,
            PackageType.SandalsLugianStudded,

            PackageType.BreastplateLugianDarkfoe,
            PackageType.GauntletsGhostfireLugian,
        };

        public static readonly HashSet<PackageType> RING_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.BronzeRing,
            PackageType.Crafted_Ring,
            PackageType.Crafted_RingBronze,
            PackageType.Crafted_RingIron,
            PackageType.Crafted_RingSilver,
            PackageType.Crafted_RingSteel,
            PackageType.PlainRing,
            PackageType.RingCrystal,
            PackageType.RingEmerald,
            PackageType.RingGolden,
            PackageType.RingOnyx,
            PackageType.RingSilver,
            PackageType.RingSkull,
        };

        public static readonly HashSet<PackageType> TALISMAN_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.TalismanBlessed,
            PackageType.TalismanBone,
            PackageType.TalismanDeruwood,
            PackageType.TalismanGreater,
            PackageType.TalismanHeartstone,
            PackageType.TalismanRunic,
            PackageType.TalismanSilver,
        };

        public static readonly HashSet<PackageType> TUMEROK_ARMOR_TEMPLATE_PACKAGES = new HashSet<PackageType> {
            PackageType.BracersTumerokBone,
            PackageType.BracersTumerokInitiate,
            PackageType.BracersTumerokReed,
            PackageType.BracersTumerokSpirit,
            PackageType.BreastplateTumerokBone,
            PackageType.BreastplateTumerokInitiate,
            PackageType.BreastplateTumerokReed,
            PackageType.BreastplateTumerokSpirit,
            PackageType.LeggingsTumerokBone,
            PackageType.LeggingsTumerokInitiate,
            PackageType.LeggingsTumerokReed,
            PackageType.LeggingsTumerokSpirit,
            PackageType.MoccasinsTumerok,
            PackageType.MoccasinsTumerokBone,
            PackageType.MoccasinsTumerokInitiate,
            PackageType.MoccasinsTumerokReed,
            PackageType.MoccasinsTumerokSpirit,
            PackageType.RobeTumerok,
            PackageType.TailguardTumerokBone,
            PackageType.TailguardTumerokInitiate,
            PackageType.TailguardTumerokReed,
            PackageType.TailguardTumerokSpirit,

            PackageType.BreastplateTumerokDarkfoe,
            PackageType.GauntletsGhostfireTumerok,
        };
    }
}
