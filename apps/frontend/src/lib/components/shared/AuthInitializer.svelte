<script lang="ts">
	import { goto } from '$app/navigation';
	import { resolve } from '$app/paths';
	import { page } from '$app/state';
	import { AuthFacade } from '$lib/features/auth/auth-facade';

	const userQuery = AuthFacade.useTanstack().userProfile();

	const publicRoutes = [resolve('/'), resolve('/(auth)/login'), resolve('/(auth)/register')];

	$effect(() => {
		const isPublicRoute = publicRoutes.includes(page.url.pathname);

		if (userQuery.data?.isFailure && !isPublicRoute) {
			goto(resolve('/(auth)/login'));
		}
	});
</script>
