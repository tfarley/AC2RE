using AC2RE.Definitions;
using System;
using System.Collections.Generic;

namespace AC2RE.Server.Database;

internal interface IMapDatabase : IDisposable {

    public List<MapObject> getMapObjectsInLandblock(LandblockId landblockId);
}
