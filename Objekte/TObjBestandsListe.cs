using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProExcelImportExport.Helper
{
    public class TObjBestandsListe : TObjKlassen
    {
        public List<TSchueler> SchuelerObjektListe;
        // List<List<String>> UrListe: JGKZ - Name - Vorname - Geburtsdatum - Geschlecht
        // List<List<String>> SchuelerListe: JGKZ - Klasse - Name - Vorname - Geburtsdatum - Geschlecht
        public List<List<String>> ListeDerSchuelerDieNichtInZensosListeEnthaltenSind;
        public List<List<String>> ArbeitsListeAusgewaehlterSchueler;

        public TObjBestandsListe(List<List<String>> UrListe) : base(UrListe)
        {
            SchuelerObjektListe = new List<TSchueler> { };
            foreach (List<String> Eintrag in SchuelerListe)
            {
                TSchueler SchuelerObjekt = new TSchueler(Eintrag[0], Eintrag[1], Eintrag[2], Eintrag[3], Eintrag[4], Eintrag[5]);
                SchuelerObjektListe.Add(SchuelerObjekt);
            }
            ListeDerSchuelerDieNichtInZensosListeEnthaltenSind = new List<List<string>> { };
            ArbeitsListeAusgewaehlterSchueler = new List<List<string>> { };
        }

        public void GleicheBestandsListeMitZensosListeAb(TObjZensosListe ZensosObjekt)
        {
            List<String> IDSchuelerListe = new List<string> { };
            ListeDerSchuelerDieNichtInZensosListeEnthaltenSind.Clear();
            foreach (List<String> Eintrag in SchuelerListe)
            {
                String IDName = Eintrag[2] + ", " + Eintrag[3];
                IDSchuelerListe.Add(IDName);
            }

            if (ZensosObjekt.ListeDerSchuelerDieNichtInBestandsListeEnthaltenSind != null)
            {
                ZensosObjekt.ListeDerSchuelerDieNichtInBestandsListeEnthaltenSind.Clear();
            }

            ZensosObjekt.ListeDerSchuelerDieNichtInBestandsListeEnthaltenSind = ZensosObjekt.BestimmeSchuelerDieNichtInAndererListeEnthaltenSind(SchuelerListe);

            if (ZensosObjekt.ListeDerSchuelerMitKlassenwechselInZensosListe != null)
            {
                ZensosObjekt.ListeDerSchuelerMitKlassenwechselInZensosListe.Clear();
            }

            ZensosObjekt.ListeDerSchuelerMitKlassenwechselInZensosListe = ZensosObjekt.BestimmeSchuelerMitVeraenderungenInAndererSchuelerListe(SchuelerListe);

            ListeDerSchuelerDieNichtInZensosListeEnthaltenSind = BestimmeSchuelerDieNichtInAndererListeEnthaltenSind(ZensosObjekt.SchuelerListe);

            foreach (List<String> Eintrag in ListeDerSchuelerDieNichtInZensosListeEnthaltenSind)
            {
                Int32 SPos = SchuelerListe.IndexOf(Eintrag);
                if (SPos > -1)
                {
                    SchuelerListe[SPos][6] = "FALSCH";
                }
            }

            foreach (List<String> Eintrag in ZensosObjekt.ListeDerSchuelerMitKlassenwechselInZensosListe)
            {
                String VName = Eintrag[2] + ", " + Eintrag[3];
                Int32 i = -1;
                foreach (String SName in IDSchuelerListe)
                {
                    i++;
                    if (SName.Contains(VName) || VName.Contains(SName))
                    {
                        SchuelerListe[i][0] = Eintrag[0];
                        SchuelerListe[i][1] = Eintrag[1];
                    }
                }
            }

            AktualisiereSchuelerUndKlassenListen();
        }

        public List<List<String>> SucheSchueler(String SuchName)
        {
            List<List<String>> SuchErgebnisListe = new List<List<string>> { };
            String SName = SuchName.Trim().ToLower();

            if (SName.StartsWith("5") | SName.StartsWith("6") | SName.StartsWith("7") | SName.StartsWith("8") | SName.StartsWith("9") | SName.StartsWith("1")
                 | (SName.StartsWith("jg") && SName.Length > 3) | (SName.StartsWith("*") & SName.Length > 1))
            {
                if (SName.StartsWith("*"))
                {
                    SName = SName.Substring(1).Trim();
                }
                SuchErgebnisListe = WaehleKlassenInSchuelerListeAus(SName);
            }
            else
            {
                SuchErgebnisListe = SucheEintragInSchuelerListeAnhandSuchName(SName);
            }
            return SuchErgebnisListe;
        }
    }
}
