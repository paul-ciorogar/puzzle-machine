#region Using declarations

using System;
using System.Collections.Generic;

#endregion

namespace ConsoleApp1
{

    /// <summary>
    ///     This is just a minimal setup for starting the problem. Feel free to define your own objects by creating or modifying this setup .
    /// </summary>
    public interface IPuzzle
    {

        /// <summary>
        ///     This will list on Console the status of the each piece
        /// </summary>
        void ListPuzzle();

        /// <summary>
        ///     This will be the starting point in our algorithm. This method will be called when the user will click on the screen
        ///     for selecting the starting piece.
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        void SetStartingPiece(int xCoordinate, int yCoordinate);

    }

    public interface IPuzzleFactory
    {

        /// <summary>
        ///     Create the puzzle from coordinates
        /// </summary>
        /// <param name="xCoordinates">A list with all coordinates from X axis </param>
        /// <param name="yCoordinates">A list with all coordinates from Y axis </param>
        /// <returns></returns>
        IPuzzle CreatePuzzle(IEnumerable<int> xCoordinates, IEnumerable<int> yCoordinates);

    }

    internal class Program
    {

        #region Statics members declarations

        private static void Main(string[] args)
        {
            IPuzzleFactory puzzleFactory = ... ;

            //creating the puzzle from the Fig3
            var xCoordinates = new[] { 15, 45, 75, 105, 135, 165, 195, 225, 255 };
            var yCoordinates = new[] { 10, 40, 70, 100, 130, 160, 190 };
            IPuzzle puzzle = puzzleFactory.CreatePuzzle(xCoordinates, yCoordinates);

            //lets assume that the user had clicked in the middle of the purple piece
            puzzle.SetStartingPiece(120, 80);

            //[...]

            //at the end, we want to see our puzzle
            puzzle.ListPuzzle();
        }

        #endregion

    }

}