using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSolver
{
    class MatrixNormal
    {
        private Fraction[] elementi; //mozda 2d array ipak ?
        private int brRedova, brStupaca;

        public MatrixNormal(int[] elements, int red, int stu)
        {
            this.brRedova = red;
            this.brStupaca = stu;
            this.elementi = new Fraction[red * red];

            for (int i = 0; i < elements.Length; i++)
            {
                elementi[i] = new Fraction(elements[i]);
            }
        }

        public MatrixNormal(double[] elements, int red, int stu)
        {
            this.brRedova = red;
            this.brStupaca = stu;
            this.elementi = new Fraction[red * red];

            for (int i = 0; i < elements.Length; i++)
            {
                elementi[i] = new Fraction(elements[i]);
            }
        }

        public String GetPostupakRang()
        {
            StringBuilder rjesenje = new StringBuilder();


            return rjesenje.ToString();
        }


        public String GetPostupakJednadzbe()
        {
            StringBuilder rjesenje = new StringBuilder();


            return rjesenje.ToString();
        }
    }
}
