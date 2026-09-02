<script lang="ts">
	let { compact } = $props<{
		compact: boolean;
	}>();
</script>

<div class="flex items-center justify-center gap-2.25 text-[20px] font-[750] tracking-[-0.04em]">
	<span class="grid h-5 w-5 rotate-45 place-items-center border-2 border-solid border-primary">
		<span class="h-1.5 w-1.5 bg-primary"></span>
	</span>

	{#if !compact}
		<span>forge</span>
	{/if}
</div>
