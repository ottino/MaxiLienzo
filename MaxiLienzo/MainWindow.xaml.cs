using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaxiLienzo
{
    public partial class MainWindow : Window
    {
        private DateTime _lastClickTime;
        private bool isDragging = false;
        private Point clickPosition;
        private bool isClickOnDragBar = false;

        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                DeleteActiveContainer();
                e.Handled = true;
            }
        }

        private void DeleteActiveContainer()
        {
            var focusedElement = FocusManager.GetFocusedElement(this) as FrameworkElement;
            if (focusedElement != null)
            {
                var container = FindParent<Border>(focusedElement);
                if (container != null && AnotacionesCanvas.Children.Contains(container))
                {
                    AnotacionesCanvas.Children.Remove(container);
                }
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isClickOnDragBar)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan timeDifference = currentTime - _lastClickTime;

                if (timeDifference.TotalMilliseconds <= 300 && e.ChangedButton == MouseButton.Left)
                {
                    AddTextBoxAtMousePosition(e);
                }

                _lastClickTime = currentTime;
            }

            isClickOnDragBar = false;
        }

        private void AddTextBoxAtMousePosition(MouseButtonEventArgs e)
        {
            var container = new Border
            {
                Width = 200,
                Height = 85,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1)
            };

            var dragBar = new Border
            {
                Height = 5,
                Background = Brushes.LightGray
            };

            TextBox newTextBox = new TextBox
            {
                Width = 200,
                Height = 80,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(1),
                FontSize = 14,
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            };

            dragBar.MouseDown += DragBar_MouseDown;
            dragBar.MouseMove += DragBar_MouseMove;
            dragBar.MouseUp += DragBar_MouseUp;

            var stackPanel = new StackPanel();
            stackPanel.Children.Add(dragBar);
            stackPanel.Children.Add(newTextBox);
            container.Child = stackPanel;

            Canvas.SetLeft(container, e.GetPosition(AnotacionesCanvas).X);
            Canvas.SetTop(container, e.GetPosition(AnotacionesCanvas).Y);

            AnotacionesCanvas.Children.Add(container);

            newTextBox.Focus();
        }

        private void DragBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isClickOnDragBar = true;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDragging = true;
                clickPosition = e.GetPosition(AnotacionesCanvas);
                Mouse.Capture((UIElement)sender);
            }
        }

        private void DragBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Border dragBar = sender as Border;
                Border container = FindParent<Border>(dragBar);

                Point currentPosition = e.GetPosition(AnotacionesCanvas);

                double left = Canvas.GetLeft(container) + (currentPosition.X - clickPosition.X);
                double top = Canvas.GetTop(container) + (currentPosition.Y - clickPosition.Y);

                left = Math.Max(0, Math.Min(left, AnotacionesCanvas.ActualWidth - container.Width));
                top = Math.Max(0, Math.Min(top, AnotacionesCanvas.ActualHeight - container.Height));

                Canvas.SetLeft(container, left);
                Canvas.SetTop(container, top);

                clickPosition = currentPosition;
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }

        private void DragBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.Capture(null);
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}