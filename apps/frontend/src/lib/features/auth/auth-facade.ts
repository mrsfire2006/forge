import { resolve } from '$app/paths';
import type { paths } from '$lib/api-schema/schema';

import type { ApiSchema, Result } from '$lib/api-schema/schema-helper';
import { ClientFetch } from '$lib/common/client-fetch';
import { createMutation, createQuery, useQueryClient } from '@tanstack/svelte-query';

type loginRequest = ApiSchema['LoginCommandRequest'];
type registerRequest = ApiSchema['RegisterCommandRequest'];
type userWorkspacesResult = ApiSchema['ResultOfGetUserWorkspacesResult'];
export type userWorkspaces = userWorkspacesResult['value'];
export type userWorkspace = NonNullable<userWorkspacesResult['value']>['data'][number] ;

export type userProfileResult = ApiSchema['ResultOfUserProfileResult'];
export class AuthFacade {
	static keys() {
		return {
			queries: {
				user: () => ['auth', 'user'] as const,
				myWorkspaces: () => ['workspace', 'my-workspaces'] as const
			},

			mutations: {
				register: () => ['auth', 'register'] as const,
				login: () => ['auth', 'login'] as const,
				logout: () => ['auth', 'logout'] as const
			},
			invalidates: {
				register: () => [this.keys().queries.user] as const,

				login: () => [this.keys().queries.user] as const,

				logout: () => [this.keys().queries.user] as const
			}
		};
	}
	public static cqrs() {
		return {
			mutations: {
				browser: {
					register: {
						mutationKey: this.keys().mutations.register,
						fn: async (request: registerRequest): Promise<Result> => {
							return ClientFetch<Result>(this.endpoints().api.mutations.register, {
								method: 'POST',
								body: JSON.stringify(request)
							});
						}
					},
					login: {
						key: this.keys().mutations.login,
						fn: (request: loginRequest): Promise<Result> => {
							return ClientFetch<Result>(this.endpoints().api.mutations.login, {
								method: 'POST',
								body: JSON.stringify(request)
							});
						}
					},
					logout: {
						key: this.keys().mutations.logout,
						fn: async (): Promise<Result> => {
							return ClientFetch<Result>(this.endpoints().api.mutations.logout, {
								method: 'POST'
							});
						}
					}
				}
			},
			queries: {
				broswer: {
					userProfile: {
						queryKey: this.keys().queries.user(),
						fn: async (): Promise<userProfileResult> => {
							return ClientFetch<userProfileResult>(this.endpoints().api.queries.userProfile, {
								method: 'GET'
							});
						}
					},
					getUserworkspaces: {
						fn: async (): Promise<userWorkspacesResult> => {
							return ClientFetch<userWorkspacesResult>(this.endpoints().api.queries.workspaces, {
								method: 'GET'
							});
						}
					}
				}
			}
		};
	}
	public static endpoints() {
		return {
			api: {
				mutations: {
					register: '/api/auth/register' satisfies keyof paths,
					login: '/api/auth/login' satisfies keyof paths,
					logout: '/api/auth/logout' satisfies keyof paths
				},

				queries: {
					userProfile: '/api/auth/me' satisfies keyof paths,
					workspaces: '/api/auth/workspaces' satisfies keyof paths
				}
			},
			bff: {
				mutations: {
					register: resolve('/api/auth/register'),
					login: resolve('/api/auth/login'),
					logout: resolve('/api/auth/logout')
				},
				queries: {
					userProfile: resolve('/api/auth/me')
				}
			}
		};
	}

	public static useTanstack() {
		return {
			login: () => {
				const queryClient = useQueryClient();
				return createMutation(() => ({
					mutationKey: this.keys().mutations.login(),
					mutationFn: this.cqrs().mutations.browser.login.fn,
					onSuccess: (data) => {
						if (data.isSuccess) {
							queryClient.invalidateQueries({ queryKey: this.keys().invalidates.login() });
						}
					}
				}));
			},
			register: () => {
				const queryClient = useQueryClient();

				return createMutation(() => ({
					mutationKey: this.keys().mutations.register(),
					mutationFn: this.cqrs().mutations.browser.register.fn,
					onSuccess: (data) => {
						if (data.isSuccess) {
							queryClient.invalidateQueries({ queryKey: this.keys().invalidates.register() });
						}
					}
				}));
			},
			logout: () => {
				const queryClient = useQueryClient();

				return createMutation(() => ({
					mutationKey: this.keys().mutations.logout(),
					mutationFn: this.cqrs().mutations.browser.logout.fn
				}));
			},
			userProfile: () => {
				return createQuery(() => ({
					queryKey: this.keys().queries.user(),
					queryFn: this.cqrs().queries.broswer.userProfile.fn,
					staleTime: 1000 * 60,
					retry: false
				}));
			},
			userWorkspaces: () => {
				return createQuery(() => ({
					queryKey: this.keys().queries.myWorkspaces(),
					queryFn: this.cqrs().queries.broswer.getUserworkspaces.fn,
					staleTime: 1000 * 60,
					retry: false
				}));
			}
		};
	}
}
