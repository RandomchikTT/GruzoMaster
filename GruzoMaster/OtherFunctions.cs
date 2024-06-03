using GruzoMaster.Objects.Cargo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public static class OtherFunctions
    {
        /// <summary>
        /// Функция преобразования строки в ХЕШ
        /// </summary>
        /// <param name="strData">Строка которую требуются зашифровать</param>
        /// <returns>ХЕШ-Код</returns>
        public static String GetSha256(String strData)
        {
            try
            {
                Byte[] message = Encoding.ASCII.GetBytes(strData);
                SHA256Managed hashString = new SHA256Managed();
                String hex = "";
                Byte[] hashValue = hashString.ComputeHash(message);
                foreach (Byte x in hashValue)
                    hex += String.Format("{0:x2}", x);
                return hex;
            }
            catch (Exception e) { MessageBox.Show("GetSha256: " + e.ToString()); return null; }
        }
        /// <summary>
        /// Функция преобразования числа в строку, с точками.
        /// </summary>
        /// <param name="money">Сумма</param>
        /// <returns>Сумма с точками</returns>
        public static String ConvertToFormatMoney(this Int32 money)
        {
            return money.ToString("N0", new CultureInfo("en-US")).Replace(",", ".");
        }
        /// <summary>
        /// Функция преобразования числа в строку, с точками. (int64)
        /// </summary>
        /// <param name="money">Сумма</param>
        /// <returns>Сумма с точками</returns>
        public static String ConvertToFormatMoney(this Int64 money)
        {
            return money.ToString("N0", new CultureInfo("en-US")).Replace(",", ".");
        }
    }
}
