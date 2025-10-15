using ErrorOr;
using Models.Statistics;

namespace er_toget_forsinket_api.Services;

public interface IStatisticsService
{
     Task<ErrorOr<GetDelayedTrainsResponse>> GetDelayedTrains();
}