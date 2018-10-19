using System;
using System.Collections.Generic;
using System.Text;

namespace iMISClubs.Models
{
    public class MemberAlert
    {
        public string Id { get; set; }
        public MemberAlertType Type { get; set; }
        public string Description { get; set; }
    }

    public enum MemberAlertType
    {
        Info,
        Warning,
        Urgent
    }
}
