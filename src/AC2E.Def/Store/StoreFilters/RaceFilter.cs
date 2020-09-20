﻿namespace AC2E.Def {

    public class RaceFilter : IPackage {

        public PackageType packageType => PackageType.RaceFilter;

        public SpeciesType race; // m_race

        public RaceFilter(AC2Reader data) {
            race = (SpeciesType)data.ReadUInt32();
        }
    }
}