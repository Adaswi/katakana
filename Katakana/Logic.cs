namespace Katakana
{
    public class Logic
    {
        private int[,] A = new int[111, 46];

        private int[] W = new int[6152];

        private string[] bitMap = new string[46];

        public void WriteToColumn(int signNr, string bits) //Funkcja do wpisywania ciągu zaków mapy bitowej(bits) dla konkretnego znaku(signNr) do tabeli u_in
        {
            for (int i = 0; i < bits.Length+1; i++)
            {
                if (i == 0) 
                    A[i, signNr] = 1;
                else 
                    A[i, signNr] = (int)bits[i-1] - '0'; //Wpisanie danego elementu ciągu znaków na odpowiednią pozycję w tabeli
                A[65 + signNr, signNr] = 1;
            }
        }

        public void WriteToTable(string[] strings)
        {
            for (int i = 0; i<A.GetLength(1); i++)
            {
                    this.WriteToColumn(i, strings[i]);   
            }
        }
    }
}
