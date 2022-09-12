using PuzzleMachine;

namespace UnitTests;

[TestClass]
public class PuzzleTest
{
    /*
        Y
        ^
        +-----------------+-----------------+-----------------+
        | (10 30) (20 30) | (20 30) (30 30) | (30 30) (40 30) |
        |                 |                 |                 |
        | (10 20) (20 20) | (20 20) (30 20) | (30 20) (40 20) |
        +-----------------+-----------------+-----------------+
        | (10 20) (20 20) | (20 20) (30 20) | (30 20) (40 20) |
        |                 |                 |                 |
        | (10 10) (20 10) | (20 10) (30 10) | (30 10) (40 10) |
        +-----------------+-----------------+-----------------+ -> X

    */

    [TestMethod("when no starting piece selected all pieces have straight lines")]
    public void Test1()
    {
        Int64[] xCoordinates = new Int64[] { 10, 20, 30, 40 };
        Int64[] yCoordinates = new Int64[] { 10, 20, 30 };
        RectanglePuzzlePiece part = new(new Rectangle(10, 10, 20, 20), Face.Straight, Face.Straight, Face.Straight, Face.Straight);

        RectanglePuzzlePiece[] expected = new[] {
            part,
            part with {Rectangle = new(20, 10, 30, 20)},
            part with {Rectangle = new(30, 10, 40, 20)},
            part with {Rectangle = new(10, 20, 20, 30)},
            part with {Rectangle = new(20, 20, 30, 30)},
            part with {Rectangle = new(30, 20, 40, 30)},
        };
        RectanglePuzzle puzzle = new(xCoordinates, yCoordinates);

        CollectionAssert.AreEqual(expected, puzzle.ListPuzzle().ToList());
    }

    [TestMethod("when starting piece, bottom left, selected")]
    public void Test2()
    {
        /*
                Y
                ^
            40  +---------+---------+---------+
                |         |         |         |
                |         >>        >>        |
                |    ^    |    ^    |    ^    |
            30  +----^----+----^----+----^----+
                |         |         |         |
                |         >>        >>        |
                |    ^    |    ^    |    ^    |
            20  +----^----+----^----+----^----+
                | XXXXXXX |         |         |
                | XXXXXXX >>        >>        |
                | XXXXXXX |         |         |
            10  +---------+---------+---------+ -> X
                10        20        30        40
        */


        Int64[] xCoordinates = new Int64[] { 10, 20, 30, 40 };
        Int64[] yCoordinates = new Int64[] { 10, 20, 30, 40 };
        RectanglePuzzlePiece part = new(new Rectangle(10, 10, 20, 20), Face.Straight, Face.Straight, Face.Straight, Face.Straight);

        RectanglePuzzlePiece[] expected = new[] {
            part with {Top = Face.Tab, Right = Face.Tab},
            part with {Rectangle = new(20, 10, 30, 20), Top = Face.Tab, Right = Face.Tab, Left = Face.Blank },
            part with {Rectangle = new(30, 10, 40, 20), Top = Face.Tab, Left = Face.Blank },
            part with {Rectangle = new(10, 20, 20, 30), Top = Face.Tab, Right = Face.Tab, Bottom = Face.Blank},
            part with {Rectangle = new(20, 20, 30, 30), Top = Face.Tab, Right = Face.Tab, Bottom = Face.Blank, Left = Face.Blank},
            part with {Rectangle = new(30, 20, 40, 30), Top = Face.Tab, Bottom = Face.Blank, Left = Face.Blank},
            part with {Rectangle = new(10, 30, 20, 40), Right = Face.Tab, Bottom = Face.Blank},
            part with {Rectangle = new(20, 30, 30, 40), Right = Face.Tab, Bottom = Face.Blank, Left = Face.Blank},
            part with {Rectangle = new(30, 30, 40, 40), Bottom = Face.Blank, Left = Face.Blank},
        };
        RectanglePuzzle puzzle = new(xCoordinates, yCoordinates);
        puzzle.SetStartingPiece(14, 16);

        CollectionAssert.AreEqual(expected, puzzle.ListPuzzle().ToList());
    }

    [TestMethod("when starting piece, bottom right, selected")]
    public void Test3()
    {
        /*
                Y
                ^
            40  +---------+---------+---------+
                |         |         |         |
                |        <<        <<         |
                |    ^    |    ^    |    ^    |
            30  +----^----+----^----+----^----+
                |         |         |         |
                |        <<        <<         |
                |    ^    |    ^    |    ^    |
            20  +----^----+----^----+----^----+
                |         |         | XXXXXXX |
                |        <<        << XXXXXXX |
                |         |         | XXXXXXX |
            10  +---------+---------+---------+ -> X
                10        20        30        40
        */


        Int64[] xCoordinates = new Int64[] { 10, 20, 30, 40 };
        Int64[] yCoordinates = new Int64[] { 10, 20, 30, 40 };
        RectanglePuzzlePiece part = new(new Rectangle(10, 10, 20, 20), Face.Straight, Face.Straight, Face.Straight, Face.Straight);

        RectanglePuzzlePiece[] expected = new[] {
            part with {Top = Face.Tab, Right = Face.Blank},
            part with {Rectangle = new(20, 10, 30, 20), Top = Face.Tab, Right = Face.Blank, Left = Face.Tab },
            part with {Rectangle = new(30, 10, 40, 20), Top = Face.Tab, Left = Face.Tab },
            part with {Rectangle = new(10, 20, 20, 30), Top = Face.Tab, Right = Face.Blank, Bottom = Face.Blank},
            part with {Rectangle = new(20, 20, 30, 30), Top = Face.Tab, Right = Face.Blank, Bottom = Face.Blank, Left = Face.Tab},
            part with {Rectangle = new(30, 20, 40, 30), Top = Face.Tab, Bottom = Face.Blank, Left = Face.Tab},
            part with {Rectangle = new(10, 30, 20, 40), Right = Face.Blank, Bottom = Face.Blank},
            part with {Rectangle = new(20, 30, 30, 40), Right = Face.Blank, Bottom = Face.Blank, Left = Face.Tab},
            part with {Rectangle = new(30, 30, 40, 40), Bottom = Face.Blank, Left = Face.Tab},
        };
        RectanglePuzzle puzzle = new(xCoordinates, yCoordinates);
        puzzle.SetStartingPiece(33, 16);

        CollectionAssert.AreEqual(expected, puzzle.ListPuzzle().ToList());
    }

    [TestMethod("when starting piece, top left, selected")]
    public void Test4()
    {
        /*
                Y
                ^
            40  +---------+---------+---------+
                | XXXXXXX |         |         |
                | XXXXXXX >>        >>        |
                | XXXXXXX |         |         |
            30  +----v----+----v----+----v----+
                |    v    |    v    |    v    |
                |         >>        >>        |
                |         |         |         |
            20  +----v----+----v----+----v----+
                |    v    |    v    |    v    |
                |         >>        >>        |
                |         |         |         |
            10  +---------+---------+---------+ -> X
                10        20        30        40
        */


        Int64[] xCoordinates = new Int64[] { 10, 20, 30, 40 };
        Int64[] yCoordinates = new Int64[] { 10, 20, 30, 40 };
        RectanglePuzzlePiece part = new(new Rectangle(10, 10, 20, 20), Face.Straight, Face.Straight, Face.Straight, Face.Straight);

        RectanglePuzzlePiece[] expected = new[] {
            part with {Top = Face.Blank, Right = Face.Tab},
            part with {Rectangle = new(20, 10, 30, 20), Top = Face.Blank, Right = Face.Tab, Left = Face.Blank },
            part with {Rectangle = new(30, 10, 40, 20), Top = Face.Blank, Left = Face.Blank },
            part with {Rectangle = new(10, 20, 20, 30), Top = Face.Blank, Right = Face.Tab, Bottom = Face.Tab},
            part with {Rectangle = new(20, 20, 30, 30), Top = Face.Blank, Right = Face.Tab, Bottom = Face.Tab, Left = Face.Blank},
            part with {Rectangle = new(30, 20, 40, 30), Top = Face.Blank, Bottom = Face.Tab, Left = Face.Blank},
            part with {Rectangle = new(10, 30, 20, 40), Right = Face.Tab, Bottom = Face.Tab},
            part with {Rectangle = new(20, 30, 30, 40), Right = Face.Tab, Bottom = Face.Tab, Left = Face.Blank},
            part with {Rectangle = new(30, 30, 40, 40), Bottom = Face.Tab, Left = Face.Blank},
        };
        RectanglePuzzle puzzle = new(xCoordinates, yCoordinates);
        puzzle.SetStartingPiece(17, 34);

        CollectionAssert.AreEqual(expected, puzzle.ListPuzzle().ToList());
    }

    [TestMethod("when starting piece, top right, selected")]
    public void Test5()
    {
        /*
                Y
                ^
            40  +---------+---------+---------+
                |         |         | XXXXXXX |
                |        <<        << XXXXXXX |
                |         |         | XXXXXXX |
            30  +----v----+----v----+----v----+
                |    v    |    v    |    v    |
                |        <<        <<         |
                |         |         |         |
            20  +----v----+----v----+----v----+
                |    v    |    v    |    v    |
                |        <<        <<         |
                |         |         |         |
            10  +---------+---------+---------+ -> X
                10        20        30        40
        */


        Int64[] xCoordinates = new Int64[] { 10, 20, 30, 40 };
        Int64[] yCoordinates = new Int64[] { 10, 20, 30, 40 };
        RectanglePuzzlePiece part = new(new Rectangle(10, 10, 20, 20), Face.Straight, Face.Straight, Face.Straight, Face.Straight);

        RectanglePuzzlePiece[] expected = new[] {
            part with {Top = Face.Blank, Right = Face.Blank},
            part with {Rectangle = new(20, 10, 30, 20), Top = Face.Blank, Right = Face.Blank, Left = Face.Tab },
            part with {Rectangle = new(30, 10, 40, 20), Top = Face.Blank, Left = Face.Tab },
            part with {Rectangle = new(10, 20, 20, 30), Top = Face.Blank, Right = Face.Blank, Bottom = Face.Tab},
            part with {Rectangle = new(20, 20, 30, 30), Top = Face.Blank, Right = Face.Blank, Bottom = Face.Tab, Left = Face.Tab},
            part with {Rectangle = new(30, 20, 40, 30), Top = Face.Blank, Bottom = Face.Tab, Left = Face.Tab},
            part with {Rectangle = new(10, 30, 20, 40), Right = Face.Blank, Bottom = Face.Tab},
            part with {Rectangle = new(20, 30, 30, 40), Right = Face.Blank, Bottom = Face.Tab, Left = Face.Tab},
            part with {Rectangle = new(30, 30, 40, 40), Bottom = Face.Tab, Left = Face.Tab},
        };
        RectanglePuzzle puzzle = new(xCoordinates, yCoordinates);
        puzzle.SetStartingPiece(32, 33);

        CollectionAssert.AreEqual(expected, puzzle.ListPuzzle().ToList());
    }

    [TestMethod("when starting piece, middle, selected")]
    public void Test6()
    {
        /*
                Y
                ^
            40  +---------+---------+---------+
                |         |         |         |
                |        <<         >>        |
                |    ^    |    ^    |    ^    |
            30  +----^----+----^----+----^----+
                |         | XXXXXXX |         |
                |        << XXXXXXX >>        |
                |         | XXXXXXX |         |
            20  +----v----+----v----+----v----+
                |    v    |    v    |    v    |
                |        <<         >>        |
                |         |         |         |
            10  +---------+---------+---------+ -> X
                10        20        30        40
        */


        Int64[] xCoordinates = new Int64[] { 10, 20, 30, 40 };
        Int64[] yCoordinates = new Int64[] { 10, 20, 30, 40 };
        RectanglePuzzlePiece part = new(new Rectangle(10, 10, 20, 20), Face.Straight, Face.Straight, Face.Straight, Face.Straight);

        RectanglePuzzlePiece[] expected = new[] {
            part with {Top = Face.Blank, Right = Face.Blank},
            part with {Rectangle = new(20, 10, 30, 20), Top = Face.Blank, Right = Face.Tab, Left = Face.Tab },
            part with {Rectangle = new(30, 10, 40, 20), Top = Face.Blank, Left = Face.Blank },
            part with {Rectangle = new(10, 20, 20, 30), Top = Face.Tab, Right = Face.Blank, Bottom = Face.Tab},
            part with {Rectangle = new(20, 20, 30, 30), Top = Face.Tab, Right = Face.Tab, Bottom = Face.Tab, Left = Face.Tab},
            part with {Rectangle = new(30, 20, 40, 30), Top = Face.Tab, Bottom = Face.Tab, Left = Face.Blank},
            part with {Rectangle = new(10, 30, 20, 40), Right = Face.Blank, Bottom = Face.Blank},
            part with {Rectangle = new(20, 30, 30, 40), Right = Face.Tab, Bottom = Face.Blank, Left = Face.Tab},
            part with {Rectangle = new(30, 30, 40, 40), Bottom = Face.Blank, Left = Face.Blank},
        };
        RectanglePuzzle puzzle = new(xCoordinates, yCoordinates);
        puzzle.SetStartingPiece(22, 24);

        CollectionAssert.AreEqual(expected, puzzle.ListPuzzle().ToList());
    }
}