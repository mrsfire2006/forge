import type { WorkspaceMemberInsert, WorkspaceMemberSelect, WorkspaceRoleType } from '../schema';
export type WorkspaceMemberUpdate = Partial<Pick<WorkspaceMemberSelect, 'role'>>;

export class WorkSpaceMember {
	private changed = false;
	constructor(private workspaceMember: WorkspaceMemberSelect) {}

	public static create(
		userId: string,
		workspaceId: string,
		role: WorkspaceRoleType
	): WorkspaceMemberInsert {
		return {
			id: crypto.randomUUID(),
			workspaceId,
			userId,
			role
		};
	}
	get id(): string {
		return this.workspaceMember.id;
	}

	changeRole(role: WorkspaceRoleType): void {
		if (role === this.workspaceMember.role) {
			return;
		}
		this.workspaceMember.role = role;
		this.changed = true;
	}

	get changes(): WorkspaceMemberUpdate {
		return {
			role: this.workspaceMember.role
		};
	}

	get hasChanges(): boolean {
		return this.changed;
	}
}
