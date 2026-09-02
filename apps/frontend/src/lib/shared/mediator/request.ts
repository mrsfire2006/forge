export interface Request<TResponse = unknown> {
	readonly responseType?: TResponse;
}
