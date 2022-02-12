using System.Threading;

namespace AC2RE.Server;

internal class ClientIdGenerator {

    private int idCounter = 0;

    public ClientId next() {
        return new((ushort)Interlocked.Increment(ref idCounter));
    }
}
