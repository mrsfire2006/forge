<script lang="ts" module>
	import type { Icon as IconType } from 'lucide-svelte';

	export type ProjectItem = {
		id: string;
		name: string;
		icon?: typeof IconType;
		color: string;
	};
</script>

<script lang="ts">
	import Logo from '$lib/components/shared/Logo.svelte';
	import Separator from '$lib/components/ui/separator/separator.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';
	import * as Collapsible from '$lib/components/ui/collapsible/index.js';

	import { cn } from '$lib/utils';
	import {
		ChevronRight,
		LayoutDashboard,
		Circle,
		ClipboardCheck,
		FolderOpen,
		Projector,
		Book,
		Rocket,
		Globe,
		Folder,
		PanelLeftOpen,
		PanelLeftClose
	} from 'lucide-svelte';
	import { resolve } from '$app/paths';
	import { page } from '$app/state';
 
	const slug = $derived(page.params.slug ?? '');
	const currentPath = $derived(page.url.pathname);

	const STATIC_MENU = $derived([
		{
			id: 'workspace',
			title: 'workspace',
			items: [
				{
					id: 'overview',
					title: 'Overview',
					icon: LayoutDashboard,
					href: resolve('/w/[slug]', { slug }),
					matchPrefix: false,
					color: undefined
				},
				{
					id: 'my tasks',
					title: 'MyTasks',
					icon: ClipboardCheck,
					href: resolve('/w/[slug]/tasks', { slug }),
					matchPrefix: false,
					color: undefined
				}
			]
		},
		{
			id: 'projects',
			title: 'projects',
			items: []
		}
	]);

	type Props = {
		projects?: ProjectItem[];
		onNavigate?: (id: string, href?: string) => void;
	};

	let { projects = [], onNavigate }: Props = $props();
	projects = [
		{
			id: 'echo',
			name: 'echo',
			icon: Projector,
			color: '#60A5FA'
		},
		{
			id: 'libres',
			name: 'libres',
			icon: Book,
			color: '#A78BFA' // violet
		},
		{
			id: 'forge',
			name: 'forge',
			icon: Folder,
			color: '#FB7185' // rose
		},
		{
			id: 'atlas',
			name: 'atlas',
			icon: Globe,
			color: '#34D399' // emerald
		},
		{
			id: 'nova',
			name: 'nova',
			icon: Rocket,
			color: '#FBBF24' // amber
		}
	];
	const menu = $derived(
		STATIC_MENU.map((section) => {
			const items =
				section.id === 'projects'
					? projects.map((p) => ({
							id: p.id,
							title: p.name,
							icon: p.icon ?? Circle,
							href: resolve('/w/[slug]/projects/[projectId]', { slug, projectId: p.id }),
							matchPrefix: true,
							color: p.color
						}))
					: section.items;

			return {
				...section,
				items: items.map((item) => ({
					...item,
					isActive: item.matchPrefix
						? currentPath === item.href || currentPath.startsWith(item.href + '/')
						: currentPath === item.href
				}))
			};
		})
	);
	const sidebar = Sidebar.useSidebar();
	const isCollapsed = $derived(sidebar.state === 'collapsed');
	const isCompact = $derived(sidebar.isMobile ? sidebar.openMobile : sidebar.open);

	const SECTION_IDS = ['workspace', 'projects'] as const;

	let openSections = $state<Record<string, boolean>>(
		Object.fromEntries(SECTION_IDS.map((id) => [id, true]))
	);
</script>

<svelte:head>
	<title>{slug} · Forge</title>
</svelte:head>

<Sidebar.Root collapsible="icon" class="  border border-t-0 border-border bg-[#191b1cf5]">
	<Sidebar.Header class="h-16 gap-0 p-0">
		<div
			class={cn(
				'flex h-16 flex-row items-center justify-between',
				'transition-[padding] duration-200 ease-linear',
				'ps-3',
				'group-data-[collapsible=icon]:ps-6'
			)}
		>
			<Logo compact={!isCompact} />
			{#if sidebar.isMobile}
				<Sidebar.Trigger
					class={cn(
						'group/trigger flex size-7 items-center justify-center rounded-md',
						'text-sidebar-foreground/50 transition-all duration-150',
						'hover:bg-sidebar-accent hover:text-sidebar-foreground',
						'focus-visible:ring-sidebar-ring p-5 focus-visible:ring-1 focus-visible:outline-none'
					)}
				>
					{#if !isCompact}
						<PanelLeftOpen class="size-4" />
					{:else}
						<PanelLeftClose class="size-4" />
					{/if}
				</Sidebar.Trigger>
			{/if}
		</div>

		<Separator
			class={cn(
				'mx-auto transition-[width] duration-200 ease-linear',
				isCollapsed ? 'w-[65%]!' : 'w-full'
			)}
		/>
	</Sidebar.Header>

	<Sidebar.Content class="gap-1 py-2">
		{#each menu as section, index (section.id)}
			<Sidebar.Group class="py-0.5">
				<Sidebar.Menu>
					<Collapsible.Root bind:open={openSections[section.id]}>
						<Sidebar.MenuItem>
							{#if isCompact}
								<Collapsible.Trigger
									class="group/trigger flex h-8 w-full items-center gap-2 rounded-md px-2 text-sm font-medium transition-colors duration-150 hover:bg-white/4"
								>
									<span
										class="flex-1 truncate text-start text-[10.5px] font-bold tracking-[0.08em] text-[#6b706d] uppercase transition-colors duration-150 group-hover/trigger:text-[#8a8f8c]"
									>
										{section.title}
									</span>

									{#if section.items.length > 0}
										<span
											class="rounded-[4px] bg-white/5 px-1.5 py-0.5 text-[10px] font-medium text-[#6b706d] tabular-nums"
										>
											{section.items.length}
										</span>
									{/if}

									<ChevronRight
										class="size-3.5 shrink-0 text-[#4d514f] transition-transform duration-200 ease-out group-data-[state=open]/trigger:rotate-90 group-data-[state=open]/trigger:text-[#8a8f8c]"
									/>
								</Collapsible.Trigger>
							{/if}

							<Collapsible.Content forceMount>
								{#snippet child({ props, open })}
									<div
										{...props}
										class="grid transition-[grid-template-rows] duration-200 ease-out"
										style:grid-template-rows={open ? '1fr' : '0fr'}
									>
										<div class="overflow-hidden">
											{#if isCompact}
												<Sidebar.MenuSub class={cn('mt-1 flex flex-col gap-1 border-none px-0')}>
													{#each section.items as item (item.id)}
														<Sidebar.MenuSubItem class={cn(!isCompact && 'w-full')}>
															<Sidebar.MenuSubButton
																href={item.href}
																onclick={() => onNavigate?.(item.id, item.href)}
																class={cn(
																	'group/item flex min-h-8.5 w-full items-center gap-2.25 rounded-md px-2.25 py-1.75 text-left text-[12px] transition-all duration-150',
																	item.isActive
																		? 'bg-[#2a2725] text-cream shadow-[inset_2px_0_0_var(--color-primary)]'
																		: 'text-[#9a9f9c] hover:bg-[#232120] hover:text-cream'
																)}
															>
																{#if item.icon}
																	{#if item.color}
																		<button
																			class="z-10 flex size-5 shrink-0 items-center justify-center rounded-md"
																			style:background-color={`${item.color}22`}
																		>
																			<item.icon class="size-3.5 shrink-0" />
																		</button>
																	{:else}
																		<item.icon
																			class={cn(
																				'size-4 shrink-0 transition-colors',
																				item.isActive
																					? 'text-cream'
																					: 'text-[#7d827f] group-hover/item:text-cream'
																			)}
																		/>
																	{/if}
																{/if}

																<span class="truncate">{item.title}</span>
															</Sidebar.MenuSubButton>
														</Sidebar.MenuSubItem>
													{:else}
														<div
															class="mx-0.5 flex flex-col items-center gap-1.5 rounded-md border border-dashed border-white/6 px-3 py-4 text-center"
														>
															<FolderOpen class="size-4 text-[#4d514f]" />
															<span class="text-[11px] leading-tight text-[#5f6461]">
																No projects yet
															</span>
														</div>
													{/each}
												</Sidebar.MenuSub>
											{:else}
												<div class="flex flex-col gap-5">
													{#each section.items.slice(0, 3) as item (item.id)}
														<a
															href={item.href}
															onclick={() => onNavigate?.(item.id, item.href)}
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
											{#if !isCompact && index < menu.length - 1}
												<Separator />
											{/if}
										</div>
									</div>
								{/snippet}
							</Collapsible.Content>
						</Sidebar.MenuItem>
					</Collapsible.Root>
				</Sidebar.Menu>
			</Sidebar.Group>
		{/each}
	</Sidebar.Content>
</Sidebar.Root>

<style>
</style>
