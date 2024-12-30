namespace WarfaceAPI.Services;

// BackgroundService реализует интерфейс IHostedSerivce
public class PlayerStatsUpdater(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var trackerService = scope.ServiceProvider.GetRequiredService<TrackerDataService>();
                // Список игроков для трекинга можно получить из базы данных
                var nicknames = new[] { "ЛюблюНорвегию" };
                foreach (var nickname in nicknames)
                {
                    await trackerService.ChangePlayerDataAsync(nickname);
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Интервал 1 час
        }
    }
}