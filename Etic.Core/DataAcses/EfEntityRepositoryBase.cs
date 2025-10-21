using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Core.DataAcses
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEfEntityRepositoryBase<TEntity>
         where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context=new TContext())
            {
                var result = context.Entry(entity);
                result.State= EntityState.Added;
                context.SaveChanges();
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            using (var context=new TContext())
            {
                return context.Set<TEntity>().Any(filter);
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var result = context.Entry(entity);
                result.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context=new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context=new TContext())
            {
                if (filter==null)
                {
                    return context.Set<TEntity>().ToList();
                }
                else
                {
                    return context.Set<TEntity>().Where(filter).ToList();
                }
            }
        }

        public void Update(TEntity entity)
        {
            using (var context=new TContext())
            {
                var result = context.Entry(entity);
                result.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
