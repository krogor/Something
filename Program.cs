using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        private class TextProperties
        {
            public int StartIndex { get; set; }
            public int LengthToBold { get; set; }
            public string TextValue { get; set; }
        }

        private class Result
        {
            public int PreviousIndex { get; set; }

            public string ResultValue { get; set; }
        }

        static void Main(string[] args)
        {
            var text = "The quick brown fox jumps over the lazy dog.The .....quick brown fox jumps over the lazy dog....";

            var arr = new List<int[]>
            {
                new[] {50, 2},
                new[] {4, 15},
                new[] {35, 8},
                new[] {1, 2}
            };

            var startDelimeter = "<b>";
            var endDelimeter = "</b>";

            var values = arr
                .Select(x => new TextProperties
                {
                    LengthToBold = x[1],
                    StartIndex = x[0],
                    TextValue = $"{startDelimeter}{text.Substring(x[0], x[1])}{endDelimeter}"
                })
                .OrderBy(x => x.StartIndex)
                .Aggregate(new Result{ PreviousIndex = 0, ResultValue = text }, (result, properties) =>
                {
                    var indexWithDelimeterLength = properties.StartIndex + result.PreviousIndex;

                    result.PreviousIndex += startDelimeter.Length + endDelimeter.Length;
                    result.ResultValue = result.ResultValue
                        .Remove(indexWithDelimeterLength, properties.LengthToBold)
                        .Insert(indexWithDelimeterLength, properties.TextValue);

                    return result;
                });

            //var previousIndex = 0;
            //foreach (var value in values)
            //{
            //    var indexWithDelimeterLength = value.Index + previousIndex;
            //    text = text
            //        .Remove(indexWithDelimeterLength, value.LengthToBold)
            //        .Insert(indexWithDelimeterLength, value.TextValue);

            //    previousIndex += 7;
            //}

            Console.WriteLine(values.ResultValue);
            Console.ReadLine();
        }
    }
}
