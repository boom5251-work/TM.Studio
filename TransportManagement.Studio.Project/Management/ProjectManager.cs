using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using TransportManagement.Studio.Project.Components;
using TransportManagement.Studio.Project.Components.Nodes;

namespace TransportManagement.Studio.Project.Management
{
    /// <summary>
    /// Отвечает за создание и открытие проектов.
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// Создает новый проект.
        /// </summary>
        /// <param name="path">Путь к файлам проекта.</param>
        /// <param name="projectInfo">Информация о проекте.</param>
        /// <returns>Проект.</returns>
        public static StudioProject CreateProject(string path, ProjectInfo projectInfo)
        {
            // Создание папок в случае необходимости.
            string rootPath = Path.Combine(path, $"""{projectInfo.Name}""");
            string outputPath = Path.Combine(rootPath, """Output""");

            var project = CreateDefaultStudioProject(rootPath, projectInfo.Name);

            Directory.CreateDirectory(rootPath);
            Directory.CreateDirectory(outputPath);
                

            // Создание файла проекта.
            string projectFilePath = Path.Combine(rootPath, $"""{projectInfo.Name}.tmsproj""");

            using (var fs = File.Create(projectFilePath))
            {
                var serializer = new XmlSerializer(typeof(StudioProject));
                serializer.Serialize(fs, project);
            }

            return project;
        }


        /// <summary>
        /// Создает и инициализирует коллекцию узлов по умолчанию.
        /// </summary>
        /// <param name="rootPath">Путь </param>
        /// <param name="projectName">Название проекта.</param>
        /// <returns>Коллекция узлов по умолчанию.</returns>
        private static StudioProject CreateDefaultStudioProject(string rootPath, string projectName)
        {
            var serializer = new XmlSerializer(typeof(StudioProject));
            string resourceName = "TransportManagement.Studio.Project.Management.Templates.ProjectFileTemplate.xml";

            StudioProject? projectTemplate = null;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                    projectTemplate = (StudioProject?)serializer.Deserialize(stream);
            }

            if (projectTemplate != null)
            {
                projectTemplate.Info.Name = projectName;

                projectTemplate.Nodes[0].Name = projectName;
                projectTemplate.Nodes[0].Path = rootPath;
                ((RootProjectNode)projectTemplate.Nodes[0]).ChildNodes[0].Path = Path.Combine(rootPath, "Output");

                return projectTemplate;
            }
            else
            {
                throw new InvalidDataException();
            }
        }


        /// <summary>
        /// Открывает файл проека.
        /// </summary>
        /// <param name="filePath">Путь к файлу проетка.</param>
        /// <returns>Проект.</returns>
        public static StudioProject? OpenProject(string filePath)
        {
            var serializer = new XmlSerializer(typeof(StudioProject));

            using (var stream = File.Open(filePath, FileMode.Open))
            {
                StudioProject? project = (StudioProject?)serializer.Deserialize(stream);
                return project;
            }
        }
    }
}