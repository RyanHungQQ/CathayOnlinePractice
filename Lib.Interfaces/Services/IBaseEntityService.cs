using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Interfaces.Services
{
    public interface IBaseEntityService<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// 取得全部.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <returns></returns>
        IList<TDto> GetAll<TDto>();
        /// <summary>
        /// 取得符合條件的資料.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IList<TDto> GetAll<TDto>(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 取得單筆資料(如果查詢結果為多筆，則回傳 FirstOrDefault() 的查詢結果).
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns name="TDto">單筆資料查詢結果</returns>
        TDto GetSingle<TDto>(Expression<Func<TEntity, bool>> expression) where TDto : class;
        /// <summary>
        /// 取得符合條件的資料筆數
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int GetCount(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 新增單筆資料至資料庫的指定 Table
        /// </summary>
        /// <typeparam name="TInputDto">The type of the input dto.</typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        void Insert<TInputDto>(TInputDto input) where TInputDto : class;
        /// <summary>
        /// 更新資料庫指定 Table的單筆資料
        /// </summary>
        /// <typeparam name="TInputDto">The type of the input dto.</typeparam>
        /// <param name="input">The input. 預備進行更新之資料</param>
        /// <returns></returns>
        void Update<TInputDto>(TInputDto input) where TInputDto : class;
        /// <summary>
        /// 刪除資料庫指定 Table的單筆資料.
        /// </summary>
        /// <param name="expression">The expression. 開發者自訂查詢 Expression</param>
        /// <returns></returns>
        void Delete(Expression<Func<TEntity, bool>> expression);
    }
}
