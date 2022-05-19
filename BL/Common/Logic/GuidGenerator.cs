﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Logic
{
    public static class GuidGenerator
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid NewSqlSequentialId()
        {
            const int RPC_S_OK = 0;
            Guid guid;

            int result = UuidCreateSequential(out guid);

            if (result != RPC_S_OK)
            {
                return Guid.NewGuid();
            }

            var s = guid.ToByteArray();
            var t = new byte[16];

            t[3] = s[0];
            t[2] = s[1];
            t[1] = s[2];
            t[0] = s[3];

            t[5] = s[4];
            t[4] = s[5];
            t[7] = s[6];
            t[6] = s[7];
            t[8] = s[8];
            t[9] = s[9];
            t[10] = s[10];
            t[11] = s[11];
            t[12] = s[12];
            t[13] = s[13];
            t[14] = s[14];
            t[15] = s[15];

            return new Guid(t);
        }
    }
}
