import type { Result } from '$lib/api-schema/schema-helper';
import { PUBLIC_ASP_API_URL } from '$env/static/public';
export async function ClientFetch<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
	const headers = new Headers(options.headers);

	if (!headers.has('Content-Type') && !(options.body instanceof FormData)) {
		headers.set('Content-Type', 'application/json');
	}

	const makeRequest = () =>
		fetch(`${PUBLIC_ASP_API_URL}${endpoint}`, {
			...options,
			headers,
			credentials: 'include'
		});

	let response = await makeRequest();

	if (response.status === 401) {
		window.location.href = '/login';
		throw new Error(response.statusText);
	}
	const result = (await response.json()) as T;

	return result;
}
