using System.Collections;
using System.Collections.Generic;

namespace MyString
{
    public partial class MyString
    {
        private class MyStringEnumerator : IEnumerator<char>
        {
            private readonly char[] _data;

            private int _currentIndex;

            internal MyStringEnumerator(char[] data)
            {
                _data = data;
                Reset();
            }

            public bool MoveNext()
            {
                if (_currentIndex >= _data.Length)
                    return false;

                Current = _data[_currentIndex];
                _currentIndex++;
                return true;
            }

            public void Reset()
            {
                _currentIndex = 0;
            }

            public char Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}