// import { pgTable, text, timestamp, unique } from 'drizzle-orm/pg-core';
// import { project } from '../projects/schema';

// export const label = pgTable(
// 	'label',
// 	{
// 		id: text('id').notNull().primaryKey(),
// 		projectId: text('project_id')
// 			.notNull()
// 			.references(() => project.id),
// 		name: text('name').notNull(),
// 		color: text('color').notNull(),
// 		createdAt: timestamp('created_at').notNull().defaultNow()
// 	},
// 	(table) => [unique().on(table.projectId, table.name)]
// );

// export type LabelSelect = typeof label.$inferSelect;
// export type LabelInsert = typeof label.$inferInsert;
