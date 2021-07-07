using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils
{
    public enum ResponseStatus
    {
        Success = 1,
        SuccessCreated = 2,
        SuccessNoContent = 3,
        Fail = 4,
        Unauthorized = 5,
        AccessDenied = 6,
        NotFound = 7,
        Error = 8
    }
}
