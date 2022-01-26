using System;
using System.Collections.Generic;
using System.Text;

namespace AC2RE.Definitions;

public class GameplayOptionsProfile : IPackage {

    public NativeType nativeType => NativeType.GameplayOptionsProfile;

    // Const - globals
    [Flags]
    public enum ContentFlag : uint {
        NONE = 0,
        ALIAS_TABLE = 1 << 0, // ALIAS_TABLE 0x00000001
        SHORTCUT_ARRAY = 1 << 1, // SHORTCUT_ARRAY 0x00000002
        SHORTCUT_SET = 1 << 2, // SHORTCUT_SET 0x00000004
        SHOW_RANGE_DAMAGE_OTHER = 1 << 3, // SHOW_RANGE_DAMAGE_OTHER 0x00000008
        SAVED_UI_LOCATIONS = 1 << 4, // SAVED_UI_LOCATIONS 0x00000010
        RADAR_MASK = 1 << 5, // RADAR_MASK 0x00000020
        FILTER_HASH = 1 << 6, // FILTER_HASH 0x00000040
        BIT_FIELD = 1 << 7, // BIT_FIELD 0x00000080
        CHAT_FONT_COLORS = 1 << 8, // CHAT_FONT_COLORS 0x00000100
        CHAT_FONT_SIZES = 1 << 9, // CHAT_FONT_SIZES 0x00000200
        CHAT_POPUP_FLAGS = 1 << 10, // CHAT_POPUP_FLAGS 0x00000400
        WINDOW_OPACITIES = 1 << 11, // WINDOW_OPACITIES 0x00000800
        SHORTCUT_HEIGHT = 1 << 12, // 0x00001000
        WINDOW_TO_CHANNEL = 1 << 13, // 0x00002000
    }

    // Enum GameplayOptionsProfile::GameplayOptionsProfile_Flags
    [Flags]
    public enum Flag : uint {
        NONE = 0, // GOP_NONE
        SHOW_SHORTCUT_NUMBERS = 1 << 0, // GOP_SHOW_SHORTCUT_NUMBERS 0x00000001

        SHOW_DAMAGE_TEXT = 1 << 2, // GOP_SHOW_DAMAGE_TEXT 0x00000004
        SHOW_DAMAGE_OTHER = 1 << 3, // GOP_SHOW_DAMAGE_OTHER 0x00000008
        SHOW_RADAR_COORDS = 1 << 4, // GOP_SHOW_RADAR_COORDS 0x00000010
        AUTO_TARGET = 1 << 5, // GOP_AUTO_TARGET 0x00000020
        ACCEPT_ALLEGIENCE = 1 << 6, // GOP_ACCEPT_ALLEGIENCE 0x00000040
        ACCEPT_FELLOWSHIP = 1 << 7, // GOP_ACCEPT_FELLOWSHIP 0x00000080
        ACCEPT_TRADE = 1 << 8, // GOP_ACCEPT_TRADE 0x00000100
        SHOW_OBJECT_NAMES = 1 << 9, // GOP_SHOW_OBJECT_NAMES 0x00000200
        BYPASS_GOLDMELT_CONFIRMATION = 1 << 10, // GOP_BYPASS_GOLDMELT_CONFIRMATION 0x00000400
        SHORTCUT_LOCKED = 1 << 11, // GOP_SHORTCUT_LOCKED 0x00000800
        SHOW_PK_DAMAGE = 1 << 12, // GOP_SHOW_PK_DAMAGE 0x00001000
        SHORTCUT_INSTASCROLL = 1 << 13, // GOP_SHORTCUT_INSTASCROLL 0x00002000
        LATENCY_INDICATOR_GRAPH = 1 << 14, // GOP_LATENCY_INDICATOR_GRAPH 0x00004000

        HAS_BEEN_VERSIONED = 1u << 31, // GOP_HAS_BEEN_VERSIONED 0x80000000
    }

    // Enum GameplayOptionsProfile::GameplayOptionsProfile_Version
    public enum Version : uint {
        UNDEFINED_VERSION, // GOV_UNDEFINED_VERSION
        CHAT_FONT_VERSION, // GOV_CHAT_FONT_VERSION
        WINDOW_OPACITIES_VERSION, // GOV_WINDOW_OPACITIES_VERSION
        BROADCAST_TEXTTYPE_VERSION, // GOV_BROADCAST_TEXTTYPE_VERSION
        BROADCAST_FILTERFIX_VERSION, // GOV_BROADCAST_FILTERFIX_VERSION + GOV_LATEST_VERSION
        SHORTCUT_HEIGHT_VERSION, // GOV_SHORTCUT_HEIGHT_VERSION
        UNK1,
        LATEST_VERSION = UNK1,
    }

    public ContentFlag contentFlags; // contentFlags
    public Dictionary<string, string> aliasTable; // m_aliasTable
    public List<ShortcutInfo> shortcutArray; // m_shortcutArray
    public uint whichShortcutSet; // m_whichShortcutSet
    public float damageTextRangeOther; // m_fDamageTextRangeOther
    public UISaveLocations savedUILocations; // m_savedUILocations
    public uint radarMask; // m_radarMask
    public Dictionary<uint, TextType> filters; // m_filterHash
    public Flag bitfield; // m_bitField
    public Version version; // m_version
    public Dictionary<TextType, uint> chatFontColors; // m_chatFontColors
    public Dictionary<TextType, uint> chatFontSizes; // m_chatFontSizes
    public Dictionary<uint, TextType> windowToChannel; // windowToChannel
    public Dictionary<TextType, bool> chatPopupFlags; // m_chatPopupFlags
    public Dictionary<uint, float> windowOpacities; // m_windowOpacities
    public int shortcutHeight; // m_iShortcutHeight

    public GameplayOptionsProfile() {

    }

    public GameplayOptionsProfile(AC2Reader data) {
        contentFlags = (ContentFlag)data.ReadUInt64();
        if (contentFlags.HasFlag(ContentFlag.ALIAS_TABLE)) {
            aliasTable = data.ReadDictionary(() => data.ReadString(Encoding.Unicode), () => data.ReadString(Encoding.Unicode));
        }
        if (contentFlags.HasFlag(ContentFlag.SHORTCUT_ARRAY)) {
            shortcutArray = data.ReadList(() => new ShortcutInfo(data));
        }
        if (contentFlags.HasFlag(ContentFlag.SHORTCUT_SET)) {
            whichShortcutSet = data.ReadUInt32();
        }
        if (contentFlags.HasFlag(ContentFlag.SHORTCUT_HEIGHT)) {
            shortcutHeight = data.ReadInt32();
        }
        if (contentFlags.HasFlag(ContentFlag.SHOW_RANGE_DAMAGE_OTHER)) {
            damageTextRangeOther = data.ReadSingle();
        }
        if (contentFlags.HasFlag(ContentFlag.SAVED_UI_LOCATIONS)) {
            savedUILocations = new(data);
        }
        if (contentFlags.HasFlag(ContentFlag.RADAR_MASK)) {
            radarMask = data.ReadUInt32();
        }
        if (contentFlags.HasFlag(ContentFlag.FILTER_HASH)) {
            filters = data.ReadDictionary(data.ReadUInt32, () => (TextType)data.ReadUInt32());
        }
        if (contentFlags.HasFlag(ContentFlag.BIT_FIELD)) {
            bitfield = (Flag)data.ReadUInt32();
        }
        version = (Version)data.ReadUInt32();
        if (contentFlags.HasFlag(ContentFlag.CHAT_FONT_COLORS)) {
            chatFontColors = data.ReadDictionary(() => (TextType)data.ReadUInt32(), data.ReadUInt32);
        }
        if (contentFlags.HasFlag(ContentFlag.CHAT_FONT_SIZES)) {
            chatFontSizes = data.ReadDictionary(() => (TextType)data.ReadUInt32(), data.ReadUInt32);
        }
        if (contentFlags.HasFlag(ContentFlag.WINDOW_TO_CHANNEL)) {
            windowToChannel = data.ReadDictionary(data.ReadUInt32, () => (TextType)data.ReadUInt32());
        }
        if (contentFlags.HasFlag(ContentFlag.CHAT_POPUP_FLAGS)) {
            chatPopupFlags = data.ReadDictionary(() => (TextType)data.ReadUInt32(), data.ReadBoolean);
        }
        if (contentFlags.HasFlag(ContentFlag.WINDOW_OPACITIES)) {
            windowOpacities = data.ReadDictionary(data.ReadUInt32, data.ReadSingle);
        }
    }

    public void write(AC2Writer data) {
        data.Write((ulong)contentFlags);
        if (contentFlags.HasFlag(ContentFlag.ALIAS_TABLE)) {
            data.Write(aliasTable, k => data.Write(k, Encoding.Unicode), v => data.Write(v, Encoding.Unicode));
        }
        if (contentFlags.HasFlag(ContentFlag.SHORTCUT_ARRAY)) {
            data.Write(shortcutArray, v => v.write(data));
        }
        if (contentFlags.HasFlag(ContentFlag.SHORTCUT_SET)) {
            data.Write(whichShortcutSet);
        }
        if (contentFlags.HasFlag(ContentFlag.SHORTCUT_HEIGHT)) {
            data.Write(shortcutHeight);
        }
        if (contentFlags.HasFlag(ContentFlag.SHOW_RANGE_DAMAGE_OTHER)) {
            data.Write(damageTextRangeOther);
        }
        if (contentFlags.HasFlag(ContentFlag.SAVED_UI_LOCATIONS)) {
            savedUILocations.write(data);
        }
        if (contentFlags.HasFlag(ContentFlag.RADAR_MASK)) {
            data.Write(radarMask);
        }
        if (contentFlags.HasFlag(ContentFlag.FILTER_HASH)) {
            data.Write(filters, data.Write, v => data.Write((uint)v));
        }
        if (contentFlags.HasFlag(ContentFlag.BIT_FIELD)) {
            data.Write((uint)bitfield);
        }
        data.Write((uint)version);
        if (contentFlags.HasFlag(ContentFlag.CHAT_FONT_COLORS)) {
            data.Write(chatFontColors, k => data.Write((uint)k), data.Write);
        }
        if (contentFlags.HasFlag(ContentFlag.CHAT_FONT_SIZES)) {
            data.Write(chatFontSizes, k => data.Write((uint)k), data.Write);
        }
        if (contentFlags.HasFlag(ContentFlag.WINDOW_TO_CHANNEL)) {
            data.Write(windowToChannel, data.Write, v => data.Write((uint)v));
        }
        if (contentFlags.HasFlag(ContentFlag.CHAT_POPUP_FLAGS)) {
            data.Write(chatPopupFlags, k => data.Write((uint)k), data.Write);
        }
        if (contentFlags.HasFlag(ContentFlag.WINDOW_OPACITIES)) {
            data.Write(windowOpacities, data.Write, data.Write);
        }
    }
}
