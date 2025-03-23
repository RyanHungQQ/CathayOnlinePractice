using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Interfaces.Repositories
{
    /// <summary>
    /// 操作資料表介面
    /// </summary>
    public interface IRepository
    {

    }

    /// <summary>
    /// 泛型操作資料表介面：僅提供單一Entity做共用性的CRUD操作
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IRepository where TEntity : BaseEntity, new()
    {
        #region === Insert ===
        /// <summary>
        /// Inserts the specified entity.
        /// 新增Entity資料.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertByDTO<TInputDTO>(TInputDTO dto, bool saveAndReturnKey = false);
        #endregion

        #region === Update ===

        /// <summary>
        /// Updates the specified entity.
        /// 更新Entity資料.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);
        public void UpdateRange(IEnumerable<TEntity> entities);
        void UpdateByDTO<TInputDto>(TInputDto dto);

        #endregion

        #region === Delete ===

        /// <summary>
        /// Deletes the specified entity.
        /// 刪除Entity資料.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified expression.
        /// 刪除"符合自訂Expression範圍"的Entity資料.
        /// </summary>
        /// <param name="expression">The expression. 自訂查詢 Expression</param>
        void Delete(Expression<Func<TEntity, bool>> expression);

        #endregion

        #region === GetSingle ===

        /// <summary>
        /// 取得單筆Entity資料(如果查詢結果為多筆，則回傳 FirstOrDefault() 的查詢結果)
        /// </summary>
        /// <param name="expression">The expression. 自訂查詢 Expression</param>
        /// <returns></returns>
        TEntity GetSingle(Expression<Func<TEntity, bool>> expression);

        #endregion

        #region === GetAll ===

        /// <summary>
        /// Gets all.
        /// 取得全部Entity資料.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all.
        /// 取得"符合自訂Expression範圍"的全部Entity資料.
        /// </summary>
        /// <param name="expression">The expression. 自訂查詢 Expression.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);

        #endregion

        #region === GetQueryable ===

        /// <summary>
        /// Gets the queryable.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();
        /// <summary>
        /// Gets the queryable.
        /// </summary>
        /// <param name="expression">The expression. 自訂查詢 Expression.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression);

        #endregion
    }
}
