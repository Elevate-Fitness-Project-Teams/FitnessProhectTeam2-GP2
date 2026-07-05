using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public enum ErrorType
    {
        None = 0,
        Failure = 1,
        Validation = 2,
        Unauthorized = 3,
        NotFound = 4,
        Conflict = 5,
        Locked = 6,
        RateLimit = 7
    }
}
