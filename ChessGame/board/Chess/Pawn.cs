namespace ChessGame.board.Chess
{
    public class Pawn : Piece
    {
        private ChessMatch _match;
        public Pawn(Colors color, Board board, ChessMatch match) : base(color, board)
        {
            _match = match;
        }

        private bool ExistsEnemy(Position position)
        {
            Piece p = board.ReturnPiece(position);
            return p != null && p.color != color;
        }
        private bool Free(Position position)
        {
            return board.ReturnPiece(position) == null;
        }
        public override bool[,] PossibleMovies()
        {
            bool[,] matrix = new bool[board.lines, board.columns];
            Position pos = new Position(0, 0);

            if (color == Colors.White)
            {
                pos.DefineValues(position.line - 1, position.column);
                if(board.ValidPosition(pos) && Free(pos))
                {
                    matrix[pos.line, pos.column] = true;
                }
                pos.DefineValues(position.line - 2, position.column);
                if(board.ValidPosition(pos) && Free(pos) && numberOfMoves == 0)
                {
                    matrix[pos.line, pos.column] = true;
                }
                pos.DefineValues(position.line - 1, position.column -1);
                if(board.ValidPosition(pos) && ExistsEnemy(pos))
                {
                    matrix[pos.line, pos.column] = true;
                }
                pos.DefineValues(position.line - 1, position.column + 1);
                if(board.ValidPosition(pos) && ExistsEnemy(pos))
                {
                    matrix[pos.line, pos.column] = true;
                }

                // #EspecialMoviment
                // EnPassant
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.ValidPosition(left) && ExistsEnemy(left) && board.ReturnPiece(left) == _match.EnPassant)
                    {
                        matrix[left.line - 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.ValidPosition(right) && ExistsEnemy(right) && board.ReturnPiece(right) == _match.EnPassant)
                    {
                        matrix[right.line - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.DefineValues(position.line + 1, position.column);
                if(board.ValidPosition(pos) && Free(pos))
                {
                    matrix[pos.line, pos.column] = true;
                }
                pos.DefineValues(position.line + 2, position.column);
                if(board.ValidPosition(pos) && Free(pos) && numberOfMoves == 0)
                {
                    matrix[pos.line, pos.column] = true;
                }
                pos.DefineValues(position.line + 1, position.column + 1);
                if(board.ValidPosition(pos) && ExistsEnemy(pos))
                {
                    matrix[pos.line, pos.column] = true;
                }
                pos.DefineValues(position.line + 1, position.column - 1);
                if(board.ValidPosition(pos) && ExistsEnemy(pos))
                {
                    matrix[pos.line, pos.column] = true;
                }
                // #EspecialMoviment
                // EnPassant
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.ValidPosition(left) && ExistsEnemy(left) && board.ReturnPiece(left) == _match.EnPassant)
                    {
                        matrix[left.line + 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.ValidPosition(right) && ExistsEnemy(right) && board.ReturnPiece(right) == _match.EnPassant)
                    {
                        matrix[right.line + 1, right.column] = true;
                    }
                }
            }
            return matrix;
        }


        public override string ToString()
        {
            return "P";
        }
    }
}