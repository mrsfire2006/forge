<script lang="ts">
	import { QueryClient, QueryClientProvider } from '@tanstack/svelte-query';
	import './layout.css';
	import { SvelteQueryDevtools } from '@tanstack/svelte-query-devtools';

	let { children } = $props();
	const queryClient = new QueryClient();
</script>

<QueryClientProvider client={queryClient}>
	<div class="h-dvh w-full">
		{@render children()}
	</div>
	<SvelteQueryDevtools />
</QueryClientProvider>
