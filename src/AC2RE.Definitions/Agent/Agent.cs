namespace AC2RE.Definitions;

public class Agent : Inventory {

    public override PackageType packageType => PackageType.Agent;

    public SkillRepository skillRepository; // m_skill_rep

    public Agent(AC2Reader data) : base(data) {
        data.ReadHO<SkillRepository>(v => skillRepository = v);
    }
}
