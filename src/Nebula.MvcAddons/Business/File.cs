using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nebula.MvcAddons.Business {
    public class File {

        private string _basePath;

        public string Id { get; set; }
        public string BasePath {
            get {
                return this._basePath;
            }
            private set {
                if (string.IsNullOrEmpty(value))
                    value = "";

                value = value.Trim();

                if (!value.EndsWith("/"))
                    value = value + "/";

                if (value.StartsWith("/"))
                    value = value.TrimStart('/');

                this._basePath = value;
            }
        }

        public string[] AcceptibleExtensions { get; set; }

        public File(string basePath, string[] acceptibleExtensions) {

            this.BasePath = basePath;
            this.AcceptibleExtensions = acceptibleExtensions;
        }

        public File(int id, string basePath, string[] acceptibleExtensions) {

            this.Id = id.ToString();
            this.BasePath = basePath;
            this.AcceptibleExtensions = acceptibleExtensions;
        }

        public string GetFilePath(string filename) {

            if (string.IsNullOrEmpty(filename))
                filename = "";

            filename = filename.Trim();

            if (string.IsNullOrEmpty(filename))
                throw new Exception("Filename cannot be blank.");

            return string.Format("{0}{1}{2}", this.BasePath, string.IsNullOrEmpty(this.Id) ? "" : this.Id + "_", filename);
        }

        public bool FileIsAcceptible(string filename) {

            string fileExtension;

            if (string.IsNullOrEmpty(filename))
                filename = "";

            filename = filename.Trim().ToLower();

            if (string.IsNullOrEmpty(filename))
                throw new Exception("Filename cannot be blank.");

            fileExtension = Path.GetExtension(filename).Replace(".", "");

            return this.AcceptibleExtensions.Contains(fileExtension);
        }
    }
}
