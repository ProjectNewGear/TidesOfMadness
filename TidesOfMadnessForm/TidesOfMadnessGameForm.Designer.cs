namespace TidesOfMadnessForm
{
    partial class TidesOfMadnessForm
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
            this.lbxOppInPlay = new System.Windows.Forms.ListBox();
            this.lbxHumanInPlay = new System.Windows.Forms.ListBox();
            this.lbxHumanHand = new System.Windows.Forms.ListBox();
            this.lblPlayerInstructions = new System.Windows.Forms.Label();
            this.cbxPlayerChoice = new System.Windows.Forms.ComboBox();
            this.pbxCardImage = new System.Windows.Forms.PictureBox();
            this.txtGameLog = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblOpponentName = new System.Windows.Forms.Label();
            this.lblOppMadnessTotal = new System.Windows.Forms.Label();
            this.lblOppMadnessThisRound = new System.Windows.Forms.Label();
            this.lblOppPointsTotal = new System.Windows.Forms.Label();
            this.lblHumanLabel = new System.Windows.Forms.Label();
            this.lblPlayerPointsTotal = new System.Windows.Forms.Label();
            this.lblPlayerMadnessThisRound = new System.Windows.Forms.Label();
            this.lblPlayerMadnessTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._tempGameStateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCardImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxOppInPlay
            // 
            this.lbxOppInPlay.FormattingEnabled = true;
            this.lbxOppInPlay.Location = new System.Drawing.Point(12, 51);
            this.lbxOppInPlay.Name = "lbxOppInPlay";
            this.lbxOppInPlay.Size = new System.Drawing.Size(319, 95);
            this.lbxOppInPlay.TabIndex = 0;
            this.lbxOppInPlay.SelectedIndexChanged += new System.EventHandler(this.lbxOppInPlay_SelectedIndexChanged);
            // 
            // lbxHumanInPlay
            // 
            this.lbxHumanInPlay.FormattingEnabled = true;
            this.lbxHumanInPlay.Location = new System.Drawing.Point(12, 243);
            this.lbxHumanInPlay.Name = "lbxHumanInPlay";
            this.lbxHumanInPlay.Size = new System.Drawing.Size(319, 95);
            this.lbxHumanInPlay.TabIndex = 1;
            this.lbxHumanInPlay.SelectedIndexChanged += new System.EventHandler(this.lbxHumanInPlay_SelectedIndexChanged);
            // 
            // lbxHumanHand
            // 
            this.lbxHumanHand.FormattingEnabled = true;
            this.lbxHumanHand.Location = new System.Drawing.Point(12, 431);
            this.lbxHumanHand.Name = "lbxHumanHand";
            this.lbxHumanHand.Size = new System.Drawing.Size(319, 95);
            this.lbxHumanHand.TabIndex = 2;
            this.lbxHumanHand.SelectedIndexChanged += new System.EventHandler(this.lbxHumanHand_SelectedIndexChanged);
            // 
            // lblPlayerInstructions
            // 
            this.lblPlayerInstructions.AutoSize = true;
            this.lblPlayerInstructions.Font = new System.Drawing.Font("Percolator Expert", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerInstructions.Location = new System.Drawing.Point(22, 625);
            this.lblPlayerInstructions.Name = "lblPlayerInstructions";
            this.lblPlayerInstructions.Size = new System.Drawing.Size(579, 38);
            this.lblPlayerInstructions.TabIndex = 3;
            this.lblPlayerInstructions.Text = "Instructions for the player go here.";
            // 
            // cbxPlayerChoice
            // 
            this.cbxPlayerChoice.Font = new System.Drawing.Font("Percolator Expert", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPlayerChoice.FormattingEnabled = true;
            this.cbxPlayerChoice.Location = new System.Drawing.Point(12, 773);
            this.cbxPlayerChoice.Name = "cbxPlayerChoice";
            this.cbxPlayerChoice.Size = new System.Drawing.Size(319, 25);
            this.cbxPlayerChoice.TabIndex = 4;
            // 
            // pbxCardImage
            // 
            this.pbxCardImage.Location = new System.Drawing.Point(1004, 51);
            this.pbxCardImage.Name = "pbxCardImage";
            this.pbxCardImage.Size = new System.Drawing.Size(400, 225);
            this.pbxCardImage.TabIndex = 5;
            this.pbxCardImage.TabStop = false;
            // 
            // txtGameLog
            // 
            this.txtGameLog.Location = new System.Drawing.Point(1004, 327);
            this.txtGameLog.Multiline = true;
            this.txtGameLog.Name = "txtGameLog";
            this.txtGameLog.ReadOnly = true;
            this.txtGameLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGameLog.Size = new System.Drawing.Size(400, 336);
            this.txtGameLog.TabIndex = 6;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Percolator Expert", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(342, 747);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(108, 51);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblOpponentName
            // 
            this.lblOpponentName.AutoSize = true;
            this.lblOpponentName.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpponentName.Location = new System.Drawing.Point(7, 19);
            this.lblOpponentName.Name = "lblOpponentName";
            this.lblOpponentName.Size = new System.Drawing.Size(324, 29);
            this.lblOpponentName.TabIndex = 8;
            this.lblOpponentName.Text = "Opponent - Cards In Play";
            // 
            // lblOppMadnessTotal
            // 
            this.lblOppMadnessTotal.AutoSize = true;
            this.lblOppMadnessTotal.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOppMadnessTotal.Location = new System.Drawing.Point(337, 80);
            this.lblOppMadnessTotal.Name = "lblOppMadnessTotal";
            this.lblOppMadnessTotal.Size = new System.Drawing.Size(199, 29);
            this.lblOppMadnessTotal.TabIndex = 9;
            this.lblOppMadnessTotal.Text = "Madness Total:";
            // 
            // lblOppMadnessThisRound
            // 
            this.lblOppMadnessThisRound.AutoSize = true;
            this.lblOppMadnessThisRound.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOppMadnessThisRound.Location = new System.Drawing.Point(337, 109);
            this.lblOppMadnessThisRound.Name = "lblOppMadnessThisRound";
            this.lblOppMadnessThisRound.Size = new System.Drawing.Size(269, 29);
            this.lblOppMadnessThisRound.TabIndex = 10;
            this.lblOppMadnessThisRound.Text = "Madness This Round:";
            // 
            // lblOppPointsTotal
            // 
            this.lblOppPointsTotal.AutoSize = true;
            this.lblOppPointsTotal.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOppPointsTotal.Location = new System.Drawing.Point(337, 51);
            this.lblOppPointsTotal.Name = "lblOppPointsTotal";
            this.lblOppPointsTotal.Size = new System.Drawing.Size(178, 29);
            this.lblOppPointsTotal.TabIndex = 11;
            this.lblOppPointsTotal.Text = "Points Total:";
            // 
            // lblHumanLabel
            // 
            this.lblHumanLabel.AutoSize = true;
            this.lblHumanLabel.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHumanLabel.Location = new System.Drawing.Point(7, 211);
            this.lblHumanLabel.Name = "lblHumanLabel";
            this.lblHumanLabel.Size = new System.Drawing.Size(291, 29);
            this.lblHumanLabel.TabIndex = 12;
            this.lblHumanLabel.Text = "Player - Cards In Play";
            // 
            // lblPlayerPointsTotal
            // 
            this.lblPlayerPointsTotal.AutoSize = true;
            this.lblPlayerPointsTotal.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerPointsTotal.Location = new System.Drawing.Point(337, 243);
            this.lblPlayerPointsTotal.Name = "lblPlayerPointsTotal";
            this.lblPlayerPointsTotal.Size = new System.Drawing.Size(178, 29);
            this.lblPlayerPointsTotal.TabIndex = 15;
            this.lblPlayerPointsTotal.Text = "Points Total:";
            // 
            // lblPlayerMadnessThisRound
            // 
            this.lblPlayerMadnessThisRound.AutoSize = true;
            this.lblPlayerMadnessThisRound.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerMadnessThisRound.Location = new System.Drawing.Point(337, 301);
            this.lblPlayerMadnessThisRound.Name = "lblPlayerMadnessThisRound";
            this.lblPlayerMadnessThisRound.Size = new System.Drawing.Size(269, 29);
            this.lblPlayerMadnessThisRound.TabIndex = 14;
            this.lblPlayerMadnessThisRound.Text = "Madness This Round:";
            // 
            // lblPlayerMadnessTotal
            // 
            this.lblPlayerMadnessTotal.AutoSize = true;
            this.lblPlayerMadnessTotal.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerMadnessTotal.Location = new System.Drawing.Point(337, 272);
            this.lblPlayerMadnessTotal.Name = "lblPlayerMadnessTotal";
            this.lblPlayerMadnessTotal.Size = new System.Drawing.Size(199, 29);
            this.lblPlayerMadnessTotal.TabIndex = 13;
            this.lblPlayerMadnessTotal.Text = "Madness Total:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 29);
            this.label1.TabIndex = 16;
            this.label1.Text = "Cards in Hand";
            // 
            // _tempGameStateLabel
            // 
            this._tempGameStateLabel.AutoSize = true;
            this._tempGameStateLabel.Font = new System.Drawing.Font("Percolator Expert", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tempGameStateLabel.Location = new System.Drawing.Point(480, 769);
            this._tempGameStateLabel.Name = "_tempGameStateLabel";
            this._tempGameStateLabel.Size = new System.Drawing.Size(189, 29);
            this._tempGameStateLabel.TabIndex = 17;
            this._tempGameStateLabel.Text = "Gamestate = X";
            // 
            // TidesOfMadnessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 824);
            this.Controls.Add(this._tempGameStateLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPlayerPointsTotal);
            this.Controls.Add(this.lblPlayerMadnessThisRound);
            this.Controls.Add(this.lblPlayerMadnessTotal);
            this.Controls.Add(this.lblHumanLabel);
            this.Controls.Add(this.lblOppPointsTotal);
            this.Controls.Add(this.lblOppMadnessThisRound);
            this.Controls.Add(this.lblOppMadnessTotal);
            this.Controls.Add(this.lblOpponentName);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtGameLog);
            this.Controls.Add(this.pbxCardImage);
            this.Controls.Add(this.cbxPlayerChoice);
            this.Controls.Add(this.lblPlayerInstructions);
            this.Controls.Add(this.lbxHumanHand);
            this.Controls.Add(this.lbxHumanInPlay);
            this.Controls.Add(this.lbxOppInPlay);
            this.Name = "TidesOfMadnessForm";
            this.Text = "Tides of Madness";
            this.Load += new System.EventHandler(this.TidesOfMadnessForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxCardImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxOppInPlay;
        private System.Windows.Forms.ListBox lbxHumanInPlay;
        private System.Windows.Forms.ListBox lbxHumanHand;
        private System.Windows.Forms.Label lblPlayerInstructions;
        private System.Windows.Forms.ComboBox cbxPlayerChoice;
        private System.Windows.Forms.PictureBox pbxCardImage;
        private System.Windows.Forms.TextBox txtGameLog;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblOpponentName;
        private System.Windows.Forms.Label lblOppMadnessTotal;
        private System.Windows.Forms.Label lblOppMadnessThisRound;
        private System.Windows.Forms.Label lblOppPointsTotal;
        private System.Windows.Forms.Label lblHumanLabel;
        private System.Windows.Forms.Label lblPlayerPointsTotal;
        private System.Windows.Forms.Label lblPlayerMadnessThisRound;
        private System.Windows.Forms.Label lblPlayerMadnessTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _tempGameStateLabel;
    }
}

