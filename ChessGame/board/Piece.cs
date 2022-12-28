namespace ChessGame.board
{
    public abstract class Piece
    {
        public Position position { get; set; }  
        public Colors color { get; protected set; }
        public Board board { get; protected set; }
        public int numberOfMoves { get; protected set; }
        
        public Piece(Colors color, Board board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.numberOfMoves = 0;
        }
        protected bool CanMove(Position position)
        {
            Piece p = board.ReturnPiece(position);
            return p == null || p.color != this.color;
        }
        public void IncrementNumberOfMoves()    
        {
            numberOfMoves++;
        }
        public void DecrementNumberOfMoves()    
        {
            numberOfMoves--;
        }
        public bool ThereArePossibleMoviments()
        {
            bool[,] matrix = PossibleMovies();
            for (int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if(matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CanMoveTo(Position position)
        {
            return PossibleMovies()[position.line, position.column];
        }
        public abstract bool[,] PossibleMovies();
    }
}