namespace CodeProject.StoreDB.DataService.Logger
{
    public static class Log4NetLogger
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}