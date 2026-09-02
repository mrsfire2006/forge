// import type { WorkspaceInsert, WorkspaceSelect } from '../schema';
// export function createSlug(value: string): string {
// 	return value
// 		.trim()
// 		.toLowerCase()
// 		.replace(/[^a-z0-9\s-]/g, '')
// 		.replace(/\s+/g, '-')
// 		.replace(/-+/g, '-');
// }
// export type WorkspaceUpdate = Partial<Pick<WorkspaceSelect, 'name' | 'slug' | 'description'>>;

// export class WorkSpace {
// 	private changed = false;
// 	constructor(private workspace: WorkspaceSelect) {}

// 	public static create(
// 		name: string,
// 		slug: string,
// 		description: string | undefined
// 	): WorkspaceInsert {
// 		return {
// 			id: crypto.randomUUID(),
// 			name,
// 			slug: createSlug(slug),
// 			description
// 		};
// 	}

// 	changeDescription(description: string): void {
// 		const trimmed = description.trim();

// 		if (!trimmed || trimmed === this.workspace.description) {
// 			return;
// 		}

// 		this.workspace.description = trimmed;
// 		this.changed = true;
// 	}

// 	changeSlug(slug: string): void {
// 		const normalized = createSlug(slug);

// 		if (!normalized || normalized === this.workspace.slug) {
// 			return;
// 		}

// 		this.workspace.slug = normalized;
// 		this.changed = true;
// 	}

// 	get changes(): WorkspaceUpdate {
// 		return {
// 			name: this.workspace.name,
// 			slug: this.workspace.slug,
// 			description: this.workspace.description
// 		};
// 	}

// 	get hasChanges(): boolean {
// 		return this.changed;
// 	}
// }
