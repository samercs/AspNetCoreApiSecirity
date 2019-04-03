using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithSecurity.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
