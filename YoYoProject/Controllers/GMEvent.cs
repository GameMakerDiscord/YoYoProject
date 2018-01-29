using System;
using System.IO;
using YoYoProject.Models;

namespace YoYoProject.Controllers
{
    public sealed class GMEvent : ControllerBase
    {
        private GMEventType eventType;
        public GMEventType EventType
        {
            get { return GetProperty(eventType); }
            set { SetProperty(value, ref eventType); }
        }

        private GMEventNumber eventNumber;
        public GMEventNumber EventNumber
        {
            get { return GetProperty(eventNumber); }
            set { SetProperty(value, ref eventNumber); }
        }

        private GMObject collision;
        public GMObject Collision
        {
            get { return GetProperty(collision); }
            set { SetProperty(value, ref collision); }
        }
        
        private bool isDnD;
        public bool IsDnD
        {
            get { return GetProperty(isDnD); }
            set { SetProperty(value, ref isDnD); }
        }

        private string contents;
        public string Contents
        {
            get
            {
                if (contents == null)
                {
                    if (File.Exists(ScriptFullPath))
                        contents = File.ReadAllText(ScriptFullPath);
                    else
                        contents = "";
                }

                return contents;
            }

            set
            {
                if (value == contents)
                    return;

                contents = value ?? "";
                Dirty = true;
            }
        }

        private string ScriptFullPath
        {
            get
            {
                string path;
                if (parent is GMTimeline)
                    path = $@"timelines\{parent.Name}\moment_{(int)EventNumber}.gml";
                else if (EventType == GMEventType.Collision)
                    path = $@"objects\{parent.Name}\Collision_{Id}.gml";
                else
                    path = $@"objects\{parent.Name}\{EventType}_{(int)EventNumber}.gml";

                return Path.Combine(Project.RootDirectory, path);
            }
        }

        private readonly GMResource parent;

        internal GMEvent(GMResource parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            this.parent = parent;
        }
        
        internal override ModelBase Serialize()
        {
            // TODO Unload OnSaveComplete
            if (contents != null)
                File.WriteAllText(ScriptFullPath, contents);

            return new GMEventModel
            {
                id = Id,
                m_owner = parent.Id,
                eventtype = (int)EventType,
                enumb = (int)EventNumber,
                collisionObjectId = Collision?.Id ?? Guid.Empty,
                IsDnD = IsDnD
            };
        }

        internal override void Deserialize(ModelBase model)
        {
            var eventModel = (GMEventModel)model;

            Id = eventModel.id;
            EventType = (GMEventType)eventModel.eventtype;
            EventNumber = (GMEventNumber)eventModel.enumb;
            // TODO Implement
            //Collision = eventModel.collisionObjectId == Guid.Empty
            //    ? null : parent.Project.Resources.Get<GMObject>(eventModel.collisionObjectId);
            IsDnD = eventModel.IsDnD;
        }
    }

    public enum GMEventType
    {
        Create = 0,
        Destroy = 1,
        Alarm = 2,
        Step = 3,
        Collision = 4,
        Keyboard = 5,
        Mouse = 6,
        Other = 7,
        Draw = 8,
        KeyPress = 9,
        KeyRelease = 10,
        Trigger = 11,
        CleanUp = 12,
        Gesture = 13
    }

    public enum GMEventNumber
    {
        Default = 0,

        Step = 0,
        StepBegin = 1,
        StepEnd = 2,

        OtherOutside = 0,
        OtherBoundary = 1,
        OtherStartGame = 2,
        OtherEndGame = 3,
        OtherStartRoom = 4,
        OtherEndRoom = 5,
        OtherNoLives = 6,
        OtherAnimationEnd = 7,
        OtherEndOfPath = 8,
        OtherNoHealth = 9,
        OtherUser0 = 10,
        OtherUser1 = 11,
        OtherUser2 = 12,
        OtherUser3 = 13,
        OtherUser4 = 14,
        OtherUser5 = 15,
        OtherUser6 = 16,
        OtherUser7 = 17,
        OtherUser8 = 18,
        OtherUser9 = 19,
        OtherUser10 = 20,
        OtherUser11 = 21,
        OtherUser12 = 22,
        OtherUser13 = 23,
        OtherUser14 = 24,
        OtherUser15 = 25,
        OtherCloseButton = 30,
        OtherOutsideView0 = 40,
        OtherOutsideView1 = 41,
        OtherOutsideView2 = 42,
        OtherOutsideView3 = 43,
        OtherOutsideView4 = 44,
        OtherOutsideView5 = 45,
        OtherOutsideView6 = 46,
        OtherOutsideView7 = 47,
        OtherBoundaryView0 = 50,
        OtherBoundaryView1 = 51,
        OtherBoundaryView2 = 52,
        OtherBoundaryView3 = 53,
        OtherBoundaryView4 = 54,
        OtherBoundaryView5 = 55,
        OtherBoundaryView6 = 56,
        OtherBoundaryView7 = 57,
        OtherAnimationUpdate = 58,
        OtherWebImageLoaded = 60,
        OtherWebSoundLoaded = 61,
        OtherWebAsync = 62,
        OtherWebUserInteraction = 63,
        OtherWebIap = 66,
        OtherWebCloud = 67,
        OtherWebNetworking = 68,
        OtherWebSteam = 69,
        OtherSocial = 70,
        OtherPushNotification = 71,
        OtherAsyncSaveLoad = 72,
        OtherAsyncAudioRecording = 73,
        OtherAsyncAudioPlayback = 74,
        OtherAsyncSystem = 75,

        DrawGui = 64,
        DrawResize = 65,
        DrawBegin = 72,
        DrawEnd = 73,
        DrawGuiBegin = 74,
        DrawGuiEnd = 75,
        DrawPre = 76,
        DrawPost = 77,

        MouseLeftButton = 0,
        MouseRightButton = 1,
        MouseMiddleButton = 2,
        MouseNoButton = 3,
        MouseLeftPressed = 4,
        MouseRightPressed = 5,
        MouseMiddlePressed = 6,
        MouseLeftReleased = 7,
        MouseRightReleased = 8,
        MouseMiddleReleased = 9,
        MouseEnter = 10,
        MouseLeave = 11,
        MouseJoystick1Left = 16,
        MouseJoystick1Right = 17,
        MouseJoystick1Up = 18,
        MouseJoystick1Down = 19,
        MouseJoystick1Button1 = 21,
        MouseJoystick1Button2 = 22,
        MouseJoystick1Button3 = 23,
        MouseJoystick1Button4 = 24,
        MouseJoystick1Button5 = 25,
        MouseJoystick1Button6 = 26,
        MouseJoystick1Button7 = 27,
        MouseJoystick1Button8 = 28,
        MouseJoystick2Left = 31,
        MouseJoystick2Right = 32,
        MouseJoystick2Up = 33,
        MouseJoystick2Down = 34,
        MouseJoystick2Button1 = 36,
        MouseJoystick2Button2 = 37,
        MouseJoystick2Button3 = 38,
        MouseJoystick2Button4 = 39,
        MouseJoystick2Button5 = 40,
        MouseJoystick2Button6 = 41,
        MouseJoystick2Button7 = 42,
        MouseJoystick2Button8 = 43,
        MouseGlobalLeftButton = 50,
        MouseGlobalRightButton = 51,
        MouseGlobalMiddleButton = 52,
        MouseGlobalLeftPressed = 53,
        MouseGlobalRightPressed = 54,
        MouseGlobalMiddlePressed = 55,
        MouseGlobalLeftReleased = 56,
        MouseGlobalRightReleased = 57,
        MouseGlobalMiddleReleased = 58,
        MouseWheelUp = 60,
        MouseWheelDown = 61,

        GestureTap = 0,
        GestureDoubleTap = 1,
        GestureDragStart = 2,
        GestureDragMove = 3,
        GestureDragEnd = 4,
        GestureFlick = 5,
        GesturePinchStart = 6,
        GesturePinchIn = 7,
        GesturePinchOut = 8,
        GesturePinchEnd = 9,
        GestureRotateStart = 10,
        GestureRotating = 11,
        GestureRotateEnd = 12,
        GestureGlobalTap = 64,
        GestureGlobalDoubleTap = 65,
        GestureGlobalDragStart = 66,
        GestureGlobalDragMove = 67,
        GestureGlobalDragEnd = 68,
        GestureGlobalFlick = 69,
        GestureGlobalPinchStart = 70,
        GestureGlobalPinchIn = 71,
        GestureGlobalPinchOut = 72,
        GestureGlobalPinchEnd = 73,
        GestureGlobalRotateStart = 74,
        GestureGlobalRotating = 75,
        GestureGlobalRotateEnd = 76
    }
}