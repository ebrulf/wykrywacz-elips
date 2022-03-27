using System;
using System.Drawing;
using System.IO;
//using System.Drawing.Imagining;
//using System.Drawing.Drawing2D;

namespace Wyzwanie3
{
    class Lista
    {
        public class Węzeł
        {
            // w węźle przechowujemy "imię"
            public int x;
            public int y;
            public int nar;
            public Węzeł następny;
        }
        public Węzeł głowa;
        public Węzeł ogon;
        public bool CzyPusta()
        {
            return głowa == null;
        }
        public int ZwróćRozmiar()
        {
            int licznik = 0;
            for (Węzeł tmp = głowa; tmp != null; tmp = tmp.następny)
            {
                licznik++;
            }
            return licznik;
        }
        public void DodajDoGłowy(int x, int y, int nar)
        {
            Węzeł tmp = new Węzeł();
            tmp.x = x;
            tmp.y = y;
            tmp.nar = nar;
            // dodany element staje się głową, więc dotychczasowa głowa staje się obiektem, który jest "następny"
            tmp.następny = głowa;
            // dodany element staje się głową
            głowa = tmp;
            if (ogon == null)
                ogon = tmp;
        }
        public void DodajDoOgona(int x, int y, int nar)
        {
            Węzeł tmp = new Węzeł();
            tmp.x = x;
            tmp.y = y;
            tmp.nar = nar;
            if (ogon == null)
            {
                // lista jest pusta, więc ogon ma tę samą wartość, co głowa
                ogon = głowa = tmp;
            }
            else
            {
                ogon.następny = tmp;
                ogon = tmp;
            }
        }
        public void UsuńZGłowy()
        {
            //int tmp;
            if (głowa != null) // sprawdzamy, czy lista nie jest pusta
            {
                //tmp = głowa.x;
                głowa = głowa.następny;
                //return tmp;
            }
            else
                throw new Exception("Lista pusta!");
        }
        public void WyświetlRekurencyjnie()
        {
            WyświetlRekurencyjnie(głowa);
        }
        public void WyświetlRekurencyjnie(Węzeł w)
        {
            if (w == null)
            {
                return;
            }
            Console.WriteLine(w.x+" "+w.y+" "+w.nar);
            WyświetlRekurencyjnie(w.następny);
        }

    }
    /*
     * Z listy narożników wydzielimy listę lewych górnych
     * dla każdego z tej listy wydzielimy listę prawych górnych z pasującym x
     * dla każdego z tej listy wydzielimy listę prawych dolnych z pasującym y
     * dla każdego z tej listy wydzielimy listę lewych dolnych z pasującymi x y - albo nawet nie lista, to 1 element ma.
     * jak którakolwiek lista pusta - wiadomo, nie ma prostokąta
     * jak nie, to po kolei sprawdzamy, czy te cztery wierzchołki łączy linia prosta
     * i jak tak, wypluwamy numer i koordy
     * */
    class Pros
    {
        public int nr;
        public int xmin;
        public int xmax;
        public int ymin;
        public int ymax;
        public Pros(string n, string xmi, string xma, string ymi, string yma)
        {
            nr = Convert.ToInt32(n);
            xmin = Convert.ToInt32(xmi);
            xmax = Convert.ToInt32(xma);
            ymin = Convert.ToInt32(ymi);
            ymax = Convert.ToInt32(yma);
        }
    }
    class Program
    {
        static readonly Color czerwony = Color.FromArgb(237, 28, 36);
        static readonly Color czarny = Color.FromArgb(0, 0, 0);
        static readonly Color zielony = Color.FromArgb(34, 177, 76);

        static void Test()
        {
            var originalbmp = new Bitmap(Bitmap.FromFile("../../../Julian_Tuwim.png")); // Load a image to resize            
            var resizedbmp = new Bitmap(originalbmp, (int)originalbmp.Width / 2, (int)originalbmp.Height / 2); //  Resize the image into 5 times to its actual size
            resizedbmp.Save("../../../ResizedImage.png"); // Save the image
        }
        static bool CzyKolor(int x, int y, Bitmap b, Color kolor)
        {
            try
            {
                if (b.GetPixel(x, y) == kolor)
                    return true;
                else
                    return false;
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Za daleko " + x + " " + y);
                return false;
                throw e; 
            }
            finally { }
            //ArgumentOutOfRangeException
        }
        static int WykryjNarog1(int x, int y, Bitmap b, Color kolor)//pierwotna wersja
        {
            int w = 1;
            if (CzyKolor(x + 1, y, b, kolor) == true) w *= 2;
            if (CzyKolor(x, y + 1, b, kolor) == true) w *= 3;
            if (CzyKolor(x - 1, y, b, kolor) == true) w *= 5;
            if (CzyKolor(x, y - 1, b, kolor) == true) w *= 7;
            return w;
        }
        static int WykryjNarog(int x, int y, Bitmap b, Color kolor)
        {
            int w = 0;

            if (CzyKolor(x, y, b, kolor) == true) { w++; }
            if (CzyKolor(x + 1, y, b, kolor) == true) w *= 2;
            if (CzyKolor(x, y + 1, b, kolor) == true) w *= 3;
            if (CzyKolor(x - 1, y, b, kolor) == true) w *= 5;
            if (CzyKolor(x, y - 1, b, kolor) == true) w *= 7;
            if (CzyKolor(x + 1, y + 1, b, kolor) == true) w *= 11;
            if (CzyKolor(x - 1, y + 1, b, kolor) == true) w *= 13;
            if (CzyKolor(x - 1, y - 1, b, kolor) == true) w *= 17;
            if (CzyKolor(x + 1, y - 1, b, kolor) == true) w *= 19;
            return w;
        }
        static bool SzukajProst(Lista.Węzeł a, Lista.Węzeł b, Lista.Węzeł c, Lista.Węzeł d, Bitmap bi, Color kolor)//nie
        {
            int i = a.x;
            int j = a.y;
            for (; i != b.x; i++)
                if (CzyKolor(i, j, bi, kolor) == false) return false;
            for (; j != c.y; j++)
                if (CzyKolor(i, j, bi, kolor) == false) return false;
            for (; i != d.x; i--)
                if (CzyKolor(i, j, bi, kolor) == false) return false;
            for (; j != a.y; j--)
                if (CzyKolor(i, j, bi, kolor) == false) return false;
            return true;
        }
        static Lista SzukajWar(int x, int y, int dziel, Lista l)
        {
            Lista w = new Lista();
            //x<0 - nie szukamy po tej zmiennej
            for (Lista.Węzeł tmp = l.głowa; tmp != null; tmp = tmp.następny)
            {
                if (x < 0)
                {
                    if (y < 0)//nieupraszczać proszę
                    {
                        if (tmp.nar % dziel == 0)
                        {
                            //Console.WriteLine(tmp.x + " " + tmp.y + " " + tmp.nar + " znalezion1");
                            w.DodajDoOgona(tmp.x, tmp.y, tmp.nar);
                        }
                    }
                    else
                    {
                        if (tmp.nar % dziel == 0 && tmp.y == y)
                        {
                            //Console.WriteLine(tmp.x + " " + tmp.y + " " + tmp.nar + " znalezion2");
                            w.DodajDoOgona(tmp.x, tmp.y, tmp.nar);
                        }
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        if (tmp.nar % dziel == 0 && tmp.x == x)
                        {
                            //Console.WriteLine(tmp.x + " " + tmp.y + " " + tmp.nar + " znalezion3");
                            w.DodajDoOgona(tmp.x, tmp.y, tmp.nar);
                        }
                    }
                    else
                    {
                        if (tmp.nar % dziel == 0 && tmp.x == x && tmp.y == y)
                        {
                            //Console.WriteLine(tmp.x + " " + tmp.y + " " + tmp.nar + " znalezion4");
                            w.DodajDoOgona(tmp.x, tmp.y, tmp.nar);
                        }
                    }
                }
            }
            return w;
        }
        static void SzuPro(Lista becky, Bitmap bi, Color kolor, StreamWriter di)
        {
            //if (becky == null)
            //break;
            Lista a = SzukajWar(-1, -1, 6, becky);
            Lista b = new Lista();
            Lista c = new Lista();
            Lista d = new Lista();
            int i = 1;
            if (a != null)
            {
                //a.WyświetlRekurencyjnie(a.głowa);
                //Console.WriteLine("Oto i lewe górne narożniki. Jest ich "+a.ZwróćRozmiar());
                for (Lista.Węzeł at = a.głowa; at != null; at = at.następny)
                {

                    b = SzukajWar(-1, at.y, 15, becky);//x - kolumna, y - wiersz, 15
                    //b.WyświetlRekurencyjnie(b.głowa);
                    //Console.WriteLine("Oto i prawe górne narożniki. Jest ich "+b.ZwróćRozmiar()+" dla y="+at.y);
                    if (b != null)
                    {
                        for (Lista.Węzeł bt = b.głowa; bt != null; bt = bt.następny)
                        {
                            c = SzukajWar(bt.x, -1, 35, becky);//35
                            //c.WyświetlRekurencyjnie(c.głowa);
                            //Console.WriteLine("Oto i prawe dolne narożniki. Jest ich "+c.ZwróćRozmiar()+"dla x="+bt.x);
                            if (c != null)
                            {
                                for (Lista.Węzeł ct = c.głowa; ct != null; ct = ct.następny)
                                {
                                    d = SzukajWar(at.x, ct.y, 14, becky);//14
                                    if (d.ZwróćRozmiar() == 1)
                                        if (SzukajProst(at, bt, ct, d.głowa, bi, kolor) == true && bt.x - at.x > 2 && ct.y - bt.y > 2)//filtr 3x3  redukuje z 300+ do 100                                   
                                            di.WriteLine(i++ + " " + at.x + " " + bt.x + " " + bt.y + " " + ct.y);
                                }
                            }
                        }
                    }
                }
            }
        }
        static Lista SzukajNarog(Bitmap b, Color kolor)
        {
            int w = b.Width;
            int h = b.Height;
            int t;
            Lista n = new Lista();
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (CzyKolor(i, j, b, kolor) == true)
                    {
                        t = WykryjNarog(i, j, b, kolor);
                        if (t % 6 == 0 || t % 15 == 0 || t % 35 == 0 || t % 14 == 0)
                        {//t=6 - SzukajProst(i, j, obraz, czerwony); - ale dla elipsy analogicznie od razu szukać trza
                            n.DodajDoOgona(i, j, t);
                        }
                    }
                }
            }
            return n;
        }
        static void WykryjPro(string d, Color kolor)
        {
            Bitmap obraz = new Bitmap("../../../" + d + ".png");
            StreamWriter di = new StreamWriter("../../../" + d + ".txt");
            Lista prim = SzukajNarog(obraz, kolor);
            //prim.WyświetlRekurencyjnie();
            SzuPro(prim, obraz, czerwony, di);
            di.Close();
        }
        static Pros OdczytPro(StreamReader sr)
        {
            string linia, nr="", xmin="", xmax="", ymin="", ymax="";
            int i = 0;
            linia = sr.ReadLine();
            try
            {
                while (i < linia.Length)
                {
                    while (i < linia.Length && linia[i] != ' ')//linia.Length na przód + koniunkcja logiczna = nie trzeba sprawdzać
                    {
                        nr += linia[i];
                        i++;
                    }
                    i++;
                    while (i < linia.Length && linia[i] != ' ')
                    {
                        xmin += linia[i];
                        i++;
                    }
                    i++;
                    while (i < linia.Length && linia[i] != ' ')
                    {
                        xmax += linia[i];
                        i++;
                    }
                    i++;
                    while (i < linia.Length && linia[i] != ' ')
                    {
                        ymin += linia[i];
                        i++;
                    }
                    i++;

                    while (i < linia.Length && linia[i] != ' ')
                    {
                        ymax += linia[i];
                        i++;
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            finally { }
            Pros w = new Pros(nr, xmin, xmax, ymin, ymax);
            return w;
        }
        static void WczytajPro(string d, Color kolor)
        {
            Bitmap obraz = new Bitmap("../../../" + d + ".png");
            //Image obraf = Image.FromFile("../../../" + d + ".png");
            Pen myPen = new Pen(zielony);
            Graphics g = Graphics.FromImage(obraz);
            StreamReader di = new StreamReader("../../../" + d + ".txt");
            //string t, xmin, xmax, ymin, ymax;
            int i = 1;
            while(!di.EndOfStream)
            {
                Pros w = OdczytPro(di);
                Rectangle dw = new Rectangle(w.xmin, w.ymin, w.xmax - w.xmin, w.ymax - w.ymin);
                g.DrawRectangle(myPen, dw);
                if(w.nr!=i)
                {
                    Console.WriteLine("Posypała się numeracja w wierszu " + i);
                }
                i++;
            }
            obraz.Save("../../../" + d + "_1.png");
            myPen.Dispose();
        }
        static void WykryjEli(string d, Color kolor)
        {
            Bitmap obraz = new Bitmap("../../../" + d + ".png");
            StreamWriter di = new StreamWriter("../../../" + d + ".txt");
            SzukajNagor(obraz, kolor, di);
            
            di.Close();
        }
        static void WczytajEli(string d, Color kolor)
        {
            Bitmap obraz = new Bitmap("../../../" + d + ".png");
            StreamReader di = new StreamReader("../../../" + d + ".txt");
            Pen myPen = new Pen(zielony);
            Graphics g = Graphics.FromImage(obraz);
            while (!di.EndOfStream)
            {
                Pros w = OdczytPro(di);
                Rectangle dw = new Rectangle(w.xmin, w.ymin, w.xmax - w.xmin, w.ymax - w.ymin);
                //if(CzyKolor(w.xmin+(w.xmax-w.xmin)/2,w.ymin,obraz,kolor)==true&& CzyKolor(w.xmin + (w.xmax - w.xmin) / 2, w.ymax, obraz, kolor) == true&& CzyKolor(w.xmin, w.ymin + (w.ymax - w.ymin) / 2, obraz, kolor) == true&& CzyKolor(w.xmax, w.ymin + (w.ymax - w.ymin) / 2, obraz, kolor) == true)
                    g.DrawEllipse(myPen, dw);//dodatkowy filtr (nie działa
            }
            obraz.Save("../../../" + d + "_1.png");
            myPen.Dispose();
        }
        
        static void SzukajNagor(Bitmap b, Color kolor, StreamWriter di)
        {
            //Lista wi = new Lista();
            int w = b.Width;
            int h = b.Height;
            int t, k=0;
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (CzyKolor(i, j, b, kolor) == true)
                    {
                        t = WykryjNarog(i, j, b, kolor);
                        if (t % 26 == 0)
                        {//t=6 - SzukajProst(i, j, obraz, czerwony); - ale dla elipsy analogicznie od razu szukać trza
                            //Console.WriteLine("Kandydat " + i + " " + j);
                            if(SzukajEli(i,j,b,kolor, di,k)==true)
                            {
                                //Console.WriteLine("Wykryto elipsę.");
                            }
                        }
                    }
                }
            }
            //return wi;
        }
        static bool SzukajEli(int x, int y, Bitmap bi, Color kolor, StreamWriter di, int p)
        {
            //już streamwriter gotowy
            int x0 = x, y0 = y;
            int a, b=1, i = 1, j = 1, k=1, l=1;//a - długość górnej kreski, b - długość bocznej kreski, i - spadek w dół, j - spadek
                                               //int h = 0;
            int w = bi.Width;
            int h = bi.Height;
            int xmin, xmax, ymin, ymax, be, ka, pe, pa;
            for (a = 1; WykryjNarog(x + a, y, bi, kolor) % 2 == 1 && x+a<w && WykryjNarog(x + a, y, bi, kolor)>0; a++)
            {

            }
            //Console.WriteLine("a dokryte");
            if (WykryjNarog(x + a, y, bi, kolor) % 11 != 0 || y==0)
            {
                //Console.WriteLine("idzie w górę");
                return false;
            }
            else//kolejność ifów jest ważna
            {//spacer w tył
                // 17 7 19
                // 5  1 2
                // 13 3 11
                while ((WykryjNarog(x - i, y + j, bi, kolor) % 5 == 0 || WykryjNarog(x - i, y + j, bi, kolor) % 13 == 0 || WykryjNarog(x-i, y+j, bi, kolor)%3==0)&& x>i && y+j<h&& WykryjNarog(x - i, y + j, bi, kolor)>0)
                {
                    if (WykryjNarog(x - i, y + j, bi, kolor) % 3 == 0) { j++; }    //w dół                11   
                    else if (WykryjNarog(x - i, y + j, bi, kolor) % 13 == 0)
                    {
                        j++; i++;
                    }
                    else if (WykryjNarog(x - i, y + j, bi, kolor) % 5 == 0)//w tył 17
                    {
                        i++;
                    }
                }
                //Console.WriteLine("dalej w lewo się nie da. " + (int)(x - i) + " " + (int)(y + j));
                xmin = x - i;//tu jeszcze dobrze;
                for (be = 1; WykryjNarog(x-i, y+j-be, bi, kolor)%7==0;be++)
                {

                }
                //di.WriteLine((int)(++p) + " " + (int)(x - i) + " " + (int)(x + a + i) + " " + (int)y + " " + (int)(y + 2 * j - be));
                if (WykryjNarog(x - i, y + j, bi, kolor) % 11 != 0)
                    return false;
                else
                {//w dół w prawo
                    while ((WykryjNarog(x - i + k, y + j, bi, kolor) % 2 == 0 || WykryjNarog(x - i + k, y + j, bi, kolor) % 11 == 0 || WykryjNarog(x - i + k, y + j, bi, kolor) % 3 == 0)&&x-i+k<w&&y+j<h&& WykryjNarog(x - i + k, y + j, bi, kolor)>0)
                    {//w prawo iść - priorytet
                        if (WykryjNarog(x - i+k, y + j, bi, kolor) % 2 == 0) { k++; } //13                    
                        else if (WykryjNarog(x - i+k, y + j, bi, kolor) % 11 == 0)
                        {
                            j++; k++;
                        }
                        else if (WykryjNarog(x - i+k, y + j, bi, kolor) % 3 == 0)//tu w dół 19
                        {
                            j++;
                        }
                    }
                    //Console.WriteLine("dalej w dół się nie da. " + (int)(x - i + k) + " " + (int)(y + j)+" "+(int)(a+i-k));
                    ymax = y + j;//tu zaczyna się balanga
                    for (ka = 1; WykryjNarog(x - i+k-ka, y + j, bi, kolor) %5== 0; ka++)
                    {
                        //x-i+k-ka/2
                    }
                    if (WykryjNarog(x - i+k, y + j, bi, kolor) % 19 != 0)//jak symetria to górna belka i dolna kończą się tak samo
                        return false;
                    else
                    {
                        //zanim pójdziemy sprawdzić pozostałe 3/4 elipsy patrzcie:
                        //x+a/2 - środek width
                        //y+j-be/2 - środek height
                        //x-i x+a+i - to wyżej, niżej x-i+k-ka/2-(x-i)=k-ka/2
                        //y y+j-be+j - (z symetrii)
                        //Elipsa(pióro, prostokąt(x-i,y,2*i+a,2j+b)
                        //di.WriteLine((int)(++p) + " " + (int)(x - i) + " " + (int)(x + 2*k-ka) + " " + (int)y + " " + (int)(y + j));
                        //return true;//zrezygnowano z numeracji (do tego zmienna k, trudno z kontaktem danych)
                        //Console.WriteLine("ponad pół za nami. j już pewne");
                        //wyszło, ze to nie wystarcza, zatem trzeba będzie oblecieć całość. czyli od x-i do x-i+k i y y+j
                        while ((WykryjNarog(x - i + k, y + j-b, bi, kolor) % 2 == 0 || WykryjNarog(x - i + k, y + j-b, bi, kolor) % 19 == 0 || WykryjNarog(x - i + k, y + j-b, bi, kolor) % 7 == 0)&&x-i+k<w&&y+j-b>0&& WykryjNarog(x - i + k, y + j - b, bi, kolor)>0)
                        {
                            if (WykryjNarog(x - i+k, y + j-b, bi, kolor) % 7 == 0) { b++; }   // 15 tu nie w bok                   
                            else if (WykryjNarog(x - i+k, y + j-b, bi, kolor) % 19 == 0)
                            {
                                b++; k++;
                            }
                            else if (WykryjNarog(x - i+k, y + j-b, bi, kolor) % 2 == 0 )//tu nie w górę 11
                            {
                                k++;
                            }
                        }
                        //Console.WriteLine("dalej w prawo się nie da. " + (int)(x - i + k) + " " + (int)(y + j));
                        xmax = x - i + k;
                        for (pe = 1; WykryjNarog(x - i + k , y + j-b+pe, bi, kolor) % 3 == 0; pe++)
                        {
                            //y+j-b+pe/2
                        }
                        //di.WriteLine((int)(++p) + " " + (int)(x - i) + " " + (int)(x -i+k) + " " + (int)y + " " + (int)(y + 2*j-2*b+pe));
                        if (WykryjNarog(x - i + k, y + j - b, bi, kolor) % 17 != 0)
                            return false;
                        else
                        {
                            //Console.WriteLine("k już pewne");
                            while ((WykryjNarog(x - i + k-l, y + j - b, bi, kolor) % 5 == 0 || WykryjNarog(x - i + k-l, y + j - b, bi, kolor) % 17 == 0 || WykryjNarog(x - i + k-l, y + j - b, bi, kolor) % 7 == 0)&&x-i+k-l>0&&y+j-b>0&& WykryjNarog(x - i + k - l, y + j - b, bi, kolor)>0)
                            {
                                if (WykryjNarog(x - i + k-l, y + j - b, bi, kolor) % 5 == 0) { l++; }   //i tak idę w tył                     
                                else if (WykryjNarog(x - i + k-l, y + j - b, bi, kolor) % 17 == 0)
                                {
                                    b++; l++;
                                }
                                else if (WykryjNarog(x - i + k-l, y + j - b, bi, kolor) % 7 == 0)
                                {
                                    b++;
                                }//
                            }
                            //Console.WriteLine("dalej w górę się nie da. " + (int)(x - i + k) + " " + (int)(y + j-b));
                            ymin = y + j - b;
                            for(pa = 1; WykryjNarog(x - i + k-l+pa, y + j - b, bi, kolor) % 2 == 0; pa++)
                            {
                                //x-i+k-l+pa/2 y+j-b współrzędne góry
                                //x-i+k y+j-b/2 współrzędne prawo
                                //x-i
                            }
                            
                            //Console.WriteLine("k-l: " + (int)(k - l) + " i: " + i + " j: " + j + " b: " + b); tu już xmin szwank
                            //di.WriteLine((int)(++p) + " " + (int)(xmin) + " " + (int)(xmax) + " " + (int)(ymin) + " " + (int)(ymax));
                            
                            //if (k-l!=i)//j!-b
                            //    return false;
                            //else
                            {//jeszcze jeden test że boki tykają czarnego, góra, dół, lewo, prawo
                                //Console.WriteLine("Gratulacje, użytkowniku");
                                //if (CzyKolor(x + (a / 2), y + j - b, bi, kolor) == true && CzyKolor(x - i + (k/2), y + j, bi, kolor) == true && CzyKolor(x - i, y + j - (be / 2), bi, kolor) == true && CzyKolor(x - i + k, y + j - b + (pe / 2), bi, kolor) == true)
                                    di.WriteLine((int)(++p) + " " + (int)(x - i) + " " + (int)(x - i + k) + " " + (int)(y + j - b) + " " + (int)(y + j));
                                //else return false;
                                //di.WriteLine((int)(++p) + " " + (int)(xmin) + " " + (int)(xmax) + " " + (int)(ymin) + " " + (int)(ymax));
                            }//zapomniało się k na p przemienić, mój błąd

                        }//zderzenia nie działają. Julian Tuwim mojego autorstwa.
                    }
                }
            }            
            return true;
        }
        static void Main(string[] args)
        {
            Test();
            string d = "IMG_9080_zaznaczenia";
            string c = "zaznaczenia_3_prostokaty";
            string f = "zaznaczenia_elipsy";
            WykryjPro(d, czerwony);
            WykryjPro(c, czarny);
            WczytajPro(d, zielony);
            WykryjEli(f, czarny);//od razu zapisuje do pliku bez numerowania
            WczytajEli(f, czarny);
            //Argument.Invalid &&FileNotFoundException            
            Console.WriteLine();            
            //na razie algorytm bez szachownicy
            /*
             * potrzeba listy na trzy elementy - x,y,rodzaj skrzyżowania
             * znajdzie lewy górny - zaczynamy szukać prostokąta
             * idziemy w prawo, nieciągłość - zero, przerywamy. jak nie to prawy górny
             * elipsy do wykrycia - czarne
             */

        }
    }
}
