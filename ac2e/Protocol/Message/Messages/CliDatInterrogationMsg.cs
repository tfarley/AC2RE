using System.IO;

public class CliDatInterrogationMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

    public NetQueue queue => NetQueue.NET_QUEUE_DATABASE;

    public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_EVENT;

    public uint regionId;
    public Language nameRuleLanguage;
    public Language[] supportedLanguages;

    public void write(BinaryWriter data) {
        data.Write(regionId);
        data.Write((uint)nameRuleLanguage);
        data.Write((uint)supportedLanguages.Length);
        foreach (Language supportedLanguages in supportedLanguages) {
            data.Write((uint)supportedLanguages);
        }
    }
}
