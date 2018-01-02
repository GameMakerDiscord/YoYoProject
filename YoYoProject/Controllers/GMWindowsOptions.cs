using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMWindowsOptions : GMResource
    {
        public string DisplayName
        {
            get { return GetProperty(inner.option_windows_display_name, "option_windows_display_name"); }
            set { SetProperty(value, out inner.option_windows_display_name, "option_windows_display_name"); }
        }

        private readonly GMWindowsOptionsModel inner;

        protected internal override string ResourcePath
        {
            get { return @"options\windows\options_windows.yy"; }
        }

        public GMWindowsOptions()
        {
            inner = new GMWindowsOptionsModel();
        }

        protected internal override ModelBase Serialize()
        {
            return new GMWindowsOptionsModel
            {
                name = "Windows",
                option_windows_display_name = DisplayName
            };
        }
    }
}