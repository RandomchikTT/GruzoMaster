using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TransportModel TransportModelName { get; set; }
        public TransportType TransportTypeName { get; set; }
        public DateTime TimeTechInspection { get; set; }
        public String ModelDescriptionName { get; set; }
        public String GovNumber { get; set; }
        public Int32 IdKey { get; set; }
    }
}
