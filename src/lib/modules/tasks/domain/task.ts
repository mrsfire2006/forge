import type { TaskInsert, TaskPriorityType, TaskSelect, TaskStatusType } from '../schema';

export type TaskUpdate = Partial<
	Pick<
		TaskSelect,
		'position' | 'status' | 'title' | 'dueDate' | 'description' | 'priority' | 'assigneeId'
	>
>;

export class Task {
	private changed = false;
	constructor(private task: TaskSelect) {}

	public static create(
		projectId: string,
		title: string,
		description: string | undefined,
		status: TaskStatusType,
		priority: TaskPriorityType,
		assigneeId: string | undefined,
		position: number,
		createdById: string
	): TaskInsert {
		return {
			id: crypto.randomUUID(),
			projectId,
			title,
			description,
			status,
			priority,
			assigneeId,
			position,
			createdBy: createdById
		};
	}

	get hasChanges() {
		return this.changed;
	}
	get changes(): TaskUpdate {
		return {
			title: this.task.title,
			assigneeId: this.task.assigneeId,
			description: this.task.description,
			position: this.task.position,
			dueDate: this.task.dueDate,
			priority: this.task.priority,
			status: this.task.status
		};
	}
}
