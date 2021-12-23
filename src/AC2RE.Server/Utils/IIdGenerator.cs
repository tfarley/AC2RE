namespace AC2RE.Server;

internal interface IIdGenerator<T> {

    public T next();
}
