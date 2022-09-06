using Atom.Data.BaseClass;
using Atom.Data.Criteria;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Atom.Data.Manager
{
    public interface IManagerBase
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProc, U parameters);
        Task SaveData<T>(string storedProc, T parameters);
        IEnumerable<TModel> GetManyModels<TModel, TCriteria>(TCriteria criteria, CancellationToken cancellationToken = new CancellationToken()) where TModel : ModelBase where TCriteria : CriteriaBase;
    }
}
