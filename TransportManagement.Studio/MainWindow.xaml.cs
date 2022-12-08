using Core.Controls.ObjectExplorer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TransportManagement.Studio.Project.Components;
using TransportManagement.Studio.Project.Components.Nodes;
using TransportManagement.Studio.Windows;

namespace TransportManagement.Studio
{
    /// <summary>
    /// Главное окно приложения.<br />
    /// Логика взаимодействия для MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetTitleBarIcon();
        }


        public MainWindow(StudioProject? project) : this()
        {
            Project = project;
            Loaded += MainWindow_Loaded;
        }



        /// <summary>
        /// Текущий проект.
        /// </summary>
        private StudioProject? Project { get; set; }



        /// <summary>
        /// Обрабатывает событие загрузки окна.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Агрументы.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProject();
        }



        #region Функции кнопок шапки страницы.
        /// <summary>
        /// Устанавливает икноки кнопок шапки.
        /// </summary>
        private void SetTitleBarIcon()
        {
            // Установка иконки кнопки "развернуть".
            if (WindowState == WindowState.Maximized)
                ((Image)maximizeButton.Content).Source = (BitmapImage)Application.Current.Resources["NormalizeIcon"];
            else
                ((Image)maximizeButton.Content).Source = (BitmapImage)Application.Current.Resources["MaximizeIcon"];
        }


        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "аккаунт".
        /// </summary>
        /// <param name="sender">Объект вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Реализовать.
        }


        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "свернуть".
        /// </summary>
        /// <param name="sender">Объект вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "развернуть".
        /// </summary>
        /// <param name="sender">Объект вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }


        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "закрыть".
        /// </summary>
        /// <param name="sender">Объект вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        /// <summary>
        /// Обрабатывает событие изменения размеров окна.
        /// </summary>
        /// <param name="sender">Объект вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetTitleBarIcon();
        }


        /// <summary>
        /// Обрабатывает события нажатия на шапку окна.
        /// </summary>
        /// <param name="sender">Объект вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion



        #region Работа с экземпляром проекта.
        /// <summary>
        /// Загружает проект.
        /// </summary>
        private void LoadProject()
        {
            if (Project != null)
            {
                InitializeObjectExplorer();
            }
            else
            {
                // TODO: Добавить сообщение о том, что не удалось открыть проект.
            }
        }


        /// <summary>
        /// Запускает инициализацию обозревателя объектов.
        /// </summary>
        private void InitializeObjectExplorer()
        {
            if (Project?.Nodes.Count == 1)
            {
                var rootBranch = new ExplorerBranch
                {
                    Text = $"Проект \"{Project.Nodes[0].Name}\"",
                    Icon = (BitmapImage)Application.Current.Resources["StudioProjectIcon"]
                };

                objectExplorer.Children.Add(rootBranch);

                RootProjectNode root = (RootProjectNode)Project.Nodes[0];
                AddObjectExplorerBranch(rootBranch, root.ChildNodes);
            } 
        }


        /// <summary>
        /// Рекурсивно добавляет элементы в обозреватель проекта.
        /// </summary>
        /// <param name="parent">Корневая ветвь обозревателя.</param>
        /// <param name="childNodes">Дочерние элементы корневого элемента проекта.</param>
        private void AddObjectExplorerBranch(ExplorerBranch parent, ProjectNodesCollection childNodes)
        {
            foreach (var node in childNodes)
            {
                var branch = new ExplorerBranch { Text = node.Name };
                parent.AddChild(branch);

                switch (node.Type)
                {
                    case ProjectNodeType.Directory:
                        var directoryNode = (DirectoryProjectNode)node;
                        branch.Icon = (BitmapImage)Application.Current.Resources["FolderIcon"];
                        AddObjectExplorerBranch(branch, directoryNode.ChildNodes);
                        break;
                }
            }
        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var createProjectWindow = new CreateProjectWindow();
            createProjectWindow.ShowDialog();            
        }
    }
}