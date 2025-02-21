using Ardalis.Specification;

namespace ICaNet.ApplicationCore.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
