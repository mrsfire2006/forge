<script lang="ts">
	import { goto } from '$app/navigation';
	import { resolve } from '$app/paths';

	import { authClient } from '$lib/authentication/auth-client';

	const session = authClient.useSession();
	$effect(() => {
		if ($session.isPending) {
			return;
		}
		if (!$session.data) {
			void goto(resolve('/login'));
		}
	});
</script>
