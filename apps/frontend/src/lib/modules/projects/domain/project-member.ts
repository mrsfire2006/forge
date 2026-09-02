import type { ProjectMemberInsert, ProjectMemberSelect } from '../schema';

export class ProjectMember {
	private changed = false;

	constructor(private projectMember: ProjectMemberSelect) {}

	static create(workspaceMemberId: string, projectId: string): ProjectMemberInsert {
		return {
			id: crypto.randomUUID(),
			workspaceMemberId,
			projectId
		};
	}

	get id(): string {
		return this.projectMember.id;
	}
	get workspaceMemberId(): string {
		return this.projectMember.workspaceMemberId;
	}

	get projectId(): string {
		return this.projectMember.projectId;
	}

	get hasChanges(): boolean {
		return this.changed;
	}
}
