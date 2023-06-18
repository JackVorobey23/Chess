using System;
using System.Collections;
using System.Collections.Generic;

public class CheckIterator : IEnumerator<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>>
{
    private int currentIndex;
    private readonly List<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>> checkFunctions;

    public CheckIterator()
    {
        currentIndex = -1;
        checkFunctions = new List<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>>();
    }

    public Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece> Current => checkFunctions[currentIndex];

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
    public void AddCheckFunction(Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece> checkFunction)
    {
        checkFunctions.Add(checkFunction);
    }

    public void Dispose()
    {

    }
}

public class ChecksCollection : IEnumerable<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>>
{
    public ChecksCollection() {}
    
    public IEnumerator<Func<List<Piece>, PieceColor, BlockingStrategyFactory, Piece>> GetEnumerator()
    {
        CheckIterator iterator = new CheckIterator();

        iterator.AddCheckFunction((board, kingColor, factory) => new GetDiagonalCheck().IsKingUnderCheck(board, kingColor, factory));
        iterator.AddCheckFunction((board, kingColor, factory) => new GetLineCheck().IsKingUnderCheck(board, kingColor, factory));
        iterator.AddCheckFunction((board, kingColor, factory) => new GetKnightCheck().IsKingUnderCheck(board, kingColor, factory));

        return iterator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}