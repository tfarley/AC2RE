﻿namespace AC2RE.Definitions {

    public class GameEventEffect : Effect {

        public override PackageType packageType => PackageType.GameEventEffect;

        public uint gameEvent; // m_gameEvent
        public bool gameEventState; // m_gameEventState

        public GameEventEffect(AC2Reader data) : base(data) {
            gameEvent = data.ReadUInt32();
            gameEventState = data.ReadBoolean();
        }
    }
}