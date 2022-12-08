using System;
using System.Xml.Serialization;

namespace TransportManagement.Studio.Project.Components.Nodes
{
    /// <summary>
    /// Корневой узел проекта.
    /// </summary>
    [Serializable]
    public sealed class RootProjectNode : ProjectNode
    {
        private ProjectNodeType type = ProjectNodeType.Root;



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