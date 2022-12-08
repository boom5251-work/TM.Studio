using System;
using System.Xml.Serialization;

namespace TransportManagement.Studio.Project.Components.Nodes
{
    /// <summary>
    /// Узел проекта.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(DirectoryProjectNode))]
    [XmlInclude(typeof(RootProjectNode))]
    public abstract class ProjectNode
    {
        /// <summary>
        /// Имя узла.
        /// </summary>
        public required string Name { get; set; }


        /// <summary>
        /// Путь к файлу или папке.
        /// </summary>
        public required string Path { get; set; }


        /// <summary>
        /// Тип узла проекта.
        /// </summary>
        public abstract ProjectNodeType Type { get; set; }
    }
}