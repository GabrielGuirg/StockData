using System.ComponentModel;

namespace C__Stock_Project
{
    public class HammerRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Hammer";

        // Checks if the Hammer pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Determine if the conditions for the Hammer pattern are met
            return candle.BodyRange < (candle.Range * 0.3) && candle.BottomWick >= (candle.BodyRange * 2) && candle.TopWick < candle.BodyRange;
        }
    }
}
