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

        internal override string ResourcePath => $@"views\{Id}.yy";

        public GMFolder()
        {
            Children = new List<GMResource>();
        }

        internal override void Create(string name)
        {
            FolderName = name ?? Project.Resources.GenerateValidName("folder");
        }

        internal override ModelBase Serialize()
        {
            return new GMFolderModel
            {
                id = Id,
                name = Id.ToString(),
                folderName = FolderName,
                children = Children.Select(x => x.Id).ToList(),
                filterType = filterType,
                isDefaultView = IsDefaultView,
                localisedFolderName = LocalizedName
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            // TODO Implement
            var folderModel = (GMFolderModel)model;

            Id = folderModel.id;
            Name = folderModel.id.ToString();
            FolderName = folderModel.folderName;
            // TODO Implement
            //Children = folderModel.children.Select(x => Project.Resources.Get(x)).ToList();
            FilterType = folderModel.filterType;
            IsDefaultView = folderModel.isDefaultView;
            LocalizedName = folderModel.localisedFolderName;
        }
    }
}