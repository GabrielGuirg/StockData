using System;
using System.ComponentModel;

namespace C__Stock_Project
{
    // Represents a Gravestone Doji pattern recognizer
    internal class GravestoneDojiRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Gravestone Doji";

        // Checks if the Gravestone Doji pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Validate the position for pattern recognition
            if (position < 0 || position >= candlesticks.Count) return false;

            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Determine if the conditions for the Gravestone Doji pattern are met
            bool isDoji = Math.Abs(candle.Close - candle.Open) < (candle.Range * 0.1);
            bool isGravestone = candle.TopWick < (candle.Range * 0.1) && candle.BottomWick > (candle.Range * 0.4);

            return isDoji && isGravestone;
        }
    }
}
