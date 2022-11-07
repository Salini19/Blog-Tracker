using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private MyContext db;


        DbSet<T> dbset;

        public Repository()
        {
            db = new MyContext();
            dbset = db.Set<T>();
        }
        public void Delete(object id)
        {
            //throw new NotImplementedException();
            T obj = dbset.Find(id);
            dbset.Remove(obj);
        }

        public IEnumerable<T> GetAll()
        {
            //throw new NotImplementedException();
            return dbset.ToList();
        }

        public T GetByID(object id)
        {
            //throw new NotImplementedException();
            T obj = dbset.Find(id);
            return obj;
        }

        public void Insert(T obj)
        {
            //throw new NotImplementedException();
            dbset.Add(obj);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            //throw new NotImplementedException();
            db.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}
