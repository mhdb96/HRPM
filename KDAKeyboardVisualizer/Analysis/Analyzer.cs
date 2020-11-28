using KDABackendLibrary;
using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Helpers;
using KDASharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDAKeyboardVisualizer.Analysis
{
    public class Analyzer
    {
        public KeystrokeData[] AllKeystrokeData;
        public List<Tuple<int, int>> KeyPressCounts = new List<Tuple<int,int>>();
        public double KeyPressCountsAvg;
        public double KeyPressCountsStdDev;
        public Range InRange = new Range();
        public Range UpRange = new Range();
        public Range UnderRange = new Range();

        public Analyzer()   
        {

            //var Holdcounts = CreateKeystrokeData()/*BinaryConnector.StaticLoad<KeystrokeData[]>(@"C:\Users\mhdb9\Desktop\data.kdf")*/;
            //GetData(Holdcounts);
            //var counts = new List<int>();
            //foreach (var item in KeyPressCounts)
            //{
            //    counts.Add(item.Item2);
            //}
            //KeyPressCountsStdDev = CalculateStandardDeviation(counts);
            //FillLists();
            //InRange.FindMinMax();
            //UpRange.FindMinMax();
            //UnderRange.FindMinMax();
        }
        public Analyzer(int id, DateTime start, DateTime end)
        {
            var Holdcounts = CreateKeystrokeData(id,start,end)/*BinaryConnector.StaticLoad<KeystrokeData[]>(@"C:\Users\mhdb9\Desktop\data.kdf")*/;
            GetData(Holdcounts);
            var counts = new List<int>();
            foreach (var item in KeyPressCounts)
            {
                counts.Add(item.Item2);
            }
            KeyPressCountsStdDev = CalculateStandardDeviation(counts);
            FillLists();
            InRange.FindMinMax();
            UpRange.FindMinMax();
            UnderRange.FindMinMax();
        }

        int[] CreateKeystrokeData(int id, DateTime start, DateTime end)
        {
            int[] counts = new int[FileHelper.GetEnumCount<KeysList>()];            
            var sessions = GlobalConfig.Connection.Sessions_GetByUserIdAndDate(id, start, end);
            foreach (var session in sessions)
            {
                foreach (var key in session.SessionKeys)
                {
                    counts[key.KeyId] += key.HoldTimesCount;
                }
            }
            return counts;
        }
        private void FillLists()
        {
            foreach (var count in KeyPressCounts)
            {
                if (count.Item2 > KeyPressCountsAvg + KeyPressCountsStdDev)
                {
                    UpRange.Elements.Add(count);
                }
                else if (count.Item2 < KeyPressCountsAvg - KeyPressCountsStdDev)
                {
                    UnderRange.Elements.Add(count);
                }
                else
                {
                    InRange.Elements.Add(count);
                }
            }
        }
        private void GetData(int[] counts)
        {
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] != 0)
                {
                    KeyPressCounts.Add(new Tuple<int, int>(i, counts[i]));
                }
            }
            
        }

        private double CalculateStandardDeviation(IEnumerable<int> values)
        {
            double standardDeviation = 0;

            if (values.Any())
            {
                // Compute the average.     
                double avg = values.Average();
                KeyPressCountsAvg = avg;
                // Perform the Sum of (value-avg)_2_2.      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));

                // Put it all together.      
                standardDeviation = Math.Sqrt(sum / values.Count());
            }
            return standardDeviation;
        }
    }
}
