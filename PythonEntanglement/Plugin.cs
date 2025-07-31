using System.IO;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PythonEntanglement.Island;

namespace PythonEntanglement;

[PluginEntrance]
// ReSharper disable once UnusedType.Global
public class Plugin : PluginBase {
    public override void Initialize(HostBuilderContext context,IServiceCollection services) {
        ScriptConfig.DefaultScriptPath = Path.Combine(PluginConfigFolder,"main.py");
        Configuration.SaveDist = Path.Combine(PluginConfigFolder,"config.json");
        Configuration.Instance = Configuration.Load();
        services.AddAction<ScriptConfig,ScriptAction>("pyem.executeScript",
                           "执行Python函数",
                           "\uE829");
        services.AddRule<ScriptConfig,ScriptRule>("pyem.ruleFunc",
                                                  "使用Python判断",
                                                  "\uE829");

        services.AddSettingsPage<SettingsPage>();
        services.AddHostedService<Register>();
        services.AddHostedService<HostIntegrationService>();
    }
}

public class Register : IHostedService {
    public Register(IActionService actionService, IRulesetService rulesetService) {
        actionService.RegisterActionHandler("pyem.executeScript",ScriptAction.Invoke);
        rulesetService.RegisterRuleHandler("pyem.ruleFunc",ScriptRule.Rule);
    }
    
    public Task StartAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}