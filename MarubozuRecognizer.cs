
using System.ComponentModel;

namespace C__Stock_Project
{
    public class MarubozuRecognizer : PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public override string PatternName => "Marubozu";

        // Checks if the Marubozu pattern is present at a specific position
        public override bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position)
        {
            // Retrieve the candlestick at the specified position
            var candle = candlesticks[position];

            // Determine if the conditions for the Marubozu pattern are met
            return candle.TopWick < (candle.Range * 0.05) && candle.BottomWick < (candle.Range * 0.05) && (candle.Close > candle.Open);
        }
    }
}
