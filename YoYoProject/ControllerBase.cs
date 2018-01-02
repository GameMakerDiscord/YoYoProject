using System;
using YoYoProject.Models;

namespace YoYoProject
{
    public abstract class ControllerBase<TController, TModel>
        where TController : ControllerBase<TController, TModel>, new()
        where TModel : ModelBase, new()
    {
        public Guid Id { get; set; }

        protected internal abstract void Deserialize(TModel model);

        protected internal abstract TModel Serialize();

        public static TController FromModel(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var controller = new TController();
            controller.Deserialize(model);

            return controller;
        }
    }
}
