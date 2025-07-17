using System;
using System.Diagnostics;
using Utilities;

namespace RiichiCalculator
{
    public interface IAnalysis
    {
        bool Enabled { get; }
        void Log(string message);
    }

    public static class AnalysisExtensions
    {
        public static void Assert<A>(this A analysis, bool condition, string message) where A : IAnalysis
        {
            if (!condition)
            {
                analysis.Log(message);
                throw new ArgumentException(message);
            }
        }
    }

    readonly struct NoAnalysis : IAnalysis
    {
        public bool Enabled => false;

        public void Log(string message)
        {
            // No-op in release builds.
        }
    }

    readonly struct DebugAnalysis : IAnalysis
    {
    #if DEBUG
        public bool Enabled => true;

        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    #else
        public bool Enabled => false;

        public void Log(string message)
        {
            // No-op in release builds.
        }
    #endif
    }

    readonly struct AssertAnalysis : IAnalysis
    {
        public bool Enabled => true;

        public void Log(string message)
        {
        }
    }

    internal unsafe ref partial struct Winnables
    {
        private string FormatExistingGroupings(TileFlags pair, int sequences, int sets)
        {
            Pair = pair;
            Sequences.Length = sequences;
            Sets.Length = sets;
            return FormatExistingGroupings();
        }

        private string FormatOriginal()
        {
            TileList_15 ungrouped = Player.Hidden; // Copy is intentional.
            ungrouped.Add(WinningTile);

            // Not 100% performant, but recalculates the original groupings without duplicating code or remove 'readonly' from fields.
            return ungrouped.ToString() + "." + new Winnables(Deal, Seat).FormatExistingGroupings();
        }

        private string FormatExistingGroupings()
        {
            var sb = new System.Text.StringBuilder();

            if (Pair != default)
            {
                sb.AppendLine($"\n\t{Pair} (pair)");
            }

            int meldSets = 0;
            int meldSequences = 0;

            for (int meld = 0; meld < Player.Melds.Length; meld++)
            {
                TileFlags flags = Player.Melds[meld].GetFlags();
                sb.AppendLine($"\n\t{flags} ");
                if (flags.UsesOneTile())
                {
                    meldSets++;
                    if (Player.Melds[meld].Length == 3)
                    {
                        sb.AppendLine($"(open triplet)");
                    }
                    else if (flags.HasFlag(TileFlags.Back))
                    {
                        sb.AppendLine($"(concealed quad)");
                    }
                    else
                    {
                        sb.AppendLine($"(open quad)");
                    }
                }
                else {
                    meldSequences++;
                    sb.AppendLine($"(open seq)");
                }
            }

            for (int sets = meldSets; sets < Sets.Length; sets++)
            {
                sb.AppendLine($"\n\t{Sets[sets]} (closed triplet)");
            }

            for (int sequences = meldSequences; sequences < Sequences.Length; sequences++)
            {
                sb.AppendLine($"\n\t{Sequences[sequences]} (closed seq)");
            }

            return sb.ToString();
        }

        internal bool Groupings_2Ungrouped(TileFlags flags)
        {
            if (flags.UsesOneTile())
            {
                Pair = flags;
                CalculateAndReplaceWins();
                return true;
            }

            return false;
        }

        internal bool Groupings_3Ungrouped(TileFlags flags)
        {
            if (flags.UsesOneTile())
            {
                Sets.Add(flags);
                CalculateAndReplaceWins();
                return true;
            }
            else if (flags.UsesOneSequence())
            {
                Sequences.Add(flags);
                CalculateAndReplaceWins();
                return true;
            }

            return false;
        }

        internal bool Groupings_5Ungrouped<A>(in A analysis, TileFlags flags, in TileList_15 ungrouped, ref TileCounts counts) where A : struct, IAnalysis
        {
            TileFlags first = ungrouped[0];
            ref byte count = ref counts[first];
            switch (count)
            {
            case 1: { // The 5 tiles needs to have a sequence.
                // Solution: Remove the pair (if able) from the sequence, then pass to 3 tiles.
                TileFlags sequence = first;
                TileFlags pair = default;
                for (int i = 1; i < 5; i++)
                {
                    TileFlags tile = ungrouped[i];
                    count = ref counts[tile];
                    if (count == 2) pair |= tile; // Must be a pair.
                    else {
                        sequence |= tile;
                        count--;
                    }
                }

                if (pair == default)
                {
                    if (analysis.Enabled)
                        analysis.Log($"The last five hidden tiles cannot make a pair: {ungrouped}.{FormatExistingGroupings()}");
                    return false;
                }

                Pair = pair;

                if (!sequence.UsesOneSequence())
                {
                    if (analysis.Enabled)
                        analysis.Log($"The last three hidden tiles do not make a sequence nor a triplet: {flags}.{FormatExistingGroupings()}");
                    return false;
                }

                Sequences.Add(sequence);
                CalculateAndReplaceWins();
                return true;
            }
            case 2: { // MUST be a part of pair
                // Solution: Remove the pair (exists) from the group, then pass to 3 tiles.
                TileFlags pair = first; // This MUST be a pair.
                TileFlags group = default;
                for (int i = 1; i < 5; i++)
                {
                    TileFlags tile = ungrouped[i];
                    count = ref counts[tile];
                    if (count == 2) pair |= tile; // MUST be a pair.
                    else group |= tile;
                }

                Pair = pair;
                
                return Groupings_3Ungrouped(group);
            }
            case 3: {
                if (flags.RanksContainSequence()) // Same as case 1 but we found the 3 count first
                {
                    count--;
                    goto case 1;
                }

                // the MUST be a triplet
                TileFlags triplet = first; // This MUST be a triplet.
                TileFlags pair = default;
                for (int i = 1; i < 5; i++)
                {
                    TileFlags tile = ungrouped[i];
                    count = ref counts[tile];
                    if (count == 2) pair |= tile; // MUST be a pair.
                    else triplet |= tile;
                }

                Sets.Add(triplet);
                return Groupings_2Ungrouped(pair); // The 2 remaining tiles must make a pair.
            }
            default:
                if (analysis.Enabled)
                    analysis.Log($"Four of the last five hidden tiles are the same and therefore cannot be grouped into a triplet/sequence and a pair: {ungrouped}.{FormatExistingGroupings()}");
                return false;
            }
        }

        internal bool Grouping_NoSequences<A>(in A analysis, ref TileList_15 ungrouped, ref TileCounts counts) where A : struct, IAnalysis
        {
            for (int i = 0; i < ungrouped.Length; i++)
            {
                TileFlags tile = ungrouped[i];
                if (!UpdatePairOrSet(analysis, tile, ref counts[tile])) // No sequence checks.
                    return false;
            }

            CalculateAndReplaceWins();
            return true;
        }

        public enum GroupCountEncoding : byte
        {
            NoTiles = 0,
            OneTile_Unsort = 1,
            TwoTiles_Unsort = 2,
            ThreeTiles_Unsort = 3,
            FourTiles_Unsort = 4,

            OneTile_Ambiguous = 5,
            TwoTiles_Ambiguous = 6,
            ThreeTiles_Ambiguous = 7,
            FourTiles_Ambiguous = 8,

            OneTile_MustSet = 9,
            TwoTiles_MustSet = 10,
            ThreeTiles_MustSet = 11,
            FourTiles_MustSet = 12,

            SecondTile_Pair = (1 << 4) + 15,

            SecondTile_Triplet0 = (2 << 4) + 15,
            ThirdTile_Triplet0 = (3 << 4) + 15,

            SecondTile_Triplet1 = (4 << 4) + 15,
            ThirdTile_Triplet1 = (5 << 4) + 15,

            SecondTile_Triplet2 = (6 << 4) + 15,
            ThirdTile_Triplet2 = (7 << 4) + 15,

            SecondTile_Triplet3 = (8 << 4) + 15,
            ThirdTile_Triplet3 = (9 << 4) + 15,


            Sequence0 = 1 << 4, // 0b0001
            Sequence1 = 1 << 5, // 0b0010
            Sequence2 = 1 << 6, // 0b0100
            Sequence3 = 1 << 7, // 0b1000
        }


        internal bool UpdatePairOrSet<A>(in A analysis, TileFlags tile, ref byte count) where A : struct, IAnalysis
        {
            Debug.Assert(count >= 1 && count <= (byte)GroupCountEncoding.ThirdTile_Triplet3, $"Ungrouped triplet tile count must be 1, 2, 3, or 4, or a valid encoding, but was {(GroupCountEncoding)count} ({count}) for tile {tile}.");
            if (count == 1 || count == 4) {
                if (analysis.Enabled) {
                    analysis.Log($"Ungrouped triplet tiles must have 2 (pair) or 3 (triplet) tiles, but found {count} for tile {tile}.{FormatExistingGroupings()}");
                }
                return false;
            }
            else if (count == 2) {
                if (Pair != default) { // This is starting a second pair
                    // Two pairs cannot exist in a hand.
                    if (analysis.Enabled) {
                        analysis.Assert(false, $"Two pairs cannot exist in a hand. Tile {Pair & TileFlags.Tiles} and tile {tile & TileFlags.Tiles} can only be used to make a pairs: .{FormatExistingGroupings()}");
                    }
                    return false;
                }

                Pair = tile; // Must be a pair.
                count = (byte)GroupCountEncoding.SecondTile_Pair; // Next tile is second tile of a pair.
            }
            else if (count == 3)
            {
                count = (byte)(Sets.Length * 2 + (byte)GroupCountEncoding.SecondTile_Triplet0); // Next tile is the second tile of the Xth triplet
                Sets.Add(tile);
            }
            else if (count == (byte)GroupCountEncoding.SecondTile_Pair)
            {
                Pair |= tile;
                count = 0; // We found both tiles, we can remove.
            }
            else // if (count <= (byte)GroupCountEncoding.Tile2_Triplet3)
            {
                int base_encoding = count - (byte)GroupCountEncoding.SecondTile_Triplet0; // The count is the index in the triplets list plus 4 so it does not overlap with maximum tile count.
                int triplet_index = base_encoding / 2;
                int tile_index = base_encoding % 2;

                Sets[triplet_index] |= tile;
                if (tile_index != 0)
                    count = 0; // This is the third tile of the triplet and there are no more tiles of this type.
                else
                    count++; // This is the second tile of the triplet.
            }

            return true;
        }

        internal void Grouping_IdentifyType(ref TileCounts counts, int index, int numsuit)
        {
            int firstnum_index = numsuit * TileExtensions.COUNT_NUMRANKS + TileExtensions.COUNT_RANKLESS + TileExtensions.INDEX_RANKS;
            int lastnum_index = firstnum_index + TileExtensions.COUNT_NUMRANKS - 1;

            int firstnum_effective_count = counts[index] & 0xF;
            int lastnum_effective_count = firstnum_effective_count;

            // search for the first tile in the larger sequence:
            for (int it = index - 1; it >= firstnum_index; it--)
            {
                int effective_count = counts[it] & 0xF; // Mask to get the actual count.
                if (effective_count == 0 || effective_count > 4)
                {
                    firstnum_index = it + 1; // This tile is not in the sequence, so the first tile is the next one.
                    break;
                }
                firstnum_effective_count = effective_count;
            }

            for (int it = index + 1; it <= lastnum_index; it++)
            {
                int effective_count = counts[it] & 0xF; // Mask to get the actual count.
                if (effective_count == 0 || effective_count > 4)
                {
                    lastnum_index = it - 1; // This tile is not in the sequence, so the last tile is the previous one.
                    break;
                }
                lastnum_effective_count = effective_count;
            }

        #if DEBUG
            Debug.Assert(firstnum_index <= lastnum_index && firstnum_index <= index && lastnum_index >= index, $"Tile index {index} must be in range of first {firstnum_index} and last {lastnum_index} numrank index.");
        #endif


            int sequence_length = lastnum_index - firstnum_index + 1;
            if (sequence_length == 1) {
                counts[firstnum_index] += 8; // Set no more sequences flag.
                return;
            }

            if (sequence_length == 2)
            {
                counts[firstnum_index] += 8; // Set no more sequences flag.
                counts[lastnum_index] += 8; // Set no more sequences flag.
                return;
            }

            int sequence_flag = 1 << (Sequences.Length + 4);
            if (firstnum_effective_count == 1 || firstnum_effective_count == 4)
            {
                goto mustSequence;
            }
            if (lastnum_effective_count == 1 || lastnum_effective_count == 4)
            {
                firstnum_index = lastnum_index - 2; // The last tile is not in the sequence, so the first tile is the one before it.
                goto mustSequence;
            }

            if (sequence_length == 3)
            {
                int middle_effective_count = counts[firstnum_index + 1] & 0xF; // Mask to get the actual count.
                if (middle_effective_count == 1 ||
                    middle_effective_count == 4 ||
                    (middle_effective_count + firstnum_effective_count + lastnum_effective_count < 8) // Check if there are enough tiles to form triplets and one pair.
                ) {
                    goto mustSequence;
                }
            }

            // For simplicity, we will assume everything else is ambiguous.
            for (; firstnum_index <= lastnum_index; firstnum_index++)
            {
                counts[firstnum_index] += 4;
            }

            return;

        mustSequence:
            counts[firstnum_index + 0] = (byte)((counts[firstnum_index + 0] - 1) | sequence_flag);
            counts[firstnum_index + 1] = (byte)((counts[firstnum_index + 1] - 1) | sequence_flag);
            counts[firstnum_index + 2] = (byte)((counts[firstnum_index + 2] - 1) | sequence_flag);
            Sequences.Add(default);
        }

        internal bool Grouping_WithSequences<A>(in A analysis, in TileList_15 ungrouped, ref TileCounts counts) where A : struct, IAnalysis
        {
            TileList_15 ambiguous = default;
            for (int i = 0; i < ungrouped.Length; i++)
            {
                TileFlags tile = ungrouped[i];
                int suit = BitOperations.TrailingZeroCount((uint)(tile & TileFlags.Suits));

                if (suit < TileExtensions.INDEX_NUMSUITS) // Must be a tile back or honor tile:
                {
                    // No sequence checks.
                    if (!UpdatePairOrSet(analysis, tile, ref counts[suit - TileExtensions.INDEX_SUITS]))
                        return false;
                    continue;
                }
                int numsuit = suit - TileExtensions.INDEX_NUMSUITS;
                int numrank = BitOperations.TrailingZeroCount((uint)(tile & TileFlags.Ranks));
                int index = TileExtensions.COUNT_RANKLESS + numsuit * TileExtensions.COUNT_NUMRANKS + (numrank - TileExtensions.INDEX_RANKS);

                ref byte count = ref counts[index];

                while (count <= 4) // The group type has not been identified yet.
                    Grouping_IdentifyType(ref counts, index, numsuit); // Note that this is almost static and does not rely on the current loop.

                if (count <= (byte)GroupCountEncoding.FourTiles_Ambiguous)
                {
                    // This tile is ambiguous, so we will try to group it later.
                    ambiguous.Add(tile);
                    continue;
                }

                if (count <= (byte)GroupCountEncoding.FourTiles_MustSet)
                {
                    // This tile must be a part of a triplet or pair, so we will try to group it later.
                    count -= 8;
                    if (!UpdatePairOrSet(analysis, tile, ref count))
                        return false;
                    continue;
                }

                if ((count & 0xF) == 0xF) // This is a triplet or pair tile.
                {
                    if (!UpdatePairOrSet(analysis, tile, ref count))
                        return false;
                    continue;
                }

                // If here, there is some sequence flag set and we should evaluate it.
                Debug.Assert(count >= 0x10, "Count is not a valid value.");

                if ((count & (1 << 4)) != 0)
                {
                    Sequences[0] |= tile;
                    count &= 0xE0; // Remove the sequence flag.
                }
                else if ((count & (1 << 5)) != 0)
                {
                    Sequences[1] |= tile;
                    count &= 0xD0; // Remove the sequence flag.
                }
                else if ((count & (1 << 6)) != 0)
                {
                    Sequences[2] |= tile;
                    count &= 0xB0; // Remove the sequence flag.
                }
                else if ((count & (1 << 7)) != 0)
                {
                    Sequences[3] |= tile;
                    count &= 0x70; // Remove the sequence flag.
                }
            }

            if (ambiguous.Length == 0)
            {
                // No ambiguous tiles, so we can return true.
                CalculateAndReplaceWins();
                return true;
            }

        #if DEBUG
            Debug.Assert(ambiguous.Length > 3, $"[ERROR] Ambiguous tiles must have more than 3 tiles in order to actually be ambiguous. Found {ambiguous.Length} from the ambiguous list. ambiguous: {ambiguous}. {FormatOriginal()}");
        #endif

            return Grouping_Ambiguous(ref ambiguous, ref counts);
        }

        internal bool Grouping_Ambiguous(ref TileList_15 ambiguous, ref TileCounts counts)
        {
            // Recursively try to group the ambiguous tiles.
        #if DEBUG
            Debug.Assert(ambiguous.Length % 3 != 1, $"[ERROR] Ambiguous tiles must have a multiple of 3 tiles (and then 2 tiles for pairs) in order to actually form a valid hand. Found {ambiguous.Length} from the ambiguous list. ambiguous: {ambiguous}. {FormatOriginal()}");
            if (Pair == default)
                Debug.Assert(ambiguous.Length % 3 == 0, $"[ERROR] If there is no pair, the ambiguous tiles must have a multiple of 3 tiles + 2 for pair in order to actually form a valid hand. Found {ambiguous.Length} from the ambiguous list. ambiguous: {ambiguous}. {FormatOriginal()}");
            else
                Debug.Assert(ambiguous.Length % 3 == 2, $"[ERROR] If there is a pair, the ambiguous tiles must have a multiple of 3 tile in order to actually form a valid hand. Found {ambiguous.Length} from the ambiguous list. ambiguous: {ambiguous}. {FormatOriginal()}");

            Debug.Assert(ambiguous.Length >= 2, $"[ERROR] Ambiguous tiles must have more than 2 tiles when recursively calling. Found {ambiguous.Length} from the ambiguous list. ambiguous: {ambiguous}. {FormatOriginal()}");
        #endif

            if (ambiguous.Length == 2)
            {
                Debug.Assert(Pair == default);
                return Groupings_2Ungrouped(ambiguous[0] | ambiguous[1]);
            }

            if (ambiguous.Length == 3)
            {
                Debug.Assert(Pair != default);
                return Groupings_3Ungrouped(ambiguous[0] | ambiguous[1] | ambiguous[2]);
            }

            if (ambiguous.Length == 5)
            {
                Debug.Assert(Pair == default);
                return Groupings_5Ungrouped(default(NoAnalysis), ambiguous[0] | ambiguous[1] | ambiguous[2] | ambiguous[3] | ambiguous[4], in ambiguous, ref counts);
            }

            TileFlags focal = ambiguous[0];

            int suit = BitOperations.TrailingZeroCount((uint)(focal & TileFlags.Suits));
            Debug.Assert(suit >= TileExtensions.INDEX_NUMSUITS, "An honor tile can never be marked as ambiguous.");

            int numsuit = suit - TileExtensions.INDEX_NUMSUITS;
            int numrank = BitOperations.TrailingZeroCount((uint)(focal & TileFlags.Ranks));
            int index = TileExtensions.COUNT_RANKLESS + numsuit * TileExtensions.COUNT_NUMRANKS + (numrank - TileExtensions.INDEX_RANKS);

            int firstnum_index = numsuit * TileExtensions.COUNT_NUMRANKS + TileExtensions.COUNT_RANKLESS + TileExtensions.INDEX_RANKS;
            int lastnum_index = firstnum_index + TileExtensions.COUNT_NUMRANKS - 1;

            int firstnum_effective_count = counts[index] - 4;
            int lastnum_effective_count = firstnum_effective_count;

            // search for the first tile in the larger sequence:
            for (int it = index - 1; it >= firstnum_index; it--)
            {
                int effective_count = counts[it] - 4; // Mask to get the actual count.
                if (effective_count <= 0 || effective_count > 4)
                {
                    firstnum_index = it + 1; // This tile is not in the sequence, so the first tile is the next one.
                    break;
                }
                firstnum_effective_count = effective_count;
            }

            for (int it = index + 1; it <= lastnum_index; it++)
            {
                int effective_count = counts[it] - 4; // Mask to get the actual count.
                if (effective_count <= 0 || effective_count > 4)
                {
                    lastnum_index = it - 1; // This tile is not in the sequence, so the last tile is the previous one.
                    break;
                }
                lastnum_effective_count = effective_count;
            }

        #if DEBUG
            Debug.Assert(firstnum_index <= lastnum_index && firstnum_index <= index && lastnum_index >= index, $"Tile index {index} must be in range of first {firstnum_index} and last {lastnum_index} numrank index.");
        #endif


            int sequence_length = lastnum_index - firstnum_index + 1;
            if (sequence_length < 3) {
                if (firstnum_effective_count == 2)
                {
                    if (Pair != default) return false;
                    return Grouping_AmbiguousAsSet(index, 2, in ambiguous, ref counts, ref Pair);
                }
                else if (firstnum_effective_count == 3)
                {
                    if (Pair != default) return false;
                    Sets.Add(default);
                    return Grouping_AmbiguousAsSet(index, 3, in ambiguous, ref counts, ref Sets[^1]);
                }

                return false;
            }
            if (firstnum_effective_count == 1 || firstnum_effective_count == 4)
            {
                Sequences.Add(default);
                return Grouping_AmbiguousAsSeq(firstnum_index, firstnum_index + 1, firstnum_index + 2, in ambiguous, ref counts, ref Sequences[^1]);
            }
            if (lastnum_effective_count == 1 || lastnum_effective_count == 4)
            {
                firstnum_index = lastnum_index - 2; // The last tile is not in the sequence, so the first tile is the one before it.
                Sequences.Add(default);
                return Grouping_AmbiguousAsSeq(firstnum_index, firstnum_index + 1, firstnum_index + 2, in ambiguous, ref counts, ref Sequences[^1]);
            }
            if (sequence_length == 3)
            {
                int middle_effective_count = counts[firstnum_index + 1] & 0xF; // Mask to get the actual count.
                if (middle_effective_count == 1 ||
                    middle_effective_count == 4 ||
                    (middle_effective_count + firstnum_effective_count + lastnum_effective_count < 8) // Check if there are enough tiles to form triplets and one pair.
                ) {
                Sequences.Add(default);
                    return Grouping_AmbiguousAsSeq(firstnum_index, firstnum_index + 1, firstnum_index + 2, in ambiguous, ref counts, ref Sequences[^1]);
                }
            }

            if (Pair == default)
            {
                if (Grouping_AmbiguousAsSet(firstnum_index, 2, in ambiguous, ref counts, ref Pair))
                {
                    return true;
                }
                Pair = default;
                counts[index] += 2;
            }
            else if (firstnum_effective_count == 3) {
                Sets.Add(default);
                if (Grouping_AmbiguousAsSet(firstnum_index, 3, in ambiguous, ref counts, ref Sets[^1]))
                {
                    return true;
                }
                Sets.Length--; // Remove the last set, as it was not valid.
                counts[index] += 3;
            }
            
            Sequences.Add(default);
            for (; firstnum_index <= lastnum_index - 2; firstnum_index++)
            {
                if (Grouping_AmbiguousAsSeq(firstnum_index, firstnum_index + 1, firstnum_index + 2, in ambiguous, ref counts, ref Sequences[^1]))
                {
                    return true;
                }
                Sequences[^1] = default; // Remove the last sequence, as it was not valid.
                counts[firstnum_index]++;
                counts[firstnum_index + 1]++;
                counts[firstnum_index + 2]++;
            }

            return false;
        }

        internal bool Grouping_AmbiguousAsSet(int index, int count, in TileList_15 ambiguous, ref TileCounts counts, ref TileFlags flags)
        {
            TileList_15 next_ambiguous = default;
            for (int i = 0; i < ambiguous.Length; i++)
            {
                if (count > 0 && index == ambiguous[i].TileIndex()) {
                    flags |= ambiguous[i];
                    count--;
                }
                else {
                    next_ambiguous.Add(ambiguous[i]);
                }
            }
            return Grouping_Ambiguous(ref next_ambiguous, ref counts);
        }

        internal bool Grouping_AmbiguousAsSeq(int index0, int index1, int index2, in TileList_15 ambiguous, ref TileCounts counts, ref TileFlags flags)
        {
            TileList_15 next_ambiguous = default;
            for (int i = 0; i < ambiguous.Length; i++)
            {
                int tile_index = ambiguous[i].TileIndex();
                if (tile_index == index0)
                {
                    index0 = -1; // Mark as used.
                    flags |= ambiguous[i];
                    counts[tile_index]--;
                }
                else if (tile_index == index1)
                {
                    index1 = -1; // Mark as used.
                    flags |= ambiguous[i];
                    counts[tile_index]--;
                }
                else if (tile_index == index2)
                {
                    index2 = -1; // Mark as used.
                    flags |= ambiguous[i];
                    counts[tile_index]--;
                }
                else {
                    next_ambiguous.Add(ambiguous[i]);
                }
            }
            return Grouping_Ambiguous(ref next_ambiguous, ref counts);
        }

        internal void SetDora(in TileCounts ungrouped)
        {
            TileCounts counts = ungrouped; // Copy the counts to avoid modifying the original.
            counts.Add(Player.Melds);

            byte count = 0;
            for (int index = 0; index < Deal.Dora.Length; index++)
            {
                TileFlags dora = Deal.Dora[index];
                count += counts[dora.DoraTileIndex()]; // Count the dora tiles in the hand.
            }

            DoraCount = count;
        }


        internal void FormGroups<A>(in A analysis) where A : struct, IAnalysis
        {
            TileCounts counts = default;
            counts.Add(Player.Hidden);
            counts.Add(WinningTile);

            SetDora(counts);


            int tiles = Player.Hidden.Length + 1; // +1 for the winning tile.

            if (tiles == 2) {
                if (!Groupings_2Ungrouped(Hand))
                {
                    if (analysis.Enabled)
                        analysis.Log($"The last two hidden tiles cannot make a pair: {Player.Hidden}.{FormatExistingGroupings()}");
                }
            }
            else if (tiles == 3)
            {
                if (!Groupings_3Ungrouped(Hand))
                {
                    if (analysis.Enabled)
                        analysis.Log($"The last three hidden tiles cannot make a triplet or sequence: {Player.Hidden}.{FormatExistingGroupings()}");
                }
            }
            else {
                TileList_15 ungrouped = Player.Hidden; // Copy is intentional.
                ungrouped.Add(WinningTile);

                if (tiles == 5)
                {
                    Groupings_5Ungrouped(analysis, Hand, in ungrouped, ref counts);
                }
                else {
                    if (tiles == 14)
                    {
                        Wins wins;

                        wins = ThirteenOrphans(counts);
                        if (!wins.Empty)
                        {
                            ReplaceBest(ref wins);
                            return;
                        }

                        wins = SevenPairs(ungrouped, counts);
                        if (!wins.Empty)
                        {
                            AddYaku(ref wins);
                            ReplaceBest(ref wins);
                        }
                    }

                    if (Hand.RanksContainSequence()) {
                        Grouping_WithSequences(analysis, in ungrouped, ref counts);
                    }
                    else {
                        Grouping_NoSequences(analysis, ref ungrouped, ref counts);
                    }
                }
            }
        }
    }
}