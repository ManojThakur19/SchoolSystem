using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebESchoolData.DataContext;

namespace WebESchoolData.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebSchoolDataContext Context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }
        public UnitOfWork(WebSchoolDataContext Context)
        {
            this.Context = Context;
        }
        public IRepositoryBase<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IRepositoryBase<T>;
            }
            IRepositoryBase<T> repo = new RepositoryBase<T>(Context);
            Repositories.Add(typeof(T), repo);
            return repo;
        }
        public async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }
        public void Rollback()
        {
            Context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
