namespace CarDealer.Desktop
{
    partial class Menuform
    {
        private System.ComponentModel.IContainer components = null;

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

        
        private void InitializeComponent()
        {
            tabMain = new TabControl();
            tabDetails = new TabPage();
            tabSpecs = new TabPage();
            tabPricing = new TabPage();
            tabMedia = new TabPage();
            tabMain.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabDetails);
            tabMain.Controls.Add(tabSpecs);
            tabMain.Controls.Add(tabPricing);
            tabMain.Controls.Add(tabMedia);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1000, 650);
            tabMain.TabIndex = 0;
            // 
            // tabDetails
            // 
            tabDetails.Location = new Point(4, 24);
            tabDetails.Name = "tabDetails";
            tabDetails.Padding = new Padding(3);
            tabDetails.Size = new Size(992, 622);
            tabDetails.TabIndex = 0;
            tabDetails.Text = "Vehicle Details";
            tabDetails.UseVisualStyleBackColor = true;
            tabDetails.Click += tabDetails_Click;
            // 
            // tabSpecs
            // 
            tabSpecs.Location = new Point(4, 24);
            tabSpecs.Name = "tabSpecs";
            tabSpecs.Padding = new Padding(3);
            tabSpecs.Size = new Size(792, 422);
            tabSpecs.TabIndex = 1;
            tabSpecs.Text = "Specifications";
            tabSpecs.UseVisualStyleBackColor = true;
            // 
            // tabPricing
            // 
            tabPricing.Location = new Point(4, 24);
            tabPricing.Name = "tabPricing";
            tabPricing.Padding = new Padding(3);
            tabPricing.Size = new Size(792, 422);
            tabPricing.TabIndex = 2;
            tabPricing.Text = "Pricing & Dates";
            tabPricing.UseVisualStyleBackColor = true;
            tabPricing.Click += tabPricing_Click;
            // 
            // tabMedia
            // 
            tabMedia.Location = new Point(4, 24);
            tabMedia.Name = "tabMedia";
            tabMedia.Padding = new Padding(3);
            tabMedia.Size = new Size(792, 422);
            tabMedia.TabIndex = 3;
            tabMedia.Text = "Photos";
            tabMedia.UseVisualStyleBackColor = true;
            // 
            // Menuform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(tabMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Menuform";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vehicle Manager";
            Load += Menuform_Load;
            tabMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabDetails;
        private TabPage tabSpecs;
        private TabPage tabPricing;
        private TabPage tabMedia;
    }
}