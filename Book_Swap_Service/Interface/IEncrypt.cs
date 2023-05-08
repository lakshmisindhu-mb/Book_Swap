using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Swap_Service.Interface
{
    public interface IEncrypt
    {
         string EncodePasswordToBase64(string password);
         string Decrypt_Password(string encodeData);
    }
}
