using System.Runtime.CompilerServices;
using Python.Runtime;
using PythonEntanglement.Island;

namespace PythonEntanglement;

public static class PythonManager {
    public static readonly List<PythonExecutionInfo> Queue = [];

    public static object? Run(PythonExecutionInfo pei) {
        if (pei.IsWait) {
            pei.Completion ??= new ManualResetEventSlim();
        }
        Queue.Add(pei);
        if (!IsEngineEnabled) Start();
        pei.Completion?.Wait();
        return pei.Result;
    }

    public static bool IsEngineEnabled;

    public static readonly Thread PythonExecutionThread = new Thread(Executor);

    static void Executor() {
        if (!PythonEngine.IsInitialized) {
            Console.WriteLine("正在初始化Python解释器");
            if (Configuration.Instance!.PythonLib != "") {
                string pydll = Configuration.Instance.PythonLib;
                string pyhome = Path.GetDirectoryName(pydll)!;
                Environment.SetEnvironmentVariable("PYTHONHOME",pyhome);
                Environment.SetEnvironmentVariable("PYTHONNET_PYDLL",pydll);   
            } else {
                Console.WriteLine("未定义Python,不更改进程环境变量");
            }
            PythonEngine.Initialize();
        }
        using (Py.GIL()) {
            while (IsEngineEnabled) {
                if (Queue.Count == 0) continue;
                PythonExecutionInfo myinfo = Queue.First();
                using (PyModule scope = Py.CreateScope()) {
                    scope.Set("ciInterface",myinfo.Interface.ToPython());
                    scope.Exec(myinfo.Code);
                    scope.Exec($"result = {myinfo.EntryPoint}(ciInterface)");
                    myinfo.Result = scope.Get<object?>("result");
                    myinfo.Completion?.Set();
                }
                Queue.Remove(myinfo);
            }   
        }
    }
    
    public static void Start() {
        IsEngineEnabled = true;
        PythonExecutionThread.Start();
    }

    public static void Stop() {
        IsEngineEnabled = false;
    }
    
    public class PythonExecutionInfo {
        public required string Code { get; set; }
        public required Interface Interface { get; set; }
        public required string EntryPoint { get; set; }

        public bool IsWait { get; set; } = true;

        public object? Result { get; set; }
        
        public ManualResetEventSlim? Completion { get; set; }
    }
}