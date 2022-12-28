using ChessGame.board;
namespace ChessGame.board.Chess
{
    public class King : Piece
    {
        private ChessMatch _match;
        public King(Colors color, Board board, ChessMatch match) : base(color, board)
        {
            _match = match;
        }
        private bool TowerCastle(Position position)
        {
            Piece p = board.ReturnPiece(position);
            return p != null && p.color == color && p is Tower && p.numberOfMoves == 0;
        }
        public override bool[,] PossibleMovies()
        {
            bool[,] matrix = new bool[board.lines, board.columns];
            // Only instance one position
            Position pos = new Position(0, 0);
            // Above
            pos.DefineValues(position.line - 1, position.column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // North east
            pos.DefineValues(position.line - 1, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // Rigth
            pos.DefineValues(position.line, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // South east
            pos.DefineValues(position.line + 1, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // Down 
            pos.DefineValues(position.line + 1, position.column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // South west
            pos.DefineValues(position.line + 1, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // Left
            pos.DefineValues(position.line, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // North West
            pos.DefineValues(position.line - 1, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                matrix[pos.line, pos.column] = true;
            }
            // #EspecialMoviment Castle
            if(numberOfMoves==0 && !_match.xeque)
            {
                // Small Castle
                Position positionTower1 = new Position(position.line, position.column + 3);
                if(TowerCastle(positionTower1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);

                    if(board.ReturnPiece(p1) == null && board.ReturnPiece(p2) == null)
                    {
                        matrix[position.line, position.column + 2] = true;
                    }
                }
                // Big Castle

                Position positionTower2 = new Position(position.line, position.column - 4);
                if(TowerCastle(positionTower2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);

                    if(board.ReturnPiece(p1) == null && board.ReturnPiece(p2) == null && board.ReturnPiece(p3) == null)
                    {
                        matrix[position.line, position.column - 2] = true;
                    }
                }

            }

            return matrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}