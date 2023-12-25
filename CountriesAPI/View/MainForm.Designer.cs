namespace CountriesAPI
{
   internal partial class MainForm
   {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
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
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         CountriesListBox = new ComboBox();
         CoaPictureBox = new PictureBox();
         CountryListBoxLabel = new Label();
         FlagPictureBox = new PictureBox();
         ContinentSelectorLabel = new Label();
         RegionsCheckedListBox = new CheckedListBox();
         StatusStrip = new StatusStrip();
         ToolStripProgressBar = new ToolStripProgressBar();
         ToolStripLabel = new ToolStripStatusLabel();
         CountryDataGridView = new DataGridView();
         ((System.ComponentModel.ISupportInitialize)CoaPictureBox).BeginInit();
         ((System.ComponentModel.ISupportInitialize)FlagPictureBox).BeginInit();
         StatusStrip.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)CountryDataGridView).BeginInit();
         SuspendLayout();
         // 
         // CountriesListBox
         // 
         CountriesListBox.DropDownStyle = ComboBoxStyle.DropDownList;
         CountriesListBox.FlatStyle = FlatStyle.System;
         CountriesListBox.FormattingEnabled = true;
         CountriesListBox.Location = new Point(12, 229);
         CountriesListBox.Name = "CountriesListBox";
         CountriesListBox.Size = new Size(190, 25);
         CountriesListBox.TabIndex = 1;
         CountriesListBox.SelectedIndexChanged += CountriesListBox_SelectedIndexChanged;
         // 
         // CoaPictureBox
         // 
         CoaPictureBox.Location = new Point(660, 237);
         CoaPictureBox.Name = "CoaPictureBox";
         CoaPictureBox.Size = new Size(190, 190);
         CoaPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
         CoaPictureBox.TabIndex = 3;
         CoaPictureBox.TabStop = false;
         // 
         // CountryListBoxLabel
         // 
         CountryListBoxLabel.AutoSize = true;
         CountryListBoxLabel.Location = new Point(12, 209);
         CountryListBoxLabel.Name = "CountryListBoxLabel";
         CountryListBoxLabel.Size = new Size(103, 17);
         CountryListBoxLabel.TabIndex = 4;
         CountryListBoxLabel.Text = "Select a country:";
         // 
         // FlagPictureBox
         // 
         FlagPictureBox.Location = new Point(660, 34);
         FlagPictureBox.Name = "FlagPictureBox";
         FlagPictureBox.Size = new Size(190, 190);
         FlagPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
         FlagPictureBox.TabIndex = 5;
         FlagPictureBox.TabStop = false;
         // 
         // ContinentSelectorLabel
         // 
         ContinentSelectorLabel.AutoSize = true;
         ContinentSelectorLabel.Location = new Point(12, 14);
         ContinentSelectorLabel.Name = "ContinentSelectorLabel";
         ContinentSelectorLabel.Size = new Size(55, 17);
         ContinentSelectorLabel.TabIndex = 7;
         ContinentSelectorLabel.Text = "Regions";
         // 
         // RegionsCheckedListBox
         // 
         RegionsCheckedListBox.CheckOnClick = true;
         RegionsCheckedListBox.FormattingEnabled = true;
         RegionsCheckedListBox.Location = new Point(12, 34);
         RegionsCheckedListBox.Name = "RegionsCheckedListBox";
         RegionsCheckedListBox.Size = new Size(190, 144);
         RegionsCheckedListBox.TabIndex = 8;
         RegionsCheckedListBox.SelectedIndexChanged += RegionsCheckedListBox_SelectedIndexChanged;
         // 
         // StatusStrip
         // 
         StatusStrip.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
         StatusStrip.Items.AddRange(new ToolStripItem[] { ToolStripProgressBar, ToolStripLabel });
         StatusStrip.Location = new Point(0, 443);
         StatusStrip.Name = "StatusStrip";
         StatusStrip.Size = new Size(862, 24);
         StatusStrip.TabIndex = 9;
         StatusStrip.Text = "Status:";
         // 
         // ToolStripProgressBar
         // 
         ToolStripProgressBar.Name = "ToolStripProgressBar";
         ToolStripProgressBar.Size = new Size(100, 18);
         ToolStripProgressBar.Style = ProgressBarStyle.Marquee;
         // 
         // ToolStripLabel
         // 
         ToolStripLabel.Name = "ToolStripLabel";
         ToolStripLabel.Size = new Size(127, 19);
         ToolStripLabel.Text = "ToolStripStatusLabel";
         // 
         // CountryDataGridView
         // 
         CountryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         CountryDataGridView.Location = new Point(208, 34);
         CountryDataGridView.Name = "CountryDataGridView";
         CountryDataGridView.RowTemplate.Height = 25;
         CountryDataGridView.Size = new Size(443, 393);
         CountryDataGridView.TabIndex = 10;
         CountryDataGridView.CellClick += CountryDataGridView_CellClick;
         // 
         // MainForm
         // 
         AutoScaleDimensions = new SizeF(7F, 17F);
         AutoScaleMode = AutoScaleMode.Font;
         BackColor = Color.GhostWhite;
         ClientSize = new Size(862, 467);
         Controls.Add(CountryDataGridView);
         Controls.Add(StatusStrip);
         Controls.Add(RegionsCheckedListBox);
         Controls.Add(ContinentSelectorLabel);
         Controls.Add(FlagPictureBox);
         Controls.Add(CountryListBoxLabel);
         Controls.Add(CoaPictureBox);
         Controls.Add(CountriesListBox);
         Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
         FormBorderStyle = FormBorderStyle.FixedSingle;
         MaximizeBox = false;
         MinimizeBox = false;
         Name = "MainForm";
         Opacity = 0.99D;
         RightToLeft = RightToLeft.No;
         ShowIcon = false;
         SizeGripStyle = SizeGripStyle.Hide;
         StartPosition = FormStartPosition.CenterScreen;
         Text = "Countries";
         Load += MainForm_Load;
         ((System.ComponentModel.ISupportInitialize)CoaPictureBox).EndInit();
         ((System.ComponentModel.ISupportInitialize)FlagPictureBox).EndInit();
         StatusStrip.ResumeLayout(false);
         StatusStrip.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)CountryDataGridView).EndInit();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion
      private ComboBox CountriesListBox;
      private PictureBox CoaPictureBox;
      private Label CountryListBoxLabel;
      private PictureBox FlagPictureBox;
      private Label ContinentSelectorLabel;
      private CheckedListBox RegionsCheckedListBox;
      private StatusStrip StatusStrip;
      private ToolStripProgressBar ToolStripProgressBar;
      private ToolStripStatusLabel ToolStripLabel;
      private DataGridView CountryDataGridView;
   }
}
