<script lang="ts">
	import { resolve } from '$app/paths';
	import UserLogo from '$lib/components/shared/UserLogo.svelte';
	import type { userWorkspace } from '$lib/features/auth/auth-facade';
	import { ArrowUpRight, Earth, LockKeyhole, SquarePen, Users } from 'lucide-svelte';
	interface Props {
		workspace: userWorkspace;
		onEdit: (workspace: userWorkspace) => void;
	}

	let { workspace, onEdit }: Props = $props();

	function formatRelativeTime(date: string): string {
		const diff = Date.now() - new Date(date).getTime();

		const seconds = Math.floor(diff / 1000);
		const minutes = Math.floor(seconds / 60);
		const hours = Math.floor(minutes / 60);
		const days = Math.floor(hours / 24);

		if (seconds < 60) {
			return 'just now';
		}

		if (minutes < 60) {
			return `${minutes} min ago`;
		}

		if (hours < 24) {
			return `${hours} ${hours === 1 ? 'hour' : 'hours'} ago`;
		}

		if (days < 7) {
			return `${days} ${days === 1 ? 'day' : 'days'} ago`;
		}

		return new Date(date).toLocaleDateString();
	}

	// workspace.membersPreviews = Array.from({ length: 15 }, (_, i) => ({
	// 	userId: crypto.randomUUID(),
	// 	username: `User ${i + 1}`
	// }));
</script>

<div class="workspace-card flex flex-col overflow-hidden">
	<div class="workspace-card-top mb-5">
		<UserLogo id={workspace.id} name={workspace.name} radius={25} size={44} />
		<button
			type="button"
			class="edit-button"
			aria-label="Edit workspace"
			onclick={(e) => {
				e.stopPropagation();
				onEdit(workspace);
			}}
		>
			<SquarePen size={16} strokeWidth={2} />
		</button>
	</div>

	<a href={resolve('/dashboard/workspaces/[slug]', { slug: workspace.slug })}>
		<h2>
			{workspace.name}
			<span class={`visibility-badge ${workspace.visibility === 'Private' ? 'private' : 'public'}`}>
				{#if workspace.visibility === 'Private'}
					<LockKeyhole size={11} aria-hidden="true" />
				{:else}
					<Earth size={11} aria-hidden="true" />
				{/if}
				{workspace.visibility}
			</span>
			<div class="workspace-arrow">
				<ArrowUpRight size={16} strokeWidth={1.5} />
			</div>
		</h2>
		<p>
			{workspace.description
				? 'workspace.description'
				: 'Build, ship, and improve the products that move Acme forward.'}
		</p>
		<div class="workspace-card-meta">
			<span>
				<Users size={16} />
				{workspace.totalMembers} members</span
			><span>{workspace.totalProjects} projects</span>
		</div>
		<div class="workspace-card-bottom">
			<div class="avatar-stack select-none">
				{#each workspace.membersPreviews.slice(0, 5) as member (member.userId)}
					<UserLogo
						customStyle="-ml-[4px]"
						hover={false}
						size={23}
						id={member.userId}
						name={member.username}
					/>
				{/each}
				{#if workspace.membersPreviews.length > 5}
					<UserLogo
						size={23}
						ignoreInitial={true}
						id=""
						backgroundColor={'#343736'}
						textColor={'#d7d9d5'}
						name={`+${workspace.membersPreviews.length - 5}`}
					/>
				{/if}
			</div>
			<small>Active {formatRelativeTime(workspace.updateAt)} min ago</small>
		</div>
	</a>
</div>

<style>
	.workspace-card {
		border: 1px solid var(--border);
		background: var(--card);
		min-height: 267px;
		color: var(--foreground);
		border-radius: 10px;
		flex-direction: column;
		padding: 20px;
		text-decoration: none;
		transition:
			border-color 0.2s,
			transform 0.2s;
		display: flex;
	}
	.workspace-card:hover {
		border-color: #5a423a;
		transform: translateY(-2px);
	}
	.workspace-card-top,
	.workspace-card-bottom,
	.workspace-card-meta {
		justify-content: space-between;
		align-items: center;
		gap: 10px;
		display: flex;
	}
	.workspace-card-top,
	.workspace-card-bottom,
	.workspace-card-meta {
		justify-content: space-between;
		align-items: center;
		gap: 10px;
		display: flex;
	}
	.workspace-card p {
		min-height: 43px;
		color: var(--muted-foreground);
		margin: 0;
		font-size: 11px;
		line-height: 1.55;
	}
	.workspace-card-meta {
		color: #a1a5a1;
		justify-content: flex-start;
		gap: 15px;
		margin-top: 22px;
		font-size: 10px;
	}
	.workspace-card-bottom {
		border-top: 1px solid #2b2d2d;
		margin-top: auto;
		padding-top: 17px;
	}
	.workspace-card-top,
	.workspace-card-bottom,
	.workspace-card-meta {
		justify-content: space-between;
		align-items: center;
		gap: 10px;
		display: flex;
	}
	.avatar-stack {
		padding-left: 4px;
		display: flex;
	}
	.workspace-card-bottom small {
		color: var(--muted-foreground);
		font-size: 10px;
	}
	.workspace-card h2 {
		letter-spacing: -0.02em;
		align-items: center;
		gap: 8px;
		margin: 0 0 7px;
		font-size: 16px;
		display: flex;
		flex-wrap: wrap;
	}

	.workspace-large-avatar.coral,
	.avatar-coral {
		background: var(--primary);
	}
	.workspace-large-avatar {
		color: #181918;
		border-radius: 9px;
		place-items: center;
		width: 44px;
		height: 44px;
		font-size: 12px;
		font-weight: 800;
		display: grid;
	}
	.visibility-badge.public {
		color: #91bad5;
		background: #222c31;
	}

	.visibility-badge.private {
		color: #ddb09e;
		background: #292522;
	}

	.workspace-card-meta span {
		align-items: center;
		gap: 5px;
		display: flex;
		margin-bottom: 5px;
	}
	.visibility-badge {
		border-radius: 5px;
		align-items: center;
		gap: 4px;
		padding: 3px 6px;
		font-size: 9px;
		font-weight: 600;
		text-transform: uppercase;
		letter-spacing: 0.02em;
		display: inline-flex;
	}
	.workspace-card .workspace-arrow {
		color: var(--muted-foreground);
		opacity: 0;
		transform: translate(-3px, 3px);
		transition:
			opacity 0.2s ease,
			transform 0.2s ease;
	}

	.workspace-card:hover .workspace-arrow {
		opacity: 1;
		transform: translate(0, 0);
	}
	.edit-button {
		display: grid;
		place-items: center;
		width: 32px;
		height: 32px;
		border-radius: 8px;
		border: 1px solid var(--border);
		background: transparent;
		color: var(--muted-foreground);
		cursor: pointer;
		flex-shrink: 0;
		transition:
			background 0.15s ease,
			color 0.15s ease,
			border-color 0.15s ease,
			transform 0.1s ease;
	}

	.edit-button:hover {
		background: var(--accent, rgba(255, 255, 255, 0.06));
		color: var(--foreground);
		border-color: #5a423a;
	}

	.edit-button:active {
		transform: scale(0.94);
	}

	.edit-button:focus-visible {
		outline: 2px solid var(--primary);
		outline-offset: 2px;
	}
	.logo-wrap {
		position: relative;
		width: fit-content;
	}

	.visibility-dot {
		position: absolute;
		bottom: -3px;
		right: -3px;
		display: grid;
		place-items: center;
		width: 18px;
		height: 18px;
		border-radius: 50%;
		border: 2px solid var(--card);
	}

	.visibility-dot.public {
		color: #91bad5;
		background: #222c31;
	}

	.visibility-dot.private {
		color: #ddb09e;
		background: #292522;
	}
</style>
