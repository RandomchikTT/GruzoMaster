using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GruzoMaster.Objects.Cargo
{
    public class CargoLog
    {
        /// <summary>
        /// Пользователь кто сделал лог
        /// </summary>
        public User UserCreated { get; set; }
        /// <summary>
        /// Что сделал
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Время создания лога
        /// </summary>
        public DateTime TimeCreated { get; set; } = DateTime.Now;
    }
}
