using System.Collections.Generic;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    // TODO Does this track deltas?
    public sealed class GMFolder : GMResource
    {
        private string folderName;
        public string FolderName
        {
            get { return GetProperty(folderName); }
            set { SetProperty(value, ref folderName); }
        }

        private string localizedName;
        public string LocalizedName
        {
            get { return GetProperty(localizedName); }
            set { SetProperty(value, ref localizedName); }
        }

        private string filterType;
        public string FilterType
        {
            get { return GetProperty(filterType); }
            set { SetProperty(value, ref filterType); }
        }

        private bool isDefaultView;
        public bool IsDefaultView
        {
            get { return GetProperty(isDefaultView); }
            set { SetProperty(value, ref isDefaultView); }
        }

        public List<GMResource> Children { get; set; } 

        protected internal override string ResourcePath => $@"views\{Id}.yy";

        public GMFolder()
        {
            Children = new List<GMResource>();
        }

        protected internal override void Create()
        {
            // NOTE Nothing to do
        }

        internal override ModelBase Serialize()
        {
            return new GMFolderModel
            {
                id = Id,
                name = Id.ToString(),
                folderName = folderName,
                children = Children.Select(x => x.Id).ToList(),
                filterType = filterType,
                isDefaultView = isDefaultView,
                localisedFolderName = localizedName
            };
        }
    }
}