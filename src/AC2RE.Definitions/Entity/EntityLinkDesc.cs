namespace AC2RE.Definitions {

    public class EntityLinkDesc : IPackage {

        public NativeType nativeType => NativeType.EntityLinkDesc;

        public string name; // m_name
        public LinkDesc input; // input
        public LinkDesc output; // output
        public PropertyCollection properties; // m_properties

        public EntityLinkDesc(AC2Reader data) {
            // TODO: Untested parsing
            throw new System.Exception();
            name = data.ReadString();
            input = new(data);
            output = new(data);
            properties = new(data);
        }
    }
}
