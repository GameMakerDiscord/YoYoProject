using System;
using System.Collections.Generic;
using YoYoProject.Models;

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
            return new GMProjectParentModel
            {
                hiddenResources = HiddenResources,
                //alteredResources = AlteredResources.Serialize(),
                projectPath = Reference?.RootDirectory
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
    }
}