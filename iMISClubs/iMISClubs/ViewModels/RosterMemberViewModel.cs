using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using iMISClubs.Models;
using Xamarin.Forms;

namespace iMISClubs.ViewModels
{
    public class RosterMemberViewModel : BaseViewModel
    {
        /*
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string InstituteTypeName { get; set; }
        public string InstituteType { get; set; }
        public string ProfileImageResource { get; set; }
        public CheckInStatus Status { get; set; }
        */
        public string FullName => RosterMember?.FullName;
        public string Description => RosterMember?.Description;
        public string InstituteTypeName => RosterMember?.InstituteTypeName;
        public string InstituteType => RosterMember?.InstituteType;
        public string ProfileImageResource => RosterMember?.ProfileImageResource;
        public CheckInStatus? Status => RosterMember?.Status;

        public RosterMember RosterMember { get; set; }

        public RosterMemberViewModel(RosterMember member)
        {
            //FullName = RosterMember?.FullName;
            //Description = RosterMember?.Description;
            //InstituteTypeName = RosterMember?.InstituteTypeName;
            //InstituteType = RosterMember?.InstituteType;
            //ProfileImageResource = RosterMember?.ProfileImageResource;
            //Status = (RosterMember == null) ? CheckInStatus.New : RosterMember.Status;

            RosterMember = member;
        }

        ICommand tapCheckinCommand;
        
        public ICommand TapCheckinCommand
        {
            get
            {
                if (tapCheckinCommand == null)
                    tapCheckinCommand = new Command(OnCheckinTapped);
                return tapCheckinCommand;
            }
        }


        void OnCheckinTapped(object s)
        {
            switch (Status)
            {
                case (CheckInStatus.CheckedIn):
                    RosterMember.Status = CheckInStatus.CheckedOut;
                    break;
                case (CheckInStatus.CheckedOut):
                    RosterMember.Status = CheckInStatus.New;
                    break;
                case (CheckInStatus.New):
                    RosterMember.Status = CheckInStatus.CheckedIn;
                    break;
            }

            OnPropertyChanged("Status");
            OnPropertyChanged("StatusColor");
            OnPropertyChanged("StatusSvgResource");
        }

        public string StatusColor
        {
            get
            {
                //rgb(225, 239, 247)
                //rgb(223, 240, 216)
                //rgb(255, 186, 186)
                switch (Status)
                {
                    case (CheckInStatus.CheckedIn): return "#dff0d8";
                    case (CheckInStatus.CheckedOut): return "#eeeeee";
                    case (CheckInStatus.New): return "#e1eff7";

                }
                return "#ffffff";
            }
        }

        public string StatusSvgResource
        {
            get
            {
                switch (Status)
                {
                    case (CheckInStatus.CheckedIn): return "iMISClubs.Svg.greenHouse.svg";
                    case (CheckInStatus.CheckedOut): return "iMISClubs.Svg.greyHouse.svg";
                    case (CheckInStatus.New): return "iMISClubs.Svg.blueHouse.svg";
                }
                return "";
            }
        }

        public ImageSource ProfileImageSource
        {
            get { return ImageSource.FromResource(ProfileImageResource); }
        }
    }
}
