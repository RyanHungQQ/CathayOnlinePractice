using AutoMapper;
using Lib.Interfaces.Repositories;
using Lib.Interfaces.Services;
using Models.Entities;
using System.Linq.Expressions;

namespace Lib.Services
{
    public class BaseEntityService<TEntity> : IBaseEntityService<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IMapper _mapper;
        protected readonly IDbEntities DbEntities;
        protected readonly IRepository<TEntity> Repository;

        public BaseEntityService(
            IMapper mapper,
            IDbEntities dbEntities,
            IRepository<TEntity> repository)
        {
            _mapper = mapper;
            DbEntities = dbEntities;
            Repository = repository;
        }
        public virtual IList<TDto> GetAll<TDto>()
        {
            var dbData = Repository.GetAll().ToList();
            var output = _mapper.Map<List<TEntity>, List<TDto>>(dbData);
            return output;
        }
        public virtual IList<TDto> GetAll<TDto>(Expression<Func<TEntity, bool>> expression)
        {
            var dbData = Repository.GetAll(expression).ToList();
            var output = _mapper.Map<List<TEntity>, List<TDto>>(dbData);
            return output;
        }
        public virtual TDto GetSingle<TDto>(Expression<Func<TEntity, bool>> expression) where TDto : class
        {
            var dbData = Repository.GetSingle(expression);
            var output = _mapper.Map<TEntity, TDto>(dbData);
            return output;
        }
        public int GetCount(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.GetQueryable(expression).Count();
        }

        public virtual void Insert<TInputDto>(TInputDto input) where TInputDto : class
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Repository.InsertByDTO(input);
            DbEntities.SaveChanges();
        }

        public virtual void Update<TInputDto>(TInputDto input) where TInputDto : class
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            Repository.UpdateByDTO(input);
            DbEntities.SaveChanges();
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> expression)
        {
            Repository.Delete(expression);
            DbEntities.SaveChanges();
        }
    }
}
