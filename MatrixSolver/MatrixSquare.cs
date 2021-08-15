using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSolver
{
    class MatrixSquare
    {
        public Fraction[,] elementi { get; set; }
        public int brRedova { get; set; }

        //stavi u generic method
        public MatrixSquare(int red)
        {
            this.brRedova = red;
            this.elementi = new Fraction[red, red];

            for (int i = 0; i < red; i++)
            {
                for (int j = 0; j < red; j++)
                {
                    if(i == j)
                    {
                        elementi[i, j] = new Fraction(1);
                    }
                    else
                    {
                        elementi[i, j] = new Fraction(0);
                    }
                }
            }
        }
        public MatrixSquare(int[] elements, int red)
        {
            this.brRedova = red;
            this.elementi = new Fraction[red, red];

            for (int i = 0; i < red; i++)
            {
                for(int j = 0; j < red; j++)
                {
                    elementi[i, j] = new Fraction(elements[i*red + j]);
                }
            }
        }
        public MatrixSquare(double[] elements, int red)
        {
            this.brRedova = red;
            this.elementi = new Fraction[red, red];

            for (int i = 0; i < red; i++)
            {
                for (int j = 0; j < red; j++)
                {
                    elementi[i, j] = new Fraction(elements[i * red + j]);
                }
            }
        }
        public MatrixSquare(String[] elements, int red)
        {
            this.brRedova = red;
            this.elementi = new Fraction[red, red];

            for (int i = 0; i < red; i++)
            {
                for (int j = 0; j < red; j++)
                {
                    elementi[i, j] = new Fraction(elements[i * red + j]);
                }
            }
        }

        public String ispisMatrice()
        {
            StringBuilder rjesenje = new StringBuilder();
            rjesenje.Append(@"\pmatrix{");

            for (int i = 0; i < brRedova; i++)
            {
                for(int j = 0; j < brRedova; j++)
                {
                    if (j != (brRedova - 1))
                    {
                        rjesenje.Append(elementi[i, j].ToString());
                        rjesenje.Append(@" &");
                    }
                    else
                    {
                        rjesenje.Append(elementi[i, j].ToString());
                    }
                }

                if (i != (brRedova - 1))
                {
                    rjesenje.Append(@"\\");
                }
            }
            return rjesenje.ToString();
        }

        public void SwapRows(int prvi, int drugi)
        {
            Fraction[] pomocniRed = new Fraction[this.brRedova];
            
            for(int i = 0; i < this.brRedova; i++)
            {
                pomocniRed[i] = elementi[prvi - 1, i];
                elementi[prvi - 1, i] = elementi[drugi - 1, i];
                elementi[drugi - 1, i] = pomocniRed[i];
            }

        }
        public void DijeliRed(int row, Fraction djeljitelj)
        {
            for(int i = 0; i < this.brRedova; i++)
            {
                elementi[row - 1, i] /= djeljitelj;
            }
        }
        public Fraction[] PomnoziRed(int row, Fraction faktor)
        {
            Fraction[] pomnozeniRed = new Fraction[this.brRedova];

            for(int i = 0; i < this.brRedova; i++)
            {
                pomnozeniRed[i] = elementi[row - 1, i] * faktor;
            }

            return pomnozeniRed;
        }


    }
}
