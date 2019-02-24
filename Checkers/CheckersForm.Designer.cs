namespace Checkers
{
    partial class CheckersForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckersForm));
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.saveGameDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslIpAddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openGameDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiServerIpAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRules = new System.Windows.Forms.ToolStripMenuItem();
            this.panelGame = new System.Windows.Forms.Panel();
            this.lvLog = new System.Windows.Forms.ListView();
            this.chStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWhite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBlack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.lbBlackScore = new System.Windows.Forms.Label();
            this.lbWhiteScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tsmiEndGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowGamePanel = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatus.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.panelGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // saveGameDialog
            // 
            this.saveGameDialog.DefaultExt = "che";
            this.saveGameDialog.Filter = "*.che|*.che";
            this.saveGameDialog.Title = "Сохранить игру";
            // 
            // mainStatus
            // 
            this.mainStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.tsslIpAddress,
            this.tsslDateTime});
            this.mainStatus.Location = new System.Drawing.Point(0, 449);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.mainStatus.Size = new System.Drawing.Size(442, 24);
            this.mainStatus.SizingGrip = false;
            this.mainStatus.TabIndex = 7;
            this.mainStatus.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(242, 19);
            this.status.Spring = true;
            this.status.Text = "Инициализация...";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslIpAddress
            // 
            this.tsslIpAddress.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslIpAddress.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.tsslIpAddress.Name = "tsslIpAddress";
            this.tsslIpAddress.Size = new System.Drawing.Size(59, 19);
            this.tsslIpAddress.Text = "localhost";
            // 
            // tsslDateTime
            // 
            this.tsslDateTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslDateTime.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.tsslDateTime.Name = "tsslDateTime";
            this.tsslDateTime.Size = new System.Drawing.Size(130, 19);
            this.tsslDateTime.Text = "Нет связи с сервером";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Enabled = false;
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(180, 22);
            this.tsmiAbout.Text = "&О программе...";
            // 
            // openGameDialog
            // 
            this.openGameDialog.DefaultExt = "che";
            this.openGameDialog.Filter = "*.che|*.che";
            this.openGameDialog.Title = "Загрузить игру";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGame,
            this.tsmiView,
            this.tsmiHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(442, 24);
            this.mainMenu.TabIndex = 6;
            this.mainMenu.Text = "menuStrip1";
            // 
            // tsmiGame
            // 
            this.tsmiGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewGame,
            this.tsmiEndGame,
            this.toolStripSeparator2,
            this.tsmiExit});
            this.tsmiGame.Name = "tsmiGame";
            this.tsmiGame.Size = new System.Drawing.Size(46, 20);
            this.tsmiGame.Text = "&Игра";
            this.tsmiGame.DropDownOpening += new System.EventHandler(this.tsmiGame_DropDownOpening);
            // 
            // tsmiNewGame
            // 
            this.tsmiNewGame.Image = ((System.Drawing.Image)(resources.GetObject("tsmiNewGame.Image")));
            this.tsmiNewGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiNewGame.Name = "tsmiNewGame";
            this.tsmiNewGame.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiNewGame.Size = new System.Drawing.Size(180, 22);
            this.tsmiNewGame.Text = "&Новая...";
            this.tsmiNewGame.Click += new System.EventHandler(this.tsmiNewGame_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(180, 22);
            this.tsmiExit.Text = "Вы&ход";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiView
            // 
            this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowGamePanel,
            this.toolStripMenuItem1,
            this.tsmiServerIpAddress});
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(39, 20);
            this.tsmiView.Text = "Вид";
            // 
            // tsmiServerIpAddress
            // 
            this.tsmiServerIpAddress.Name = "tsmiServerIpAddress";
            this.tsmiServerIpAddress.Size = new System.Drawing.Size(188, 22);
            this.tsmiServerIpAddress.Text = "Адрес cервера...";
            this.tsmiServerIpAddress.Click += new System.EventHandler(this.tsmiServerIpAddress_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRules,
            this.toolStripSeparator5,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(65, 20);
            this.tsmiHelp.Text = "Спра&вка";
            // 
            // tsmiRules
            // 
            this.tsmiRules.Enabled = false;
            this.tsmiRules.Name = "tsmiRules";
            this.tsmiRules.Size = new System.Drawing.Size(180, 22);
            this.tsmiRules.Text = "&Правила игры...";
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.Color.LightGray;
            this.panelGame.Controls.Add(this.lvLog);
            this.panelGame.Controls.Add(this.label2);
            this.panelGame.Controls.Add(this.lbBlackScore);
            this.panelGame.Controls.Add(this.lbWhiteScore);
            this.panelGame.Controls.Add(this.label1);
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGame.Location = new System.Drawing.Point(205, 24);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(237, 425);
            this.panelGame.TabIndex = 10;
            this.panelGame.Visible = false;
            // 
            // lvLog
            // 
            this.lvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chStep,
            this.chWhite,
            this.chBlack});
            this.lvLog.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvLog.FullRowSelect = true;
            this.lvLog.GridLines = true;
            this.lvLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLog.HideSelection = false;
            this.lvLog.Location = new System.Drawing.Point(8, 74);
            this.lvLog.MultiSelect = false;
            this.lvLog.Name = "lvLog";
            this.lvLog.ShowGroups = false;
            this.lvLog.ShowItemToolTips = true;
            this.lvLog.Size = new System.Drawing.Size(221, 343);
            this.lvLog.TabIndex = 1;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            this.lvLog.VirtualMode = true;
            this.lvLog.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvLog_RetrieveVirtualItem);
            this.lvLog.SelectedIndexChanged += new System.EventHandler(this.lvLog_SelectedIndexChanged);
            // 
            // chStep
            // 
            this.chStep.Text = "Ход";
            this.chStep.Width = 40;
            // 
            // chWhite
            // 
            this.chWhite.Text = "Белые";
            this.chWhite.Width = 80;
            // 
            // chBlack
            // 
            this.chBlack.Text = "Чёрные";
            this.chBlack.Width = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Счёт:";
            // 
            // lbBlackScore
            // 
            this.lbBlackScore.BackColor = System.Drawing.Color.Black;
            this.lbBlackScore.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbBlackScore.ForeColor = System.Drawing.Color.White;
            this.lbBlackScore.Location = new System.Drawing.Point(121, 25);
            this.lbBlackScore.Name = "lbBlackScore";
            this.lbBlackScore.Size = new System.Drawing.Size(89, 25);
            this.lbBlackScore.TabIndex = 2;
            this.lbBlackScore.Text = "Чёрные: 0";
            this.lbBlackScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbWhiteScore
            // 
            this.lbWhiteScore.BackColor = System.Drawing.Color.White;
            this.lbWhiteScore.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbWhiteScore.Location = new System.Drawing.Point(31, 25);
            this.lbWhiteScore.Name = "lbWhiteScore";
            this.lbWhiteScore.Size = new System.Drawing.Size(89, 25);
            this.lbWhiteScore.TabIndex = 2;
            this.lbWhiteScore.Text = "Белые: 0";
            this.lbWhiteScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Партия:";
            // 
            // tsmiEndGame
            // 
            this.tsmiEndGame.Enabled = false;
            this.tsmiEndGame.Name = "tsmiEndGame";
            this.tsmiEndGame.Size = new System.Drawing.Size(180, 22);
            this.tsmiEndGame.Text = "Завершить";
            this.tsmiEndGame.Click += new System.EventHandler(this.tsmiEndGame_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 6);
            // 
            // tsmiShowGamePanel
            // 
            this.tsmiShowGamePanel.CheckOnClick = true;
            this.tsmiShowGamePanel.Name = "tsmiShowGamePanel";
            this.tsmiShowGamePanel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.tsmiShowGamePanel.Size = new System.Drawing.Size(188, 22);
            this.tsmiShowGamePanel.Text = "Панель игры";
            this.tsmiShowGamePanel.Click += new System.EventHandler(this.tsmiShowGamePanel_Click);
            // 
            // CheckersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(442, 473);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CheckersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Шашки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckersForm_FormClosing);
            this.Load += new System.EventHandler(this.CheckersForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CheckersForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckersForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CheckersForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CheckersForm_MouseUp);
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.panelGame.ResumeLayout(false);
            this.panelGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.SaveFileDialog saveGameDialog;
        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.OpenFileDialog openGameDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiRules;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader chStep;
        private System.Windows.Forms.ColumnHeader chWhite;
        private System.Windows.Forms.ColumnHeader chBlack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbBlackScore;
        private System.Windows.Forms.Label lbWhiteScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel tsslDateTime;
        private System.Windows.Forms.ToolStripStatusLabel tsslIpAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiServerIpAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiEndGame;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowGamePanel;
    }
}

