import { AspFetch } from '$lib/common/asp-fetch';
import { AuthFacade } from '$lib/features/auth/auth-facade.js';

export async function GET({ request }) {
	return AspFetch(request, AuthFacade.endpoints().api.queries.userProfile, {
		method: 'GET'
	});
}
