using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using ProExcelImportExport.Helper;

namespace ProExcelImportExport
{
    public partial class FoSucheSchueler : Form
    {
        TObjBestandsListe ObjBestandsListe;
        List<List<String>> ListeSuchergebnisse = new List<List<string>> { };
        List<List<String>> ListeAuswahlSchueler;
        bool sortAfterNameAndClass = true;

        public FoSucheSchueler(TObjBestandsListe ObjektBestandsListe)
        {
            InitializeComponent();
            ObjBestandsListe = ObjektBestandsListe;
            ListeAuswahlSchueler = new List<List<string>> { };
            rBKlasse.Checked = true;
            rBName.Checked = false;
        }

        private void SortiereListeSuche()
        {
            List<List<String>> sortedList = TObjKlassen.SortiereListeNachKlassen(ListeSuchergebnisse);

            ListeSuchergebnisse = sortedList;
            cLBSuchErgebnis.Items.Clear();

            foreach (List<String> schueler in ListeSuchergebnisse)
            {
                cLBSuchErgebnis.Items.Add(schueler[1] + " " + schueler[2] + ", " + schueler[3]);
            }

        }

        private void SortiereListeAuswahlSchueler()
        {
            List<List<String>> sortedList = new List<List<string>> { };
            if (sortAfterNameAndClass)
            {
                // Sortiere nach Klasse und Namen
                sortedList = TObjKlassen.SortiereListeNachKlassen(ListeAuswahlSchueler);
            }
            else
            {
                // Sortiere nach Namen
                sortedList = TObjKlassen.SortiereListeNachNamen(ListeAuswahlSchueler);
            }

            ListeAuswahlSchueler = sortedList;
            cLBAuswahlListe.Items.Clear();

            foreach (List<String> schueler in ListeAuswahlSchueler)
            {
                cLBAuswahlListe.Items.Add(schueler[1] + " " + schueler[2] + ", " + schueler[3]);
            }
        }

        // Methode zum Sortieren der Linken Liste
        private void SortiereListeSuchergebnisse()
        {
            // Sortiere nach Klasse und Namen 
            // Credits: Thomas Wendland
            ListeSuchergebnisse = TObjKlassen.SortiereListeNachKlassen(ListeSuchergebnisse);

            cLBSuchErgebnis.Items.Clear();

            foreach (List<String> schueler in ListeSuchergebnisse)
            {
                cLBSuchErgebnis.Items.Add(schueler[1] + " " + schueler[2] + ", " + schueler[3]);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            ObjBestandsListe.ArbeitsListeAusgewaehlterSchueler.Clear();

            foreach(List<String> Eintrag in ListeAuswahlSchueler)
            {
                ObjBestandsListe.ArbeitsListeAusgewaehlterSchueler.Add(Eintrag);
            }
            Close();           
        }

        private void tBEingabeSuchBegriff_TextChanged(object sender, EventArgs e)
        {
            List<List<String>> SuchErgebnisListe = ObjBestandsListe.SucheSchueler(tBEingabeSuchBegriff.Text); // Liste mit Schülern die Suche entsprechen
            cLBSuchErgebnis.Items.Clear();
            ListeSuchergebnisse = SuchErgebnisListe;

            foreach (List<String> schueler in SuchErgebnisListe)
            {
                if (!ListeAuswahlSchueler.Contains(schueler)) {
                    cLBSuchErgebnis.Items.Add(schueler[1] + " " + schueler[2] + ", " + schueler[3]);
                }
            }
        }

        private void cLBSuchErgebnis_SelectedValueChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < cLBSuchErgebnis.SelectedItems.Count; i++)
            {
                int index = cLBSuchErgebnis.Items.IndexOf(cLBSuchErgebnis.SelectedItems[i]);
                cLBAuswahlListe.Items.Add(cLBSuchErgebnis.SelectedItems[i]);
                ListeAuswahlSchueler.Add(ListeSuchergebnisse[index]);

                cLBSuchErgebnis.Items.RemoveAt(index);
                ListeSuchergebnisse.RemoveAt(index); // Die Zeile gabs davor nicht, da war der Fehler.
            }

            SortiereListeAuswahlSchueler();
        }

        private void bTAswahlAllerEintraege_Click(object sender, EventArgs e)
        {
            int AnzEintraege = cLBSuchErgebnis.Items.Count;
        }

        private void rBKlasse_Click(object sender, EventArgs e)
        {
            rBName.Checked = false;
            sortAfterNameAndClass = true;
            SortiereListeAuswahlSchueler();
        }

        private void rBName_Click(object sender, EventArgs e)
        {
            rBKlasse.Checked = false;
            sortAfterNameAndClass = false;
            SortiereListeAuswahlSchueler();
        }

        private void cLBAuswahlListe_SelectedValueChanged(object sender, EventArgs e)
        {
            List<List<String>> SuchErgebnisListe = ObjBestandsListe.SucheSchueler(tBEingabeSuchBegriff.Text); // Komplette Liste mit allen Suchergebnissen egal ob sie bereits in der Auswahlliste sind
            for (int i = 0; i < cLBAuswahlListe.SelectedItems.Count; i++)
            {
                int index = cLBAuswahlListe.Items.IndexOf(cLBAuswahlListe.SelectedItems[i]);

                // Die Variable ListeSuchergebnisse kann die Werte garnicht enthalten da wir sie ja löschen sobald wir sie von der linken Liste in die Rechte "schieben". Daher brauchen wir eine "cleane" Liste
                if (SuchErgebnisListe.Contains(ListeAuswahlSchueler[index]))
                {
                    cLBSuchErgebnis.Items.Add(cLBAuswahlListe.SelectedItems[i]);
                }

                ListeSuchergebnisse.Add(ListeAuswahlSchueler[index]); // Fehlte auch, hinzufügen des Schülers zu unserer "Background" Liste

                // Jetzt werden die wieder hinzugefügt, müssen aber noch sortiert werden.
                SortiereListeSuchergebnisse();

                ListeAuswahlSchueler.RemoveAt(index);
                cLBAuswahlListe.Items.RemoveAt(index);
            }
        }

        private void btExcelExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.DefaultExt = ".xlsx";
            SFD.RestoreDirectory = true;
            
            SFD.Filter = "Excel documents (.xlsx)|*.xlsx";

            DialogResult result = SFD.ShowDialog();
            if (result==DialogResult.OK)
            {
                String DateiName = SFD.FileName;

                TObjExcelExport ExcelObjekt = new TObjExcelExport(DateiName, ListeAuswahlSchueler);
                ExcelObjekt.ExportFancy();
            }
        }
    }
}
