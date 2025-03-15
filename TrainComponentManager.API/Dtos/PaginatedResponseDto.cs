﻿namespace TrainComponentManager.API.Dtos
{
    public class PaginatedResponseDto<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; } = [];
    }
}
