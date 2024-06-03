using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Objects
{
    public class Transport
    {
        public enum TransportModel : Int32
        {
            /// <summary>
            /// Ничего если не выбрано
            /// </summary>
            None = 0,
            Iveco = 1,
            DAF = 2,
            Scania = 3,
            Mercedes = 4,
            Sitrak = 5,
        }
        public enum TransportType : Int32
        {
            /// <summary>
            /// Ничего если не выбрано
            /// </summary>
            None = 0,
            /// <summary>
            /// Сядельный тягач
            /// </summary>
            TruckTractor = 1,
            /// <summary>
            /// Грузовик
            /// </summary>
            Truck = 2,
            /// <summary>
            /// Лёгкие коммерческие
            /// </summary>
            LightCommercial = 3,
        }
        /// <summary>
        /// Модель машины
        /// </summary>
        public TransportModel TransportModelName { get; set; }
        /// <summary>
        /// Тип Транспорта
        /// </summary>
        public TransportType TransportTypeName { get; set; }
        /// <summary>
        /// Время действия тех осмотра
        /// </summary>
        public DateTime TimeTechInspection { get; set; }
        /// <summary>
        /// Модель транпсорта
        /// </summary>
        public String ModelDescriptionName { get; set; }
        /// <summary>
        /// Гос Номер
        /// </summary>
        public String GovNumber { get; set; }
        /// <summary>
        /// Уникальный айди транспорта
        /// </summary>
        public Int32 IdKey { get; set; }
        /// <summary>
        /// Получение списка всего транспорта
        /// </summary>
        /// <returns>Список обьектов транспорта</returns>
        public static async Task<List<Transport>> GetTransports()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport`");
                List<Transport> transport = new List<Transport>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        transport.Add(new Transport()
                        {
                            IdKey = Convert.ToInt32(row["id"]),
                            TransportModelName = (Transport.TransportModel)Convert.ToInt32(row["Brand"]),
                            ModelDescriptionName = Convert.ToString(row["Model"]),
                            TransportTypeName = (Transport.TransportType)Convert.ToInt32(row["Type"]),
                            GovNumber = Convert.ToString(row["GovNumber"]),
                            TimeTechInspection = Convert.ToDateTime(row["TechInspection"])
                        });
                    }
                }
                return transport;
            }
            catch (Exception ex) { MessageBox.Show("GetTransports: " + ex.ToString()); return new List<Transport>(); }
        }
        /// <summary>
        /// Получение транспорта по его ID
        /// </summary>
        /// <param name="id">ID из базы данных</param>
        /// <returns>Обьект Transport</returns>
        public static async Task<Transport> GetTransportById(Int32 id)
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `transport` WHERE `id`={id}");
                Transport transport = null;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    transport = new Transport()
                    {
                        IdKey = Convert.ToInt32(row["id"]),
                        TransportModelName = (Transport.TransportModel)Convert.ToInt32(row["Brand"]),
                        ModelDescriptionName = Convert.ToString(row["Model"]),
                        TransportTypeName = (Transport.TransportType)Convert.ToInt32(row["Type"]),
                        GovNumber = Convert.ToString(row["GovNumber"]),
                        TimeTechInspection = Convert.ToDateTime(row["TechInspection"])
                    };
                }
                return transport;
            }
            catch (Exception ex) { MessageBox.Show("GetTransportById: " + ex.ToString()); return null; }
        }
    }
}
