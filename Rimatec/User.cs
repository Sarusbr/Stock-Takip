using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimatec
{
    class UserData
    {
        public UserData()
        {

        }

        public int UserID { get; set; }

        public string realName { get; set; }

        public string realLastName { get; set; }

        public string title { get; set; }

        public bool admins { get; set; }

        public bool stockDisplay { get; set; }

        public bool stockChange { get; set; }

        public bool moveDisplay { get; set; }

        public bool moveChange { get; set; }

        public bool notificationEdit { get; set; }

    }
}
