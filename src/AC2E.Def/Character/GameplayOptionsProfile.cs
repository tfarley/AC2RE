using System;
using System.Collections.Generic;
using System.Text;

namespace AC2E.Def {

    public class GameplayOptionsProfile : IPackage {

        [Flags]
        public enum ContentFlag : uint {
            NONE = 0,
            ALIAS_TABLE = 1 << 0, // 0x00000001
            SHORTCUT_ARRAY = 1 << 1, // 0x00000002
            SHORTCUT_SET = 1 << 2, // 0x00000004
            SHOW_RANGE_DAMAGE_OTHER = 1 << 3, // 0x00000008
            SAVED_UI_LOCATIONS = 1 << 4, // 0x00000010
            RADAR_MASK = 1 << 5, // 0x00000020
            FILTER_HASH = 1 << 6, // 0x00000040
            BIT_FIELD = 1 << 7, // 0x00000080
            CHAT_FONT_COLORS = 1 << 8, // 0x00000100
            CHAT_FONT_SIZES = 1 << 9, // 0x00000200
            CHAT_POPUP_FLAGS = 1 << 10, // 0x00000400
            WINDOW_OPACITIES = 1 << 11, // 0x00000800
            UNK1 = 1 << 12, // 0x00001000
            WINDOW_TO_CHANNEL = 1 << 13, // 0x00002000
        }

        // Enum GameplayOptionsProfile::GameplayOptionsProfile_Flags
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            SHOW_SHORTCUT_NUMBERS = 1 << 0, // 0x00000001
            SHOW_DAMAGE_TEXT = 1 << 1, // 0x00000002
            SHOW_DAMAGE_OTHER = 1 << 2, // 0x00000004
            SHOW_RADAR_COORDS = 1 << 3, // 0x00000008
            AUTO_TARGET = 1 << 4, // 0x00000010
            ACCEPT_ALLEGIENCE = 1 << 5, // 0x00000020
            ACCEPT_FELLOWSHIP = 1 << 6, // 0x00000040
            ACCEPT_TRADE = 1 << 7, // 0x00000080
            SHOW_OBJECT_NAMES = 1 << 8, // 0x00000100
            BYPASS_GOLDMELT_CONFIRMATION = 1 << 9, // 0x00000200
            SHORTCUT_LOCKED = 1 << 10, // 0x00000400
            SHOW_PK_DAMAGE = 1 << 11, // 0x00000800
            SHORTCUT_INSTASCROLL = 1 << 12, // 0x00001000
            LATENCY_INDICATOR_GRAPH = 1 << 13, // 0x00002000

            HAS_BEEN_VERSIONED = 1u << 31, // 0x80000000
        }

        // Enum GameplayOptionsProfile::GameplayOptionsProfile_Version
        public enum Version : uint {
            UNDEFINED_VERSION,
            CHAT_FONT_VERSION,
            WINDOW_OPACITIES_VERSION,
            BROADCAST_TEXTTYPE_VERSION,
            BROADCAST_FILTERFIX_VERSION,
            UNK1,
            UNK2,
            LATEST_VERSION = UNK2,
        }

        public NativeType nativeType => NativeType.GAMEPLAYOPTIONSPROFILE;

        public ContentFlag contentFlags; // contentFlags
        public Dictionary<string, string> aliasTable; // m_aliasTable
        public List<ShortcutInfo> shortcutArray; // m_shortcutArray
        public uint whichShortcutSet; // m_whichShortcutSet
        public float damageTextRangeOther; // m_fDamageTextRangeOther
        public UISaveLocations savedUILocations; // m_savedUILocations
        public uint radarMask; // m_radarMask
        public Dictionary<uint, uint> filterDict; // m_filterHash
        public Flag bitfield; // m_bitField
        public Version version; // m_version
        public Dictionary<TextType, uint> chatFontColors; // m_chatFontColors
        public Dictionary<TextType, uint> chatFontSizes; // m_chatFontSizes
        public Dictionary<uint, TextType> windowToChannel; // windowToChannel
        public Dictionary<TextType, bool> chatPopupFlags; // m_chatPopupFlags
        public Dictionary<uint, float> windowOpacities; // m_windowOpacities
        public uint unk1;

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
            if (contentFlags.HasFlag(ContentFlag.UNK1)) {
                unk1 = data.ReadUInt32();
            }
            if (contentFlags.HasFlag(ContentFlag.SHOW_RANGE_DAMAGE_OTHER)) {
                damageTextRangeOther = data.ReadSingle();
            }
            if (contentFlags.HasFlag(ContentFlag.SAVED_UI_LOCATIONS)) {
                savedUILocations = new UISaveLocations(data);
            }
            if (contentFlags.HasFlag(ContentFlag.RADAR_MASK)) {
                radarMask = data.ReadUInt32();
            }
            if (contentFlags.HasFlag(ContentFlag.FILTER_HASH)) {
                filterDict = data.ReadDictionary(data.ReadUInt32, data.ReadUInt32);
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
            if (contentFlags.HasFlag(ContentFlag.UNK1)) {
                data.Write(unk1);
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
                data.Write(filterDict, data.Write, data.Write);
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
}
