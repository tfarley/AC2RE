namespace AC2RE.Server.Migration;

internal interface IMigration {

    public bool optional { get; }

    public void execute();
}
