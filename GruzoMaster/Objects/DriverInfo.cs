using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// Серия паспорта
        /// </summary>
        public String SerialPassport { get; set; }

        /// <summary>
        /// Номер паспорта
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
        /// <returns>Список объектов всех водителей</returns>
        public static async Task<List<Driver>> GetDrivers()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead("SELECT * FROM `drivers`");
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return new List<Driver>();
                }

                var driverInfos = new List<Driver>();
                foreach (DataRow row in dataTable.Rows)
                {
                    var listLicense = JsonConvert.DeserializeObject<List<License>>(row["ListLicenses"].ToString());
                    var numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, string>>(row["PhoneNumbers"].ToString());

                    driverInfos.Add(new Driver
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
                return driverInfos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"GetDrivers: {ex}");
                return new List<Driver>();
            }
        }

        /// <summary>
        /// Получение водителя по его ID
        /// </summary>
        /// <param name="id">Айди из базы данных</param>
        /// <returns>Объект класса Driver</returns>
        public static async Task<Driver> GetDriverById(int id)
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `drivers` WHERE `id`={id}");
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dataTable.Rows[0];
                var listLicense = JsonConvert.DeserializeObject<List<License>>(row["ListLicenses"].ToString());
                var numberCalls = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, string>>(row["PhoneNumbers"].ToString());

                return new Driver
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
            catch (Exception ex)
            {
                MessageBox.Show($"GetDriverById: {ex}");
                return null;
            }
        }
    }
}
