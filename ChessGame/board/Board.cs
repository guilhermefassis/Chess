using ChessGame.board.Exceptions;

namespace ChessGame.board
{
    public class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines,columns];
        }
        public Piece ReturnPiece(int line, int column)
        {
            return pieces[line, column];
        }
        // Method Overload ReturnPieces
        public Piece ReturnPiece(Position position)
        {
            return pieces[position.line, position.column];
        }
        public bool PieceExist(Position position)
        {
            ValidatePosition(position);
            return ReturnPiece(position) == null;
        }
        public void MakeAPiece(Piece p, Position position)
        {
            if (!PieceExist(position))
            {
                throw new BoardException("A piece exists in the position");
            }
            pieces[position.line, position.column] = p;
            p.position = position;
            
        }
        public Piece RemovePiece(Position position)
        {
            if (ReturnPiece(position) == null)
            {
                return null;
            }
            Piece aux = ReturnPiece(position);
            aux.position = null; 
            pieces[position.line, position.column] = null;
            return aux;
        }
        public bool ValidPosition(Position position)
        {
            if(position.line < 0 || position.line >= lines || position.column < 0 || position.column >= columns)
            {
                return false;
            }
            return true;
        }
        // Created a especiall exception
        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position !");
            }
        }
    }
}