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

namespace ProExcelImportExport
{
    public partial class FoSucheSchueler : Form
    {
        TObjBestandsListe ObjBestandsListe;
        List<List<String>> ListeSuchergebnisse;
        List<List<String>> ListeAuswahlSchueler;
        
        public FoSucheSchueler(TObjBestandsListe ObjektBestandsListe)
        {
            InitializeComponent();
            ObjBestandsListe = ObjektBestandsListe;
            ListeSuchergebnisse = new List<List<string>> { };
            ListeAuswahlSchueler = new List<List<string>> { };
            rBKlasse.Checked = true;
            rBName.Checked = false;
        }

        private void LoescheEintraegeAuswahlListe(List<String> ZuLoeschendeEintraege)
        {
                    GleicheErgebnisListeMitAuswahlListeAb();
        }

        private void SortiereListeAuswahlSchueler()
        {
        }

        private void AddListeAuswahlSchueler(List<List<String>> ListeNeuerEintraege)
        {
            SortiereListeAuswahlSchueler();
        }

        private void GleicheErgebnisListeMitAuswahlListeAb()
        {
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
            List<List<String>> SuchErgebnisListeTest = ObjBestandsListe.SucheSchueler(tBEingabeSuchBegriff.Text);
            List<List<String>> SuchErgebnisListe = new List<List<string>> { };

            GleicheErgebnisListeMitAuswahlListeAb();
        }

        private void cLBSuchErgebnis_SelectedValueChanged(object sender, EventArgs e)
        {
            List<List<String>> ListeNeuerEintraege = new List<List<string>> { };
            List<String> ListeZuLoeschenderEintraege = new List<string> { };
            Int32 AnzEintraege = cLBSuchErgebnis.Items.Count;
            AddListeAuswahlSchueler(ListeNeuerEintraege);
            LoescheEintraegeAuswahlListe(ListeZuLoeschenderEintraege);
        }

        private void bTAswahlAllerEintraege_Click(object sender, EventArgs e)
        {
            Int32 AnzEintraege = cLBSuchErgebnis.Items.Count;
        }

        private void rBKlasse_Click(object sender, EventArgs e)
        {
        
        }

        private void rBName_Click(object sender, EventArgs e)
        {
            rBKlasse.Checked = !rBName.Checked;
            SortiereListeAuswahlSchueler();
        }

        private void cLBAuswahlListe_SelectedValueChanged(object sender, EventArgs e)
        {
                List<String> ListeZuLoeschenderEintraege = new List<String> { };

            LoescheEintraegeAuswahlListe(ListeZuLoeschenderEintraege);
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
            }
        }
    }
}
