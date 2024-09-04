using Tasks.Common.DTOs;
using Tasks.Common.DTOs.Task;
using Tasks.Common.Requests;

namespace Tasks.BLL.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDTO> AddAsync(Guid userId, CreateTaskDTO dto);
        Task<bool> DeleteAsync(Guid userId, Guid id);
        Task<PageList<TaskDTO>> GetAllAsync(GetTasksRequest request);
        Task<TaskDTO> GetAsync(Guid id);
        Task<TaskDTO> UpdateAsync(UpdateTaskDTO dto, Guid id, Guid userId);
    }
}
