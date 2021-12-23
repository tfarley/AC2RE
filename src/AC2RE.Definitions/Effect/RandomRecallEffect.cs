using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RandomRecallEffect : Effect {

    public override PackageType packageType => PackageType.RandomRecallEffect;

    public List<WPString> destinations; // m_destinationArray

    public RandomRecallEffect(AC2Reader data) : base(data) {
        data.ReadPkg<RArray>(v => destinations = v.to<WPString>());
    }
}
