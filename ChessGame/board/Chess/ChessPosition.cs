using ChessGame.board;

namespace ChessGame.board.Chess
{
    public class ChessPosition
    {
        public char column { get; set; } 
        public int line { get; set; }
        public ChessPosition(char column, int line)
        {
            this.line = line;
            this.column = column;
        }
        public Position ToPosition()
        {
            return new Position(8 - line, column - 'a');
        }
        
        public override string ToString()
        {
            return " " + column + line;
        }
    }
}   