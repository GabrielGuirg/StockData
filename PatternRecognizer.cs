using System.Collections.Generic;
using System.ComponentModel;

namespace C__Stock_Project
{
    public abstract class PatternRecognizer
    {
        // Gets the name of the recognized pattern
        public abstract string PatternName { get; }

        // Checks if the pattern is present at a specific position
        public abstract bool IsPatternPresent(BindingList<SmartCandlestick> candlesticks, int position);

        // Gets the range covering the candlesticks involved in the pattern
        public virtual (int start, int end) GetPatternRange(BindingList<SmartCandlestick> candlesticks, int position)
        {
            return (position, position); // Default range covering only the current candlestick
        }
    }
}
