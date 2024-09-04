using AutoMapper;
using Tasks.Common.DTOs.Task;

namespace Tasks.BLL.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskDTO, Entities.Task>();
            CreateMap<UpdateTaskDTO, Entities.Task>();
            CreateMap<Entities.Task, TaskDTO>();
        }
    }
}
