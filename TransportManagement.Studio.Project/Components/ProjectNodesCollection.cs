using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TransportManagement.Studio.Project.Components.Nodes;

namespace TransportManagement.Studio.Project.Components
{
    /// <summary>
    /// Коллекция узлов проекта.
    /// </summary>
    [Serializable]
    public sealed class ProjectNodesCollection : IList<ProjectNode>
    {
        /// <summary>
        /// Элементы коллеции.
        /// </summary>
        private ProjectNode[] _nodes = new ProjectNode[0];



        #region IList
        /// <summary>
        /// Возвращает узел проекты по индексу.
        /// </summary>
        /// <param name="index">Индекс элемента.</param>
        /// <returns>Узел проекта.</returns>
        public ProjectNode this[int index]
        {
            get => _nodes[index];
            set => _nodes[index] = value;
        }

        /// <summary>
        /// Количество элементов в коллекции.
        /// </summary>
        public int Count => _nodes.Length;

        /// <summary>
        /// Указывает, доступна ли коллекция только для чтения.
        /// </summary>
        public bool IsReadOnly => false;



        /// <summary>
        /// Добавляет узел проекта в коллекцию.
        /// </summary>
        /// <param name="item">Узел проека.</param>
        public void Add(ProjectNode item)
        {
            Array.Resize(ref _nodes, _nodes.Length + 1);
            _nodes[Count - 1] = item;
        }


        /// <summary>
        /// Удалаяет все узлы проекта из коллеции.
        /// </summary>
        public void Clear() => Array.Clear(_nodes);


        /// <summary>
        /// Проверяет наличие узла в коллекции.
        /// </summary>
        /// <param name="item">Искомый узел.</param>
        /// <returns>True, если узел найден. False - нет.</returns>
        public bool Contains(ProjectNode item) => _nodes.Contains(item);


        /// <summary>
        /// Копирует элементы коллекции в массив, начиная с указанного индекса.
        /// </summary>
        /// <param name="array">Массив для компирования узлов проекта.</param>
        /// <param name="arrayIndex">Индекс.</param>
        public void CopyTo(ProjectNode[] array, int arrayIndex) => _nodes.CopyTo(array, arrayIndex);


        /// <summary>
        /// Возвращает перечислитель коллекции обобщенного типа.
        /// </summary>
        /// <returns>Перечислитель коллекции обобщенного типа.</returns>
        public IEnumerator<ProjectNode> GetEnumerator() => _nodes.ToList().GetEnumerator();


        /// <summary>
        /// Возвращает индекс первого вхождения узла проекта в коллеции.
        /// </summary>
        /// <param name="item">Узел проекта, индекс которого необходимо получить.</param>
        /// <returns>Индекс узла проекта.</returns>
        public int IndexOf(ProjectNode item) => _nodes.ToList().IndexOf(item);


        /// <summary>
        /// Вставляет узел проекта по указанному индексу.
        /// </summary>
        /// <param name="index">Узел проекта.</param>
        /// <param name="item">Индекс.</param>
        public void Insert(int index, ProjectNode item) => _nodes.ToList().Insert(index, item);


        /// <summary>
        /// Удаляет первое вхождение узла проекта в коллекции.
        /// </summary>
        /// <param name="item">Узел проекта, который необходимо удалить.</param>
        /// <returns>True, если узел удален. False - нет.</returns>
        public bool Remove(ProjectNode item)
        {
            var list = _nodes.ToList();
            bool result = list.Remove(item);
            _nodes = list.ToArray();
            return result;
        }


        /// <summary>
        /// Удаляет узел проекта из коллеции по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        public void RemoveAt(int index)
        {
            var list = new List<ProjectNode>(_nodes);
            list.RemoveAt(index);
            _nodes = list.ToArray();
        }


        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>Перечислитель коллекции.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
        #endregion
    }
}