using System.Collections.Generic;

namespace AC2E.Def {

    public static class CraftPackages {

        public static readonly HashSet<PackageType> REFINING_RECIPE_PACKAGES = new HashSet<PackageType> {
            PackageType.BlockRefiningRecipe,
            PackageType.BoardRefiningRecipe,
            PackageType.BrickRefiningRecipe,
            PackageType.DramasticRefiningRecipe,
            PackageType.MoonglassRefiningRecipe,
            PackageType.PlasterRefiningRecipe,
            PackageType.RunewoodRefiningRecipe,
            PackageType.SteelRefiningRecipe,
            PackageType.TileRefiningRecipe,
            PackageType.TukaliteRefiningRecipe,
        };
    }
}
