using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Models.Models
{
    public class RateUserResponse : CrudStatus
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
