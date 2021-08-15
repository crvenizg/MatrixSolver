using System.Text;

namespace MatrixSolver
{
    class PomocneFunkcije
    {
        public static void OtvoriMatricuHidden(ref StringBuilder postupak)
        {
            postupak.Append(@"\matrix{");
        }
        public static void OtvoriMatricuBrackets(ref StringBuilder postupak)
        {
            postupak.Append(@"\pmatrix{");
        }
        public static void ZatvoriMatricu(ref StringBuilder postupak)
        {
            postupak.Append(@"}");
        }
        public static void DodajKraciNoviRed(ref StringBuilder postupak)
        {
            postupak.Append(@" \\ ");
        }
        public static void DodajDuziNoviRed(ref StringBuilder postupak)
        {
            postupak.Append(@" \\ \\ \\ ");
        }
        public static void DodajKraciNoviStupac(ref StringBuilder postupak)
        {
            postupak.Append(@" & ");
        }
        public static void DodajDuziNoviStupac(ref StringBuilder postupak)
        {
            postupak.Append(@" &&&&&&&& ");
        }
        public static void DodajOdvojakRedova(ref StringBuilder postupak)
        {
            postupak.Append(@" \\ \\ ");
            postupak.Append(@"- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
            postupak.Append(@" \\ \\ ");
        }
            
        //usage
        //PomocneFunkcije.OtvoriMatricuHidden(ref postupak);
        //PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);

        //PomocneFunkcije.ZatvoriMatricu(ref postupak);

        //PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
        //PomocneFunkcije.DodajDuziNoviStupac(ref postupak);

        //PomocneFunkcije.DodajKraciNoviRed(ref postupak);
        //PomocneFunkcije.DodajDuziNoviRed(ref postupak);


        //PomocneFunkcije.DodajOdvojakRedova(ref postupak);

    }
}
