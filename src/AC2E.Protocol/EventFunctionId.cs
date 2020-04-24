namespace AC2E.Protocol {

    public enum EventFunctionId : uint {
        StartAttack = 0x82040008,
        ClientRemoveEffect = 0x82080016,
        ClientAddEffect = 0x82080017,
        ExitPortalSpace = 0x8212001B,
        EnterPortalSpace = 0x82120020,
    }
}
