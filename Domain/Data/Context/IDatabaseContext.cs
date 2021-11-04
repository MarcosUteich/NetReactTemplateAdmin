using Domain.Data.ContextRepository;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Data.Context
{
    public interface IDatabaseContext
    {
        string ConnectionString { get; }
        IQueryable<T> Table<T>(string collectionName);
        Task CreateTable(string name, string collation);
        Task DeleteTable(string name);
        Task<bool> CollectionExists(string name);
        Task CreateIndex<T>(IRepository<T> repository, OrderBuilder<T> orderBuilder, string indexName, bool unique = false) where T : BaseEntity;
        Task DeleteIndex<T>(IRepository<T> repository, string indexName) where T : BaseEntity;
    }
}
