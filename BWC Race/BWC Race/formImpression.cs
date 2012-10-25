using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using CarlosAg.ExcelXmlWriter;

namespace CWB_Race
{
    public partial class formImpression : Form
    {
        #region declarations

        int indexCategorie = -1;
        int indexPageAImprimer;
        int nbrLignesImprimees;

        List<formMain.c_courreur> v_listeInscrits = new List<formMain.c_courreur>();

        private const int WM_PRINT = 0x0317;
        private const int PRF_CLIENT = 0x00000004;
        private const int PRF_CHILDREN = 0x00000010;
        private const int NBR_LIGNE_MAXI = 36;

        Bitmap bmp = new Bitmap(500, 500);
       // defParamApplic paramApplic = new defParamApplic();
        //tables Table = new tables();
       // TypeImpression typeImpression;

        string categorieSelectionnee;

        #endregion

        #region methodes

        public formImpression(List<formMain.c_courreur> liste)//(TypeImpression typeimpression)
        {
            InitializeComponent();
            {
            /*    ColumnPlace.Width = 95;
                ColumnPlace.HeaderText = "Categorie";
                ColumnPlace.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ColumnTours.Width = 85;
                ColumnTours.HeaderText = "Date Naissance";
                ColumnTours.DefaultCellStyle.Font = new Font(ColumnTours.DefaultCellStyle.Font, FontStyle.Regular);
                ColumnTemps.Visible = false; */
            }
            cLBCategories.Items.Add(" Temps au tour ");
            cLBCategories.Items.Add(" Toutes Categories ");
            foreach (formMain.c_categorie categ in formMain.v_listeCategories)
            {
                cLBCategories.Items.Add(categ.Nom);
            }
            tBClubCourse.Text = formMain.v_Course.Club;
            tBNomCourse.Text = formMain.v_Course.Nom;
            tBLieuCourse.Text = formMain.v_Course.Lieu;
            tBDateCourse.Text = formMain.v_Course.Date.ToShortDateString();
            v_listeInscrits = liste;
            
        }

        public void ExportExcel()
        {
            Workbook excel = new Workbook();
            excel.Worksheets.Clear();

            WorksheetStyle style = excel.Styles.Add("Arial12CenterBold");
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Font.Bold = true;
            style.Font.Size = 12;
            style = excel.Styles.Add("Arial22CenterBold");
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Alignment.Vertical = StyleVerticalAlignment.Center;
            style.Font.Bold = true;
            style.Font.Size = 22;
            style = excel.Styles.Add("Arial22Center");
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Alignment.Vertical = StyleVerticalAlignment.Center;
            style.Font.Size = 22;
            style = excel.Styles.Add("Arial12Center");
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Font.Size = 12;
            style.Borders.Add(StylePosition.Bottom, LineStyleOption.Double);
            style.Borders.Add(StylePosition.Left, LineStyleOption.Double);
            style.Borders.Add(StylePosition.Right, LineStyleOption.Double);
            style.Borders.Add(StylePosition.Top, LineStyleOption.Double);
            style = excel.Styles.Add("Arial12");
            style.Font.Size = 12;
            style.Borders.Add(StylePosition.Bottom, LineStyleOption.Dash);
            style.Borders.Add(StylePosition.Left, LineStyleOption.Dash);
            style.Borders.Add(StylePosition.Right, LineStyleOption.Dash);
            style.Borders.Add(StylePosition.Top, LineStyleOption.Dash);

            int indexPBar = 0;
            formMain.pBExcel.Visible = true;
            Application.DoEvents();
            foreach (string nomCategorie in cLBCategories.Items)
            {
                remplirdGVResultat(nomCategorie, false);
                Worksheet sheet = excel.Worksheets.Add(nomCategorie);
                for (int i = 1; i <= 8; i++) { sheet.Table.Columns.Add(); }
                sheet.Table.DefaultRowHeight = 22;
                //crée entete
                ajoutLigneEnteteExcel(sheet, DateTime.Now.ToLongDateString(), "Arial12CenterBold");
                ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                formMain.c_categorie categorie = new formMain.c_categorie();
                categorie = formMain.v_listeCategories.Find(c => c.Nom == nomCategorie);
                try
                {
                    ajoutLigneEnteteExcel(sheet, string.Format("{0} TOURS", categorie.NbrTours), "Arial12Center");
                    ajoutLigneEnteteExcel(sheet, categorie.Nom, "Arial22CenterBold");
                    ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                    ajoutLigneEnteteExcel(sheet, string.Format("{0} - {1} ANS", categorie.AgeMini, categorie.AgeMaxi), "Arial22Center");
                    ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                }
                catch
                {
                    ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                    ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                    ajoutLigneEnteteExcel(sheet, "TOUS COURREURS", "Arial22CenterBold");
                    ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                    ajoutLigneEnteteExcel(sheet, "", "Arial22Center");
                }
                //ajoute les noms de colonne
                sheet.Table.Rows.Add();
                foreach (DataGridViewColumn colonne in dGVResultat.Columns)
                {
                    sheet.Table.Rows[sheet.Table.Rows.Count - 1].Cells.Add(colonne.HeaderText.TrimStart(), DataType.String, "Arial12Center");
                }
                //exporte les resultats
                foreach (DataGridViewRow dGRow in dGVResultat.Rows)
                {
                    sheet.Table.Rows.Add();
                    foreach (DataGridViewCell dGVCell in dGRow.Cells)
                    {
                        try
                        {
                            int.Parse(dGVCell.Value.ToString());
                            sheet.Table.Rows[sheet.Table.Rows.Count - 1].Cells.Add(dGVCell.Value.ToString(), DataType.Number, "Arial12");
                        }
                        catch
                        {
                            try { sheet.Table.Rows[sheet.Table.Rows.Count - 1].Cells.Add(dGVCell.Value.ToString(), DataType.String, "Arial12"); }
                            catch { }
                        }
                    }                        
                }
               // Array largeursColonne = new arr
                int colonneIndex = 0;
                foreach (int width in new int[] { 35, 180, 150, 60, 70, 150, 35 })
                {
                    sheet.Table.Columns[colonneIndex].Width = width;
                    colonneIndex++;
                }
                indexPBar++;
                formMain.m_MAJProgressBar(indexPBar);
                Application.DoEvents();
            }
            SaveFileDialog sFD = new SaveFileDialog();
            sFD.Filter = "Fichier Excel *.xls|*.xls";
            sFD.OverwritePrompt = true;
            if (sFD.ShowDialog() == DialogResult.OK)
            {
                excel.Save(sFD.FileName);
                MessageBox.Show("Export du fichier Excel terminé", "CourseVelo >> Excel", MessageBoxButtons.OK);
            }
            formMain.pBExcel.Visible = false;
        }

        private static void ajoutLigneEnteteExcel(Worksheet sheet, string text, string style)
        {
            sheet.Table.Rows.Add();
            sheet.Table.Rows[sheet.Table.Rows.Count - 1].Cells.Add(text, DataType.String, style);
            sheet.Table.Rows[sheet.Table.Rows.Count - 1].Cells[0].MergeAcross = 7;
        }


        private void remplirdGVResultat(string nomCategorie, bool pourImprimer)
        {
            #region Temps au tour
            if (nomCategorie == " Temps au tour ")
            {
                lbTempsAuTour.Items.Clear();
                List<string> liste = new List<string>();
                foreach (formMain.c_courreur courreur in formMain.v_listeCourreurs)
                {
                    string plaque1 = string.Format("{0:000}", courreur.Plaque);
                    if (plaque1 != "000")
                    {
                        string plaque2 = string.Format("{0:000}", courreur.PLaqueBis);
                        StringBuilder result = new StringBuilder(string.Format(" {0,-20}... {1,-15} ", courreur.Nom, courreur.Prenom));
                        int timing = 0;
                        foreach (string temps in formMain.v_tempsAuTour)
                        {
                            if (temps.StartsWith(plaque1) | temps.StartsWith(plaque2))
                            {
                                try
                                {
                                    int tempsEnSecondes = int.Parse(temps.Substring(10, 2)) * 3600 + int.Parse(temps.Substring(13, 2)) * 60
                                                         + int.Parse(temps.Substring(16, 2));
                                    TimeSpan tS = new TimeSpan((long)(tempsEnSecondes - timing) * 10000000);
                                    result.Append(string.Format("{0:00}:{1:00}.{2:00} ", tS.Hours, tS.Minutes, tS.Seconds));
                                    timing = tempsEnSecondes;
                                }
                                catch { }
                            }
                        }
                        liste.Add(result.ToString());
                        liste.Sort();
                    }
                }
                foreach (string line in liste)
                {
                    string ligne = line.Clone().ToString();
                    if (ligne.Length > 90)
                    {
                        while (ligne.Length > 90)
                        {

                            lbTempsAuTour.Items.Add(ligne.Substring(0, 90));
                            ligne = ligne.Remove(0, 90);
                        }
                        lbTempsAuTour.Items.Add(ligne);
                    }
                    else
                    { lbTempsAuTour.Items.Add(ligne); }
                }
                lbTempsAuTour.Visible = true;
                lbTempsAuTour.BringToFront();
                return;
            }
            #endregion
            #region Classement
            lbTempsAuTour.Visible = false;
            string[] elements;
            List<string> classement = new List<string>();
            lBClassement.Items.Clear();
            dGVResultat.Rows.Clear();
            foreach (formMain.c_courreur courreur in v_listeInscrits)
            {
                lBClassement.Items.Add(string.Format("{0:000},{1:00000},{2:000}", 999 - courreur.nbrTours, courreur.TempsEnSecondes, courreur.numeroInterne));
            }
            if (nomCategorie == " Toutes Categories ")
            { tBCategorieCourse.Text = nomCategorie; nomCategorie = ""; }
            //differencier garçon et fille
            m_trierResultats(nomCategorie, classement, true);
            classement.Add("");
            classement.Add("");
            classement.Add("");
            m_trierResultats(nomCategorie, classement, false);

            dGVResultat.Rows.Clear();
            // foreach (string ligne in classement)
            for (int i = nbrLignesImprimees + 1; i <= classement.Count; i++)
            {
                string temp = (string)classement[i - 1];
                elements = temp.Split(char.Parse("!"));
                dGVResultat.Rows.Add(elements);
                nbrLignesImprimees++;
                if ((nbrLignesImprimees % NBR_LIGNE_MAXI == 0) && (pourImprimer) && (nbrLignesImprimees < classement.Count))
                {
                    indexPageAImprimer--;
                    break;
                }
            }
            if (nomCategorie != "") { tBCategorieCourse.Text = nomCategorie; }
            cLBCategories.SelectedIndex = indexCategorie;
            if ((!pourImprimer) || ((pourImprimer) && (nbrLignesImprimees % NBR_LIGNE_MAXI) != 0)) { nbrLignesImprimees = 0; }
            #endregion
        }

        private void m_trierResultats(string nomCategorie, List<string> classement, bool sexe)
        {
            int place = 1;
            foreach (string line in lBClassement.Items)
            {
                int numeroInterne = int.Parse(line.ToString().Substring(10, 3));
                formMain.c_courreur c = formMain.v_listeCourreurs.Find(x => x.numeroInterne == numeroInterne);
                if (formMain.v_listeCategories.Find(categ => categ.numeroInterne == c.CategorieNumInterne).Nom.Contains(nomCategorie))
                {
                    if (c.Sexe == sexe)
                    {
                        string lV = place.ToString() + "!";
                        lV += (c.Nom) + "!";
                        lV += (c.Prenom) + "!";
                        lV += (c.Plaque.ToString()) + " / " + c.PLaqueBis.ToString() + "!";
                        lV += c.NumeroLicence + "!";
                        lV += c.Club + "!";
                        //lV+=(c.Categorie);
                        lV += (c.nbrTours.ToString()) + "!";
                        lV += (string.Format("{0:00}:{1:00}.{2:00}", c.TempsEnSecondes / 3600, (c.TempsEnSecondes / 60) % 60, c.TempsEnSecondes % 60));
                        place++;
                        classement.Add(lV);
                    }
                }
            }
        }

        public void PreparerForm()
        {
            //remplir cLBCategories
   /*         Table.MAJPlaqueCourreurDansTableCourreur(paramApplic.CourseID);
            Table.MAJListe(NomTables.Courreurs);
            Table.MAJListe(NomTables.Categories);
            foreach (string cA in Table.SortedCategories().Values)
            {
                cLBCategories.Items.Add(cA);
            }
            cLBCategories.Items.Add("Tous");
            Table.CoursesBindingSource.Position = Table.CoursesBindingSource.Find("CourseID", paramApplic.CourseID.ToString()); */
        }

        private void apercu()
        {
            if (cLBCategories.CheckedItems.Count == 0)
            {
                MessageBox.Show("Pas de catgégorie selectionnée");
                return;
            }
           // this.Height = 1200;
            gBPage.Height = 1020;
            dGVResultat.Height = 870;
            indexPageAImprimer = 0;
            PrintDocument pdc = new PrintDocument();
            pdc.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            if (cBPrevisualisation.Checked)
            {
                PrintPreviewDialog pPDialog = new PrintPreviewDialog();
                pPDialog.Document = pdc;
                pPDialog.ShowDialog();
            }
            PrintDialog pD = new PrintDialog();
            pD.Document = pdc;
            pD.PrinterSettings.Copies = short.Parse(dUDNbrExemplaires.Text);
            if (pD.ShowDialog() == DialogResult.OK)
            {
                pdc.Print();
            }
            nbrLignesImprimees = 0;
            gBPage.Height = 570;
            dGVResultat.Height = 430;
         //   this.Height = 727;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            remplirdGVResultat(cLBCategories.CheckedItems[indexPageAImprimer].ToString(), true);
            Application.DoEvents();
            ev.Graphics.DrawImage(PrintWindowEx(), 40, 40);
            ev.HasMorePages = (indexPageAImprimer < cLBCategories.CheckedItems.Count - 1);
            if (!ev.HasMorePages)
            { indexPageAImprimer = 0; }
            else
            { indexPageAImprimer++; }
        }

        private Bitmap PrintWindowEx()
        {
            Bitmap bmp = null;
            Graphics gr = null;
            IntPtr hdc = IntPtr.Zero;
            try
            {
                bmp = new Bitmap(this.gBPage.Width, this.gBPage.Height, this.CreateGraphics());
                gr = Graphics.FromImage(bmp);
                hdc = gr.GetHdc();
                IntPtr wParam = hdc;
                IntPtr lParam = new IntPtr(PRF_CLIENT | PRF_CHILDREN);
                Message msg = Message.Create(gBPage.Handle, WM_PRINT, wParam, lParam);
                this.WndProc(ref msg);
            }
            catch { }
            finally
            {
                if (gr != null)
                {
                    if (hdc != IntPtr.Zero)
                        gr.ReleaseHdc(hdc);
                    gr.Dispose();
                }
            }
            return bmp;
        } 

        #endregion

        #region evenements

        private void formImpression_Load(object sender, EventArgs e)
        {
            PreparerForm();
        }

        private void bTLancerImpression_Click(object sender, EventArgs e)
        {
            apercu();
        }

        private void dTPDateCourse_ValueChanged(object sender, EventArgs e)
        {
            tBDateCourse.Text = dTPDateCourse.Text;
        }

        private void bTArriere_Click(object sender, EventArgs e)
        {
            if (indexCategorie > 0)
            {
                indexCategorie--;
                categorieSelectionnee = cLBCategories.Items[indexCategorie].ToString();
                remplirdGVResultat(categorieSelectionnee, false);
            }
        }

        private void bTAvant_Click(object sender, EventArgs e)
        {
            if (indexCategorie < (cLBCategories.Items.Count - 1))
            {
                indexCategorie++;
                categorieSelectionnee = cLBCategories.Items[indexCategorie].ToString();
                remplirdGVResultat(categorieSelectionnee, false);
            }
        }

        private void cBToutSelectionner_CheckedChanged(object sender, EventArgs e)
        {
            for (int index = 0; index < cLBCategories.Items.Count; index++)
            {
                cLBCategories.SetItemChecked(index, cBToutSelectionner.Checked);
            }
        }

        #endregion
    }
}