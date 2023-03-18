using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;

namespace GlobalTaskTaskManagement.Infra.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        readonly TaskManagementContext _context;
        readonly IDbConnection _connection;

        const string insertSql = @"
            INSERT INTO Task
            (Id, Status, Description, Date, Active)
            VALUES
            (@Id, @Status, @Description, @Date, @Active)
        ";

        const string updateSql = @"
           Update Task
            Set  
                Status = @Status, 
                Description = @Description, 
                Date = @Date,
                Active = @Active    
            Where Id = @Id
        ";

        const string querySql = @"
            Select 
                Id, Status, Description, Date, Active
            From Task
            Where Active = 1
        ";

        const string queryByIdSql = @"
            Select 
                Id, Status, Description, Date, Active
            From Task
            Where Id = @id
        ";

        public TaskRepository(TaskManagementContext context)
        {
            _context = context;
            _connection = _context.Database.GetDbConnection();
        }

        public async Task AddAsync(TaskItem task)
        {
            await _connection.ExecuteAsync(insertSql, task);
        }

        public async Task<IEnumerable<TaskItem>> GetAsync()
        {
            return await _connection.QueryAsync<TaskItem>(querySql);
        }

        public async Task<TaskItem> GetAsync(Guid id)
        {
            return await _connection.QueryFirstAsync<TaskItem>(queryByIdSql, new { id });
        }

        public async Task UpdateAsync(TaskItem task)
        {
           await _connection.ExecuteAsync(updateSql, task);
        }
    }
}
