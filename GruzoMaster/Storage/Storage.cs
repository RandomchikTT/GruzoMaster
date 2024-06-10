using GruzoMaster.Storage.Objects;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace GruzoMaster.Storage
{
    public class Storage
    {
        /// <summary>
        /// Путь до файла с хар-ками
        /// </summary>
        private const string PathToStorage = "./storage.json";

        /// <summary>
        /// Текущий конфиг пользователя
        /// </summary>
        public static Storage StorageInstance { get; private set; } = null;

        /// <summary>
        /// Настройки для запоминания данных при авторизации
        /// </summary>
        public AuthData AuthorizationData { get; set; } = null;

        /// <summary>
        /// Сохраняет текущее состояние хранилища в файл.
        /// </summary>
        public static void SaveStorageInstance()
        {
            try
            {
                String json = JsonConvert.SerializeObject(StorageInstance, Formatting.None);
                File.WriteAllText(PathToStorage, json);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("SaveStorageInstance", ex);
            }
        }

        /// <summary>
        /// Создает новое хранилище и сохраняет его в файл.
        /// </summary>
        public static void CreateStorage()
        {
            try
            {
                StorageInstance = new Storage();
                SaveStorageInstance();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("CreateStorage", ex);
            }
        }

        /// <summary>
        /// Загружает текущее состояние хранилища из файла.
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

                var json = File.ReadAllText(PathToStorage);
                StorageInstance = JsonConvert.DeserializeObject<Storage>(json) ?? new Storage();
                SaveStorageInstance();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("LoadStorageInstance", ex);
            }
        }

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>
        /// <param name="methodName">Имя метода, в котором произошла ошибка.</param>
        /// <param name="ex">Исключение.</param>
        private static void ShowErrorMessage(string methodName, Exception ex)
        {
            MessageBox.Show($"{methodName}: {ex}");
        }
    }
}
