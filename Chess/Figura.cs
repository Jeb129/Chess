using System.Collections.Generic;

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
        public List<int[]> GetMoves(Figura[,] Deck, Move last)
        {
            List<int[]> moves = new List<int[]>();
            switch (Type)
            {
                case Types.King:
                    {
                        GetKingMoves(Deck, moves);
                        break;
                    }
                case Types.Queen:
                case Types.Rook:
                    {
                        GetRockMoves(Deck, moves);

                        if (Type == Types.Queen)
                            goto case Types.Bishop;
                        break;
                    }
                case Types.Bishop:
                    {
                        GetBishopMoves(Deck, moves);
                        break;
                    }
                case Types.Knight:
                    {
                        GetKnightMoves(Deck,moves);
                        break;
                    }
                case Types.Pawn:
                    {
                        GetPawnMoves(Deck, last, moves);
                        break;
                    }
            }
            return moves;
        }
        private void ClearEdge(Figura[,] Deck, List<int[]> moves)
        {
            for (int i = moves.Count - 1; i >= 0; i--)
                if (moves[i][0] < 0 || moves[i][1] < 0 || moves[i][0] > 7 || moves[i][1] > 7)
                    moves.RemoveAt(i);
            for (int i = moves.Count - 1; i >= 0; i--)
                if (Deck[moves[i][0], moves[i][1]] != null && Deck[moves[i][0], moves[i][1]].Team == Team)
                    moves.RemoveAt(i);
        }
        private void GetPawnMoves(Figura[,] Deck, Move last, List<int[]> moves)
        {
            int r = 1;
            if (Team == Teams.Black)
                r = -1;
            if (Deck[Row + r, Col] == null)
                moves.Add(new int[] { Row + r, Col });
            bool cross = 
                last != null
                && last.Type == Type
                && (last.OldPos[0] == 6 || last.OldPos[0] == 1)
                && last.NewPos[0] == Row
                && (last.NewPos[0] == 3 || last.NewPos[0] == 4);

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
        }
        private void GetKnightMoves(Figura[,] Deck, List<int[]> moves)
        {
            moves.Add(new int[] { Row + 2, Col + 1 });
            moves.Add(new int[] { Row + 1, Col + 2 });
            moves.Add(new int[] { Row + 2, Col - 1 });
            moves.Add(new int[] { Row + 1, Col - 2 });
            moves.Add(new int[] { Row - 2, Col + 1 });
            moves.Add(new int[] { Row - 1, Col + 2 });
            moves.Add(new int[] { Row - 2, Col - 1 });
            moves.Add(new int[] { Row - 1, Col - 2 });
            ClearEdge(Deck, moves);
        }
        private void GetBishopMoves(Figura[,] Deck, List<int[]> moves)
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
        }
        private void GetRockMoves(Figura[,] Deck, List<int[]> moves)
        {
            int c = Col + 1;
            while (c < 8 && Deck[Row, c] == null)//Вправо
                moves.Add(new int[] { Row, c++ });
            if (c < 8 && Deck[Row, c].Team != Team)
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
        }
        private void GetKingMoves(Figura[,] Deck, List<int[]> moves)
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
            ClearEdge(Deck, moves);
        }
        public override bool Equals(object obj)
        {
            return obj is Figura figura &&
                   Type == figura.Type &&
                   Team == figura.Team &&
                   Fmove == figura.Fmove &&
                   Row == figura.Row &&
                   Col == figura.Col;
        }

        public override int GetHashCode()
        {
            int hashCode = 1359565429;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Team.GetHashCode();
            hashCode = hashCode * -1521134295 + Fmove.GetHashCode();
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Col.GetHashCode();
            return hashCode;
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
        public static bool operator ==(Figura left, Figura right)
        {
            return EqualityComparer<Figura>.Default.Equals(left, right);
        }

        public static bool operator !=(Figura left, Figura right)
        {
            return !(left == right);
        }
    }
}
