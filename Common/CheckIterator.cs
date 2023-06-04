using System;
using System.Collections;
using System.Collections.Generic;

// Define the ChessIterator class
public class ChessIterator : IEnumerator<Func<List<Piece>, PieceColor, Piece>>
{
    private readonly List<Piece> _board;
    private readonly PieceColor _kingColor;
    private int currentIndex;
    private readonly List<Func<List<Piece>, PieceColor, Piece>> checkFunctions;

    public ChessIterator(List<Piece> board, PieceColor kingColor)
    {
        _board = board;
        _kingColor = kingColor;
        currentIndex = -1;
        checkFunctions = new List<Func<List<Piece>, PieceColor, Piece>>();
    }

    public Func<List<Piece>, PieceColor, Piece> Current => checkFunctions[currentIndex];

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        currentIndex++;
        return currentIndex < checkFunctions.Count;
    }

    public void Reset()
    {
        currentIndex = -1;
    }
    public void AddCheckFunction(Func<List<Piece>, PieceColor, Piece> checkFunction)
    {
        checkFunctions.Add(checkFunction);
    }

    public void Dispose()
    {
        
    }
}

public class ChessCheckIterator : IEnumerable<Func<List<Piece>, PieceColor, Piece>>
{
    private readonly List<Piece> _board;
    private readonly PieceColor _kingColor;

    public ChessCheckIterator(List<Piece> board, PieceColor kingColor)
    {
        _board = board;
    }

    public IEnumerator<Func<List<Piece>, PieceColor, Piece>> GetEnumerator()
    {
        ChessIterator iterator = new ChessIterator(_board, _kingColor);
        
        iterator.AddCheckFunction((board, kingColor) => IsKingUnderDiagonalCheck(board, kingColor));
        iterator.AddCheckFunction((board, kingColor) => IsKingUnderLineCheck(board, kingColor));
        iterator.AddCheckFunction((board, kingColor) => IsKingUnderKnightCheck(board, kingColor));

        return iterator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private Piece IsKingUnderDiagonalCheck(List<Piece> board, PieceColor kingColor)
    {
        // Check if king is under check - implementation 1
        return new Piece(PieceName.Bishop, "e2", kingColor == PieceColor.Black ? PieceColor.White : PieceColor.Black);
    }

    private Piece IsKingUnderLineCheck(List<Piece> board, PieceColor kingColor)
    {
        // Check if king is under check - implementation 2
        return new Piece(PieceName.Rook, "e2", kingColor == PieceColor.Black ? PieceColor.White : PieceColor.Black);
    }

    private Piece IsKingUnderKnightCheck(List<Piece> board, PieceColor kingColor)
    {
        // Check if king is under check - implementation 3
        return new Piece(PieceName.Knight, "e2", kingColor == PieceColor.Black ? PieceColor.White : PieceColor.Black);
    }
}