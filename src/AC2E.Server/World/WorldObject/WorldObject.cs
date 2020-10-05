﻿using AC2E.Def;
using System.Collections.Generic;
using System.Numerics;

namespace AC2E.Server {

    internal class WorldObject {

        public readonly InstanceId id;
        public bool deleted;
        public ushort instanceStamp;

        public bool inWorld;
        public float heading;
        public Vector3 motion;
        public Dictionary<InvLoc, InstanceId> equippedItems = new Dictionary<InvLoc, InstanceId>();
        public List<InstanceId> containedItems = new List<InstanceId>();

        public PhysicsDesc physics;
        public VisualDesc visual;
        public CBaseQualities qualities;

        public WorldObject(InstanceId id) : this(id, new PhysicsDesc(), new VisualDesc(), new CBaseQualities()) {
            qualities.weenieDesc = new WeenieDesc();
        }

        public WorldObject(InstanceId id, PhysicsDesc physics, VisualDesc visual, CBaseQualities qualities) {
            this.id = id;

            this.physics = physics;
            this.visual = visual;
            this.qualities = qualities;
        }

        public InstanceIdWithStamp getInstanceIdWithStamp(ushort otherStamp = 0) {
            return new InstanceIdWithStamp {
                id = id,
                instanceStamp = instanceStamp,
                otherStamp = otherStamp,
            };
        }
    }
}
