using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PythonEntanglement.Island;

[HidePageTitle]
[SettingsPageInfo("pyem.master","PyEm","\uE829","\uE828")]
public partial class SettingsPage : SettingsPageBase {
    public Configuration Settings { get; set; }
    
    public SettingsPage() {
        Settings = Configuration.Instance!;
        InitializeComponent();
    }
    
    IStorageProvider? StorageProvider { get => TopLevel.GetTopLevel(this)?.StorageProvider; }
    async void SelectFile(object? sender,RoutedEventArgs e) {
        if (StorageProvider == null) return;
        //todo: 添加Linux支持
        IReadOnlyList<IStorageFile> fileSel = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            Title = "选择脚本",
            FileTypeFilter = [
                new FilePickerFileType("python*.dll"){
                    Patterns = ["*.dll"]
                }
            ],
            AllowMultiple = false
        });
        try {
            IStorageFile file = fileSel[0];
            string? path = file.TryGetLocalPath();
            if (path == null) return;
            Settings.PythonLib = path;   
        } catch { 
            //ignored(neutralized)
        }
    }
}

public class Configuration: ObservableObject {
    public static Configuration? Instance;
    public Configuration() {
        PropertyChanged += Save;
    }
    public static Configuration Load() {
        if (File.Exists(SaveDist)) return JsonSerializer.Deserialize<Configuration>(File.ReadAllText(SaveDist))!;
        Configuration nCfg = new Configuration();
        nCfg.Save();
        return nCfg;
    }
    void Save(object? sender,PropertyChangedEventArgs e) {
        Save();
    }
    public void Save() {
        File.WriteAllText(SaveDist,JsonSerializer.Serialize(this,new JsonSerializerOptions {
            WriteIndented = true, 
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        }));
    }

    public static string SaveDist = "";
    
    string _pythonLib = "";
    public string PythonLib {
        get => _pythonLib;
        set {
            if (value == _pythonLib) return;
            _pythonLib = value;
            OnPropertyChanged();
        }
    }
}