interface ClientResult<T = unknown> {
	errorMessage?: string;
	isSuccess: boolean;
	value?: T;
}

export class ServerResult<T = unknown> {
	constructor(
		public isSuccess = false,
		public value?: T,
		public errorMessage?: string
	) {}

	public getClientResult(): ClientResult<T> {
		return {
			isSuccess: this.isSuccess,
			errorMessage: this.errorMessage,
			value: this.value
		};
	}
	public static success<T = unknown>(value?: T): ServerResult<T> {
		const result = new ServerResult<T>();

		result.value = value;
		result.isSuccess = true;
		result.errorMessage = undefined;

		return result;
	}

	public static failure(errorMessage: string): ServerResult<never> {
		const result = new ServerResult<never>();

		result.errorMessage = errorMessage;
		result.isSuccess = false;
		result.value = undefined;
		return result;
	}
}
