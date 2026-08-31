<script lang="ts">
	import './layout.css';
 
	let { children } = $props();
</script>


<div class="h-dvh w-full">
	{@render children()}
</div>
