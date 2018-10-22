using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using iMISClubs.Svg;
using Xamarin.Forms;

namespace iMISClubs.Models
{
    public class RosterMember
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string InstituteTypeName { get; set; }
        public string InstituteType { get; set; }
        public string ProfileImageResource { get; set; }
        public CheckInStatus Status { get; set; }
        public Collection<MemberAlert> MemberAlerts { get; set; }
    }

    public enum CheckInStatus
    {
        New,
        CheckedIn,
        CheckedOut
    }
    
}