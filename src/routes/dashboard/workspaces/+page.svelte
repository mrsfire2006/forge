<script>
	import { resolve } from '$app/paths';
	import { Plus, Search } from 'lucide-svelte';
</script>

<main class="h-full p-[54px_48px_80px]">
	<div class="feature-heading">
		<div>
			<p class="eyebrow">Your spaces</p>
			<h1>Workspaces</h1>
			<p>Everything your teams are building, in one place.</p>
		</div>
		<a class="create-button" href={resolve('/dashboard/users')}
			>
           <Plus/>
			Create workspace</a
		>
	</div>
	<dir class="directory-toolbar"
		><div class="directory-search">
			<Search/><input placeholder="Search workspaces..." aria-label="Search workspaces" value="" />
		</div></dir
	>
	<div class="workspace-summary"></div>
	<div class="workspaces-grid"></div>
</main>

<style>
	.feature-heading {
		justify-content: space-between;
		align-items: flex-end;
		gap: 28px;
		margin-bottom: 34px;
		display: flex;
	}
	.directory-toolbar {
		justify-content: space-between;
		align-items: center;
		gap: 14px;
		margin-bottom: 16px;
		display: flex;
	}
	.directory-search {
		border: 1px solid var(--border);
		width: min(100%, 390px);
		color: var(--muted-foreground);
		background: #1b1d1e;
		border-radius: 7px;
		align-items: center;
		gap: 10px;
		padding: 10px 12px;
		display: flex;
	}
	.directory-search input {
		min-width: 0;
		color: var(--foreground);
		font: inherit;
		background: none;
		border: 0;
		outline: 0;
		flex: 1;
		font-size: 12px;
	}
	.eyebrow {
		text-transform: uppercase;
		letter-spacing: 0.14em;
		color: #747a77;
		margin: 0 0 8px;
		font-size: 9px;
		font-weight: 700;
	}
	.feature-heading h1,
	.create-layout h1 {
		letter-spacing: -0.05em;
		margin: 7px 0 8px;
		font-size: 39px;
	}
	.feature-heading p:not(.eyebrow),
	.create-intro {
		color: var(--muted-foreground);
		margin: 0;
		font-size: 13px;
		line-height: 1.6;
	}

	.create-button {
		background: var(--cream);
		color: #242424;
		cursor: pointer;
		border: 0;
		border-radius: 6px;
		align-items: center;
		gap: 6px;
		padding: 8px 12px;
		font-size: 11px;
		font-weight: 700;
		display: flex;
	}
</style>
