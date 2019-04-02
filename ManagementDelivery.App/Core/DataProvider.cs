namespace ManagementDelivery.App.Core
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Ins
        {
            get
            {
                if (_instance == null)
                    _instance = new DataProvider();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public StockManagementDB DB { get; set; }

        private DataProvider()
        {
            DB = new StockManagementDB();
        }
    }
}
