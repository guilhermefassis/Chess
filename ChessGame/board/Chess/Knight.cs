namespace ChessGame.board.Chess
{
    public class Knight : Piece
    {
        public Knight(Colors color, Board board) : base(color, board)
        {
        }

        public override bool[,] PossibleMovies()
        {
            bool[,] matrix = new bool[board.lines, board.columns];
            // Only instance one position
            Position pos = new Position(0, 0);
            pos.DefineValues(position.line - 1, position.column + 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            pos.DefineValues(position.line + 1, position.column + 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            pos.DefineValues(position.line - 1, position.column - 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            pos.DefineValues(position.line + 1, position.column - 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }

            pos.DefineValues(position.line + 2, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            pos.DefineValues(position.line + 2, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            pos.DefineValues(position.line - 2, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            pos.DefineValues(position.line - 2, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            return matrix;
        }

        public override string ToString()
        {
            return "H";
        }
    }
}