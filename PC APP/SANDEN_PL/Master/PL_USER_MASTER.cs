using SANDEN_COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANDEN_PL
{
    #region User Master
    public class PL_USER_MASTER:Common
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Group { get; set; }
        //public string EmailId { get; set; }
        //public string EmpCode { get; set; }

        //public string Designation { get; set; }
        public bool Active { get; set; }

    }
    #endregion

}
