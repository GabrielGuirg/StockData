using System;

namespace C__Stock_Project
{
    /// <summary>
    /// Represents an advanced version of a Candlestick, offering additional properties and methods
    /// for analyzing commonly used candlestick patterns in financial markets.
    /// </summary>
    public class SmartCandlestick : Candlestick
    {
        /// <summary>
        /// Gets the range of the candlestick.
        /// </summary>
        public double Range => High - Low;

        /// <summary>
        /// Gets the range of the candlestick body.
        /// </summary>
        public double BodyRange => Math.Abs(Open - Close);

        /// <summary>
        /// Gets the length of the top wick of the candlestick.
        /// </summary>
        public double TopWick => High - Math.Max(Open, Close);

        /// <summary>
        /// Gets the length of the bottom wick of the candlestick.
        /// </summary>
        public double BottomWick => Math.Min(Open, Close) - Low;
    }
}

