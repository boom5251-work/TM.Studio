using System;
using System.Xml.Serialization;

namespace TransportManagement.Studio.Project.FileAssociations
{
    /// <summary>
    /// Данные ассоциации файла.
    /// </summary>
    [Serializable]
    public sealed class Association
    {
        /// <summary>
        /// Расширение файла.
        /// </summary>
        public required string Extension { get; set; }

        /// <summary>
        /// Идентификатор программы.
        /// </summary>
        public required string ProgramId { get; set; }

        /// <summary>
        /// Описание типв файла.
        /// </summary>
        public required string FileTypeDescription { get; set; }

        /// <summary>
        /// Путь к иконке.
        /// </summary>
        public required string IconPath { get; set; }

        /// <summary>
        /// Путь к исполняемому файлу.
        /// </summary>
        [XmlIgnore]
        public string? ExecutableFilePath { get; set; }
    }
}