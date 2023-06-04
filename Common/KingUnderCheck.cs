class KingUnderCheck
{
    private List<Piece> _checks;
    private List<Piece> _board;
    public KingUnderCheck(List<Piece> checks, List<Piece> board)
    {
        _checks = checks;
        _board = board;
    }
    public bool MateOnBoard()
    {
        Piece king = _board.Find(p => p.PieceName == PieceName.King && p.PieceColor != _checks[0].PieceColor);

        bool onlyKingMoves = false;

        if(_checks.Count > 1 || _checks.Exists(p => p.PieceName == PieceName.Knight)) 
        {
            onlyKingMoves = true;
        }

        return false;
    }

    private void CalculateKingsMove()
    {
        
    }
    private void CalculateAllMoves(Piece checker, Piece king)
    {
        if(checker.PieceName == PieceName.Bishop) 
        {
            
        }
    }
}