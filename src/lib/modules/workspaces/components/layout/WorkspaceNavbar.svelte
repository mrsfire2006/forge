<script>
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
	import { ChevronRight, PanelLeftClose, PanelLeftOpen } from 'lucide-svelte';
	import { page } from '$app/state';

	const slug = $derived(page.params.slug ?? '');
	const projectId = $derived(page.params.projectId ?? '');
	const currentPath = $derived(page.url.pathname);
	const pageName = $derived(
		currentPath === `/w/${slug}`
			? 'Overview'
			: currentPath === `/w/${slug}/tasks`
				? 'My Tasks'
				: currentPath === `/w/${slug}/projects`
					? 'Projects'
					: ''
	);
	const sidebar = Sidebar.useSidebar();
	const isCompact = $derived(sidebar.isMobile ? sidebar.openMobile : sidebar.open);
</script>

<nav class="flex h-16 flex-row items-center justify-between border-b border-b-border bg-background">
	<div class="flex min-w-0 items-center gap-3">
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
		<div class="h-5 w-px bg-border/70"></div>

		<nav aria-label="Breadcrumb" class="flex items-center gap-2 text-sm">
			<span class="font-medium text-muted-foreground">
				{slug}
			</span>

			<ChevronRight class="size-3.5 text-muted-foreground/50" />

			{#if projectId}
				<span class="text-muted-foreground"> Projects </span>

				<ChevronRight class="size-3.5 text-muted-foreground/50" />

				<span class="font-semibold text-foreground">
					{projectId}
				</span>
			{:else}
				<span class="font-semibold text-foreground">
					{pageName}
				</span>
			{/if}
		</nav>
	</div>
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
		<!-- النقطة الملونة مع نبض خفيف -->
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

		<!-- النص -->
		<span class="font-medium whitespace-nowrap select-none"> Tap to close </span>
	</button>
{/if}
