using ErrorOr;
using er_toget_forsinket_api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Models.Statistics;

namespace er_toget_forsinket_api.Services;

public class StatisticsService : IStatisticsService
{
    private readonly PostgresContext _database;
    private readonly IMemoryCache _cache;

    // how long to cache results (e.g. 1 minute)
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(1);

    public StatisticsService(PostgresContext database, IMemoryCache cache)
    {
        _database = database;
        _cache = cache;
    }

    public async Task<ErrorOr<GetDelayedTrainsResponse>> GetDelayedTrains()
    {
        // cache key for this query
        const string cacheKey = "DelayedTrainsStats";

        if (_cache.TryGetValue(cacheKey, out GetDelayedTrainsResponse cached))
        {
            return cached; // return cached data
        }
        var result = await _database.Database
            .SqlQueryRaw<DelayedTrainsResult>(@"
        SELECT 
            COUNT(DISTINCT asp.stop_point_ref) AS NumberOfAffectedStops, 
            COUNT(DISTINCT asp.dated_vehicle_journey_ref) AS NumberOfDelayedTrains
        FROM train_delays.situation_exchange se
        LEFT JOIN train_delays.affected_stop_point asp 
            ON se.""Id"" = asp.""SituationExchangeId""
        WHERE se.end_time::timestamptz > CURRENT_TIMESTAMP
          AND se.report_type = 'incident';
    ")
            .AsNoTracking()
            .FirstAsync();

        var response = new GetDelayedTrainsResponse
        {
            numberOfdelayedTrains = result.NumberOfDelayedTrains,
            numberOfAffectedStops = result.NumberOfAffectedStops
        };

        // store in cache
        _cache.Set(cacheKey, response, CacheDuration);

        return response;
    }

    private class DelayedTrainsResult
    {
        public int NumberOfAffectedStops { get; set; }
        public int NumberOfDelayedTrains { get; set; }
    }
}
