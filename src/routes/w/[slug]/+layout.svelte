<script>
	import WorkspaceNavbar from '$lib/modules/workspaces/components/layout/WorkspaceNavbar.svelte';
	import WorkspaceSidebar from '$lib/modules/workspaces/components/layout/WorkspaceSidebar.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
  	let { children } = $props();
 </script>

<Sidebar.Provider style="--sidebar-width: 22rem; --sidebar-width-mobile: 20rem;">
	<WorkspaceSidebar />

	<Sidebar.Inset class="relative">
		<WorkspaceNavbar />

 		<div>
			{@render children()}
		</div>
	</Sidebar.Inset>
</Sidebar.Provider>

<style>
</style>
