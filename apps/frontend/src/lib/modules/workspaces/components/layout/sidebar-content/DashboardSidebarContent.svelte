<script lang="ts">
	import { resolve } from '$app/paths';
	import { page } from '$app/state';
	import UserLogo from '$lib/components/shared/UserLogo.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
	import { cn } from '$lib/utils';
	import { Layers, Users } from 'lucide-svelte';
	const currentPath = $derived(page.url.pathname);
	const sidebar = Sidebar.useSidebar();
	const isCompact = $derived(sidebar.isMobile ? sidebar.openMobile : sidebar.open);

	const menu = $derived([
		{
			id: 'workspaces',
			title: 'workspaces',
			href: resolve('/dashboard/workspaces'),
			icon: Layers,
			isActive: currentPath === '/dashboard/workspaces'
		},
		{
			id: 'users',
			title: 'users',
			href: resolve('/dashboard/users'),
			icon: Users,
			isActive: currentPath === '/dashboard/users'
		}
	]);
</script>

<Sidebar.Content class="gap-1 py-2">
	<Sidebar.Group>
		<Sidebar.Menu>
			<Sidebar.MenuItem>
				{#if isCompact}
					<div class="flex flex-col gap-1">
						<span
							class="mb-2.5 truncate px-6 text-start text-[10.5px] font-bold tracking-[0.08em] text-[#777d7a] uppercase transition-colors duration-150 group-hover/trigger:text-[#8a8f8c]"
						>
							Discover
						</span>
						<Sidebar.MenuSub class="border-none">
							{#each menu as item (item.id)}
								<Sidebar.MenuSubItem>
									<Sidebar.MenuSubButton
										href={item.href}
										class={cn(
											'group/item flex min-h-10 w-full cursor-pointer items-center gap-2.25 rounded-md px-2.25 py-1.75 text-left text-[12px] transition-all duration-150',
											item.isActive
												? 'bg-[#2a2725] text-cream shadow-[inset_2px_0_0_var(--color-primary)]'
												: 'text-[#9a9f9c] hover:bg-[#232120] hover:text-cream'
										)}
									>
										<item.icon
											class={cn(
												'size-4 shrink-0 transition-colors',
												item.isActive ? 'text-cream' : 'text-[#7d827f] group-hover/item:text-cream'
											)}
										/>
										<span class="truncate">{item.title}</span>
									</Sidebar.MenuSubButton>
								</Sidebar.MenuSubItem>
							{/each}
						</Sidebar.MenuSub>
					</div>
				{:else}
					<div class="flex w-full flex-col gap-5">
						{#each menu as item (item.id)}
							<a
								href={item.href}
								title={item.title}
								class={cn(
									'group/icon relative flex h-11 w-11 shrink-0 items-center justify-center rounded-xl transition-all duration-150',
									item.isActive
										? 'bg-[#2f2b27] text-cream'
										: 'text-[#8a8f8c] hover:bg-white/8 hover:text-cream active:scale-95'
								)}
							>
								<item.icon
									class={cn(
										'size-5 shrink-0 transition-transform duration-150',
										item.isActive ? 'text-primary' : 'group-hover/icon:scale-110'
									)}
								/>
							</a>
						{/each}
					</div>
				{/if}
			</Sidebar.MenuItem>
		</Sidebar.Menu>
	</Sidebar.Group>
</Sidebar.Content>

<Sidebar.Footer>
	<Sidebar.Menu>
		<Sidebar.MenuItem>
			<UserLogo id="" name="mrs" size={isCompact ? 40 : 30}>
				{#snippet badge()}
					{#if isCompact}
						<div class="flex flex-col items-start">
							<span class="font-bold text-foreground">mrs</span>
							<span class="text-[10px]">Admin</span>
						</div>
					{/if}
				{/snippet}
			</UserLogo>
		</Sidebar.MenuItem>
	</Sidebar.Menu>
</Sidebar.Footer>
