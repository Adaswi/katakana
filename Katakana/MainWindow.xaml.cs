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
            var logic = new Logic();

            string[] strings = new string[46] { "0000000011111100000001000010100000100000001000000010000001000000",
"0000000000000000000001000000100000010000001100001101000000010000",
"0000000000010000111111001000010010000100000001000000100000110000",
"0000000011111110000100000001000000010000000100000001000011111110",
"0000000000010000000100001111110000110000010100001001000000110000",
"0000000000100000111111000010010000100100001001000100010010011000",
"0000000000100000000110000110000000011100111000000001000000010000",
"0000000001000000011110000100100010001000000010000001000001100000",
"0000000001000000010000000111110010010000100100000001000001100000",
"0000000000000000111110000000100000001000000010000000100011111000",
"0000000001000100010001001111111001000100010001000000010000111000",
"0000000000000000001000001001010001000100000010000001000011100000",
"0000000000000000111100000001000000100000001100000100100010001000",
"0000000001000000010111101110001001000010010001000100000000111110",
"0000000000000000100010000100100001001000000010000001000001100000",
"0000000001000000011110000100100010101000000100000001000001100000",
"0000000000001000011100000001000011111100000100000001000001100000",
"0000000000000000000000000010010010010100010001000000100000110000",
"0000000000000000011110000000000011111100000100000001000001100000",
"0000000000000000100000001000000011100000100100001000000010000000",
"0000000000000000000100001111100000010000000100000010000011000000",
"0000000000000000000000000110000000000000000000000000000011110000",
"0000000000000000111111000000010000110100000010000001010001100010",
"0000000000100000111110000001000000101000111000000010000000100000",
"0000000000000000000010000000100000010000000100000010000011000000",
"0000000000000000010100000100100001001000010001001000010010000100",
"0000000000000000100010001111000010000000100000001000000001111000",
"0000000000000000111110000000100000001000000010000001000001100000",
"0000000000000000000000000000000000100000010100001000100000000100",
"0000000000100000111110000010000000100000101010001010100000100000",
"0000000000000000111111000000010001000100001010000001000000010000",
"0000000011000000001100001100000000110000000000001100000000110000",
"0000000000010000000100000010000000100000001001000100110011110010",
"0000000000001000000010000110100000010000001010000100100010000000",
"0000000000000000111110000100000011111000010000000100000000111000",
"0000000001000000001011100011001011100010000101000001000000010000",
"0000000000000000011100000001000000010000000100000001000011111100",
"0000000000000000111110000000100011111000000010000000100011111000",
"0111000000000000111110000000100000001000000010000001000001100000",
"0000000000000000100100001001000010010000000100000010000011000000",
"0000000000000000000100000101000001010010010100101001010010011000",
"0000000000000000100000001000000010010000100100001010000011000000",
"0000000000000000111110001000100010001000100010001111100010001000",
"0000000000000000111111001000010010000100000001000000100000110000",
"0000000000000000111111000000010011111100000001000000100000110000",
"0000000000000000100000000100010000000100000010000001000011100000" };


            logic.WriteToColumn(strings);

            logic.GenerateWeightValues();
            var iteracja = 0;
            while(iteracja<10000)
            {
                iteracja += 1;

                logic.SetRandomR();
                logic.ForwardPropagationPhase();
                logic.BackwordPropagationPhase();
                logic.UpdateWeightValues();
            }

            logic.WriteToRow(0, "0000000000100000111110000010000000100000101010001010100000100000");
            logic.R = 0;

            logic.ForwardPropagationPhase();
            var i = 1 + 1;
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
