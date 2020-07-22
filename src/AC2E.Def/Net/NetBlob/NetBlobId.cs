﻿using System;

namespace AC2E.Def {

    public struct NetBlobId {

        [Flags]
        public enum Flag : ulong {
            NONE = 0,

            OUT_OF_WORLD = 0x2000000000000000,
            CELL = 0x4000000000000000,
            EPHEMERAL = 0x8000000000000000,
        }

        private static readonly ulong ORDERING_TYPE_MASK = 0x1F00000000000000;
        private static readonly ulong ORDERING_STAMP_MASK = 0x0000FFFF00000000;
        private static readonly ulong SEQUENCE_ID_MASK = 0x00FF0000FFFFFFFF;

        public ulong id;

        public NetBlobId(ulong id) {
            this.id = id;
        }

        public NetBlobId(Flag flags, byte orderingType, ushort orderingStamp, ulong sequenceId) {
            id = (ulong)flags | (((ulong)orderingType << 56) & ORDERING_TYPE_MASK) | ((ulong)orderingStamp) << 32 | (sequenceId & SEQUENCE_ID_MASK);
        }

        public bool isEphemeral {
            get => getFlag(Flag.EPHEMERAL);
            set => setFlag(Flag.EPHEMERAL, value);
        }

        public bool isCell {
            get => getFlag(Flag.CELL);
            set => setFlag(Flag.CELL, value);
        }

        public bool isOutOfWorld {
            get => getFlag(Flag.OUT_OF_WORLD);
            set => setFlag(Flag.OUT_OF_WORLD, value);
        }

        public byte orderingType {
            get => (byte)((id & ORDERING_TYPE_MASK) >> 56);
            set => id = (id & ~ORDERING_TYPE_MASK) | (((ulong)value << 56) & ORDERING_TYPE_MASK);
        }

        public ushort orderingStamp {
            get => (ushort)((id & ORDERING_STAMP_MASK) >> 32);
            set => id = (id & ~ORDERING_STAMP_MASK) | (((ulong)value << 32) & ORDERING_STAMP_MASK);
        }

        public ulong sequenceId {
            get => id & SEQUENCE_ID_MASK;
            set => id = (id & ~SEQUENCE_ID_MASK) | (value & SEQUENCE_ID_MASK);
        }

        private bool getFlag(Flag flag) {
            return (id & (ulong)flag) != 0;
        }

        private void setFlag(Flag flag, bool set) {
            if (set) {
                id |= (ulong)flag;
            } else {
                id &= ~(ulong)flag;
            }
        }
    }
}