using System;
using System.Runtime.Serialization;

namespace WinReproductorMp3
{
    [Serializable]
    internal class FirebaseStorageException : Exception
    {
        private string url;
        private string resultContent;
        private Exception ex;

        public FirebaseStorageException()
        {
        }

        public FirebaseStorageException(string message) : base(message)
        {
        }

        public FirebaseStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FirebaseStorageException(string url, string resultContent, Exception ex)
        {
            this.url = url;
            this.resultContent = resultContent;
            this.ex = ex;
        }

        protected FirebaseStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}