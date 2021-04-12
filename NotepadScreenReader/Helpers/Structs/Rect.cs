namespace NotepadScreenReader.Helpers.Structs
{
    internal struct Rect
    {
        internal int Left { get;}
        internal int Top { get;}
        internal int Right { get;}
        internal int Bottom { get; }

        internal bool Contains(Point point)
        {
            return (point.X >= Left && point.X <= Right) && (point.Y <= Bottom && point.Y >= Top);
        }
    }
}