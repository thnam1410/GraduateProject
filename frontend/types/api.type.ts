export type ApiResponse<T = any> = {
	success: boolean;
	errors?: Record<string, string[]>;
	error?: any;
	message: string;
	result?: T;
};
export type PaginatedList<TItems> = {
	currentPage: number;
	items: TItems[];
	limit: number;
	offset: number;
	totalCount: number;
	totalPages: number;
};
