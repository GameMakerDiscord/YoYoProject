using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMMacOptions : GMResource
    {
        private string displayName;
        public string DisplayName
        {
            get { return GetProperty(displayName); }
            set { SetProperty(value, ref displayName); }
        }

        protected internal override string ResourcePath => @"options\mac\options_mac.yy";
        
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