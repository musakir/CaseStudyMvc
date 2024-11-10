using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMvc.Entities.Helper
{
    public static class MkHelper
    {
        public static int FISID = 0;

        public static Exception SonHata = (Exception)null;

        public static string mkToString(this object pText)
        {
            string str = "";

            SonHata = (Exception)null;

            try
            {
                str = !(pText is DBNull) && pText != null ? Convert.ToString(pText) : "";
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return str;
        }

        public static int mkToInt(this object pText)
        {
            int result = 0;

            SonHata = (Exception)null;

            try
            {
                if (pText == null)
                    pText = "0";

                int.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static float mkToFloat(this object pText)
        {
            float result = 0.0f;

            SonHata = (Exception)null;

            try
            {
                if (pText == null)
                    pText = 0;

                float.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static bool mkToBool(this object pText)
        {
            bool result = false;

            SonHata = (Exception)null;

            try
            {
                bool.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static void LogYaz(string Metin)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists) di.Create();

            FileStream fs = new FileStream(@"c:\bykmLOGS\CaseStudyMvcLogDosyasi.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine("(" + DateTime.Now.ToString("dd/MM") + " " + DateTime.Now.ToLongTimeString() + ") " + Metin);
            m_streamWriter.Flush();
            m_streamWriter.Close();
        }

        public static string mkFloatToDbString(this object pText)
        {
            string str = "";

            SonHata = (Exception)null;

            try
            {
                str = !(pText is DBNull) && pText != null ? Convert.ToString(pText) : "";
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            if (!string.IsNullOrWhiteSpace(str))
                str = str.Replace(",", ".");

            return str;
        }

        public static string mkToDbString(this object pText)
        {
            string str = "";

            SonHata = (Exception)null;

            try
            {
                str = !(pText is DBNull) && pText != null ? Convert.ToString(pText) : "";
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            if (!string.IsNullOrWhiteSpace(str))
                str = str.Replace("'", " ");

            return str;
        }

        public static string mkEncryptString(string Text)
        {
            byte[] Iv = new byte[16];
            byte[] Array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF32.GetBytes("KeyMk123");
                aes.IV = Iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(Text);
                        }

                        Array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(Array);
        }

        public static string mkDecryptString(string Text)
        {
            byte[] Iv = new byte[16];
            byte[] Buffer = Convert.FromBase64String(Text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF32.GetBytes("KeyMk123");
                aes.IV = Iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(Buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
