using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Figura
    {
        public Figura(Types type, Teams team, int row, int col)
        {
            Type = type;
            Team = team;
            Fmove = true;
            Row = row;
            Col = col;
        }
        public Types Type { get; set; }
        public Teams Team { get; set; }
        public bool Fmove { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        /// <summary>
        /// Проверка возможных ходов без учёта шахов
        /// </summary>
        /// <param name="Deck"></param>
        /// <returns></returns>
        public List<int[]> GetMoves(Figura[,] Deck, List<MoveHistory> history)
        {
            List<int[]> moves = new List<int[]>();
            switch (Type)
            {
                case Types.King:
                    {
                        moves.Add(new int[] { Row, Col + 1 });
                        moves.Add(new int[] { Row, Col - 1 });
                        moves.Add(new int[] { Row + 1, Col });
                        moves.Add(new int[] { Row - 1, Col });
                        moves.Add(new int[] { Row + 1, Col + 1 });
                        moves.Add(new int[] { Row + 1, Col - 1 });
                        moves.Add(new int[] { Row - 1, Col + 1 });
                        moves.Add(new int[] { Row - 1, Col - 1 });
                        //Рокировка
                        if (Fmove && (Row == 0 || Row == 7) && Col == 4)
                        {
                            if (Deck[Row, Col + 1] == null
                                && Deck[Row, Col + 3] != null
                                && Deck[Row, Col + 3].Fmove)
                                moves.Add(new int[] { Row, Col + 2 });

                            if (Deck[Row, Col - 1] == null
                                && Deck[Row, Col - 3] == null
                                && Deck[Row, Col - 4] != null
                                && Deck[Row, Col - 4].Fmove)
                                moves.Add(new int[] { Row, Col - 2 });
                        }
                        break;
                    }
                case Types.Queen:
                case Types.Rook:
                    {
                        int c = Col + 1;
                        while (c < 8 && Deck[Row, c] == null)//Вправо
                            moves.Add(new int[] { Row, c++ });
                        if  (c < 8 && Deck[Row, c].Team != Team)
                            moves.Add(new int[] { Row, c });

                        c = Col - 1;
                        while (c >= 0 && Deck[Row, c] == null)//Влево
                            moves.Add(new int[] { Row, c-- });
                        if (c >= 0 && Deck[Row, c].Team != Team)
                            moves.Add(new int[] { Row, c });

                        int r = Row + 1;
                        while (r < 8 && Deck[r, Col] == null)//Вверх
                            moves.Add(new int[] { r++, Col });
                        if (r < 8 && Deck[r, Col].Team != Team)
                            moves.Add(new int[] { r, Col });

                        r = Row - 1;
                        while (r >= 0 && Deck[r, Col] == null)//вниз
                            moves.Add(new int[] { r--, Col });
                        if (r >= 0 && Deck[r, Col].Team != Team)
                            moves.Add(new int[] { r, Col });

                        if (Type == Types.Queen)
                            goto case Types.Bishop;
                        break;
                    }
                case Types.Bishop:
                    {
                        int c = Col + 1;
                        int r = Row + 1;
                        while (c < 8 && r < 8 && Deck[r, c] == null)//1 четверть
                            moves.Add(new int[] { r++, c++ });
                        if (r < 8 && c < 8 && Deck[r, c].Team != Team)
                            moves.Add(new int[] { r, c });

                        c = Col + 1;
                        r = Row - 1;
                        while (c < 8 && r >= 0 && Deck[r, c] == null)//2 четверть
                            moves.Add(new int[] { r--, c++ });
                        if (r >= 0 && c < 8 && Deck[r, c].Team != Team)
                            moves.Add(new int[] { r, c });

                        c = Col - 1;
                        r = Row - 1;
                        while (c >= 0 && r >= 0 && Deck[r, c] == null)//3 четверть
                            moves.Add(new int[] { r--, c-- });
                        if (r >= 0 && c >= 0 && Deck[r, c].Team != Team)
                            moves.Add(new int[] { r, c });

                        c = Col - 1;
                        r = Row + 1;
                        while (c >= 0 && r < 8 && Deck[r, c] == null)//4 четверть
                            moves.Add(new int[] { r++, c-- });
                        if (r < 8 && c >= 0 && c < 8 && Deck[r, c].Team != Team)
                            moves.Add(new int[] { r, c });
                        break;
                    }
                case Types.Knight:
                    {
                        moves.Add(new int[] { Row + 2, Col + 1 });
                        moves.Add(new int[] { Row + 1, Col + 2 });
                        moves.Add(new int[] { Row + 2, Col - 1 });
                        moves.Add(new int[] { Row + 1, Col - 2 });
                        moves.Add(new int[] { Row - 2, Col + 1 });
                        moves.Add(new int[] { Row - 1, Col + 2 });
                        moves.Add(new int[] { Row - 2, Col - 1 });
                        moves.Add(new int[] { Row - 1, Col - 2 });
                        break;
                    }
                case Types.Pawn:
                    {
                        int r = 1;
                        if (Team == Teams.Black)
                            r = -1;
                        if (Deck[Row + r, Col] == null)
                            moves.Add(new int[] { Row + r, Col });

                        MoveHistory last = null;
                        if (history.Count > 0)
                            last = history[history.Count - 1];

                        bool cross = last != null
                            && last.Type == Type
                            && (last.OldPos[0] == 6 || last.OldPos[0] == 1)
                            && last.NewPos[0] == Row;
                        if (
                            Col < 7
                            && ((Deck[Row + r, Col + 1] != null && Deck[Row + r, Col + 1].Team != Team)
                            || cross && last.NewPos[1] == Col + 1))
                            moves.Add(new int[] { Row + r, Col + 1 });

                        if (Col > 0
                            && ((Deck[Row + r, Col - 1] != null && Deck[Row + r, Col - 1].Team != Team)
                            || cross && last.NewPos[1] == Col - 1))
                            moves.Add(new int[] { Row + r, Col - 1 });

                        if (Fmove && Deck[Row + r, Col] == null && Deck[Row + 2 * r, Col] == null)
                            moves.Add(new int[] { Row + 2 * r, Col });
                        break;
                    }
            }
            if (Type == Types.Knight || Type == Types.King)
            {
                for (int i = moves.Count - 1; i >= 0; i--)
                    if (moves[i][0] < 0 || moves[i][1] < 0 || moves[i][0] > 7 || moves[i][1] > 7)
                        moves.RemoveAt(i);
                for (int i = moves.Count - 1; i >= 0; i--)
                    if (Deck[moves[i][0], moves[i][1]] != null && Deck[moves[i][0], moves[i][1]].Team == Team)
                        moves.RemoveAt(i);
            }
            return moves;
        }
        public enum Types
        {
            Rook,
            Knight,
            Bishop,
            Queen,
            King,
            Pawn,
        }
        public enum Teams
        {
            Black,
            White
        }
    }
}
