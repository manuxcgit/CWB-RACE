using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace CWB_Race
{
    public partial class formMain : Form
    {

        #region declarations
        public enum Federations { Ufolep, FSGT, FFC }
        #region class
        public class c_course
        {
            public string Nom;
            public string Club;
            public string Lieu;
            public DateTime Date;
            //public List<int> v_listeInscrits = new List<int>();
            public string listeInscritsEnString;
            public bool DoublerPlaque;

            public c_course() { }

            public c_course(c_course course)
            {
                Nom = course.Nom; Club = course.Club; Lieu = course.Lieu; Date = course.Date;// v_listeInscrits = course.v_listeInscrits; 
                listeInscritsEnString = course.listeInscritsEnString; DoublerPlaque = course.DoublerPlaque;
            }
        }
        public class c_categorie
        {
            public string Nom;
            public int DecalageDepartEnSecondes;
            public int NbrTours;
            public int AgeMini;
            public int AgeMaxi;
            public int numeroInterne;
            public static int dernierNumInterne;

            public c_categorie() { }

            public c_categorie(c_categorie categ)
            {
                Nom = categ.Nom; DecalageDepartEnSecondes = categ.DecalageDepartEnSecondes; NbrTours = categ.NbrTours; AgeMini = categ.AgeMini;
                AgeMaxi = categ.AgeMaxi; numeroInterne = categ.numeroInterne;
            }
        }
        public class c_courreur
        {
            public string Nom;
            public string Prenom;
            public DateTime DateNaissance;
            public Boolean Sexe;
            public int Plaque;
            public int PLaqueBis;
            public string Club;
            public string Departement;
            public int CategorieNumInterne;
            public string Federation;
            public string NumeroLicence;
            public int numeroInterne;
            public int nbrTours;
            public int TempsEnSecondes;
            public static int dernierNumInterne;
            public int PlaceGenerale = 0; //pour voir evolution classement pendant course
            public int PlaceCategorie = 0;
            public int ClassementGeneral = 0; // -1,0,ou 1 pour memoriser si monte ou descend au classement
            public int ClassementCategorie = 0;

            public c_courreur() { }

            public c_courreur(c_courreur item)
            {
                Nom = item.Nom; Prenom = item.Prenom; DateNaissance = item.DateNaissance; Sexe = item.Sexe; Plaque = item.Plaque; PLaqueBis = item.PLaqueBis; numeroInterne = item.numeroInterne;
                Club = item.Club; Departement = item.Departement; CategorieNumInterne = item.CategorieNumInterne; Federation = item.Federation; NumeroLicence = item.NumeroLicence;
            }

        }
        #endregion
        #region Variables
        public static List<c_categorie> v_listeCategories = new List<c_categorie>();
        public static List<c_courreur> v_listeCourreurs = new List<c_courreur>();
        List<int> v_listeInscrits = new List<int>();
        public static c_course v_Course = new c_course();

        string v_filtreCourreur = "";
        DateTime v_depart;
        int v_delaiSecuriteEnSecondes = 0;
        c_courreur dernierCourreurSaisi = new c_courreur();

        public static List<string> v_tempsAuTour = new List<string>();

        #endregion
        #endregion

        public formMain()
        {
            InitializeComponent();
            m_chargerParametres();
            tBPlaqueBis.Visible = false;
        }

        #region  EVENTS

        private void e_cmdDemarrer_Click(object sender, EventArgs e)
        {
            if (cmdDemarrer.Text == "Demarrer")
            {
                v_depart = DateTime.Now;
                timerCourse.Enabled = true;
                cmdDemarrer.Text = "Arreter";
                cmdImprimer.Enabled = false;
                cmdExportExcel.Enabled = false;
                //RAZ tours pilotes
                lBPassage.Items.Clear();
                foreach (c_courreur courreur in v_listeCourreurs)
                {
                    courreur.nbrTours = 0;
                    courreur.TempsEnSecondes = 0;
                }
                m_MAJAffichageCourse(dernierCourreurSaisi, false);
            }
            else
            {
                if (MessageBox.Show("Voulez vous arreter ?", "EN COURSE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return; }
                timerCourse.Enabled = false;
                cmdDemarrer.Text = "Demarrer";
                cmdImprimer.Enabled = true;
                cmdExportExcel.Enabled = true;
            }
        }

        private void e_cmdImporterCourse_Click(object sender, EventArgs e)
        {
            m_importerCourse();
        }

        private void e_cmdImporterResultat_Click(object sender, EventArgs e)
        {
            m_importerResultat();
        }

        private void e_cmdImprimer_Click(object sender, EventArgs e)
        {
            List<c_courreur> listeInscrits = (from string Plaque in v_Course.listeInscritsEnString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                                              select (v_listeCourreurs.Find(c => c.numeroInterne == int.Parse(Plaque.Substring(0, 3))))).ToList();
            formImpression fI = new formImpression(listeInscrits);
            v_tempsAuTour.Clear();
            v_tempsAuTour = (from string temps in lBPassage.Items select temps).ToList();
            fI.ShowDialog();
        }

        private void e_cmdInscrire_Click(object sender, EventArgs e)
        {
            m_inscrireCourreur(lBCourreurs.Items[lBCourreurs.SelectedIndex].ToString(), tBNumeroCourreurAInscrire.Text, tBNumeroAInscrireBis.Text);
            tbFiltreNom.Text = "";
            tbFiltreNom.Focus();
        }

        private void e_cmdSauvegarderCategorie_Click(object sender, EventArgs e)
        {
            c_categorie newcateg = new c_categorie();
            try
            {
                newcateg.Nom = tBNomCategorie.Text;
                newcateg.DecalageDepartEnSecondes = int.Parse(tBDecalage.Text);
                newcateg.AgeMaxi = int.Parse(tBAgeMaxi.Text);
                newcateg.AgeMini = int.Parse(tBAgeMini.Text);
                newcateg.NbrTours = int.Parse(tBNbrTours.Text);
                if (tBNumeroInterneCategorie.Text == "")
                { newcateg.numeroInterne = c_categorie.dernierNumInterne + 1; c_categorie.dernierNumInterne++; }
                else
                { newcateg.numeroInterne = int.Parse(tBNumeroInterneCategorie.Text); }
                int index = v_listeCategories.FindIndex(c => c.numeroInterne == newcateg.numeroInterne);
                if (index > -1) { v_listeCategories.RemoveAt(index); }
                v_listeCategories.Add(newcateg);
                c_Ini ini = new c_Ini("Categories.ini");
                ini.m_Write(newcateg, newcateg.numeroInterne.ToString());
                tBNomCategorie.Text = "";
                tBDecalage.Text = "";
                tBAgeMaxi.Text = "";
                tBAgeMini.Text = "";
                tBNbrTours.Text = "";
                tBNumeroInterneCategorie.Text = "";
                m_MAJListeCategories();
                m_MAJLVCourreursInscrits();
                m_MAJListeCourreurs("");
            }
            catch { MessageBox.Show("Verifiez les valeurs !"); }
        }

        private void e_cmdSauvegarderCourreur_Click(object sender, EventArgs e)
        {
            try
            {
                if ((tBNomCourreur.Text == "") || (tBPrenomCourreur.Text == "")) { return; }
                c_courreur v_courreur = new c_courreur();
                v_courreur.Nom = tBNomCourreur.Text;
                v_courreur.Prenom = tBPrenomCourreur.Text;
                v_courreur.CategorieNumInterne = v_listeCategories.Find(c => c.Nom == cBCategorieCourreur.Text).numeroInterne;
                v_courreur.Club = tBClubCourreur.Text;
                v_courreur.DateNaissance = dTPCourreur.Value;
                v_courreur.Departement = cBDepartement.Text;
                v_courreur.Federation = cBFederations.Text;
                v_courreur.NumeroLicence = tBNumeroLicence.Text;
                v_courreur.Sexe = !rBFeminin.Checked;
                try
                {
                    v_courreur.Plaque = int.Parse(tBPlaque.Text);
                    if (tBPlaqueBis.Visible) { v_courreur.PLaqueBis = int.Parse(tBPlaqueBis.Text); }
                }
                catch { }

                //cherche si existe deja
                int index = v_listeCourreurs.FindIndex(courreur => (courreur.Nom == v_courreur.Nom) & (courreur.Prenom == v_courreur.Prenom));
                if ((index > -1) && (tBNumeroInterneCourreur.Text == ""))
                {
                    if (MessageBox.Show("Ce courreur existe deja, voulez vous le remplacer ?", "DOUBLON", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    { return; }
                }
                if (tBNumeroInterneCourreur.Text == "")
                {
                    v_courreur.numeroInterne = c_courreur.dernierNumInterne + 1;
                    c_courreur.dernierNumInterne++;
                }
                else
                {
                    v_courreur.numeroInterne = int.Parse(tBNumeroInterneCourreur.Text);
                    v_listeCourreurs.RemoveAt(index);
                }
                v_listeCourreurs.Add(v_courreur);
                c_Ini v_ini = new c_Ini("Courreurs.ini");
                v_ini.m_Write(v_courreur, v_courreur.numeroInterne.ToString());

                //regarde si pilote à inscrire dans course
                bool v_reussi = true;
                if (cBInscrireDansCourse.Checked)
                {
                    //verifie si deja dans liste
                    if (!v_listeInscrits.Contains(v_courreur.numeroInterne))
                    {
                        v_reussi = m_inscrireCourreur(v_courreur.Nom + " ... " + v_courreur.Prenom, tBPlaque.Text, tBPlaqueBis.Text);
                    }
                }
                else
                { lBCourreurs.Items.Add(v_courreur.Nom + " ... " + v_courreur.Prenom); }

                if (v_reussi)
                {
                    tBNomCourreur.Text = "";
                    tBPrenomCourreur.Text = "";
                    tBNumeroInterneCourreur.Text = "";
                    m_MAJListeCourreurs("");
                    m_sauvegarderCourse();
                    tBPlaque.Text = "";
                    tBPlaqueBis.Text = "";
                    tBNomCourreur.Focus();
                }
            }
            catch { MessageBox.Show("Probleme de sauvegarde"); }
        }

        private void e_cmdSauvegarderCourse_Click(object sender, EventArgs e)
        {
            v_Course.Nom = tBNomCourse.Text;
            v_Course.Club = tBClub.Text;
            v_Course.Lieu = tBLieu.Text;
            v_Course.Date = dTPCourse.Value;
            v_Course.DoublerPlaque = cBDoublerPlaque.Checked;
            m_sauvegarderCourse();
        }

        private void e_dTPCourreur_Leave(object sender, EventArgs e)
        {
            TimeSpan ecart = new DateTime(2011, 12, 31) - dTPCourreur.Value;
            int annees = ecart.Days / 365;
            foreach (c_categorie categ in v_listeCategories)
            {
                if ((annees >= categ.AgeMini) & (annees <= categ.AgeMaxi))
                {
                    cBCategorieCourreur.Text = categ.Nom;
                    break;
                }
            }
        }

        private void e_lBCourreurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // m_inscrireCourreur();
        }

        private void e_lBCoursesExistantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string v_resultat = lBCoursesExistantes.Items[lBCoursesExistantes.SelectedIndex].ToString();
                if (v_resultat.StartsWith("passage")) { m_ImporterResultatSelectionne(v_resultat); }
            }
            catch { }
        }

        private void e_lVCategories_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_trierColonneLV(sender, e);
        }

        private void e_lVCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = v_listeCategories.FindIndex(c => c.Nom = lVCategories.SelectedItems[0].SubItems[1].Text);
            //if (index < 0) { return; }
            try
            {
                c_categorie categ = new c_categorie(v_listeCategories.Find(c => c.Nom == lVCategories.SelectedItems[0].SubItems[0].Text));
                tBNomCategorie.Text = categ.Nom;
                tBDecalage.Text = categ.DecalageDepartEnSecondes.ToString();
                tBAgeMaxi.Text = categ.AgeMaxi.ToString();
                tBAgeMini.Text = categ.AgeMini.ToString();
                tBNbrTours.Text = categ.NbrTours.ToString();
                tBNumeroInterneCategorie.Text = categ.numeroInterne.ToString();
            }
            catch { }
        }

        private void e_lVCourreurs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_trierColonneLV(sender, e);
        }

        private void e_lVCourreurs_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            { e.NewWidth = 0; e.Cancel = true; }
        }

        private void e_lVCourreurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   if (tBNomCourreur.Text != "")
               {
                   MessageBox.Show("Enregistrez ce courreur ou supprimez le nom");
                   return;
               } */
            try
            {
                int indexCourreur = v_listeCourreurs.FindIndex(c => (c.Nom == lVCourreurs.SelectedItems[0].SubItems[1].Text) & (c.Prenom == lVCourreurs.SelectedItems[0].SubItems[2].Text));
                c_courreur courreur = new c_courreur(v_listeCourreurs[indexCourreur]);
                tBNomCourreur.Text = courreur.Nom;
                tBPrenomCourreur.Text = courreur.Prenom;
                tBNumeroLicence.Text = courreur.NumeroLicence;
                tBNumeroInterneCourreur.Text = courreur.numeroInterne.ToString();
                tBPlaque.Text = courreur.Plaque.ToString();
                tBPlaqueBis.Text = courreur.PLaqueBis.ToString();
                cBCategorieCourreur.Text = v_listeCategories.Find(c => c.numeroInterne == courreur.CategorieNumInterne).Nom;
                dTPCourreur.Value = courreur.DateNaissance;
                cBDepartement.Text = courreur.Departement;
                cBFederations.Text = courreur.Federation;
                tBClubCourreur.Text = courreur.Club;
                rBMasculin.Checked = courreur.Sexe;
                rBFeminin.Checked = !rBMasculin.Checked;
            }
            catch { }
        }

        private void e_lVCourreursInscrits_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_trierColonneLV(sender, e);
        }

        private void e_lvCourse_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_trierColonneLV(sender, e);
        }

        private void e_mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerCourse.Enabled) { e.Cancel = true; return; }
            e.Cancel = (MessageBox.Show("Voulez vous quitter ?", "ARRET APPLICATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No);
        }

        private void e_tBDelaiSecurite_TextChanged(object sender, EventArgs e)
        {
            try
            {
                v_delaiSecuriteEnSecondes = int.Parse(tBDelaiSecurite.Text) * 60;
            }
            catch { }

        }

        private void e_tBNomCourreur_Leave(object sender, EventArgs e)
        {
            tBNomCourreur.Text = tBNomCourreur.Text.ToUpper();
        }

        private void e_tBPrenomCourreur_Leave(object sender, EventArgs e)
        {
            try
            {
                string prenom = tBPrenomCourreur.Text;
                prenom = prenom.Substring(0, 1).ToUpper() + prenom.Substring(1, prenom.Length - 1).ToLower();
                tBPrenomCourreur.Text = prenom;
            }
            catch { }
        }

        private void e_tBSaisieCourreur_TextChanged(object sender, EventArgs e)
        {
            if (!timerCourse.Enabled) { tBSaisieCourreur.Text = ""; return; }
            try
            {
                if (tBSaisieCourreur.Text.Length == 3)
                {
                    int Plaque = int.Parse(tBSaisieCourreur.Text);
                    if (Plaque == 0) { return; }
                    c_courreur courreur = v_listeCourreurs.Find(c => (c.Plaque == Plaque) || (c.PLaqueBis == Plaque));
                    c_categorie categorie = v_listeCategories.Find(ca => ca.numeroInterne == courreur.CategorieNumInterne);
                    TimeSpan tS = DateTime.Now - v_depart;
                    int chrono = (int)tS.TotalSeconds;
                    chrono -= categorie.DecalageDepartEnSecondes;
                    //controle delai securite
                    if ((chrono - courreur.TempsEnSecondes) > v_delaiSecuriteEnSecondes)
                    {
                        int nbrToursCateg = 99;
                        try
                        {
                            nbrToursCateg = v_listeCategories.Find(c => c.numeroInterne == courreur.CategorieNumInterne).NbrTours;
                        }
                        catch { }
                        if (courreur.nbrTours < nbrToursCateg)
                        { courreur.nbrTours++; }
                        courreur.TempsEnSecondes = chrono;
                        lBPassage.Items.Add(string.Format("{0:000}  ...  {1:00}:{2:00}.{3:00}", Plaque, tS.Hours, tS.Minutes, tS.Seconds));
                        m_MAJAffichageCourse(courreur, true);
                        tBSaisieCourreur.Text = "";
                        lDernierCourreur.Text = "Dernier courreur saisi : " + courreur.Nom + " " + courreur.Prenom + " ... " + categorie.Nom;
                        dernierCourreurSaisi = courreur;
                    }
                    else
                    {
                        lBPassage.Items.Add(string.Format("{0:000} ... trop rapide", Plaque));
                        tBSaisieCourreur.Text = "";
                    }
                }
            }
            catch
            {
                TimeSpan tS = DateTime.Now - v_depart;
                lBPassage.Items.Add(tBSaisieCourreur.Text + string.Format("  ***  {0:00}:{1:00}.{2:00}", tS.Hours, tS.Minutes, tS.Seconds));
                tBSaisieCourreur.Text = "";
            }
            lBPassage.SelectedIndex = lBPassage.Items.Count - 1;
            lBPassage.TopIndex = lBPassage.SelectedIndex;
        }

        private void e_tCCategorieCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectionne les courreurs à afficher
            if (tCCategorieCourse.SelectedIndex == 0)
            { v_filtreCourreur = ""; }
            else
            { v_filtreCourreur = tCCategorieCourse.SelectedTab.Text; }
            m_MAJAffichageCourse(dernierCourreurSaisi, false);
        }

        private void e_timerCourse_Tick(object sender, EventArgs e)
        {
            tBSaisieCourreur.Focus();
            TimeSpan ecoule = DateTime.Now - v_depart;
            tBChrono.Text = string.Format("{0:00}:{1:00}.{2:00}", ecoule.Hours, ecoule.Minutes, ecoule.Seconds);
            string[] ls = (from string l in lBPassage.Items select l).ToArray();
            string v_time = (new FileInfo(Application.ExecutablePath).DirectoryName + "\\passage " + v_depart.ToShortDateString().Replace("/", " ") + " " + v_depart.ToShortTimeString().Replace(':', ' ') + ".txt");
            File.WriteAllLines(v_time, ls);
        }

        private void e_TSMI_modifierCourreur_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = lVCourreursInscrits.SelectedItems[0].SubItems[0].Text;
                string premnom = lVCourreursInscrits.SelectedItems[0].SubItems[1].Text;
                int index = 0;
                foreach (ListViewItem lVI in lVCourreurs.Items)
                {
                    if ((lVI.SubItems[1].Text == nom) && (lVI.SubItems[2].Text == premnom))
                    {
                        lVCourreurs.Items[index].Selected = true;
                        break;
                    }
                    else { index++; }
                }
                if (index < lVCourreurs.Items.Count) { tCMain.SelectedIndex = 2; }
            }
            catch { }
        }

        private void e_TSMI_SupprimerCourreur_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = lVCourreursInscrits.SelectedItems[0].SubItems[0].Text;
                string prenom = lVCourreursInscrits.SelectedItems[0].SubItems[1].Text;
                if (MessageBox.Show("Voulez vous enlever " + prenom + " " + nom + " de cette course ?", "Modifier liste", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return; }
                string Plaque = (lVCourreursInscrits.SelectedItems[0].SubItems[3].Text.Split('/')[0].Trim());
                string[] paires = v_Course.listeInscritsEnString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                v_Course.listeInscritsEnString = "";
                foreach (string paire in paires)
                {
                    if (!paire.Contains(">" + Plaque)) { v_Course.listeInscritsEnString += paire + "|"; }
                }
                c_courreur courreur = v_listeCourreurs.Find(c => c.Plaque == int.Parse(Plaque));
                courreur.Plaque = 0;
                courreur.PLaqueBis = 0;
                int index = v_listeInscrits.FindIndex(c1 => c1 == courreur.numeroInterne);
                v_listeInscrits.RemoveAt(index);
                m_sauvegarderCourse();
                m_MAJLVCourreursInscrits();
                //lBCourreurs.Items.Add(nom + " ... " + prenom);
            }
            catch { }
        }
        #endregion

        #region METHODES
        void m_chargerParametres()
        {
            try
            {
                //charger categories et courreurs
                cBFederations.Items.Clear();
                foreach (Federations v_fede in Enum.GetValues(typeof(Federations)))
                {
                    cBFederations.Items.Add(v_fede.ToString());
                }
                c_Ini ini = new c_Ini("Categories.ini");
                int index = 1;
                c_categorie categ = new c_categorie();
                ini.m_Read(categ, index.ToString());
                while (index <= c_categorie.dernierNumInterne)
                {
                    if (categ.Nom != "")
                    {
                        v_listeCategories.Add(new c_categorie(categ));
                    }
                    index++;
                    ini.m_Read(categ, index.ToString());
                }
                ini = new c_Ini("Courreurs.ini");
                index = 1;
                c_courreur courreur = new c_courreur();
                ini.m_Read(courreur, index.ToString());
                while (index <= c_courreur.dernierNumInterne)
                {
                    courreur.Plaque = 0;
                    courreur.PLaqueBis = 0;
                    v_listeCourreurs.Add(new c_courreur(courreur));
                    index++;
                    ini.m_Read(courreur, index.ToString());
                }
                m_MAJListeCourreurs("");
                m_MAJListeCategories();
                //cherche courses existantes
                string[] courses = (from c
                                   in File.ReadAllLines(new FileInfo(Application.ExecutablePath).DirectoryName + "\\courses.ini")
                                    where c.StartsWith("[")
                                    select c.Replace("[", "").Replace("]", "")).ToArray();
                lBCoursesExistantes.Items.AddRange(courses);
            }
            catch { }
        }

        void m_importerCourse()
        {
            try
            {
                string nom_course = lBCoursesExistantes.Items[lBCoursesExistantes.SelectedIndex].ToString();
                if (nom_course == "") { MessageBox.Show("Saisissez le nom de la course"); return; }
                c_Ini v_ini = new c_Ini("Courses.ini");
                c_course course = new c_course();
                v_ini.m_Read(course, nom_course);
                if (course.Nom != "") { v_Course = new c_course(course); }
                v_listeInscrits = new List<int>();
                foreach (string couple in v_Course.listeInscritsEnString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))// ('|',StringSplitOptions.RemoveEmptyEntries))
                {
                    int numInterne = int.Parse(couple.Substring(0, 3));
                    int Plaque = int.Parse(couple.Substring(4, 3));
                    int PlaqueBis = 0;
                    if (couple.Length > 7) { PlaqueBis = int.Parse(couple.Substring(7, 3)); }
                    v_listeCourreurs.Find(c => c.numeroInterne == numInterne).Plaque = Plaque;
                    v_listeCourreurs.Find(c => c.numeroInterne == numInterne).PLaqueBis = PlaqueBis;
                    v_listeInscrits.Add(numInterne);
                }
                m_MAJLVCourreursInscrits();
                tBNomCourse.Text = v_Course.Nom;
                dTPCourse.Value = v_Course.Date;
                tBLieu.Text = v_Course.Lieu;
                tBClub.Text = v_Course.Club;
                cBDoublerPlaque.Checked = v_Course.DoublerPlaque;
                cBInscrireDansCourse.Visible = true;
                cmdImprimer.Enabled = true;
                cmdExportExcel.Enabled = true;
            }
            catch { }
        }

        void m_importerResultat()
        {
            try
            {
                if (v_Course.Nom == null) { return; }
            }
            catch { return; }
            //selectionner le fichier course 
            lBCoursesExistantes.Items.Clear();
            var v_listeResultats = from string res in Directory.GetFiles(new FileInfo(Application.ExecutablePath).DirectoryName)
                                   where res.Contains("passage")
                                   select new FileInfo(res).Name;
            lBCoursesExistantes.Items.AddRange(v_listeResultats.ToArray());
            MessageBox.Show("Selectionner un Resultat");
            label25.Text = "Resultats à importer :";
        }

        void m_ImporterResultatSelectionne(string resultat)
        {
            lBPassage.Items.Clear();
            string[] passage = File.ReadAllLines(new FileInfo(Application.ExecutablePath).DirectoryName + "\\" + resultat);
            foreach (c_courreur c in v_listeCourreurs) { c.nbrTours = 0; c.TempsEnSecondes = 0; }
            foreach (string line in passage)
            {
                try
                {
                    int numPlaque = int.Parse(line.Substring(0, 3));
                    c_courreur courreur = v_listeCourreurs.Find(c1 => (c1.Plaque == numPlaque) | (c1.PLaqueBis == numPlaque));
                    c_categorie categ = v_listeCategories.Find(c => c.numeroInterne == courreur.CategorieNumInterne);
                    if (courreur.nbrTours < categ.NbrTours)
                    {
                        courreur.nbrTours++;
                        courreur.TempsEnSecondes = (int.Parse(line.Substring(10, 2)) * 3600) + (int.Parse(line.Substring(13, 2)) * 60) + int.Parse(line.Substring(16, 2));
                    }
                    lBPassage.Items.Add(line.Replace("***", "..."));
                }
                catch
                {
                    MessageBox.Show(line + " non traduite");
                    lBPassage.Items.Add(line);
                }
            }
            File.Copy(new FileInfo(Application.ExecutablePath).DirectoryName + "\\passage.txt", new FileInfo(Application.ExecutablePath).DirectoryName + "\\passage.old", true);
            m_MAJAffichageCourse(null, false);
            tCMain.SelectedIndex = 3;
        }

        bool m_inscrireCourreur(string NomCourreur, string Plaque1, string Plaque2)
        {
            if (Plaque1 == "")
            {
                MessageBox.Show("Saisissez une Plaque !!");
                return false;
            }
            try
            {
                //if (lBCourreurs.SelectedIndex < 0) { return fa; }
                string[] nom = NomCourreur.Split(new string[] { " ... " }, StringSplitOptions.RemoveEmptyEntries);
                int index = v_listeCourreurs.FindIndex(c => (c.Nom == nom[0]) & (c.Prenom == nom[1]));
                if (index > -1)
                {
                    int Plaque = int.Parse(Plaque1);
                    int PlaqueBis = int.Parse(Plaque2);
                    int existe = v_listeCourreurs.FindIndex(c => c.Plaque == Plaque);
                    if ((existe > -1) & (existe != index))
                    {
                        MessageBox.Show("Cette Plaque est deja attribuée !");
                        return false; ;
                    }
                    v_listeCourreurs[index].Plaque = Plaque;
                    v_listeCourreurs[index].PLaqueBis = PlaqueBis;
                    v_listeInscrits.Add(v_listeCourreurs[index].numeroInterne);
                    m_sauvegarderCourse();
                    tBNumeroCourreurAInscrire.Text = (Plaque + 1).ToString();
                    tBNumeroAInscrireBis.Text = (PlaqueBis + 1).ToString();
                    m_MAJLVCourreursInscrits();
                    m_MAJListeCourreurs("");
                }
                return true;
            }
            catch { return false; }
        }

        void m_MAJAffichageCourse(c_courreur CourreurSaisi, bool NouveauClassement)
        {
            try
            {
                lBClassement.Items.Clear();
                lvCourse.Items.Clear();
                lvCourse.Items.AddRange(m_remplirLVCourse(CourreurSaisi, NouveauClassement, true).ToArray());
                Application.DoEvents();
                try
                {
                    if (CourreurSaisi != null)
                    {
                        //met focus sur ligne du coureur
                        ListViewItem lv = (from ListViewItem l in lvCourse.Items where int.Parse(l.SubItems[1].Text) == CourreurSaisi.Plaque select l).First();
                        int height = lvCourse.Height / 26;
                        if (lv.Index >= height)
                        { lvCourse.TopItem = lvCourse.Items[lv.Index - height + 2]; }
                    }
                }
                catch { }
                try
                {
                    c_categorie categ = v_listeCategories.Find(c => c.Nom == tCCategorieCourse.SelectedTab.Text);
                    tbNbrToursACourrir.Text = categ.NbrTours.ToString();
                    if (lvCourse.Items[0].SubItems[5].Text != "")
                    {
                        int nbrToursCourrus = int.Parse(lvCourse.Items[0].SubItems[5].Text);
                        tBNbrToursRestant.Text = (categ.NbrTours - nbrToursCourrus).ToString();
                    }
                }
                catch { tBNbrToursRestant.Text = "**"; tbNbrToursACourrir.Text = "**"; }
            }
            catch { }
        }

        void m_MAJListeCategories()
        {
            try
            {
                //trie categorie par ages
                v_listeCategories.Sort(m_compareAgeMini);
                lVCategories.Items.Clear();
                cBCategorieCourreur.Items.Clear();
                try
                {
                    tCCategorieCourse.TabPages.Clear();
                }
                catch (Exception e) { }
                tCCategorieCourse.TabPages.Add("Toutes Catégories");

                foreach (c_categorie c in v_listeCategories)
                {
                    ListViewItem lv = new ListViewItem(c.Nom);
                    lVCategories.Items.Add(lv);
                    cBCategorieCourreur.Items.Add(c.Nom);
                    tCCategorieCourse.TabPages.Add(c.Nom);
                }
            }
            catch { MessageBox.Show("Pb MAJ Liste Categories"); }
            pBExcel.Maximum = v_listeCategories.Count + 1; //pour toutes categories en plus
        }

        void m_MAJListeCourreurs(string filter)
        {
            c_courreur c_test = new c_courreur();
            c_categorie categ_test = new c_categorie();
            filter = filter.ToUpper();
            try
            {
                lVCourreurs.Items.Clear();
                lBCourreurs.Items.Clear();
                foreach (c_courreur courreur in v_listeCourreurs)
                {
                    if (courreur.Nom.StartsWith(filter))
                    {
                        c_test = courreur;
                        //  if (c_test.Prenom == "test")
                        //  { }
                        var v_line = new ListViewItem();
                        v_line.Text = courreur.numeroInterne.ToString();
                        v_line.SubItems.Add(courreur.Nom);
                        v_line.SubItems.Add(courreur.Prenom);
                        v_line.SubItems.Add(v_listeCategories.Find(c => c.numeroInterne == courreur.CategorieNumInterne).Nom);
                        v_line.SubItems.Add(courreur.Plaque.ToString() + " / " + courreur.PLaqueBis.ToString());
                        lVCourreurs.Items.Add(v_line);

                        if ((courreur.Plaque == 0) & (courreur.Nom.StartsWith(tbFiltreNom.Text)))
                        { lBCourreurs.Items.Add(courreur.Nom + " ... " + courreur.Prenom); }
                    }
                }
            }
            catch { MessageBox.Show("Pb MAJ Liste courreurs " + c_test.Nom + c_test.Prenom); }
        }

        void m_MAJLVCourreursInscrits()
        {
            lVCourreursInscrits.Items.Clear();
            foreach (int index in v_listeInscrits)
            {
                try
                {
                    ListViewItem lv = new ListViewItem();
                    c_courreur courreur = new c_courreur(v_listeCourreurs.Find(c => c.numeroInterne == index));
                    lv.Text = courreur.Nom;
                    lv.SubItems.Add(courreur.Prenom);
                    lv.SubItems.Add(v_listeCategories.Find(c => c.numeroInterne == courreur.CategorieNumInterne).Nom);
                    lv.SubItems.Add(courreur.Plaque.ToString() + " / " + courreur.PLaqueBis.ToString());
                    lVCourreursInscrits.Items.Add(lv);
                    //supprime du listboxCourreurs
                    string name = courreur.Nom + " ... " + courreur.Prenom;
                    int index1 = 0;
                    foreach (string line in lBCourreurs.Items)
                    {
                        if (line == name)
                        {
                            lBCourreurs.Items.RemoveAt(index1);
                            break;
                        }
                        else index1++;
                    }
                }
                catch { }
            }
            m_MAJListeCourreurs("");
            m_MAJAffichageCourse(null, false);
        }

        public List<ListViewItem> m_remplirLVCourse(c_courreur CourreurSaisi, bool NouveauClassement, bool Recursif) //nouveauclassement pour mise à jour des places, sinon fleches n'apparaissent qu'un fois
        {
            int v_nbrToursMax = v_listeCategories.Max(c => c.NbrTours);
            bool v_nbrTourMaxFini = false;
            int place = 1;
            List<ListViewItem> result = new List<ListViewItem>();
            //classe par categ aussi
            if ((Recursif) && (NouveauClassement) && (CourreurSaisi != null))
            {
                if (v_filtreCourreur == "")
                {
                    try
                    {
                        v_filtreCourreur = (v_listeCategories.Find(categ => categ.numeroInterne == CourreurSaisi.CategorieNumInterne)).Nom;
                        m_remplirLVCourse(CourreurSaisi, true, false);
                    }
                    catch { }
                    v_filtreCourreur = "";
                }
                else
                {
                    string past = v_filtreCourreur;
                    v_filtreCourreur = "";
                    m_remplirLVCourse(CourreurSaisi, true, false);
                    v_filtreCourreur = past;
                }
            }
            #region Classe les coureurs
            lBClassement.Items.Clear();
            foreach (string line in v_Course.listeInscritsEnString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    c_courreur courreur = v_listeCourreurs.Find(c => c.numeroInterne == int.Parse(line.Substring(0, 3)));
                    lBClassement.Items.Add(string.Format("{0:000},{1:00000},{2:000}", 999 - courreur.nbrTours, courreur.TempsEnSecondes, courreur.numeroInterne));
                }
                catch { MessageBox.Show("Pb identification courreur ds m_remplirLVCourse " + line); }
            }
            #endregion
            int v_tempsRef = 0;
            int v_toursRef = 0;
            int v_nbrFini = 0;
            int v_placeGenerale = 0;
            int v_placeCategorie = 0;
            foreach (string line in lBClassement.Items)
            {
                int numeroInterne = int.Parse(line.ToString().Substring(10, 3));
                c_courreur c = v_listeCourreurs.Find(x => x.numeroInterne == numeroInterne);
                v_placeGenerale++;
                if (v_listeCategories.Find(categ => categ.numeroInterne == c.CategorieNumInterne).Nom.Contains(v_filtreCourreur))
                {
                    v_placeCategorie++;
                    if (place == 1)
                    { v_tempsRef = c.TempsEnSecondes; v_toursRef = c.nbrTours; }
                    if (c.nbrTours >= v_nbrToursMax) { v_nbrTourMaxFini = true; }
                    ListViewItem lV = new ListViewItem(string.Format("{0:000}", place));
                    c_categorie categ_ici = v_listeCategories.Find(categ => categ.numeroInterne == c.CategorieNumInterne);
                    if (v_Course.DoublerPlaque)
                    {
                        lV.SubItems.Add(c.Plaque.ToString() + " / " + c.PLaqueBis.ToString());
                    }
                    else { lV.SubItems.Add(c.Plaque.ToString()); }
                    #region ajoute une fleche si chgt de place
                    string v_nomFinal = c.Nom;
                    if (NouveauClassement)
                    {
                        if (v_filtreCourreur == "")
                        {
                            if (c.PlaceGenerale != 0)
                            {
                                if (c.PlaceGenerale < v_placeGenerale)
                                {
                                    c.ClassementGeneral = -1;
                                }
                                if (c.PlaceGenerale > v_placeGenerale)
                                {
                                    c.ClassementGeneral = 1;
                                }
                                if (c.PlaceGenerale == v_placeGenerale)
                                {
                                    c.ClassementGeneral = 0;
                                }
                            }
                        }
                        else
                        {
                            if (c.PlaceCategorie != 0)
                            {
                                if (c.PlaceCategorie < v_placeCategorie)
                                {
                                    c.ClassementCategorie = -1;
                                }
                                if (c.PlaceCategorie > v_placeCategorie)
                                {
                                    c.ClassementCategorie = 1;
                                }
                                if (c.PlaceCategorie == v_placeCategorie)
                                {
                                    c.ClassementCategorie = 0;
                                }
                            }
                        }
                    }
                    if (v_filtreCourreur == "")
                    {
                        switch (c.ClassementGeneral)
                        {
                            case -1: v_nomFinal += " ↓"; break;
                            case 1: v_nomFinal += " ↑"; break;
                        }
                    }
                    else
                    {
                        switch (c.ClassementCategorie)
                        {
                            case -1: v_nomFinal += " ↓"; break;
                            case 1: v_nomFinal += " ↑"; break;
                        }
                    }
                    #endregion
                    lV.SubItems.Add(v_nomFinal);
                    lV.SubItems.Add(c.Prenom);
                    lV.SubItems.Add(categ_ici.Nom);
                    lV.SubItems.Add(c.nbrTours.ToString());
                    lV.SubItems.Add(string.Format("{0:00}:{1:00}.{2:00}", c.TempsEnSecondes / 3600, (c.TempsEnSecondes / 60) % 60, c.TempsEnSecondes % 60));
                    if (place > 1)
                    {
                        if (c.nbrTours == v_toursRef)
                        {
                            if ((c.TempsEnSecondes - v_tempsRef) > 0)
                            { lV.SubItems.Add(string.Format("{0:00}:{1:00}.{2:00}", (c.TempsEnSecondes - v_tempsRef) / 3600, ((c.TempsEnSecondes - v_tempsRef) / 60) % 60, (c.TempsEnSecondes - v_tempsRef) % 60)); }
                        }
                        else { lV.SubItems.Add((v_toursRef - c.nbrTours).ToString() + " tour(s)"); }
                    }
                    //evite afficher courreur sans Plaque
                    //filtre les courreurs non termines
                    bool v_ajouter = true;
                    if (c.nbrTours >= categ_ici.NbrTours) { v_ajouter = false; }
                    if (v_nbrTourMaxFini) { if (c.TempsEnSecondes > v_tempsRef) { v_ajouter = false; } }
                    if (!v_ajouter)
                    { lV.SubItems[0].BackColor = Color.Red; v_nbrFini++; }
                    v_ajouter |= !cBEncoreEnCourse.Checked;
                    //met le courreur saisi en vert
                    if (CourreurSaisi != null)
                    {
                        if ((c == CourreurSaisi) & (lV.SubItems[0].BackColor != Color.Red))
                        {
                            lV.SubItems[0].BackColor = Color.Lime;
                            Application.DoEvents();
                        }
                    }

                    if ((c.Plaque > 0) & (v_ajouter))
                    {
                        result.Add(lV);
                    }
                    if (NouveauClassement)
                    {
                        if (v_filtreCourreur != "")
                        {
                            c.PlaceCategorie = v_placeCategorie;
                        }
                        else { c.PlaceGenerale = v_placeGenerale; }
                    }
                    Application.DoEvents();
                    place++;
                }
            }
            tbNbrEncoreEnCourse.Text = (place - v_nbrFini - 1).ToString();
            return result;
        }

        void m_sauvegarderCourse()
        {
            c_Ini v_ini = new c_Ini("Courses.ini");
            v_Course.listeInscritsEnString = "";
            foreach (int numerointerne in v_listeInscrits)
            {
                c_courreur v_courreur = v_listeCourreurs.Find(c => c.numeroInterne == numerointerne);
                v_Course.listeInscritsEnString += string.Format("{0:000}>{1:000}{2:000}|", numerointerne, v_courreur.Plaque, v_courreur.PLaqueBis);
            }
            v_ini.m_Write(v_Course, v_Course.Nom);
            m_MAJLVCourreursInscrits();
            cBInscrireDansCourse.Visible = true;
            cmdImprimer.Enabled = true;
            cmdExportExcel.Enabled = true;
        }

        void m_trierColonneLV(object sender, ColumnClickEventArgs e)
        {
            try//teste si colonne vide
            { var test = (sender as ListView).Items[0].SubItems[e.Column].Text; }
            catch { return; }
            ((ListView)sender).ListViewItemSorter = new ListViewItemSorter(e.Column, cBSensTriCourreur.Checked);
            ((ListView)sender).Sort();
            cBSensTriCourreur.Checked = (!cBSensTriCourreur.Checked);
            ((ListView)sender).ListViewItemSorter = null;
        }

        private int m_compareAgeMini(c_categorie a, c_categorie b)
        {
            return string.Compare(string.Format("{0:000}", a.AgeMini), string.Format("{0:000}", b.AgeMini));
        }

        #endregion

        private void e_cmdExportExcel_Click(object sender, EventArgs e)
        {
            List<c_courreur> listeInscrits = (from string Plaque in v_Course.listeInscritsEnString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                                              select (v_listeCourreurs.Find(c => c.numeroInterne == int.Parse(Plaque.Substring(0, 3))))).ToList();
            formImpression fI = new formImpression(listeInscrits);
            fI.ExportExcel();
        }

        private void e_tbFiltreNom_TextChanged(object sender, EventArgs e)
        {
            m_MAJListeCourreurs("");
        }

        private void e_tbFiltreNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        public static void m_MAJProgressBar(int index)
        {
            pBExcel.Value = index;
        }

        private void e_cBEncoreEnCourse_CheckedChanged(object sender, EventArgs e)
        {
            m_MAJAffichageCourse(dernierCourreurSaisi, false);
        }

        private void e_cBDoublerPlaque_CheckedChanged(object sender, EventArgs e)
        {
            tBPlaqueBis.Visible = cBDoublerPlaque.Checked;
            tBNumeroAInscrireBis.Visible = cBDoublerPlaque.Checked;
        }

        private void e_tBNomCourreur_TextChanged(object sender, EventArgs e)
        {
            //tri le lvpilote 
            m_MAJListeCourreurs(tBNomCourreur.Text);
        }
    }

    public class ListViewItemSorter : IComparer
    {
        #region définitions

        private int colonne;
        private bool triUp;

        #endregion

        #region methodes

        public int Compare(object x, object y)
        {
            try
            {
                string itemx = ((ListViewItem)x).SubItems[colonne].Text;
                string itemy = ((ListViewItem)y).SubItems[colonne].Text;

                // tri par ordre croissant ou decroissant
                if (triUp) { return String.Compare(itemx, itemy); } else { return string.Compare(itemy, itemx); }
            }
            catch { return 0; }
        }
        public ListViewItemSorter(int colonne, bool triup)
        {
            this.colonne = colonne;
            this.triUp = triup;
        }

        #endregion
    }

}


