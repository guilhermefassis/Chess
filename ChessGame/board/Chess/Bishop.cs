namespace ChessGame.board.Chess
{
    public class Bishop : Piece
    {
        public Bishop(Colors color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return  "B";
        }

        public override bool[,] PossibleMovies()
        {
            bool[,] matrix = new bool[board.lines, board.columns];
            Position pos = new Position(0, 0);
            
            // NW
            pos.DefineValues(position.line - 1, position.column - 1);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.DefineValues(pos.line - 1, pos.column - 1);
            }
            // NE
            pos.DefineValues(position.line - 1, position.column + 1);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.DefineValues(pos.line - 1, pos.column + 1);
            }
            // SE
            pos.DefineValues(position.line + 1, position.column + 1);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.DefineValues(pos.line + 1, pos.column + 1);
            }
            // SO
            pos.DefineValues(position.line + 1, position.column - 1);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.DefineValues(pos.line + 1, pos.column - 1);
            }
            
            return matrix;
        }
    }
}