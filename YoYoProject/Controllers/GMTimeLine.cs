using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMTimeline : GMResource
    {
        public MomentManager Moments { get; }

        internal override string ResourcePath => $@"timelines\{Name}\{Name}.yy";

        public GMTimeline()
        {
            Moments = new MomentManager(this);
        }

        internal override void Create(string name)
        {
            Name = Project.Resources.GenerateValidName(name ?? "timeline");
        }

        internal override ModelBase Serialize()
        {
            return new GMTimelineModel
            {
                id = Id,
                name = Name,
                momentList = Moments.Serialize()
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var timelineModel = (GMTimelineModel)model;

            Id = timelineModel.id;
            Name = timelineModel.name;
            Moments.Deserialize(timelineModel.momentList);
        }

        public sealed class MomentManager : IEnumerable<KeyValuePair<int, GMEvent>>
        {
            public int Count => moments.Count;

            public ICollection<int> Keys => moments.Keys;

            public ICollection<GMEvent> Values => moments.Values;

            public GMEvent this[int key] => moments[key];

            private readonly Dictionary<int, GMEvent> moments;
            private readonly GMTimeline gmTimeline;

            internal MomentManager(GMTimeline gmTimeline)
            {
                if (gmTimeline == null)
                    throw new ArgumentNullException(nameof(gmTimeline));

                moments = new Dictionary<int, GMEvent>();

                this.gmTimeline = gmTimeline;
            }

            public GMEvent Get(int moment)
            {
                GMEvent @event;
                if (moments.TryGetValue(moment, out @event))
                    return @event;

                return null;
            }
            
            public GMEvent Create(int moment)
            {
                var @event = Get(moment);
                if (@event != null)
                    return @event;

                @event = new GMEvent(gmTimeline)
                {
                    Project = gmTimeline.Project,
                    Id = Guid.NewGuid(),
                    EventType = GMEventType.Create,
                    EventNumber = (GMEventNumber)moment, // HACK Eww
                    Collision = null,
                    IsDnD = gmTimeline.Project.DragAndDrop
                };

                moments.Add(moment, @event);

                return @event;
            }

            public void Delete(int moment)
            {
                moments.Remove(moment);
            }

            public void Clear()
            {
                moments.Clear();
            }

            internal List<GMTimelineMomentModel> Serialize()
            {
                return moments.Select(x => new GMTimelineMomentModel
                {
                    id = Guid.NewGuid(), // TODO Don't regenerate ids like this
                    moment = x.Key,
                    evnt = (GMEventModel)x.Value.Serialize()
                }).ToList();
            }

            internal void Deserialize(List<GMTimelineMomentModel> momentListModel)
            {
                foreach (var momentModel in momentListModel)
                {
                    var @event = new GMEvent(gmTimeline)
                    {
                        Project = gmTimeline.Project,
                        Id = momentModel.evnt.id
                    };
                    @event.Deserialize(momentModel.evnt);

                    moments.Add(momentModel.moment, @event);
                }
            }

            public IEnumerator<KeyValuePair<int, GMEvent>> GetEnumerator()
            {
                return moments.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
