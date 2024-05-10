using System.Data.Common;

namespace Core.Domain.Data
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }

        DbConnection GetDbConnection();
    }
}