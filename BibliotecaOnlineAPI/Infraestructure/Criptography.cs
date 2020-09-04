using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaOnlineAPI.Infraestructure
{
    public  class Criptography
    {
        public static bool ValidacionPassword(string password, byte[] HashPassword, byte[] Saltpassword)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Saltpassword) ) 
            {
                var Computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < Computed.Length; i++)
                {
                    if (Computed[i] != HashPassword[i]) return false;

                }
            }
            return true;
        }

        public static void CrearPasswordEncriptado(string Password, out byte[] HashPassword, out byte[] Saltpassword)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                Saltpassword = hmac.Key;
                HashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }


    }
}
