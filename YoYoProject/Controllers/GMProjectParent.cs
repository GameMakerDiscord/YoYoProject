using System;
using System.Collections.Generic;
using YoYoProject.Models;
using YoYoProject.Utility;

namespace YoYoProject.Controllers
{
    public sealed class GMProjectParent : ControllerBase
    {
        public GMProject Reference { get; private set; }

        internal List<Guid> HiddenResources { get; private set; }

        //internal GMResourceManager AlteredResources { get; }

        internal GMProjectParent(GMProject project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            Reference = null;
            HiddenResources = new List<Guid>();
            //AlteredResources = new GMResourceManager(project);
        }

        internal override ModelBase Serialize()
        {
            // HACK Definitely a nasty hack, would be better to store the unexpanded macro in RootDirectory
            //      and expand it upon demand instead...
            var projectPath = Reference?.RootDirectory;
            if (projectPath == Macros.Expand("${base_project}"))
                projectPath = "${base_project}";

            return new GMProjectParentModel
            {
                hiddenResources = HiddenResources,
                //alteredResources = AlteredResources.Serialize(),
                alteredResources = new SortedDictionary<Guid, GMResourceInfoModel>(),
                projectPath = projectPath
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var parentProjectModel = (GMProjectParentModel)model;

            HiddenResources = parentProjectModel.hiddenResources;
            //AlteredResources.Deserialize(parentProjectModel.alteredResources);

            if (!string.IsNullOrEmpty(parentProjectModel.projectPath))
                Reference = GMProject.Load(parentProjectModel.projectPath);
        }

        public void SetAsBaseProject()
        {
            Reference = GMProject.Load("${base_project}");
        }
    }
}