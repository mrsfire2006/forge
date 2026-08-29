import { betterAuth } from 'better-auth';
import { drizzleAdapter } from '@better-auth/drizzle-adapter';
import { db } from '$lib/server/db';
import { env } from '$env/dynamic/private';
const auth = betterAuth({
	database: drizzleAdapter(db, {
		provider: 'pg',
		schemaName: 'auth'
	}),
	secret:env.BETTER_AUTH_SECRET,
	baseURL:env.BETTER_AUTH_URL,
	emailAndPassword: {
		enabled: true
	}
});
export default auth;
