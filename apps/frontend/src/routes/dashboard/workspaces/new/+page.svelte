<script>
	import { resolve } from '$app/paths';
	import CreateEditWorkspace from '$lib/features/workspaces/components/CreateEditWorkspace.svelte';
 	import { ArrowLeft } from 'lucide-svelte';
</script>

<main class="feature-content">
	<div class="create-layout">
		<div class="create-intro-col">
			<a href={resolve('/dashboard/workspaces')} class="back-link">
				<ArrowLeft size={14} />
				<span>Back to workspaces</span>
			</a>

			<p class="eyebrow form-eyebrow">New workspace</p>
			<h1>Create a workspace</h1>
			<p class="create-intro">A focused home for a team, project, or community.</p>
		</div>
		<CreateEditWorkspace mode='create'/>
	</div>
</main>

<style>
	.feature-content {
		display: flex;
		justify-content: center;
		align-items: center;
		min-height: 100dvh;
		padding: 54px 24px 80px;
	}

	.create-layout {
		display: grid;
		grid-template-columns: 0.8fr 1.2fr;
		gap: clamp(10px, 5vw, 75px);
		width: 100%;
		max-width: 900px;
	}

	.create-intro-col {
		display: flex;
		flex-direction: column;
		align-items: flex-start;
	}

	.back-link {
		color: var(--muted-foreground);
		align-items: center;
		gap: 6px;
		font-size: 12px;
		font-weight: 500;
		text-decoration: none;
		display: inline-flex;
		margin: -6px 0 28px -6px;
		padding: 6px 10px;
		border-radius: 6px;
		transition:
			color 0.15s,
			background 0.15s;
	}

	.back-link:hover {
		color: var(--foreground);
		background: color-mix(in srgb, var(--foreground) 6%, transparent);
	}

	.back-link :global(svg) {
		transition: transform 0.15s;
	}

	.back-link:hover :global(svg) {
		transform: translateX(-2px);
	}

	.eyebrow {
		text-transform: uppercase;
		letter-spacing: 0.14em;
		color: #747a77;
		margin: 0 0 8px;
		font-size: 9px;
		font-weight: 700;
	}

	.create-intro {
		color: var(--muted-foreground);
		margin: 0;
		font-size: 13px;
		line-height: 1.6;
	}

	.create-layout h1 {
		letter-spacing: -0.05em;
		margin: 7px 0 8px;
		font-size: 39px;
	}

	/* Tablet */
	@media (max-width: 900px) {
		.create-layout {
			max-width: 640px;
		}
	}

	/* Below this, stack into a single column */
	@media (max-width: 720px) {
		.feature-content {
			padding: 32px 20px 56px;
			align-items: flex-start;
		}
		.create-layout {
			grid-template-columns: 1fr;
			gap: 28px;
		}
		.create-layout h1 {
			font-size: 28px;
		}
		.back-link {
			margin-bottom: 16px;
		}
	}

	/* Small phones */
	@media (max-width: 420px) {
		.feature-content {
			padding: 24px 16px 40px;
		}
		.create-layout h1 {
			font-size: 24px;
		}
	}
</style>