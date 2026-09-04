import { AspFetch } from '$lib/common/asp-fetch';
import { WorkspaceFacade } from '$lib/features/workspaces/workspace-facade.js';

export async function POST({ request }) {
	const body = await request.json();

	return AspFetch(request, WorkspaceFacade.endpoints().api.mutations.createWorkspace, {
		method: 'POST',
		body: JSON.stringify(body)
	});
}
