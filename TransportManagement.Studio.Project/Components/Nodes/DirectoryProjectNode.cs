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
        private ProjectNodeType type = ProjectNodeType.Directory;



        /// <summary>
        /// Коллекция дочерних узлов корневого узла.
        /// </summary>
        public required ProjectNodesCollection ChildNodes { get; set; }


        public override ProjectNodeType Type
        {
            get => type;
            set => type = value;
        }
    }
}