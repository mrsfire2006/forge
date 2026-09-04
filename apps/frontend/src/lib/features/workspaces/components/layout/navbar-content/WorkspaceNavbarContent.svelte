<script lang="ts">
	import { page } from '$app/state';
	import { ChevronRight } from 'lucide-svelte';
	const projectId = $derived(page.params.projectId ?? '');
	const slug = $derived(page.params.slug ?? '');
	const currentPath = $derived(page.url.pathname);

	const pageName = $derived(
		currentPath === `/dashboard/workspaces/${slug}`
			? 'Overview'
			: currentPath === `/dashboard/workspaces/${slug}/tasks`
				? 'My Tasks'
				: currentPath === `/dashboard/workspaces/${slug}/projects`
					? 'Projects'
					: ''
	);
 
</script>

<nav aria-label="Breadcrumb" class="ml-5 flex shrink grow items-center justify-start gap-2 text-sm">
	<div class="h-5 w-px bg-border/70"></div>
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
