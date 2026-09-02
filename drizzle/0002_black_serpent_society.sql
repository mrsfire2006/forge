CREATE TYPE "public"."workspace_visibility" AS ENUM('public', 'private');--> statement-breakpoint
ALTER TABLE "workspace" ADD COLUMN "visibility" "workspace_visibility" DEFAULT 'private' NOT NULL;--> statement-breakpoint
ALTER TABLE "project" ADD COLUMN "color" text NOT NULL;