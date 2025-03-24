using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

public class CursorMover
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out Point lpPoint);

    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: CursorMover.exe <interval_ms> <offset_x> <offset_y>");
            return;
        }

        int intervalMs = int.Parse(args[0]);
        int offsetX = int.Parse(args[1]);
        int offsetY = int.Parse(args[2]);

        Console.WriteLine("Cursor movement started. Press Ctrl+C to stop.");

        try
        {
            while (true)
            {
                Point currentPosition;
                if (GetCursorPos(out currentPosition))
                {
                    SetCursorPos(currentPosition.X + offsetX, currentPosition.Y + offsetY);
                }
                Thread.Sleep(intervalMs);
            }
        }
        catch (ThreadAbortException)
        {
            Console.WriteLine("Cursor movement stopped.");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }
}
