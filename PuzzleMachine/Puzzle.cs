namespace PuzzleMachine;

public class Puzzle : IPuzzle
{
    private List<Int64> XCoordinates { get; init; }
    private List<Int64> YCoordinates { get; init; }

    private Rectangle? StartingPiece = null;

    public Puzzle(IEnumerable<Int64> xCoordinates, IEnumerable<Int64> yCoordinates)
    {
        XCoordinates = xCoordinates.ToList();
        YCoordinates = yCoordinates.ToList();
    }

    public IEnumerable<PuzzlePiece> ListPuzzle()
    {
        for (int y = 0; y < YCoordinates.Count - 1; y++)
        {
            for (int x = 0; x < XCoordinates.Count - 1; x++)
            {
                yield return GeneratePiece(
                    XCoordinates[x], YCoordinates[y],
                    XCoordinates[x + 1], YCoordinates[y + 1]
                );
            }
        }

        yield break;
    }

    private PuzzlePiece GeneratePiece(Int64 ax, Int64 ay, Int64 bx, Int64 by)
    {
        Rectangle rectangle = new Rectangle(ax, ay, bx, by);
        Face top = Face.Straight;
        Face left = Face.Straight;
        Face bottom = Face.Straight;
        Face right = Face.Straight;

        if (StartingPiece == null) return new PuzzlePiece(rectangle);

        if (bx >= StartingPiece.B.X && bx < XCoordinates[^1]) right = Face.Tab;
        if (bx > StartingPiece.B.X) left = Face.Blank;

        if (by >= StartingPiece.B.Y && by < YCoordinates[^1]) top = Face.Tab;
        if (by > StartingPiece.B.Y) bottom = Face.Blank;

        if (ax <= StartingPiece.A.X && ax > XCoordinates[0]) left = Face.Tab;
        if (ax < StartingPiece.A.X) right = Face.Blank;

        if (ay <= StartingPiece.A.Y && ay > YCoordinates[0]) bottom = Face.Tab;
        if (ay < StartingPiece.A.Y) top = Face.Blank;

        return new PuzzlePiece(rectangle, top, left, bottom, right);
    }

    public void SetStartingPiece(Int64 xCoordinate, Int64 yCoordinate)
    {
        var xData = FindContaining(xCoordinate, XCoordinates);
        var yData = FindContaining(yCoordinate, YCoordinates);

        if (!xData.HasValue) return;
        if (!yData.HasValue) return;

        StartingPiece = new Rectangle(
            new Point(xData.Value.Item1, yData.Value.Item1),
            new Point(xData.Value.Item2, yData.Value.Item2)
        );
    }

    private static Nullable<(Int64, Int64)> FindContaining(Int64 val, List<Int64> coordinates)
    {
        int minNum = 0;
        int maxNum = coordinates.Count - 1;

        while (minNum <= maxNum)
        {
            int mid = (minNum + maxNum) / 2;
            if (coordinates[mid] < val && val <= coordinates[mid + 1])
            {
                return (coordinates[mid], coordinates[mid + 1]);
            }
            else if (coordinates[mid - 1] < val && val <= coordinates[mid])
            {
                return (coordinates[mid - 1], coordinates[mid]);
            }
            else if (val < coordinates[mid])
            {
                maxNum = mid - 1;
            }
            else
            {
                minNum = mid + 1;
            }
        }

        return null;
    }
}

public enum Face
{
    Straight, Blank, Tab
}

public record PuzzlePiece
{
    public Rectangle Rectangle { get; init; }
    public Face Top { get; init; }
    public Face Left { get; init; }
    public Face Bottom { get; init; }
    public Face Right { get; init; }

    public PuzzlePiece(Rectangle rectangle, Face top, Face left, Face bottom, Face right)
    {
        Rectangle = rectangle;
        Top = top;
        Left = left;
        Bottom = bottom;
        Right = right;
    }

    public PuzzlePiece(Rectangle rectangle)
    {
        Rectangle = rectangle;
        Top = Face.Straight;
        Left = Face.Straight;
        Bottom = Face.Straight;
        Right = Face.Straight;
    }
}

public record Point
{
    public Int64 X { get; init; }
    public Int64 Y { get; init; }

    public Point(Int64 x, Int64 y)
    {
        X = x;
        Y = y;
    }

}

public record Rectangle
{
    public Point A { get; init; }
    public Point B { get; init; }

    public Rectangle(Point a, Point b)
    {
        A = a;
        B = b;
    }

    public Rectangle(Int64 ax, Int64 ay, Int64 bx, Int64 by)
    {
        A = new Point(ax, ay);
        B = new Point(bx, by);
    }
}

