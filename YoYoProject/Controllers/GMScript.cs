using System.IO;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMScript : GMResource
    {
        private bool isCompatibility;
        public bool IsCompatibility
        {
            get { return GetProperty(isCompatibility); }
            set { SetProperty(value, ref isCompatibility); }
        }

        private bool isDnD;
        public bool IsDnD
        {
            get { return GetProperty(isDnD); }
            set { SetProperty(value, ref isDnD); }
        }

        private string contents;
        public string Contents
        {
            get
            {
                if (contents == null)
                {
                    if (File.Exists(ScriptFullPath))
                        contents = File.ReadAllText(ScriptFullPath);
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

        internal override string ResourcePath => $@"scripts\{Name}\{Name}.yy";

        private string ScriptFullPath => Path.Combine(Project.RootDirectory, $@"scripts\{Name}\{Name}.gml");

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "script");
            IsCompatibility = false;
            IsDnD = Project.DragAndDrop;
            Contents = "";

            AddResourceToFolder("GMScript");
        }

        internal override ModelBase Serialize()
        {
            // TODO Unload OnSaveComplete
            if (contents != null)
                File.WriteAllText(ScriptFullPath, contents);
            
            return new GMScriptModel
            {
                id = Id,
                name = Name,
                IsCompatibility = IsCompatibility,
                IsDnD = IsDnD
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var scriptModel = (GMScriptModel)model;

            Id = scriptModel.id;
            Name = scriptModel.name;
            IsCompatibility = scriptModel.IsCompatibility;
            IsDnD = scriptModel.IsDnD;
        }
    }
}
