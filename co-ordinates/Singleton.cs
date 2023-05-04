using System.Security.Cryptography.X509Certificates;

namespace co_ordinates
{
    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly object padlock = new object();
        public int totalcount;
        public int count;
        public string countstr;
        Singleton()
        {
            
        }

        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
    }
}
