using Ardalis.Specification;

namespace ICaNet.ApplicationCore.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
