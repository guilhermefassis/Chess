using System.Collections.Generic;
using ChessGame.board.Exceptions;

namespace ChessGame.board.Chess
{
    public class ChessMatch
    {
        public Board board { get; private set; } 
        public int turn { get; private set; }
        public Colors currentPayerColor { get; private set; }
        public bool closed { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool xeque { get; private set; }
        public Piece EnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPayerColor = Colors.White;
            closed = false; 
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            EnPassant = null;
            PutPieces();
        }

        public Piece PerformMoviment(Position origin, Position destination)
        {
            Piece p = board.RemovePiece(origin);
            p.IncrementNumberOfMoves();
            Piece capturedPiece = board.RemovePiece(destination);
            board.MakeAPiece(p, destination);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            // #EspecialMoviment
            // Small Castle
            if (p is King && destination.column == origin.column + 2)
            {
                Position TowerOrigin = new Position(origin.line, origin.column + 3);
                Position TowerDestination = new Position(origin.line, origin.column + 1);

                Piece T = board.RemovePiece(TowerOrigin);
                T.IncrementNumberOfMoves();
                board.MakeAPiece(T, TowerDestination);
            }
            // Big Castle
            if (p is King && destination.column == origin.column - 2)
            {
                Position TowerOrigin = new Position(origin.line, origin.column - 4);
                Position TowerDestination = new Position(origin.line, origin.column - 1);

                Piece T = board.RemovePiece(TowerOrigin);
                T.IncrementNumberOfMoves();
                board.MakeAPiece(T, TowerDestination);
            }
            // EspecialMoviment
            // EnPassant
            if(p is Pawn)
            {
                if(origin.column != destination.column && capturedPiece == null)
                {
                    Position pos;
                    if (p.color == Colors.White)
                    {
                        pos = new Position(destination.line + 1, destination.column);
                    }
                    else
                    {
                        pos = new Position(destination.line - 1, destination.column);
                    }
                    capturedPiece = board.RemovePiece(pos);
                    captured.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }
        public void MakeMove (Position origin, Position destination)
        {
            Piece capturedPiece = PerformMoviment(origin, destination);
            
            if (ItsInCheck(currentPayerColor))
            {
                UndoMoviment(origin, destination, capturedPiece);
                throw new BoardException("You cant put yourself in check");
            }

            Piece p = board.ReturnPiece(destination);
            // #EspecialMoviment
            // Promotion
            if (p is Pawn)
            {
                if(p.color == Colors.White && destination.line == 0 || p.color == Colors.Black && destination.line == 8)
                {
                    p = board.RemovePiece(destination);
                    pieces.Remove(p);
                    Piece queen = new Queen(p.color, board);
                    board.MakeAPiece(queen, destination);
                }
            }
            if (ItsInCheck(AdversaryColor(currentPayerColor)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if(CheckMate(AdversaryColor(currentPayerColor)))
            {
                closed = true;
            }
            else
            {
                turn++;
                ChangePlayer();
            }
            // #EspecialMoviment
            // EnPassant
            if (p is Pawn && destination.line == origin.line - 2 || destination.line == origin.line + 2)
            {
                EnPassant = p;
            }
            else
            {
                EnPassant = null;
            }
        }
        public void UndoMoviment(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = board.RemovePiece(destination);
            p.DecrementNumberOfMoves();
            if (capturedPiece != null)
            {
                board.MakeAPiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            board.MakeAPiece(p, origin);

            
            // Small Castle
            if (p is King && destination.column == origin.column + 2)
            {
                Position TowerOrigin = new Position(origin.line, origin.column + 3);
                Position TowerDestination = new Position(origin.line, origin.column + 1);

                Piece T = board.RemovePiece(TowerDestination);
                T.IncrementNumberOfMoves();
                board.MakeAPiece(T, TowerOrigin);
            }
            // Big  Castle
            if (p is King && destination.column == origin.column - 2)
            {
                Position TowerOrigin = new Position(origin.line, origin.column - 4);
                Position TowerDestination = new Position(origin.line, origin.column - 1);

                Piece T = board.RemovePiece(TowerDestination);
                T.IncrementNumberOfMoves();
                board.MakeAPiece(T, TowerOrigin);
            }
            // #EspecialMoviment
            // EnPassant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == EnPassant)
                {
                    Piece pawn = board.RemovePiece(destination);
                    Position posPawn;

                    if (p.color == Colors.White)
                    {
                        posPawn = new Position(3, destination.column);
                    }
                    else
                    {
                        posPawn = new Position(4, destination.column);
                    }
                    board.MakeAPiece(pawn, posPawn);
                }
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if(board.ReturnPiece(position) == null)
            {
                throw new BoardException("Don't exists a piece in this location.");
            }
            if(currentPayerColor != board.ReturnPiece(position).color)
            {
                throw new BoardException("The origin piece is not your's. ");
            }
            if(board.ReturnPiece(position).ThereArePossibleMoviments())
            {
                throw new BoardException("There is not possibles moviments in this location.");
            }   
        }
        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!board.ReturnPiece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Destination position is invalid."); 
            }
        }
        private void ChangePlayer()
        {
            if (currentPayerColor == Colors.White)
            {
                currentPayerColor = Colors.Black;
            }
            else
            {
                currentPayerColor = Colors.White;
            }
        }
        public HashSet<Piece> CapturedPiecesColor(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        private Colors AdversaryColor(Colors color)
        {
            if(color == Colors.White)
            {
                return Colors.Black;
            }
            return Colors.White;
        }
        private Piece King(Colors color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool ItsInCheck(Colors color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException("The king dont exists.");
            }
            foreach (Piece x in PiecesInGame(AdversaryColor(color)))
            {
                bool[,] matrix = x.PossibleMovies();
                if (matrix[K.position.line, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckMate(Colors color)
        {
            if(ItsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] matrix = x.PossibleMovies();
                for (int i = 0; i < board.lines; i++)
                {
                    for(int j = 0; j < board.columns; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position origin = x.position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = PerformMoviment(origin, destination);
                            bool checkTest = ItsInCheck(color);
                            UndoMoviment(origin, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public HashSet<Piece> PiecesInGame(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPiecesColor(color));
            return aux;
        }
        public void PutANewPiece(char column, int line, Piece piece)
        {
            board.MakeAPiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);
        }
        private void PutPieces()
        {
            PutANewPiece('a', 1, new Tower(Colors.White, board));
            PutANewPiece('h', 1, new Tower(Colors.White, board));
            PutANewPiece('g', 1, new Knight(Colors.White, board));
            PutANewPiece('b', 1, new Knight(Colors.White, board));
            PutANewPiece('f', 1, new Bishop(Colors.White, board));
            PutANewPiece('c', 1, new Bishop(Colors.White, board));
            PutANewPiece('d', 1, new Queen(Colors.White, board));
            PutANewPiece('e', 1, new King(Colors.White, board, this));
            PutANewPiece('a', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('b', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('c', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('d', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('e', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('f', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('g', 2, new Pawn(Colors.White, board, this));
            PutANewPiece('h', 2, new Pawn(Colors.White, board, this));

            PutANewPiece('a', 8, new Tower(Colors.Black, board));
            PutANewPiece('h', 8, new Tower(Colors.Black, board));
            PutANewPiece('g', 8, new Knight(Colors.Black, board));
            PutANewPiece('b', 8, new Knight(Colors.Black, board));
            PutANewPiece('f', 8, new Bishop(Colors.Black, board));
            PutANewPiece('c', 8, new Bishop(Colors.Black, board));
            PutANewPiece('d', 8, new Queen(Colors.Black, board));
            PutANewPiece('e', 8, new King(Colors.Black, board, this));
            PutANewPiece('a', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('b', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('c', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('d', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('e', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('f', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('g', 7, new Pawn(Colors.Black, board, this));
            PutANewPiece('h', 7, new Pawn(Colors.Black, board, this));
        }
    }
}