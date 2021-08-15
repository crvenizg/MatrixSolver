using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSolver
{
    class InverzMatrice
    {
        public static String GetPostupakInverzCramer(MatrixSquare matrica)
        {
            MatrixSquare inverznaMatrica = new MatrixSquare(matrica.brRedova);


            StringBuilder postupak = new StringBuilder();
            for(int i = 0; i < matrica.brRedova; i++)
            {
                postupak.Append(matrica.ispisMatrice());
                postupak.Append(inverznaMatrica.ispisMatrice());

                //dodaj indeksiranje ono pro
                matrica.DijeliRed(i, matrica.elementi[i, i]);
                inverznaMatrica.DijeliRed(i, matrica.elementi[i, i]);

                postupak.Append(i + 1);
                postupak.Append(@"red/:");
                postupak.Append(matrica.elementi[i, i]);
                postupak.Append(@"=");



            }
            
            

            

            return postupak.ToString();
        }

        public static String GetPostupakInverz()
        {
            StringBuilder postupak = new StringBuilder();


            return postupak.ToString();
        }
    }
}
