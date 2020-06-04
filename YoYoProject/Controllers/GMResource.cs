using System;
using System.Linq;

namespace YoYoProject.Controllers
{
    public abstract class GMResource : ControllerBase
    {
        public string Name { get; set; }
        
        internal abstract string ResourcePath { get; }
        
        internal Guid ResourceInfoId { get; set; }

        internal abstract void Create(string name);

        public void AddResourceToFolder(string filtername)
        {
            var fld = Project.Resources.GetAllOfType<GMFolder>().Where(n => n.FilterType == filtername);
            fld.FirstOrDefault().Children.Add(this);
        }
    }
}