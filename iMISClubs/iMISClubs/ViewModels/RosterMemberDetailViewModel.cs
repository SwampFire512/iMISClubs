using System;

using iMISClubs.Models;

namespace iMISClubs.ViewModels
{
    public class RosterMemberDetailViewModel : BaseViewModel
    {
        public RosterMember Member { get; set; }
        public RosterMemberDetailViewModel(RosterMember member = null)
        {
            Title = member?.FullName;
            Member = member;
        }
    }
}
