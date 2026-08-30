import { pgEnum, pgTable, text, timestamp, unique } from 'drizzle-orm/pg-core';
import { workspace, workspaceMember } from '../workspaces/schema';
export const projectStatus = pgEnum('project_status', [
	'planning',
	'active',
	'completed',
	'archived'
]);

export const project = pgTable('project', {
	id: text('id').notNull().primaryKey(),

	workspaceId: text('workspace_id')
		.notNull()
		.references(() => workspace.id),

	name: text('name').notNull(),

	description: text('description'),

	status: projectStatus('status').notNull().default('planning'),

	startDate: timestamp('start_date'),

	dueDate: timestamp('due_date'),

	completedAt: timestamp('completed_at'),

	archivedAt: timestamp('archived_at'),

	createdBy: text('created_by')
		.notNull()
		.references(() => workspaceMember.id),

	createdAt: timestamp('created_at').defaultNow().notNull(),

	updatedAt: timestamp('updated_at')
		.defaultNow()
		.$onUpdate(() => new Date())
		.notNull()
});
export const projectMember = pgTable(
	'project_member',
	{
		id: text('id').notNull().primaryKey(),
		workspaceMemberId: text('workspace_member_id')
			.notNull()
			.references(() => workspaceMember.id),
		projectId: text('project_id')
			.notNull()
			.references(() => project.id),
		joinedAt: timestamp('joined_at').defaultNow().notNull()
	},
	(table) => [unique().on(table.workspaceMemberId, table.projectId)]
);

export type ProjectSelect = typeof project.$inferSelect;
export type ProjectInsert = typeof project.$inferInsert;
export type ProjectMemberSelect = typeof projectMember.$inferSelect;
export type ProjectMemberInsert = typeof projectMember.$inferInsert;
export type ProjectStatusType = (typeof projectStatus.enumValues)[number];
