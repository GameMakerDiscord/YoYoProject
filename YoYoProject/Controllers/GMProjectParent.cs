using System;
using System.Collections.Generic;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMProjectParent : ControllerBase
    {
        internal override ModelBase Serialize()
        {
            return new GMProjectParentModel
            {
                hiddenResources = new List<Guid>(),
                alteredResources = new Dictionary<Guid, GMResourceInfoModel>(),
                projectPath = "${base_project}"
            };
        }
    }
}