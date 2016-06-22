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
            this.didFail = didFail;
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

    public static class FunctionResult
    {

        public static FunctionResult<bool> Fail(string friendlyError)
        {
            return new FunctionResult<bool>(false, friendlyError, true);
        }

        public static FunctionResult<T> Success<T>(T result)
        {
            return new FunctionResult<T>(result);

        }

        public static FunctionResult<bool> Success()
        {
            return new FunctionResult<bool>(true);
        }
    }
}
