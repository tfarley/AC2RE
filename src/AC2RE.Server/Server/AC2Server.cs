using AC2RE.Server.Database;
using AC2RE.Server.Migration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AC2RE.Server;

internal class AC2Server {

    private static readonly double TICK_DELTA_TIME = 1.0 / 20.0;
    private static readonly double MAX_DELTA_TIME = TICK_DELTA_TIME * 3.0;

    private readonly ServerTime time = new(TICK_DELTA_TIME, MAX_DELTA_TIME);

    private bool active;

    private IMigrationDatabase? migrationDb;
    private IAccountDatabase? accountDb;
    private IMapDatabase? mapDb;
    private IWorldDatabase? worldDb;

    private MigrationManager? migrationManager;

    private AccountManager? accountManager;
    private ClientManager? clientManager;
    private ContentManager? contentManager;
    private PlayerManager? playerManager;

    private World? world;

    private PacketReceiveManager? packetReceiveManager;

    private NetInterface? logonNetInterface;
    private NetInterface? gameNetInterface;
    private readonly List<ServerListener> serverListeners = new();

    [MethodImpl(MethodImplOptions.Synchronized)]
    ~AC2Server() {
        if (active) {
            Logs.STATUS.warn("Didn't stop AC2Server before destruction!");
            stop();
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void start(int port = 0) {
        if (active) {
            stop();
        }

        time.restart();

        Logs.STATUS.info("Connecting to database...");
        while (true) {
            try {
                BaseMySqlDatabase.createConnection().Dispose();
                break;
            } catch (Exception e) {
                Logs.STATUS.error(e, "Failed to make initial database connection");
            }
        }

        Logs.STATUS.info("Running migrations...");
        MigrationManager.bootstrap();
        migrationDb = new MigrationMySqlDatabase();
        migrationManager = new(migrationDb);
        migrationManager.runMigrations();

        Logs.STATUS.info("Initializing databases...");
        accountDb = new AccountMySqlDatabase();
        mapDb = new MapMySqlDatabase();
        worldDb = new WorldMySqlDatabase();

        Logs.STATUS.info("Initializing account manager...");
        accountManager = new(accountDb);

        Logs.STATUS.info("Initializing client manager...");
        clientManager = new();

        Logs.STATUS.info("Initializing content manager...");
        contentManager = new();

        Logs.STATUS.info("Initializing player manager...");
        playerManager = new(clientManager);

        Logs.STATUS.info("Initializing world...");
        world = new(mapDb, worldDb, time, contentManager, playerManager);

        Logs.STATUS.info("Initializing packet receive manager...");
        packetReceiveManager = new(accountManager, clientManager, time, world);

        Logs.STATUS.info("Initializing network...");
        logonNetInterface = new(port);
        gameNetInterface = new(logonNetInterface.port + 1);
        serverListeners.Add(new(logonNetInterface, packetReceiveManager.processReceive));
        serverListeners.Add(new(gameNetInterface, packetReceiveManager.processReceive));

        Logs.STATUS.info("Server initialization complete.");

        active = true;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void stop() {
        if (!active) {
            return;
        }

        world!.save();

        world!.disconnectAll();

        clientManager!.processClients(client => client.flushSend(gameNetInterface!, time.time, time.elapsedTime));

        foreach (ServerListener serverListener in serverListeners) {
            serverListener.stop();
        }
        serverListeners.Clear();

        logonNetInterface!.close();
        gameNetInterface!.close();

        contentManager!.Dispose();

        migrationManager = null;

        accountManager = null;
        clientManager = null;
        contentManager = null;
        playerManager = null;

        world = null;

        packetReceiveManager = null;

        migrationDb!.Dispose();
        accountDb!.Dispose();
        mapDb!.Dispose();
        worldDb!.Dispose();

        migrationDb = null;
        accountDb = null;
        mapDb = null;
        worldDb = null;

        active = false;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void tick() {
        if (!active) {
            return;
        }

        time.beginFrame();

        while (time.tryTick()) {
            packetReceiveManager!.processNetBlobs();

            world!.tick();

            clientManager!.processClients(client => client.flushSend(gameNetInterface!, time.time, time.elapsedTime));
        }
    }
}
