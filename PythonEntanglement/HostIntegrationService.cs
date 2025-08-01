using ClassIsland.Core.Abstractions.Services;
using Microsoft.Extensions.Hosting;

namespace PythonEntanglement;

public class HostIntegrationService: IHostedService {
    public static ILessonsService? LessonsService;
    public static IActionService? ActionService;
    public HostIntegrationService(ILessonsService lessonsService, IActionService actionService) {
        LessonsService = lessonsService;
        ActionService = actionService;
    }
    
    public Task StartAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}