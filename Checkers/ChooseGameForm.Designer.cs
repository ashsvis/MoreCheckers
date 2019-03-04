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
            this.tabNetGame = new System.Windows.Forms.TabPage();
            this.lvRecentGames = new System.Windows.Forms.ListView();
            this.chUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbGamerName = new System.Windows.Forms.TextBox();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnRecentGames = new System.Windows.Forms.Button();
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabComputerGame = new System.Windows.Forms.TabPage();
            this.tabSelfGame = new System.Windows.Forms.TabPage();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbRules = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tcSelectGame.SuspendLayout();
            this.tabNetGame.SuspendLayout();
            this.tabComputerGame.SuspendLayout();
            this.tabSelfGame.SuspendLayout();
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
            this.tcSelectGame.Size = new System.Drawing.Size(356, 255);
            this.tcSelectGame.TabIndex = 0;
            this.tcSelectGame.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcSelectGame_Selected);
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
            this.tabNetGame.Size = new System.Drawing.Size(348, 227);
            this.tabNetGame.TabIndex = 2;
            this.tabNetGame.Text = "По сети";
            this.tabNetGame.UseVisualStyleBackColor = true;
            // 
            // lvRecentGames
            // 
            this.lvRecentGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRecentGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUser,
            this.chSide});
            this.lvRecentGames.FullRowSelect = true;
            this.lvRecentGames.HideSelection = false;
            this.lvRecentGames.Location = new System.Drawing.Point(6, 65);
            this.lvRecentGames.MultiSelect = false;
            this.lvRecentGames.Name = "lvRecentGames";
            this.lvRecentGames.ShowItemToolTips = true;
            this.lvRecentGames.Size = new System.Drawing.Size(225, 128);
            this.lvRecentGames.TabIndex = 1;
            this.lvRecentGames.UseCompatibleStateImageBehavior = false;
            this.lvRecentGames.View = System.Windows.Forms.View.Details;
            this.lvRecentGames.SelectedIndexChanged += new System.EventHandler(this.lvRecentGames_SelectedIndexChanged);
            // 
            // chUser
            // 
            this.chUser.Text = "Пользователь";
            this.chUser.Width = 140;
            // 
            // chSide
            // 
            this.chSide.Text = "Сторона";
            this.chSide.Width = 80;
            // 
            // tbGamerName
            // 
            this.tbGamerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbGamerName.Location = new System.Drawing.Point(6, 21);
            this.tbGamerName.Name = "tbGamerName";
            this.tbGamerName.Size = new System.Drawing.Size(141, 23);
            this.tbGamerName.TabIndex = 0;
            this.tbGamerName.TextChanged += new System.EventHandler(this.tbGamerName_TextChanged);
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJoinGame.Enabled = false;
            this.btnJoinGame.Location = new System.Drawing.Point(237, 65);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(99, 25);
            this.btnJoinGame.TabIndex = 2;
            this.btnJoinGame.Text = "Подключиться";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnRecentGames
            // 
            this.btnRecentGames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecentGames.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRecentGames.Location = new System.Drawing.Point(6, 199);
            this.btnRecentGames.Name = "btnRecentGames";
            this.btnRecentGames.Size = new System.Drawing.Size(72, 22);
            this.btnRecentGames.TabIndex = 4;
            this.btnRecentGames.Text = "Обновить";
            this.btnRecentGames.UseVisualStyleBackColor = true;
            this.btnRecentGames.Click += new System.EventHandler(this.btnRecentGames_Click);
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateGame.Enabled = false;
            this.btnCreateGame.Location = new System.Drawing.Point(237, 96);
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
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя игрока:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Игры для подключения:";
            // 
            // tabComputerGame
            // 
            this.tabComputerGame.Controls.Add(this.textBox1);
            this.tabComputerGame.Location = new System.Drawing.Point(4, 24);
            this.tabComputerGame.Name = "tabComputerGame";
            this.tabComputerGame.Padding = new System.Windows.Forms.Padding(8);
            this.tabComputerGame.Size = new System.Drawing.Size(348, 227);
            this.tabComputerGame.TabIndex = 1;
            this.tabComputerGame.Text = "С компьютером";
            this.tabComputerGame.UseVisualStyleBackColor = true;
            // 
            // tabSelfGame
            // 
            this.tabSelfGame.Controls.Add(this.tbRules);
            this.tabSelfGame.Location = new System.Drawing.Point(4, 24);
            this.tabSelfGame.Name = "tabSelfGame";
            this.tabSelfGame.Padding = new System.Windows.Forms.Padding(8);
            this.tabSelfGame.Size = new System.Drawing.Size(348, 227);
            this.tabSelfGame.TabIndex = 0;
            this.tabSelfGame.Text = "Тестовый режим";
            this.tabSelfGame.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(208, 278);
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
            this.btnCancel.Location = new System.Drawing.Point(289, 278);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbRules
            // 
            this.tbRules.BackColor = System.Drawing.Color.White;
            this.tbRules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRules.Location = new System.Drawing.Point(11, 11);
            this.tbRules.Multiline = true;
            this.tbRules.Name = "tbRules";
            this.tbRules.ReadOnly = true;
            this.tbRules.Size = new System.Drawing.Size(326, 38);
            this.tbRules.TabIndex = 2;
            this.tbRules.Text = "Тестовый режим предназначен для игры с самим собой.\r\nПередвигайте фишки за себя и" +
    " за противника.";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(11, 11);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(326, 38);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Игра с компьютером задумана, но пока не реализована...";
            // 
            // ChooseGameForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 314);
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
            this.Load += new System.EventHandler(this.ChooseGameForm_Load);
            this.tcSelectGame.ResumeLayout(false);
            this.tabNetGame.ResumeLayout(false);
            this.tabNetGame.PerformLayout();
            this.tabComputerGame.ResumeLayout(false);
            this.tabComputerGame.PerformLayout();
            this.tabSelfGame.ResumeLayout(false);
            this.tabSelfGame.PerformLayout();
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
        private System.Windows.Forms.ColumnHeader chSide;
        private System.Windows.Forms.TextBox tbRules;
        private System.Windows.Forms.TextBox textBox1;
    }
}