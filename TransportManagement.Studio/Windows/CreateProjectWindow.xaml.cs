using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using TransportManagement.Studio.Project.Components;
using TransportManagement.Studio.Project.Management;

namespace TransportManagement.Studio.Windows
{
    /// <summary>
    /// Окно создания проекта.
    /// Логика взаимодействия для CreateProjectWindow.xaml
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog(this) == CommonFileDialogResult.Ok)
            {
                projectPathBox.Text = dialog.FileName;
            }
            else
            {

            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(projectNameBox.Text) && !string.IsNullOrEmpty(projectPathBox.Text))
            {
                string projectName = projectNameBox.Text;
                string projectPath = projectPathBox.Text.Replace("/", @"\");

                if (App.Version != null)
                {
                    var version = SerializableVersion.FromVersion(App.Version);

                    var projectInfo = new ProjectInfo
                    {
                        Name = projectName,
                        MinimalVersion = version,
                    };

                    ProjectManager.CreateProject(projectPath, projectInfo);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(projectNameBox.Text))
                {
                    // TODO: Вывести сообщение об ошибке.
                }
                else if (string.IsNullOrEmpty(projectPathBox.Text))
                {
                    // TODO: Вывести сообщение об ошибке.
                }
            }
        }
    }
}