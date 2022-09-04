#region Using declarations

#endregion

namespace PuzzleMachine;

public interface IPuzzle
{

    /// <summary>
    ///     This will return each piece
    /// </summary>
    /// <returns>IEnumerable<PuzzlePart></returns>
    IEnumerable<PuzzlePiece> ListPuzzle();

    /// <summary>
    ///     This will be the starting point in our algorithm. This method will be called when the user will click on the screen
    ///     for selecting the starting piece.
    /// </summary>
    /// <param name="xCoordinate"></param>
    /// <param name="yCoordinate"></param>
    void SetStartingPiece(Int64 xCoordinate, Int64 yCoordinate);

}

internal class Program
{

    #region Statics members declarations

    private static void Main(string[] args)
    {
        //creating the puzzle from the Fig3
        Int64[] xCoordinates = new Int64[] { 15, 45, 75, 105, 135, 165, 195, 225, 255 };
        Int64[] yCoordinates = new Int64[] { 10, 40, 70, 100, 130, 160, 190 };
        IPuzzle puzzle = new Puzzle(xCoordinates, yCoordinates);

        //lets assume that the user had clicked in the middle of the purple piece
        puzzle.SetStartingPiece(120, 80);

        //at the end, we want to see our puzzle
        puzzle.ListPuzzle();
    }

    #endregion

}