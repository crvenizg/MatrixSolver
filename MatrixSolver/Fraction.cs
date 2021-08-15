using System;
using System.Linq;

namespace MatrixSolver
{
    class Fraction
    {
        private int brojnik, nazivnik;
        //properti
        public int GetBrojnik()
        {
            return brojnik;
        }
        public int GetNazivnik()
        {
            return nazivnik;
        }

        //staviti u jedan kao tip ove kontruktore i onda ako je int radi ovo za float double radi to i to
        public Fraction(int broj)
        {
            this.brojnik = broj;
            this.nazivnik = 1;
        }
        public Fraction(String razlomak)
        {
            if(razlomak.Contains('/') == true)
            {
                String[] clanovi = razlomak.Trim().Split('/');
                //skrati razlomak

                int skraceniBrojnik = Convert.ToInt32(clanovi[0]), skraceniNazivnik = Convert.ToInt32(clanovi[1]);

                for (int i = 2; i <= skraceniBrojnik;)
                {
                    if (skraceniBrojnik % i == 0 && skraceniNazivnik % i == 0)
                    {
                        skraceniBrojnik /= i;
                        skraceniNazivnik /= i;
                    }
                    else
                    {
                        i++;
                    }
                }

                this.brojnik = skraceniBrojnik;
                this.nazivnik = skraceniNazivnik;
            }
            else
            {
                this.brojnik = Convert.ToInt32(razlomak);
                this.nazivnik = 1;
            }

            
        }
        public Fraction(int gore, int dolje)
        {
            this.brojnik = gore;
            this.nazivnik = dolje;
        }
        public Fraction(double broj)
        {
            if((broj - Convert.ToInt32(broj)) == 0)
            {
                this.brojnik = Convert.ToInt32(broj);
                this.nazivnik = 1;
            }
            else
            {
                //convert decimal to fraction
            }

        }


        //overloadaj operatore puta dijeljeno minus plus
        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(-a.brojnik, a.nazivnik);
        public static Fraction operator +(Fraction prvi, Fraction drugi)
        {
            int nazivnik = prvi.nazivnik * drugi.nazivnik;
            int brojnik = prvi.brojnik * drugi.nazivnik + drugi.brojnik * prvi.nazivnik;
            Fraction razlomak = new Fraction(brojnik, nazivnik);
            return SkratiRazlomak(razlomak);
        }
        public static Fraction operator -(Fraction prvi, Fraction drugi)
        {
            int nazivnik = prvi.nazivnik * drugi.nazivnik;
            int brojnik = prvi.brojnik * drugi.nazivnik - drugi.brojnik * prvi.nazivnik;
            Fraction razlomak = new Fraction(brojnik, nazivnik);
            return SkratiRazlomak(razlomak);
        }
        public static Fraction operator *(Fraction prvi, Fraction drugi)
        {
            Fraction novi = new Fraction(prvi.brojnik * drugi.brojnik, prvi.nazivnik * drugi.nazivnik);
            return SkratiRazlomak(novi);
        }
        public static Fraction operator *(Fraction razlomak, int skalar)
        {
            Fraction novi = new Fraction(razlomak.brojnik * skalar, razlomak.nazivnik);
            return SkratiRazlomak(novi);
        }
        public static Fraction operator /(Fraction prvi, Fraction drugi)
        {
            Fraction novi = new Fraction(prvi.brojnik * drugi.nazivnik, prvi.nazivnik * drugi.brojnik);
            return SkratiRazlomak(novi);
        }
        public static Fraction operator /(Fraction razlomak, double skalar)
        {
            Fraction novi = new Fraction(razlomak.brojnik, razlomak.nazivnik * Convert.ToInt32(skalar));
            return SkratiRazlomak(novi);
        }

        public static Fraction SkratiRazlomak(Fraction stariRazlomak)
        {
            int skraceniBrojnik = stariRazlomak.brojnik, skraceniNazivnik = stariRazlomak.nazivnik;

            if (skraceniBrojnik == 0)
            {
                return new Fraction(0, 1);
            }

            if(skraceniBrojnik < 0 && skraceniNazivnik < 0)
            {
                skraceniBrojnik *= -1;
                skraceniNazivnik *= -1;
            }

            if(skraceniNazivnik < 0)
            {
                skraceniNazivnik *= -1;
                skraceniBrojnik *= -1;
            }

            for (int i = 2; i <= Math.Abs(skraceniBrojnik);)
            {
                if(Math.Abs(skraceniBrojnik) % i == 0 && skraceniNazivnik % i == 0)
                {
                    skraceniBrojnik /= i;
                    skraceniNazivnik /= i;
                }
                else
                {
                    i++;
                }
            }

            Fraction noviRazlomak = new Fraction(skraceniBrojnik, skraceniNazivnik);

            return noviRazlomak;
        }




        public bool Equals(Fraction drugi)
        {
            drugi = SkratiRazlomak(drugi);

            if(this.brojnik == drugi.brojnik && this.nazivnik == drugi.nazivnik)
            {
                return true;
            }
            return false;
        }
        public override String ToString()
        {
            if(this.nazivnik == 1)
            {
                return Convert.ToString(this.brojnik);
            }
            //else if(this.brojnik < 0)
            //{
            //    return @"-\frac{" + Math.Abs(this.brojnik) + "}{" + this.nazivnik + "}";
            //}
            else
            {
                return @"\frac{" + this.brojnik + "}{" + this.nazivnik + "}";
            }
            
        }
    }
}
