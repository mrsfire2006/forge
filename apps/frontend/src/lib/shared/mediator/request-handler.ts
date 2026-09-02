import type { Request } from './request';

export interface RequestHandler<TRequest extends Request, TResponse> {
	execute(request: TRequest): Promise<TResponse>;
}
