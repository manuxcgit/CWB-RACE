namespace CWB_Race
{
    partial class formImpression
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formImpression));
            this.dGVResultat = new System.Windows.Forms.DataGridView();
            this.ColumnPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrenom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPlaque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNumLicence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTemps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gBParamImpression = new System.Windows.Forms.GroupBox();
            this.cBPrevisualisation = new System.Windows.Forms.CheckBox();
            this.cBToutSelectionner = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bTAvant = new System.Windows.Forms.Button();
            this.bTArriere = new System.Windows.Forms.Button();
            this.bTLancerImpression = new System.Windows.Forms.Button();
            this.dUDNbrExemplaires = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cLBCategories = new System.Windows.Forms.CheckedListBox();
            this.dTPDateCourse = new System.Windows.Forms.DateTimePicker();
            this.gBPage = new System.Windows.Forms.GroupBox();
            this.lBClassement = new System.Windows.Forms.ListBox();
            this.gBInfoCourse = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tBCommentaire = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tBDateCourse = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tBClubCourse = new System.Windows.Forms.TextBox();
            this.tBLieuCourse = new System.Windows.Forms.TextBox();
            this.tBNomCourse = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tBCategorieCourse = new System.Windows.Forms.TextBox();
            this.lbTempsAuTour = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGVResultat)).BeginInit();
            this.gBParamImpression.SuspendLayout();
            this.gBPage.SuspendLayout();
            this.gBInfoCourse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVResultat
            // 
            this.dGVResultat.AllowUserToAddRows = false;
            this.dGVResultat.AllowUserToDeleteRows = false;
            this.dGVResultat.AllowUserToResizeColumns = false;
            this.dGVResultat.AllowUserToResizeRows = false;
            this.dGVResultat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dGVResultat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dGVResultat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dGVResultat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVResultat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPlace,
            this.ColumnNom,
            this.ColumnPrenom,
            this.ColumnPlaque,
            this.ColumnNumLicence,
            this.ColumnClub,
            this.ColumnTours,
            this.ColumnTemps});
            this.dGVResultat.Location = new System.Drawing.Point(7, 134);
            this.dGVResultat.MultiSelect = false;
            this.dGVResultat.Name = "dGVResultat";
            this.dGVResultat.ReadOnly = true;
            this.dGVResultat.RowHeadersVisible = false;
            this.dGVResultat.Size = new System.Drawing.Size(664, 430);
            this.dGVResultat.TabIndex = 0;
            // 
            // ColumnPlace
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnPlace.DefaultCellStyle = dataGridViewCellStyle21;
            this.ColumnPlace.HeaderText = "Place";
            this.ColumnPlace.Name = "ColumnPlace";
            this.ColumnPlace.ReadOnly = true;
            this.ColumnPlace.Width = 40;
            // 
            // ColumnNom
            // 
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnNom.DefaultCellStyle = dataGridViewCellStyle22;
            this.ColumnNom.HeaderText = "Nom";
            this.ColumnNom.Name = "ColumnNom";
            this.ColumnNom.ReadOnly = true;
            this.ColumnNom.Width = 150;
            // 
            // ColumnPrenom
            // 
            this.ColumnPrenom.HeaderText = "Prénom";
            this.ColumnPrenom.Name = "ColumnPrenom";
            this.ColumnPrenom.ReadOnly = true;
            // 
            // ColumnPlaque
            // 
            this.ColumnPlaque.HeaderText = "Plaque";
            this.ColumnPlaque.Name = "ColumnPlaque";
            this.ColumnPlaque.ReadOnly = true;
            this.ColumnPlaque.Width = 45;
            // 
            // ColumnNumLicence
            // 
            this.ColumnNumLicence.HeaderText = "N° Licence";
            this.ColumnNumLicence.Name = "ColumnNumLicence";
            this.ColumnNumLicence.ReadOnly = true;
            this.ColumnNumLicence.Width = 70;
            // 
            // ColumnClub
            // 
            this.ColumnClub.HeaderText = "Club";
            this.ColumnClub.Name = "ColumnClub";
            this.ColumnClub.ReadOnly = true;
            this.ColumnClub.Width = 85;
            // 
            // ColumnTours
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnTours.DefaultCellStyle = dataGridViewCellStyle23;
            this.ColumnTours.HeaderText = "  Tours";
            this.ColumnTours.Name = "ColumnTours";
            this.ColumnTours.ReadOnly = true;
            this.ColumnTours.Width = 50;
            // 
            // ColumnTemps
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnTemps.DefaultCellStyle = dataGridViewCellStyle24;
            this.ColumnTemps.HeaderText = "         Temps";
            this.ColumnTemps.Name = "ColumnTemps";
            this.ColumnTemps.ReadOnly = true;
            // 
            // gBParamImpression
            // 
            this.gBParamImpression.Controls.Add(this.cBPrevisualisation);
            this.gBParamImpression.Controls.Add(this.cBToutSelectionner);
            this.gBParamImpression.Controls.Add(this.label2);
            this.gBParamImpression.Controls.Add(this.bTAvant);
            this.gBParamImpression.Controls.Add(this.bTArriere);
            this.gBParamImpression.Controls.Add(this.bTLancerImpression);
            this.gBParamImpression.Controls.Add(this.dUDNbrExemplaires);
            this.gBParamImpression.Controls.Add(this.label1);
            this.gBParamImpression.Controls.Add(this.cLBCategories);
            this.gBParamImpression.Controls.Add(this.dTPDateCourse);
            this.gBParamImpression.Location = new System.Drawing.Point(12, 4);
            this.gBParamImpression.Name = "gBParamImpression";
            this.gBParamImpression.Size = new System.Drawing.Size(677, 103);
            this.gBParamImpression.TabIndex = 1;
            this.gBParamImpression.TabStop = false;
            this.gBParamImpression.Text = "Parametres Impression";
            // 
            // cBPrevisualisation
            // 
            this.cBPrevisualisation.AutoSize = true;
            this.cBPrevisualisation.Checked = true;
            this.cBPrevisualisation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBPrevisualisation.Location = new System.Drawing.Point(23, 48);
            this.cBPrevisualisation.Name = "cBPrevisualisation";
            this.cBPrevisualisation.Size = new System.Drawing.Size(99, 17);
            this.cBPrevisualisation.TabIndex = 13;
            this.cBPrevisualisation.Text = "Prévisualisation";
            this.cBPrevisualisation.UseVisualStyleBackColor = true;
            // 
            // cBToutSelectionner
            // 
            this.cBToutSelectionner.AutoSize = true;
            this.cBToutSelectionner.Location = new System.Drawing.Point(23, 71);
            this.cBToutSelectionner.Name = "cBToutSelectionner";
            this.cBToutSelectionner.Size = new System.Drawing.Size(110, 17);
            this.cBToutSelectionner.TabIndex = 8;
            this.cBToutSelectionner.Text = "Tout Selectionner";
            this.cBToutSelectionner.UseVisualStyleBackColor = true;
            this.cBToutSelectionner.CheckedChanged += new System.EventHandler(this.cBToutSelectionner_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(502, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Affichage";
            // 
            // bTAvant
            // 
            this.bTAvant.Location = new System.Drawing.Point(567, 17);
            this.bTAvant.Name = "bTAvant";
            this.bTAvant.Size = new System.Drawing.Size(51, 32);
            this.bTAvant.TabIndex = 6;
            this.bTAvant.Text = ">";
            this.bTAvant.UseVisualStyleBackColor = true;
            this.bTAvant.Click += new System.EventHandler(this.bTAvant_Click);
            // 
            // bTArriere
            // 
            this.bTArriere.Location = new System.Drawing.Point(435, 17);
            this.bTArriere.Name = "bTArriere";
            this.bTArriere.Size = new System.Drawing.Size(51, 32);
            this.bTArriere.TabIndex = 5;
            this.bTArriere.Text = "<";
            this.bTArriere.UseVisualStyleBackColor = true;
            this.bTArriere.Click += new System.EventHandler(this.bTArriere_Click);
            // 
            // bTLancerImpression
            // 
            this.bTLancerImpression.Location = new System.Drawing.Point(435, 62);
            this.bTLancerImpression.Name = "bTLancerImpression";
            this.bTLancerImpression.Size = new System.Drawing.Size(183, 32);
            this.bTLancerImpression.TabIndex = 4;
            this.bTLancerImpression.Text = "Lancer Impression";
            this.bTLancerImpression.UseVisualStyleBackColor = true;
            this.bTLancerImpression.Click += new System.EventHandler(this.bTLancerImpression_Click);
            // 
            // dUDNbrExemplaires
            // 
            this.dUDNbrExemplaires.Items.Add("9");
            this.dUDNbrExemplaires.Items.Add("8");
            this.dUDNbrExemplaires.Items.Add("7");
            this.dUDNbrExemplaires.Items.Add("6");
            this.dUDNbrExemplaires.Items.Add("5");
            this.dUDNbrExemplaires.Items.Add("4");
            this.dUDNbrExemplaires.Items.Add("3");
            this.dUDNbrExemplaires.Items.Add("2");
            this.dUDNbrExemplaires.Items.Add("1");
            this.dUDNbrExemplaires.Location = new System.Drawing.Point(143, 25);
            this.dUDNbrExemplaires.Name = "dUDNbrExemplaires";
            this.dUDNbrExemplaires.Size = new System.Drawing.Size(38, 20);
            this.dUDNbrExemplaires.TabIndex = 2;
            this.dUDNbrExemplaires.Text = "1";
            this.dUDNbrExemplaires.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre d\'exemplaires :";
            // 
            // cLBCategories
            // 
            this.cLBCategories.CheckOnClick = true;
            this.cLBCategories.FormattingEnabled = true;
            this.cLBCategories.Location = new System.Drawing.Point(246, 15);
            this.cLBCategories.Name = "cLBCategories";
            this.cLBCategories.Size = new System.Drawing.Size(120, 79);
            this.cLBCategories.TabIndex = 3;
            // 
            // dTPDateCourse
            // 
            this.dTPDateCourse.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPDateCourse.Location = new System.Drawing.Point(263, 48);
            this.dTPDateCourse.Name = "dTPDateCourse";
            this.dTPDateCourse.Size = new System.Drawing.Size(86, 20);
            this.dTPDateCourse.TabIndex = 12;
            this.dTPDateCourse.ValueChanged += new System.EventHandler(this.dTPDateCourse_ValueChanged);
            // 
            // gBPage
            // 
            this.gBPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gBPage.BackColor = System.Drawing.Color.ForestGreen;
            this.gBPage.Controls.Add(this.lBClassement);
            this.gBPage.Controls.Add(this.gBInfoCourse);
            this.gBPage.Controls.Add(this.dGVResultat);
            this.gBPage.Controls.Add(this.lbTempsAuTour);
            this.gBPage.Location = new System.Drawing.Point(12, 113);
            this.gBPage.Name = "gBPage";
            this.gBPage.Size = new System.Drawing.Size(677, 570);
            this.gBPage.TabIndex = 2;
            this.gBPage.TabStop = false;
            // 
            // lBClassement
            // 
            this.lBClassement.FormattingEnabled = true;
            this.lBClassement.Location = new System.Drawing.Point(60, 287);
            this.lBClassement.Name = "lBClassement";
            this.lBClassement.Size = new System.Drawing.Size(120, 95);
            this.lBClassement.Sorted = true;
            this.lBClassement.TabIndex = 2;
            this.lBClassement.Visible = false;
            // 
            // gBInfoCourse
            // 
            this.gBInfoCourse.BackColor = System.Drawing.Color.LimeGreen;
            this.gBInfoCourse.Controls.Add(this.label8);
            this.gBInfoCourse.Controls.Add(this.tBCommentaire);
            this.gBInfoCourse.Controls.Add(this.pictureBox1);
            this.gBInfoCourse.Controls.Add(this.tBDateCourse);
            this.gBInfoCourse.Controls.Add(this.label7);
            this.gBInfoCourse.Controls.Add(this.label6);
            this.gBInfoCourse.Controls.Add(this.tBClubCourse);
            this.gBInfoCourse.Controls.Add(this.tBLieuCourse);
            this.gBInfoCourse.Controls.Add(this.tBNomCourse);
            this.gBInfoCourse.Controls.Add(this.label5);
            this.gBInfoCourse.Controls.Add(this.label4);
            this.gBInfoCourse.Controls.Add(this.label3);
            this.gBInfoCourse.Controls.Add(this.tBCategorieCourse);
            this.gBInfoCourse.Location = new System.Drawing.Point(6, 19);
            this.gBInfoCourse.Name = "gBInfoCourse";
            this.gBInfoCourse.Size = new System.Drawing.Size(665, 103);
            this.gBInfoCourse.TabIndex = 1;
            this.gBInfoCourse.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(224, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Categorie : ";
            // 
            // tBCommentaire
            // 
            this.tBCommentaire.Location = new System.Drawing.Point(304, 16);
            this.tBCommentaire.Name = "tBCommentaire";
            this.tBCommentaire.Size = new System.Drawing.Size(196, 20);
            this.tBCommentaire.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::BWC_Race.Properties.Resources.cwb;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(587, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // tBDateCourse
            // 
            this.tBDateCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBDateCourse.Location = new System.Drawing.Point(557, 11);
            this.tBDateCourse.Name = "tBDateCourse";
            this.tBDateCourse.Size = new System.Drawing.Size(98, 26);
            this.tBDateCourse.TabIndex = 8;
            this.tBDateCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(515, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Date :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Commentaire :";
            // 
            // tBClubCourse
            // 
            this.tBClubCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBClubCourse.Location = new System.Drawing.Point(54, 68);
            this.tBClubCourse.Name = "tBClubCourse";
            this.tBClubCourse.Size = new System.Drawing.Size(152, 20);
            this.tBClubCourse.TabIndex = 5;
            // 
            // tBLieuCourse
            // 
            this.tBLieuCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBLieuCourse.Location = new System.Drawing.Point(54, 42);
            this.tBLieuCourse.Name = "tBLieuCourse";
            this.tBLieuCourse.Size = new System.Drawing.Size(152, 20);
            this.tBLieuCourse.TabIndex = 4;
            // 
            // tBNomCourse
            // 
            this.tBNomCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBNomCourse.Location = new System.Drawing.Point(55, 16);
            this.tBNomCourse.Name = "tBNomCourse";
            this.tBNomCourse.Size = new System.Drawing.Size(152, 20);
            this.tBNomCourse.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Club :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Lieu :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nom :";
            // 
            // tBCategorieCourse
            // 
            this.tBCategorieCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBCategorieCourse.Location = new System.Drawing.Point(304, 56);
            this.tBCategorieCourse.Name = "tBCategorieCourse";
            this.tBCategorieCourse.Size = new System.Drawing.Size(256, 35);
            this.tBCategorieCourse.TabIndex = 11;
            this.tBCategorieCourse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbTempsAuTour
            // 
            this.lbTempsAuTour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTempsAuTour.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTempsAuTour.FormattingEnabled = true;
            this.lbTempsAuTour.ItemHeight = 14;
            this.lbTempsAuTour.Location = new System.Drawing.Point(7, 8);
            this.lbTempsAuTour.Name = "lbTempsAuTour";
            this.lbTempsAuTour.Size = new System.Drawing.Size(664, 550);
            this.lbTempsAuTour.TabIndex = 3;
            this.lbTempsAuTour.Visible = false;
            // 
            // formImpression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 695);
            this.Controls.Add(this.gBPage);
            this.Controls.Add(this.gBParamImpression);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formImpression";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impression des resultats";
            this.Load += new System.EventHandler(this.formImpression_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVResultat)).EndInit();
            this.gBParamImpression.ResumeLayout(false);
            this.gBParamImpression.PerformLayout();
            this.gBPage.ResumeLayout(false);
            this.gBInfoCourse.ResumeLayout(false);
            this.gBInfoCourse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVResultat;
        private System.Windows.Forms.GroupBox gBParamImpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox cLBCategories;
        private System.Windows.Forms.DomainUpDown dUDNbrExemplaires;
        private System.Windows.Forms.Button bTArriere;
        private System.Windows.Forms.Button bTLancerImpression;
        private System.Windows.Forms.CheckBox cBToutSelectionner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bTAvant;
        private System.Windows.Forms.GroupBox gBPage;
        private System.Windows.Forms.DateTimePicker dTPDateCourse;
        private System.Windows.Forms.CheckBox cBPrevisualisation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrenom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPlaque;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumLicence;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClub;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTours;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTemps;
        private System.Windows.Forms.ListBox lBClassement;
        private System.Windows.Forms.GroupBox gBInfoCourse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tBCommentaire;
        private System.Windows.Forms.TextBox tBDateCourse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBClubCourse;
        private System.Windows.Forms.TextBox tBLieuCourse;
        private System.Windows.Forms.TextBox tBNomCourse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBCategorieCourse;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox lbTempsAuTour;
    }
}