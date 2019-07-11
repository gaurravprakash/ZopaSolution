using Calculator.Lenders;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Reader.Csv
{
    /// <summary>
    /// The LenderCsvReader class.
    /// </summary>
    public class LenderCsvReader
    {
        /// <summary>
        /// Returns a list of Lenders from the specified file.
        /// </summary>
        /// <param name="filePath">The market data file path.</param>
        /// <returns></returns>
        public static IEnumerable<Lender> Read(string filePath)
        {
            using (var reader = new CsvReader(File.OpenText(filePath)))
            {
                return reader.GetRecords<DataRow>().Select(r => new Lender { MoneyAvailable = r.Available, RateOfInterest = r.Rate }).ToList();
            }
        }

        /// <summary>
        /// Represents each row in the market data file.
        /// </summary>
        private class DataRow
        {
            /// <summary>
            /// The lender name.
            /// </summary>
            public string Lender { get; set; }

            /// <summary>
            /// The lender's rate.
            /// </summary>
            public decimal Rate { get; set; }

            /// <summary>
            /// Money available at the lender.
            /// </summary>
            public int Available { get; set; }
        }
    }
}
