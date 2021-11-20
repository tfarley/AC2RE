namespace AC2RE.Definitions {

    // Enum LandDefs::DirectionEnum
    public enum Direction : uint {
        Center, // No_Direction + Center_Direction + IN_VIEWER_BLOCK
        North, // North_Direction + NORTH_OF_VIEWER
        South, // South_Direction + SOUTH_OF_VIEWER
        East, // East_Direction + EAST_OF_VIEWER
        West, // West_Direction + WEST_OF_VIEWER
        Northwest, // Northwest_Direction + NORTHWEST_OF_VIEWER
        Southwest, // Southwest_Direction + SOUTHWEST_OF_VIEWER
        Northeast, // Northeast_Direction + NORTHEAST_OF_VIEWER
        Southeast, // Southeast_Direction + SOUTHEAST_OF_VIEWER
        UNKNOWN, // UNKNOWN + Num_Directions
    }
}
