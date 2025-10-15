using ErrorOr;
using er_toget_forsinket_api.Models.TrainDelays;

namespace er_toget_forsinket_api.Services;

public interface ITrainDelaysService
{
    Task<ErrorOr<double>> EstimatedTimetable(EstimatedTimetableRaw request);
    Task<ErrorOr<double>> SituationExchange(SituationExchangeRaw request);
}