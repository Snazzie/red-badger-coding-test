using System.ComponentModel.DataAnnotations;

namespace MarsProgram.Data;

public class GridSize
{
    [Range(0, 50)]
    public int X { get; }
    [Range(0, 50)]
    public int Y { get; }

    public GridSize(int x, int y)
    {
        X = x;
        Y = y;
    }
}