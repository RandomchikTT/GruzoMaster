using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GruzoMaster.Objects.Cargo
{
    public class Cargo
    {
        /// <summary>
        /// Айди груза
        /// </summary>
        public Int32 ID { get; set; }
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
        public string GetDeliveryTypeDescription()
        {
            switch (this.DeliveryType)
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
    }
}