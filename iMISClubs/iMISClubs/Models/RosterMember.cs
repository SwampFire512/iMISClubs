using System;
using System.Collections.ObjectModel;

namespace iMISClubs.Models
{
    public class RosterMember
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string InstituteTypeName { get; set; }
        public string InstituteType { get; set; }
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