using System.Security.Claims;
using er_toget_forsinket_api.Models.TrainDelays;
using er_toget_forsinket_api.Services;

namespace er_toget_forsinket_api.Endpoints;

public class WebhookEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapPost("webhooks/entur/deviations/et",
                async (HttpRequest request, ITrainDelaysService trainDelaysService) =>
                {
                    using var reader = new StreamReader(request.Body);
                    var message = await reader.ReadToEndAsync();
                    var entity = new EstimatedTimetableRaw
                    {
                        RawMessage = message,
                        ReceivedDateTime = DateTimeOffset.UtcNow
                    };
                    var result = await trainDelaysService.EstimatedTimetable(entity);
                    return result.Match(
                        _ => Results.Ok(),
                        errors => Results.Problem(errors.FirstOrDefault().Description)
                    );
                })
            .WithName("post_entur_deviation_estimated_timetable")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Entur Deviations");
        app.MapPost("webhooks/entur/deviations/sx",
                async (HttpRequest request, ITrainDelaysService trainDelaysService) =>
                {
                    using var reader = new StreamReader(request.Body);
                    var message = await reader.ReadToEndAsync();
                    var entity = new SituationExchangeRaw
                    {
                        RawMessage = message,
                        ReceivedDateTime = DateTimeOffset.UtcNow
                    };
                    var result = await trainDelaysService.SituationExchange(entity);
                    return result.Match(
                        _ => Results.Ok(),
                        errors => Results.Problem(errors.FirstOrDefault().Description)
                    );
                })
            .WithName("post_entur_deviation_situation_exchange")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Entur Deviations");
    }
}