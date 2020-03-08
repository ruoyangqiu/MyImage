using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyImage.Data
{
    public class ImageMock
    {
        #region Singleton
        private static volatile ImageMock instance;
        private static readonly object syncRoot = new Object();

        public static ImageMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ImageMock();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        public string url { get; set; }

        public void add(string u)
        {
            url = u;
        }
    }
}
