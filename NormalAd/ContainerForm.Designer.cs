namespace NormalAd
{
    partial class ContainerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContainerForm));
            this.btnCl = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblCType = new System.Windows.Forms.Label();
            this.lblContainer = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.dgvContainers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContainers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCl
            // 
            this.btnCl.BackColor = System.Drawing.Color.Azure;
            this.btnCl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCl.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnCl.Location = new System.Drawing.Point(669, 544);
            this.btnCl.Margin = new System.Windows.Forms.Padding(4);
            this.btnCl.Name = "btnCl";
            this.btnCl.Size = new System.Drawing.Size(119, 36);
            this.btnCl.TabIndex = 86;
            this.btnCl.Text = "Clear";
            this.btnCl.UseVisualStyleBackColor = false;
            this.btnCl.Click += new System.EventHandler(this.btnCl_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(259, 433);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(256, 74);
            this.txtNotes.TabIndex = 73;
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.BackColor = System.Drawing.Color.Transparent;
            this.lblCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapacity.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCapacity.Location = new System.Drawing.Point(14, 302);
            this.lblCapacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(113, 29);
            this.lblCapacity.TabIndex = 72;
            this.lblCapacity.Text = "Capacity";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblStatus.Location = new System.Drawing.Point(14, 371);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(85, 29);
            this.lblStatus.TabIndex = 71;
            this.lblStatus.Text = "Status";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.BackColor = System.Drawing.Color.Transparent;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblNotes.Location = new System.Drawing.Point(14, 434);
            this.lblNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(82, 29);
            this.lblNotes.TabIndex = 70;
            this.lblNotes.Text = "Notes";
            // 
            // lblCType
            // 
            this.lblCType.AutoSize = true;
            this.lblCType.BackColor = System.Drawing.Color.Transparent;
            this.lblCType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCType.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCType.Location = new System.Drawing.Point(11, 252);
            this.lblCType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCType.Name = "lblCType";
            this.lblCType.Size = new System.Drawing.Size(192, 29);
            this.lblCType.TabIndex = 67;
            this.lblCType.Text = "Container Type";
            // 
            // lblContainer
            // 
            this.lblContainer.AutoSize = true;
            this.lblContainer.BackColor = System.Drawing.Color.Transparent;
            this.lblContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContainer.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblContainer.Location = new System.Drawing.Point(11, 197);
            this.lblContainer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContainer.Name = "lblContainer";
            this.lblContainer.Size = new System.Drawing.Size(226, 29);
            this.lblContainer.TabIndex = 66;
            this.lblContainer.Text = "Container Number";
            // 
            // txtNumber
            // 
            this.txtNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.Location = new System.Drawing.Point(258, 197);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(256, 30);
            this.txtNumber.TabIndex = 64;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Azure;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Location = new System.Drawing.Point(395, 544);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 36);
            this.btnSave.TabIndex = 65;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lavender;
            this.label1.Location = new System.Drawing.Point(494, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 42);
            this.label1.TabIndex = 63;
            this.label1.Text = "Container";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "--SELECT--",
            "Refrigerated",
            "Open Top"});
            this.comboBox1.Location = new System.Drawing.Point(258, 257);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(256, 24);
            this.comboBox1.TabIndex = 90;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtCapacity
            // 
            this.txtCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapacity.Location = new System.Drawing.Point(258, 302);
            this.txtCapacity.Margin = new System.Windows.Forms.Padding(4);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(256, 30);
            this.txtCapacity.TabIndex = 91;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "--SELECT--",
            "Available",
            "In Use",
            "Maintenance"});
            this.cmbStatus.Location = new System.Drawing.Point(258, 371);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(256, 24);
            this.cmbStatus.TabIndex = 92;
            // 
            // dgvContainers
            // 
            this.dgvContainers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContainers.Location = new System.Drawing.Point(535, 197);
            this.dgvContainers.Name = "dgvContainers";
            this.dgvContainers.RowHeadersWidth = 51;
            this.dgvContainers.RowTemplate.Height = 24;
            this.dgvContainers.Size = new System.Drawing.Size(573, 310);
            this.dgvContainers.TabIndex = 93;
            this.dgvContainers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContainers_CellClick);
            this.dgvContainers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContainers_CellContentClick);
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1120, 624);
            this.Controls.Add(this.dgvContainers);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnCl);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblCType);
            this.Controls.Add(this.lblContainer);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Name = "ContainerForm";
            this.Text = "Container Form";
            this.Load += new System.EventHandler(this.ContainerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContainers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCl;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblCType;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DataGridView dgvContainers;
    }
}