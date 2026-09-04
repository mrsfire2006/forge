<script>
	import { resolve } from '$app/paths';
	import Button from '$lib/components/ui/button/button.svelte';
	import { AuthFacade } from '$lib/features/auth/auth-facade';
	const { mutateAsync: LogoutAsync } = AuthFacade.useTanstack().logout();
</script>

<div class="flex">
	<Button href={resolve('/dashboard/workspaces')}>workspaces</Button>
	<Button href={resolve('/dashboard/users')}>users</Button>
	<Button href={resolve('/dashboard/workspaces/[slug]', { slug: 'hello' })}>W</Button>
	<Button
		onclick={async () => {
			await LogoutAsync();
		}}
	>Logout</Button>
</div>
