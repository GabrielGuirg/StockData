using System.ComponentModel;

namespace C__Stock_Project
{
    public class DojiRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Doji";

        // Checks if the Doji pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Determine if the conditions for the Doji pattern are met
            return candle.BodyRange < (candle.Range * 0.1);
        }
    }
}

