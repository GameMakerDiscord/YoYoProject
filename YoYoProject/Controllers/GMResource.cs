using System;

namespace YoYoProject.Controllers
{
    public abstract class GMResource : ControllerBase
    {
        public string Name { get; set; }
        
        internal abstract string ResourcePath { get; }
        
        internal Guid ResourceInfoId { get; set; }

        internal abstract void Create(string name);
    }
}