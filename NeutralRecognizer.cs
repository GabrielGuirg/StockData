using System.ComponentModel;
using System;

namespace C__Stock_Project
{
    public class NeutralRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Neutral";

        // Checks if the Neutral pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position < 0 || position >= candlesticks.Count) return false;

            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Determine if the conditions for the Neutral pattern are met
            return Math.Abs(candle.Open - candle.Close) < (candle.Range * 0.1);
        }
    }
}
