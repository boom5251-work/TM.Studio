using System;
using System.Xml.Serialization;

namespace TransportManagement.Studio.Project.Components.Nodes
{
    /// <summary>
    /// Узел директории проекта.
    /// </summary>
    [Serializable]
    public sealed class DirectoryProjectNode : ProjectNode
    {
        /// <summary>
        /// Коллекция дочерних узлов корневого узла.
        /// </summary>
        public required ProjectNodesCollection ChildNodes { get; set; }


        [XmlIgnore]
        public override ProjectNodeType Type => ProjectNodeType.Folder;
    }
}