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
            this.mainTools = new System.Windows.Forms.ToolStrip();
            this.tsbNewGame = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveGame = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openGameDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelfGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAutoGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNetGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectNetGameEnemy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateSelfNetGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTunings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRules = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLog = new System.Windows.Forms.Panel();
            this.lvLog = new System.Windows.Forms.ListView();
            this.chStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWhite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBlack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.lbBlackScore = new System.Windows.Forms.Label();
            this.lbWhiteScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatus.SuspendLayout();
            this.mainTools.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.panelLog.SuspendLayout();
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
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.tsslDateTime});
            this.mainStatus.Location = new System.Drawing.Point(0, 396);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.mainStatus.Size = new System.Drawing.Size(506, 24);
            this.mainStatus.SizingGrip = false;
            this.mainStatus.TabIndex = 7;
            this.mainStatus.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(359, 19);
            this.status.Spring = true;
            this.status.Text = "Готово";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainTools
            // 
            this.mainTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewGame,
            this.tsbSaveGame,
            this.toolStripSeparator1});
            this.mainTools.Location = new System.Drawing.Point(0, 24);
            this.mainTools.Name = "mainTools";
            this.mainTools.Size = new System.Drawing.Size(506, 25);
            this.mainTools.TabIndex = 8;
            this.mainTools.Text = "toolStrip1";
            // 
            // tsbNewGame
            // 
            this.tsbNewGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewGame.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewGame.Image")));
            this.tsbNewGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewGame.Name = "tsbNewGame";
            this.tsbNewGame.Size = new System.Drawing.Size(23, 22);
            this.tsbNewGame.Text = "&Новая игра";
            // 
            // tsbSaveGame
            // 
            this.tsbSaveGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveGame.Enabled = false;
            this.tsbSaveGame.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveGame.Image")));
            this.tsbSaveGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveGame.Name = "tsbSaveGame";
            this.tsbSaveGame.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveGame.Text = "&Сохранить";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Enabled = false;
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(162, 22);
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
            this.toolStripSeparator5.Size = new System.Drawing.Size(159, 6);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGame,
            this.tsmiTools,
            this.tsmiHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(506, 24);
            this.mainMenu.TabIndex = 6;
            this.mainMenu.Text = "menuStrip1";
            // 
            // tsmiGame
            // 
            this.tsmiGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewGame,
            this.tsmiOpenGame,
            this.toolStripSeparator,
            this.tsmiSaveGame,
            this.toolStripSeparator2,
            this.tsmiExit});
            this.tsmiGame.Name = "tsmiGame";
            this.tsmiGame.Size = new System.Drawing.Size(46, 20);
            this.tsmiGame.Text = "&Игра";
            // 
            // tsmiNewGame
            // 
            this.tsmiNewGame.Image = ((System.Drawing.Image)(resources.GetObject("tsmiNewGame.Image")));
            this.tsmiNewGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiNewGame.Name = "tsmiNewGame";
            this.tsmiNewGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNewGame.Size = new System.Drawing.Size(172, 22);
            this.tsmiNewGame.Text = "&Новая";
            // 
            // tsmiOpenGame
            // 
            this.tsmiOpenGame.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenGame.Image")));
            this.tsmiOpenGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiOpenGame.Name = "tsmiOpenGame";
            this.tsmiOpenGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpenGame.Size = new System.Drawing.Size(172, 22);
            this.tsmiOpenGame.Text = "&Открыть";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmiSaveGame
            // 
            this.tsmiSaveGame.Enabled = false;
            this.tsmiSaveGame.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSaveGame.Image")));
            this.tsmiSaveGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiSaveGame.Name = "tsmiSaveGame";
            this.tsmiSaveGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSaveGame.Size = new System.Drawing.Size(172, 22);
            this.tsmiSaveGame.Text = "&Сохранить";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(172, 22);
            this.tsmiExit.Text = "Вы&ход";
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelfGame,
            this.tsmiAutoGame,
            this.tsmiNetGame,
            this.toolStripMenuItem1,
            this.tsmiTunings});
            this.tsmiTools.Name = "tsmiTools";
            this.tsmiTools.Size = new System.Drawing.Size(79, 20);
            this.tsmiTools.Text = "&Настройки";
            // 
            // tsmiSelfGame
            // 
            this.tsmiSelfGame.Name = "tsmiSelfGame";
            this.tsmiSelfGame.Size = new System.Drawing.Size(192, 22);
            this.tsmiSelfGame.Text = "Игра с самим собой";
            // 
            // tsmiAutoGame
            // 
            this.tsmiAutoGame.Name = "tsmiAutoGame";
            this.tsmiAutoGame.Size = new System.Drawing.Size(192, 22);
            this.tsmiAutoGame.Text = "Игра с компьютером";
            // 
            // tsmiNetGame
            // 
            this.tsmiNetGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectNetGameEnemy,
            this.tsmiCreateSelfNetGame});
            this.tsmiNetGame.Name = "tsmiNetGame";
            this.tsmiNetGame.Size = new System.Drawing.Size(192, 22);
            this.tsmiNetGame.Text = "Игра по сети";
            // 
            // tsmiSelectNetGameEnemy
            // 
            this.tsmiSelectNetGameEnemy.Name = "tsmiSelectNetGameEnemy";
            this.tsmiSelectNetGameEnemy.Size = new System.Drawing.Size(198, 22);
            this.tsmiSelectNetGameEnemy.Text = "Выбрать противника...";
            // 
            // tsmiCreateSelfNetGame
            // 
            this.tsmiCreateSelfNetGame.Name = "tsmiCreateSelfNetGame";
            this.tsmiCreateSelfNetGame.Size = new System.Drawing.Size(198, 22);
            this.tsmiCreateSelfNetGame.Text = "Создать свою игру...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // tsmiTunings
            // 
            this.tsmiTunings.Enabled = false;
            this.tsmiTunings.Name = "tsmiTunings";
            this.tsmiTunings.Size = new System.Drawing.Size(192, 22);
            this.tsmiTunings.Text = "&Параметры...";
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
            this.tsmiRules.Size = new System.Drawing.Size(162, 22);
            this.tsmiRules.Text = "&Правила игры...";
            // 
            // panelLog
            // 
            this.panelLog.BackColor = System.Drawing.Color.DarkGray;
            this.panelLog.Controls.Add(this.lvLog);
            this.panelLog.Controls.Add(this.label2);
            this.panelLog.Controls.Add(this.lbBlackScore);
            this.panelLog.Controls.Add(this.lbWhiteScore);
            this.panelLog.Controls.Add(this.label1);
            this.panelLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLog.Location = new System.Drawing.Point(269, 49);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(237, 347);
            this.panelLog.TabIndex = 10;
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
            this.lvLog.FullRowSelect = true;
            this.lvLog.GridLines = true;
            this.lvLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLog.HideSelection = false;
            this.lvLog.Location = new System.Drawing.Point(8, 74);
            this.lvLog.MultiSelect = false;
            this.lvLog.Name = "lvLog";
            this.lvLog.ShowGroups = false;
            this.lvLog.ShowItemToolTips = true;
            this.lvLog.Size = new System.Drawing.Size(221, 265);
            this.lvLog.TabIndex = 1;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            this.lvLog.VirtualMode = true;
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
            // tsslDateTime
            // 
            this.tsslDateTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslDateTime.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.tsslDateTime.Name = "tsslDateTime";
            this.tsslDateTime.Size = new System.Drawing.Size(130, 19);
            this.tsslDateTime.Text = "Нет связи с сервером";
            // 
            // CheckersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 420);
            this.Controls.Add(this.panelLog);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainTools);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CheckersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Шашки";
            this.Load += new System.EventHandler(this.CheckersForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CheckersForm_Paint);
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            this.mainTools.ResumeLayout(false);
            this.mainTools.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.panelLog.ResumeLayout(false);
            this.panelLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.SaveFileDialog saveGameDialog;
        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStrip mainTools;
        private System.Windows.Forms.ToolStripButton tsbNewGame;
        private System.Windows.Forms.ToolStripButton tsbSaveGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.OpenFileDialog openGameDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelfGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutoGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiNetGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectNetGameEnemy;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateSelfNetGame;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiTunings;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiRules;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader chStep;
        private System.Windows.Forms.ColumnHeader chWhite;
        private System.Windows.Forms.ColumnHeader chBlack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbBlackScore;
        private System.Windows.Forms.Label lbWhiteScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel tsslDateTime;
    }
}

