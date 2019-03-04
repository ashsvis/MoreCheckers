using Checkers.CheckersServiceReference;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Checkers
{
    public partial class ChooseGameForm : Form
    {
        private Guid gameGuid;

        public ChooseGameForm(Guid gameGuid)
        {
            InitializeComponent();
            this.gameGuid = gameGuid;
        }

        private void ChooseGameForm_Load(object sender, EventArgs e)
        {
            GetActiveGames();
        }

        public Guid OpponentGameGuid { get { return gameGuid; } }

        public PlayMode PlayMode { get; set; } = PlayMode.NetGame;

        public string PlayerName { get => tbGamerName.Text; set { tbGamerName.Text = value; } }

        private void btnRecentGames_Click(object sender, EventArgs e)
        {           
            GetActiveGames();
        }

        private async void GetActiveGames()
        {
            var games = await Client.GetActiveGamesAsync();
            btnJoinGame.Enabled = false;
            lvRecentGames.Items.Clear();
            foreach (var game in games.Where(item => item != gameGuid))
            {
                var status = await Client.GetGameStatusAsync(game);
                if (!status.Exists || status.WinPlayer != WinPlayer.Wait) continue;
                var lvi = new ListViewItem(status.PlayerName) { Tag = game };
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, status.Player.ToString()) { Tag = status.Player });
                lvRecentGames.Items.Add(lvi);
            }
        }

        private void tcSelectGame_Selected(object sender, TabControlEventArgs e)
        {
            if (tcSelectGame.SelectedTab == tabNetGame)
            {
                GetActiveGames();
                PlayMode = PlayMode.NetGame;
            }
            else if (tcSelectGame.SelectedTab == tabComputerGame)
                PlayMode = PlayMode.Game;
            else
                PlayMode = PlayMode.TestGame;
        }

        private void lvRecentGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lview = (ListView)sender;
            btnJoinGame.Enabled = tbGamerName.Text.Trim().Length > 0 && lview.SelectedItems.Count > 0;
        }

        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            var lview = lvRecentGames;
            if (lview.SelectedItems.Count == 0) return;
            gameGuid = (Guid)lview.SelectedItems[0].Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbGamerName_TextChanged(object sender, EventArgs e)
        {
            btnCreateGame.Enabled = tbGamerName.Text.Trim().Length > 0;
        }
    }
}
