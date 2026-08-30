import { user } from '$lib/authentication/auth-schema';
import { pgEnum, pgTable, text, timestamp, unique } from 'drizzle-orm/pg-core';

export const workspace = pgTable('workspace', {
	id: text('id').primaryKey(),
	name: text('name').notNull(),
	slug: text('slug').notNull().unique(),
	description: text('description'),
	createdAt: timestamp('created_at').defaultNow().notNull(),
	updatedAt: timestamp('updated_at')
		.defaultNow()
		.$onUpdate(() => new Date())
		.notNull()
});

export const workspaceRole = pgEnum('workspace_role', ['owner', 'admin', 'member']);
export const workspaceMember = pgTable(
	'workspace_member',
	{
		id: text('id').primaryKey(),
		workspaceId: text('workspace_id')
			.notNull()
			.references(() => workspace.id),
		userId: text('user_id')
			.notNull()
			.references(() => user.id),
		role: workspaceRole('role').notNull().default('member'),
		joinedAt: timestamp('joined_at').defaultNow().notNull()
	},
	(table) => [unique().on(table.userId, table.workspaceId)]
);

export type WorkspaceSelect = typeof workspace.$inferSelect;
export type WorkspaceInsert = typeof workspace.$inferInsert;
export type WorkspaceMemberSelect = typeof workspaceMember.$inferSelect;
export type WorkspaceMemberInsert = typeof workspaceMember.$inferInsert;
export type WorkspaceRoleType = (typeof workspaceRole.enumValues)[number];
