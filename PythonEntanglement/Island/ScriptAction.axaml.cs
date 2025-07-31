using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ClassIsland.Core.Abstractions.Controls;

namespace PythonEntanglement.Island;

public partial class ScriptAction: ActionSettingsControlBase<ScriptConfig> {
    public ScriptAction() {
        InitializeComponent();
    }

    public static void Invoke(object? rawConfig,string _) {
        ScriptConfig config = (ScriptConfig)rawConfig!;
        if (!File.Exists(config.Path)) {
            string? dir = Path.GetDirectoryName(config.Path);
            if (dir != null & !Directory.Exists(dir)) Directory.CreateDirectory(dir!);
            File.WriteAllText(config.Path,"");
        }

        PythonManager.Run(new PythonManager.PythonExecutionInfo {
            Code = File.ReadAllText(config.Path),
            Interface = new Interface(),
            EntryPoint = config.EntryPoint
        });
    }

    IStorageProvider? StorageProvider { get => TopLevel.GetTopLevel(this)?.StorageProvider; }
    async void SelectFile(object? sender,RoutedEventArgs e) {
        if (StorageProvider == null) return;
        IReadOnlyList<IStorageFile> fileSel = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            Title = "选择脚本",
            FileTypeFilter = [
                new FilePickerFileType("Python脚本"){
                    Patterns = ["*.py"],
                    MimeTypes = ["text/plain"]
                }
            ],
            AllowMultiple = false
        });
        try {
            IStorageFile file = fileSel[0];
            string? path = file.TryGetLocalPath();
            if (path == null) return;
            Settings.Path = path;   
        } catch { 
            //ignored(neutralized)
        }
    }
}