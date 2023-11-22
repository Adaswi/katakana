using System;
using System.Diagnostics.CodeAnalysis;

namespace Katakana
{
    public class Logic
    {
        private int[,] UOne = new int[65, 46];
        private float[] UTwo = new float[55];
        private float[] UThree = new float[46];

        private float[,] WOne = new float[55, 65];
        private float[,] WTwo = new float[46, 55];

        private float[] SOne = new float[55];
        private float[] STwo = new float[46];

        private float[] FOne = new float[55];
        private float[] FTwo = new float[46];

        private float[] DOne = new float[55]; 
        private float[] DTwo = new float[46];

        private int[,] C = new int[46, 46];

        private float Ro = 0.2f;


        public void WriteToRow(int signNr, string bits) //Funkcja do wpisywania ciągu zaków mapy bitowej(bits) dla konkretnego znaku(signNr) do tabeli UOne
        {
            for (int i = 0; i < bits.Length+1; i++)
            {
                if (i == 0) 
                    UOne[i, signNr] = 1; //Wpisanie jedynek dla U0
                else 
                    UOne[i, signNr] = (int)bits[i-1] - '0'; //Wpisanie danego elementu ciągu znaków na odpowiednią pozycję w tabeli
                C[signNr, signNr] = 1; //Zaznaczenie bitu odpowiadającego wynikowi
            }
        }

        public void WriteToColumn(string[] strings) //Funkcja do wpisywania całej tablicy ciągu znaków mapy bitowej(strings) dla wszystkich znaków do tabeli UOne
        {
            for (int i = 0; i<UOne.GetLength(1); i++)
            {
                this.WriteToRow(i, strings[i]);   
            }
        }

        public void GenerateWeightValues() //Generowanie wag o wartościach od 0 do 0.5
        {
            for (int i = 0; i < WOne.GetLength(0); i++) //WOne - wagi między warstwą wejściową i pośrednią
            {
                for (int j = 0; j < WOne.GetLength(1); j++)
                {
                    WOne[i,j] = NextFloat(0, 0.5f);
                }
            }

            for (int i = 0; i < WTwo.GetLength(0); i++) //WTwo - wagi między warstwą pośrednią i wyjściową
            {
                for (int j = 0; j < WTwo.GetLength(1); j++)
                {
                    WTwo[i, j] = NextFloat(0, 0.5f);
                }
            }
        }
        static float NextFloat(float min, float max) //generowanie liczby typu float w zakresie od min do max
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        public void ForwardPropagationPhase() //Faza wstępnej propagacji
        {
            int r = (int)Math.Floor(NextFloat(0, 47-float.MinValue)); //Losowe generowanie wektora trenującego

            for(int i = 0; i<SOne.Length; i++) //Obliczanie S i U dla warstwy pośredniej
            {
                var sum = 0f;
                for(int j = 0; j<WOne.GetLength(1); j++)
                {
                    sum = sum + WOne[i, j] * UOne[j,r];
                }
                SOne[i] = sum;
                UTwo[i] = (float)(1 / (1 + Math.Exp(-SOne[i])));
            }

            for (int m = 0; m < STwo.Length; m++) //Obliczanie S i U dla warstwy wyjściowej
            {
                var sum = WTwo[m, 0] * UOne[m,r];
                for (int n = 1; n < WTwo.GetLength(1); n++)
                {
                    sum = sum + WTwo[m, n] * UTwo[n-1];
                }
                STwo[m] = sum;
                UThree[m] = (float)(1 / (1 + Math.Exp(-STwo[m])));
            }
        }

        public void BackwordPropagationPhase()
        {
            for (int i=0; i < FTwo.Length; i++)
            {
                FTwo[i] = UThree[i]*(1 - UThree[i]);
                DTwo[i] = (C[i,i]- UThree[i]) * FTwo[i];
            }

            for (int i=0; i < FOne.Length; i++)
            {
                FOne[i] = UThree[i] * (1 - UThree[i]);
                var sum = 0f;
                for (int j = 0; j < DTwo.Length; j++)
                {
                    sum = sum + DTwo[j] * WTwo[j,i];
                }
                DOne[i] = sum * FOne[i];
            }
        }

        public void UpdateWeightValues()
        {
            for (int i=0;  i < WTwo.GetLength(0); i++)
            {
                for (int j=0; j < DTwo.GetLength(1); j++)
                {
                    WTwo[i, j] = WTwo[i, j] + Ro * DTwo[i] * UThree[j];
                }
            }

            for (int i=0;i < WOne.GetLength(0);i++)
            {
                for (int j=0;j < DOne.GetLength(1);j++)
                {
                    WOne[i, j] = WOne[i, j] + Ro * DOne[i] * UThree[j];
                }
            }
        }
    }
}
