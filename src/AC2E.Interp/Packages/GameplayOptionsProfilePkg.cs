using AC2E.Def.Enums;
using AC2E.Def.Extensions;
using AC2E.Interp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class GameplayOptionsProfilePkg : IPackage {

        [Flags]
        public enum ContentFlag : uint {
            NONE = 0,
            ALIAS_TABLE = 1 << 0,
            SHORTCUT_ARRAY = 1 << 1,
            SHORTCUT_SET = 1 << 2,
            SHOW_RANGE_DAMAGE_OTHER = 1 << 3,
            SAVED_UI_LOCATIONS = 1 << 4,
            RADAR_MASK = 1 << 5,
            FILTER_HASH = 1 << 6,
            BIT_FIELD = 1 << 7,
            CHAT_FONT_COLORS = 1 << 8,
            CHAT_FONT_SIZES = 1 << 9,
            CHAT_POPUP_FLAGS = 1 << 10,
            WINDOW_OPACITIES = 1 << 11,
            UNK1 = 1 << 12,
            WINDOW_TO_CHANNEL = 1 << 13,
        }

        [Flags]
        public enum Flag : uint {
            NONE = 0,
            SHOW_SHORTCUT_NUMBERS = 1 << 0,
            SHOW_DAMAGE_TEXT = 1 << 1,
            SHOW_DAMAGE_OTHER = 1 << 2,
            SHOW_RADAR_COORDS = 1 << 3,
            AUTO_TARGET = 1 << 4,
            ACCEPT_ALLEGIENCE = 1 << 5,
            ACCEPT_FELLOWSHIP = 1 << 6,
            ACCEPT_TRADE = 1 << 7,
            SHOW_OBJECT_NAMES = 1 << 8,
            BYPASS_GOLDMELT_CONFIRMATION = 1 << 9,
            SHORTCUT_LOCKED = 1 << 10,
            SHOW_PK_DAMAGE = 1 << 11,
            SHORTCUT_INSTASCROLL = 1 << 12,
            LATENCY_INDICATOR_GRAPH = 1 << 13,
            HAS_BEEN_VERSIONED = 1u << 31,
        }

        [Flags]
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
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAP_OBJECT);

        public uint id { get; set; }

        public ContentFlag contentFlags;
        public Dictionary<string, string> m_aliasTable;
        public List<ShortcutInfoPkg> m_shortcutArray;
        public uint m_whichShortcutSet;
        public float m_fDamageTextRangeOther;
        public UISaveLocationsPkg m_savedUILocations;
        public uint m_radarMask;
        public Dictionary<uint, uint> m_filterHash;
        public Flag m_bitField;
        public Version m_version;
        public Dictionary<TextType, uint> m_chatFontColors;
        public Dictionary<TextType, uint> m_chatFontSizes;
        public Dictionary<uint, TextType> windowToChannel;
        public Dictionary<TextType, bool> m_chatPopupFlags;
        public Dictionary<uint, float> m_windowOpacities;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write((uint)contentFlags);
            data.Write((uint)0);
            if (contentFlags.HasFlag(ContentFlag.ALIAS_TABLE)) {
                data.Write(m_aliasTable, data.Write, data.Write);
            }
            if (contentFlags.HasFlag(ContentFlag.SHORTCUT_ARRAY)) {
                data.Write(m_shortcutArray, v => v.write(data, references));
            }
            if (contentFlags.HasFlag(ContentFlag.SHORTCUT_SET)) {
                data.Write(m_whichShortcutSet);
            }
            if (contentFlags.HasFlag(ContentFlag.SHOW_RANGE_DAMAGE_OTHER)) {
                data.Write(m_fDamageTextRangeOther);
            }
            if (contentFlags.HasFlag(ContentFlag.SAVED_UI_LOCATIONS)) {
                m_savedUILocations.write(data, references);
            }
            if (contentFlags.HasFlag(ContentFlag.RADAR_MASK)) {
                data.Write(m_radarMask);
            }
            if (contentFlags.HasFlag(ContentFlag.FILTER_HASH)) {
                data.Write(m_filterHash, data.Write, data.Write);
            }
            if (contentFlags.HasFlag(ContentFlag.BIT_FIELD)) {
                data.Write((uint)m_bitField);
            }
            data.Write((uint)m_version);
            if (contentFlags.HasFlag(ContentFlag.CHAT_FONT_COLORS)) {
                data.Write(m_chatFontColors, k => data.Write((uint)k), data.Write);
            }
            if (contentFlags.HasFlag(ContentFlag.CHAT_FONT_SIZES)) {
                data.Write(m_chatFontSizes, k => data.Write((uint)k), data.Write);
            }
            if (contentFlags.HasFlag(ContentFlag.WINDOW_TO_CHANNEL)) {
                data.Write(windowToChannel, data.Write, v => data.Write((uint)v));
            }
            if (contentFlags.HasFlag(ContentFlag.CHAT_POPUP_FLAGS)) {
                data.Write(m_chatPopupFlags, k => data.Write((uint)k), v => data.Write(v ? (uint)1 : (uint)0));
            }
            if (contentFlags.HasFlag(ContentFlag.WINDOW_OPACITIES)) {
                data.Write(m_windowOpacities, data.Write, data.Write);
            }
            if (contentFlags.HasFlag(ContentFlag.UNK1)) {
                throw new Exception();
            }
        }
    }
}
