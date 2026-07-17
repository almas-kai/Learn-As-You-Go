using Ardalis.Specification;

namespace Application.Abstractions.Data;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
