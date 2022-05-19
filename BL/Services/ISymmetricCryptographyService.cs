﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface ISymmetricCryptographyService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
