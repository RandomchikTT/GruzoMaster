using System;
using System.Collections.Generic;
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
        Created = 0,
        /// <summary>
        /// В обработке
        /// </summary>
        InProcessing = 1,
        /// <summary>
        /// Не успешно
        /// </summary>
        NotSuccessful = 2,
        /// <summary>
        /// Завершен
        /// </summary>
        Сompleted = 3,
    }
}
