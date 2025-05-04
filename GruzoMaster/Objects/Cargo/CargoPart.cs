using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Objects.Cargo
{
    public class CargoPart
    {
        public Int64 ID { get; set; }
        public Int32 Transport { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Weight { get; set; }
        public int Volume { get; set; }
        public Int64 CargoID { get; set; }
        public CargoDeliveryType CargoDeliveryType { get; set; }
        public async Task SaveToDatabase()
        {
            try
            {
                Int64 ID = await MySQL.QueryLastInsertAsync($"INSERT INTO `cargo_parts` (`CargoID`, `TransportID`, `DeliveryDate`, `Weight`, `Volume`, `DeliveryType`) " +
                    $"VALUES ({CargoID}, {this.Transport}, '{this.DeliveryDate:yyyy-MM-dd}', {this.Weight}, {this.Volume}, {(Int32)this.CargoDeliveryType})");
                this.ID = ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SaveToDatabase: " + ex.ToString());
            }
        }
        public static async Task<List<CargoPart>> GetCargoPartsByOrderId(long id)
        {
            try
            {
                List<CargoPart> cargoParts = new List<CargoPart>();
                var result = await MySQL.QueryRead($"SELECT * FROM cargo_parts WHERE CargoID = {id}");

                foreach (DataRow row in result.Rows)
                {
                    cargoParts.Add(new CargoPart
                    {
                        CargoID = Convert.ToInt32(row["CargoID"]),
                        ID = Convert.ToInt32(row["ID"]),
                        Transport = Convert.ToInt32(row["TransportID"]),
                        DeliveryDate = Convert.ToDateTime(row["DeliveryDate"]),
                        Weight = Convert.ToInt32(row["Weight"]),
                        Volume = Convert.ToInt32(row["Volume"]),
                        CargoDeliveryType = (CargoDeliveryType)Convert.ToInt32(row["DeliveryType"])
                    });
                }

                return cargoParts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetCargoPartsByOrderId: " + ex.ToString());
                return null;
            }
        }
        public static async Task<CargoPart> GetCargoPartById(long id)
        {
            try
            {
                List<CargoPart> cargoParts = new List<CargoPart>();
                var result = await MySQL.QueryRead($"SELECT * FROM cargo_parts WHERE ID = {id}");
                if (result == null || result.Rows.Count <= 0) return null;
                CargoPart cargoPart = new CargoPart
                {
                    CargoID = Convert.ToInt32(result.Rows[0]["CargoID"]),
                    ID = Convert.ToInt32(result.Rows[0]["ID"]),
                    Transport = Convert.ToInt32(result.Rows[0]["TransportID"]),
                    DeliveryDate = Convert.ToDateTime(result.Rows[0]["DeliveryDate"]),
                    Weight = Convert.ToInt32(result.Rows[0]["Weight"]),
                    Volume = Convert.ToInt32(result.Rows[0]["Volume"]),
                    CargoDeliveryType = (CargoDeliveryType)Convert.ToInt32(result.Rows[0]["DeliveryType"])
                };
                return cargoPart;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetCargoPartById: " + ex.ToString());
                return null;
            }
        }
        public async Task UpdateInDatabase()
        {
            try
            {
                await MySQL.QueryAsync($"UPDATE `cargo_parts` SET " +
                    $"`TransportID` = {this.Transport}, " +
                    $"`DeliveryDate` = '{this.DeliveryDate:yyyy-MM-dd}', " +
                    $"`Weight` = {this.Weight}, " +
                    $"`Volume` = {this.Volume}, " +
                    $"`DeliveryType` = {(Int32)this.CargoDeliveryType} " +
                    $"WHERE `CargoID` = {CargoID} AND `ID` = {this.ID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("UpdateInDatabase: " + ex.ToString());
            }
        }
    }
}
