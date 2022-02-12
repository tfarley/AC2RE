namespace AC2RE.Definitions;

public class TaggedIterationList {

    // CAllIterationList::PTaggedIterationList
    public uint datName; // idDatFile
    public DatFileType datType; // idDatFile
    public CMostlyConsecutiveIntSet iterationList; // List

    public TaggedIterationList(AC2Reader data) {
        datName = data.ReadUInt32();
        datType = data.ReadEnum<DatFileType>();
        iterationList = new(data);
    }
}
