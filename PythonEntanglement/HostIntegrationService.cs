using ClassIsland.Core.Abstractions.Services;
using Microsoft.Extensions.Hosting;

namespace PythonEntanglement;

public class HostIntegrationService: IHostedService {
    public static ILessonsService? LessonsService;

    public HostIntegrationService(ILessonsService lessonsService) {
        LessonsService = lessonsService;
    }
    
    public Task StartAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}