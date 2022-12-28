using ChessGame.board;
using ChessGame.board.Chess;
using ChessGame.board.Exceptions;

namespace ChessGame
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
               ChessMatch chessMatch = new ChessMatch();
               
               while (!chessMatch.closed)
               {
                    try
                    {
                        System.Console.Clear();
                        Screen.PrintMatch(chessMatch);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.GetChessPosition().ToPosition();
                        bool[,] possiblePositions = chessMatch.board.ReturnPiece(origin).PossibleMovies();
                        
                        Console.Clear();
                        Screen.PrintBoardLines(chessMatch.board, possiblePositions);
                        
                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.GetChessPosition().ToPosition();
                        chessMatch.ValidateDestinationPosition(origin, destination);

                        chessMatch.MakeMove(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
               }
            }
             catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            // ChessPosition chessPosition = new ChessPosition('a', 1);
            // System.Console.WriteLine(chessPosition);
            // System.Console.WriteLine(chessPosition.ToPosition());

            // Console.ReadKey();

        }
    }
}