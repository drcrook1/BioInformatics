using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.Core.Interfaces
{
    public class FunctionResult<T>
    {
        private T result;
        private string err;
        public FunctionResult(T result, string friendlyError = null)
        {
            this.result = result;
            this.err = friendlyError;
        }
        public T GetResult()
        {
            return result;
        }
        public string GetFriendlyError()
        {
            return err;
        }
    }
}
