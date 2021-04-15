using System.Threading.Tasks;

namespace WebESchoolData.Repository
{
    public interface IUnitOfWork
    {
        IRepositoryBase<T> Repository<T>() where T : class;
        Task<int> Commit();
        void Rollback();
    }
}
