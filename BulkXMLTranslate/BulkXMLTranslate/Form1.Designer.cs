namespace BulkXMLTranslate
{
    partial class Form1
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
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.cmbDest = new System.Windows.Forms.ComboBox();
            this.tbxLonger = new System.Windows.Forms.TextBox();
            this.btnTranslateSingle = new System.Windows.Forms.Button();
            this.btnTranslateAll = new System.Windows.Forms.Button();
            this.lstSource = new System.Windows.Forms.ListBox();
            this.lstDest = new System.Windows.Forms.ListBox();
            this.stsInfo = new System.Windows.Forms.StatusStrip();
            this.tstProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tstLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stsInfo.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbSource
            // 
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Location = new System.Drawing.Point(12, 27);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(197, 21);
            this.cmbSource.TabIndex = 2;
            // 
            // cmbDest
            // 
            this.cmbDest.FormattingEnabled = true;
            this.cmbDest.Location = new System.Drawing.Point(310, 27);
            this.cmbDest.Name = "cmbDest";
            this.cmbDest.Size = new System.Drawing.Size(197, 21);
            this.cmbDest.TabIndex = 3;
            // 
            // tbxLonger
            // 
            this.tbxLonger.Location = new System.Drawing.Point(12, 367);
            this.tbxLonger.Multiline = true;
            this.tbxLonger.Name = "tbxLonger";
            this.tbxLonger.ReadOnly = true;
            this.tbxLonger.Size = new System.Drawing.Size(495, 87);
            this.tbxLonger.TabIndex = 4;
            // 
            // btnTranslateSingle
            // 
            this.btnTranslateSingle.Font = new System.Drawing.Font("Lucida Sans", 21F, System.Drawing.FontStyle.Bold);
            this.btnTranslateSingle.Location = new System.Drawing.Point(215, 95);
            this.btnTranslateSingle.Name = "btnTranslateSingle";
            this.btnTranslateSingle.Size = new System.Drawing.Size(89, 89);
            this.btnTranslateSingle.TabIndex = 5;
            this.btnTranslateSingle.Text = "----->";
            this.btnTranslateSingle.UseVisualStyleBackColor = true;
            this.btnTranslateSingle.Click += new System.EventHandler(this.btnTranslateSingle_Click);
            // 
            // btnTranslateAll
            // 
            this.btnTranslateAll.Font = new System.Drawing.Font("Lucida Sans", 21F, System.Drawing.FontStyle.Bold);
            this.btnTranslateAll.Location = new System.Drawing.Point(215, 206);
            this.btnTranslateAll.Name = "btnTranslateAll";
            this.btnTranslateAll.Size = new System.Drawing.Size(89, 89);
            this.btnTranslateAll.TabIndex = 6;
            this.btnTranslateAll.Text = "==>";
            this.btnTranslateAll.UseVisualStyleBackColor = true;
            this.btnTranslateAll.Click += new System.EventHandler(this.btnTranslateAll_Click);
            // 
            // lstSource
            // 
            this.lstSource.FormattingEnabled = true;
            this.lstSource.Location = new System.Drawing.Point(12, 54);
            this.lstSource.Name = "lstSource";
            this.lstSource.Size = new System.Drawing.Size(197, 303);
            this.lstSource.TabIndex = 7;
            this.lstSource.SelectedIndexChanged += new System.EventHandler(this.lst_SelectChanged);
            // 
            // lstDest
            // 
            this.lstDest.FormattingEnabled = true;
            this.lstDest.Location = new System.Drawing.Point(310, 58);
            this.lstDest.Name = "lstDest";
            this.lstDest.Size = new System.Drawing.Size(197, 303);
            this.lstDest.TabIndex = 8;
            this.lstDest.SelectedIndexChanged += new System.EventHandler(this.lst_SelectChanged);
            // 
            // stsInfo
            // 
            this.stsInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstProgress,
            this.tstLblInfo});
            this.stsInfo.Location = new System.Drawing.Point(0, 464);
            this.stsInfo.Name = "stsInfo";
            this.stsInfo.Size = new System.Drawing.Size(519, 22);
            this.stsInfo.TabIndex = 9;
            this.stsInfo.Text = "statusStrip1";
            // 
            // tstProgress
            // 
            this.tstProgress.AutoSize = false;
            this.tstProgress.Name = "tstProgress";
            this.tstProgress.Size = new System.Drawing.Size(350, 16);
            // 
            // tstLblInfo
            // 
            this.tstLblInfo.Name = "tstLblInfo";
            this.tstLblInfo.Size = new System.Drawing.Size(121, 17);
            this.tstLblInfo.Spring = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(519, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 486);
            this.Controls.Add(this.stsInfo);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lstDest);
            this.Controls.Add(this.lstSource);
            this.Controls.Add(this.btnTranslateAll);
            this.Controls.Add(this.btnTranslateSingle);
            this.Controls.Add(this.tbxLonger);
            this.Controls.Add(this.cmbDest);
            this.Controls.Add(this.cmbSource);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Bulk xml translate";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.stsInfo.ResumeLayout(false);
            this.stsInfo.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.ComboBox cmbDest;
        private System.Windows.Forms.TextBox tbxLonger;
        private System.Windows.Forms.Button btnTranslateSingle;
        private System.Windows.Forms.Button btnTranslateAll;
        private System.Windows.Forms.ListBox lstSource;
        private System.Windows.Forms.ListBox lstDest;
        private System.Windows.Forms.StatusStrip stsInfo;
        private System.Windows.Forms.ToolStripProgressBar tstProgress;
        private System.Windows.Forms.ToolStripStatusLabel tstLblInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    }
}

