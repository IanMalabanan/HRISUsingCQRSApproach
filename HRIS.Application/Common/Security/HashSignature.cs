using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HRIS.Application.Common.Security
{
    public class HashSignature
    {
        public static string Sha256Encryptor(string val)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            
            SHA256Managed sha256hasher = new SHA256Managed();
            
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(val));
            
            return Convert.ToBase64String(hashedDataBytes);
        }

        public static string GenerateSignature(string val)
        {
            string rawString = string.Empty;
            
            var bytes = Encoding.UTF8.GetBytes(val);
            
            using (var hash = SHA512.Create())
            {
                hash.ComputeHash(bytes);

                rawString = BitConverter.ToString(hash.Hash).Replace("-", "").ToLower();
            }

            return rawString;
        }
    }
}

