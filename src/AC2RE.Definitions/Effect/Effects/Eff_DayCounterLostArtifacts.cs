namespace AC2RE.Definitions;

public class Eff_DayCounterLostArtifacts : Effect {

    public override PackageType packageType => PackageType.Eff_DayCounterLostArtifacts;

    public bool dayTime; // DayTime

    public Eff_DayCounterLostArtifacts(AC2Reader data) : base(data) {
        dayTime = data.ReadBoolean();
    }
}
