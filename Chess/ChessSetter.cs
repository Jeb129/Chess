using System;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessSetter : Form
    {
        public Figura.Types Chosed { get; set; }
        public ChessSetter(Figura.Teams team)
        {
            InitializeComponent();
            Chosed = Figura.Types.Queen;
            PutChess(team);
        }

        private void Rook_Click(object sender, EventArgs e)
        {
            int f = Int32.Parse((sender as Button).Name[1].ToString());
            Chosed = (Figura.Types)f;
            Close();
        }
        void PutChess(Figura.Teams team)
        {
            if (team == Figura.Teams.White)
            {
                t0.BackgroundImage = Properties.Resources.WhiteRook;
                t1.BackgroundImage = Properties.Resources.WhiteKnight;
                t2.BackgroundImage = Properties.Resources.WhiteBishop;
                t3.BackgroundImage = Properties.Resources.WhiteQueen;
            }
            else
            {
                t0.BackgroundImage = Properties.Resources.BlackRook;
                t1.BackgroundImage = Properties.Resources.BlackKnight;
                t2.BackgroundImage = Properties.Resources.BlackBishop;
                t3.BackgroundImage = Properties.Resources.BlackQueen;
            }
            Refresh();
        }
    }
}
