using System.ComponentModel.DataAnnotations;

namespace MarsProgram.Data;

public class GridBounds
{
    public int X { get; }
    public int Y { get; }

    public GridBounds(int x, int y)
    {
        if (x < 0 || x > 50)
            throw new ArgumentOutOfRangeException(nameof(x), x, "Grid size must be between 0 and 50");
        if (y < 0 || y > 50)
            throw new ArgumentOutOfRangeException(nameof(y), y, "Grid size must be between 0 and 50");
        
        X = x;
        Y = y;
    }
}