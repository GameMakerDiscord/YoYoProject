using System.Collections.Generic;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject
{
    public sealed class GMFolder : GMResource
    {
        public string FolderName
        {
            get { return GetProperty(inner.folderName); }
            set { SetProperty(value, out inner.folderName); }
        }

        public string LocalizedName
        {
            get { return GetProperty(inner.localisedFolderName); }
            set { SetProperty(value, out inner.localisedFolderName); }
        }

        public string FilterType
        {
            get { return GetProperty(inner.filterType); }
            set { SetProperty(value, out inner.filterType); }
        }

        public bool IsDefaultView
        {
            get { return GetProperty(inner.isDefaultView); }
            set { SetProperty(value, out inner.isDefaultView); }
        }

        public List<GMResource> Children { get; set; } 

        protected internal override string ResourcePath
        {
            get { return $@"views\{Id}.yy"; }
        }

        private readonly GMFolderModel inner;

        public GMFolder()
        {
            Children = new List<GMResource>();
            inner = new GMFolderModel();
        }

        protected internal override ModelBase Serialize()
        {
            return new GMFolderModel
            {
                id = Id,
                name = Id.ToString(),
                folderName = FolderName,
                children = Children.Select(x => x.Id).ToList(),
                filterType = FilterType,
                isDefaultView = IsDefaultView,
                localisedFolderName = LocalizedName
            };
        }
    }
}