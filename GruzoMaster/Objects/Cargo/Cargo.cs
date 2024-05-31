using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Windows.Forms;

namespace GruzoMaster.Objects.Cargo
{
    public class Cargo
    {
        /// <summary>
        /// Айди груза
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// Компания заказчик
        /// </summary>
        public Company CustomerCompany { get; set; }
        /// <summary>
        /// Пользователь который создал заказа
        /// </summary>
        public User CreateUserCargo { get; set; }
        /// <summary>
        /// Экспедитор
        /// </summary>
        public User Forwarder { get; set; } = null;
        /// <summary>
        /// Транспорт выполняющий перевоз груза
        /// </summary>
        public Transport TransportCargo { get; set; }
        /// <summary>
        /// Название груза
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Описание груза
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Адрес начальной точки
        /// </summary>
        public String AddressFromCargo { get; set; }
        /// <summary>
        /// Адресс конечной точки
        /// </summary>
        public String AddressToCargo { get; set; }
        /// <summary>
        /// Водитель выполняющий перевозку
        /// </summary>
        public DriverInfo Driver { get; set; }
        /// <summary>
        /// Сумму которую получим по окончанию выполнения заказа
        /// </summary>
        public Int32 Price { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        public CargoDeliveryType DeliveryType { get; set; }
        /// <summary>
        /// Логирование про груз
        /// </summary>
        public List<CargoLog> CargoLogs { get; set; }
        public static string GetDeliveryTypeDescription(CargoDeliveryType DeliveryType)
        {
            switch (DeliveryType)
            {
                case CargoDeliveryType.InProcessing:
                    return "В обработке";
                case CargoDeliveryType.Сompleted:
                    return "Выполнено";
                case CargoDeliveryType.NotSuccessful:
                    return "Отменено";
                case CargoDeliveryType.Created:
                    return "Создан";
                default:
                    return "Неизвестный статус"; // Обработка неизвестных значений
            }
        }
        public static CargoDeliveryType GetCargoDeliveryTypeByName(String name)
        {
            foreach (CargoDeliveryType cargoDeliveryType in Enum.GetValues(typeof(CargoDeliveryType)))
            {
                if (GetDeliveryTypeDescription(cargoDeliveryType) == name)
                {
                    return cargoDeliveryType;
                }
            }
            return CargoDeliveryType.Created;
        }
        public async void Create()
        {
            try
            {
                Int32 idForwarder = this.Forwarder != null ? this.Forwarder.ID : -1;
                Int64 id = await MySQL.QueryLastInsertAsync($"INSERT INTO `cargo` (`ID_user_creator`,`ID_company`,`ID_Transport`,`Name`,`Description`," +
                    $"`AddressFromCargo`,`AddressToCargo`,`DriverID`,`Price`,`DeliveryType`,`CargoLogs`,`ForwarderID`) " +
                    $"VALUES ({this.CreateUserCargo.ID},{this.CustomerCompany.IdKey},{this.TransportCargo.IdKey}," +
                    $"'{this.Name}','{this.Description}','{this.AddressFromCargo}','{this.AddressToCargo}',{this.Driver.IdKey},{this.Price},{(Int32)this.DeliveryType}," +
                    $"'{JsonConvert.SerializeObject(this.CargoLogs)}',{idForwarder})");
                this.ID = id;
            }
            catch (Exception ex) { MessageBox.Show("Create: " + ex.ToString()); }
        }
    }
}