namespace Katakana
{
    public class Logic
    {
        private int[,] u_in = new int[64, 48];

        public void WriteToColumn(int signNr, string bits) //Funkcja do wpisywania ciągu zaków mapy bitowej(bits) dla konkretnego znaku(signNr) do tabeli u_in
        {
            for (int i = 0; i < bits.Length; i++)
            {
                u_in[i,signNr] = (int)bits[i]-'0'; //Wpisanie danego elementu ciągu znaków na odpowiednią pozycję w tabeli
            }
        }
    }
}
