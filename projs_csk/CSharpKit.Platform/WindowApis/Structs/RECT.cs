using System;
using System.Runtime.InteropServices;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// RECT
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT : IEquatable<RECT>
    {
        public RECT(Int32 width, Int32 height)
            : this(0, 0, width, height) { }

        public RECT(Int32 left, Int32 top, Int32 right, Int32 bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static readonly RECT Empty = new RECT();

        public Int32 Left;
        public Int32 Top;
        public Int32 Right;
        public Int32 Bottom;

        public Int32 Width
        {
            get { return Math.Abs(Right - Left); }
        }
        public Int32 Height
        {
            get { return Math.Abs(Bottom - Top); }
        }

        public Boolean IsEmpty
        {
            get { return this == Empty; }
        }

        #region IEquatable<RECT> 成员

        public Boolean Equals(RECT other)
        {
            return true
                && this.Left.Equals(other.Left)
                && this.Top.Equals(other.Top)
                && this.Right.Equals(other.Right)
                && this.Bottom.Equals(other.Bottom)
                ;
        }

        #endregion

        public override Boolean Equals(Object obj)
        {
            return this.Equals((RECT)obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override String ToString()
        {
            return base.ToString();
        }

        public static Boolean operator ==(RECT rect1, RECT rect2)
        {
            return rect1.Equals(rect2);
        }
        public static Boolean operator !=(RECT rect1, RECT rect2)
        {
            return !rect1.Equals(rect2);
        }

        //@EndOf(RECT)
    }
}
