using System;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public abstract class ControllerBase
    {
        public Guid Id { get; set; }

        protected internal abstract ModelBase Serialize();
    }
}
