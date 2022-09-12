namespace PuzzleMachine;

public class RectanglePuzzle : IPuzzle
{
    private List<Int64> XCoordinates { get; init; }
    private List<Int64> YCoordinates { get; init; }

    private Point PuzzleTopRightPoint { get; init; }
    private Point PuzzleBottomLeftPoint { get; init; }

    private Rectangle? StartingPiece;

    public RectanglePuzzle(IEnumerable<Int64> xCoordinates, IEnumerable<Int64> yCoordinates)
    {
        XCoordinates = xCoordinates.ToList();
        YCoordinates = yCoordinates.ToList();

        PuzzleTopRightPoint = new Point(XCoordinates[^1], YCoordinates[^1]);
        PuzzleBottomLeftPoint = new Point(XCoordinates[0], YCoordinates[0]);
    }

    public void SetStartingPiece(Int64 xCoordinate, Int64 yCoordinate)
    {
        var xData = FindFirstMinAndMaxCoordinates(xCoordinate, XCoordinates);
        var yData = FindFirstMinAndMaxCoordinates(yCoordinate, YCoordinates);

        if (!xData.HasValue) return;
        if (!yData.HasValue) return;

        StartingPiece = new Rectangle(
            new Point(xData.Value.Item1, yData.Value.Item1),
            new Point(xData.Value.Item2, yData.Value.Item2)
        );
    }

    public IEnumerable<IPuzzlePiece> ListPuzzle()
    {
        for (int y = 0; y < YCoordinates.Count - 1; y++)
        {
            for (int x = 0; x < XCoordinates.Count - 1; x++)
            {
                yield return GeneratePiece(new Rectangle(
                    XCoordinates[x], YCoordinates[y],
                    XCoordinates[x + 1], YCoordinates[y + 1]
                ));
            }
        }
        yield break;
    }

    private RectanglePuzzlePiece GeneratePiece(Rectangle piece)
    {
        Face top = Face.Straight;
        Face left = Face.Straight;
        Face bottom = Face.Straight;
        Face right = Face.Straight;

        if (StartingPiece == null) return new RectanglePuzzlePiece(piece);

        if (piece.IsRightOf(StartingPiece))
        {
            if (IsNotLastInRow(piece)) right = Face.Tab;
            left = Face.Blank;
        }

        if (piece.IsAbove(StartingPiece))
        {
            if (IsNotLastInColumn(piece)) top = Face.Tab;
            bottom = Face.Blank;
        }

        if (piece.IsOnSameColumnAs(StartingPiece))
        {
            if (IsNotLastInRow(piece)) right = Face.Tab;
            if (IsNotFirstInRow(piece)) left = Face.Tab;
        }

        if (piece.IsOnSameRowAs(StartingPiece))
        {
            if (IsNotLastInColumn(piece)) top = Face.Tab;
            if (IsNotFirstInColumn(piece)) bottom = Face.Tab;
        }

        if (piece.IsLeftOf(StartingPiece))
        {
            if (IsNotFirstInRow(piece)) left = Face.Tab;
            right = Face.Blank;
        }

        if (piece.IsBelow(StartingPiece))
        {
            if (IsNotFirstInColumn(piece)) bottom = Face.Tab;
            top = Face.Blank;
        }

        return new RectanglePuzzlePiece(piece, top, left, bottom, right);
    }

    private bool IsNotFirstInRow(Rectangle rectangle) => rectangle.BottomLeftPoint.IsRightOf(PuzzleBottomLeftPoint);
    private bool IsNotLastInRow(Rectangle rectangle) => rectangle.TopRightPoint.IsLeftOf(PuzzleTopRightPoint);
    private bool IsNotFirstInColumn(Rectangle rectangle) => rectangle.BottomLeftPoint.IsAbove(PuzzleBottomLeftPoint);
    private bool IsNotLastInColumn(Rectangle rectangle) => rectangle.TopRightPoint.IsBelow(PuzzleTopRightPoint);

    private static Nullable<(Int64, Int64)> FindFirstMinAndMaxCoordinates(Int64 val, List<Int64> coordinates)
    {
        int minNum = 0;
        int maxNum = coordinates.Count - 1;

        // Binary search
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

public record RectanglePuzzlePiece : IPuzzlePiece
{
    public Rectangle Rectangle { get; init; }
    public Face Top { get; init; }
    public Face Left { get; init; }
    public Face Bottom { get; init; }
    public Face Right { get; init; }

    public RectanglePuzzlePiece(Rectangle rectangle, Face top, Face left, Face bottom, Face right)
    {
        Rectangle = rectangle;
        Top = top;
        Left = left;
        Bottom = bottom;
        Right = right;
    }

    public RectanglePuzzlePiece(Rectangle rectangle)
    {
        Rectangle = rectangle;
        Top = Face.Straight;
        Left = Face.Straight;
        Bottom = Face.Straight;
        Right = Face.Straight;
    }

    public override string ToString()
    {
        string rectangle = $"bottom left x: {Rectangle.BottomLeftPoint.X}\tbottom left y: {Rectangle.BottomLeftPoint.Y}\ttop right x: {Rectangle.TopRightPoint.X}\ttop right y: {Rectangle.TopRightPoint.Y}\t";
        string sides = $"top: {Top}\tright: {Right}\tbottom: {Bottom}\tleft: {Left}";
        return rectangle + sides;
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

    public bool IsRightOf(Point point) => this.X > point.X;
    public bool IsLeftOf(Point point) => this.X < point.X;
    public bool IsAbove(Point point) => this.Y > point.Y;
    public bool IsBelow(Point point) => this.Y < point.Y;
}




public record Rectangle
{
    public Point BottomLeftPoint { get; init; }
    public Point TopRightPoint { get; init; }

    public Rectangle(Point leftBottom, Point topRight)
    {
        BottomLeftPoint = leftBottom;
        TopRightPoint = topRight;
    }

    public Rectangle(Int64 leftBottomX, Int64 leftBottomY, Int64 topRightX, Int64 topRightY)
    {
        BottomLeftPoint = new Point(leftBottomX, leftBottomY);
        TopRightPoint = new Point(topRightX, topRightY);
    }

    public bool IsRightOf(Point point) => this.TopRightPoint.IsRightOf(point);
    public bool IsRightOf(Rectangle rectangle) => IsRightOf(rectangle.TopRightPoint);

    public bool IsLeftOf(Point point) => this.BottomLeftPoint.IsLeftOf(point);
    public bool IsLeftOf(Rectangle rectangle) => IsLeftOf(rectangle.BottomLeftPoint);

    public bool IsAbove(Point point) => this.TopRightPoint.IsAbove(point);
    public bool IsAbove(Rectangle rectangle) => IsAbove(rectangle.TopRightPoint);

    public bool IsBelow(Point point) => this.BottomLeftPoint.IsBelow(point);
    public bool IsBelow(Rectangle rectangle) => IsBelow(rectangle.BottomLeftPoint);

    public bool IsOnSameColumnAs(Rectangle rectangle) => rectangle.TopRightPoint.X == this.TopRightPoint.X;

    public bool IsOnSameRowAs(Rectangle rectangle) => rectangle.TopRightPoint.Y == this.TopRightPoint.Y;
}



