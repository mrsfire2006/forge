<script lang="ts">
	import Logo from '$lib/components/shared/Logo.svelte';
	import Separator from '$lib/components/ui/separator/separator.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';

	import { cn } from '$lib/utils';
	import { PanelLeftOpen, PanelLeftClose } from 'lucide-svelte';
	import WorkspaceSidebarContent from './sidebar-content/WorkspaceSidebarContent.svelte';
	import { page } from '$app/state';
	import DashboardSidebarContent from './sidebar-content/DashboardSidebarContent.svelte';
	const sidebar = Sidebar.useSidebar();
	const isCollapsed = $derived(sidebar.state === 'collapsed');
	const currentPath = $derived(page.url.pathname);

	const isCompact = $derived(sidebar.isMobile ? sidebar.openMobile : sidebar.open);
</script>

<svelte:head>
	<!-- <title>{slug} · Forge</title> -->
</svelte:head>

<Sidebar.Root collapsible="icon" class="  border border-t-0 border-border bg-[#191b1cf5]">
	<Sidebar.Header class="h-16 min-h-16 gap-0 p-0">
		<div
			class={cn(
				'flex h-16 flex-row items-center justify-between',
				'transition-[padding] duration-200 ease-linear',
				'ps-3',
				'group-data-[collapsible=icon]:ps-6'
			)}
		>
			<Logo compact={!isCompact} />
			{#if sidebar.isMobile}
				<Sidebar.Trigger
					class={cn(
						'group/trigger flex size-7 items-center justify-center rounded-md',
						'text-sidebar-foreground/50 transition-all duration-150',
						'hover:bg-sidebar-accent hover:text-sidebar-foreground',
						'focus-visible:ring-sidebar-ring p-5 focus-visible:ring-1 focus-visible:outline-none'
					)}
				>
					{#if !isCompact}
						<PanelLeftOpen class="size-4" />
					{:else}
						<PanelLeftClose class="size-4" />
					{/if}
				</Sidebar.Trigger>
			{/if}
		</div>

		<Separator
			class={cn(
				'mx-auto transition-[width] duration-200 ease-linear',
				isCollapsed ? 'w-[65%]!' : 'w-full'
			)}
		/>
	</Sidebar.Header>
	{#if currentPath === '/dashboard/workspaces' || currentPath === '/dashboard/users' || currentPath === '/dashboard/workspaces/new'}
		<DashboardSidebarContent />
	{:else}
		<WorkspaceSidebarContent />
	{/if}
</Sidebar.Root>
