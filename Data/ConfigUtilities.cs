using Model.Config;
using System.IO;
using System.Text.Json;

namespace Data;

public static class ConfigUtilities {
    private static readonly JsonSerializerOptions _JSON_SERIALIZER_OPTIONS_ = new() {
        WriteIndented = true
    };
    private static readonly string _CONFIG_PATH_ = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");


    public static AppConfig LoadConfig() {
        if(File.Exists(_CONFIG_PATH_)) {
            string jsonConfig = File.ReadAllText(_CONFIG_PATH_);
            try {
                AppConfig? config = JsonSerializer.Deserialize<AppConfig>(jsonConfig);
                if(config is not null) { return config; }
            }
            catch {

            }
        }
        AppConfig appConfig = new AppConfig();
        SaveConfig(appConfig);
        return appConfig;
    }

    public static void SaveConfig(AppConfig appConfig) {
        string configJson =
            JsonSerializer.Serialize(appConfig, _JSON_SERIALIZER_OPTIONS_);
        File.WriteAllText(_CONFIG_PATH_, configJson);
    }
}

