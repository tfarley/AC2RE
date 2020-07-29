namespace AC2E.Def {

    public class DiskFileInfo {

        public uint magic; // magic_
        public uint blockSize; // iBlockSize_
        public uint fileSize; // fileSize_
        public uint dataSetLm; // data_set_lm
        public uint dataSubsetLm; // data_subset_lm
        public uint firstFreeBlock; // firstFree_
        public uint finalFreeBlock; // finalFree_
        public uint numFreeBlocks; // iFreeBlocks_
        public uint treeRootOffset; // btreeRoot_
        public uint youngLruLm; // young_lru_lm
        public uint oldLruLm; // old_lru_lm
        public bool useLruFm; // use_lru_fm
        public uint masterMapId; // master_map_id_m
        public uint engPackVersion; // eng_pack_vnum
        public uint gamePackVersion; // game_pack_vnum
        public DatIdStamp idVersion; // id_vnum

        public DiskFileInfo(AC2Reader data) {
            magic = data.ReadUInt32();
            blockSize = data.ReadUInt32();
            fileSize = data.ReadUInt32();
            dataSetLm = data.ReadUInt32();
            dataSubsetLm = data.ReadUInt32();
            firstFreeBlock = data.ReadUInt32();
            finalFreeBlock = data.ReadUInt32();
            numFreeBlocks = data.ReadUInt32();
            treeRootOffset = data.ReadUInt32();
            youngLruLm = data.ReadUInt32();
            oldLruLm = data.ReadUInt32();
            useLruFm = data.ReadBoolean();
            masterMapId = data.ReadUInt32();
            engPackVersion = data.ReadUInt32();
            gamePackVersion = data.ReadUInt32();
            idVersion = new DatIdStamp(data);
        }
    }
}
