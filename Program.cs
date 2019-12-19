using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using ProExcelImportExport.Helper;

namespace ProExcelImportExport
{
    public class TProgVerzeichnisse
    {
        public String StartVerzeichnis;
        public String ListenVerzeichnis;
        public String ZensosDatei;
        public Boolean ZensosDateiExistenz;
        public String BestandsDatei;
        public Boolean BestandsDateiExistenz;
        public TProgVerzeichnisse()
        {
            StartVerzeichnis = System.IO.Directory.GetCurrentDirectory();
            StartVerzeichnis = System.IO.Directory.GetParent(StartVerzeichnis).ToString(); // Entwicklung
            StartVerzeichnis = System.IO.Directory.GetParent(StartVerzeichnis).ToString(); // Entwicklung
            ListenVerzeichnis = StartVerzeichnis+@"\Listen";
            ZensosDatei = ListenVerzeichnis + @"\Zensos.xlsx";
            ZensosDateiExistenz = File.Exists(ZensosDatei);
            BestandsDatei = ListenVerzeichnis + @"\Bestand.xlsx";
            BestandsDateiExistenz = File.Exists(BestandsDatei);            
        }
    }

    public class TLeereEintragsListe
    {
        public List<List<String>> LeereListe = new List<List<string>> { };
    }


    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [STAThread]    // Compilerdirektive für die Nutzung von Systemdialogen
        static void Main(string[] args)
        {
            LoggerUtil.InitLogger();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TObjExcelImport ExcelBestandsListe;
            TObjBestandsListe ObjektBestandsListe;
            TObjExcelImport ExcelZensosListe;
            TObjZensosListe ObjektZensosListe;
            TProgVerzeichnisse ProgrammVerzeichnisse = new TProgVerzeichnisse();

            if (ProgrammVerzeichnisse.BestandsDateiExistenz)
            {
                ExcelBestandsListe = new TObjExcelImport(ProgrammVerzeichnisse.BestandsDatei);
                ObjektBestandsListe = new TObjBestandsListe(ExcelBestandsListe.EintragsListe);
            }
            else
            {
                TLeereEintragsListe LeereEintragsListe = new TLeereEintragsListe();
                ObjektBestandsListe = new TObjBestandsListe(LeereEintragsListe.LeereListe);
            }

            if (ProgrammVerzeichnisse.ZensosDateiExistenz)
            {
                ExcelZensosListe = new TObjExcelImport(ProgrammVerzeichnisse.ZensosDatei);
                ObjektZensosListe = new TObjZensosListe(ExcelZensosListe.EintragsListe);

            }
            else
            {
                TLeereEintragsListe LeereEintragsListe = new TLeereEintragsListe();
                ObjektZensosListe = new TObjZensosListe(LeereEintragsListe.LeereListe);
            }

            ObjektBestandsListe.GleicheBestandsListeMitZensosListeAb(ObjektZensosListe);

            List<List<String>> shorter = ObjektZensosListe.ListeDerSchuelerDieNichtInBestandsListeEnthaltenSind;
            for (int i = 0; i < shorter.Count; i++)
            {
                try
                {
                    Console.WriteLine(shorter[i][0] + " " + shorter[i][1] + " " + shorter[i][2] + " " + shorter[i][3] + " " + shorter[i][4] + " " + shorter[i][5]);
                } catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }

            foreach (List<String> Eintrag in ObjektZensosListe.ListeDerSchuelerMitKlassenwechselInZensosListe)
            {
                try
                {
                    Console.WriteLine(Eintrag[0] + " " + Eintrag[1] + " " + Eintrag[2] + " " + Eintrag[3] + " " + Eintrag[4] + " " + Eintrag[5]);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }

            Console.WriteLine("");
            foreach (List<String> Eintrag in ObjektBestandsListe.ListeDerSchuelerDieNichtInZensosListeEnthaltenSind)
            {
                Console.WriteLine(Eintrag[0] + " " + Eintrag[1] + " " + Eintrag[2] + " " + Eintrag[3] + " " + Eintrag[4] + " " + Eintrag[5]);
            }

            Console.WriteLine("");

            foreach (List<String> Eintrag in ObjektBestandsListe.SucheSchueler("Alex"))
            {
                Console.WriteLine(Eintrag[0] + " " + Eintrag[1] + " " + Eintrag[2] + " " + Eintrag[3] + " " + Eintrag[4] + " " + Eintrag[5]);
            }

            Console.WriteLine("");


            foreach (List<String> Eintrag in ObjektBestandsListe.WaehleKlassenInSchuelerListeAus("7"))
            {
                Console.WriteLine(Eintrag[0] + " " + Eintrag[1] + " " + Eintrag[2] + " " + Eintrag[3] + " " + Eintrag[4] + " " + Eintrag[5]);
            }

            Console.WriteLine("");

            foreach (List<String> Eintrag in ObjektBestandsListe.WaehleKlassenInSchuelerListeAus("JG19"))
            {
                Console.WriteLine(Eintrag[0] + " " + Eintrag[1] + " " + Eintrag[2] + " " + Eintrag[3] + " " + Eintrag[4] + " " + Eintrag[5] + " " + Eintrag[6] + " " + Eintrag[7]);
            }

            Console.WriteLine("done");
            Console.WriteLine("");

            FoSucheSchueler FoTest = new FoSucheSchueler(ObjektBestandsListe);
            FoTest.ShowDialog();

            Console.WriteLine("Auswahlergebnis: ");
            foreach (List<String> Eintrag in ObjektBestandsListe.ArbeitsListeAusgewaehlterSchueler)
            {
                Console.WriteLine(Eintrag[0] + " " + Eintrag[1] + " " + Eintrag[2] + " " + Eintrag[3] + " " + Eintrag[4] + " " + Eintrag[5]);
            }

            Console.ReadKey();         
        }
    }
}
