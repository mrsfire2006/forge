import type { ProjectInsert, ProjectSelect, ProjectStatusType } from '../schema';

export type ProjectUpdate = Partial<
	Pick<
		ProjectSelect,
		'name' | 'description' | 'status' | 'dueDate' | 'archivedAt' | 'completedAt' | 'color'
	>
>;

export class Project {
	private changed = false;

	constructor(private project: ProjectSelect) {}

	public static create(
		name: string,
		workspaceId: string,
		description: string | undefined,
		workspaceMemberId: string,
		status: ProjectStatusType,
		color: string
	): ProjectInsert {
		return {
			id: crypto.randomUUID(),
			name,
			workspaceId,
			createdBy: workspaceMemberId,
			description,
			status,
			color
		};
	}
	changeColor(color: string) {
		if (!color || color === this.project.color) {
			return;
		}
		this.project.color = color;
		this.changed = true;
	}
	changeStatus(status: ProjectStatusType): void {
		if (this.project.status === status) {
			return;
		}

		const now = new Date();

		switch (status) {
			case 'completed':
				this.project.completedAt = now;
				this.project.archivedAt = null;
				break;

			case 'archived':
				this.project.archivedAt = now;
				break;

			case 'planning':
			case 'active':
				this.project.completedAt = null;
				this.project.archivedAt = null;
				break;
		}

		this.project.status = status;
		this.changed = true;
	}

	changeName(name: string): void {
		const trimmed = name.trim();
		if (!trimmed || trimmed === this.project.name) {
			return;
		}
		this.project.name = trimmed;
		this.changed = true;
	}
	changeDescription(description: string): void {
		const trimmed = description.trim();

		if (!trimmed || trimmed === this.project.description) {
			return;
		}

		this.project.description = trimmed || null;

		this.changed = true;
	}
	get changes(): ProjectUpdate {
		return {
			name: this.project.name,
			description: this.project.description,
			status: this.project.status,
			dueDate: this.project.dueDate,
			archivedAt: this.project.archivedAt,
			completedAt: this.project.completedAt,
			color: this.project.color
		};
	}

	get hasChanges(): boolean {
		return this.changed;
	}
}
