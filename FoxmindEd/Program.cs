using CommandLine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FoxmindEd
{
    public class Program
    {
        public const int BreackedSumma = -1;

        static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<Options>(args);
            if (parserResult.Tag == ParserResultType.NotParsed)
                return;

            parserResult.WithParsed(o =>
            {
                try
                {
                    var filePath = o.FilePath;
                    var existing = File.Exists(filePath);

                    if (!existing)
                    {
                        ConsoleNotification.Error($"File {filePath} doesn't exist...");
                        return;
                    }

                    var strArray = File.ReadAllLines(filePath);

                    var rowCount = ProcessRows(strArray, out double maxSumma);

                    if (maxSumma == 0)
                        ConsoleNotification.Warning("Max summa is zero...");

                    ConsoleNotification.Info($"Records with max summa are: {GetRowsBy(rowCount, maxSumma)}");
                    ConsoleNotification.Info($"Records with characters are: {GetRowsBy(rowCount, BreackedSumma)}");

                    ConsoleNotification.Success("Done!");
                }
                catch (Exception e)
                {
                    ConsoleNotification.Error(e.Message);
                }
            });

            ConsoleNotification.Done();

            string GetRowsBy(Dictionary<int, double> rowCount, double searchValue)
                => string.Join(", ", rowCount.Where(v => v.Value == searchValue).Select(k => k.Key));
        }

        public static Dictionary<int, double> ProcessRows(string[] strArray, out double maxSumma)
        {
            if (strArray == null || !strArray.Any())
                throw new Exception("File doesn't contain any data...");

            if (strArray.Length == 1)
                ConsoleNotification.Warning("File contains only one record...");

            var rowCount = new Dictionary<int, double>();
            maxSumma = 0;

            for (int i = 0; i < strArray.Length; i++)
            {
                ConsoleNotification.Info(strArray[i]);

                var strPosition = i + 1;
                var elements = strArray[i].Split(",");
                double summa = 0;

                for (int j = 0; j < elements.Length; j++)
                {
                    var element = elements[j].Trim();

                    if (!double.TryParse(element, NumberStyles.Float, CultureInfo.InvariantCulture, out double res))
                    {
                        summa = BreackedSumma;
                        break;
                    }

                    summa += res;
                }

                rowCount.Add(strPosition, summa);

                if (summa > maxSumma)
                    maxSumma = summa;
            }

            return rowCount;
        }
    }
}
