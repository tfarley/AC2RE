namespace AC2E.Server {

    internal interface IIdGenerator<T> {

        public T next();
    }
}
