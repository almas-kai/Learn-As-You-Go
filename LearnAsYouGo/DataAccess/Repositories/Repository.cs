using Application.Abstractions.Data;
using Ardalis.Specification.EntityFrameworkCore;
using DataAccess.Contexts;

namespace DataAccess.Repositories;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public Repository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
