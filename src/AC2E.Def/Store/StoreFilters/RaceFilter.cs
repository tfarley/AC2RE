namespace AC2E.Def {

    public class RaceFilter : StoreFilter {

        public override PackageType packageType => PackageType.RaceFilter;

        public SpeciesType race; // m_race

        public RaceFilter(AC2Reader data) : base(data) {
            race = (SpeciesType)data.ReadUInt32();
        }
    }
}
