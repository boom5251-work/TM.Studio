using System;
using System.IO;
using System.Reflection;
using System.Windows;
using TransportManagement.Studio.Project.Components;
using TransportManagement.Studio.Project.FileAssociations;
using TransportManagement.Studio.Project.Management;

namespace TransportManagement.Studio
{
    /// <summary>
    /// Логика взаимодействия для App.xaml.
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // TODO: Добавить логику запуска ассоциаций.
            Associator.EnsureAssociationsSet();
        }

        

        /// <summary>
        /// Версия текущей сборки.
        /// </summary>
        public static Version? Version => Assembly.GetExecutingAssembly().GetName().Version;



        /// <summary>
        /// Вызывает событие Startup.
        /// </summary>
        /// <param name="e">Аргументы.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (!TryOpenProject(out StudioProject? project))
            {
                new MainWindow().Show();
            }
            else
            {
                new MainWindow(project).Show();
            }
        }


        /// <summary>
        /// Пытается открыть проект по пути из агрументов командной строки.
        /// </summary>
        /// <param name="project">Проект.</param>
        /// <returns>True, если файл проекта удалось открыть. False - нет.</returns>
        private bool TryOpenProject(out StudioProject? project)
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length >= 2 && File.Exists(args[1]))
            {
                project = ProjectManager.OpenProject(args[1]);
                return true;
            }
            else
            {
                project = null;
                return false;
            }
        }
    }
}