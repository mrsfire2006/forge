<script lang="ts">
	import { LockKeyhole, Earth, Check, ArrowUpRight, LoaderCircle } from 'lucide-svelte';
	import { WorkspaceFacade, type workspaceVisibility } from '../workspace-facade';
	import { goto } from '$app/navigation';
	import { resolve } from '$app/paths';
	import type { userWorkspace } from '$lib/features/auth/auth-facade';
	import * as Dialog from '$lib/components/ui/dialog/index.js';
	import type { Snippet } from 'svelte';

	interface Props {
		mode?: 'create' | 'edit';
		workspace?: userWorkspace;
		trigger?: Snippet;
		openPop?: boolean;
		onOpenChange?: (open: boolean) => void;
	}

	let { mode = 'create', workspace, trigger, openPop, onOpenChange }: Props = $props();

	const createWorkspace = WorkspaceFacade.useTanstack().createWorkspace();

	let formData = $state({ name: '', description: '' });
	let visibility = $state('Private');
	$effect(() => {
		if (workspace) {
			formData.name = workspace.name;
			formData.description = workspace.description ?? '';
			visibility = workspace.visibility;
		}
	});
	let error = $state('');

	let isValid = $derived(formData.name.trim().length > 0);

	async function handleSubmit(e: SubmitEvent) {
		e.preventDefault();
		if (!isValid || createWorkspace.isPending) return;

		error = '';

		const result = await createWorkspace.mutateAsync({
			name: formData.name.trim(),
			description: formData.description.trim(),
			visibility: visibility as workspaceVisibility
		});

		if (result.isSuccess) {
			await goto(resolve('/dashboard/workspaces/[slug]', { slug: result.value?.slug ?? '' }));
		} else {
			error = result.errorMessage ?? 'Error';
		}
	}
</script>

{#if mode === 'edit'}
	<Dialog.Root open={openPop} {onOpenChange}>
		<Dialog.Trigger>
			{#snippet child()}
				{@render trigger?.()}
			{/snippet}
		</Dialog.Trigger>
		<Dialog.Content class="p-0">
			<form class="workspace-form " onsubmit={handleSubmit}>
				<div class="form-group">
					<label for="workspace-name">Workspace name</label>
					<input
						id="workspace-name"
						placeholder="e.g. Product Design"
						bind:value={formData.name}
						autocomplete="off"
					/>
				</div>

				<div class="form-group">
					<label for="workspace-description">Description <small>Optional</small></label>
					<textarea
						id="workspace-description"
						placeholder="What will this workspace help your team do?"
						rows="4"
						bind:value={formData.description}></textarea>
				</div>

				<fieldset class="visibility-fieldset">
					<legend>Who can access it?</legend>

					<label class="visibility-choice" class:selected={visibility === 'Private'}>
						<input type="radio" name="visibility" value="Private" bind:group={visibility} />
						<LockKeyhole size={18} aria-hidden="true" />
						<span>
							<strong>Private</strong>
							<small>Only invited members can access this workspace.</small>
						</span>
						{#if visibility === 'Private'}
							<Check size={16} class="check-icon" aria-hidden="true" />
						{/if}
					</label>

					<label class="visibility-choice" class:selected={visibility === 'Public'}>
						<input type="radio" name="visibility" value="Public" bind:group={visibility} />
						<Earth size={18} aria-hidden="true" />
						<span>
							<strong>Public</strong>
							<small>Anyone in Forge can discover and browse it.</small>
						</span>
						{#if visibility === 'Public'}
							<Check size={16} class="check-icon" aria-hidden="true" />
						{/if}
					</label>
				</fieldset>

				{#if error}
					<p class="form-error">{error}</p>
				{/if}

				<button type="submit" class="auth-submit" disabled={!isValid || createWorkspace.isPending}>
					{#if createWorkspace.isPending}
						<LoaderCircle size={16} class="spin" aria-hidden="true" />
						Creating…
					{:else}
						Create workspace
						<ArrowUpRight size={16} aria-hidden="true" />
					{/if}
				</button>
			</form>
		</Dialog.Content>
	</Dialog.Root>
{:else}
	<form class="workspace-form" onsubmit={handleSubmit}>
		<div class="form-group">
			<label for="workspace-name">Workspace name</label>
			<input
				id="workspace-name"
				placeholder="e.g. Product Design"
				bind:value={formData.name}
				autocomplete="off"
			/>
		</div>

		<div class="form-group">
			<label for="workspace-description">Description <small>Optional</small></label>
			<textarea
				id="workspace-description"
				placeholder="What will this workspace help your team do?"
				rows="4"
				bind:value={formData.description}></textarea>
		</div>

		<fieldset class="visibility-fieldset">
			<legend>Who can access it?</legend>

			<label class="visibility-choice" class:selected={visibility === 'Private'}>
				<input type="radio" name="visibility" value="Private" bind:group={visibility} />
				<LockKeyhole size={18} aria-hidden="true" />
				<span>
					<strong>Private</strong>
					<small>Only invited members can access this workspace.</small>
				</span>
				{#if visibility === 'Private'}
					<Check size={16} class="check-icon" aria-hidden="true" />
				{/if}
			</label>

			<label class="visibility-choice" class:selected={visibility === 'Public'}>
				<input type="radio" name="visibility" value="Public" bind:group={visibility} />
				<Earth size={18} aria-hidden="true" />
				<span>
					<strong>Public</strong>
					<small>Anyone in Forge can discover and browse it.</small>
				</span>
				{#if visibility === 'Public'}
					<Check size={16} class="check-icon" aria-hidden="true" />
				{/if}
			</label>
		</fieldset>

		{#if error}
			<p class="form-error">{error}</p>
		{/if}

		<button type="submit" class="auth-submit" disabled={!isValid || createWorkspace.isPending}>
			{#if createWorkspace.isPending}
				<LoaderCircle size={16} class="spin" aria-hidden="true" />
				Creating…
			{:else}
				Create workspace
				<ArrowUpRight size={16} aria-hidden="true" />
			{/if}
		</button>
	</form>
{/if}

<style>
	.workspace-form {
		border: 1px solid var(--border);
		background: var(--card);
		border-radius: 12px;
		gap: 22px;
		padding: 28px;
		display: grid;
	}
	.form-group {
		gap: 7px;
		display: grid;
	}
	.form-group label {
		color: #c5c8c4;
		font-size: 12px;
	}
	.form-group label small {
		color: var(--muted-foreground);
		margin-left: 5px;
		font-weight: 400;
	}
	.form-group input,
	.form-group textarea {
		border: 1px solid var(--border);
		width: 100%;
		color: var(--foreground);
		font: inherit;
		background: #151718;
		border-radius: 7px;
		outline: none;
		padding: 11px 12px;
		font-size: 13px;
		transition:
			border-color 0.15s,
			box-shadow 0.15s;
	}
	.form-group textarea {
		resize: vertical;
		font-size: 12px;
	}
	.form-group input:focus,
	.form-group textarea:focus {
		border-color: var(--primary);
		box-shadow: 0 0 0 3px color-mix(in srgb, var(--primary) 20%, transparent);
	}

	.visibility-fieldset {
		border: 0;
		gap: 8px;
		margin: 0;
		padding: 0;
		display: grid;
	}
	.visibility-fieldset legend {
		color: #c5c8c4;
		margin-bottom: 2px;
		font-size: 12px;
	}
	.visibility-choice {
		border: 1px solid var(--border);
		color: var(--muted-foreground);
		cursor: pointer;
		border-radius: 8px;
		align-items: center;
		gap: 11px;
		padding: 13px;
		display: flex;
		position: relative;
		transition:
			border-color 0.15s,
			background 0.15s;
	}
	.visibility-choice:hover {
		border-color: color-mix(in srgb, var(--primary) 40%, var(--border));
	}
	.visibility-choice.selected {
		color: var(--primary);
		background: #25201e;
		border-color: #6c463e;
	}
	.visibility-choice input {
		opacity: 0;
		position: absolute;
	}
	.visibility-choice span {
		display: grid;
		gap: 3px;
	}
	.visibility-choice strong {
		font-size: 13px;
		font-weight: 600;
		color: var(--foreground);
		letter-spacing: -0.01em;
	}
	.visibility-choice small {
		color: var(--muted-foreground);
		font-weight: 400;
		font-size: 11.5px;
		line-height: 1.4;
	}
	.visibility-choice :global(.check-icon) {
		margin-inline-start: auto;
		flex-shrink: 0;
	}

	.form-error {
		color: #e5878a;
		font-size: 12px;
		margin: -8px 0 0;
	}

	.auth-submit {
		background: var(--primary);
		width: 100%;
		color: var(--primary-foreground);
		cursor: pointer;
		border: 0;
		border-radius: 7px;
		justify-content: center;
		align-items: center;
		gap: 8px;
		margin-top: 5px;
		padding: 12px 16px;
		font-size: 13px;
		font-weight: 700;
		transition:
			filter 0.2s,
			transform 0.2s,
			opacity 0.2s;
		display: flex;
	}
	.auth-submit:hover:not(:disabled) {
		filter: brightness(1.08);
	}
	.auth-submit:active:not(:disabled) {
		transform: scale(0.98);
	}
	.auth-submit:disabled {
		opacity: 0.5;
		cursor: not-allowed;
	}
	.auth-submit :global(.spin) {
		animation: spin 0.8s linear infinite;
	}
	@keyframes spin {
		to {
			transform: rotate(360deg);
		}
	}
</style>
