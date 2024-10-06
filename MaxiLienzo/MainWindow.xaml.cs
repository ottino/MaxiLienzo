using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaxiLienzo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Evento para cerrar la ventana
        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana
        }

        // Evento para minimizar la ventana
        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Minimiza la ventana
        }

        // Evento para maximizar/restaurar la ventana
        private void MaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized; // Maximiza la ventana
            }
            else
            {
                this.WindowState = WindowState.Normal; // Restaura el tamaño original
            }
        }

        // Evento para mover la ventana
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove(); // Permite mover la ventana
            }
        }
    }
}