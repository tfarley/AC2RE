using System;

namespace AC2RE.Definitions {

    public interface IWritable {

        void write(AC2Writer data) {
            throw new NotImplementedException("IWritable implementor must override write().");
        }
    }
}
