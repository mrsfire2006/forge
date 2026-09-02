<script lang="">
	import { resolve } from '$app/paths';
	import { redirect } from '@sveltejs/kit';

	redirect(307, resolve('/dashboard/workspaces'));
</script>
