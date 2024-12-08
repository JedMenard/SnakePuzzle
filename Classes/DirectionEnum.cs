namespace SnakePuzzle.Classes;

public enum DirectionEnum
{
    PositiveX,
    NegativeX,
    PositiveY,
    NegativeY,
    PositiveZ,
    NegativeZ,
}

public static class DirectionEnumExtensions
{
    public static List<DirectionEnum> GetOrthogonalDirections(this DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.PositiveX:
            case DirectionEnum.NegativeX:
                return new List<DirectionEnum>
                {
                    DirectionEnum.PositiveY,
                    DirectionEnum.NegativeY,
                    DirectionEnum.PositiveZ,
                    DirectionEnum.NegativeZ
                };
            case DirectionEnum.PositiveY:
            case DirectionEnum.NegativeY:
                return new List<DirectionEnum>
                {
                    DirectionEnum.PositiveX,
                    DirectionEnum.NegativeX,
                    DirectionEnum.PositiveZ,
                    DirectionEnum.NegativeZ
                };
            case DirectionEnum.PositiveZ:
            case DirectionEnum.NegativeZ:
                return new List<DirectionEnum>
                {
                    DirectionEnum.PositiveX,
                    DirectionEnum.NegativeX,
                    DirectionEnum.PositiveY,
                    DirectionEnum.NegativeY
                };
            default:
                throw new NotImplementedException($"Unexpected direction enum: {direction}");
        }
    }

    public static DirectionEnum GetOppositeDirection(this DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.PositiveX:
                return DirectionEnum.NegativeX;
            case DirectionEnum.NegativeX:
                return DirectionEnum.PositiveX;
            case DirectionEnum.PositiveY:
                return DirectionEnum.NegativeY;
            case DirectionEnum.NegativeY:
                return DirectionEnum.PositiveY;
            case DirectionEnum.PositiveZ:
                return DirectionEnum.NegativeZ;
            case DirectionEnum.NegativeZ:
                return DirectionEnum.PositiveZ;
            default:
                throw new NotImplementedException($"Unexpected direction: {direction}");
        }
    }
}
