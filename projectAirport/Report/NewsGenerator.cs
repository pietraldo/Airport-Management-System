using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public class NewsGenerator
    {
        private List<Media> _media;
        private List<IReportable> _reportable;
        private IEnumerator<string?> _allCombinationIterator;

        public NewsGenerator(List<Media> media, List<IReportable> reportabl)
        {
            _media = media;
            _reportable = reportabl;
            _allCombinationIterator = GetEnumerator();
        }

        public void PrintAllNews()
        {
            string? news;
            while ((news = GenerateNextNews()) != null)
            {
                Console.WriteLine(news);
            }
        }

        public string? GenerateNextNews()
        {
            _allCombinationIterator.MoveNext();
            return _allCombinationIterator.Current;
        }

        private IEnumerator<string?> GetEnumerator()
        {
            foreach (var reportable in _reportable)
            {
                foreach (var medio in _media)
                {
                    yield return medio.Report(reportable);
                }
            }
            yield return null;
        }
    }
}
