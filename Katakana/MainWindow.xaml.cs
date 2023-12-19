using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        }


        private void scan_Click(object sender, RoutedEventArgs e)
        {
            string signString = "";
            int row = 0;
            int col = 0;
            int[,] sign = new int[8, 8];
            int control = 0;
            foreach (var rectangle in rectangles) //foreach writes selected boxes to an int[,] data holder
            {
                if (rectangle.Fill == Brushes.Black)
                {
                    sign[row, col] = 1;
                    control++;
                }
                else
                {
                    sign[row, col] = 0;
                }
                col++;
                if (col == 8)
                {
                    col = 0;
                    row++;
                }
            }

            while (true && control != 0) // this while is responsible for shifting the whole bitmap to the bottom left corner
            {
                int checkCol = 0;
                for (int i = 0; i < 8; i++)
                {
                    checkCol += sign[i, 0];
                }
                if (checkCol == 0)
                {
                    for (int r = 0; r < 8; r++)
                    {
                        for (int c = 1; c < 8; c++)
                            sign[r, c - 1] = sign[r, c];
                    }
                    for (int r = 0; r < 8; r++)
                        sign[r, 7] = 0;
                }


                int checkRow = 0;
                for (int i = 0; i < 8; i++)
                {
                    checkRow += sign[7, i];
                }
                if (checkRow == 0)
                {
                    for (int r = 6; r >= 0; r--)
                    {
                        for (int c = 0; c < 8; c++)
                            sign[r + 1, c] = sign[r, c];
                    }
                    for (int c = 0; c < 8; c++)
                        sign[0, c] = 0;
                }

                if (checkCol != 0 && checkRow != 0)
                    break;
            }

            //Added only temporarily ik it looks like sh... sorry -_-   

            string location = "D:\\TestFileSave\\Test.txt";

            List<string> znakiDoPliku = new List<string>(File.ReadAllLines(location));

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    signString += sign[r, c];
                }
            }

            znakiDoPliku.Add(signString);



            try
            {
                File.WriteAllLines(location, znakiDoPliku.ToArray());
            }
            catch (Exception ex)
            {
                textbox.Text = $"An error occurred: {ex.Message}";
            }

            textbox.Text = signString; ////comment
        }
    }
}
