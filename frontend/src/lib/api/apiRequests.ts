import { env } from '$env/dynamic/public';

export default async function apiRequest<T>(
	method: 'GET' | 'POST' | 'PUT' | 'DELETE',
	endpoint: string,
	authToken?: string,
	body?: any
): Promise<T> {
	// console.log(`${method} - ${endpoint} - ${JSON.stringify(body)}`);

	const options: RequestInit = {
		method,
		headers: {
			'Content-Type': 'application/json',
			Authorization: `Bearer ${authToken}`
		}
	};

	if (method !== 'GET' && method !== 'DELETE') {
		options.body = JSON.stringify(body);
	}
	const response = await fetch(`${env.PUBLIC_BACKEND_BASE_URL}${endpoint}`, options);
	if (!response.ok) {
		console.error(`${endpoint} ${response.status}: ${response.statusText}`);
		console.error(await response.text());
		return [] as T;
	}
	try {
		return (await response.json()) as T;
	} catch (error) {
		console.error('response.json failed', error);
		return {} as T;
	}
}
