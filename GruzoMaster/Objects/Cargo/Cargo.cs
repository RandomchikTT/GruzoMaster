using GruzoMaster.CargoMenu;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
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
        public DateTime DeadlineTime { get; set; }
        public List<CargoPart> CargoParts { get; set; } = new List<CargoPart>();
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
        public async Task<Boolean> SplitCargo(int totalWeight, int totalVolume)
        {
            int remainingWeight = totalWeight;
            int remainingVolume = totalVolume;
            CargoParts.Clear(); // Очистим список перед новым распределением

            List<Transport> allVehicles = await Transport.GetTransports();
            if (allVehicles.Count <= 0)
            {
                MessageBox.Show("У вас нету транспорта !");
                return false;
            }
            DateTime deliveryDate = DateTime.Now; // Начнем с текущей даты
            List<CargoPart> cargoParts = new List<CargoPart>();
            List<Cargo> cargoOrders = await MainCargoMenu.GetCargoList();
            while (remainingWeight > 0 || remainingVolume > 0)
            {
                List<Transport> availableVehicles = await GetAvailableVehicles(cargoOrders, deliveryDate, allVehicles);
                if (deliveryDate > this.DeadlineTime)
                {
                    MessageBox.Show("Не хватает машин или все машины заняты для указанного дедлайна.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;   
                }
                foreach (Transport currentVehicle in availableVehicles)
                {
                    int assignableWeight = Math.Min(remainingWeight, currentVehicle.Capacity);
                    int assignableVolume = Math.Min(remainingVolume, currentVehicle.Volume);
                    if (remainingVolume <= 0 && remainingWeight <= 0) break;
                    cargoParts.Add(new CargoPart
                    {
                        CargoID = this.ID,
                        Transport = currentVehicle.IdKey,
                        DeliveryDate = deliveryDate,
                        Weight = assignableWeight,
                        CargoDeliveryType = CargoDeliveryType.Created,
                        Volume = assignableVolume
                    });
                    remainingWeight -= assignableWeight;
                    remainingVolume -= assignableVolume;
                }
                deliveryDate = deliveryDate.AddDays(1);
            }
            CargoParts = cargoParts;
            return true;
        }


        public async Task<List<Transport>> GetAvailableVehicles(List<Cargo> cargoOrders, DateTime date, List<Transport> allVehicles)
        {
            if (!cargoOrders.Any(c => c.CargoParts.Any(p => p.DeliveryDate == date)))
            {
                return allVehicles;
            }
            List<Transport> busyVehicleIds = cargoOrders
                .Where(c => c.CargoParts.Any(p => p.DeliveryDate == date)) // Проверяем, есть ли заказы на эту дату
                .SelectMany(c => c.CargoParts) // Берем все части грузов
                .Where(p => p.DeliveryDate == date) // Фильтруем по дате
                .GroupBy(p => p.Transport) // Группируем по транспорту
                .Where(g =>
                {
                    Transport vehicle = allVehicles.Find(x => x.IdKey == g.Key);
                    if (vehicle == null) return false;

                    int totalWeight = g.Sum(p => p.Weight);
                    int totalVolume = g.Sum(p => p.Volume);

                    return totalWeight >= vehicle.Capacity || totalVolume >= vehicle.Volume;
                })
                .Select(g => allVehicles.Find(x => x.IdKey == g.Key))
                .Where(v => v != null)
                .ToList();


            return allVehicles.Where(v => !busyVehicleIds.Contains(v)).ToList();
        }



        public async Task<Boolean> Create(int totalWeight, int totalVolume)
        {
            try
            {
                Boolean isSplited = await this.SplitCargo(totalWeight, totalVolume);
                if (!isSplited)
                {
                    return false;
                }
                Int32 idForwarder = this.Forwarder != null ? this.Forwarder.ID : -1;

                long newCargoId = await MySQL.QueryLastInsertAsync($@"
                    INSERT INTO `cargo`
                    (
                        `ID`, 
                        `ID_user_creator`, 
                        `ID_company`, 
                        `Name`, 
                        `Description`, 
                        `AddressFromCargo`, 
                        `AddressToCargo`, 
                        `Price`, 
                        `DeliveryType`, 
                        `CargoLogs`, 
                        `ForwarderID`,
                        'DeadlineTime'
                    ) 
                    VALUES 
                    (
                        {this.ID}, 
                        {this.CreateUserCargo.ID}, 
                        {this.CustomerCompany.IdKey}, 
                        '{this.Name}', 
                        '{this.Description}', 
                        '{this.AddressFromCargo}', 
                        '{this.AddressToCargo}', 
                        {this.Price}, 
                        {(int)this.DeliveryType}, 
                        '{JsonConvert.SerializeObject(this.CargoLogs)}', 
                        {idForwarder},
                        {this.DeadlineTime}
                    );
                ");

                this.ID = newCargoId;
                foreach (var part in CargoParts)
                {
                    part.CargoID = newCargoId;
                    await part.SaveToDatabase();
                }
                return true;
            }
            catch (Exception ex) { MessageBox.Show("Create: " + ex.ToString()); return false; }
        }
        public async void Update()
        {
            try
            {
                Int32 idForwarder = this.Forwarder != null ? this.Forwarder.ID : -1;
                await MySQL.QueryAsync($"UPDATE `cargo` SET " +
                        $"`ID_user_creator` = {this.CreateUserCargo.ID}, " +
                        $"`ID_company` = {this.CustomerCompany.IdKey}, " +
                        $"`Name` = '{this.Name}', " +
                        $"`Description` = '{this.Description}', " +
                        $"`AddressFromCargo` = '{this.AddressFromCargo}', " +
                        $"`AddressToCargo` = '{this.AddressToCargo}', " +
                        $"`Price` = {this.Price}, " +
                        $"`DeliveryType` = {(Int32)this.DeliveryType}, " +
                        $"`DeadlineTime` = {this.DeadlineTime}, " +
                        $"`CargoLogs` = '{JsonConvert.SerializeObject(this.CargoLogs)}', " +
                        $"`ForwarderID` = {idForwarder} " +
                        $"WHERE `ID` = {this.ID}");
                foreach (var part in CargoParts)
                {
                    await part.UpdateInDatabase();
                }
            }
            catch (Exception ex) { MessageBox.Show("Update: " + ex.ToString()); }
        }
    }
}