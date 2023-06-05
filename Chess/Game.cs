using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Chess.Figura;

namespace Chess
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            ButtonDeck = new Button[,] 
            {
            { a1, b1, c1, d1, e1, f1, g1, h1 },
            { a2, b2, c2, d2, e2, f2, g2, h2 },
            { a3, b3, c3, d3, e3, f3, g3, h3 },
            { a4, b4, c4, d4, e4, f4, g4, h4 },
            { a5, b5, c5, d5, e5, f5, g5, h5 },
            { a6, b6, c6, d6, e6, f6, g6, h6 },
            { a7, b7, c7, d7, e7, f7, g7, h7 },
            { a8, b8, c8, d8, e8, f8, g8, h8 }
            };
            //StartDeck();
            //Рокировка
            PutChess(GameDeck, Types.King, Teams.Black, 7, 4);
            PutChess(GameDeck, Types.King, Teams.White, 0, 4);
            //PutChess(GameDeck, Types.Queen, Teams.Black, 7, 3);
            //PutChess(GameDeck, Types.Queen, Teams.White, 0, 3);
            //PutChess(GameDeck, Types.Rook, Teams.Black, 7, 0);
            //PutChess(GameDeck, Types.Rook, Teams.White, 0, 0);
            //PutChess(GameDeck, Types.Rook, Teams.Black, 7, 7);
            //PutChess(GameDeck, Types.Rook, Teams.White, 0, 7);
            //Превращение
            PutChess(GameDeck, Types.Pawn, Teams.White, 5, 7);
            GameDeck[5, 7].Fmove = false;
            PutChess(GameDeck, Types.Pawn, Teams.Black, 6, 6);
            GameDeck[6, 6].Fmove = true;
        }

        readonly Button[,] ButtonDeck = new Button[8, 8]; //Визуальная доска из кнопок
        readonly static Figura[,] GameDeck = new Figura[8,8];//Логическая доска из фигур

        readonly List<Move> MoveHistory = new List<Move>();
        public List<DeckHistory> History = new List<DeckHistory>() {new DeckHistory(null, DeckCopy(GameDeck)) }; //История ходов.
        bool White2move = true; //Проверка хода белых
        bool Check = false; //Шах на доске
        bool EndGame = false;
        uint MoveCount = 0; //Счётчик ходов
        //Информация о выбранной фигуре
        Figura SelectF; 
        List<int[]> Smoves = new List<int[]>();

        #region Графика
        /// <summary>
        /// Стандартная расстановка фигур
        /// </summary>
        void StartDeck()
        {
            //Ставим пешки
            for (int i = 0; i < 8; i++)
            {
                PutChess(GameDeck,Types.Pawn, Teams.White,1, i);
                PutChess(GameDeck, Types.Pawn, Teams.Black,6, i);
            }
            //Ставим фигуры слева от короля и короля
            for (int i = 0; i < 5; i++)
            {
                PutChess(GameDeck, (Types)i, Teams.White,0, i);
                PutChess(GameDeck, (Types)i, Teams.Black,7, i);
            }
            //Фигуры справа
            for (int i = 0; i < 4; i++)
            {
                PutChess(GameDeck, (Types)(3-i), Teams.White,0, i + 4);
                PutChess(GameDeck, (Types)(3-i), Teams.Black,7, i + 4);
            }
        }
        void PutChess(Figura[,] Deck, Types type, Teams team, int row, int col,bool Paint = true)
        {
            Figura chess = new Figura(type, team, row, col);
            if (Deck[row, col] != null && team == Deck[row, col].Team)
                return;
            Deck[row, col] = chess;
            if (Paint)
                PaintChess(row, col);
        }
        void DelChess(Figura[,]Deck,int row, int col,bool Paint = true)
        {
            Deck[row, col] = null;
            if (Paint)
                PaintChess(row, col);
        }
        /// <summary>
        /// Отрисовка конкретного поля
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void PaintChess(int row, int col)
        {
            Figura chess = GameDeck[row, col];
            if (chess == null)
                ButtonDeck[row, col].BackgroundImage = null;
            else
            if (chess.Team == Teams.White)
                switch (chess.Type)
                {
                    case Types.King:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.WhiteKing;
                        break;
                    case Types.Queen:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.WhiteQueen;
                        break;
                    case Types.Bishop:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.WhiteBishop;
                        break;
                    case Types.Knight:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.WhiteKnight;
                        break;
                    case Types.Rook:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.WhiteRook;
                        break;
                    case Types.Pawn:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.WhitePawn;
                        break;
                }
            else
                switch (chess.Type)
                {
                    case Types.King:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.BlackKing;
                        break;
                    case Types.Queen:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.BlackQueen;
                        break;
                    case Types.Bishop:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.BlackBishop;
                        break;
                    case Types.Knight:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.BlackKnight;
                        break;
                    case Types.Rook:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.BlackRook;
                        break;
                    case Types.Pawn:
                        ButtonDeck[chess.Row, chess.Col].BackgroundImage = Properties.Resources.BlackPawn;
                        break;
                }
            ButtonDeck[row, col].Refresh();
        }
        void ShowMoves()
        {
            ButtonDeck[SelectF.Row, SelectF.Col].BackColor = Color.Khaki;
            foreach (int[] m in Smoves)
                ButtonDeck[m[0], m[1]].BackColor = Color.DarkSeaGreen;
        }
        void HideMoves()
        {
            foreach (Button a in ButtonDeck)
                if (a.Tag.ToString() == "B")
                    a.BackColor = Color.Peru;
                else
                    a.BackColor = Color.PeachPuff;
        }
        void ClearDeck()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    DelChess(GameDeck, i, j);
            HideMoves();
        }
        #endregion
        void Moving(Figura[,] Deck,Figura select, int r, int c, bool Paint = true)
        {
            //Взятие на проходе
            if (select.Type == Types.Pawn && select.Col != c && Deck[r,c]==null)
                DelChess(Deck, select.Row, c, Paint);
            //Превращение
            if (Paint&&select.Type == Types.Pawn && (r == 0 || r == 7))
            {
                ChessSetter chessSetter = new ChessSetter(select.Team);
                chessSetter.ShowDialog();
                select.Type = chessSetter.Chosed;
            }
            //Рокировка
            if (select.Fmove && select.Type == Types.King && (r == 0 || r == 7))
            {
                if (c == 6)
                {
                    PutChess(Deck,Types.Rook, select.Team, r, 5);
                    DelChess(Deck, r, 7);
                }
                if (c == 2)
                {
                    PutChess(Deck, Types.Rook, select.Team, r, 3);
                    DelChess(Deck, r, 0);
                }
            }

            PutChess(Deck,select.Type, select.Team, r, c,Paint);
            DelChess(Deck, select.Row, select.Col, Paint);
            if (Paint)
            {
                Move move = new Move(++MoveCount, select.Team, select.Type, new int[] { select.Row, select.Col }, new int[] { r, c });
                MoveHistory.Add(move);
                HistoryBox.DataSource = null;
                HistoryBox.DataSource = MoveHistory;
                White2move = !White2move;
                Deck[r,c].Fmove = false;
            }
        }
        /// <summary>
        /// Проверка шахов
        /// </summary>
        /// <param name="Ch"></param>
        /// <returns></returns>
        bool CheckChecker(Figura[,] Deck,Teams team,int Krow,int Kcol)
        {
            foreach (Figura Ch in Deck)
                if (Ch != null && Ch.Team != team)
                {
                    List<int[]> Premove = Ch.GetMoves(Deck,History);
                    foreach (int[] m2 in Premove)
                        if (Krow == m2[0] && Kcol == m2[1])
                            return true;
                }
            return false;
        }   
        int MovesCounter(Teams team)
        {
            int[] Kpos = KingFind(GameDeck, team);
            if (Kpos.Length == 0)
            {
                MessageBox.Show("ЗА ИМПЕРАТОРА!!!!");
                Application.Exit();
            }
            Check = CheckChecker(GameDeck, team, Kpos[0], Kpos[1]);
            if (Check)
                ButtonDeck[Kpos[0], Kpos[1]].BackColor = Color.Red;
            int mC = 0;
            foreach (Figura Ch in GameDeck)
                if (Ch != null && Ch.Team == team)
                {
                    List<int[]> moves = CheckRemove(Ch);
                    mC += moves.Count;
                }
            return mC;
        }
        private void EndCheck(Teams team)
        {
            int mC = MovesCounter(team);
            bool NEM = NotEnoughMaterial();
            if (NEM)
            {
                MessageBox.Show("Ничья");
                EndGame = true;
            }
            if (mC == 0)
            {
                MessageBox.Show(Check ? "Шах и мат" : "Пат");
                EndGame = true;
            }
        }
        private bool NotEnoughMaterial()
        {
            List<Figura> f = new List<Figura>();
            foreach (Figura Ch in GameDeck)
                if (Ch != null && Ch.Type != Types.King)
                    f.Add(Ch);
            return f.Count == 0 || f.Count == 1 && (f[0].Type == Types.Knight || f[0].Type == Types.Bishop);
        }
        private List<int[]> CheckRemove(Figura select)
        {
            List<int[]> moves = select.GetMoves(GameDeck,History);
            int c = 0;
            for (int i = moves.Count - 1; i >= 0; i--)
            {
                Figura[,] Deck = DeckCopy(GameDeck);
                Moving(Deck, select, moves[i][0], moves[i][1], false);
                //Рокировка через шах
                if (select.Type == Types.King && select.Fmove && (moves[i][1] == 2 || moves[i][1] == 6))
                    switch (moves[i][1])
                    {
                        case 2: c = 3; goto case 0;
                        case 6: c = 5; goto case 0;
                        case 0:
                            if (Check || CheckChecker(Deck, select.Team, select.Row, c))
                            {
                                moves.RemoveAt(i);
                                continue;
                            }
                            break;
                    }
                int[] Kpos = KingFind(Deck, select.Team);
                if (CheckChecker(Deck, select.Team, Kpos[0], Kpos[1]))
                    moves.RemoveAt(i);
            }
            return moves;
        }
        int[] KingFind(Figura[,] Deck, Teams team)
        {
            int[] Kpos = new int[0];
            foreach (Figura Ch in Deck)
                if (Ch != null && Ch.Team == team && Ch.Type == Types.King)
                {
                    Kpos = new int[2];
                    Kpos[0] = Ch.Row;
                    Kpos[1] = Ch.Col;
                    break;
                }
            return Kpos;
        }
        static Figura[,] DeckCopy(Figura[,] Old)
        {
            Figura[,] New = new Figura[8, 8];
            for (int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                    New[i, j] = Old[i, j];
            return New;
        }
        private void ChessSelect(int r, int c)
        {
            if (GameDeck[r, c] != null && GameDeck[r, c].Team == (Teams)1 != White2move)
                return;
            SelectF = GameDeck[r, c];
            Smoves = CheckRemove(SelectF);
            ShowMoves();
        }
        #region Обработчики
        private void Pole_Click(object sender, EventArgs e)
        {
            if (EndGame)
            {
                RestartClick(sender, e);
                return;
            }
            if(!(sender is Button cur)) return;
            //получаем координаты кнопки
            int c = cur.Name[0] - 'a';
            int r = int.Parse(cur.Name[1].ToString()) - 1;
            //если клик поппустому полю при отсутствии выбраной игуры
            if (GameDeck[r, c] == null && SelectF == null)
                return;
            HideMoves();
            if (SelectF!=null &&(GameDeck[r, c] == null || GameDeck[r, c] != null && GameDeck[r, c].Team != SelectF.Team))
            {
                //Проверяем, существует ли такой ход (Возможно стоит вынести)
                bool Possible = false;
                for (int i = 0; i < Smoves.Count && !Possible; i++)
                    if (r == Smoves[i][0] && c == Smoves[i][1])
                        Possible = true;
                if (!Possible) return;

                Moving(GameDeck, SelectF, r, c);
                EndCheck((Teams)(White2move ? 1 : 0));
                SelectF = null;
            }
            else
                ChessSelect(r, c);
        }
        private void RestartClick(object sender, EventArgs e)
        {
            ClearDeck();
            StartDeck();
            SelectF = null;
            White2move = true;
            EndGame = false;
            MoveCount = 0;
            History.Clear();
            HistoryBox.DataSource = null;
        }
        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
    public class Move
    {
        public Move(uint num, Teams team, Types type, int[] oldpos, int[]newpos)
        {
            Num = num;
            Team = team;
            Type = type;
            OldPos = oldpos;
            NewPos = newpos;
        }
        public uint Num { get; set; }
        public Teams Team { get; set; }
        public Types Type { get; set; }
        public int[] OldPos { get; set; }
        public int[] NewPos { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Move move &&
                   Num == move.Num &&
                   Team == move.Team &&
                   Type == move.Type &&
                   OldPos[0] == move.OldPos[0] &&
                   OldPos[1] == move.OldPos[1] &&
                   NewPos[0] == move.NewPos[0] &&
                   NewPos[1] == move.NewPos[1];
        }

        public override int GetHashCode()
        {
            int hashCode = -2068199779;
            hashCode = hashCode * -1521134295 + Num.GetHashCode();
            hashCode = hashCode * -1521134295 + Team.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(OldPos);
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(NewPos);
            return hashCode;
        }

        public override string ToString()
        {
            string Old = $"{(char)(OldPos[1] + 'a')}{OldPos[0]+1}";
            string New = $"{(char)(NewPos[1] + 'a')}{NewPos[0]+1}";
            return $"{Num}. "+Old+" - "+New;
        }

        public static bool operator ==(Move left, Move right)
        {
            return EqualityComparer<Move>.Default.Equals(left, right);
        }

        public static bool operator !=(Move left, Move right)
        {
            return !(left == right);
        }
    }
    public class DeckHistory
    {
        public DeckHistory(Move last, Figura[,] deck)
        {
            Last = last;
            Deck = deck;
        }
        public Move Last { get; set; }
        public Figura[,] Deck { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is DeckHistory history)) 
                return false;
            bool DeckEqual = true;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (Deck[i, j] != history.Deck[i,j])
                        DeckEqual = false;
            return DeckEqual && Last == history.Last;
        }
        public override int GetHashCode()
        {
            int hashCode = 233958889;
            hashCode = hashCode * -1521134295 + EqualityComparer<Move>.Default.GetHashCode(Last);
            hashCode = hashCode * -1521134295 + EqualityComparer<Figura[,]>.Default.GetHashCode(Deck);
            return hashCode;
        }
        public static bool operator ==(DeckHistory left, DeckHistory right)
        {
            return EqualityComparer<DeckHistory>.Default.Equals(left, right);
        }
        public static bool operator !=(DeckHistory left, DeckHistory right)
        {
            return !(left == right);
        }
    }
}