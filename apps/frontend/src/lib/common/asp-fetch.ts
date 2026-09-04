// src/lib/server/asp-fetch.ts

import { PUBLIC_ASP_API_URL } from '$env/static/public';

export async function AspFetch(
	request: Request,
	endpoint: string,
	options: RequestInit = {}
): Promise<Response> {
	const headers = new Headers(options.headers);

	if (!headers.has('Content-Type') && !(options.body instanceof FormData)) {
		headers.set('Content-Type', 'application/json');
	}

	const cookie = request.headers
		.get('cookie')
		?.split(';')
		.find((cookie) => cookie.trim().startsWith('.AspNetCore.Identity.Application='));
	if (cookie) {
		headers.set('cookie', cookie);
	}
	return await fetch(`${PUBLIC_ASP_API_URL}${endpoint}`, {
		...options,
		headers
	});
}
