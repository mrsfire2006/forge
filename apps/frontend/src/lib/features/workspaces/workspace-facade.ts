import { resolve } from '$app/paths';
import type { paths } from '$lib/api-schema/schema';
import type { ApiSchema, Result } from '$lib/api-schema/schema-helper';
import { ClientFetch } from '$lib/common/client-fetch';
import { createMutation, useQueryClient } from '@tanstack/svelte-query';
import { AuthFacade } from '../auth/auth-facade';

type createWorkspaceRequest = ApiSchema['CreateWorkspaceCommand'];
type createWorkspaceResult = ApiSchema['ResultOfCreateWorkspaceResult'];

type userWorkspacesResult = ApiSchema['ResultOfGetUserWorkspacesResult'];

export type workspaceVisibility = ApiSchema['WorkspaceVisibility'];
export class WorkspaceFacade {
	static keys() {
		return {
			queries: {
				myWorkspaces: () => ['workspace', 'my-workspaces'] as const
			},
			mutations: {},
			invalidates: {
				createWorkspace: () => [this.keys().queries.myWorkspaces()] as const
			}
		};
	}
	public static endpoints() {
		return {
			api: {
				mutations: {
					createWorkspace: '/api/workspace/new' satisfies keyof paths
				}
			},
			bff: {
				mutations: {
					createWorkspace: resolve('/api/workspace/createWorkspace')
				}
			}
		};
	}
	public static cqrs() {
		return {
			mutations: {
				browser: {
					createWorkspace: {
						fn: async (request: createWorkspaceRequest): Promise<createWorkspaceResult> => {
							return ClientFetch<createWorkspaceResult>(
								this.endpoints().api.mutations.createWorkspace,
								{
									method: 'POST',
									body: JSON.stringify(request)
								}
							);
						}
					}
				}
			},
			queries: {
				browser: {
					getUserworkspaces: {
						fn: async (): Promise<createWorkspaceResult> => {
							return ClientFetch<createWorkspaceResult>(
								this.endpoints().api.mutations.createWorkspace,
								{
									method: 'GET'
								}
							);
						}
					}
				}
			}
		};
	}

	public static useTanstack() {
		return {
			createWorkspace: () => {
				const queryclient = useQueryClient();
				return createMutation(() => ({
					mutationFn: this.cqrs().mutations.browser.createWorkspace.fn,
					onSuccess: (data) => {
						if (data.isSuccess) {
							queryclient.invalidateQueries({
								queryKey: AuthFacade.keys().queries.myWorkspaces()
							});
						}
					}
				}));
			}
		};
	}
}
