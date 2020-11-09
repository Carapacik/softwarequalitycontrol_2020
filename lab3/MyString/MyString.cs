using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyString
{
    public partial class MyString : IEnumerable<char>
    {
        private char[] _data = new char[0];

        public int Length => _data.Length;

        public IEnumerable<char> GetStringData => _data.Concat("\0");

        public char this[in int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return new MyStringEnumerator(_data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void FillWithData(IEnumerable<char> chars)
        {
            if (Length > 0)
                Clear();

            _data = chars.ToArray();
        }

        public void Clear()
        {
            _data = new char[0];
        }

        public MyString SubString(int start, int length = int.MaxValue)
        {
            if (start + length > Length)
                throw new Exception("SubString length is too big");

            return new MyString(this.Skip(start).Take(length));
        }

        private static int Compare(MyString a, MyString b)
        {
            var len = Math.Min(a.Length, b.Length);
            for (var i = 0; i < len; i++)
            {
                if (a[i] < b[i])
                    return -1;

                if (a[i] > b[i])
                    return 1;
            }

            if (a.Length > b.Length)
                return 1;

            if (a.Length < b.Length)
                return -1;

            return 0;
        }

        public override bool Equals(object other)
        {
            if (!(other is MyString otherString))
                return false;

            return Compare(this, otherString) == 0;
        }

        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }

        public void WriteToStream(Stream stream)
        {
            using var writer = new StreamWriter(stream);
            foreach (var item in this) writer.Write(item);
        }

        public void ReadFromStream(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var textLine = reader.ReadLine();
            if (textLine == null)
                return;

            FillWithData(textLine);
        }

        #region Constructors

        public MyString()
        {
            FillWithData(new char[0]);
        }

        public MyString(IEnumerable<char> chars)
        {
            FillWithData(chars);
        }

        public MyString(IReadOnlyCollection<char> chars, int length)
        {
            if (length > chars.Count)
                throw new IndexOutOfRangeException();

            FillWithData(chars.Take(length));
        }

        public MyString(MyString otherMyString)
        {
            FillWithData(otherMyString);
        }

        public MyString(string str)
        {
            FillWithData(str);
        }

        #endregion

        #region Operators

        public static MyString operator +(MyString a, MyString b)
        {
            return new MyString(a.Concat(b));
        }

        public static MyString operator +(string a, MyString b)
        {
            return new MyString(a.Concat(b));
        }

        public static MyString operator +(MyString a, string b)
        {
            return new MyString(a.Concat(b));
        }

        public static MyString operator +(IEnumerable<char> a, MyString b)
        {
            return new MyString(a.Concat(b));
        }

        public static MyString operator +(MyString a, IEnumerable<char> b)
        {
            return new MyString(a.Concat(b));
        }

        public static bool operator ==(MyString a, MyString b)
        {
            return Compare(a, b) == 0;
        }

        public static bool operator !=(MyString a, MyString b)
        {
            return Compare(a, b) != 0;
        }

        public static bool operator <(MyString a, MyString b)
        {
            return Compare(a, b) < 0;
        }

        public static bool operator >(MyString a, MyString b)
        {
            return Compare(a, b) > 0;
        }

        public static bool operator <=(MyString a, MyString b)
        {
            return Compare(a, b) <= 0;
        }

        public static bool operator >=(MyString a, MyString b)
        {
            return Compare(a, b) >= 0;
        }

        #endregion
    }
}