<script>
	import { goto } from '$app/navigation';
	import LoadingCircle from '$lib/components/shared/loading-circle.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
	import { AuthFacade } from '$lib/features/auth/auth-facade';
	import DashboardNavbar from '$lib/features/workspaces/components/layout/DashboardNavbar.svelte';
	import DashboardSidebar from '$lib/features/workspaces/components/layout/DashboardSidebar.svelte';
	let { children } = $props();
	let userQuery = AuthFacade.useTanstack().userProfile();
	let userProfile = $derived(userQuery.data);
	$effect(() => {
		if ( userProfile?.isFailure || userProfile?.statusCode === 401) {
			goto('/login');
		}
	});
</script>

{#if userQuery && userQuery.isSuccess}
	<Sidebar.Provider style="--sidebar-width: 22rem; --sidebar-width-mobile: 20rem;">
		<DashboardSidebar />

		<Sidebar.Inset class="relative">
			<DashboardNavbar />
			<div class="h-full">
				{@render children()}
			</div>
		</Sidebar.Inset>
	</Sidebar.Provider>
{:else if userProfile?.isFailure || userQuery.isError}
	<div class="flex min-h-screen items-center justify-center">Redirecting...</div>
{:else if userQuery.isPending}
	<LoadingCircle />
{/if}

<style>
</style>
