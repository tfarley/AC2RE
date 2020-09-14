namespace AC2E.Def {

    public class Eff_Emp_Me_Templar_SigilOfValor : Effect {

        public override PackageType packageType => PackageType.Eff_Emp_Me_Templar_SigilOfValor;

        public DataId empowermentLevel1Effect; // m_effEmpowermentLevel1
        public DataId empowermentLevel2Effect; // m_effEmpowermentLevel2
        public DataId empowermentLevel3Effect; // m_effEmpowermentLevel3
        public DataId valorThresholdTableDid; // m_didValorThresholdTable

        public Eff_Emp_Me_Templar_SigilOfValor(AC2Reader data) : base(data) {
            empowermentLevel1Effect = data.ReadDataId();
            empowermentLevel2Effect = data.ReadDataId();
            empowermentLevel3Effect = data.ReadDataId();
            valorThresholdTableDid = data.ReadDataId();
        }
    }
}
