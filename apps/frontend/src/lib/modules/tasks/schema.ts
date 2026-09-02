// import { integer, pgEnum, pgTable, text, timestamp, unique } from 'drizzle-orm/pg-core';
// import { project, projectMember } from '../projects/schema';
// import { label } from '../labels/schema';

// export const taskStatus = pgEnum('task_status', [
// 	'backlog',
// 	'in_progress',
// 	'review',
// 	'testing',
// 	'done'
// ]);
// export const taskPriority = pgEnum('task_priority', ['low', 'medium', 'high', 'urgent']);

// export const task = pgTable(
// 	'task',
// 	{
// 		id: text('id').primaryKey(),
// 		projectId: text('project_id')
// 			.notNull()
// 			.references(() => project.id),
// 		title: text('title').notNull(),
// 		description: text('description'),
// 		status: taskStatus('status').notNull().default('backlog'),
// 		priority: taskPriority('priority').notNull().default('medium'),
// 		assigneeId: text('assignee_id')
// 			.references(() => projectMember.id),
// 		createdBy: text('created_by')
// 			.notNull()
// 			.references(() => projectMember.id),
// 		dueDate: timestamp('due_date'),
// 		position: integer('position').notNull().default(1000),

// 		createdAt: timestamp('created_at').notNull().defaultNow(),

// 		updatedAt: timestamp('updated_at')
// 			.defaultNow()
// 			.$onUpdate(() => new Date())
// 			.notNull()
// 	},
// 	(table) => [unique().on(table.projectId, table.status, table.position)]
// );
// export const taskLabel = pgTable(
// 	'task_label',
// 	{
// 		taskId: text('task_id')
// 			.notNull()
// 			.references(() => task.id),
// 		labelId: text('label_id')
// 			.notNull()
// 			.references(() => label.id)
// 	},
// 	(table) => [unique().on(table.labelId, table.taskId)]
// );

// export type TaskSelect = typeof task.$inferSelect;
// export type TaskInsert = typeof task.$inferInsert;
// export type TaskPriorityType = (typeof taskPriority.enumValues)[number];
// export type TaskStatusType = (typeof taskStatus.enumValues)[number];

// export type TaskLabelSelect = typeof taskLabel.$inferSelect;
// export type TaskLabelInsert = typeof taskLabel.$inferInsert;
