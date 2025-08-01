using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text.Json;
using ClassIsland.Core;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Shared.Models.Action;
using Microsoft.Win32;
using Python.Runtime;
using Action = ClassIsland.Shared.Models.Action.Action;

// ReSharper disable InconsistentNaming

namespace PythonEntanglement;

public class Interface {
    public AppBase app = AppBase.Current;
    
    public string code_name { get => AppBase.AppCodeName; }
    public string version { get => AppBase.AppVersion; }

    public ILessonsService? lessonsService = HostIntegrationService.LessonsService;
    
    public IActionService? actionService = HostIntegrationService.ActionService;
    public void showInfo(string msg) {
        //todo: 实现消息弹窗
    }

    public ActionSetManagerClass action = new ActionSetManagerClass();
    
    public class ActionSetManagerClass {
        public void invoke(string json) {
            try {
                ObservableCollection<Action>? obj = JsonSerializer.Deserialize<ObservableCollection<Action>>(json);
                if (obj == null) return;
                ActionSet acts = new ActionSet {
                    Actions = obj
                };
                HostIntegrationService.ActionService!.Invoke(acts);
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                //ignore (temp)
            }
        }
        
        public void sendSignal(string signal) {
            //todo: 修改为一个阳间的实现
            string actions = """
                             [
                                 {
                                     "Id": "classisland.broadcastSignal",
                                     "Settings": {
                                         "SignalName": "
                             """ + signal +
                             """
                             ",
                                         "IsRevert": false,
                                         "IsActive": false
                                     },
                                     "IsActive": false
                                 }
                             ]
                             """;
            Console.WriteLine(actions);
            invoke(actions);
        }
    }
}