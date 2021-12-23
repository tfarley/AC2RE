namespace AC2RE.Definitions;

// Func *::PostCEvt_*
public enum ClientEventFunctionId : uint {
    Undef = 0,

    AI__PetRemove = 0x8200001A, // WM_AI::PostCEvt_AI_PetRemove
    AI__PetAdd = 0x8200001B, // WM_AI::PostCEvt_AI_PetAdd
    AI__UpdatePetName = 0x82000034, // WM_AI::PostCEvt_AI_UpdatePetName
    AI__UpdatePetMode = 0x82000035, // WM_AI::PostCEvt_AI_UpdatePetMode

    Admin__LogEvent = 0x820100EE, // WM_Admin::PostCEvt_LogEvent

    Allegiance__Patron_SwearDone = 0x8203002D, // WM_Allegiance::PostCEvt_Patron_SwearDone
    Allegiance__UpdateAllegianceProfile = 0x8203002E, // WM_Allegiance::PostCEvt_UpdateAllegianceProfile
    Allegiance__RequestVassal = 0x8203002F, // WM_Allegiance::PostCEvt_RequestVassal
    Allegiance__UpdateAllegianceChatDone = 0x82030030, // WM_Allegiance::PostCEvt_UpdateAllegianceChatDone
    Allegiance__Vassal_SwearDone = 0x82030033, // WM_Allegiance::PostCEvt_Vassal_SwearDone
    Allegiance__HandleAllegianceHierarchyForExport = 0x8203006A, // WM_Allegiance::PostCEvt_HandleAllegianceHierarchyForExport

    Combat__UpdateAutoMove = 0x82040015, // WM_Combat::PostCEvt_UpdateAutoMove
    Combat__EndSpecialAttack = 0x82040019, // WM_Combat::PostCEvt_EndSpecialAttack
    Combat__AttackError = 0x8204001A, // WM_Combat::PostCEvt_Combat_AttackError
    Combat__UpdateAttackState = 0x8204001B, // WM_Combat::PostCEvt_UpdateAttackState
    Combat__BasicAttacksFailed = 0x8204001C, // WM_Combat::PostCEvt_Combat_BasicAttacksFailed
    Combat__ToggleSpecialAttack = 0x82040029, // WM_Combat::PostCEvt_ToggleSpecialAttack

    Communication__CHearSay = 0x82050010, // WM_Communication::PostCEvt_CHearSay
    Communication__DisplayStringInfo = 0x82050011, // WM_Communication::PostCEvt_DisplayStringInfo
    Communication__TextEvent = 0x82050012, // WM_Communication::PostCEvt_TextEvent
    Communication__HearCustomEmote = 0x82050013, // WM_Communication::PostCEvt_HearCustomEmote
    Communication__CDoSay = 0x82050016, // WM_Communication::PostCEvt_CDoSay
    Communication__CHearTell = 0x82050017, // WM_Communication::PostCEvt_CHearTell
    Communication__RefreshChannels = 0x8205001E, // WM_Communication::PostCEvt_RefreshChannels
    Communication__CBroadcastStringInfoLocal = 0x8205001F, // WM_Communication::PostCEvt_CBroadcastStringInfoLocal

    Craft__AddIngredient_Done = 0x82060016, // WM_Craft::PostCEvt_AddIngredient_Done
    Craft__RemoveRecipe_Done = 0x82060018, // WM_Craft::PostCEvt_RemoveRecipe_Done
    Craft__CraftRefresh = 0x82060019, // WM_Craft::PostCEvt_CraftRefresh
    Craft__UpdateRecipe_Done = 0x8206001A, // WM_Craft::PostCEvt_UpdateRecipe_Done
    Craft__AddRecipe_Done = 0x8206001B, // WM_Craft::PostCEvt_AddRecipe_Done
    Craft__ExecuteCraft_Done = 0x8206001C, // WM_Craft::PostCEvt_ExecuteCraft_Done
    Craft__UpdateRecipes_Done = 0x82060031, // WM_Craft::PostCEvt_UpdateRecipes_Done
    Craft__UpdateCraftSkills_Done = 0x82060033, // WM_Craft::PostCEvt_UpdateCraftSkills_Done
    Craft__UpdateCraftSkill_Done = 0x82060034, // WM_Craft::PostCEvt_UpdateCraftSkill_Done

    Death__DisplayDeathMsg = 0x8207002B, // WM_Death::PostCEvt_DeathSystem_DisplayDeathMsg
    Death__DisplayKillingMsg = 0x8207002D, // WM_Death::PostCEvt_DeathSystem_DisplayKillingMsg
    Death__AnnounceWhoKilledMe = 0x82070030, // WM_Death::PostCEvt_Death_AnnounceWhoKilledMe
    Death__UpdateDeathState = 0x82070033, // WM_Death::PostCEvt_UpdateDeathState
    Death__ResurrectionRequestedByOther = 0x82070044, // WM_Death::PostCEvt_ResurrectionRequestedByOther

    Effect__ClientRemoveEffect = 0x82080016, // WM_Effect::PostCEvt_Effect_ClientRemoveEffect
    Effect__ClientAddEffect = 0x82080017, // WM_Effect::PostCEvt_Effect_ClientAddEffect
    Effect__PulseMissed = 0x8208002E, // WM_Effect::PostCEvt_Effect_PulseMissed

    Examination__UpdateExaminationProfile = 0x820A0009, // WM_Examination::PostCEvt_UpdateExaminationProfile

    Fellowship__UpdateFellowMaxVigor = 0x820C0021, // WM_Fellowship::PostCEvt_UpdateFellowMaxVigor
    Fellowship__ClearFellow = 0x820C0026, // WM_Fellowship::PostCEvt_ClearFellow
    Fellowship__UpdateFellow = 0x820C0027, // WM_Fellowship::PostCEvt_UpdateFellow
    Fellowship__ClearFellowship = 0x820C0028, // WM_Fellowship::PostCEvt_ClearFellowship
    Fellowship__UpdateFellowship = 0x820C002B, // WM_Fellowship::PostCEvt_UpdateFellowship
    Fellowship__UpdateFellowVigor = 0x820C002D, // WM_Fellowship::PostCEvt_UpdateFellowVigor
    Fellowship__RequestRecruitment = 0x820C002E, // WM_Fellowship::PostCEvt_RequestRecruitment
    Fellowship__UpdateFellowHealth = 0x820C0030, // WM_Fellowship::PostCEvt_UpdateFellowHealth
    Fellowship__ExpireRecruitment = 0x820C0031, // WM_Fellowship::PostCEvt_ExpireRecruitment
    Fellowship__UpdateFellowLevel = 0x820C0032, // WM_Fellowship::PostCEvt_UpdateFellowLevel
    Fellowship__UpdateFellowMaxHealth = 0x820C0033, // WM_Fellowship::PostCEvt_UpdateFellowMaxHealth
    Fellowship__UpdateFellowCellID = 0x820C003C, // WM_Fellowship::PostCEvt_UpdateFellowCellID

    Inventory__EquipItem_Done = 0x820E0051, // WM_Inventory::PostCEvt_EquipItem_Done
    Inventory__ResetContents = 0x820E0053, // WM_Inventory::PostCEvt_Inventory_ResetContents
    Inventory__ReorganizeContents_Done = 0x820E0055, // WM_Inventory::PostCEvt_ReorganizeContents_Done
    Inventory__UnEquipItem_Done = 0x820E0056, // WM_Inventory::PostCEvt_UnEquipItem_Done
    Inventory__DiscreteDestroyContainedItem = 0x820E0058, // WM_Inventory::PostCEvt_DiscreteDestroyContainedItem
    Inventory__DestroyContainedItem = 0x820E005C, // WM_Inventory::PostCEvt_DestroyContainedItem
    Inventory__OpenContainer_Done = 0x820E005D, // WM_Inventory::PostCEvt_OpenContainer_Done
    Inventory__MoveItem_Done = 0x820E005E, // WM_Inventory::PostCEvt_MoveItem_Done
    Inventory__ResetInventory = 0x820E005F, // WM_Inventory::PostCEvt_Inventory_ResetInventory
    Inventory__CloseContainer_Done = 0x820E0060, // WM_Inventory::PostCEvt_CloseContainer_Done
    Inventory__TransmuteAllFromContainer_Done = 0x820E006A, // WM_Inventory::PostCEvt_TransmuteAllFromContainer_Done
    Inventory__TakeAllFromContainer_Done = 0x820E006C, // WM_Inventory::PostCEvt_TakeAllFromContainer_Done
    Inventory__UpdateBackpack = 0x820E006D, // WM_Inventory::PostCEvt_UpdateBackpack

    Money__DragFromMoneyBag_Done = 0x820F0006, // WM_Money::PostCEvt_DragFromMoneyBag_Done
    Money__DragToMoneyBag_Done = 0x820F0009, // WM_Money::PostCEvt_DragToMoneyBag_Done

    PK__Faction_UpdateStatus = 0x82100022, // WM_PK::PostCEvt_Faction_UpdateStatus

    Player__ExitPortalSpace = 0x8212001B, // WM_Player::PostCEvt_ExitPortalSpace
    Player__SetAnimationFrozen = 0x8212001F, // WM_Player::PostCEvt_SetAnimationFrozen
    Player__EnterPortalSpace = 0x82120020, // WM_Player::PostCEvt_EnterPortalSpace
    Player__SetMovementFrozen = 0x82120021, // WM_Player::PostCEvt_SetMovementFrozen
    Player__DisplayMessage = 0x82120024, // WM_Player::PostCEvt_DisplayMessage
    Player__ForceExamineItem = 0x82120026, // WM_Player::PostCEvt_ForceExamineItem
    Player__HandleCharacterSessionStart = 0x82120027, // WM_Player::PostCEvt_HandleCharacterSessionStart
    Player__PortalStorm_Warning = 0x82120029, // WM_Player::PostCEvt_PortalStorm_Warning
    Player__UpdateSelectionInfo = 0x8212002C, // WM_Player::PostCEvt_UpdateSelectionInfo
    Player__ListCurrentlyTrainedSkills_Done = 0x8212005E, // WM_Player::PostCEvt_ListCurrentlyTrainedSkills_Done
    Player__ClearMarkers = 0x82120061, // WM_Player::PostCEvt_ClearMarkers
    Player__ExitPortalScene = 0x82120062, // WM_Player::PostCEvt_ExitPortalScene
    Player__EnterPortalScene = 0x82120064, // WM_Player::PostCEvt_EnterPortalScene
    Player__DisplayMarker = 0x8212006C, // WM_Player::PostCEvt_DisplayMarker

    Quest__PlayScenes = 0x82130021, // WM_Quest::PostCEvt_PlayScenes
    Quest__UpdateStoryQuest_Done = 0x82130023, // WM_Quest::PostCEvt_UpdateStoryQuest_Done
    Quest__UpdateQuest_Done = 0x82130024, // WM_Quest::PostCEvt_UpdateQuest_Done

    Skill__UpdateInfo = 0x8214000D, // WM_Skill::PostCEvt_Skill_UpdateInfo
    Skill__UpdateEverything = 0x8214000E, // WM_Skill::PostCEvt_Skill_UpdateEverything
    Skill__UpdateRepository = 0x82140011, // WM_Skill::PostCEvt_Skill_UpdateRepository
    Skill__RemoveInfo = 0x82140014, // WM_Skill::PostCEvt_Skill_RemoveInfo

    Trade__BeOffered = 0x82150044, // WM_Trade::PostCEvt_Client_Trade_BeOffered
    Trade__BeRegistered = 0x82150046, // WM_Trade::PostCEvt_Client_Trade_BeRegistered
    Trade__BeRevoked = 0x82150048, // WM_Trade::PostCEvt_Client_Trade_BeRevoked
    Trade__BeTampered = 0x8215004C, // WM_Trade::PostCEvt_Client_Trade_BeTampered
    Trade__BeOpened = 0x8215004E, // WM_Trade::PostCEvt_Client_Trade_BeOpened
    Trade__BeDeclined = 0x8215004F, // WM_Trade::PostCEvt_Client_Trade_BeDeclined
    Trade__BeAccepted = 0x82150050, // WM_Trade::PostCEvt_Client_Trade_BeAccepted
    Trade__BeFailed = 0x82150051, // WM_Trade::PostCEvt_Client_Trade_BeFailed
    Trade__BeReset = 0x82150052, // WM_Trade::PostCEvt_Client_Trade_BeReset
    Trade__BeClosed = 0x82150053, // WM_Trade::PostCEvt_Client_Trade_BeClosed
    Trade__BeRefreshed = 0x82150056, // WM_Trade::PostCEvt_Client_Trade_BeRefreshed

    Usage__TryToUseItem_Done = 0x82160021, // WM_Usage::PostCEvt_Usage_TryToUseItem_Done
    Usage__ProcessUsageFailure = 0x82160023, // WM_Usage::PostCEvt_Usage_ProcessUsageFailure
    Usage__ShouldApplyTotem = 0x8216002C, // WM_Usage::PostCEvt_ShouldApplyTotem
    Usage__UseBook = 0x8216002E, // WM_Usage::PostCEvt_Usage_UseBook

    Vendor__ClientReplyTransact = 0x82170021, // WM_Vendor::PostCEvt_Vendor_ClientReplyTransact
    Vendor__ClientReplyInventory = 0x82170022, // WM_Vendor::PostCEvt_Vendor_ClientReplyInventory
    Vendor__ClientCloseShopReply = 0x82170023, // WM_Vendor::PostCEvt_Vendor_ClientCloseShopReply
    Vendor__ClientRequestShopReply = 0x82170025, // WM_Vendor::PostCEvt_Vendor_ClientRequestShopReply

    Store__UpdateSale = 0x9B5E0046, // WM_Store::PostCEvt_Store_UpdateSale
    Store__UpdateStore = 0x9B5E0049, // WM_Store::PostCEvt_Store_UpdateStore
    Store__Request_Done = 0x9B5E004C, // WM_Store::PostCEvt_Store_Request_Done
    Store__LeaveStore = 0x9B5E004D, // WM_Store::PostCEvt_Store_LeaveStore
    Store__LeaveConsignment = 0x9B5E004F, // WM_Store::PostCEvt_Store_LeaveConsignment
    Store__RemoveSaleReminders = 0x9B5E0050, // WM_Store::PostCEvt_Store_RemoveSaleReminders
    Store__EnterCatalog = 0x9B5E0052, // WM_Store::PostCEvt_Store_EnterCatalog
    Store__EnterConsignment = 0x9B5E0053, // WM_Store::PostCEvt_Store_EnterConsignment
    Store__EnterStore = 0x9B5E0056, // WM_Store::PostCEvt_Store_EnterStore
    Store__UpdateConsignment = 0x9B5E0057, // WM_Store::PostCEvt_Store_UpdateConsignment
    Store__InitSaleReminders = 0x9B5E005D, // WM_Store::PostCEvt_Store_InitSaleReminders
    Store__LeaveCatalog = 0x9B5E005E, // WM_Store::PostCEvt_Store_LeaveCatalog
    Store__UpdateSaleReminders = 0x9B5E0060, // WM_Store::PostCEvt_Store_UpdateSaleReminders
    Store__UpdateCatalog = 0x9B5E0062, // WM_Store::PostCEvt_Store_UpdateCatalog
}
