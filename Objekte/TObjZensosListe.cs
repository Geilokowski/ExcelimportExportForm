using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProExcelImportExport.Helper
{
    public class TObjZensosListe : TObjKlassen
    {
        // List<List<String>> UrListe: Klasse - Name - Vorname - Geburtsdatum - Geschlecht
        // List<List<String>> SchuelerListe: JGKZ - Klasse - Name - Vorname - Geburtsdatum - Geschlecht
        public List<List<String>> ListeDerSchuelerDieNichtInBestandsListeEnthaltenSind;
        public List<List<String>> ListeDerSchuelerMitKlassenwechselInZensosListe;


        public TObjZensosListe(List<List<String>> UrListe) : base(UrListe)
        {
            ListeDerSchuelerDieNichtInBestandsListeEnthaltenSind = new List<List<string>> { };
            ListeDerSchuelerMitKlassenwechselInZensosListe = new List<List<string>> { };
        }

        public List<List<String>> ErstelleNeueBestandsSchuelerListe()
        {
            List<List<String>> NeueBestandsListe = new List<List<String>> { };
            TSchueler SchuelerEintrag;
            foreach (List<String> Eintrag in SchuelerListe)
            {
                List<String> NeuerBestandSchueler = new List<string> { };
                // List<List<String>> NeueBestandsListe: JGKZ - Klasse - Name - Vorname - Geburtsdatum - Geschlecht

                SchuelerEintrag = new TSchueler(Eintrag[0], Eintrag[1], Eintrag[2], Eintrag[3], Eintrag[4], Eintrag[5]);

                NeuerBestandSchueler.Add(SchuelerEintrag.JahrgangKZ);
                NeuerBestandSchueler.Add(SchuelerEintrag.Klasse);
                NeuerBestandSchueler.Add(SchuelerEintrag.Name);
                NeuerBestandSchueler.Add(SchuelerEintrag.Vorname);
                NeuerBestandSchueler.Add(SchuelerEintrag.GeburtsDatum.ToShortDateString());
                NeuerBestandSchueler.Add(SchuelerEintrag.Geschlecht);
                NeuerBestandSchueler.Add(SchuelerEintrag.UserNameID);
                NeuerBestandSchueler.Add("true");  // InZensos
                NeuerBestandSchueler.Add("false");  // InITK
                NeuerBestandSchueler.Add("n.n.");  // UserNameITK
                NeuerBestandSchueler.Add("false");  // InO365
                NeuerBestandSchueler.Add("n.n.");  // UserNameO365
                NeueBestandsListe.Add(NeuerBestandSchueler);
            }
            return NeueBestandsListe;
        }

        public List<List<String>> ErstelleNeueBestandsUrListe()
        {
            List<List<String>> NeueBestandsListe = new List<List<String>> { };
            List<List<String>> BestandsListe = ErstelleNeueBestandsSchuelerListe();

            foreach (List<String> Eintrag in BestandsListe)
            {
                // List<List<String>> NeueBestandsListe: JGKZ - Name - Vorname - Geburtsdatum - Geschlecht ...

                List<String> NeuerBestandSchueler = new List<string> { };
                Eintrag.RemoveAt(1);
                NeueBestandsListe.Add(Eintrag);

            }
            return NeueBestandsListe;
        }

        public TObjBestandsListe CreateNewBestandsListeObject()
        {
            TObjBestandsListe BestandsListeObject = new TObjBestandsListe(ErstelleNeueBestandsUrListe());
            return BestandsListeObject;
        }



    }
}
