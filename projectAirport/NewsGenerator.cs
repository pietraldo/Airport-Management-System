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
        private AllCombinationIterator _allCombinationIterator;

        public NewsGenerator(List<Media> media, List<IReportable> reportabl)
        {
            _media = media;
            _reportable = reportabl;
            _allCombinationIterator = new AllCombinationIterator(_media, _reportable);
        }

        public string? GenerateNextNews()
        {
            if(_allCombinationIterator.MoveNext())
            {
                (Media mediaObj, IReportable reporatbleObj) = _allCombinationIterator.Current;
                return mediaObj.Report(reporatbleObj);
            }
            return null;
        }

        private class AllCombinationIterator : IEnumerator<(Media, IReportable)>
        {
            private List<Media> _media;
            private List<IReportable> _reportable;
            private IEnumerator<IReportable> _repEnumerator;
            private IEnumerator<Media> _mediaEnumerator;

            public AllCombinationIterator(List<Media> media, List<IReportable> reportable)
            {
                _media = media;
                _reportable = reportable;
                Reset();
            }

            public bool MoveNext()
            {
                if(!_repEnumerator.MoveNext())
                {
                    _repEnumerator.Reset();
                    _repEnumerator.MoveNext();
                    if (!_mediaEnumerator.MoveNext())
                    {
                        Reset();
                        return false;
                    }
                }
                return true;
            }

            public void Reset()
            {
                _mediaEnumerator= _media.GetEnumerator();
                _repEnumerator=_reportable.GetEnumerator();
                _mediaEnumerator.MoveNext();
            }
            public void Dispose()
            {
                _mediaEnumerator.Dispose();
                _repEnumerator.Dispose();
            }

            object IEnumerator.Current => Current;

            public (Media, IReportable) Current
            {
                get
                {
                    if (_repEnumerator == null || _mediaEnumerator == null)
                        throw new InvalidOperationException("Enumerator is not initialized.");
                    return new (_mediaEnumerator.Current, _repEnumerator.Current);
                }
            }

        }
    }

    
}
