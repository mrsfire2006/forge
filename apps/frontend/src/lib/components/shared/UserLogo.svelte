<script lang="ts">
	import { cn } from '$lib/utils';
	import type { Snippet } from 'svelte';
	import { useSidebar } from '../ui/sidebar';

	interface Props {
		name: string;
		id: string;
		size?: number;
		badge?: Snippet;
		customStyle?: string;
		showBadge?: boolean;
	}

	let { name, id, badge, customStyle, showBadge = true, size = 40 }: Props = $props();
	let initials = $derived.by(() => {
		const value = name.trim();

		if (!value) {
			return '?';
		}

		return value
			.split(/\s+/)
			.map((word) => word[0])
			.join('')
			.slice(0, 2)
			.toUpperCase();
	});

	function hashId(value: string): number {
		let hash = 0;

		for (const char of value) {
			hash = (hash << 5) - hash + char.charCodeAt(0);
			hash |= 0;
		}

		return Math.abs(hash);
	}
	function hslToRgb(h: number, s: number, l: number): [number, number, number] {
		s /= 100;
		l /= 100;

		const c = (1 - Math.abs(2 * l - 1)) * s;
		const x = c * (1 - Math.abs(((h / 60) % 2) - 1));
		const m = l - c / 2;

		let r: number;
		let g: number;
		let b: number;

		if (h < 60) [r, g, b] = [c, x, 0];
		else if (h < 120) [r, g, b] = [x, c, 0];
		else if (h < 180) [r, g, b] = [0, c, x];
		else if (h < 240) [r, g, b] = [0, x, c];
		else if (h < 300) [r, g, b] = [x, 0, c];
		else [r, g, b] = [c, 0, x];

		return [Math.round((r + m) * 255), Math.round((g + m) * 255), Math.round((b + m) * 255)];
	}

	function getLuminance([r, g, b]: [number, number, number]) {
		const values = [r, g, b].map((value) => {
			const channel = value / 255;

			return channel <= 0.03928 ? channel / 12.92 : Math.pow((channel + 0.055) / 1.055, 2.4);
		});

		return 0.2126 * values[0] + 0.7152 * values[1] + 0.0722 * values[2];
	}

	let colors = $derived.by(() => {
		const value = id.trim() || name.trim();

		if (!value) {
			return {
				background: 'hsl(220, 15%, 25%)',
				text: 'white'
			};
		}

		const hash = hashId(value);

		const hue = hash % 360;
		const saturation = 65 + (hash % 15); // 65-80%
		const lightness = 45 + (hash % 10); // 45-55%

		const background = `hsl(${hue}, ${saturation}%, ${lightness}%)`;
		const rgb = hslToRgb(hue, saturation, lightness);
		const backgroundLuminance = getLuminance(rgb);

		const whiteContrast = (1 + 0.05) / (backgroundLuminance + 0.05);

		const blackContrast = (backgroundLuminance + 0.05) / 0.05;

		return {
			background,
			text: whiteContrast >= blackContrast ? 'white' : 'black'
		};
	});

	const sidebar = useSidebar();
</script>

<div
	class={cn(
		'group/user relative flex items-center gap-2 rounded-xl px-2 py-1.5',
		'transition-all duration-150',
		'text-[#8a8f8c] hover:bg-white/6 hover:text-cream',
		'active:scale-[0.98]',
		customStyle
	)}
>
	<div
		class="user-logo"
		style="	
			
			background-color: {colors.background};
			color: {colors.text};
			width: {size}px;
			height: {size}px;
			font-size: {size * 0.4}px;"
		title={name}
	>
		{initials}
	</div>
	<div class={sidebar.isMobile && !showBadge ? 'hidden' : 'block'}>
		{@render badge?.()}
	</div>
</div>

<style>
	.user-logo {
		display: inline-flex;
		align-items: center;
		justify-content: center;
		border-radius: 50%;
		color: white;
		font-weight: 600;
		user-select: none;
		flex-shrink: 0;

		font-family: 'Plus Jakarta Sans', sans-serif;
		transition:
			width 0.15s ease,
			height 0.15s ease,
			font-size 0.15s ease;
	}
</style>
