namespace AC2E.Def {

	// Const *_HoldingLocationID
	public enum HoldingLocation : uint {
        INVALID = 0,
		R_HAND = 1,
		L_HAND = 2,
		DRUM = 3,
		ARROW = 4,
		PARTICLE_FRONT = 5,
		PARTICLE_BACK = 6,
		PARTICLE_RIGHT = 7,
		PARTICLE_LEFT = 8,
		PARTICLE_CENTER = 9,

		ARROW_OFFHAND = 16,
		L_HAND2 = 17,
		R_HAND2 = 18,
		L_HAND3 = 19,

		ORIGIN = 0x40000003,
		CAMERA_ORIGIN = 0x40000004,
		CAMERA_TARGET = 0x40000004,
		L_HAND4 = 0x40000006,
		CLOAK = 0x40000007,
	}
}
