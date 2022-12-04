using System;

namespace TransportManagement.Studio.Project.Components
{
    /// <summary>
    /// Версия предназначенная для сериализации.
    /// </summary>
    [Serializable]
    public sealed class SerializableVersion : ICloneable, IComparable
    {
        /// <summary>
        /// Основной номер (требуется значение).
        /// </summary>
        public required int Major { get; set; }

        /// <summary>
        /// Младший номер (требуется значение)
        /// </summary>
        public required int Minor { get; set; }



        /// <summary>
        /// Преобразует системную версию в сериализуемую версию
        /// </summary>
        /// <param name="version">Системная версия сборки.</param>
        /// <returns>Сериализуемая версия.</returns>
        public static SerializableVersion FromVersion(Version version)
        {
            return new SerializableVersion
            {
                Major = version.Major,
                Minor = version.Minor
            };
        }


        /// <summary>
        /// Создает копию текущего экземпляра объекта.
        /// </summary>
        /// <returns>Копия текущего экземпляра SerializableVersion.</returns>
        public object Clone()
        {
            return new SerializableVersion
            {
                Major = Major,
                Minor = Minor
            };
        }


        /// <summary>
        /// Сравнивает версию с текущей.
        /// </summary>
        /// <param name="obj">Вервия.</param>
        /// <returns>Сравнение.</returns>
        public int CompareTo(object? obj)
        {
            SerializableVersion? version = obj as SerializableVersion;

            if (Major > version?.Major)
            {
                return 1;
            }
            else if (Major < version?.Major)
            {
                return -1;
            }
            else
            {
                if (Minor > version?.Minor)
                    return 1;
                else if (Minor < version?.Minor)
                    return -1;
                else
                    return 0;
            }
        }


        /// <summary>
        /// Хранит хеш-функцию для этого объекта.
        /// </summary>
        /// <returns>Хеш-код.</returns>
        public override int GetHashCode()
        {
            int hashcode = (23 * 31 + Major) * 31 + Minor;
            return hashcode;
        }


        /// <summary>
        /// Определяет, равен ли указанная текущей версии.
        /// </summary>
        /// <param name="obj">Вервия.</param>
        /// <returns>Результат сравнения.</returns>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is SerializableVersion)
            {
                var version = (SerializableVersion)obj;
                return this == version;

            }
            else return false;
        }



        /// <summary>
        /// Проверяет равенство версий.
        /// </summary>
        /// <param name="v1">Версия 1.</param>
        /// <param name="v2">Версия 2.</param>
        /// <returns>Результат сравнения.</returns>
        public static bool operator ==(SerializableVersion v1, SerializableVersion v2) =>
            v1.Major == v2.Major && v1.Minor == v1.Minor;

        /// <summary>
        /// Проверяет неравенство версий.
        /// </summary>
        /// <param name="v1">Версия 1.</param>
        /// <param name="v2">Версия 2.</param>
        /// <returns>Результат сравнения.</returns>
        public static bool operator !=(SerializableVersion v1, SerializableVersion v2) =>
            v1.Major != v2.Major || v1.Minor != v2.Minor;

        /// <summary>
        /// Проверяет меньше ли первая версия второй.
        /// </summary>
        /// <param name="v1">Версия 1.</param>
        /// <param name="v2">Версия 2.</param>
        /// <returns>Результат сравнения.</returns>
        public static bool operator <(SerializableVersion v1, SerializableVersion v2) =>
            v1.Major < v2.Major || (v1.Major == v2.Major && v1.Minor < v2.Minor);

        /// <summary>
        /// Проверяет больше ли первая версия второй.
        /// </summary>
        /// <param name="v1">Версия 1.</param>
        /// <param name="v2">Версия 2.</param>
        /// <returns>Результат сравнения.</returns>
        public static bool operator >(SerializableVersion v1, SerializableVersion v2) =>
            v1.Major > v2.Major || (v1.Major == v2.Major && v1.Minor > v2.Minor);

        /// <summary>
        /// Проверяет меньше или равна ли первая версия второй.
        /// </summary>
        /// <param name="v1">Версия 1.</param>
        /// <param name="v2">Версия 2.</param>
        /// <returns>Результат сравнения.</returns>
        public static bool operator <=(SerializableVersion v1, SerializableVersion v2) =>
            v1 < v2 || v1 == v2;

        /// <summary>
        /// Проверяет больше или ровна ли первая версия второй.
        /// </summary>
        /// <param name="v1">Версия 1.</param>
        /// <param name="v2">Версия 2.</param>
        /// <returns>Результат сравнения.</returns>
        public static bool operator >=(SerializableVersion v1, SerializableVersion v2) =>
            v1 > v2 || v1 == v2;
    }
}