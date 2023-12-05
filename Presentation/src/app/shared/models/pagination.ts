export interface Pagination<T> {
    pageSize: number,
    count: number,
    pageIndex: number,
    data: T
}