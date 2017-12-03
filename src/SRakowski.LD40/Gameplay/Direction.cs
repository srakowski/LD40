using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Gameplay
{
    enum Direction
    {
        NorthWest,
        NorthEast,
        East,
        SouthEast,
        SouthWest,
        West,
    }

    static class DirectionHelpers
    {
        public static Point GetPointInDirection(this Point self, int row, Direction direction) =>
            direction == Direction.NorthWest ? self + new Point(row % 2 == 0 ? -1 : 0, -1) :
            direction == Direction.NorthEast ? self + new Point(row % 2 == 0 ? 0 : 1, -1) :
            direction == Direction.East ? self + new Point(1, 0) :
            direction == Direction.SouthEast ? self + new Point(row % 2 == 0 ? 0 : 1, 1) :
            direction == Direction.SouthWest ? self + new Point(row % 2 == 0 ? -1 : 0, 1) :
            direction == Direction.West ? self + new Point(-1, 0) :
            self;

    }
}
