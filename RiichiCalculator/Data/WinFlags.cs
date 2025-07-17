namespace RiichiCalculator
{
    [System.Flags]
    public enum PlayerFlags
    {
        /// <summary>
        /// The player has declared "Ready Hand" (Riichi).
        /// </summary>
        ReadyHand = 1 << 0,
        /// <summary>
        /// The player has declared "Ready Hand" on the first discard, and no tile calls or concealed quad declarations have been made before.
        /// </summary>
        /// <remarks>
        /// This is true when BeforeFirstDiscard was true when calling ReadyHand. If this is true, then ReadyHand must also true.
        /// </remarks>
        DoubleReady = 1 << 1,
        /// <summary>
        /// The player has not discarded and no tile calls or concealed quad declarations have been made. If ReadyHand, this represents no discard/calls/quads have been made since the ReadyHand declaration.
        /// </summary>
        BeforeFirstDiscard = 1 << 2,
        /// <summary>
        /// If the player is in the process of resolving a quad, that is, about to or currently revealing dora or making an additional draw.
        /// </summary>
        /// <remarks>
        /// This is used to determine if the player won after making a quad, or by stealing a quad from another player (who has this flag set).
        /// </remarks>
        MakingAQuad = 1 << 3,
    }
}