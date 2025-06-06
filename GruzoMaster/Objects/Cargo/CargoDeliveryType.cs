using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruzoMaster.Objects.Cargo
{
    public enum CargoDeliveryType : Int32
    {
        /// <summary>
        /// Заказ создан
        /// </summary>
        [Description("Заказ создан")]
        Created = 0,
        /// <summary>
        /// В обработке
        /// </summary>
        [Description("В обработке")]
        InProcessing = 1,
        /// <summary>
        /// Не успешно
        /// </summary>
        [Description("Не успешно")]
        NotSuccessful = 2,
        /// <summary>
        /// Завершен
        /// </summary>
        [Description("Завершен")]
        Сompleted = 3,
    }
}
