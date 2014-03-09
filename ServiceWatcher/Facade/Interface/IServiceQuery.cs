using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWatcher.Facade.Interface
{
    public interface IQueryFor<in TInput,out TOutput>
    {
        TOutput ExecuteQueryWith(TInput input);
    }
}
