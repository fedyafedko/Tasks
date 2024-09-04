using Tasks.Common.DTOs;

namespace Tasks.BLL.Services.Interfaces
{
    public interface IPaginationHelper
    {
        PageList<T> Apply<T>(List<T> items, int page, int pageSize);
    }
}