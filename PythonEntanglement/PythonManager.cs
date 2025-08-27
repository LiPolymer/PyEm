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
                Console.WriteLine("来活力");
                PythonExecutionInfo myinfo = Queue.First();
                using (PyModule scope = Py.CreateScope()) {
                    Console.WriteLine("创建域");
                    scope.Set("ciInterface",myinfo.Interface.ToPython());
                    List<string> codeStack = [];
                    //todo:直接在此注入dummy.py
                    foreach (string line in myinfo.Code.Split('\n')) {
                        if (line.Contains("from dummy import Interface")) continue;
                        if (line.Contains(":Interface")) {
                            codeStack.Add(line.Replace(":Interface",""));
                        } else if (line.Contains(": Interface")) {
                            codeStack.Add(line.Replace(": Interface",""));
                        } else {
                            codeStack.Add(line);
                        }
                    }
                    Console.WriteLine("处理完成");
                    scope.Exec(string.Join("\n",codeStack));
                    Console.WriteLine("注入完成");
                    scope.Exec($"result = {myinfo.EntryPoint}(ciInterface)");
                    Console.WriteLine("执行完成");
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