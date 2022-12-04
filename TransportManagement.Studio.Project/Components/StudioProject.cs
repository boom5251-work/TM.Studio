using System;

namespace TransportManagement.Studio.Project.Components
{
    /// <summary>
    /// Проект ПО TM.Studio.
    /// </summary>
    [Serializable]
    public sealed class StudioProject
    {
        /// <summary>
        /// Основная информация о проектк.
        /// </summary>
        public required ProjectInfo Info { get; init; }


        /// <summary>
        /// Коллекция узлов проекта.
        /// </summary>
        public required ProjectNodesCollection Nodes { get; init; }
    }
}