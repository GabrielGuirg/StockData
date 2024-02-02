using System;
using System.ComponentModel;

namespace C__Stock_Project
{
    // Represents a pattern recognizer for Dragonfly Doji patterns
    public class DragonflyDojiRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Dragonfly Doji";

        // Checks if the Dragonfly Doji pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position < 0 || position >= candlesticks.Count) return false;

            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Define the threshold for considering a "small body"
            double smallBodyThreshold = Math.Min(0.1 * candle.Range, 0.03); // For example, 10% of the range or 0.03, whichever is smaller

            // Check if the conditions for the Dragonfly Doji pattern are met
            return
                Math.Abs(candle.Open - candle.Close) <= smallBodyThreshold && // Small or no body
                candle.TopWick <= smallBodyThreshold && // Small or no upper wick
                candle.BottomWick > 0.5 * candle.Range; // Long lower wick
        }
    }
}

