using System.Collections.Generic;
using System.Linq;

namespace Calculator.Lenders
{
    /// <summary>
    /// The class LenderQuoteCalculator.
    /// </summary>
    public class LenderQuoteCalculator
    {
        /// <summary>
        /// The duration of loan in months.
        /// </summary>
        public const int LoanDuration = 36;

        /// <summary>
        /// Calculates and returns the loan quote.
        /// </summary>
        /// <param name="loan">The requested loan amount.</param>
        /// <param name="lenders">The list of lenders.</param>
        /// <returns></returns>
        public static CalculationResult GetLoanQuote(int loan, IEnumerable<Lender> lenders)
        {
            //Quote cannot be provided if total money is less than requested loan.
            if (loan > lenders.Sum(x => x.MoneyAvailable))
            {
                return null;
            }
            var totalRepayAmt = CalculateTotalRepayment(loan, lenders.OrderBy(x => x.RateOfInterest));
            var quotedRate = (totalRepayAmt - loan) / loan;
            var monthlyRepay = loan * (1 + quotedRate) / LoanDuration;
            CalculationResult result = new CalculationResult
            {
                LoanAmount = loan,
                MonthlyRepayment = monthlyRepay,
                QuotedRate = quotedRate,
                TotalRepayment = totalRepayAmt
            };
            return result;
        }

        /// <summary>
        /// Calculates total repayable amount.
        /// </summary>
        /// <param name="loan">The requested loan amount.</param>
        /// <param name="lenders">The list of lenders.</param>
        /// <returns></returns>
        private static decimal CalculateTotalRepayment(int loan, IEnumerable<Lender> lenders)
        {
            int totalBorrowed = 0;
            decimal totalRepayAmount = 0m;

            foreach (var lender in lenders)
            {
                var currentBorrowed = loan < totalBorrowed + lender.MoneyAvailable ? loan - totalBorrowed : lender.MoneyAvailable;
                totalRepayAmount += currentBorrowed + (currentBorrowed * lender.RateOfInterest);
                totalBorrowed += currentBorrowed;
                if (totalBorrowed >= loan)
                {
                    break;
                }
            }
            return totalRepayAmount;
        }
    }
}
