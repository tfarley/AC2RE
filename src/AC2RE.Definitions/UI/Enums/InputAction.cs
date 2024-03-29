﻿namespace AC2RE.Definitions;

// Dat file 2300002B
public enum InputAction : uint {
    Undef = 0,

    EXAMINATION_PANEL_KEY = 1090519041, // EXAMINATION_PANEL_KEY
    INVENTORY_PANEL_KEY = 1090519042, // INVENTORY_PANEL_KEY
    NAVIGATION_PANEL_KEY = 1090519043, // NAVIGATION_PANEL_KEY
    OPTION_PANEL_KEY = 1090519044, // OPTION_PANEL_KEY
    QUEST_PANEL_KEY = 1090519045, // QUEST_PANEL_KEY
    SKILL_PANEL_KEY = 1090519046, // SKILL_PANEL_KEY
    SOCIAL_PANEL_KEY = 1090519047, // SOCIAL_PANEL_KEY
    TOOLBAR_PANEL_KEY = 1090519048, // TOOLBAR_PANEL_KEY
    STATUSBAR_PANEL_KEY = 1090519049, // STATUSBAR_PANEL_KEY
    RADAR_KEY = 1090519051, // RADAR_KEY
    ALL_UI_KEY = 1090519052, // ALL_UI_KEY
    USE_KEY = 1090519053, // USE_KEY
    LOGOUT_KEY = 1090519054, // LOGOUT_KEY
    SHOW_NAMES_KEY = 1090519055, // SHOW_NAMES_KEY
    START_COMMAND_KEY = 1090519056, // START_COMMAND_KEY
    START_ALIAS_KEY = 1090519057, // START_ALIAS_KEY
    AUTOATTACK_KEY = 1090519059, // AUTOATTACK_KEY
    VISUALIZE_SOUND_KEY = 1090519060, // VISUALIZE_SOUND_KEY
    BENCHMARKING_KEY = 1090519061, // BENCHMARKING_KEY
    DEBUG_HUD_KEY = 1090519062, // DEBUG_HUD_KEY
    SCREENSHOT_KEY = 1090519063, // SCREENSHOT_KEY
    SELECTION_SELF_KEY = 1090519064, // SELECTION_SELF_KEY
    SELECTION_PREVIOUS_CREATURE_KEY = 1090519065, // SELECTION_PREVIOUS_CREATURE_KEY
    SELECTION_NEXT_CREATURE_KEY = 1090519066, // SELECTION_NEXT_CREATURE_KEY
    SELECTION_NEAREST_CREATURE_KEY = 1090519067, // SELECTION_NEAREST_CREATURE_KEY
    SELECTION_PREVIOUS_ITEM_KEY = 1090519068, // SELECTION_PREVIOUS_ITEM_KEY
    SELECTION_NEXT_ITEM_KEY = 1090519069, // SELECTION_NEXT_ITEM_KEY
    SELECTION_NEAREST_ITEM_KEY = 1090519070, // SELECTION_NEAREST_ITEM_KEY
    SELECTION_PREVIOUS_PC_KEY = 1090519071, // SELECTION_PREVIOUS_PC_KEY
    SELECTION_NEXT_PC_KEY = 1090519072, // SELECTION_NEXT_PC_KEY
    SELECTION_NEAREST_PC_KEY = 1090519073, // SELECTION_NEAREST_PC_KEY
    SELECTION_PREVIOUS_FOE_KEY = 1090519074, // SELECTION_PREVIOUS_FOE_KEY
    SELECTION_NEXT_FOE_KEY = 1090519075, // SELECTION_NEXT_FOE_KEY
    SELECTION_NEAREST_FOE_KEY = 1090519076, // SELECTION_NEAREST_FOE_KEY
    SELECTION_PREVIOUS_FELLOW_KEY = 1090519077, // SELECTION_PREVIOUS_FELLOW_KEY
    SELECTION_NEXT_FELLOW_KEY = 1090519078, // SELECTION_NEXT_FELLOW_KEY
    SELECTION_NEAREST_FELLOW_KEY = 1090519079, // SELECTION_NEAREST_FELLOW_KEY
    SELECTION_LAST_KEY = 1090519080, // SELECTION_LAST_KEY
    SELECTION_OFF_KEY = 1090519081, // SELECTION_OFF_KEY
    SELECTION_LAST_ATTACKER_KEY = 1090519082, // SELECTION_LAST_ATTACKER_KEY
    SHORTCUT_1_KEY = 1090519083, // SHORTCUT_1_KEY
    SHORTCUT_2_KEY = 1090519084, // SHORTCUT_2_KEY
    SHORTCUT_3_KEY = 1090519085, // SHORTCUT_3_KEY
    SHORTCUT_4_KEY = 1090519086, // SHORTCUT_4_KEY
    SHORTCUT_5_KEY = 1090519087, // SHORTCUT_5_KEY
    SHORTCUT_6_KEY = 1090519088, // SHORTCUT_6_KEY
    SHORTCUT_7_KEY = 1090519089, // SHORTCUT_7_KEY
    SHORTCUT_8_KEY = 1090519090, // SHORTCUT_8_KEY
    SHORTCUT_9_KEY = 1090519091, // SHORTCUT_9_KEY
    SHORTCUT_0_KEY = 1090519092, // SHORTCUT_0_KEY
    SHORTCUT_SET_1_KEY = 1090519093, // SHORTCUT_SET_1_KEY
    SHORTCUT_SET_2_KEY = 1090519094, // SHORTCUT_SET_2_KEY
    SHORTCUT_SET_3_KEY = 1090519095, // SHORTCUT_SET_3_KEY
    SHORTCUT_SET_4_KEY = 1090519096, // SHORTCUT_SET_4_KEY
    SHORTCUT_SET_5_KEY = 1090519097, // SHORTCUT_SET_5_KEY
    SHORTCUT_SET_6_KEY = 1090519098, // SHORTCUT_SET_6_KEY
    SHORTCUT_SET_7_KEY = 1090519099, // SHORTCUT_SET_7_KEY
    SHORTCUT_SET_8_KEY = 1090519100, // SHORTCUT_SET_8_KEY
    SHORTCUT_SET_9_KEY = 1090519101, // SHORTCUT_SET_9_KEY
    SHORTCUT_SET_0_KEY = 1090519102, // SHORTCUT_SET_0_KEY
    SHORTCUT_SET_NEXT_KEY = 1090519103, // SHORTCUT_SET_NEXT_KEY
    SHORTCUT_SET_PREV_KEY = 1090519104, // SHORTCUT_SET_PREV_KEY
    CHATWINDOW_1_KEY = 1090519105, // CHATWINDOW_1_KEY
    CHATWINDOW_2_KEY = 1090519106, // CHATWINDOW_2_KEY
    CHATWINDOW_3_KEY = 1090519107, // CHATWINDOW_3_KEY
    CHATWINDOW_4_KEY = 1090519108, // CHATWINDOW_4_KEY
    PRINT_ACTIVE_SOUND_KEY = 1090519109, // PRINT_ACTIVE_SOUND_KEY
    GROOVE_LEVEL_UP_KEY = 1090519110, // GROOVE_LEVEL_UP_KEY
    GROOVE_LEVEL_DOWN_KEY = 1090519111, // GROOVE_LEVEL_DOWN_KEY

    REPLY_KEY = 1090519113, // REPLY_KEY

    SELECTION_FELLOW_1_KEY = 1090519123, // SELECTION_FELLOW_1_KEY
    SELECTION_FELLOW_2_KEY = 1090519124, // SELECTION_FELLOW_2_KEY
    SELECTION_FELLOW_3_KEY = 1090519125, // SELECTION_FELLOW_3_KEY
    SELECTION_FELLOW_4_KEY = 1090519126, // SELECTION_FELLOW_4_KEY
    SELECTION_FELLOW_5_KEY = 1090519127, // SELECTION_FELLOW_5_KEY
    SELECTION_FELLOW_6_KEY = 1090519128, // SELECTION_FELLOW_6_KEY
    SELECTION_FELLOW_7_KEY = 1090519129, // SELECTION_FELLOW_7_KEY
    SELECTION_FELLOW_8_KEY = 1090519130, // SELECTION_FELLOW_8_KEY
    SELECTION_FELLOW_9_KEY = 1090519131, // SELECTION_FELLOW_9_KEY
    SELECTION_PREVIOUS_PET_KEY = 1090519132, // SELECTION_PREVIOUS_PET_KEY
    SELECTION_NEXT_PET_KEY = 1090519133, // SELECTION_NEXT_PET_KEY
    SELECTION_NEAREST_PET_KEY = 1090519134, // SELECTION_NEAREST_PET_KEY
    SHORTCUT_B_1_KEY = 1090519135, // SHORTCUT_B_1_KEY
    SHORTCUT_B_2_KEY = 1090519136, // SHORTCUT_B_2_KEY
    SHORTCUT_B_3_KEY = 1090519137, // SHORTCUT_B_3_KEY
    SHORTCUT_B_4_KEY = 1090519138, // SHORTCUT_B_4_KEY
    SHORTCUT_B_5_KEY = 1090519139, // SHORTCUT_B_5_KEY
    SHORTCUT_B_6_KEY = 1090519140, // SHORTCUT_B_6_KEY
    SHORTCUT_B_7_KEY = 1090519141, // SHORTCUT_B_7_KEY
    SHORTCUT_B_8_KEY = 1090519142, // SHORTCUT_B_8_KEY
    SHORTCUT_B_9_KEY = 1090519143, // SHORTCUT_B_9_KEY
    SHORTCUT_B_0_KEY = 1090519144, // SHORTCUT_B_0_KEY

    SELECTION_NEXT_EXAM_TAB_KEY = 1090519146, // SELECTION_NEXT_EXAM_TAB_KEY
    SELECTION_PREV_EXAM_TAB_KEY = 1090519147, // SELECTION_PREV_EXAM_TAB_KEY
}
