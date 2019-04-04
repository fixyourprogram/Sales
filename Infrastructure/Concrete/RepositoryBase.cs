using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helpers;
using System.Linq.Expressions;
using Domain.Entities;
using System.Transactions;

namespace Infrastructure.Concrete
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public SalesEntities1 Context { get; set; }

        public RepositoryBase()
        {
            Context = new SalesEntities1();
            this.Context.Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                using (var nolock = Nolock())
                {
                    var data = this.Context.Set<T>().AsNoTracking();
                    nolock.Complete();
                    return data;
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
        }

        public void Save(T entity)
        {

            try
            {
                if (entity != null)
                {
                    this.Context.Set<T>().Add(entity);
                    this.Context.SaveChanges();


                }
            }

            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                //SaveLogError(entity, new Exception(ex.Message));
                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {

                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);

                throw new Exception(sb.ToString());

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);
                throw new Exception(sb.ToString());
            }
        }

        public void Save(IEnumerable<T> entity)
        {
            try
            {
                if (entity != null)
                {
                    this.Context.Set<T>().AddRange(entity);
                    this.Context.SaveChanges();


                }
            }

            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                //SaveLogError(entity, new Exception(ex.Message));
                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {

                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);

                throw new Exception(sb.ToString());

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);
                throw new Exception(sb.ToString());
            }
        }

        public void Update(T entity, string id)
        {
            try
            {
                var trackedEntity = this.Context.Set<T>().Find(id);
                this.Context.Entry(trackedEntity).CurrentValues.SetValues(entity);

                this.Context.Entry(trackedEntity).State = EntityState.Modified;
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {

                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);

                throw new Exception(sb.ToString());
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);
                throw new Exception(sb.ToString());
            }
        }

        public void Update(T entity, long id)
        {
            try
            {
                var trackedEntity = this.Context.Set<T>().Find(id);
                this.Context.Entry(trackedEntity).CurrentValues.SetValues(entity);

                this.Context.Entry(trackedEntity).State = EntityState.Modified;
                this.Context.SaveChanges();
            }

            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {

                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);

                throw new Exception(sb.ToString());

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);
                throw new Exception(sb.ToString());
            }
        }

        public void Delete(long id)
        {
            try
            {
                var entity = this.Context.Set<T>().Find(id);
                this.Context.Set<T>().Remove(entity);
                this.Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                throw new DbEntityValidationException(sb.ToString(), ex);
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);

                throw new Exception(sb.ToString());

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);
                throw new Exception(sb.ToString());
            }
        }

        public void Delete(long keyId, long secondKeyId)
        {
            try
            {
                var entity = this.Context.Set<T>().Find(keyId, secondKeyId);
                this.Context.Set<T>().Remove(entity);
                this.Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                throw new DbEntityValidationException(sb.ToString(), ex);
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);

                throw new Exception(sb.ToString());

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("{0} : {1}", ExceptionHelper.GetAllInnerException(ex), ex.Message);
                throw new Exception(sb.ToString());
            }
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    var data = this.Context.Set<T>().Where(filter).FirstOrDefault();
                    nolock.Complete();
                    return data;
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (Expression<Func<T, object>> include in includes)
                        query = query.Include(include);


                    if (filter != null)
                        query = query.Where(filter);



                    return query.AsNoTracking().FirstOrDefault();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
        }

        public IQueryable<T> GetAllQueryable(System.Linq.Expressions.Expression<Func<T, bool>> filter, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;
                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (Expression<Func<T, object>> include in includes)
                        query = query.Include(include);

                    if (filter != null)
                        query = query.Where(filter);
                    return query;
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IQueryable<T> GetAllQueryable(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    return this.Context.Set<T>().Where(filter);
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
        }

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;
                    return this.Context.Set<T>().Where(filter).AsNoTracking().ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter, int skip, int take, System.Linq.Expressions.Expression<Func<T, long>> ordering, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;
                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (Expression<Func<T, object>> include in includes)
                        query = query.Include(include);


                    if (filter != null)
                        query = query.Where(filter);

                    return query.AsNoTracking().OrderByDescending(ordering).Skip(skip).Take(take).ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter, int skip, int take, System.Linq.Expressions.Expression<Func<T, DateTime?>> ordering, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;
                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (Expression<Func<T, object>> include in includes)
                        query = query.Include(include);


                    if (filter != null)
                        query = query.Where(filter);

                    return query.AsNoTracking().OrderBy(ordering).Skip(skip).Take(take).ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter, int skip, int take, System.Linq.Expressions.Expression<Func<T, long>> ordering, params string[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;

                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (string include in includes)
                        query = query.Include(include);

                    if (filter != null)
                        query = query.Where(filter);

                    return query.OrderBy(ordering).Skip(skip).Take(take).ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;
                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (Expression<Func<T, object>> include in includes)
                        query = query.Include(include);


                    if (filter != null)
                        query = query.Where(filter);

                    return query.ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IList<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter, params string[] includes)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = false;

                    IQueryable<T> query = this.Context.Set<T>();
                    foreach (string include in includes)
                        query = query.Include(include);


                    if (filter != null)
                        query = query.Where(filter);

                    return query.ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public IList<T> GetAllNotLazy(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            try
            {
                using (var nolock = Nolock())
                {
                    this.Context.Configuration.LazyLoadingEnabled = true;
                    IQueryable<T> query = this.Context.Set<T>();

                    if (filter != null)
                        query = query.Where(filter);

                    return query.AsNoTracking().ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {

                var sb = new StringBuilder();


                foreach (var item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    // sb.AppendFormat("{0} \n", failure.Entry.Entity.GetType());
                    foreach (var error in item.ValidationErrors)
                    {
                        sb.AppendFormat("{0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }


                throw new DbEntityValidationException(
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                foreach (var item in ex.Entries)
                {

                    DbEntityEntry entry = item;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                sb.AppendFormat("{0} : {1}", ex.InnerException, ex.Message);

                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHelper.GetAllInnerException(ex));
            }

        }

        public static TransactionScope Nolock()
        {
            var options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadUncommitted;
            return new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.Required, options);
        }
    }
}
