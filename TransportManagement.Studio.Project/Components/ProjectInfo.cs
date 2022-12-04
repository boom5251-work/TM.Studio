using System;

namespace TransportManagement.Studio.Project.Components
{
    /// <summary>
    /// Основная информация о проекте.
    /// </summary>
    [Serializable]
    public sealed class ProjectInfo
    {
        /// <summary>
        /// Название проекта.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Минимальная необходимая версия ПО.
        /// </summary>
        public required SerializableVersion MinimalVersion { get; set; }
    }
}