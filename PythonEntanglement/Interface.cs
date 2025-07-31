using ClassIsland.Core;
using ClassIsland.Core.Abstractions.Services;
using Microsoft.Win32;
using Python.Runtime;

// ReSharper disable InconsistentNaming

namespace PythonEntanglement;

public class Interface {
    public AppBase app = AppBase.Current;
    
    public string code_name { get => AppBase.AppCodeName; }
    public string ver { get => AppBase.AppVersion; }

    public ILessonsService? lessonsService = HostIntegrationService.LessonsService;

    public void showInfo(string msg) {
        
    }
}