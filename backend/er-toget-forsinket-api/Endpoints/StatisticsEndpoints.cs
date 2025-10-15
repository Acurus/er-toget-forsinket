using System.Security.Claims;
using er_toget_forsinket_api.Models;
using er_toget_forsinket_api.Services;
using Models.Statistics;

namespace er_toget_forsinket_api.Endpoints;

public class StatisticsEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("statistics/get_delayed_trains",
                async (IStatisticsService statisticsService) =>
                {
                    var res = await statisticsService.GetDelayedTrains();
                    return res.Match(
                        res => Results.Ok(res),
                        errors => Results.Problem(errors.FirstOrDefault().Description)
                    );
                })
            .WithName("get_delayed_trains")
            .Produces<GetDelayedTrainsResponse>()
            .Produces(404)
            .WithTags("Statistics");
    }
}