using System;
using System.IO;
using YoYoProject.Common;
using YoYoProject.Models;
using YoYoProject.Utility;

namespace YoYoProject.Controllers
{
    public sealed class GMIncludedFile : GMResource
    {
        private TargetPlatforms platforms;
        public TargetPlatforms Platforms
        {
            get { return GetProperty(platforms); }
            set { SetProperty(value, ref platforms); }
        }

        private string filePath;
        private string FilePath
        {
            get { return GetProperty(filePath); }
            set { SetProperty(value, ref filePath); }
        }

        private string fileName;
        private string FileName
        {
            get { return GetProperty(fileName); }
            set { SetProperty(value, ref fileName); }
        }

        private string pendingFilePath;

        internal override string ResourcePath => $@"datafiles_yy\{Name}.yy";

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "includefile");
            Platforms = TargetPlatforms.AllPlatforms;
            FilePath = "datafiles";
            FileName = null;
        }

        public void SetFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            Name = Path.GetFileName(path);
            FilePath = "datafiles";
            FileName = Name;

            pendingFilePath = path;
        }

        public FileStream GetFileStream()
        {
            string relativePath;
            if (pendingFilePath != null)
                relativePath = pendingFilePath;
            else if (FilePath != null && FileName != null)
                relativePath = $@"{FilePath}\{FileName}";
            else
                return null;

            string fullFilePath = Path.Combine(Project.RootDirectory, relativePath);
            return File.Exists(fullFilePath) ? File.OpenRead(fullFilePath) : null;
        }

        internal override ModelBase Serialize()
        {
            if (pendingFilePath != null)
            {
                string destPath = Path.Combine(Project.RootDirectory, $@"{FilePath}\{FileName}");
                FileSystem.EnsureDirectory(Path.GetDirectoryName(destPath));
                File.Copy(pendingFilePath, destPath, true);
                pendingFilePath = null;
            }

            return new GMIncludedFileModel
            {
                id = Id,
                name = Name,
                origName = "",
                exists = false,
                size = 0,
                exportAction = 0,
                exportDir = "",
                overwrite = false,
                freeData = false,
                removeEnd = false,
                store = false,
                CopyToMask = Platforms,
                tags = "",
                fileName = FileName,
                filePath = FilePath
            };
        }
    }
}
