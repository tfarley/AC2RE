using System;

namespace AC2E.Def {

    public interface IWritable {

        void write(AC2Writer data) {
            throw new NotImplementedException("IWritable implementor must override write().");
        }
    }
}
