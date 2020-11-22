namespace AC2RE.Definitions {

    public class Eff_RashanDrudgeBane : Effect {

        public override PackageType packageType => PackageType.Eff_RashanDrudgeBane;

        public StringInfo response10; // m_Response10
        public StringInfo response1; // m_Response1
        public StringInfo response2; // m_Response2
        public StringInfo response3; // m_Response3
        public StringInfo response4; // m_Response4
        public StringInfo response5; // m_Response5
        public StringInfo response6; // m_Response6
        public StringInfo response7; // m_Response7
        public StringInfo response8; // m_Response8
        public StringInfo response9; // m_Response9

        public Eff_RashanDrudgeBane(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => response10 = v);
            data.ReadPkg<StringInfo>(v => response1 = v);
            data.ReadPkg<StringInfo>(v => response2 = v);
            data.ReadPkg<StringInfo>(v => response3 = v);
            data.ReadPkg<StringInfo>(v => response4 = v);
            data.ReadPkg<StringInfo>(v => response5 = v);
            data.ReadPkg<StringInfo>(v => response6 = v);
            data.ReadPkg<StringInfo>(v => response7 = v);
            data.ReadPkg<StringInfo>(v => response8 = v);
            data.ReadPkg<StringInfo>(v => response9 = v);
        }
    }
}
