using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMatch.Entities.Database
{
    public class UserLike
    {
        public User SourceUser { get; set; }
        public int SourceUserId { get; set; }

        public User LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}
