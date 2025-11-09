namespace Tower.Runtime.Core
{   
    public interface ITableData 
    {
        void LoadData();
    }

    public abstract class TableDataBase : ITableData
    {
        public abstract void LoadData();
    }
}
