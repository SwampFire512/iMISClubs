using System;
using System.Collections.Generic;
using System.Text;

namespace iMISClubs.Models
{
    class ContactImage
    {
        public enum MaskType
        {
            Circle,
            Square,
            None
        }

        public string Id { get; set; }
        public string Path { get; set; }
        public MaskType Mask { get; set; }

    }
}
