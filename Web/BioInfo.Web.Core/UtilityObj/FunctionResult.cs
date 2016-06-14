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
        private bool didFail;
        public FunctionResult(T result, string friendlyError = null, bool didFail = false)
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
        public bool DidFail()
        {
            return didFail;
        }
    }
}
