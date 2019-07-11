namespace Calculator.Lenders
{
    /// <summary>
    /// The class CalculationResult.
    /// </summary>
    public class CalculationResult
    {
        /// <summary>
        /// The requested loan amount.
        /// </summary>
        public int LoanAmount { get; set; }

        /// <summary>
        /// The quoted rate of interest.
        /// </summary>
        public decimal QuotedRate { get; set; }

        /// <summary>
        /// The monthly repayment amount.
        /// </summary>
        public decimal MonthlyRepayment { get; set; }

        /// <summary>
        /// The total repayment amount.
        /// </summary>
        public decimal TotalRepayment { get; set; }
    }
}
