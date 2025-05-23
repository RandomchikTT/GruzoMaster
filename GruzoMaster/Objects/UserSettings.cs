﻿using System;
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
            /// <summary>
            /// Возможность удалять транспорт
            /// </summary>
            CanDeleteTransport = 6,
            /// <summary>
            /// Возможность добавлять транспорт
            /// </summary>
            CanAppendTransport = 7,
            /// <summary>
            /// Возможность изменять данные транспорта
            /// </summary>
            CanEditDataTransport = 8,
            /// <summary>
            /// Открывать меню с компаниями
            /// </summary>
            CanCheckCompanyMenu = 9,
            /// <summary>
            /// Делать экспорт данных про компанию
            /// </summary>
            CanMakeExportDataCompany = 10,
            /// <summary>
            /// Добавлять компанию
            /// </summary>
            CanAppendCompany = 11,
            /// <summary>
            /// Удалять компанию
            /// </summary>
            CanDeleteCompany = 12,
            /// <summary>
            /// Изменять информацию о компании
            /// </summary>
            CanEditCompany = 13,
            /// <summary>
            /// Просматривать меню о грузах
            /// </summary>
            CheckCargoMenu = 14,
            /// <summary>
            /// Изменять данные о грузе
            /// </summary>
            EditingCargoMenu = 15,
            /// <summary>
            /// Открывать окно с экспедиторами
            /// </summary>
            CanOpenForwarderMenu = 16,
        }
        private static Dictionary<UserType, Dictionary<UserSetting, Boolean>> UserSettingDictionary = new Dictionary<UserType, Dictionary<UserSetting, Boolean>>()
        {
            { UserType.Admin, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, true },
                { UserSetting.CanCheckDrivers, true },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
                { UserSetting.CanDeleteTransport, true },
                { UserSetting.CanAppendTransport, true },
                { UserSetting.CanEditDataTransport, true },
                { UserSetting.CanCheckCompanyMenu, true },
                { UserSetting.CanMakeExportDataCompany, true },
                { UserSetting.CanAppendCompany, true },
                { UserSetting.CanEditCompany, true },
                { UserSetting.CanDeleteCompany, true },
                { UserSetting.CheckCargoMenu, true },
                { UserSetting.EditingCargoMenu, true },
                { UserSetting.CanOpenForwarderMenu, true },
            }},
            { UserType.User, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, false },
                { UserSetting.CanCheckDrivers, false },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
                { UserSetting.CanDeleteTransport, true },
                { UserSetting.CanAppendTransport, true },
                { UserSetting.CanEditDataTransport, true },
                { UserSetting.CanCheckCompanyMenu, true },
                { UserSetting.CanMakeExportDataCompany, true },
                { UserSetting.CanAppendCompany, true },
                { UserSetting.CanEditCompany, true },
                { UserSetting.CanDeleteCompany, true },
                { UserSetting.CheckCargoMenu, true },
                { UserSetting.EditingCargoMenu, true },
                { UserSetting.CanOpenForwarderMenu, true },
            }},
            { UserType.Owner, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, true },
                { UserSetting.CanCheckDrivers, true },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
                { UserSetting.CanDeleteTransport, true },
                { UserSetting.CanAppendTransport, true },
                { UserSetting.CanEditDataTransport, true },
                { UserSetting.CanCheckCompanyMenu, true },
                { UserSetting.CanMakeExportDataCompany, true },
                { UserSetting.CanAppendCompany, true },
                { UserSetting.CanEditCompany, true },
                { UserSetting.CanDeleteCompany, true },
                { UserSetting.CheckCargoMenu, true },
                { UserSetting.EditingCargoMenu, true },
                { UserSetting.CanOpenForwarderMenu, true },
            }},
            { UserType.Forwarder, new Dictionary<UserSetting, Boolean>()
            {
                { UserSetting.CanCheckLogs, true },
                { UserSetting.CanCheckDrivers, true },
                { UserSetting.CanEditDrivers, true },
                { UserSetting.CanAppendDrivers, true },
                { UserSetting.CanCheckTransport, true },
                { UserSetting.CanDeleteTransport, true },
                { UserSetting.CanAppendTransport, true },
                { UserSetting.CanEditDataTransport, true },
                { UserSetting.CanCheckCompanyMenu, true },
                { UserSetting.CanMakeExportDataCompany, true },
                { UserSetting.CanAppendCompany, true },
                { UserSetting.CanEditCompany, true },
                { UserSetting.CanDeleteCompany, true },
                { UserSetting.CheckCargoMenu, true },
                { UserSetting.EditingCargoMenu, true },
                { UserSetting.CanOpenForwarderMenu, true },
            }},
        };
        public static Boolean GetAccessUser(UserSetting userSetting)
        {
            try
            {
                if (User.LoggedUser == null) return false;
                if (User.LoggedUser.UserType == UserType.Owner) return true;
                if (!UserSettingDictionary.TryGetValue(User.LoggedUser.UserType, out Dictionary<UserSetting, Boolean> userPermision)) return false;
                if (!userPermision.ContainsKey(userSetting)) return false;
                return userPermision[userSetting];
            }
            catch (Exception e) { MessageBox.Show("GetAccessUser: " + e.ToString()); return false; }
        }
    }
}
