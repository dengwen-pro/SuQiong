using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuQiong.Domain.Service
{
    public interface IBaseService<T> where T : class, new()
    {
        Task<int> Insert(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(Expression<Func<T, bool>> whereLambda);

        Task<bool> IsExist(Expression<Func<T, bool>> whereLambda);

        Task<T> GetEntity(Expression<Func<T, bool>> whereLambda);

        Task<List<T>> Select();

        Task<List<T>> Select(Expression<Func<T, bool>> whereLambda);

        Task<Tuple<List<T>, int>> Select<S>(int pageSize, int pageIndex, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc);
    }
}
