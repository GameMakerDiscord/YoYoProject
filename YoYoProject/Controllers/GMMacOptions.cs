using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMMacOptions : GMResource
    {
        public string DisplayName
        {
            get { return GetProperty(inner.option_mac_display_name, "option_mac_display_name"); }
            set { SetProperty(value, out inner.option_mac_display_name, "option_mac_display_name"); }
        }

        protected internal override string ResourcePath => @"options\mac\options_mac.yy";

        private readonly GMMacOptionsModel inner;

        public GMMacOptions()
        {
            inner = new GMMacOptionsModel();
        }
        
        protected internal override ModelBase Serialize()
        {
            return new GMMacOptionsModel
            {
                id = Id,
                name = "macOS",
                option_mac_display_name = DisplayName
            };
        }
    }
}