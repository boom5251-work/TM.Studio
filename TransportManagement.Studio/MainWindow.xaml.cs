using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TransportManagement.Studio.Project.Components;
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


        public MainWindow(StudioProject? project) : base()
        {
            Project = project;
        }



        /// <summary>
        /// Текущий проект.
        /// </summary>
        private StudioProject? Project { get; set; }



        #region Функции кнопок шапки страницы.
        /// <summary>
        /// Устанавливает икноки кнопок шапки.
        /// </summary>
        private void SetTitleBarIcon()
        {
            // Установка иконки кнопки "развернуть".
            if (WindowState == WindowState.Maximized)
                ((Image)maximizeButton.Content).Source = (BitmapImage)Resources["NormalizeIcon"];
            else
                ((Image)maximizeButton.Content).Source = (BitmapImage)Resources["MaximizeIcon"];
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