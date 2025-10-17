import { env } from '$env/dynamic/private';
import apiRequest from "$lib/api/apiRequests";
import type { DelayedTrainsResponse } from "$lib/models/DelayedTrainsResponse";

export async function load() {
    const response = await apiRequest<DelayedTrainsResponse>(
        'GET',
        '/statistics/get_delayed_trains'
    );
    return {
        "numberOfdelayedTrains": response.numberOfdelayedTrains,
        "timeSinceLastDelayMinutes": response.numberOfAffectedStops
    };
}