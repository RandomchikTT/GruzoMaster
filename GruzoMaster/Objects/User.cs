using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public enum PhoneNumber
    {
        Bellarusian = 0,
        Russian = 1,
        Litva = 2,
    }
    public enum CompanyBankData
    {
        INN = 0,
        LTD = 1,
        NameOfBank = 2,
        NumberBank = 3,
        AddressBank = 4,
    }
    public enum License
    {
        B = 0,
        C = 1,
        CE = 2,
        A = 3,
        D = 4,
    }
    public enum UserType
    {
        User = 0,
        Admin = 1,
        Owner = 2,
    }
    public class User
    {
        /// <summary>
        /// Авторизованный пользователь
        /// </summary>
        public static User LoggedUser = null;
        public String Login { get; set; } = String.Empty;
        public String Name { get; set; } = String.Empty;
        public UserType UserType { get; set; } = UserType.User;
    }
}
