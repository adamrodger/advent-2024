using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Utilities;

/// <summary>
/// Utilities for dealing with grids
/// </summary>
public static class GridUtilities
{
    private static readonly (int x, int y)[] Deltas =
    {
        (0, -1),
        (-1, 0),
        (1, 0),
        (0, 1)
    };

    /// <summary>
    /// Convert the input into a char grid
    /// </summary>
    /// <param name="input">Input</param>
    /// <returns>Char grid</returns>
    public static char[,] ToGrid(this IReadOnlyList<string> input)
    {
        // y,x remember, not x,y
        char[,] grid = new char[input.Count, input[0].Length];

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[y, x] = input[y][x];
            }
        }

        return grid;
    }

    /// <summary>
    /// Parse the grid
    /// </summary>
    /// <param name="input">Input lines</param>
    /// <returns>Grid</returns>
    public static T[,] ToGrid<T>(this IReadOnlyList<string> input)
    {
        // y,x remember, not x,y
        T[,] grid = new T[input.Count, input[0].Length];

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                grid[y, x] = (T)Convert.ChangeType(input[y][x].ToString(), typeof(T));
            }
        }

        return grid;
    }

    /// <summary>
    /// Get the adjacent 4 values from the given position
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>Adjacent values</returns>
    public static IEnumerable<T> Adjacent4<T>(this T[,] grid, int x, int y)
    {
        foreach ((int dx, int dy) in Deltas)
        {
            int nx = x + dx;
            int ny = y + dy;

            if (nx >= 0 && ny >= 0 && nx < grid.GetLength(1) && ny < grid.GetLength(0))
            {
                yield return grid[ny, nx];
            }
        }
    }

    /// <summary>
    /// Get the adjacent 4 positions from the given position
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>Adjacent positions</returns>
    public static IEnumerable<Point2D> Adjacent4Positions<T>(this T[,] grid, Point2D point)
        => grid.Adjacent4Positions(point.X, point.Y);

    /// <summary>
    /// Get the adjacent 4 positions from the given position
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>Adjacent positions</returns>
    public static IEnumerable<Point2D> Adjacent4Positions<T>(this T[,] grid, int x, int y)
    {
        foreach ((int dx, int dy) in Deltas)
        {
            int nx = x + dx;
            int ny = y + dy;

            if (nx >= 0 && ny >= 0 && nx < grid.GetLength(1) && ny < grid.GetLength(0))
            {
                yield return (nx, ny);
            }
        }
    }

    /// <summary>
    /// Get the adjacent 8 values from the given position
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>Adjacent values</returns>
    public static IEnumerable<T> Adjacent8<T>(this T[,] grid, int x, int y)
    {
        bool rowAbove = y - 1 >= 0;
        bool rowBelow = y + 1 < grid.GetLength(0);
        bool columnBefore = x - 1 >= 0;
        bool columnAfter = x + 1 < grid.GetLength(1);

        if (rowAbove && columnBefore)
        {
            yield return grid[y - 1, x - 1];
        }

        if (rowAbove)
        {
            yield return grid[y - 1, x];
        }

        if (rowAbove && columnAfter)
        {
            yield return grid[y - 1, x + 1];
        }

        if (columnBefore)
        {
            yield return grid[y, x - 1];
        }

        if (columnAfter)
        {
            yield return grid[y, x + 1];
        }

        if (rowBelow && columnBefore)
        {
            yield return grid[y + 1, x - 1];
        }

        if (rowBelow)
        {
            yield return grid[y + 1, x];
        }

        if (rowBelow && columnAfter)
        {
            yield return grid[y + 1, x + 1];
        }
    }

    /// <summary>
    /// Get the adjacent 8 positions from the given position
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>Adjacent positions</returns>
    public static IEnumerable<Point2D> Adjacent8Positions<T>(this T[,] grid, Point2D point)
        => grid.Adjacent8Positions(point.X, point.Y);

    /// <summary>
    /// Get the adjacent 8 positions from the given position
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>Adjacent positions</returns>
    public static IEnumerable<Point2D> Adjacent8Positions<T>(this T[,] grid, int x, int y)
    {
        bool rowAbove = y - 1 >= 0;
        bool rowBelow = y + 1 < grid.GetLength(0);
        bool columnBefore = x - 1 >= 0;
        bool columnAfter = x + 1 < grid.GetLength(1);

        if (rowAbove && columnBefore)
        {
            yield return (x - 1, y - 1);
        }

        if (rowAbove)
        {
            yield return (x, y - 1);
        }

        if (rowAbove && columnAfter)
        {
            yield return (x + 1, y - 1);
        }

        if (columnBefore)
        {
            yield return (x - 1, y);
        }

        if (columnAfter)
        {
            yield return (x + 1, y);
        }

        if (rowBelow && columnBefore)
        {
            yield return (x - 1, y + 1);
        }

        if (rowBelow)
        {
            yield return (x, y + 1);
        }

        if (rowBelow && columnAfter)
        {
            yield return (x + 1, y + 1);
        }
    }

    /// <summary>
    /// Perform an action for each cell of the grid
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="cellAction">Action to perform on each cell</param>
    /// <param name="lineAction">Action to perform at the end of each line</param>
    /// <typeparam name="T">Grid type</typeparam>
    public static void ForEach<T>(this T[,] grid, Action<T> cellAction, Action lineAction = null)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                cellAction(grid[y, x]);
            }

            lineAction?.Invoke();
        }
    }

    /// <summary>
    /// Perform an action for each cell of the grid
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="action">Action to perform on each cell</param>
    /// <typeparam name="T">Grid type</typeparam>
    public static void ForEach<T>(this T[,] grid, Action<int, int, T> action)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                action(x, y, grid[y, x]);
            }
        }
    }

    /// <summary>
    /// Print the grid to a string representation
    /// </summary>
    /// <param name="grid">Grid</param>
    /// <param name="print">Print the string to the debugger output</param>
    /// <typeparam name="T">Grid type</typeparam>
    /// <returns>String representation</returns>
    public static string Print<T>(this T[,] grid, bool print = true)
    {
        var builder = new StringBuilder(grid.GetLength(0) * (grid.GetLength(1) + Environment.NewLine.Length));
        grid.ForEach(cell => builder.Append(cell), () => builder.AppendLine());
        builder.AppendLine();

        string result = builder.ToString();

        if (print)
        {
            Debug.Write(result);
            Debug.Flush();
        }

        return result;
    }

    /// <summary>
    /// Process the input line by line and char by char
    /// </summary>
    /// <param name="input">Input</param>
    /// <param name="action">Processing action</param>
    public static void ForEach(this IEnumerable<string> input, Action<Point2D, char> action)
    {
        int y = 0;

        foreach (string line in input)
        {
            for (int x = 0; x < line.Length; x++)
            {
                action((x, y), line[x]);
            }

            y++;
        }
    }

    /// <summary>
    /// Take a slice of the grid from a given start point along a given bearing
    /// </summary>
    /// <remarks>
    /// If the slice hits the edge of the grid then it will stop yielding, and thus the
    /// final result may be smaller than the requested size
    /// </remarks>
    /// <param name="grid">Grid</param>
    /// <param name="current">Start point</param>
    /// <param name="bearing">Bearing</param>
    /// <param name="size">Size of the slice</param>
    /// <returns>Slice of the grid</returns>
    public static IEnumerable<T> Slice<T>(this T[,] grid, Point2D current, Bearing bearing, int size)
    {
        yield return grid[current.Y, current.X];

        for (int i = 0; i < size - 1; i++)
        {
            current = current.Move(bearing);

            if (!current.InBounds(grid))
            {
                yield break;
            }

            yield return grid[current.Y, current.X];
        }
    }

    /// <summary>
    /// Enumerate all points on the grid
    /// </summary>
    /// <typeparam name="T">Grid type</typeparam>
    /// <param name="grid">Grid</param>
    /// <returns>All points</returns>
    public static IEnumerable<Point2D> Points<T>(this T[,] grid)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                yield return (x, y);
            }
        }
    }

    /// <summary>
    /// Enumerate all points and values on the grid
    /// </summary>
    /// <typeparam name="T">Grid type</typeparam>
    /// <param name="grid">Grid</param>
    /// <returns>All points</returns>
    public static IEnumerable<(Point2D Point, T Value)> Select<T>(this T[,] grid)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                yield return ((x, y), grid[y, x]);
            }
        }
    }

    /// <summary>
    /// Get the grid element at the given point
    /// </summary>
    /// <typeparam name="T">Grid type</typeparam>
    /// <param name="grid">Grid</param>
    /// <param name="point">Point</param>
    /// <returns>Element at point</returns>
    public static T At<T>(this T[,] grid, Point2D point)
        => grid[point.Y, point.X];

    /// <summary>
    /// Get the grid element at the given point
    /// </summary>
    /// <typeparam name="T">Grid type</typeparam>
    /// <param name="grid">Grid</param>
    /// <param name="point">Point</param>
    /// <returns>Element at point</returns>
    public static T AtOrDefault<T>(this T[,] grid, Point2D point)
        => point.InBounds(grid) ? grid[point.Y, point.X] : default;
}
