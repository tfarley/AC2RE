using System.Threading;

class Program {

    private static readonly AC2Server server = new AC2Server();

    static void Main(string[] args) {
        ALog.setCategoryEnabled(CatTransport.i, true);
        ALog.setCategoryEnabled(CatProto.i, true);

        ALog.info("Hello World!");

        server.start(7777);

        while (true) {
            server.processReceive();
            Thread.Sleep(100);
        }
    }
}
