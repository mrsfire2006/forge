import type { components } from './schema';

export type ApiSchema = components['schemas'];

type ApiResult = ApiSchema['Result'];

export interface Result<T = unknown> extends ApiResult {
	value?: T;
}
