namespace Checkers
{
    partial class ChooseGameForm
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
            this.tcSelectGame = new System.Windows.Forms.TabControl();
            this.tabSelfGame = new System.Windows.Forms.TabPage();
            this.tabComputerGame = new System.Windows.Forms.TabPage();
            this.tabNetGame = new System.Windows.Forms.TabPage();
            this.lvRecentGames = new System.Windows.Forms.ListView();
            this.chUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbGamerName = new System.Windows.Forms.TextBox();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnRecentGames = new System.Windows.Forms.Button();
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcSelectGame.SuspendLayout();
            this.tabNetGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSelectGame
            // 
            this.tcSelectGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSelectGame.Controls.Add(this.tabNetGame);
            this.tcSelectGame.Controls.Add(this.tabComputerGame);
            this.tcSelectGame.Controls.Add(this.tabSelfGame);
            this.tcSelectGame.Location = new System.Drawing.Point(12, 12);
            this.tcSelectGame.Name = "tcSelectGame";
            this.tcSelectGame.SelectedIndex = 0;
            this.tcSelectGame.Size = new System.Drawing.Size(314, 212);
            this.tcSelectGame.TabIndex = 0;
            this.tcSelectGame.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcSelectGame_Selected);
            // 
            // tabSelfGame
            // 
            this.tabSelfGame.Location = new System.Drawing.Point(4, 24);
            this.tabSelfGame.Name = "tabSelfGame";
            this.tabSelfGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabSelfGame.Size = new System.Drawing.Size(306, 184);
            this.tabSelfGame.TabIndex = 0;
            this.tabSelfGame.Text = "Тестовый режим";
            this.tabSelfGame.UseVisualStyleBackColor = true;
            // 
            // tabComputerGame
            // 
            this.tabComputerGame.Location = new System.Drawing.Point(4, 24);
            this.tabComputerGame.Name = "tabComputerGame";
            this.tabComputerGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabComputerGame.Size = new System.Drawing.Size(306, 184);
            this.tabComputerGame.TabIndex = 1;
            this.tabComputerGame.Text = "С компьютером";
            this.tabComputerGame.UseVisualStyleBackColor = true;
            // 
            // tabNetGame
            // 
            this.tabNetGame.Controls.Add(this.lvRecentGames);
            this.tabNetGame.Controls.Add(this.tbGamerName);
            this.tabNetGame.Controls.Add(this.btnJoinGame);
            this.tabNetGame.Controls.Add(this.btnRecentGames);
            this.tabNetGame.Controls.Add(this.btnCreateGame);
            this.tabNetGame.Controls.Add(this.label2);
            this.tabNetGame.Controls.Add(this.label1);
            this.tabNetGame.Location = new System.Drawing.Point(4, 24);
            this.tabNetGame.Name = "tabNetGame";
            this.tabNetGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetGame.Size = new System.Drawing.Size(306, 184);
            this.tabNetGame.TabIndex = 2;
            this.tabNetGame.Text = "По сети";
            this.tabNetGame.UseVisualStyleBackColor = true;
            // 
            // lvRecentGames
            // 
            this.lvRecentGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUser});
            this.lvRecentGames.Location = new System.Drawing.Point(9, 22);
            this.lvRecentGames.MultiSelect = false;
            this.lvRecentGames.Name = "lvRecentGames";
            this.lvRecentGames.Size = new System.Drawing.Size(170, 112);
            this.lvRecentGames.TabIndex = 5;
            this.lvRecentGames.UseCompatibleStateImageBehavior = false;
            this.lvRecentGames.View = System.Windows.Forms.View.Details;
            this.lvRecentGames.SelectedIndexChanged += new System.EventHandler(this.lvRecentGames_SelectedIndexChanged);
            // 
            // chUser
            // 
            this.chUser.Text = "Пользователь";
            this.chUser.Width = 150;
            // 
            // tbGamerName
            // 
            this.tbGamerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGamerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbGamerName.Location = new System.Drawing.Point(6, 155);
            this.tbGamerName.Name = "tbGamerName";
            this.tbGamerName.Size = new System.Drawing.Size(173, 23);
            this.tbGamerName.TabIndex = 2;
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJoinGame.Enabled = false;
            this.btnJoinGame.Location = new System.Drawing.Point(195, 150);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(99, 25);
            this.btnJoinGame.TabIndex = 4;
            this.btnJoinGame.Text = "Подключиться";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnRecentGames
            // 
            this.btnRecentGames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecentGames.Location = new System.Drawing.Point(194, 22);
            this.btnRecentGames.Name = "btnRecentGames";
            this.btnRecentGames.Size = new System.Drawing.Size(99, 25);
            this.btnRecentGames.TabIndex = 1;
            this.btnRecentGames.Text = "Обновить";
            this.btnRecentGames.UseVisualStyleBackColor = true;
            this.btnRecentGames.Click += new System.EventHandler(this.btnRecentGames_Click);
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateGame.Location = new System.Drawing.Point(195, 119);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(99, 25);
            this.btnCreateGame.TabIndex = 3;
            this.btnCreateGame.Text = "Создать";
            this.btnCreateGame.UseVisualStyleBackColor = true;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя игрока:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Игры для подключения:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(166, 235);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(247, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ChooseGameForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(338, 271);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tcSelectGame);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseGameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая игра";
            this.tcSelectGame.ResumeLayout(false);
            this.tabNetGame.ResumeLayout(false);
            this.tabNetGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSelectGame;
        private System.Windows.Forms.TabPage tabSelfGame;
        private System.Windows.Forms.TabPage tabComputerGame;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabNetGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.TextBox tbGamerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRecentGames;
        private System.Windows.Forms.ListView lvRecentGames;
        private System.Windows.Forms.ColumnHeader chUser;
    }
}