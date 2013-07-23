
namespace BankNet.Core.Provider
{
    public class CacheProvider
    {
        static ICacheProvider _instance;
        static ICacheProvider Instance
        {
            get { return _instance ?? (_instance = new MemcacheProvider()); }
        }

        private static string sRoot = "PayGate_";

        public static object Get(string key)
        {
            return Instance.Get(sRoot + key);
        }

        public static void Add(string key, object value)
        {
            Instance.Add(sRoot + key, value);
        }

        public static void AddWithTimeOut(string key, object value, int timeout)
        {
            Instance.AddWithTimeOut(sRoot + key, value, timeout);
        }

        public static void Update(string key, object value)
        {
            Instance.Update(sRoot + key, value);
        }

        public static void UpdateWithTimeOut(string key, object value, int timeout)
        {
            Instance.UpdateWithTimeOut(sRoot + key, value, timeout);
        }

        public static void Remove(string key)
        {
            Instance.Remove(sRoot + key);
        }

        public static void FlusAll()
        {
            Instance.FlusAll();
        }
    }
}