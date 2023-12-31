﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Helpers
{
    public class PasswordSalt
    {
        public static string Create()
        {
            byte[] randomBytes = new byte[128 / 8];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
    public class PasswordHash
    {
        public static string Create(string password, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(password: password, salt: Encoding.UTF8.GetBytes(salt), prf: KeyDerivationPrf.HMACSHA512, iterationCount: 80000, numBytesRequested: 256 / 8);
            return Convert.ToBase64String(valueBytes);
        }

        public static bool Validate(string password, string salt, string hash)
            => Create(password, salt) == hash;
    }
}
