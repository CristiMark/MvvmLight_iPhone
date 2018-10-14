using System;

namespace MvvmLight_iPhone.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}