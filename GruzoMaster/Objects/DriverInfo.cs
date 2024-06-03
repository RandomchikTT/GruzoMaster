using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public class Driver
    {
        /// <summary>
        /// Полное имя
        /// </summary>
        public String FullName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// Действие мед справки
        /// </summary>
        public DateTime MedSpavka { get; set; }
        /// <summary>
        /// Список открытых лицензий
        /// </summary>
        public List<License> ListLicense { get; set; }
        /// <summary>
        /// Серия пасспорта
        /// </summary>
        public String SerialPassport { get; set; }
        /// <summary>
        /// Номер пасспорта
        /// </summary>
        public String NumberPassport { get; set; }
        /// <summary>
        /// Телефоны
        /// </summary>
        public Dictionary<PhoneNumber, String> PhoneNumbers { get; set; }
        /// <summary>
        /// Адрес проживания
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// Уникальный айди
        /// </summary>
        public Int32 IdKey { get; set; }
        /// <summary>
        /// Получение списка водителей
        /// </summary>
        /// <returns>Список обьектов всех водителей</returns>
        public static async Task<List<Driver>> GetDrivers()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `drivers`");
                List<Driver> driverInfos = new List<Driver>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<License> listLicense = JsonConvert.DeserializeObject<List<License>>(row["ListLicenses"].ToString());
                        Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(row["PhoneNumbers"].ToString());
                        driverInfos.Add(new Driver()
                        {
                            FullName = Convert.ToString(row["FullName"]),
                            BirthDay = Convert.ToDateTime(row["DateBirthday"]),
                            MedSpavka = Convert.ToDateTime(row["MedSpravka"]),
                            ListLicense = listLicense,
                            PhoneNumbers = numberCalls,
                            SerialPassport = Convert.ToString(row["SerialPassport"]),
                            NumberPassport = Convert.ToString(row["NumberPassport"]),
                            Address = Convert.ToString(row["Address"]),
                            IdKey = Convert.ToInt32(row["id"]),
                        });
                    }
                }
                return driverInfos;
            }
            catch (Exception ex) { MessageBox.Show("GetDriverById: " + ex.ToString()); return new List<Driver>(); }
        }
        /// <summary>
        /// Получение водителя по его ID
        /// </summary>
        /// <param name="id">Айди из базы Данных</param>
        /// <returns>Обьект класса DriverInfo</returns>
        public static async Task<Driver> GetDriverById(Int32 id)
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={id}");
                Driver driverInfo = null;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    List<License> listLicense = JsonConvert.DeserializeObject<List<License>>(row["ListLicenses"].ToString());
                    Dictionary<PhoneNumber, String> numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(row["PhoneNumbers"].ToString());
                    driverInfo = new Driver()
                    {
                        FullName = Convert.ToString(row["FullName"]),
                        BirthDay = Convert.ToDateTime(row["DateBirthday"]),
                        MedSpavka = Convert.ToDateTime(row["MedSpravka"]),
                        ListLicense = listLicense,
                        PhoneNumbers = numberCalls,
                        SerialPassport = Convert.ToString(row["SerialPassport"]),
                        NumberPassport = Convert.ToString(row["NumberPassport"]),
                        Address = Convert.ToString(row["Address"]),
                        IdKey = Convert.ToInt32(row["id"]),
                    };
                }
                return driverInfo;
            }
            catch (Exception ex) { MessageBox.Show("GetDriverById: " + ex.ToString()); return null; }
        }
    }
}
