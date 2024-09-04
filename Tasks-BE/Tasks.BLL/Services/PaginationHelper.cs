using Tasks.BLL.Services.Interfaces;
using Tasks.Common.DTOs;

namespace Tasks.BLL.Services
{
    public class PaginationHelper : IPaginationHelper
    {
        public PageList<T> Apply<T>(List<T> items, int page, int pageSize)
        {
            var pageUsers = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageList = new PageList<T>()
            {
                TotalCount = items.Count,
                TotalPages = (int)Math.Ceiling((decimal)items.Count / pageSize),
                Items = pageUsers
            };

            return pageList;
        }
    }
}
