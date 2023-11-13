using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Katakana
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Rectangle> rectangles = new List<Rectangle>();
        public MainWindow()
        {
            InitializeComponent();
            FillGridWithRectangles();
        }

        private void FillGridWithRectangles()
        {
            for (int row = 0; row < PixelGrid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < PixelGrid.ColumnDefinitions.Count; column++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Fill = Brushes.White, // You can set your desired color
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 0.5,
                        Name = $"Pixel{row}{column}"
                    };

                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, column);

                    rectangle.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

                    rectangles.Add(rectangle);
                    PixelGrid.Children.Add(rectangle);
                }
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Handle the click event here
            Rectangle clickedRectangle = sender as Rectangle;
            if (clickedRectangle != null)
            {
                if (clickedRectangle.Fill == Brushes.Black)
                    clickedRectangle.Fill = Brushes.White;
                else
                    clickedRectangle.Fill = Brushes.Black;
            }
            RetrieveRectangleColors(); // for testing only
        }

        private void RetrieveRectangleColors() // for testing
        {
            String clors ="";
            foreach (var rectangle in rectangles)
            {
                // Retrieve information about the color of each rectangle
                Color color = ((SolidColorBrush)rectangle.Fill).Color;
                string colorInfo = $"Rectangle at Row {Grid.GetRow(rectangle)}, Column {Grid.GetColumn(rectangle)} has color: {color} \n";
                clors += colorInfo;
                // Use the color information as needed (print to console, store in a list, etc.)
            }
            textbox.Text = clors;
        }
    }
}
