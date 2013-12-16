using System;

namespace SleepHack.DataStorage
{
    public interface IDataService
    {
        Type DataType { get; set; }
        bool SaveData(object data);
        object LoadData();
    }

}