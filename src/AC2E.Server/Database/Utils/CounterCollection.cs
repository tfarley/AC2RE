using MongoDB.Driver;

namespace AC2E.Server.Database {

    internal class CounterCollection {

        private readonly string type;
        private readonly IMongoCollection<Counter> counters;

        public CounterCollection(IMongoDatabase database, string type, ulong initialCount = 1) {
            this.type = type;
            counters = setupCounters(database);

            counters.FindOneAndUpdate(
                r => r.type == type,
                Builders<Counter>.Update
                    .SetOnInsert(r => r.type, type)
                    .SetOnInsert(r => r.count, initialCount),
                new() { IsUpsert = true });
        }

        private IMongoCollection<Counter> setupCounters(IMongoDatabase database) {
            IMongoCollection<Counter> counters = database.GetCollection<Counter>("counter");

            return counters;
        }

        public ulong next() {
            Counter counter = counters.FindOneAndUpdate(
                c => c.type == type,
                Builders<Counter>.Update.Inc("count", 1));
            return counter.count;
        }

#pragma warning disable
        private class Counter {

            public string type;
            public ulong count;
        }
#pragma warning enable
    }
}
