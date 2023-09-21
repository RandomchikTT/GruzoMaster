using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public class UserSettings
    {
        public enum UserSetting
        {
            /// <summary>
            /// Возможность просматривать логи
            /// </summary>
            CanCheckLogs = 0,
            /// <summary>
            /// Возможность просматривать водителей
            /// </summary>
            CanCheckDrivers = 1,
            /// <summary>
            /// Возможность изменять данные водителей
            /// </summary>
            CanEditDrivers = 2,
            /// <summary>
            /// Возможность удалять водителей
            /// </summary>
            CanDeleteDrivers = 3,
            /// <summary>
            /// Возможность добавлять водителей
            /// </summary>
            CanAppendDrivers = 4,
            /// <summary>
            /// Возможность смотреть автопарк
            /// </summary>
            CanCheckTransport = 5,
        }
        public static Dictionary<UserType, Dictionary<UserSetting, Boolean>> UserSettingDictionary = new Dictionary<UserType, Dictionary<UserSetting, Boolean>>()
        {
            { UserType.Admin, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, true },
                { UserSetting.CanCheckDrivers, true },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
            }},
            { UserType.User, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, true },
                { UserSetting.CanCheckDrivers, true },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
            }},
            { UserType.Owner, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, true },
                { UserSetting.CanCheckDrivers, true },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
            }},
        };
        public static Boolean GetAccessUser(UserSetting userSetting)
        {
            try
            {
                if (User.LoggedUser == null) return false;
                if (User.LoggedUser.UserType == UserType.Owner) return true;
                return UserSettingDictionary[User.LoggedUser.UserType][userSetting];
            }
            catch (Exception e) { MessageBox.Show("GetAccessUser: " + e.ToString()); return false; }
        }
    }
}
