using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;
using NLog;
using System.Diagnostics;

namespace ProExcelImportExport.Helper
{
    public class TObjExcelImport
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public List<List<String>> EintragsListe = new List<List<string>>();
        public readonly String ExcelDatei;

        public TObjExcelImport(String ExcelFile)
        {
            FileInfo existingFile = new FileInfo(ExcelFile);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Erstes Worksheet auswählen
                int totalRows = worksheet.Dimension.Rows; // Anzahl der Zeilen
                int totalColumns = worksheet.Dimension.Columns; // Anzahl der Spalten

                Logger.Debug("Beginne einleseung der Excel Datei " + existingFile.Name + ". Sie hat " + totalRows + " Zeilen und " + totalColumns + " Spalten"); // Debug Info
                Logger.Debug("Beginne mit dem einlesen...");

                Stopwatch sw = new Stopwatch();
                sw.Start();

                for (int row = 1; row < totalRows; row++)
                {
                    List<String> currentRowEntries = new List<string>();
                    for(int column = 1; column < totalColumns; column++)
                    {
                        Object val = worksheet.Cells[row, column].Value;
                        if (val == null) {
                            currentRowEntries.Add("n.n.");
                        } else
                        {
                            currentRowEntries.Add(worksheet.Cells[row, column].Value.ToString());
                        }
                    }
                    EintragsListe.Add(currentRowEntries);
                }

                sw.Stop();
                Logger.Debug("Einlesen der Datei " + existingFile.Name + " in " + sw.ElapsedMilliseconds + "ms abgeschlossen");
                Logger.Debug("Zelle A1: " + EintragsListe[0][0]);

                Logger.Debug("Beginne Datumkonvertierung");
                sw.Reset();
                sw.Start();

                if (EintragsListe.Count > 0) // Wissen sie eigentlich wie lange das hier dauert? fast 1.5s. Das ist sowas von uneffizient
                {
                    int DatumPos = 3; // Hier jetzt mal Hardcoded, ich weiß, ist nicht so schön aber jetzt erstmal egal
                    for(int i = 1; i < EintragsListe.Count; i++)
                    //foreach (List<String> SEintrag in EintragsListe)
                    {
                        try
                        {
                            // Das hier hab ich mal für sie behoben. Ihr Code hat nicht funktioniert und hat fast 1.5s gedauert...
                            EintragsListe[i][DatumPos] = DateTime.Parse(EintragsListe[i][DatumPos]).ToShortDateString();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                        
                }

                sw.Stop();
                Logger.Debug("Datumskonvertierung für Datei in " + sw.ElapsedMilliseconds + "ms abgeschlossen");
            }
        }
    }
}
