using Bulky.DataAcess.Data;
using Bulky.DataAcess.Repositoryy.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAcess.Repositoryy
{
    public class UnityOfWork : IUnityOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public UnityOfWork(ApplicationDbContext db )
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

       

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
