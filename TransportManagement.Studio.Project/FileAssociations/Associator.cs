using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace TransportManagement.Studio.Project.FileAssociations
{
    /// <summary>
    /// Отвечает за функции ассоциации файлов с определенными расширениями с текущим приложением.
    /// </summary>
    public static class Associator
    {
        /// <summary>
        /// Основной путь к ключам реестра.
        /// </summary>
        private static readonly string MAIN_PATH = """Software\Classes\""";



        /// <summary>
        /// Идентификатор события.
        /// </summary>
        private const int SHCNE_ASSOCCHANGED = 0x8000000;

        /// <summary>
        /// Флаги.
        /// </summary>
        private const int SHCNF_FLUSH = 0x1000;



        /// <summary>
        /// Уведомляет систему о событии, которое вызвало приложение.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <param name="flags">Флаги.</param>
        /// <param name="item1">Элемент 1.</param>
        /// <param name="item2">Элемент 2.</param>
        /// <returns></returns>
        [DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, nint item1, nint item2);


        /// <summary>
        /// Гарантирует создание ассоциаций с файлами.
        /// </summary>
        public static void EnsureAssociationsSet()
        {
            var serializer = new XmlSerializer(typeof(List<Association>));
            var resourceName = "TransportManagement.Studio.Project.FileAssociations.Data.FileAssociations.xml";

            List<Association>? associations = null;

            // Десеириализация данных из внедренного ресурса.
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                    associations = (List<Association>?)serializer.Deserialize(stream);
            }

            // Изменение данных ассоциаций.
            if (associations != null)
            {
                foreach (var association in associations)
                {
                    association.ExecutableFilePath = Environment.ProcessPath;
                    association.IconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, association.IconPath);
                }

                EnsureAssociationsSet(associations);
            }
        }


        /// <summary>
        /// Гарантирует создание ассоциаций с файлами.
        /// </summary>
        /// <param name="associations">Данные ассоциаций.</param>
        /// <returns>True, если были внесены какие-то изменения. False - нет.</returns>
        private static bool EnsureAssociationsSet(List<Association> associations)
        {
            bool result = false;

            foreach (var association in associations)
                result |= SetAssociation(association);

            if (result)
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, nint.Zero, nint.Zero);

            return result;
        }


        /// <summary>
        /// Создает ассоциацию с файлом.
        /// </summary>
        /// <param name="association">Данные ассоциации.</param>
        /// <returns>True, если были внесены какие-то изменения. False - нет.</returns>
        private static bool SetAssociation(Association association)
        {
            bool result = false;

            // Расширение.
            result |= SetKeyDefaultValue($"{MAIN_PATH}{association.Extension}", association.ProgramId);
            // Описание типа файла.
            result |= SetKeyDefaultValue($"{MAIN_PATH}{association.ProgramId}", association.FileTypeDescription);
            // Путь к исполняемому файлу.
            result |= SetKeyDefaultValue($@"{MAIN_PATH}{association.ProgramId}\shell\open\command", $"\"{association.ExecutableFilePath}\" \"%1\"");
            // Путь к иконке.
            result |= SetKeyDefaultValue($@"{MAIN_PATH}{association.ProgramId}\DefaultIcon", association.IconPath);

            return result;
        }


        /// <summary>
        /// Устанавливает значение в реестре.
        /// </summary>
        /// <param name="keyPath">Путь ключа реестра.</param>
        /// <param name="value">Значение.</param>
        /// <returns>True, если значение в реестре изменено. False - нет.</returns>
        private static bool SetKeyDefaultValue(string keyPath, string value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(keyPath))
            {
                if (key.GetValue(null) as string != value)
                {
                    key.SetValue(null, value);
                    return true;
                }
            }

            return false;
        }
    }
}