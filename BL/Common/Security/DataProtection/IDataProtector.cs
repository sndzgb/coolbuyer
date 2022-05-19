using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Security.DataProtection
{
    public interface IDataProtector
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
