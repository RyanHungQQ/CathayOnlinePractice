using AutoMapper;
using Lib.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using NLog;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly IDbEntities Db;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private DbSet<TEntity> DbSet { get; set; }

        public GenericRepository(IDbEntities db, IMapper mapper)
        {
            Db = db;
            _mapper = mapper;
            DbSet = Db.Set<TEntity>();
            _logger = LogManager.GetCurrentClassLogger();
        }


        #region === Insert ===
        public void Insert(TEntity dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            DbSet.Add(dto);
        }
        public void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// Dto Mapping 以新增 Entity
        /// </summary>
        /// <typeparam name="TInputDTO"></typeparam>
        /// <param name="dto"></param>
        /// <param name="saveAndReturnKey"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void InsertByDTO<TInputDTO>(TInputDTO dto, bool saveAndReturnKey = false)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var entity = _mapper.Map<TEntity>(dto);
            Insert(entity);

            if (!saveAndReturnKey)
                return;

            Db.SaveChanges();
            var keys = Db.Entry(entity).Metadata.FindPrimaryKey().Properties.Select(x => x.Name).ToList();
            foreach (var key in keys)
            {
                var entityProp = entity.GetType().GetProperty(key);
                var dtoProperty = typeof(TInputDTO).GetProperty(key);
                if (dtoProperty == null)
                    continue;

                dtoProperty.SetValue(dto, entityProp.GetValue(entity));
            }
        }

        #endregion

        #region === Update ===
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            DbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            DbSet.UpdateRange(entities);
        }
        public void UpdateByDTO<TInputDto>(TInputDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var entity = _mapper.Map<TEntity>(dto);
            Update(entity);
        }

        #endregion

        #region === Delete ===

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Db.Entry(entity).State = EntityState.Unchanged;
            Db.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            DbSet.RemoveRange(DbSet.Where(expression));
        }

        #endregion

        #region === GetSingle ===

        public TEntity GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.AsNoTracking().FirstOrDefault(expression);
        }

        #endregion

        #region === GetAll ===

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                var data = DbSet.ToList();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var data = DbSet.Where(expression).AsNoTracking().ToList();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                throw;
            }
        }

        #endregion

        #region === GetQueryable ===
        public IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }
        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return DbSet.Where(expression).AsNoTracking();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                throw;
            }

        }

        #endregion
    }
}
