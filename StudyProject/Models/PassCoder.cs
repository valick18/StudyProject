using StudyProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace StudyProject.Models
{
    public class PassCoder
    {
        tbUser user;
        public PassCoder(tbUser user)
        {
            this.user = user;
        }

        public byte[] EncryptPassword(string usedpassword)
        {
            return HashGenerator.HashBin(usedpassword, user.Login + "Its my own cool application with a strong encription. It had to be salt -> 15KFs;vjrAsdf32n4 ;)");
        }

        public bool VerifyPassword(string usedpassword)
        {
            byte[] Hash = EncryptPassword(usedpassword);
            if ((user.Password == null) || (Hash.Length != user.Password.Length)) return false;
            for (byte b = 0; b < Hash.Length; b++)
                if (Hash[b] != user.Password[b]) return false;
            return true;
        }

    }
}