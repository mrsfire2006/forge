<script>
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
	import DashboardNavbar from '$lib/modules/workspaces/components/layout/DashboardNavbar.svelte';
	import DashboardSidebar from '$lib/modules/workspaces/components/layout/DashboardSidebar.svelte';
	let { children } = $props();
</script>

<Sidebar.Provider style="--sidebar-width: 22rem; --sidebar-width-mobile: 20rem;">
	<DashboardSidebar />

	<Sidebar.Inset class="relative">
		<DashboardNavbar />
		<div class="h-full">
			{@render children()}
		</div>
	</Sidebar.Inset>
</Sidebar.Provider>

<style>
</style>
