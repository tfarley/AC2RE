namespace AC2E.Def {

    public class RandomRecallEffect : Effect {

        public override PackageType packageType => PackageType.RandomRecallEffect;

        public RArray<WPString> destinations; // m_destinationArray

        public RandomRecallEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => destinations = v.to<WPString>());
        }
    }
}
