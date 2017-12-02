using Microsoft.Xna.Framework;

namespace SRakowski.LD40.Gameplay
{
    enum Direction
    {
        NorthWest,
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
    }

    static class DirectionHelpers
    {
        public static Point GetPointInDirection(this Point self, Direction direction) =>
            direction == Direction.NorthWest ? self + new Point(-1, -1) :
            direction == Direction.North ? self + new Point(0, -1) :
            direction == Direction.NorthEast ? self + new Point(1, -1) :
            direction == Direction.East ? self + new Point(1, 0) :
            direction == Direction.SouthEast ? self + new Point(1, 1) :
            direction == Direction.South ? self + new Point(0, 1) :
            direction == Direction.SouthWest ? self + new Point(-1, 1) :
            direction == Direction.West ? self + new Point(-1, 0) :
            self;

    }
}
