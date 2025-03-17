export interface TrainComponent {
    id: number;
    name: string;
    uniqueNumber: string;
    canAssignQuantity: boolean;
    quantity: number;
}

export interface PaginatedResponse<T> {
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    items: T[];
}