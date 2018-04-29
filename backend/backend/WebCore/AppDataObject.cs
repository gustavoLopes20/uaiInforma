using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.WebCore
{
    public class AppDataObject
    {
        public long Id { get; set; }

        private string _rid = null;

        [MaxLength(64)]
        public string RID { get { return _rid; } set { _rid = value; } }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Registro { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataUpdate { get; set; } = DateTime.UtcNow;

        public bool Ativo { get; set; } = true;

        public AppDataObject()
        {
            _rid = GenerateUniqueRID();
        }

        public static string GenerateUniqueRID(bool maior = false)
        {
            try
            {
                string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var r = new Random();
                long salt = DateTime.UtcNow.Ticks;
                salt *= 10000;
                salt += r.Next(1000000000);

                if (salt < 0)
                    salt = salt * -1;

                var result = new string(
                    Enumerable.Repeat(chars, 24)
                              .Select(s => s[r.Next(s.Length)])
                              .ToArray());


                string text = salt.ToString() + result;

                byte[] buffer = Encoding.Default.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

                if (maior)
                    hash += result;

                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
    }
}
