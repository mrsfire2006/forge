
import { AspFetch } from '$lib/common/asp-fetch';
import { AuthFacade } from '$lib/features/auth/auth-facade.js';

export async function POST({ request }) {
	const body = await request.json();

	return AspFetch(request, AuthFacade.endpoints().api.mutations.login, {
		method: 'POST',
		body: JSON.stringify(body)
	});
}
