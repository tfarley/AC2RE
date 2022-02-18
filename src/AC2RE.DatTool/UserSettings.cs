using AC2RE.Definitions;
using AC2RE.Utils;
using System;
using System.IO;

namespace AC2RE.DatTool;

internal static class UserSettings {

    private static readonly string APP_PATH = Path.Join("AC2RE", "DatTool");
    private static readonly string FILE_PATH = "cfg.txt";

    private struct Data {

        public string? portalDatFilePath { get; set; }
        public string? highresDatFilePath { get; set; }
        public string? cell1DatFilePath { get; set; }
        public string? localDatFilePath { get; set; }
        public string? extractSelections { get; set; }
    }

    private static Data data = AppUserDataManager.loadOrDefaultJson<Data>(APP_PATH, FILE_PATH);

    public static string? portalDatFilePath {
        get => data.portalDatFilePath;
        set { data.portalDatFilePath = value; save(); }
    }

    public static string? highresDatFilePath {
        get => data.highresDatFilePath;
        set { data.highresDatFilePath = value; save(); }
    }

    public static string? cell1DatFilePath {
        get => data.cell1DatFilePath;
        set { data.cell1DatFilePath = value; save(); }
    }

    public static string? localDatFilePath {
        get => data.localDatFilePath;
        set { data.localDatFilePath = value; save(); }
    }

    public static DbType[] extractSelections {
        get {
            string[] selectionStrings = data.extractSelections?.Split(',') ?? Array.Empty<string>();
            DbType[] dbTypes = new DbType[selectionStrings.Length];
            for (int i = 0; i < selectionStrings.Length; i++) {
                if (Enum.TryParse(selectionStrings[i], out DbType dbType)) {
                    dbTypes[i] = dbType;
                }
            }
            return dbTypes;
        }
        set {
            string[] selectionStrings = new string[value.Length];
            for (int i = 0; i < value.Length; i++) {
                selectionStrings[i] = value[i].ToString();
            }

            data.extractSelections = string.Join(',', selectionStrings);
            save();
        }
    }

    private static void save() {
        AppUserDataManager.saveJson(data, APP_PATH, FILE_PATH);
    }
}
