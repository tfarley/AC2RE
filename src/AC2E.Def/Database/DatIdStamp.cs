namespace AC2E.Def {

    public class DatIdStamp {

        public GUID majorVersion; // _maj_vnum
        public uint minorVersion; // _min_vnum

        public DatIdStamp(AC2Reader data) {
            majorVersion = new GUID(data);
            minorVersion = data.ReadUInt32();
        }
    }
}
