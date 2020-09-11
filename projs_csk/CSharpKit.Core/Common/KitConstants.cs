using System;

namespace CSharpKit
{
    /// <summary>
    /// KitConstants - 工具包常量
    /// </summary>
    public abstract class KitConstants
    {
        protected KitConstants() { }


        #region 无效数据定义

        public const Int32 InvalidValue = -9999;
        public const Single InvalidValue_Float = -9999.0f;
        public const Double InvalidValue_Double = -9999.0d;

        public const Int32 InvalidData = InvalidValue;
        public const Single InvalidData_Float = InvalidValue_Float;
        public const Double InvalidData_Double = InvalidValue_Double;

        #endregion

        #region 特殊值定义

        public const Int32 MinSpv = 0x8000;    //最小特殊值
        public const Int32 MaxSpv = 0x800f;    //最大特殊值

        #endregion



        //@@@
    }

}
