using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using System.Diagnostics;

namespace ProExcelImportExport.Helper
{
    public class TObjExcelExport
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        FileInfo FileInfo;
        List<List<String>> ListToSave;

        public TObjExcelExport(String ExcelFile, List<List<String>> ListToSave)
        {
            this.FileInfo = new FileInfo(ExcelFile);
            this.ListToSave = ListToSave;
        }

        public void ExportFancy()
        {
            Logger.Debug("Beginne mit Export von Datei " + FileInfo.Name + ". " + ListToSave.Count + " Datensätze werden gespeichert.");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            String[] headerArray = { "JGKZ", "Klasse", "Name", "Vorname", "Geburtsdatum", "Geschlecht", "UsernameID", "InZensos", "InITK", "UsernameITK", "InO365", "UsernameO365" };
            List<String> header = headerArray.ToList();

            List<String> listToAppenAtEnd = new List<string>();
            int modifier = 0;

            int zensosIndex = 7;
            if(IsAllSame(ListToSave, zensosIndex, "True"))
            {
                ListToSave = RemoveColumn(ListToSave, zensosIndex);
                header.RemoveAt(zensosIndex);
                listToAppenAtEnd.Add("Alle Schüler sind in der Zensos Liste");
                modifier++;
                Logger.Debug("Alle Schüler sind in der Zensos Liste daher wurde die Spalte entfernt");
            }
            else if(IsAllSame(ListToSave, zensosIndex, "False"))
            {
                ListToSave = RemoveColumn(ListToSave, zensosIndex);
                header.RemoveAt(zensosIndex);
                listToAppenAtEnd.Add("Kein Schüler ist in der Zensos Liste");
                modifier++;
                Logger.Debug("Kein Schüler ist in der Zensos Liste daher wird die Spalte entfernt");
            }
            else{
                Logger.Debug("Nur einige Schüler sind in der Zensos Liste daher wird die Spalte nicht entfernt. Wert des ersten Schülers: " + ListToSave[0][zensosIndex]);
            }

            int itkIndex = 8 - modifier;
            if (IsAllSame(ListToSave, itkIndex, "True"))
            {
                ListToSave = RemoveColumn(ListToSave, itkIndex);
                ListToSave = RemoveColumn(ListToSave, itkIndex);
                header.RemoveAt(itkIndex);
                header.RemoveAt(itkIndex);
                listToAppenAtEnd.Add("Alle Schüler sind in der ITK");
                modifier = modifier + 2;
                Logger.Debug("Alle Schüler sind in der ITK daher wurde die Spalte entfernt");
            }
            else if (IsAllSame(ListToSave, itkIndex, "False"))
            {
                ListToSave = RemoveColumn(ListToSave, itkIndex);
                ListToSave = RemoveColumn(ListToSave, itkIndex);
                header.RemoveAt(itkIndex);
                header.RemoveAt(itkIndex);
                listToAppenAtEnd.Add("Kein Schüler ist in der ITK");
                modifier = modifier + 2;
                Logger.Debug("Kein Schüler ist in Office 365 Liste daher wird die Spalte entfernt");
            }
            else
            {
                Logger.Debug("Nur einige Schüler sind in dere ITK daher wird die Spalte nicht entfernt. Wert des ersten Schülers: " + ListToSave[0][itkIndex]);
            }

            int officeIndex = 10 - modifier;
            if (IsAllSame(ListToSave, officeIndex, "True"))
            {
                ListToSave = RemoveColumn(ListToSave, officeIndex);
                ListToSave = RemoveColumn(ListToSave, officeIndex);
                header.RemoveAt(officeIndex);
                header.RemoveAt(officeIndex);
                listToAppenAtEnd.Add("Alle Schüler sind in Office 365");
                Logger.Debug("Alle Schüler sind in Office 365 daher wurde die Spalte entfernt " + ListToSave[0].Count);
            }
            else if (IsAllSame(ListToSave, officeIndex, "False"))
            {
                ListToSave = RemoveColumn(ListToSave, officeIndex);
                ListToSave = RemoveColumn(ListToSave, officeIndex);
                header.RemoveAt(officeIndex);
                header.RemoveAt(officeIndex);
                listToAppenAtEnd.Add("Kein Schüler ist in Office 365");
                Logger.Debug("Kein Schüler ist in Office 365 daher wird die Spalte entfernt");
            }
            else
            {
                Logger.Debug("Nur einige Schüler sind in Office 365 daher wird die Spalte nicht entfernt. Wert des ersten Schülers: " + ListToSave[0][officeIndex]);
            }

            ListToSave.Insert(0, header.ToList());

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Export");

                for (int row = 0; row < ListToSave.Count; row++)
                {
                    for (int column = 0; column < ListToSave[row].Count; column++)
                    {
                        try
                        {
                            worksheet.Cells[row + 1, column + 1].Value = ListToSave[row][column];
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }

                using (var range = worksheet.Cells[1, 1, 1, header.Count])
                {
                    range.Style.Font.Bold = true;
                }

                //Autofilter erstellen
                worksheet.Cells[1,1, ListToSave.Count, header.Count].AutoFilter = true;

                worksheet.Cells.AutoFitColumns(0);

                int i = 0;
                foreach (String str in listToAppenAtEnd)
                {
                    worksheet.Cells[ListToSave.Count + i + 4, 1].Value = str;
                    worksheet.Cells[ListToSave.Count + i + 4, 1].Style.Font.Bold = true;
                    i++;
                }

                // Titel 
                worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Schülerliste Export";
                // Seitennummer + Seeitenanzahl in die untere Ecke
                worksheet.HeaderFooter.OddFooter.RightAlignedText = string.Format("Seite {0} von {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                // Namen der Tabelle in die untere Ecke
                worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                // Dateipfad in die untere Ecke
                worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                worksheet.View.PageLayoutView = true;
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                try
                {
                    package.SaveAs(FileInfo);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                sw.Stop();
                Logger.Debug("Die ausgewählten Schüler wurden erfolgreich in der Excel Datei " + FileInfo.Name + " innerhalb von " + sw.ElapsedMilliseconds + "ms gespeichert");
            }
        }

        public static bool IsAllSame(List<List<String>> list, int column, String value)
        {
            foreach(List<String> row in list)
            {
                if (!row[column].Equals(value))
                {
                    return false;
                }
            }

            return true;
        }

         // TODO: Unit tests
        public List<List<String>> RemoveColumn(List<List<String>> list, int column)
        {
            List<List<String>> returnList = list;

            for (int row = 0; row < returnList.Count; row++)
            {
                if(returnList[row].Count > column)
                {
                    returnList[row].RemoveAt(column);
                }
            }

            return returnList;
        }

        public void Export()
        {
            Logger.Debug("Beginne mit Export von Datei " + FileInfo.Name + ". " + ListToSave.Count + " Datensätze werden gespeichert.");

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Export");

                for (int row = 0; row < ListToSave.Count; row++)
                {
                    for (int column = 0; column < ListToSave[row].Count; column++)
                    {
                        try
                        {
                            worksheet.Cells[row + 1, column + 1].Value = ListToSave[row][column];
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }

                try
                {
                    package.SaveAs(FileInfo);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }
    }
}
