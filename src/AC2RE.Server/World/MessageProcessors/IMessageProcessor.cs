using AC2RE.Definitions;

namespace AC2RE.Server;

internal interface IMessageProcessor {

    public bool processMessage(ClientConnection client, Player player, INetMessage genericMsg);
}
