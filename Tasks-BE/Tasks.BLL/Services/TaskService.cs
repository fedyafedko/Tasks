using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tasks.BLL.Services.Interfaces;
using Tasks.Common.DTOs;
using Tasks.Common.DTOs.Task;
using Tasks.Common.Exceptions;
using Tasks.Common.Extensions;
using Tasks.Common.Requests;
using Tasks.DAL.Repositories.Interfaces;
using Tasks.Entities.Enums;

namespace Tasks.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(
            IRepository<Entities.Task> taskRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskDTO> AddAsync(Guid userId, CreateTaskDTO dto)
        {
            var entity = _mapper.Map<Entities.Task>(dto);

            entity.UserId = userId;
            entity.CreatedAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;

            await _taskRepository.InsertAsync(entity);

            return _mapper.Map<TaskDTO>(entity);
        }

        public async Task<TaskDTO> UpdateAsync(UpdateTaskDTO dto, Guid id, Guid userId)
        {
            var task = _taskRepository.FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException($"Task with id not found. Id: {id}");

            if (task.UserId != userId)
            {
                throw new InvalidCredentialsException("You are not the owner and do not have permission to perform this action.");
            }

            task = _mapper.Map(dto, task);
            task.UpdateAt = DateTime.Now;

            await _taskRepository.UpdateAsync(task);

            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid id)
        {
            var task = _taskRepository.FirstOrDefault(x => x.Id == id)
                ?? throw new NotFoundException($"Task with id not found. Id: {id}");

            if (task.UserId != userId)
            {
                throw new InvalidCredentialsException("You are not the owner and do not have permission to perform this action.");
            }

            var result = await _taskRepository.DeleteAsync(task);

            return result;
        }

        public async Task<TaskDTO> GetAsync(Guid id)
        {
            var task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"Task with id not found. Id: {id}");

            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<PageList<TaskDTO>> GetAllAsync(GetTasksRequest request)
        {
            var query = GetTasksQuery(request);

            var tasks = await query.ToListAsync();

            var taskDTO = _mapper.Map<List<TaskDTO>>(tasks);

            var paginationTasks = taskDTO.Pagination(request.Page, request.PageSize);

            return paginationTasks;
        }

        private IQueryable<Entities.Task> GetTasksQuery(GetTasksRequest request)
        {
            var query = _taskRepository.AsQueryable();

            if (request.Status != null)
            {
                query = query.Where(x => x.Status == request.Status);
            }

            if (request.Priority != null)
            {
                query = query.Where(x => x.Priority == request.Priority);
            }

            if (request.DueDate != null)
            {
                query = query.Where(x => x.DueDate == request.DueDate);
            }

            query = request.Sorting switch
            {
                Sorting.Priority => query.OrderBy(x => x.Priority),
                Sorting.DueDate => query.OrderByDescending(x => x.DueDate),
                _ => query
            };

            return query;
        }
    }
}
