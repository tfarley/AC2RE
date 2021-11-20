namespace AC2RE.Definitions {

    // Const *_ShortcutType
    public enum ShortcutType : uint {
        Undef = 0, // Undef_ShortcutType

        Skill = 0x40000001, // Skill_ShortcutType
        Item = 0x40000002, // Item_ShortcutType
        Alias = 0x40000003, // Alias_ShortcutType
        Recipe = 0x40000004, // Recipe_ShortcutType

        NewRecipe = 0x40000006, // NewRecipe_ShortcutType
    }
}
