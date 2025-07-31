using CommunityToolkit.Mvvm.ComponentModel;

namespace PythonEntanglement.Island;

public class ScriptConfig : ObservableObject {
    string _path = DefaultScriptPath;
    string _entryPoint = "main";

    public static string DefaultScriptPath = "";
    
    public string Path {
        get => _path;
        set {
            if (value == _path) return;
            _path = value;
            OnPropertyChanged();
        }
    }

    public string EntryPoint {
        get => _entryPoint;
        set {
            if (value == _entryPoint) return;
            _entryPoint = value;
            OnPropertyChanged();
        }
    }
}