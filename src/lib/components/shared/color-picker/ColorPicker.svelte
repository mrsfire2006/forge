<script lang="ts">
	import * as Popover from '$lib/components/ui/popover/index.js';
	import { Button } from '$lib/components/ui/button/index.js';
	import { Check, X } from 'lucide-svelte';

	import { useColorPicker } from './color-picker-context.svelte';

	const colorPicker = useColorPicker();

	let tempColor = $state('#60A5FA');

	$effect(() => {
		if (colorPicker.open) {
			tempColor = colorPicker.color;
		}
	});

	function updateColor(color: string) {
		tempColor = color.toUpperCase();
	}

	function updateHex(event: Event) {
		const input = event.currentTarget as HTMLInputElement;

		let color = input.value.trim();

		if (color && !color.startsWith('#')) {
			color = `#${color}`;
		}

		tempColor = color.toUpperCase();
	}

	function applyColor() {
		if (!/^#[0-9A-F]{6}$/i.test(tempColor)) {
			return;
		}

		colorPicker.setColor(tempColor);
		colorPicker.apply();
	}

	function cancel() {
		colorPicker.close();
	}
</script>

<Popover.Root bind:open={colorPicker.open}>
	<Popover.Content class="w-72 p-4">
		<div class="space-y-4">
			<!-- Header -->
			<div class="flex items-center justify-between">
				<h3 class="text-sm font-semibold text-foreground">Choose color</h3>

				<Popover.Close
					class="hover:bg-accent flex size-7 items-center justify-center rounded-md text-muted-foreground transition-colors hover:text-foreground"
					aria-label="Close"
					onclick={cancel}
				>
					<X class="size-4" />
				</Popover.Close>
			</div>

			<!-- Color -->
			<input
				type="color"
				value={tempColor}
				oninput={(event) => {
					const input = event.currentTarget as HTMLInputElement;
					updateColor(input.value);
				}}
				class="h-32 w-full cursor-pointer rounded-lg border border-border bg-transparent p-1"
				aria-label="Choose color"
			/>

			<!-- HEX -->
			<div class="flex items-end gap-3">
				<div
					class="size-10 shrink-0 rounded-lg border border-border shadow-inner"
					style:background-color={tempColor}
					aria-hidden="true"
				></div>

				<label class="min-w-0 flex-1">
					<span
						class="mb-1 block text-[10px] font-medium tracking-wider text-muted-foreground uppercase"
					>
						HEX
					</span>

					<input
						type="text"
						value={tempColor}
						oninput={updateHex}
						class="h-9 w-full rounded-md border border-border bg-background px-3 font-mono text-xs text-foreground transition-colors outline-none focus:border-primary focus:ring-1 focus:ring-primary/30"
						maxlength="7"
						autocomplete="off"
						spellcheck="false"
						aria-label="HEX color"
					/>
				</label>
			</div>

			<!-- Actions -->
			<div class="flex justify-end gap-2 pt-1">
				<Button variant="ghost" size="sm" onclick={cancel}>Cancel</Button>

				<Button size="sm" class="gap-2" onclick={applyColor}>
					<Check class="size-4" />
					Choose
				</Button>
			</div>
		</div>
	</Popover.Content>
</Popover.Root>
