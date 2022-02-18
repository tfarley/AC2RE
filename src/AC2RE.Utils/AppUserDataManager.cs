using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AC2RE.Utils;

public static class AppUserDataManager {

    private static readonly JsonSerializerOptions JSON_OPTIONS = new() {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        WriteIndented = true,
    };

    private static string getFullFilePath(string appPath, string filePath) {
        return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appPath, filePath);
    }

    public static bool tryLoadJson<T>(string appPath, string filePath, [NotNullWhen(true)] out T? data) {
        string fullFilePath = getFullFilePath(appPath, filePath);
        if (File.Exists(fullFilePath)) {
            string jsonData = File.ReadAllText(fullFilePath);
            if (!string.IsNullOrEmpty(jsonData)) {
                data = JsonSerializer.Deserialize<T>(jsonData, JSON_OPTIONS);
                if (data != null) {
                    return true;
                }
            }
        }

        data = default;
        return false;
    }

    public static T loadOrDefaultJson<T>(string appPath, string filePath) where T : new() {
        if (!tryLoadJson(appPath, filePath, out T? data)) {
            data = new();
        }
        return data;
    }

    public static void saveJson<T>(T data, string appPath, string filePath) {
        string fullFilePath = getFullFilePath(appPath, filePath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath)!);
        File.WriteAllText(fullFilePath, JsonSerializer.Serialize(data, JSON_OPTIONS));
    }
}
