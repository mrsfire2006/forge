import { getContext, setContext } from 'svelte';

const COLOR_PICKER_KEY = Symbol('color-picker');

type OpenOptions = {
	value: string;
	onchoose?: (color: string) => void;
};

class ColorPickerState {
	open = $state(false);
	color = $state('#60A5FA');

	private onchoose?: (color: string) => void;

	openPicker({ value, onchoose }: OpenOptions) {
		this.color = value;
		this.onchoose = onchoose;
		this.open = true;
	}

	apply() {
		this.onchoose?.(this.color);
		this.close();
	}

	close() {
		this.open = false;
		this.onchoose = undefined;
	}

	setColor(color: string) {
		this.color = color;
	}
}

export function setColorPicker() {
	const state = new ColorPickerState();

	setContext(COLOR_PICKER_KEY, state);

	return state;
}

export function useColorPicker() {
	return getContext<ColorPickerState>(COLOR_PICKER_KEY);
}
