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
        /// <summary>
        /// Настройки для запоминания данных при авторизации
        /// </summary>
        public AuthData AuthorizationData { get; set; } = null;
        /// <summary>
        /// Функция сохранения хранилища
        /// </summary>
        public static void SaveStorageInstance()
        {
            try
            {
                File.WriteAllText(PathToStorage, JsonConvert.SerializeObject(StorageInstance, Formatting.None));
            }
            catch (Exception ex) { MessageBox.Show("SaveStorageInstance: " + ex.ToString()); }
        }
        /// <summary>
        /// Функция создания хранилища
        /// </summary>
        public static void CreateStorage()
        {
            try
            {
                StorageInstance = new Storage();
                SaveStorageInstance();
            }
            catch (Exception e) { MessageBox.Show("CreateStorage: " + e.ToString()); }
        }
        /// <summary>
        /// Функция загрузки Хранилища
        /// </summary>
        public static void LoadStorageInstance()
        {
            try
            {
                if (!File.Exists(PathToStorage))
                {
                    CreateStorage();
                    return;
                }
                try
                {
                    StorageInstance = JsonConvert.DeserializeObject<Storage>(File.ReadAllText(PathToStorage));
                }
                catch
                {
                    CreateStorage();
                }
            }
            catch (Exception ex) { MessageBox.Show("LoadStorageInstance: " + ex.ToString()); }
        }
    }
}
