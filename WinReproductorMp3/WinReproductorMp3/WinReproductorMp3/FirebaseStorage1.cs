using System;
using System.Threading.Tasks;

namespace WinReproductorMp3
{
    internal class FirebaseStorage
    {
        public object Options { get; internal set; }
        public object StorageBucket { get; internal set; }

        internal Task<IDisposable> CreateHttpClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}