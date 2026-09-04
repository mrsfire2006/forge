
export async function BffFetch<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
	const headers = new Headers(options.headers);

	if (!headers.has('Content-Type') && !(options.body instanceof FormData)) {
		headers.set('Content-Type', 'application/json');
	}

	const response = await fetch(endpoint, {
		...options,
		headers,
		credentials: 'include'
	});

	if (response.status === 401) {
		window.location.href = '/login';
		throw new Error('Unauthorized');
	}

	return (await response.json()) as T;
}
