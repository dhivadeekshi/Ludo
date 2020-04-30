
public class Constants
{
    public const int NoOfPawnsPerPlayer = 4;

    public class DiceRoll
    {
        public const int min = 1; // Min face of the dice
        public const int max = 6; // Max face of the dice
        public const int RollToGetOutFromStart = 6;  // The no on the dice rolled that a player can take his pawn out
        public const int RollForExtraTurn = 6; // The no on the dice rolled that a player can get an extra dice throw
        public const float DiceRollAnimationDuration = 0.3f;
        public const float WaitForDiceDisplayDuration = 0.5f;
    }

    public class Tiles
    {
        public const int StartTileNo = 0;        // Tile no for start
        public const int FirstTileNoOut = 1;    // Starting tile no immediately on getting out
        public const int TotalStepsToReachHome = 57; // Total no of tiles from start to home (5 + 6 * 8 + 1 * 4)
        public const int TotalTilesInOuterPath = 52;
        public const int InnerTileStarting = 52; // Starting tile no of the inner tiles
        public const int InnerTileEnding = 56; // Last tile no of the inner tiles
        public static readonly int[] SafeTiles = { 1, 9, 14, 22, 27, 35, 40, 48 };  // Every 8 & 5 tiles starting @ 1
        public static readonly int[] InnerTiles = { 52, 53, 54, 55, 56 };   // Inner tiles for each player from his Start 
        public static readonly int[] StartingTilesNo = { 1, 14, 27, 40 }; // Starting tile no for each player clockwise starting from bottom left

    }

    public class Pawn
    {
        public const string IdleAnimationName = "PawnIdle";
        public const string HighlightAnimationName = "PawnHighlight";
        public const string MoveHighlightAnimationName = "PawnHighlight";
    }
}
