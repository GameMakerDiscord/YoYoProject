using System;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public abstract class ControllerBase
    {
        public Guid Id { get; set; }

        // TODO Really, Serialize needs to be able to take a ConfigTree.Node and return what the model
        //      would look like at that config without any side-effects
        protected internal abstract ModelBase Serialize();
    }
}
