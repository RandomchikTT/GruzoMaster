﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <summary>
        /// Пользователь
        /// </summary>
        User = 0,
        /// <summary>
        /// Администратор
        /// </summary>
        Admin = 1,
        /// <summary>
        /// Владелец
        /// </summary>
        Owner = 2,
        /// <summary>
        /// Экспедитор
        /// </summary>
        Forwarder = 3,
    }
    public class User
    {
        /// <summary>
        /// Авторизованный пользователь
        /// </summary>
        public static User LoggedUser = null;
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public String Login { get; set; } = String.Empty;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public String Name { get; set; } = String.Empty;
        /// <summary>
        /// Уровень доступа пользователя
        /// </summary>
        public UserType UserType { get; set; } = UserType.User;
        /// <summary>
        /// Уникальный айди
        /// </summary>
        public Int32 ID { get; set; }
        /// <summary>
        /// Получение обьекта User по его ID
        /// </summary>
        /// <param name="id">ID из Базы Данных</param>
        /// <returns></returns>
        public static async Task<User> GetUserById(Int32 id)
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `users` WHERE `id`={id}");
                User user = null;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    user = new User()
                    {
                        ID = Convert.ToInt32(row["id"]),
                        Name = Convert.ToString(row["Name"]),
                        Login = Convert.ToString(row["Login"]),
                        UserType = (UserType)Convert.ToInt32(row["UserType"]),
                    };
                }
                return user;
            }
            catch (Exception e) { MessageBox.Show("GetUserById: " + e.ToString()); return null; }
        }
        public static async Task<List<User>> GetForwarderList()
        {
            List<User> list = new List<User>();
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `users` WHERE `UserType`={Convert.ToInt32(UserType.Admin)}");
                foreach (DataRow row in dataTable.Rows)
                {
                    String name = row["Name"].ToString();
                    Int32 id = Convert.ToInt32(row["id"].ToString());
                    list.Add(new User()
                    {
                        ID = id,
                        Name = name,
                        Login = Convert.ToString(row["Login"]),
                        UserType = (UserType)Convert.ToInt32(row["UserType"]),
                    });
                }
                return list;
            }
            catch (Exception ex) { MessageBox.Show("LoadFordwarderList: " + ex.ToString()); return list; }
        }
    }
}
