using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MatrixSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            txtPostupak.Formula = "";

            rbtnInverz.IsChecked = false;
            rbtnInverzCramer.IsChecked = false;
            rbtnRang.IsChecked = false;
            rbtnRjesavanje.IsChecked = false;
            rbtnVektorski.IsChecked = false;
            rbtnRavninaTocke.IsChecked = false;
            rbtnDeterminanta.IsChecked = false;
            rbtnSustavPrekoCramera.IsChecked = false;
            rbtnMnozenjeMatrica.IsChecked = false;
            rbtnInputOutputMatricaPrvaVrsta.IsChecked = false;
            rbtnInputOutputMatricaDrugaVrsta.IsChecked = false;

            txtBrStupaca.IsEnabled = true;

            txtBrRedova.Text = "";
            txtBrStupaca.Text = "";
            txtElements.Text = "";

            btnIzracunaj.IsEnabled = false;
        }
        private void btn_clearFields_click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            if (rbtnMnozenjeMatrica.IsChecked == true)
            {
                txtElements.Text = "()_()";
            }
            else
            {
                txtElements.Text = "";
            }
            
        }


        private void btn_TypeExample(object sender, RoutedEventArgs e)
        {
            var radiobutton = sender as RadioButton;
            if (radiobutton.IsChecked == true)
            {
                txtBrStupaca.IsEnabled = false;
                txtBrRedova.IsEnabled = false;
                ClearFields();

                if (radiobutton.Content.Equals("Inverz Cramerovim pravilom"))
                {
                    //radi, gotov
                    txtElements.Text = "2 1 3 2 4 0 0 3 1";
                    //txtElements.Text = "2 1 3 2";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Inverz elementarnim transformacijama"))
                {
                    //radi, gotov
                    txtElements.Text = "2 1 3 2 4 0 0 3 1";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Rang"))
                {
                    txtElements.Text = "2 1 3 2;4 0 0 3;1 4 -2 1";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Rješavanje jednadžbi"))
                {
                    txtElements.Text = "2 3 0 -1 -1;1 1 1 1 13;1 2 -1 1 1;-3 4 1 2 10";
                    btnIzracunaj.IsEnabled = true;
                    //algoritam je jednostavan, ali samo ako je 0 na stozeru kao onda trazi ispod redak bez nule i zamjeni retke, ako ne nade onda idi udesno
                }
                if (radiobutton.Content.Equals("Vektorski umnožak"))
                {
                    //radi, gotov
                    //txtElements.Text = "(1,2,3) (4,5,6)"; // TESTNI a jbg stavi kao dva vektora primjer ispod
                    //txtElements.Text = "(,,) (,,)"; // ZELJENI
                    txtElements.Text = "1 2 3 4 5 6";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Ravnina preko 3 točke"))
                {
                    //radi, gotov
                    //txtElements.Text = "(1,2,-3) (-4,5,6) (7,-8,9)";
                    txtElements.Text = "1 2 -3 -4 5 6 7 -8 9";
                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Determinanta matrice"))
                {
                    //radi, gotov
                    txtElements.Text = "1 2 3 4 5 6 7 8 9";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Cramerovo rješavanje jednadžbi"))
                {
                    //radi, gotov
                    txtElements.Text = "1 1 3 -7;-1 -1 3 1;1 -1 1 1";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Množenje matrica"))
                {
                    //radi, gotov
                    txtElements.Text = "(1 2;4 5;7 8)_(1 2 3 4;5 6 7 8)";

                    btnIzracunaj.IsEnabled = true;
                }
                if (radiobutton.Content.Equals("Input output analiza 1"))
                {
                    txtElements.Text = "ne radi jos";

                    btnIzracunaj.IsEnabled = false;
                }
                if (radiobutton.Content.Equals("Input output analiza 2"))
                {
                    txtElements.Text = "ne radi jos";

                    btnIzracunaj.IsEnabled = false;
                }
            }
        }
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (rbtnInverz.IsChecked == true || rbtnInverzCramer.IsChecked == true)
            {

                String[] stringElementi = txtElements.Text.Trim().Split(' ');
                int redMatrice = Convert.ToInt32(Math.Sqrt(stringElementi.Length));
                double[] elementi = Array.ConvertAll(stringElementi, Double.Parse);
                double[,] matrica = new double[redMatrice, redMatrice];
                for (int i = 0; i < redMatrice; i++)
                {
                    for (int j = 0; j < redMatrice; j++)
                    {
                        matrica[i, j] = elementi[i * redMatrice + j];
                    }
                }
                //if (txtElements.Text.Contains('.') == false)
                //if (txtElements.Text.Contains('.') == true)
                //{
                //    double[] elementi = Array.ConvertAll(stringElementi, Double.Parse);

                //}
                //else
                //{
                //    int[] elementi = Array.ConvertAll(stringElementi, Int32.Parse);
                //}

                if (rbtnInverzCramer.IsChecked == true)
                {
                    txtPostupak.Formula = inverzCramer(matrica, redMatrice);
                }
                if (rbtnInverz.IsChecked == true)
                {
                    txtPostupak.Formula = inverzElementarnimTrans(matrica, redMatrice);
                }
            }

            if (rbtnRang.IsChecked == true || rbtnRjesavanje.IsChecked == true)
            {
                
                if (rbtnRang.IsChecked == true)
                {
                    String[] stringOdvojeniGranicnik = txtElements.Text.Trim().Split(';');
                    Fraction[,] matrica = new Fraction[stringOdvojeniGranicnik.Length, stringOdvojeniGranicnik[0].Trim().Split(' ').Length];
                    double[] pomocniRed;

                    for(int i = 0; i < matrica.GetLength(0); i++)
                    {
                        pomocniRed = Array.ConvertAll(stringOdvojeniGranicnik[i].Trim().Split(' '), Double.Parse);

                        for (int j = 0; j < matrica.GetLength(1); j++)
                        {
                            matrica[i, j] = new Fraction(pomocniRed[j]);
                        }
                    }


                    txtPostupak.Formula = rangMatrice(matrica);
                }
                if (rbtnRjesavanje.IsChecked == true)
                {
                    String[] stringOdvojeniGranicnik = txtElements.Text.Trim().Split(';');
                    Fraction[,] matrica = new Fraction[stringOdvojeniGranicnik.Length, stringOdvojeniGranicnik[0].Trim().Split(' ').Length-1];
                    double[] pomocniRed;
                    Fraction[] stupac = new Fraction[stringOdvojeniGranicnik.Length];

                    for (int i = 0; i < matrica.GetLength(0); i++)
                    {
                        pomocniRed = Array.ConvertAll(stringOdvojeniGranicnik[i].Trim().Split(' '), Double.Parse);

                        for (int j = 0; j < matrica.GetLength(1); j++)
                        {
                            matrica[i, j] = new Fraction(pomocniRed[j]);
                        }
                        stupac[i] = new Fraction(pomocniRed[pomocniRed.Length - 1]);
                    }


                    txtPostupak.Formula = rjesavanjeJednazdbi(matrica, stupac);
                }
            }

            if (rbtnVektorski.IsChecked == true)
            {
                double[] prviVektor, drugiVektor;

                if (txtElements.Text.Trim().Contains("("))
                {
                    String[] vektori = txtElements.Text.Trim().Split(' ');

                    for (int i = 0; i < vektori.Length; i++)
                    {
                        vektori[i] = vektori[i].Trim('(', ')');
                    }

                    prviVektor = Array.ConvertAll(vektori[0].Trim().Split(','), Double.Parse);
                    drugiVektor = Array.ConvertAll(vektori[1].Trim().Split(','), Double.Parse);
                }
                else
                {
                    double[] vektori = Array.ConvertAll(txtElements.Text.Trim().Split(' '), Double.Parse);
                    prviVektor = new double[3];
                    drugiVektor = new double[3];

                    for(int i = 0; i < 3; i++)
                    {
                        prviVektor[i] = vektori[i];
                        drugiVektor[i] = vektori[i+3];
                    }

                }

                

                txtPostupak.Formula = izracunajVektorskiUmnozak(prviVektor, drugiVektor);
            }

            if (rbtnRavninaTocke.IsChecked == true)
            {
                double[] tockaA, tockaB, tockaC;

                if (txtElements.Text.Trim().Contains("("))
                {
                    String[] tocke = txtElements.Text.Trim().Split(' ');

                    for (int i = 0; i < tocke.Length; i++)
                    {
                        tocke[i] = tocke[i].Trim('(', ')');
                    }

                    tockaA = Array.ConvertAll(tocke[0].Trim().Split(','), Double.Parse);
                    tockaB = Array.ConvertAll(tocke[1].Trim().Split(','), Double.Parse);
                    tockaC = Array.ConvertAll(tocke[2].Trim().Split(','), Double.Parse);
                }
                else
                {
                    double[] vektori = Array.ConvertAll(txtElements.Text.Trim().Split(' '), Double.Parse);
                    tockaA = new double[3];
                    tockaB = new double[3];
                    tockaC = new double[3];

                    for (int i = 0; i < 3; i++)
                    {
                        tockaA[i] = vektori[i];
                        tockaB[i] = vektori[i + 3];
                        tockaC[i] = vektori[i + 6];
                    }
                }


                txtPostupak.Formula = izracunajRavninuPrekoTocki(tockaA, tockaB, tockaC);
            }

            if (rbtnDeterminanta.IsChecked == true)
            {
                String[] stringElementi = txtElements.Text.Trim().Split(' ');
                double[] elementi = Array.ConvertAll(stringElementi, Double.Parse);

                if (elementi.Length == 4)
                {
                    double[,] elementiMatrice = new double[2, 2];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            elementiMatrice[i, j] = elementi[i * 2 + j];
                        }
                    }

                    //txtPostupak.Formula = determinantaRedaTriSarrus(elementiMatrice);
                }
                else if (elementi.Length == 9)
                {

                    double[,] elementiMatrice = new double[3, 3];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            elementiMatrice[i, j] = elementi[i * 3 + j];
                        }
                    }

                    txtPostupak.Formula = @"1.nacin Sarrusovo Pravilo \\ \\ " + determinantaRedaTriSarrus(elementiMatrice)
                        + @"\\ \\ 2.nacin Laplaceov Razvoj Po Prvome Retku \\ \\ " + determinantaRedaTriLaplace(elementiMatrice);
                }
                else if (elementi.Length == 16)
                {
                    //ma kad nadem vremena, mozes preko drugih matrica dobiti
                }
                else
                {
                    txtPostupak.Formula = @"\matrix{NIJE & PODRZANO & RJESAVANJE & ZA & MATRICU & SA & " + elementi.Length.ToString() +
                        @" & ELEMENATA \\ NAPISI & MATRICU & SA & 9 & ILI & 16 & ELEMENATA}";
                }
            }

            if (rbtnSustavPrekoCramera.IsChecked == true)
            {
                String[] stringOdvojeniGranicnik = txtElements.Text.Trim().Split(';');

                double[] prviRedMatriceGlavne = Array.ConvertAll(stringOdvojeniGranicnik[0].Trim().Split(' '), Double.Parse);
                double[] drugiRedMatriceGlavne = Array.ConvertAll(stringOdvojeniGranicnik[1].Trim().Split(' '), Double.Parse);
                double[] treciRedMatriceGlavne = Array.ConvertAll(stringOdvojeniGranicnik[2].Trim().Split(' '), Double.Parse);

                if (prviRedMatriceGlavne.Length == 4 && drugiRedMatriceGlavne.Length == 4 && treciRedMatriceGlavne.Length == 4)
                {
                    txtPostupak.Formula = determinantaCramerTriReda(prviRedMatriceGlavne, drugiRedMatriceGlavne, treciRedMatriceGlavne);
                }
                else
                {
                    txtPostupak.Formula = @"\matrix{MORA & BITI & 3 & PUTA & PO & 4 & ELEMENATA}";
                }
            }

            if (rbtnMnozenjeMatrica.IsChecked == true)
            {
                String[] stringMatrice = txtElements.Text.Trim().Split('_');

                String[] stringPrvaMatrica = stringMatrice[0].Trim('(', ')').Split(';');
                int brRedovaPrve = stringPrvaMatrica.Length;
                int brStupacaPrve = stringPrvaMatrica[0].Split(' ').Length;
                double[,] matricaPrva = new double[brRedovaPrve, brStupacaPrve];
                for (int i = 0; i < brRedovaPrve; i++)
                {
                    double[] red = Array.ConvertAll(stringPrvaMatrica[i].Split(' '), Double.Parse);
                    for (int j = 0; j < brStupacaPrve; j++)
                    {
                        matricaPrva[i, j] = red[j];
                    }
                }

                String[] stringDrugaMatrica = stringMatrice[1].Trim('(', ')').Split(';');
                int brRedovaDruge = stringDrugaMatrica.Length;
                int brStupacaDruge = stringDrugaMatrica[0].Split(' ').Length;
                double[,] matricaDruga = new double[brRedovaDruge, brStupacaDruge];
                for (int i = 0; i < brRedovaDruge; i++)
                {
                    double[] red = Array.ConvertAll(stringDrugaMatrica[i].Split(' '), Double.Parse);
                    for (int j = 0; j < brStupacaDruge; j++)
                    {
                        matricaDruga[i, j] = red[j];
                    }
                }

                txtPostupak.Formula = izracunajUmnozakMatrica(matricaPrva, matricaDruga);

            }
            
            if (rbtnInputOutputMatricaPrvaVrsta.IsChecked == true)
            {

            }
            if (rbtnInputOutputMatricaDrugaVrsta.IsChecked == true)
            {

            }
        }



        private String inverzCramer(double[,] elementi, int redMatrice)
        {
            StringBuilder postupak = new StringBuilder();

            if (redMatrice == 2)
            {
                postupak.Append(@"\matrix{ A=\pmatrix{a & b \\ c & d} &&& A^{-1}=\frac{1}{detA}*\pmatrix{d & -b \\ -c & a}} \\ \\");
                postupak.Append(@"\matrix{ A=\pmatrix{" + elementi[0, 0] + @"&" + elementi[0, 1] + @"\\");
                postupak.Append(elementi[1, 0] + @"&" + elementi[1, 1] + @"} &&&  A^{-1}=\frac{1}{");
                postupak.Append(elementi[0, 0] + @"*" + dodajZagradu(elementi[1, 1]) + @"-" + dodajZagradu(elementi[0, 1]) + @"*" + dodajZagradu(elementi[1, 0]) + @"}");
                postupak.Append(@"* \pmatrix{" + elementi[1, 1] + @"& -" + dodajZagradu(elementi[0, 1]) + @"\\ -");
                postupak.Append(dodajZagradu(elementi[1, 0]) + @"&" + elementi[0, 0] + @"} =\frac{1}{");
                postupak.Append(elementi[0, 0] * elementi[1, 1] - elementi[1, 0] * elementi[0, 1]);
                postupak.Append(@"} * \pmatrix{" + elementi[1, 1] + @"& " + dodajPozitivniBezPreznaka(elementi[0, 1]) + @"\\ ");
                postupak.Append(dodajPozitivniBezPreznaka(elementi[1, 0]) + @"&" + elementi[0, 0] + @"}");
                PomocneFunkcije.ZatvoriMatricu(ref postupak);
            }
            else if (redMatrice == 3)
            {

                postupak.Append(dodajMatricu(elementi));
                PomocneFunkcije.DodajDuziNoviRed(ref postupak);
                postupak.Append(@"detA=");
                postupak.Append(determinantaRedaTriSarrus(elementi));

                int pozRed, pozStu;
                double eksponent;
                double[,] matricaRedaDva = new double[2, 2];
                double[,] matricaRedaTri = new double[3, 3];

                PomocneFunkcije.DodajDuziNoviRed(ref postupak);
                PomocneFunkcije.OtvoriMatricuHidden(ref postupak);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {


                        pozRed = i; pozStu = j;
                        postupak.Append(@"a_{" + (i + 1).ToString() + (j + 1).ToString() + @"} = (-1)^{" + (i + 1).ToString() + @"+" + (j + 1).ToString() + @"}* ");

                        matricaRedaDva = vratiNovuMatricu(pozRed, pozStu, elementi);

                        postupak.Append(@"\pmatrix{");
                        for (int k = 0; k < 2; k++)
                        {
                            for (int z = 0; z < 2; z++)
                            {
                                postupak.Append(matricaRedaDva[k, z]);
                                if (z == 0)
                                {
                                    PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                                }
                            }
                            if (k == 0)
                            {
                                PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                            }
                        }
                        PomocneFunkcije.ZatvoriMatricu(ref postupak);

                        eksponent = Math.Pow(-1, i + j);
                        postupak.Append(@" = " + eksponent.ToString() + @"* [");
                        postupak.Append(matricaRedaDva[0, 0] + @"*" + dodajZagradu(matricaRedaDva[1, 1]) + @"-");
                        postupak.Append(matricaRedaDva[1, 0] + @"*" + dodajZagradu(matricaRedaDva[0, 1]));
                        postupak.Append(@"] = ");
                        matricaRedaTri[i, j] = eksponent * (matricaRedaDva[0, 0] * matricaRedaDva[1, 1] - matricaRedaDva[0, 1] * matricaRedaDva[1, 0]);
                        postupak.Append(matricaRedaTri[i, j]);



                        if ((i + j) % 2 == 0)
                        {
                            postupak.Append(@" && ");
                        }
                        else
                        {
                            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                        }
                    }
                }

                double iznosDeterminante = izracunajDeterminantuSarrus(elementi);

                PomocneFunkcije.ZatvoriMatricu(ref postupak);

                PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                postupak.Append(@"A^{-1}=\frac{1}{" + iznosDeterminante + @"}*");
                postupak.Append(dodajMatricu(matricaRedaTri));
                postupak.Append(@"^{T}=");

                postupak.Append(@"\frac{1}{" + iznosDeterminante + @"}*");
                matricaRedaTri = transponirajMatricu(matricaRedaTri);
                postupak.Append(dodajMatricu(matricaRedaTri));
                postupak.Append(@"=");
                //dodaj moju vrstu sa razlomcima lijepo zapisanom, makar je i ovo funkcionalno

                Fraction[,] matricaRazlomci = new Fraction[redMatrice, redMatrice];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matricaRazlomci[i, j] = new Fraction(matricaRedaTri[i, j]) / iznosDeterminante;
                        matricaRazlomci[i, j] = Fraction.SkratiRazlomak(matricaRazlomci[i, j]);
                    }
                }
                postupak.Append(dodajMatricu(matricaRazlomci));
            }
            else;

            return postupak.ToString();
        }
        private String inverzElementarnimTrans(double[,] elementi, int redMatrice)
        {
            StringBuilder postupak = new StringBuilder();

            if (redMatrice == 2)
            {

            }
            else if (redMatrice == 3)
            {
                double[,] inverznaMatrica = stvoriJedinicnuMatricu(redMatrice);

                postupak.Append(determinantaRedaTriSarrus(elementi));
                PomocneFunkcije.DodajDuziNoviRed(ref postupak);
                PomocneFunkcije.DodajDuziNoviRed(ref postupak);

                if (izracunajDeterminantuSarrus(elementi) == 0)
                {
                    postupak.Append(@"NemaInverza");
                }
                else
                {
                    Fraction[,] elementiRazlomci = new Fraction[redMatrice, redMatrice];
                    Fraction djeljitelj, temp, faktor;
                    int brRetkaNadenoga, dodanPrvi;
                    for (int i = 0; i < redMatrice; i++)
                    {
                        for (int j = 0; j < redMatrice; j++)
                        {
                            elementiRazlomci[i, j] = new Fraction(elementi[i, j]);
                        }
                    }

                    Fraction[,] inverznaMatricaRazlomci = new Fraction[redMatrice, redMatrice];

                    for (int i = 0; i < redMatrice; i++)
                    {
                        for (int j = 0; j < redMatrice; j++)
                        {
                            inverznaMatricaRazlomci[i, j] = new Fraction(inverznaMatrica[i, j]);
                        }
                    }

                    for (int brPostupka = 0; brPostupka < redMatrice; brPostupka++)
                    {

                        postupak.Append(ispisMatriceZaInverz(elementiRazlomci, inverznaMatricaRazlomci));



                        //ako je 0 zamjeni redove
                        if (elementiRazlomci[brPostupka, brPostupka].Equals(new Fraction(0)) == true)
                        {
                            //nademo ne nul clan
                            brRetkaNadenoga = nadiNeNulStozer(elementiRazlomci, brPostupka);


                            postupak.Append(@"=");
                            postupak.Append((brPostupka + 1).ToString() + @"r <=> " + (brRetkaNadenoga + 1).ToString() + @"r");
                            //zamjenimo
                            for (int j = 0; j < redMatrice; j++)
                            {
                                temp = elementiRazlomci[brPostupka, j];
                                elementiRazlomci[brPostupka, j] = elementiRazlomci[brRetkaNadenoga, j];
                                elementiRazlomci[brRetkaNadenoga, j] = temp;

                                temp = inverznaMatricaRazlomci[brPostupka, j];
                                inverznaMatricaRazlomci[brPostupka, j] = inverznaMatricaRazlomci[brRetkaNadenoga, j];
                                inverznaMatricaRazlomci[brRetkaNadenoga, j] = temp;
                            }


                            postupak.Append(@" = ");
                            postupak.Append(ispisMatriceZaInverz(elementiRazlomci, inverznaMatricaRazlomci));

                        }



                        //ako je razlicit od 1, dijeli sa tim razlomkom da bude jedan
                        if (elementiRazlomci[brPostupka, brPostupka].Equals(new Fraction(1)) == false)
                        {
                            postupak.Append(@"=");
                            postupak.Append((brPostupka + 1).ToString() + @"r :" + dodajZagradu(elementiRazlomci[brPostupka, brPostupka]));

                            djeljitelj = elementiRazlomci[brPostupka, brPostupka];

                            for (int j = 0; j < redMatrice; j++)
                            {
                                elementiRazlomci[brPostupka, j] /= djeljitelj;
                                elementiRazlomci[brPostupka, j] = Fraction.SkratiRazlomak(elementiRazlomci[brPostupka, j]);
                                inverznaMatricaRazlomci[brPostupka, j] /= djeljitelj;
                                inverznaMatricaRazlomci[brPostupka, j] = Fraction.SkratiRazlomak(inverznaMatricaRazlomci[brPostupka, j]);
                            }


                            postupak.Append(@"=");
                            postupak.Append(ispisMatriceZaInverz(elementiRazlomci, inverznaMatricaRazlomci));

                        }


                        //mnozi red razlomkom i dodaj drugima
                        dodanPrvi = 0;

                        for (int i = 0; i < redMatrice; i++)
                        {
                            if (i != brPostupka && elementiRazlomci[i, brPostupka].Equals(new Fraction(0)) == false)
                            {
                                if (dodanPrvi == 0)
                                {
                                    postupak.Append(@"=");
                                    PomocneFunkcije.OtvoriMatricuHidden(ref postupak);
                                    dodanPrvi++;
                                }
                                else
                                {
                                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                                }
                                postupak.Append((i + 1).ToString() + @"r +" + (brPostupka + 1).ToString() + @"r *" + dodajZagradu(elementiRazlomci[i, brPostupka] * (-1)).ToString());

                                faktor = elementiRazlomci[i, brPostupka];

                                for (int j = 0; j < redMatrice; j++)
                                {
                                    elementiRazlomci[i, j] -= elementiRazlomci[brPostupka, j] * faktor;
                                    elementiRazlomci[i, j] = Fraction.SkratiRazlomak(elementiRazlomci[i, j]);
                                    inverznaMatricaRazlomci[i, j] -= inverznaMatricaRazlomci[brPostupka, j] * faktor;
                                    inverznaMatricaRazlomci[i, j] = Fraction.SkratiRazlomak(inverznaMatricaRazlomci[i, j]);
                                }
                            }
                        }
                        //postupak.Append(@"\\ \\"); mozda
                        if (dodanPrvi != 0)
                        {
                            PomocneFunkcije.ZatvoriMatricu(ref postupak);
                            postupak.Append(@"=");
                            postupak.Append(ispisMatriceZaInverz(elementiRazlomci, inverznaMatricaRazlomci));
                        }

                        PomocneFunkcije.DodajDuziNoviRed(ref postupak);

                    }

                    PomocneFunkcije.DodajDuziNoviRed(ref postupak);
                    postupak.Append(@"A^{-1}=");
                    postupak.Append(dodajMatricu(inverznaMatricaRazlomci));
                }


            }
            else if (redMatrice == 4)
            {


            }
            else;

            return postupak.ToString();
        }
        private String rangMatrice(Fraction[,] matrica)
        {
            StringBuilder postupak = new StringBuilder();
            int ukupnoRedaka = matrica.GetLength(0), ukupnoStupaca = matrica.GetLength(1);

            Fraction djeljitelj, temp, faktor;
            int brRetkaNadenoga, dodanPrvi;
            //jos fali da pomaknemo stupac udesno ako ne nade ne nul poziciju jer kod ranga je to moguce
            for (int brPostupka = 0; brPostupka < ukupnoRedaka; brPostupka++)
            {
                postupak.Append(dodajMatricu(matrica));

                //ako je 0 zamjeni redove
                if (matrica[brPostupka, brPostupka].Equals(new Fraction(0)) == true)
                {
                    //nademo ne nul clan
                    brRetkaNadenoga = nadiNeNulStozer(matrica, brPostupka);

                    postupak.Append(@"=");
                    postupak.Append((brPostupka + 1).ToString() + @"r <=> " + (brRetkaNadenoga + 1).ToString() + @"r");
                    //zamjenimo
                    for (int j = 0; j < ukupnoStupaca; j++)
                    {
                        temp = matrica[brPostupka, j];
                        matrica[brPostupka, j] = matrica[brRetkaNadenoga, j];
                        matrica[brRetkaNadenoga, j] = temp;

                    }


                    postupak.Append(@" = ");
                    postupak.Append(dodajMatricu(matrica));

                }

                //ako je razlicit od 1, dijeli sa tim razlomkom da bude jedan
                if (matrica[brPostupka, brPostupka].Equals(new Fraction(1)) == false)
                {
                    postupak.Append(@"=");
                    postupak.Append((brPostupka + 1).ToString() + @"r :" + dodajZagradu(matrica[brPostupka, brPostupka]));

                    djeljitelj = matrica[brPostupka, brPostupka];

                    for (int j = 0; j < ukupnoStupaca; j++)
                    {
                        matrica[brPostupka, j] /= djeljitelj;
                        matrica[brPostupka, j] = Fraction.SkratiRazlomak(matrica[brPostupka, j]);
                    }


                    postupak.Append(@"=");
                    postupak.Append(dodajMatricu(matrica));

                }


                //mnozi red razlomkom i dodaj drugima
                dodanPrvi = 0;

                for (int i = 0; i < ukupnoRedaka; i++)
                {
                    if (i != brPostupka && matrica[i, brPostupka].Equals(new Fraction(0)) == false)
                    {
                        if (dodanPrvi == 0)
                        {
                            postupak.Append(@"=");
                            PomocneFunkcije.OtvoriMatricuHidden(ref postupak);
                            dodanPrvi++;
                        }
                        else
                        {
                            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                        }
                        postupak.Append((i + 1).ToString() + @"r +" + (brPostupka + 1).ToString() + @"r *" + dodajZagradu(matrica[i, brPostupka] * (-1)).ToString());

                        faktor = matrica[i, brPostupka];

                        for (int j = 0; j < ukupnoStupaca; j++)
                        {
                            matrica[i, j] -= matrica[brPostupka, j] * faktor;
                            matrica[i, j] = Fraction.SkratiRazlomak(matrica[i, j]);
                        }
                    }
                }
                //postupak.Append(@"\\ \\"); mozda
                if (dodanPrvi != 0)
                {
                    PomocneFunkcije.ZatvoriMatricu(ref postupak);
                    postupak.Append(@"=");
                    postupak.Append(dodajMatricu(matrica));
                }

                PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            }
            return postupak.ToString();
        }
        private String rjesavanjeJednazdbi(Fraction[,] matrica, Fraction[] rjesenjeStupac)
        {
            StringBuilder postupak = new StringBuilder();
            int ukupnoRedaka = matrica.GetLength(0);
            int ukupnoStupaca = matrica.GetLength(1) + 1;
            Fraction djeljitelj, temp, faktor;
            int brRetkaNadenoga, dodanPrvi;

            for (int brPostupka = 0; brPostupka < ukupnoRedaka; brPostupka++)
            {
                
                postupak.Append(ispisZaJednadzbe(matrica, rjesenjeStupac));
                
                //ako je 0 zamjeni redove
                if (matrica[brPostupka, brPostupka].Equals(new Fraction(0)) == true)
                {
                    //nademo ne nul clan
                    brRetkaNadenoga = nadiNeNulStozer(matrica, brPostupka);

                    postupak.Append(@"=");
                    postupak.Append((brPostupka + 1).ToString() + @"r <=> " + (brRetkaNadenoga + 1).ToString() + @"r");
                    //zamjenimo
                    for (int j = 0; j < ukupnoStupaca; j++)
                    {
                        if(j != (ukupnoStupaca-1))
                        {
                            temp = matrica[brPostupka, j];
                            matrica[brPostupka, j] = matrica[brRetkaNadenoga, j];
                            matrica[brRetkaNadenoga, j] = temp;
                        }
                        else
                        {
                            temp = rjesenjeStupac[brPostupka];
                            rjesenjeStupac[brPostupka] = rjesenjeStupac[brRetkaNadenoga];
                            rjesenjeStupac[brRetkaNadenoga] = temp;
                        }
                        

                    }

                    postupak.Append(@" = ");
                    postupak.Append(ispisZaJednadzbe(matrica, rjesenjeStupac));

                }

                //ako je razlicit od 1, dijeli sa tim razlomkom da bude jedan
                if (matrica[brPostupka, brPostupka].Equals(new Fraction(1)) == false)
                {
                    postupak.Append(@"=");
                    postupak.Append((brPostupka + 1).ToString() + @"r :" + dodajZagradu(matrica[brPostupka, brPostupka]));

                    djeljitelj = matrica[brPostupka, brPostupka];

                    for (int j = 0; j < ukupnoStupaca; j++)
                    {
                        if (j != (ukupnoStupaca - 1))
                        {
                            matrica[brPostupka, j] /= djeljitelj;
                            matrica[brPostupka, j] = Fraction.SkratiRazlomak(matrica[brPostupka, j]);
                        }
                        else
                        {
                            rjesenjeStupac[brPostupka] /= djeljitelj;
                            rjesenjeStupac[brPostupka] = Fraction.SkratiRazlomak(rjesenjeStupac[brPostupka]);
                        }
                            
                    }

                    postupak.Append(@"=");
                    postupak.Append(ispisZaJednadzbe(matrica, rjesenjeStupac));

                }

                //mnozi red razlomkom i dodaj drugima
                dodanPrvi = 0;

                for (int i = 0; i < ukupnoRedaka; i++)
                {
                    if (i != brPostupka && matrica[i, brPostupka].Equals(new Fraction(0)) == false)
                    {
                        if (dodanPrvi == 0)
                        {
                            postupak.Append(@"=");
                            PomocneFunkcije.OtvoriMatricuHidden(ref postupak);
                            dodanPrvi++;
                        }
                        else
                        {
                            postupak.Append(@"\\");
                        }
                        postupak.Append((i + 1).ToString() + @"r +" + (brPostupka + 1).ToString() + @"r *" + dodajZagradu(matrica[i, brPostupka] * (-1)).ToString());

                        faktor = matrica[i, brPostupka];

                        for (int j = 0; j < ukupnoStupaca; j++)
                        {
                            if (j != (ukupnoStupaca - 1))
                            {
                                matrica[i, j] -= matrica[brPostupka, j] * faktor;
                                matrica[i, j] = Fraction.SkratiRazlomak(matrica[i, j]);
                            }
                            else
                            {
                                rjesenjeStupac[i] -= rjesenjeStupac[brPostupka] * faktor;
                                rjesenjeStupac[i] = Fraction.SkratiRazlomak(rjesenjeStupac[i]);
                            }
                        }
                    }
                }
                //postupak.Append(@"\\ \\"); mozda
                if (dodanPrvi != 0)
                {
                    PomocneFunkcije.ZatvoriMatricu(ref postupak);
                    postupak.Append(@"=");
                    postupak.Append(ispisZaJednadzbe(matrica, rjesenjeStupac));
                }

                PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            }

            return postupak.ToString();
        }
        private String izracunajVektorskiUmnozak(double[] prviVektor, double[] drugiVektor)
        {
            StringBuilder postupak = new StringBuilder();

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(@"\vec{i} & \vec{j} & \vec{k} \\ ");
            postupak.Append(prviVektor[0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(prviVektor[1]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(prviVektor[2]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(drugiVektor[0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(drugiVektor[1]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(drugiVektor[2]);


            postupak.Append(@"} = \vec{i} * ");

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(prviVektor[1]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(prviVektor[2]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(drugiVektor[1]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(drugiVektor[2]);
            postupak.Append(@"} - \vec{j} * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(prviVektor[0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(prviVektor[2]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(drugiVektor[0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(drugiVektor[2]);
            postupak.Append(@"} + \vec{k} * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(prviVektor[0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(prviVektor[1]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(drugiVektor[0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(drugiVektor[1]);


            postupak.Append(@"} = (");

            postupak.Append(prviVektor[1] * drugiVektor[2]);
            postupak.Append(@" - ");
            if (prviVektor[2] * drugiVektor[1] < 0)
            {
                postupak.Append(@"(");
            }
            postupak.Append(prviVektor[2] * drugiVektor[1]);
            if (prviVektor[2] * drugiVektor[1] < 0)
            {
                postupak.Append(@")");
            }
            postupak.Append(@", -(");

            postupak.Append(prviVektor[0] * drugiVektor[2]);
            postupak.Append(@" - ");
            if (prviVektor[2] * drugiVektor[0] < 0)
            {
                postupak.Append(@"(");
            }
            postupak.Append(prviVektor[2] * drugiVektor[0]);
            if (prviVektor[2] * drugiVektor[0] < 0)
            {
                postupak.Append(@")");
            }
            postupak.Append(@"), ");

            postupak.Append(prviVektor[0] * drugiVektor[1]);
            postupak.Append(@" - ");
            if (prviVektor[1] * drugiVektor[0] < 0)
            {
                postupak.Append(@"(");
            }
            postupak.Append(prviVektor[1] * drugiVektor[0]);
            if (prviVektor[1] * drugiVektor[0] < 0)
            {
                postupak.Append(@")");
            }
            postupak.Append(@") \\ \\ = (");

            postupak.Append(prviVektor[1] * drugiVektor[2] - prviVektor[2] * drugiVektor[1]);
            postupak.Append(@", ");
            postupak.Append(-1 * (prviVektor[0] * drugiVektor[2] - prviVektor[2] * drugiVektor[0]));
            postupak.Append(@", ");
            postupak.Append(prviVektor[0] * drugiVektor[1] - prviVektor[1] * drugiVektor[0]);
            postupak.Append(@")");

            return postupak.ToString();
        }
        private String izracunajRavninuPrekoTocki(double[] tockaA, double[] tockaB, double[] tockaC)
        {
            StringBuilder postupak = new StringBuilder();
            string[] znakovi = new string[] { "x", "y", "z" };
            double prvaMatElem1 = tockaB[1] - tockaA[1], prvaMatElem2 = tockaB[2] - tockaA[2], prvaMatElem3 = tockaC[1] - tockaA[1], prvaMatElem4 = tockaC[2] - tockaA[2];
            double drugaMatElem1 = tockaB[0] - tockaA[0], drugaMatElem2 = tockaB[2] - tockaA[2], drugaMatElem3 = tockaC[0] - tockaA[0], drugaMatElem4 = tockaC[2] - tockaA[2];
            double trecaMatElem1 = tockaB[0] - tockaA[0], trecaMatElem2 = tockaB[1] - tockaA[1], trecaMatElem3 = tockaC[0] - tockaA[0], trecaMatElem4 = tockaC[1] - tockaA[1];


            //fali formula
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(dodajZaRavninuBezRacuna(tockaA, znakovi));
            postupak.Append(@" \\ ");
            postupak.Append(dodajZaRavninuBezRacuna(tockaA, tockaB.Select(x => x.ToString()).ToArray()));
            postupak.Append(@" \\ ");
            postupak.Append(dodajZaRavninuBezRacuna(tockaA, tockaC.Select(x => x.ToString()).ToArray()));

            postupak.Append(@"} = ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(dodajZaRavninuSaRacunom(tockaA, znakovi));
            postupak.Append(@" \\ ");
            postupak.Append(dodajZaRavninuSaRacunom(tockaA, tockaB.Select(x => x.ToString()).ToArray()));
            postupak.Append(@" \\ ");
            postupak.Append(dodajZaRavninuSaRacunom(tockaA, tockaC.Select(x => x.ToString()).ToArray()));



            postupak.Append(@"} \\ = (");
            postupak.Append(znakovi[0]);
            postupak.Append(dodajTocku(tockaA[0]));
            postupak.Append(@") * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(prvaMatElem1);
            postupak.Append(@" & ");
            postupak.Append(prvaMatElem2);
            postupak.Append(@" \\ ");
            postupak.Append(prvaMatElem3);
            postupak.Append(@" & ");
            postupak.Append(prvaMatElem4);
            postupak.Append(@"} - (");

            postupak.Append(znakovi[1]);
            postupak.Append(dodajTocku(tockaA[1]));
            postupak.Append(@") * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(drugaMatElem1);
            postupak.Append(@" & ");
            postupak.Append(drugaMatElem2);
            postupak.Append(@" \\ ");
            postupak.Append(drugaMatElem3);
            postupak.Append(@" & ");
            postupak.Append(drugaMatElem4);
            postupak.Append(@"} + (");

            postupak.Append(znakovi[2]);
            postupak.Append(dodajTocku(tockaA[2]));
            postupak.Append(@") * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(trecaMatElem1);
            postupak.Append(@" & ");
            postupak.Append(trecaMatElem2);
            postupak.Append(@" \\ ");
            postupak.Append(trecaMatElem3);
            postupak.Append(@" & ");
            postupak.Append(trecaMatElem4);
            postupak.Append(@"} \\ = (");

            postupak.Append(znakovi[0]);
            postupak.Append(dodajTocku(tockaA[0]));
            postupak.Append(@") * (");
            postupak.Append(prvaMatElem1 * prvaMatElem4);
            postupak.Append(@"-");
            postupak.Append(dodajZagradu(prvaMatElem2 * prvaMatElem3));
            postupak.Append(@") - (");

            postupak.Append(znakovi[1]);
            postupak.Append(dodajTocku(tockaA[1]));
            postupak.Append(@") * (");
            postupak.Append(drugaMatElem1 * drugaMatElem4);
            postupak.Append(@"-");
            postupak.Append(dodajZagradu(drugaMatElem2 * drugaMatElem3));
            postupak.Append(@") + (");

            postupak.Append(znakovi[2]);
            postupak.Append(dodajTocku(tockaA[2]));
            postupak.Append(@") * (");
            postupak.Append(trecaMatElem1 * trecaMatElem4);
            postupak.Append(@"-");
            postupak.Append(dodajZagradu(trecaMatElem2 * trecaMatElem3));
            postupak.Append(@")");


            double brUzX = prvaMatElem1 * prvaMatElem4 - prvaMatElem2 * prvaMatElem3;
            double brUzY = drugaMatElem1 * drugaMatElem4 - drugaMatElem2 * drugaMatElem3;
            double brUzZ = trecaMatElem1 * trecaMatElem4 - trecaMatElem2 * trecaMatElem3;
            double slobodniClan = (-1) * (brUzX * tockaA[0] - brUzY * tockaA[1] + brUzZ * tockaA[2]);

            postupak.Append(@" \\ ");
            postupak.Append(brUzX);
            postupak.Append(@"*(");
            postupak.Append(znakovi[0]);
            postupak.Append(dodajTocku(tockaA[0]));
            postupak.Append(@") ");
            postupak.Append(dodajTocku(brUzY));
            postupak.Append(@"*(");
            postupak.Append(znakovi[1]);
            postupak.Append(dodajTocku(tockaA[1]));
            postupak.Append(@") ");
            if (brUzZ > 0)
            {
                postupak.Append(@"+");
            }
            postupak.Append(brUzZ);
            postupak.Append(@"*(");
            postupak.Append(znakovi[2]);
            postupak.Append(dodajTocku(tockaA[2]));
            postupak.Append(@") = 0 \\");
            //fali linija postupka

            postupak.Append(brUzX);
            postupak.Append(znakovi[0]);
            if (brUzY > 0)
            {
                postupak.Append(@"+");
            }
            postupak.Append(brUzY);
            postupak.Append(znakovi[1]);
            if (brUzZ > 0)
            {
                postupak.Append(@"+");
            }
            postupak.Append(brUzZ);
            postupak.Append(znakovi[2]);
            if (slobodniClan > 0)
            {
                postupak.Append(@"+");
            }
            postupak.Append(slobodniClan);
            postupak.Append(@" = 0");


            return postupak.ToString();
        }
        private String determinantaRedaTriSarrus(double[,] elementi)
        {
            double[] dijelovi = new double[6];
            double rezultat;

            StringBuilder postupak = new StringBuilder();

            postupak.Append(dodajMatricu(elementi));

            PomocneFunkcije.OtvoriMatricuHidden(ref postupak);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    postupak.Append(elementi[i, j]);
                    if (i == 2 && j == 1)
                    {
                        PomocneFunkcije.ZatvoriMatricu(ref postupak);
                        postupak.Append(@" = ");
                    }
                    else if ((j + 1) % 2 == 0)
                    {
                        PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                    }
                    else
                    {
                        PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                dijelovi[i] = elementi[0, i % 3] * elementi[1, (i + 1) % 3] * elementi[2, (i + 2) % 3];

                postupak.Append(dodajZagradu(elementi[0, i % 3]));
                postupak.Append(@"*");
                postupak.Append(dodajZagradu(elementi[1, (i + 1) % 3]));
                postupak.Append(@"*");
                postupak.Append(dodajZagradu(elementi[2, (i + 2) % 3]));

                if (i == 2)
                {
                    postupak.Append(@"-[");
                }
                else
                {
                    postupak.Append(@"+");
                }
            }

            for (int i = 0; i < 3; i++)
            {
                dijelovi[i + 3] = elementi[2, i % 3] * elementi[1, (i + 1) % 3] * elementi[0, (i + 2) % 3];

                postupak.Append(dodajZagradu(elementi[2, i % 3]));
                postupak.Append(@"*");
                postupak.Append(dodajZagradu(elementi[1, (i + 1) % 3]));
                postupak.Append(@"*");
                postupak.Append(dodajZagradu(elementi[0, (i + 2) % 3]));

                if (i == 2)
                {
                    postupak.Append(@"] \\ \\ =");
                }
                else
                {
                    postupak.Append(@"+");
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (dijelovi[i] >= 0 && i != 0)
                {
                    postupak.Append(@"+");
                }
                postupak.Append(dijelovi[i]);
            }

            for (int i = 3; i < 6; i++)
            {
                if (dijelovi[i] < 0)
                {
                    postupak.Append(@"+");
                }
                else
                {
                    postupak.Append(@"-");
                }
                postupak.Append(Math.Abs(dijelovi[i]));
            }

            rezultat = dijelovi[0] + dijelovi[1] + dijelovi[2] - dijelovi[3] - dijelovi[4] - dijelovi[5];

            postupak.Append(@"=");
            postupak.Append(rezultat);

            return postupak.ToString();
        }
        private String determinantaRedaTriLaplace(double[,] elementi)
        {

            StringBuilder postupak = new StringBuilder();

            postupak.Append(dodajMatricu(elementi));
            postupak.Append(@" = ");
            postupak.Append(elementi[0, 0]);
            postupak.Append(@" * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(elementi[1, 1]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(elementi[1, 2]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(elementi[2, 1]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(elementi[2, 2]);
            postupak.Append(@"} - ");
            postupak.Append(dodajZagradu(elementi[0, 1]));
            postupak.Append(@" * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(elementi[1, 0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(elementi[1, 2]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(elementi[2, 0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(elementi[2, 2]);
            postupak.Append(@"} + ");
            postupak.Append(dodajZagradu(elementi[0, 2]));
            postupak.Append(@" * ");
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            postupak.Append(elementi[1, 0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(elementi[1, 1]);
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);
            postupak.Append(elementi[2, 0]);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(elementi[2, 1]);
            postupak.Append(@"} = ");

            double lijeviUmnozakPrvi = elementi[1, 1] * elementi[2, 2], desniUmnozakPrvi = elementi[1, 2] * elementi[2, 1];
            double lijeviUmnozakDrugi = elementi[1, 0] * elementi[2, 2], desniUmnozakDrugi = elementi[1, 2] * elementi[2, 0];
            double lijeviUmnozakTreci = elementi[1, 0] * elementi[2, 1], desniUmnozakTreci = elementi[1, 1] * elementi[2, 0];

            postupak.Append(elementi[0, 0]);
            postupak.Append(@"*(");
            postupak.Append(lijeviUmnozakPrvi);
            postupak.Append(@"-");
            postupak.Append(dodajZagradu(desniUmnozakPrvi));
            postupak.Append(@")");

            postupak.Append(dodajIzracunatiPreznakNakonMinusa(elementi[0, 1]));
            postupak.Append(@"*(");
            postupak.Append(lijeviUmnozakDrugi);
            postupak.Append(@"-");
            postupak.Append(dodajZagradu(desniUmnozakDrugi));
            postupak.Append(@")");

            postupak.Append(dodajIzracunatiPreznakNakonPlusa(elementi[0, 2]));
            postupak.Append(@"*(");
            postupak.Append(lijeviUmnozakTreci);
            postupak.Append(@"-");
            postupak.Append(dodajZagradu(desniUmnozakTreci));
            postupak.Append(@") \\ \\ = ");

            double prvi = elementi[0, 0] * (lijeviUmnozakPrvi - desniUmnozakPrvi);
            double drugi = -1 * elementi[0, 1] * (lijeviUmnozakDrugi - desniUmnozakDrugi);
            double treci = elementi[0, 2] * (lijeviUmnozakTreci - desniUmnozakTreci);

            postupak.Append(prvi);
            postupak.Append(dodajIzracunatiPreznakNakonPlusa(drugi));
            postupak.Append(dodajIzracunatiPreznakNakonPlusa(treci));

            double rezultatDrugiNacin = prvi + drugi + treci;

            postupak.Append(@"=");
            postupak.Append(rezultatDrugiNacin);


            return postupak.ToString();
        }
        private String determinantaCramerTriReda(double[] prviRedMatriceGlavne, double[] drugiRedMatriceGlavne, double[] treciRedMatriceGlavne)
        {
            StringBuilder postupak = new StringBuilder();

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            for (int i = 0; i < prviRedMatriceGlavne.Length; i++)
            {
                postupak.Append(prviRedMatriceGlavne[i]);
                if (i == 2)
                {
                    postupak.Append(@"& | &");
                }
                else if (i == 3)
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }
                else
                {
                    PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                }

            }
            for (int i = 0; i < prviRedMatriceGlavne.Length; i++)
            {
                postupak.Append(drugiRedMatriceGlavne[i]);
                if (i == 2)
                {
                    postupak.Append(@"& | &");
                }
                else if (i == 3)
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }
                else
                {
                    PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                }

            }
            for (int i = 0; i < prviRedMatriceGlavne.Length; i++)
            {
                postupak.Append(treciRedMatriceGlavne[i]);
                if (i == 2)
                {
                    postupak.Append(@"& | &");
                }
                else if (i == 3) ;
                else
                {
                    PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                }

            }
            PomocneFunkcije.ZatvoriMatricu(ref postupak);
            PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            postupak.Append(@"D = ");


            double[,] elementi = new double[3, 3];
            int k;

            //1. matrica
            for (int i = 0; i < 3; i++)
            {
                elementi[0, i] = prviRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[1, i] = drugiRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[2, i] = treciRedMatriceGlavne[i];
            }

            double rezD = izracunajDeterminantuSarrus(elementi);
            postupak.Append(determinantaRedaTriSarrus(elementi));


            //2. matrica
            k = 0;
            for (int i = 0; i < 3; i++)
            {
                elementi[0, i] = prviRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[1, i] = drugiRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[2, i] = treciRedMatriceGlavne[i];
            }

            elementi[0, k] = prviRedMatriceGlavne[3];
            elementi[1, k] = drugiRedMatriceGlavne[3];
            elementi[2, k] = treciRedMatriceGlavne[3];

            double rezDx = izracunajDeterminantuSarrus(elementi);
            PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            postupak.Append(@"D_{x}=");
            postupak.Append(determinantaRedaTriSarrus(elementi));

            //3. matrica
            k = 1;
            for (int i = 0; i < 3; i++)
            {
                elementi[0, i] = prviRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[1, i] = drugiRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[2, i] = treciRedMatriceGlavne[i];
            }

            elementi[0, k] = prviRedMatriceGlavne[3];
            elementi[1, k] = drugiRedMatriceGlavne[3];
            elementi[2, k] = treciRedMatriceGlavne[3];

            double rezDy = izracunajDeterminantuSarrus(elementi);
            PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            postupak.Append(@"D_{y}=");
            postupak.Append(determinantaRedaTriSarrus(elementi));

            //4. matrica
            k = 2;
            for (int i = 0; i < 3; i++)
            {
                elementi[0, i] = prviRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[1, i] = drugiRedMatriceGlavne[i];
            }
            for (int i = 0; i < 3; i++)
            {
                elementi[2, i] = treciRedMatriceGlavne[i];
            }

            elementi[0, k] = prviRedMatriceGlavne[3];
            elementi[1, k] = drugiRedMatriceGlavne[3];
            elementi[2, k] = treciRedMatriceGlavne[3];

            double rezDz = izracunajDeterminantuSarrus(elementi);
            PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            postupak.Append(@"D_{z}=");
            postupak.Append(determinantaRedaTriSarrus(elementi));

            if (rezD != 0)
            {
                postupak.Append(@"\\ \\ \matrix{x=\frac{D_{x}}{D}=\frac{");
                postupak.Append(rezDx);
                postupak.Append(@"}{");
                postupak.Append(rezD);
                postupak.Append(@"}=");
                postupak.Append(rezDx / rezD);

                postupak.Append(@"&&&&& y=\frac{D_{y}}{D}=\frac{");
                postupak.Append(rezDy);
                postupak.Append(@"}{");
                postupak.Append(rezD);
                postupak.Append(@"}=");
                postupak.Append(rezDy / rezD);

                postupak.Append(@"&&&&& z=\frac{D_{z}}{D}=\frac{");
                postupak.Append(rezDz);
                postupak.Append(@"}{");
                postupak.Append(rezD);
                postupak.Append(@"}=");
                postupak.Append(rezDz / rezD);

                PomocneFunkcije.ZatvoriMatricu(ref postupak);
            }

            return postupak.ToString();
        }
        private String izracunajUmnozakMatrica(double[,] matricaPrva, double[,] matricaDruga)
        {
            StringBuilder postupak = new StringBuilder();

            if (matricaPrva.GetLength(1) != matricaDruga.GetLength(0))
            {
                return "BrojStupacaPrveMatriceNijeJednakBrojuRedakaDrugeMatrice";
            }

            PomocneFunkcije.OtvoriMatricuHidden(ref postupak);
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(dodajMatricu(matricaDruga));
            PomocneFunkcije.DodajKraciNoviRed(ref postupak);

            postupak.Append(dodajMatricu(matricaPrva));
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            PomocneFunkcije.ZatvoriMatricu(ref postupak);
            PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            //postupak.Append(@"& } \\ \\ = ");
            postupak.Append(@" = ");

            double[,] matricaUmnozka = new double[matricaPrva.GetLength(0), matricaDruga.GetLength(1)];
            int ukupnoUmnozaka = matricaPrva.GetLength(1);
            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            for (int i = 0; i < matricaUmnozka.GetLength(0); i++)
            {

                for (int j = 0; j < matricaUmnozka.GetLength(1); j++)
                {


                    for (int k = 0; k < ukupnoUmnozaka; k++)
                    {
                        matricaUmnozka[i, j] += matricaPrva[i, k] * matricaDruga[k, j];
                        postupak.Append(dodajZagradu(matricaPrva[i, k]) + @"*" + dodajZagradu(matricaDruga[k, j]));
                        if (k != (ukupnoUmnozaka - 1))
                        {
                            postupak.Append(@"+");
                        }
                    }

                    if (j != (matricaUmnozka.GetLength(1) - 1))
                    {
                        postupak.Append(@"&&&");
                    }
                }

                if (i != (matricaUmnozka.GetLength(0) - 1))
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }
                else
                {
                    PomocneFunkcije.ZatvoriMatricu(ref postupak);
                }


            }

            PomocneFunkcije.DodajDuziNoviRed(ref postupak);
            postupak.Append(@" = ");
            postupak.Append(dodajMatricu(matricaUmnozka));

            return postupak.ToString();
        }
        

        


        private double izracunajDeterminantuSarrus(double[,] elementi)
        {
            double[] dijelovi = new double[6];

            for (int i = 0; i < 3; i++)
            {
                dijelovi[i] = elementi[0, i % 3] * elementi[1, (i + 1) % 3] * elementi[2, (i + 2) % 3];
                dijelovi[i + 3] = elementi[2, i % 3] * elementi[1, (i + 1) % 3] * elementi[0, (i + 2) % 3];
            }

            return dijelovi[0] + dijelovi[1] + dijelovi[2] - dijelovi[3] - dijelovi[4] - dijelovi[5];
        }
        private double[,] transponirajMatricu(double[,] matricaKvadratna)
        {
            double[,] transponiranaMatrica = new double[matricaKvadratna.GetLength(0), matricaKvadratna.GetLength(1)];
            

            for(int i = 0; i < matricaKvadratna.GetLength(0); i++)
            {
                for(int j = 0; j < matricaKvadratna.GetLength(1); j++)
                {
                    transponiranaMatrica[i, j] = matricaKvadratna[j, i];
                }
            }
            return transponiranaMatrica;
        }
        private double[,] vratiNovuMatricu(int pozRed, int pozStu, double[,] elementi)
        {
            double[,] matricaRedaDva = new double[2, 2];
            int k = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(i != pozRed && j != pozStu)
                    {
                        matricaRedaDva[k / 2, k % 2] = elementi[i, j];
                        k++;
                    }
                }
            }

            return matricaRedaDva;
        }
        private double[,] stvoriJedinicnuMatricu(int redMatrice)
        {
            double[,] jedinicnaMatrica = new double[redMatrice, redMatrice];

            for (int i = 0; i < redMatrice; i++)
            {
                jedinicnaMatrica[i, i] = 1;
            }

            return jedinicnaMatrica;
        }
        private int nadiNeNulStozer(Fraction[,] elementiRazlomci, int brPostupka)
        {
            int redMatrice = elementiRazlomci.GetLength(0);
            int redakNaden = 0;

            for (int i = brPostupka + 1; i < redMatrice; i++)
            {
                if(elementiRazlomci[i, brPostupka].Equals(new Fraction(0)) == false)
                {
                    return i;
                }
            }

            return redakNaden;
        }
        

        
        
        private String dodajZaRavninuBezRacuna(double[] prva, string[] druga)
        {
            StringBuilder postupak = new StringBuilder();

            postupak.Append(druga[0]);
            postupak.Append(@"-");

            if (prva[0] < 0)
            {
                postupak.Append(@"(");
            }
            postupak.Append(prva[0]);
            if (prva[0] < 0)
            {
                postupak.Append(@")");
            }
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(druga[1]);
            postupak.Append(@"-");
            if (prva[1] < 0)
            {
                postupak.Append(@"(");
            }
            postupak.Append(prva[1]);
            if (prva[1] < 0)
            {
                postupak.Append(@")");
            }
            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(druga[2]);
            postupak.Append(@"-");
            if (prva[2] < 0)
            {
                postupak.Append(@"(");
            }
            postupak.Append(prva[2]);
            if (prva[2] < 0)
            {
                postupak.Append(@")");
            }

            return postupak.ToString();
        }
        private String dodajZaRavninuSaRacunom(double[] prva, string[] druga)
        {
            StringBuilder postupak = new StringBuilder();

            postupak.Append(druga[0]);
            postupak.Append(dodajTocku(prva[0]));

            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(druga[1]);
            postupak.Append(dodajTocku(prva[1]));

            PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
            postupak.Append(druga[2]);
            postupak.Append(dodajTocku(prva[2]));


            return postupak.ToString();
        }
        private String dodajTocku(double broj)
        {
            StringBuilder postupak = new StringBuilder();

            if (broj < 0)
            {
                postupak.Append(@"+");
            }
            else
            {
                postupak.Append(@"-");
            }
            postupak.Append(Math.Abs(broj));

            return postupak.ToString();
        }
        private String dodajMatricu(double[,] matrica)
        {
            StringBuilder postupak = new StringBuilder();

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    postupak.Append(matrica[i, j]);

                    if (j != (matrica.GetLength(1) - 1))
                    {
                        PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                    }
                }


                if (i != (matrica.GetLength(0) - 1))
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }
                else
                {
                    PomocneFunkcije.ZatvoriMatricu(ref postupak);
                }
            }

            return postupak.ToString();
        }
        private String dodajMatricu(Fraction[,] matrica)
        {
            StringBuilder postupak = new StringBuilder();

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    postupak.Append(matrica[i, j]);

                    if (j != (matrica.GetLength(1) - 1))
                    {
                        PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                    }
                }


                if (i != (matrica.GetLength(0) - 1))
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }
                else
                {
                    PomocneFunkcije.ZatvoriMatricu(ref postupak);
                }
            }

            return postupak.ToString();
        }
        private String ispisMatriceZaInverz(Fraction[,] zadana, Fraction[,] inverzna)
        {
            StringBuilder postupak = new StringBuilder();

            int redMatrice = zadana.GetLength(0);

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            for (int i = 0; i < redMatrice; i++)
            {
                for (int j = 0; j < redMatrice; j++)
                {
                    postupak.Append(zadana[i, j]);
                    PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                }

                postupak.Append(@" | ");
                PomocneFunkcije.DodajKraciNoviStupac(ref postupak);

                for (int j = 0; j < redMatrice; j++)
                {
                    postupak.Append(inverzna[i, j]);

                    if (j != (redMatrice - 1))
                    {
                        PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                    }

                }

                if (i != (redMatrice - 1))
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }

            }


            PomocneFunkcije.ZatvoriMatricu(ref postupak);

            return postupak.ToString();
        }
        private String ispisZaJednadzbe(Fraction[,] matrica, Fraction[] rjesenjeStupac)
        {
            StringBuilder postupak = new StringBuilder();

            int ukupnoRedaka = matrica.GetLength(0);
            int ukupnoStupaca = matrica.GetLength(1) + 1;

            PomocneFunkcije.OtvoriMatricuBrackets(ref postupak);
            for (int i = 0; i < ukupnoRedaka; i++)
            {
                for (int j = 0; j < ukupnoStupaca; j++)
                {
                    if (j == (ukupnoStupaca - 2))
                    {
                        postupak.Append(matrica[i, j]);
                        postupak.Append(@"& | &");
                    }
                    else if (j == (ukupnoStupaca - 1))
                    {
                        postupak.Append(rjesenjeStupac[i]);
                    }
                    else
                    {
                        postupak.Append(matrica[i, j]);
                        PomocneFunkcije.DodajKraciNoviStupac(ref postupak);
                    }
                }
                if (i < (ukupnoRedaka - 1))
                {
                    PomocneFunkcije.DodajKraciNoviRed(ref postupak);
                }
            }
            PomocneFunkcije.ZatvoriMatricu(ref postupak);

            return postupak.ToString();
        }
        private String dodajPozitivniBezPreznaka(double broj)
        {
            if (broj < 0)
            {
                return Math.Abs(broj).ToString();
            }
            else
            {
                return "-" + broj.ToString();
            }
        }
        private String dodajZagradu(double broj)
        {
            if (broj < 0)
            {
                return "(" + broj.ToString() + ")";
            }
            else
            {
                return broj.ToString();
            }
        }
        private String dodajZagradu(Fraction broj)
        {
            if (broj.GetBrojnik() < 0)
            {
                return "(" + broj.ToString() + ")";
            }
            else
            {
                return broj.ToString();
            }
        }
        private String dodajIzracunatiPreznakNakonMinusa(double broj)
        {
            if (broj < 0)
            {
                return "+" + Math.Abs(broj).ToString();
            }
            else
            {
                return "-" + broj.ToString();
            }
        }
        private String dodajIzracunatiPreznakNakonPlusa(double broj)
        {
            if (broj >= 0)
            {
                return "+" + broj.ToString();
            }
            else
            {
                return broj.ToString();
            }
        }

    }
}
