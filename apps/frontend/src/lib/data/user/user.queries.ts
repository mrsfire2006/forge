import { userKeys } from './user.keys';

export const userQueries = {

	detail: (id: string) => ({
		queryKey: userKeys.detail(id)
	})
};
