<script>
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
	import { ChevronDown, PanelLeftClose, PanelLeftOpen } from 'lucide-svelte';
	import UserLogo from '$lib/components/shared/UserLogo.svelte';
	import { page } from '$app/state';
	import WorkspaceNavbarContent from './navbar-content/WorkspaceNavbarContent.svelte';
	import DashboardNavbarContent from './navbar-content/DashboardNavbarContent.svelte';
	import * as Popover from '$lib/components/ui/popover/index.js';

	const sidebar = Sidebar.useSidebar();
	const isCompact = $derived(sidebar.isMobile ? sidebar.openMobile : sidebar.open);

	const currentPath = $derived(page.url.pathname);
</script>

<nav
	class="flex min-h-16 flex-row items-center justify-between border-b border-b-border bg-background"
>
	<Sidebar.Trigger
		class="
				
				group hover:bg-accent hover:text-accent-foreground focus-visible:ring-ring ml-2 flex
				size-9 shrink-0 items-center
				justify-center
				rounded-md
				border
				border-border/60
				bg-background text-muted-foreground
				shadow-sm transition-all
				duration-150
				focus-visible:ring-1 focus-visible:outline-none
				active:scale-95
			"
	>
		{#if !isCompact}
			<PanelLeftOpen class="size-4" />
		{:else}
			<PanelLeftClose class="size-4" />
		{/if}
	</Sidebar.Trigger>

	{#if currentPath === '/dashboard/workspaces' || currentPath === '/dashboard/users'}
		<DashboardNavbarContent />
	{:else}
		<WorkspaceNavbarContent />
	{/if}

	<Popover.Root>
		<Popover.Trigger>
			<UserLogo showBadge={false} customStyle="mr-5" id="" name="mrs">
				{#snippet badge()}
					<ChevronDown size={15} />
				{/snippet}
			</UserLogo>
		</Popover.Trigger>
		<Popover.Content class="w-20">settings</Popover.Content>
	</Popover.Root>
</nav>
{#if sidebar.isMobile && sidebar.openMobile}
	<button
		type="button"
		onclick={() => sidebar.setOpenMobile(false)}
		aria-label="close sidebar"
		class="
            group fixed top-1/2 right-5 z-1000
            flex origin-right
			rotate-90
            items-center gap-2.5
            rounded-xl rounded-tl-none rounded-tr-none
            border border-t-0 border-white/15
            bg-[#1a1c1d]/95
            px-5 py-2.5
            text-[11px] font-semibold
            tracking-widest text-gray-300
            uppercase
            shadow-xl shadow-black/40
            backdrop-blur-xl
            transition-all duration-300 ease-out
            hover:border-white/25
            hover:bg-[#232526]
            hover:text-white
            hover:shadow-2xl hover:shadow-black/50
            focus-visible:ring-2
            focus-visible:ring-primary/60
            focus-visible:ring-offset-2
            focus-visible:ring-offset-[#0d0f10]
            focus-visible:outline-none
            active:scale-[0.97]
            active:shadow-lg
        "
	>
		<span class="relative flex size-2 shrink-0">
			<span
				class="
                    absolute inline-flex h-full w-full
                    animate-ping rounded-full bg-primary opacity-30
                    group-hover:opacity-60
                "
			></span>
			<span
				class="
                    relative inline-flex size-2 rounded-full bg-primary
                    transition-transform duration-200
                    group-hover:scale-125
                "
			></span>
		</span>

		<span class="font-medium whitespace-nowrap select-none"> Tap to close </span>
	</button>
{/if}
