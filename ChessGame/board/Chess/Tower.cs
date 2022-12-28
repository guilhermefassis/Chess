using ChessGame.board;
namespace ChessGame.board.Chess
{
    public class Tower : Piece
    {
         public Tower(Colors color, Board board) : base(color, board)
        {
        }

        public override bool[,] PossibleMovies()
        {
            bool[,] matrix = new bool[board.lines, board.columns];
            // Only instance one position
            Position pos = new Position(0, 0);
            // vertical moves
            pos.DefineValues(position.line - 1, position.column);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }
            pos.DefineValues(position.line + 1 , position.column);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }
            // horizontal moves
            pos.DefineValues(position.line , position.column + 1);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.column = pos.column + 1;
            }
            pos.DefineValues(position.line , position.column - 1);
            while(board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
                if (board.ReturnPiece(pos) != null && board.ReturnPiece(pos).color != this.color)
                {
                    break;
                }
                pos.column = pos.column - 1;
            }
            return matrix;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}