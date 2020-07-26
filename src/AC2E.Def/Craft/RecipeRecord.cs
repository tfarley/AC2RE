namespace AC2E.Def {

    public class RecipeRecord : IPackage {

        public PackageType packageType => PackageType.RecipeRecord;

        public uint recipe; // m_recipe
        public double lastUsedTime; // m_ttLastUsed
        public int successes; // m_iSuccesses
        public double lastClientUpdateTime; // m_ttLastClientUpdate
        public int charges; // m_iCharges
        public DataId recipeDid; // m_didRecipe
        public double nextRefreshGameTime; // m_nextRefreshGame
        public int lastSpinnerValue; // m_iLastSpinnerValue
        public int nextRefreshRealTime; // m_nextRefreshReal

        public RecipeRecord() {

        }

        public RecipeRecord(AC2Reader data) {
            recipe = data.ReadUInt32();
            lastUsedTime = data.ReadDouble();
            successes = data.ReadInt32();
            lastClientUpdateTime = data.ReadDouble();
            charges = data.ReadInt32();
            recipeDid = data.ReadDataId();
            nextRefreshGameTime = data.ReadDouble();
            lastSpinnerValue = data.ReadInt32();
            nextRefreshRealTime = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.Write(recipe);
            data.Write(lastUsedTime);
            data.Write(successes);
            data.Write(lastClientUpdateTime);
            data.Write(charges);
            data.Write(recipeDid);
            data.Write(nextRefreshGameTime);
            data.Write(lastSpinnerValue);
            data.Write(nextRefreshRealTime);
        }
    }
}
