<script lang="ts">
	import { page } from '$app/state';

	const { children } = $props();
	const slug = $derived(page.params.slug);
</script>

<svelte:head>
	<title>{slug}</title>
</svelte:head>
{@render children()}
