using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BioInfo.App.Library.Common.Base
{
    public class DispatcherViewModelBase : ViewModelBase
    {
      //  public CoreDispatcher Dispatcher { get; set; }
        public DispatcherViewModelBase()
        {
   //         Dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
        }
        protected void SetWithDispatcher<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
         
               Set<T>(propertyExpression, ref field, newValue);
           
        }
    }
}
