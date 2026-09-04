import { AspFetch } from '$lib/common/asp-fetch';
import { AuthFacade } from '$lib/features/auth/auth-facade';

export async function POST({ request }) {
	

	return AspFetch(request, AuthFacade.endpoints().api.mutations.logout, {
		method: 'POST',
 	});
}
