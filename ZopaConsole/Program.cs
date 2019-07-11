using Calculator.Lenders;
using Reader.Csv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ZopaConsole
{
    /// <summary>
    /// Driver console Program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Minimum loan value.
        /// </summary>
        const int LoanMinVal = 1000;

        /// <summary>
        /// Maximum loan value.
        /// </summary>
        const int LoanMaxVal = 15000;

        /// <summary>
        /// The value that loan can be a multiple of.
        /// </summary>
        const int IncrementVal = 100;

        /// <summary>
        /// Loan amount as requested.
        /// </summary>
        static int LoanRequested;

        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Input parameters.</param>
        static void Main(string[] args)
        {
            //Set en-GB culture for current thread.
            var culture = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            //Validate user input.
            if (!ValidateInput(args))
            {
                return;
            }
            //Obtain list of Lenders.
            IEnumerable<Lender> lenders = LenderCsvReader.Read(args[0]);
            //Obtain Loan quote.
            CalculationResult result = LenderQuoteCalculator.GetLoanQuote(LoanRequested, lenders);
            //Print obtained loan quote to console window.
            DisplayResult(result);
        }

        /// <summary>
        /// Validates the user input.
        /// </summary>
        /// <param name="args">Input parameters.</param>
        /// <returns></returns>
        static bool ValidateInput(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please provide input in the following format : [market_file_path] [loan_amount]");
                return false;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Please make sure that market data file exists and is accessible.");
                return false;
            }
            if (!int.TryParse(args[1], out LoanRequested))
            {
                Console.WriteLine("Please provide an integer value for loan amount.");
                return false;
            }
            if (LoanRequested < LoanMinVal || LoanRequested > LoanMaxVal)
            {
                Console.WriteLine($"Loan amount must be between {LoanMinVal} and {LoanMaxVal}, both values inclusive.");
                return false;
            }
            if (LoanRequested % IncrementVal != 0)
            {
                Console.WriteLine("Loan amount must be a multiple of 100.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Outputs the calculated result to console window.
        /// </summary>
        /// <param name="result">CalculationResult</param>
        static void DisplayResult(CalculationResult result)
        {
            if (result == null)
            {
                Console.WriteLine("It is not possible to provide a quote at this time.");
                return;
            }
            Console.WriteLine($"Requested amount: {result.LoanAmount:C0}");
            Console.WriteLine($"Annual Interest Rate: {result.QuotedRate:P1}");
            Console.WriteLine($"Monthly repayment: {result.MonthlyRepayment:C2}");
            Console.WriteLine($"Total repayment: {result.TotalRepayment:C2}");
        }
    }
}
