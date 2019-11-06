namespace ProExcelImportExport
{
    partial class FoSucheSchueler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btClose = new System.Windows.Forms.Button();
            this.tBEingabeSuchBegriff = new System.Windows.Forms.TextBox();
            this.cLBSuchErgebnis = new System.Windows.Forms.CheckedListBox();
            this.bTAswahlAllerEintraege = new System.Windows.Forms.Button();
            this.cLBAuswahlListe = new System.Windows.Forms.CheckedListBox();
            this.gBSortieren = new System.Windows.Forms.GroupBox();
            this.rBKlasse = new System.Windows.Forms.RadioButton();
            this.rBName = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btExcelExport = new System.Windows.Forms.Button();
            this.gBSortieren.SuspendLayout();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(404, 381);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(294, 37);
            this.btClose.TabIndex = 0;
            this.btClose.Text = "Liste ausgewählter Einträge übernehmen und den Dialog beenden.";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // tBEingabeSuchBegriff
            // 
            this.tBEingabeSuchBegriff.Location = new System.Drawing.Point(21, 49);
            this.tBEingabeSuchBegriff.Name = "tBEingabeSuchBegriff";
            this.tBEingabeSuchBegriff.Size = new System.Drawing.Size(302, 20);
            this.tBEingabeSuchBegriff.TabIndex = 1;
            this.tBEingabeSuchBegriff.TextChanged += new System.EventHandler(this.tBEingabeSuchBegriff_TextChanged);
            // 
            // cLBSuchErgebnis
            // 
            this.cLBSuchErgebnis.CheckOnClick = true;
            this.cLBSuchErgebnis.FormattingEnabled = true;
            this.cLBSuchErgebnis.Location = new System.Drawing.Point(21, 131);
            this.cLBSuchErgebnis.Name = "cLBSuchErgebnis";
            this.cLBSuchErgebnis.Size = new System.Drawing.Size(302, 244);
            this.cLBSuchErgebnis.TabIndex = 3;
            this.cLBSuchErgebnis.SelectedValueChanged += new System.EventHandler(this.cLBSuchErgebnis_SelectedValueChanged);
            // 
            // bTAswahlAllerEintraege
            // 
            this.bTAswahlAllerEintraege.Location = new System.Drawing.Point(21, 381);
            this.bTAswahlAllerEintraege.Name = "bTAswahlAllerEintraege";
            this.bTAswahlAllerEintraege.Size = new System.Drawing.Size(302, 37);
            this.bTAswahlAllerEintraege.TabIndex = 4;
            this.bTAswahlAllerEintraege.Text = "Kein Eintrag vorhanden.";
            this.bTAswahlAllerEintraege.UseVisualStyleBackColor = true;
            this.bTAswahlAllerEintraege.Visible = false;
            this.bTAswahlAllerEintraege.Click += new System.EventHandler(this.bTAswahlAllerEintraege_Click);
            // 
            // cLBAuswahlListe
            // 
            this.cLBAuswahlListe.CheckOnClick = true;
            this.cLBAuswahlListe.FormattingEnabled = true;
            this.cLBAuswahlListe.Location = new System.Drawing.Point(404, 132);
            this.cLBAuswahlListe.Name = "cLBAuswahlListe";
            this.cLBAuswahlListe.Size = new System.Drawing.Size(294, 244);
            this.cLBAuswahlListe.TabIndex = 6;
            this.cLBAuswahlListe.SelectedValueChanged += new System.EventHandler(this.cLBAuswahlListe_SelectedValueChanged);
            // 
            // gBSortieren
            // 
            this.gBSortieren.AutoSize = true;
            this.gBSortieren.Controls.Add(this.rBKlasse);
            this.gBSortieren.Controls.Add(this.rBName);
            this.gBSortieren.Location = new System.Drawing.Point(404, 30);
            this.gBSortieren.Name = "gBSortieren";
            this.gBSortieren.Size = new System.Drawing.Size(290, 59);
            this.gBSortieren.TabIndex = 7;
            this.gBSortieren.TabStop = false;
            this.gBSortieren.Text = "Sortierung der ausgewählten Einträge:";
            // 
            // rBKlasse
            // 
            this.rBKlasse.AutoSize = true;
            this.rBKlasse.Checked = true;
            this.rBKlasse.Location = new System.Drawing.Point(16, 23);
            this.rBKlasse.Name = "rBKlasse";
            this.rBKlasse.Size = new System.Drawing.Size(147, 17);
            this.rBKlasse.TabIndex = 1;
            this.rBKlasse.TabStop = true;
            this.rBKlasse.Text = "nach Klassen und Namen";
            this.rBKlasse.UseVisualStyleBackColor = true;
            this.rBKlasse.CheckedChanged += new System.EventHandler(this.rBKlasse_CheckedChanged);
            this.rBKlasse.Click += new System.EventHandler(this.rBKlasse_Click);
            // 
            // rBName
            // 
            this.rBName.AutoSize = true;
            this.rBName.Location = new System.Drawing.Point(184, 23);
            this.rBName.Name = "rBName";
            this.rBName.Size = new System.Drawing.Size(86, 17);
            this.rBName.TabIndex = 0;
            this.rBName.Text = "nach Namen";
            this.rBName.UseVisualStyleBackColor = true;
            this.rBName.Click += new System.EventHandler(this.rBName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Eingabefeld Suchmaske:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Liste Suchergebnis:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(401, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "klassenübergreifende Liste ausgewählter Einträge:";
            // 
            // btExcelExport
            // 
            this.btExcelExport.Location = new System.Drawing.Point(404, 430);
            this.btExcelExport.Name = "btExcelExport";
            this.btExcelExport.Size = new System.Drawing.Size(294, 32);
            this.btExcelExport.TabIndex = 11;
            this.btExcelExport.Text = "Liste ausgewählter Einträge als Excel-Datei exportieren.";
            this.btExcelExport.UseVisualStyleBackColor = true;
            this.btExcelExport.Click += new System.EventHandler(this.btExcelExport_Click);
            // 
            // FoSucheSchueler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 486);
            this.Controls.Add(this.btExcelExport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gBSortieren);
            this.Controls.Add(this.cLBAuswahlListe);
            this.Controls.Add(this.bTAswahlAllerEintraege);
            this.Controls.Add(this.cLBSuchErgebnis);
            this.Controls.Add(this.tBEingabeSuchBegriff);
            this.Controls.Add(this.btClose);
            this.Name = "FoSucheSchueler";
            this.Text = "FoSucheSchueler";
            this.Load += new System.EventHandler(this.FoSucheSchueler_Load);
            this.gBSortieren.ResumeLayout(false);
            this.gBSortieren.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TextBox tBEingabeSuchBegriff;
        private System.Windows.Forms.CheckedListBox cLBSuchErgebnis;
        private System.Windows.Forms.Button bTAswahlAllerEintraege;
        private System.Windows.Forms.CheckedListBox cLBAuswahlListe;
        private System.Windows.Forms.GroupBox gBSortieren;
        private System.Windows.Forms.RadioButton rBKlasse;
        private System.Windows.Forms.RadioButton rBName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btExcelExport;
    }
}