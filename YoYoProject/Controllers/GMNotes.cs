using System.IO;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMNotes : GMResource
    {
        private string contents;
        public string Contents
        {
            get
            {
                if (contents != null)
                {
                    if (File.Exists(ContentsFullPath))
                        contents = File.ReadAllText(ContentsFullPath);
                    else
                        contents = "";
                }

                return contents;
            }

            set
            {
                if (value == contents)
                    return;

                contents = value ?? "";
                Dirty = true;
            }
        }

        private string ContentsFullPath => Path.Combine(Project.RootDirectory, $@"notes\{Name}.txt");

        protected internal override string ResourcePath => $@"notes\{Name}.yy";

        protected internal override void Create()
        {
            Name = Project.Resources.GenerateValidName("note");
        }

        internal override ModelBase Serialize()
        {
            // TODO Unload OnSaveComplete
            if (contents != null)
                File.WriteAllText(ContentsFullPath, contents);

            return new GMNotesModel
            {
                id = Id,
                name = Name
            };
        }
    }
}
