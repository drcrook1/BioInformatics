using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.App.Library.Common.Base
{
    // program to the interface in shared code
    public interface IDispatchOnUIThread
    {
        Task Invoke(Action action);
    }
    //UAP 
   // IDispatchOnUIThread

    // iOS
    //public class DispatchAdapter : IDispatchOnUIThread
    //{
    //    public readonly NSObject owner;
    //    public DispatchAdapter(NSObject owner)
    //    {
    //        this.owner = owner;
    //    }
    //    public void Invoke(Action action)
    //    {
    //        owner.BeginInvokeOnMainThread(new NSAction(action));
    //    }
    //}
    //// Android
    //public class DispatchAdapter : IDispatchOnUIThread
    //{
    //    public readonly Activity owner;
    //    public DispatchAdapter(Activity owner)
    //    {
    //        this.owner = owner;
    //    }
    //    public void Invoke(Action action)
    //    {
    //        owner.RunOnUiThread(action);
    //    }
    //}
    //// WP7
    //public class DispatchAdapter : IDispatchOnUIThread
    //{
    //    public void Invoke(Action action)
    //    {
    //        Deployment.Current.Dispatcher.BeginInvoke(action);
    //    }
    //}
}
