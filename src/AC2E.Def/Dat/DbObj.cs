namespace AC2E.Def {

    public abstract class DbObj {

        public DataId did; // m_DID

        public DbObj(AC2Reader data) {
            did = data.ReadDataId();
        }
    }
}
