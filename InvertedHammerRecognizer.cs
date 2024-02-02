
using System.ComponentModel;

namespace C__Stock_Project
{
    public class InvertedHammerRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Inverted Hammer";

        // Checks if the Inverted Hammer pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Determine if the conditions for the Inverted Hammer pattern are met
            return candle.BodyRange < (candle.Range * 0.3) && candle.TopWick >= (candle.BodyRange * 2) && candle.BottomWick < candle.BodyRange;
        }
    }
}
