
public class Constants { 
    public const float DiceRollAnimationDuration = 0.3f;

    public const int NoOfPawnsPerPlayer = 4;


    public class DiceRoll
    {
        public const int min = 1; // Min face of the dice
        public const int max = 6; // Max face of the dice
        public const int RollToGetOutFromStart = 6;  // The no on the dice rolled that a player can take his pawn out
        public const int StartTileNo = 0;        // Tile no for start
        public const int FirstTileNoOut = 1;    // Starting tile no immediately on getting out
        public const int TotalStepsToReachHome = 57; // Total no of tiles from start to home (5 + 6 * 8 + 1 * 4)
        public static readonly int[] SafeTiles = { 1, 9, 14,  22, 27, 35, 40, 48 };  // Every 8 & 5 tiles starting @ 1
        public static readonly int[] InnerTiles = { 52, 53, 54, 55, 56 };   // Inner tiles for each player from his Start 
        public const int InnerTileStarting = 52; // Starting tile no of the inner tiles
        public const int InnerTileEnding = 56; // Last tile no of the inner tiles
    }
}
