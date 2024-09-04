using Tasks.Entities.Enums;

namespace Tasks.Common.Requests
{
    public class GetTasksRequest : PaginationSortingRequest
    {
        public Status? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority? Priority { get; set; }
    }

    public class PaginationSortingRequest
    {
        public Sorting? Sorting { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
