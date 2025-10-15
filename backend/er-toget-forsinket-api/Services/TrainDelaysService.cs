using ErrorOr;
using er_toget_forsinket_api.Models.TrainDelays;
using er_toget_forsinket_api.Repositories;
using System.Xml.Linq;

namespace er_toget_forsinket_api.Services;

public class TrainDelaysService : ITrainDelaysService
{
    private readonly PostgresContext _database;
    private readonly ILogger<TrainDelaysService> _logger;

    public TrainDelaysService(PostgresContext database, ILogger<TrainDelaysService> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<ErrorOr<double>> EstimatedTimetable(EstimatedTimetableRaw request)
    {
        var res = await _database.AddAsync(request);
        var saveRes = await _database.SaveChangesAsync();
        return saveRes;
    }

    public async Task<ErrorOr<double>> SituationExchange(SituationExchangeRaw request)
    {
        var situationExchange = ParseSituationExchange(request.RawMessage);
        await _database.AddAsync(request);
        await _database.AddAsync(situationExchange);
        var saveRes = await _database.SaveChangesAsync();
        return saveRes;
    }

    private static SituationExchange ParseSituationExchange(string xmlString)
    {
        XNamespace ns = "http://www.siri.org.uk/siri";
        var doc = XDocument.Parse(xmlString);

        var situationExchange = new SituationExchange
        {
            CreationTime = (string)doc.Root.Element(ns + "CreationTime"),
            ParticipantRef = (string)doc.Root.Element(ns + "ParticipantRef"),
            Priority = (int?)doc.Root.Element(ns + "Priority"),
            SituationNumber = (string)doc.Root.Element(ns + "SituationNumber"),
            Version = (string)doc.Root.Element(ns + "Version"),
            VersionedAtTime = (string)doc.Root.Element(ns + "VersionedAtTime"),
            SummaryNo = doc.Root.Elements(ns + "Summary")
                .FirstOrDefault(e => e.Attribute(XNamespace.Xml + "lang")?.Value == "NO")?.Value
        };

        var journeys = doc.Root
            .Descendants(ns + "AffectedVehicleJourney")
            .Select(j => new AffectedVehicleJourney
            {
                DatedVehicleJourneyRef = (string)j.Element(ns + "DatedVehicleJourneyRef"),
                StopPointRef = (string)j
                    .Descendants(ns + "AffectedStopPoint")
                    .FirstOrDefault()?.Element(ns + "StopPointRef"),
                StopConditions = string.Join(",", j
                    .Descendants(ns + "AffectedStopPoint")
                    .Elements(ns + "StopCondition")
                    .Select(sc => sc.Value))
            }).ToList();

        situationExchange.AffectedJourneys = journeys;
        return situationExchange;
    }
}