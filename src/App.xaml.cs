using System.Windows;

namespace EducatedMonkey
{
    /// <summary>
    /// Точка входа в приложение
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            // Запуск главного окна
            Run(new MainWindow());
        }
    }
}
