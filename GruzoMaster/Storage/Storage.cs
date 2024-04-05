using GruzoMaster.Storage.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster.Storage
{
    public class Storage
    {
        /// <summary>
        /// Путь до файла с хар-ками
        /// </summary>
        const String PathToStorage = "./storage.json";
        /// <summary>
        /// Текущий конфиг пользователя
        /// </summary>
        public static Storage StorageInstance { get; private set; } = null;
        public AuthData AuthorizationData { get; set; } = null;
        public static void SaveStorageInstance()
        {
            try
            {
                File.WriteAllText(PathToStorage, JsonConvert.SerializeObject(StorageInstance, Formatting.None));
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.ToString()); }
        }
        public static void LoadStorageInstance()
        {
            try
            {
                if (!File.Exists(PathToStorage))
                {
                    StorageInstance = new Storage();
                    SaveStorageInstance();
                    return;
                }
                StorageInstance = JsonConvert.DeserializeObject<Storage>(File.ReadAllText(PathToStorage));
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.ToString()); }
        }
    }
}
