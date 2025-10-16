using ErrorOr;
using er_toget_forsinket_api.Repositories;
using Models.Statistics;

namespace er_toget_forsinket_api.Services;

public class StatisticsService : IStatisticsService
{
    private readonly PostgresContext _database;

    public StatisticsService(PostgresContext database)
    {
        _database = database;
    }


    public async Task<ErrorOr<GetDelayedTrainsResponse>> GetDelayedTrains()
    {
        return await Task.FromResult(new GetDelayedTrainsResponse
        {
            numberOfdelayedTrains = 50,
            timeSinceLastDelayMinutes = 15
        });
    }
}