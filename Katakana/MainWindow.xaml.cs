﻿using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
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

            logic.R = 0;

            logic.ForwardPropagationPhase();
            var i = 1 + 1;
        }
    }
}
