﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
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
        }
        #region Графика
        private readonly Button[,] ButtonDeck = new Button[8, 8]; //Визуальная доска из кнопок
        SoundPlayer Sound = new SoundPlayer(Properties.Resources.MoveSound);
        bool SoundOnOff = true;
        /// <summary>
        /// Отрисовка полей на доске
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void PaintChess(int row, int col)
        {
            Figura chess = GameDeck[row, col];
            if (chess == null)
                ButtonDeck[row, col].BackgroundImage = null;
            else
            {
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
            }
            ButtonDeck[row, col].Refresh();

        }
        /// <summary>
        /// подсвечивает выбранную фигуру и её возможные ходы
        /// </summary>
        private void ShowMoves()
        {
            ButtonDeck[SelectF.Row, SelectF.Col].BackColor = Color.Khaki;
            foreach (int[] m in Smoves)
                ButtonDeck[m[0], m[1]].BackColor = Color.DarkSeaGreen;
        }
        /// <summary>
        /// Убирает подсветку ходов
        /// </summary>
        private void HideMoves()
        {
            foreach (Button a in ButtonDeck)
                if (a.Tag.ToString() == "B")
                    a.BackColor = Color.Peru;
                else
                    a.BackColor = Color.PeachPuff;
        }
        /// <summary>
        /// Подсвечивает короля, которому объявлен шах
        /// </summary>
        private void CheckShow()
        {
            if (!Check)
                return;
            int[] kpos = KingFind(GameDeck, (White2move ? Teams.White : Teams.Black));
            ButtonDeck[kpos[0], kpos[1]].BackColor = Color.Red;
        }
        /// <summary>
        /// Отображает таймеры
        /// </summary>
        private void TimeShow()
        {
            WhiteTimer.Text = TimeFormat(WhiteT);
            BlackTimer.Text = TimeFormat(BlackT);
        }
        #endregion
        #region Игровой процесс
        private uint MoveCount = 0; //Счётчик ходов
        private uint DrawCount = 0; //Ходы без взятий и движения пешек
        private bool Check = false; //Шах на доске
        private bool Playing = false; //Идёт игра
        private bool White2move = true; //Проверка хода белых
        private readonly Figura[,] GameDeck = new Figura[8, 8];//Логическая доска из фигур     
        public List<DeckHistory> History = new List<DeckHistory>(); //История состояний доски.

        private Figura SelectF; //Информация о выбранной фигуре
        private List<int[]> Smoves = new List<int[]>(); //Ходы выбранной фигуры
        /// <summary>
        /// Ставит новую фигуру на выбранную позицию
        /// </summary>
        /// <param name="Deck"></param>
        /// <param name="type"></param>
        /// <param name="team"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="Paint">отрисовка положения на игровом поле</param>
        private void PutChess(Figura[,] Deck, Types type, Teams team, int row, int col, bool Paint = true)
        {
            Figura chess = new Figura(type, team, row, col);
            if (Deck[row, col] != null && team == Deck[row, col].Team)
                return;
            Deck[row, col] = chess;
            if (Paint)
                PaintChess(row, col);
        }
        /// <summary>
        /// Удаляет фигуру с позиции
        /// </summary>
        /// <param name="Deck"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="Paint">отрисовка</param>
        private void DelChess(Figura[,] Deck, int row, int col, bool Paint = true)
        {
            Deck[row, col] = null;
            if (Paint)
                PaintChess(row, col);
        }
        /// <summary>
        /// Совершает ход согласно правилам
        /// </summary>
        /// <param name="Deck"></param>
        /// <param name="select"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <param name="Paint">Отрисовка</param>
        private void Moving(Figura[,] Deck, Figura select, int r, int c, bool Paint = true)
        {
            //считаем ходы без взятия или движения пешки
            byte d = 1;
            if (select.Type == Types.Pawn || Deck[r, c] != null)
                d--;
            //Взятие на проходе
            if (select.Type == Types.Pawn && select.Col != c && Deck[r, c] == null)
                DelChess(Deck, select.Row, c, Paint);
            //Превращение
            if (Paint && select.Type == Types.Pawn && (r == 0 || r == 7))
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
                    PutChess(Deck, Types.Rook, select.Team, r, 5);
                    DelChess(Deck, r, 7);
                }
                if (c == 2)
                {
                    PutChess(Deck, Types.Rook, select.Team, r, 3);
                    DelChess(Deck, r, 0);
                }
            }

            PutChess(Deck, select.Type, select.Team, r, c, Paint);
            DelChess(Deck, select.Row, select.Col, Paint);

            if (Paint)
            {
                Move move = new Move(++MoveCount, select.Team, select.Type, new int[] { select.Row, select.Col }, new int[] { r, c });
                Deck[r, c].Fmove = false;
                MovePaint(move, d);
            }
        }
        /// <summary>
        /// Действия при отрисовке хода
        /// </summary>
        /// <param name="move"></param>
        /// <param name="d"></param>
        private void MovePaint(Move move, byte d)
        {
            if (SoundOnOff)
                Sound.Play();
            DrawCount = d * ++DrawCount;
            HistoryBox.Items.Add(move);
            History.Add(new DeckHistory(move, DeckCopy(GameDeck)));
            TimerAdd();
            White2move = !White2move;
        }
        /// <summary>
        /// Удаляет из списка возможных все ходы, приводящие к шаху
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        private List<int[]> CheckRemove(Figura select)
        {
            List<int[]> moves = select.GetMoves(GameDeck, History[History.Count - 1].Last);
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
        /// <summary>
        /// Проверяет, находится ли выбранное поле под боем вражеских фигур
        /// </summary>
        /// <param name="Deck"></param>
        /// <param name="team"></param>
        /// <param name="Krow"></param>
        /// <param name="Kcol"></param>
        /// <returns></returns>
        private bool CheckChecker(Figura[,] Deck, Teams team, int Krow, int Kcol)
        {
            foreach (Figura Ch in Deck)
                if (Ch != null && Ch.Team != team)
                {
                    List<int[]> Premove = Ch.GetMoves(Deck, History[History.Count - 1].Last);
                    foreach (int[] m2 in Premove)
                        if (Krow == m2[0] && Kcol == m2[1])
                            return true;
                }
            return false;
        }
        #endregion
        #region Игра по времени
        private bool GameTime = false; //Включен ли таймер в партии
        private int WhiteT = 0; //Таймер белых
        private int BlackT = 0;//Таймер чёрных
        private int Increment = 0;//Прибавка после хода
        /// <summary>
        /// Установка таймера
        /// </summary>
        private void TimerSetup()
        {
            TimeSetter ts = new TimeSetter();
            ts.ShowDialog();
            if (ts.Timer <= 0) return;
            WhiteT = ts.Timer;
            BlackT = WhiteT;
            Increment = ts.Increment;
            TimeShow();
        }
        /// <summary>
        /// Добавление времени после хода
        /// </summary>
        private void TimerAdd()
        {
            GameTimer.Stop();
            if (White2move)
                WhiteT += Increment;
            else
                BlackT += Increment;
            GameTimer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!GameTime) return;
            if (White2move)
                WhiteT--;
            else
                BlackT--;
            TimeShow();
            if (Playing && (WhiteT <= 0 || BlackT <= 0))
                EndCheck(White2move ? Teams.White : Teams.Black);
        }

        #endregion
        #region Проверка состояния доски
        /// <summary>
        /// проверяет условия окончания партии
        /// </summary>
        /// <param name="team"></param>
        private void EndCheck(Teams team)
        {
            if (!(CanCheckmate(Teams.White) || CanCheckmate(Teams.Black)) ||
                ThreeRepeats() || DrawCount >= 50)
            {
                GameTime = false;
                Playing = false;
                MessageBox.Show("Ничья");
            }
            if (NoMoves(team))
            {
                GameTime = false;
                Playing = false;
                MessageBox.Show(Check ? "Шах и мат. Победа " + (White2move ? "чёрных" : "белых") : "Пат");
            }
            if (!GameTime) return;
            if (WhiteT <= 0 || BlackT <= 0)
            {
                //При истечении времени, если другая сторона не может объявить мат королю, объявляется ничья
                string TimeResult = CanCheckmate(White2move ? Teams.Black : Teams.White) ?
                    "Победа " + (White2move ? "чёрных" : "белых") : "Ничья";
                GameTime = false;
                Playing = false;
                MessageBox.Show("Время белых истекло. " + TimeResult);
            }
        }
        /// <summary>
        /// У стороны нет возможных ходо
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        private bool NoMoves(Teams team)
        {
            int[] Kpos = KingFind(GameDeck, team);
            if (Kpos.Length == 0)
            {
                MessageBox.Show("ЗА ИМПЕРАТОРА!!!!");
                Application.Exit();
            }
            Check = CheckChecker(GameDeck, team, Kpos[0], Kpos[1]);
            int mC = 0;
            foreach (Figura Ch in GameDeck)
                if (Ch != null && Ch.Team == team)
                {
                    List<int[]> moves = CheckRemove(Ch);
                    mC += moves.Count;
                }
            return mC == 0;
        }
        /// <summary>
        /// Может ли сторона поставить мат
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        private bool CanCheckmate(Teams team)
        {
            List<Figura> t = new List<Figura>();
            List<Figura> e = new List<Figura>();
            foreach (Figura Ch in GameDeck)
                if (Ch != null && Ch.Type != Types.King)
                    if(Ch.Team == team)
                        t.Add(Ch);
                    else
                        e.Add(Ch);
            return 
                t.Count > 0 && e.Count >0 ||
                t.Count >1 ||
                t.Count == 1 && !(t[0].Type == Types.Knight || t[0].Type == Types.Bishop);
        }
        /// <summary>
        /// Троекратное повторение позиции
        /// </summary>
        /// <returns></returns>
        private bool ThreeRepeats()
        {
            int r = 0;
            for (int i = History.Count - 2; i >= 0; i--)
                if (History[i] == History[History.Count - 1])
                    r++;
            return r >= 2;
        }
        #endregion
        #region Вспомогательные методы
        /// <summary>
        /// Ищет положение короля на доске
        /// </summary>
        /// <param name="Deck"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        private int[] KingFind(Figura[,] Deck, Teams team)
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
        /// <summary>
        /// Копирует доску с фигурами
        /// </summary>
        /// <param name="Old"></param>
        /// <returns></returns>
        private Figura[,] DeckCopy(Figura[,] Old)
        {
            Figura[,] New = new Figura[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    New[i, j] = Old[i, j];
            return New;
        }
        /// <summary>
        /// Очищает доску
        /// </summary>
        private void ClearDeck()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    DelChess(GameDeck, i, j);
            HideMoves();
        }
        /// <summary>
        /// Стандартная расстановка фигур
        /// </summary>
        private void StartDeck()
        {
            //Ставим пешки
            for (int i = 0; i < 8; i++)
            {
                PutChess(GameDeck, Types.Pawn, Teams.White, 1, i);
                PutChess(GameDeck, Types.Pawn, Teams.Black, 6, i);
            }
            //Ставим фигуры слева от короля и короля
            for (int i = 0; i < 5; i++)
            {
                PutChess(GameDeck, (Types)i, Teams.White, 0, i);
                PutChess(GameDeck, (Types)i, Teams.Black, 7, i);
            }
            //Фигуры справа
            for (int i = 0; i < 4; i++)
            {
                PutChess(GameDeck, (Types)(3 - i), Teams.White, 0, i + 4);
                PutChess(GameDeck, (Types)(3 - i), Teams.Black, 7, i + 4);
            }
        }
        /// <summary>
        /// создаёт строку с отображением времени
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string TimeFormat(int time)
        {
            if (time < 0)
                return "";

            int h = time / 3600;
            int m = (time % 3600) / 60;
            int s = ((time % 3600) % 60) % 60;

            if (h >= 1)
                return $"{h} : {m:00} : {s:00}";
            else
                return $"{m} : {s:00}";
        }
        /// <summary>
        /// Является ли выбранная клетка одним из ходов фигуры
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool IsPossible(int c, int r)
        {
            bool Possible = false;
            for (int i = 0; i < Smoves.Count && !Possible; i++)
                if (r == Smoves[i][0] && c == Smoves[i][1])
                    Possible = true;
            return Possible;
        }
        /// <summary>
        /// Действия при клике на непустое поле
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        private void ChessSelect(int r, int c)
        {
            if (GameDeck[r, c] != null && GameDeck[r, c].Team == (Teams)1 != White2move)
                return;
            if (!GameTime && WhiteT > 0)
            {
                GameTime = true;
                GameTimer.Start();
            }
            SelectF = GameDeck[r, c];
            Smoves = CheckRemove(SelectF);
            ShowMoves();
        }
        #endregion
        #region Обработчики
        /// <summary>
        /// клик по полю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deck_Click(object sender, EventArgs e)
        {
            if (!Playing)
            {
                RestartClick(sender, e);
                return;
            }
            if (!(sender is Button cur)) return;
            //получаем координаты кнопки
            int c = cur.Name[0] - 'a';
            int r = int.Parse(cur.Name[1].ToString()) - 1;
            //если клик поппустому полю при отсутствии выбраной игуры
            if (GameDeck[r, c] == null && SelectF == null)
                return;
            HideMoves();
            if (SelectF != null && (GameDeck[r, c] == null || 
                GameDeck[r, c] != null && GameDeck[r, c].Team != SelectF.Team))
            {
                if (!IsPossible(c, r))
                {
                    SelectF= null;
                    Smoves.Clear();
                    return;
                }

                    Moving(GameDeck, SelectF, r, c);
                EndCheck((Teams)(White2move ? 1 : 0));
                SelectF = null;
            }
            else
                ChessSelect(r, c);
            CheckShow();
            if(GameTime)
                TimeShow();
        }
        /// <summary>
        /// Рестарт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartClick(object sender, EventArgs e)
        {
            SelectF = null;
            White2move = true;
            Playing = true;
            Check = false;
            GameTime = false;
            WhiteT = 0;
            BlackT = 0;
            Increment = 0;
            MoveCount = 0;
            WhiteTimer.Text = "";
            BlackTimer.Text = "";
            HistoryBox.Items.Clear();
            History = new List<DeckHistory>() { new DeckHistory(null, DeckCopy(GameDeck)) };

            TimerSetup();
            ClearDeck();
            StartDeck();
            //StartDeck();
        }
        /// <summary>
        /// Переключение звука
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundButton_Click(object sender, EventArgs e)
        {
            SoundOnOff = !SoundOnOff;
            soundButton.Text = SoundOnOff ? "🔊" : "🔇";
        }
        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void AboutButton_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }
        #endregion
    }
    public class Move
    {
        public Move(uint num, Teams team, Types type, int[] oldpos, int[] newpos)
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
            string Old = $"{(char)(OldPos[1] + 'a')}{OldPos[0] + 1}";
            string New = $"{(char)(NewPos[1] + 'a')}{NewPos[0] + 1}";
            return $"{Num}. " + Old + " - " + New;
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
            if (!(obj is DeckHistory h))
                return false;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Deck[i, j] != h.Deck[i, j])
                        return false;
                    else
                    if (Deck[i, j] != null &&
                        (Deck[i, j].Type == Types.Pawn || Deck[i, j].Type == Types.King)
                        && Last != null)
                    {
                        List<int[]> m1 = Deck[i, j].GetMoves(Deck, Last);
                        List<int[]> m2 = h.Deck[i, j].GetMoves(h.Deck, h.Last);
                        if (m1.Count != m2.Count)
                            return false;
                        for (int i2 = 0; i2 < m1.Count; i2++)
                            if (m1[i2][0] != m2[i2][0] ||
                                m1[i2][1] != m2[i2][1])
                                return false;
                    }
                }
            return true;
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
            return left.Equals(right);
        }
        public static bool operator !=(DeckHistory left, DeckHistory right)
        {
            return !(left == right);
        }
    }
}