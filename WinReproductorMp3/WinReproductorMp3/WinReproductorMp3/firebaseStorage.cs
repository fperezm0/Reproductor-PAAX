using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinReproductorMp3
{
    class FirebaseStorageReference
    {
        private const string FirebaseStorageEndpoint = "https://firebasestorage.googleapis.com/v0/b/";

        private readonly FirebaseStorage storage;
        private readonly List<string> children;

        internal FirebaseStorageReference(FirebaseStorage storage, string childRoot)
        {
            this.children = new List<string>();

            this.storage = storage;
            this.children.Add(childRoot);
        }

        /// <summary>
        /// Starts uploading given stream to target location.
        /// </summary>
        /// <param name="stream"> Stream to upload. </param>
        /// <param name="cancellationToken"> Cancellation token which can be used to cancel the operation. </param>
        /// <returns> <see cref="FirebaseStorageTask"/> which can be used to track the progress of the upload. 
       

        /// <summary>
        /// Starts uploading given stream to target location.
        /// </summary>
        /// <param name="stream"> Stream to upload. </param>
        /// <returns> <see cref="FirebaseStorageTask"/> which can be used to track the progress of the upload. 
        public FirebaseStorageTask PutAsync(Stream fileStream)
        {
            return this.PutAsync(fileStream);
        }

        /// <summary>
        /// Gets the url to download given file.
        /// </summary>
        

        /// <summary>
        /// Deletes a file at target location.
        /// </summary>
        public async Task DeleteAsync()
        {
            var url = this.GetDownloadUrl();
            var resultContent = "N/A";

           
        }

        /// <summary>
        /// Constructs firebase path to the file.
        /// </summary>
        /// <param name="name"> Name of the entity. This can be folder or a file name or full path.</param>
        /// <example>
        ///     storage
        ///         .Child("some")
        ///         .Child("path")
        ///         .Child("to/file.mp3");
        /// </example>
        /// <returns> <see cref="FirebaseStorageReference"/> for fluid syntax. 
        public FirebaseStorageReference Child(string name)
        {
            this.children.Add(name);
            return this;
        }

        private string GetTargetUrl()
        {
            return $"{FirebaseStorageEndpoint}{this.storage.StorageBucket}/o?name={this.GetEscapedPath()}";
        }

        private string GetDownloadUrl()
        {
            return $"{FirebaseStorageEndpoint}{this.storage.StorageBucket}/o/{this.GetEscapedPath()}";
        }

        private string GetFullDownloadUrl()
        {
            return this.GetDownloadUrl() + "?alt=media&token=";
        }

        private string GetEscapedPath()
        {
            return Uri.EscapeDataString(string.Join("/", this.children));
        }
    }
}

