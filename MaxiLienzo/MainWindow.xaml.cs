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
        private DateTime _lastClickTime;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Evento para añadir una caja de texto al hacer doble clic
        private void Canvas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Crear un nuevo TextBox para la anotación
            TextBox newTextBox = new TextBox
            {
                Width = 100, // Ancho de la caja de texto
                Height = 15, // Alto de la caja de texto
                FontStyle = FontStyles.Italic,
                Background = Brushes.Transparent, // Fondo transparente para el TextBox
                BorderThickness = new Thickness(0), // Sin borde
                FontSize = 10 // Tamaño de la fuente
            }; 

            // Establecer la posición del TextBox donde se hizo el doble clic
            Canvas.SetLeft(newTextBox, e.GetPosition(AnotacionesCanvas).X);
            Canvas.SetTop(newTextBox, e.GetPosition(AnotacionesCanvas).Y);

            // Añadir el TextBox al Canvas
            AnotacionesCanvas.Children.Add(newTextBox);

            // Poner foco en el TextBox para que el usuario pueda empezar a escribir inmediatamente
            newTextBox.Focus();
        }
        // Evento para manejar el MouseDown y verificar si fue un doble clic
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan timeDifference = currentTime - _lastClickTime;

            if (timeDifference.TotalMilliseconds <= 300 && e.ChangedButton == MouseButton.Left)
            {
                // Si es un doble clic, añadir una caja de texto
                AddTextBoxAtMousePosition(e);
            }

            _lastClickTime = currentTime;
        }

        // Método para agregar una caja de texto en la posición del ratón
        private void AddTextBoxAtMousePosition(MouseButtonEventArgs e)
        {
            TextBox newTextBox = new TextBox
            {
                Width = 100, // Ancho de la caja de texto
                Height = 30, // Alto de la caja de texto
                Background = Brushes.Transparent, // Fondo transparente para el TextBox
                BorderThickness = new Thickness(0), // Sin borde
                FontSize = 14 // Tamaño de la fuente
            };

            // Establecer la posición del TextBox donde se hizo el clic
            Canvas.SetLeft(newTextBox, e.GetPosition(AnotacionesCanvas).X);
            Canvas.SetTop(newTextBox, e.GetPosition(AnotacionesCanvas).Y);

            // Añadir el TextBox al Canvas
            AnotacionesCanvas.Children.Add(newTextBox);

            // Poner foco en el TextBox para que el usuario pueda empezar a escribir inmediatamente
            newTextBox.Focus();
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