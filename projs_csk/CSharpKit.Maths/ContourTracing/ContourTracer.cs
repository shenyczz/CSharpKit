/******************************************************************************
 * 
 * Announce: CSharpKit, Basic algorithms, components and definitions.
 *           Copyright (C) ShenYongchen.
 *           All rights reserved.
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 *  WebSite: http://github.com/shenyczz/CSharpKit
 *
 * THIS CODE IS LICENSED UNDER THE MIT LICENSE (MIT).
 * THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
 * ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
 * IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
 * PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
 * 
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using CSharpKit;
using CSharpKit.GeoApi.Geometries;
using GeoPoint = CSharpKit.GeoApi.Geometries.Point;

namespace CSharpKit.Maths.ContourTracing
{
    /// <summary>
    /// ContourTracer - 等值线追踪器
    /// </summary>
    public class ContourTracer
    {
        // 辅助点
        struct AssistPoint
        {
            public int i;		//该等值点所在边的行号  
            public int j;		//该等值点所在边的列号

            public double x;	//x坐标
            public double y;	//y坐标

            public bool bHorV;  //在横边还是列边上 1-->横边；0-->纵边
        };

        //追踪过程中需要利用如下三个辅助等值点完成追踪： 
        AssistPoint m_PreviousPoint;    // 前一个等值点
        AssistPoint m_CurrentPoint;     // 当前等值点
        AssistPoint m_NextPoint;        // 要追踪的下一个等值点

        // 不存在等值点
        private const double NotExistContourPoint = -1.0;

        private int _xcols, _ycols;
        private ContourPlex _ContourPlex;
        private Double[,] m_xSide, m_ySide;
        private int _TraceVersion = 2;

        public ContourTracer()
        {
            _ContourPlex = null;
        }

        /// <summary>
        /// 网格信息
        /// </summary>
        public IGridInfo Grid { get; set; }

        /// <summary>
        /// 格点数据
        /// </summary>
        public Double[,] GridValue { get; set; }

        /// <summary>
        /// 追踪值
        /// </summary>
        public Double TraceValue { get; set; }

        /// <summary>
        /// 等值线
        /// </summary>
        public ContourPlex ContourPlex
        {
            get { return _ContourPlex; }
        }

        /// <summary>
        /// 追踪等值线
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 每次追踪一组指定值的等值线
        /// </remarks>
        public Boolean Tracing()
        {
            // 网格信息
            if (false
                //|| Grid == null
                || Grid.Width <= 0
                || Grid.Height <= 0
                || Grid.MaxX < Grid.MinX
                || Grid.MaxY < Grid.MinY
                || Grid.IntervalX < 1.0e-12
                || Grid.IntervalY < 1.0e-12
                )
                return false;

            // 等值线簇实例
            _ContourPlex = new ContourPlex();

            // 1.为xSide和ySide分配内存空间 - ok
            PrepareSide();

            // 2.扫描网格纵横边,内插等值点 - ok
            Boolean b1 = InterpolateTracingValue();

            // 3.先追踪开路曲线
            Boolean b2 = TracingOpenedContour();
            int n2 = _ContourPlex.Count;    // test

            // 4.再追踪闭合曲线
            Boolean b3 = TracingClosedContour();
            int n3 = _ContourPlex.Count;    // test

            return b1 & b2 & b3;
        }

        #region --私有辅助函数--

        /// <summary>
        /// 为xSide和ySide分配内存空间
        /// </summary>
        private void PrepareSide()
        {
            int rows = Grid.Height;
            int cols = Grid.Width;

            // 网格中存在rows*(cols-1)条横边,
            // 所以需要为m_xSide分配rows*(cols-1)空间就行
            m_xSide = new Double[rows, cols - 1];
            _xcols = cols - 1;

            // 网格中存在(rows-1)*cols条纵边
            // 所以需要为m_ySide分配(rows-1)*cols空间就行
            m_ySide = new Double[(rows - 1), cols];
            _ycols = cols;
        }

        /// <summary>
        /// 扫描网格的纵、横边，并线性插值计算等值点
        /// </summary>
        /// <returns>成功返回 True,否则返回 False</returns>
        /// <remarks>
        /// <para></para>
        /// <para>将各边上的等值点情况存储于m_xSide和m_ySide数组中,xSide存储所有横边上的等值线情况, ySide存储所有纵边上的等值点情况。</para>
        /// <para>在插值计算时，对『与追踪值相等的数据』要进行修正处理后才计算，但在做修正处理时不要改变原来的数据</para>
        /// <para>网格点标识如下:</para>
        /// <para>
        /// (i+1,j)·--------·(i+1,j+1)
        ///         |        |
        ///         |        |
        ///         |        |
        ///         |        |
        ///  (i,j) ·--------·(i,j+1)
        /// </para>
        /// <para>i:表示行号(向上增加)</para>
        /// <para>j:表示列号(向右增加)</para>
        /// <para>标识一个网格交点时，行号在前，列号在右，如：(i,j)</para>
        /// <para>xSide,ySide中存储r值，(w为追踪值)</para>
        /// <para>对于网格横边，r = (w - pData[i][j]) / (pData[i][j+1]-pData[i][j]);</para>
        /// <para>对于网格纵边，r = (w - pData[i][j]) / (pData[i+1][j]-pData[i][j]);</para>
        /// <para>由于浮点运算的误差，xSide[i][j],ySide[i][j]有可能等于 1.0 或 0.0 </para>
        /// <para>考虑如下情况：</para>
        /// <para>(1)当追踪值与网格点上的值很接近(但不相等)时，由于运算误差，就会等于1.0,比如追踪0值时，遇到如下边:</para>
        /// <para>     20 ·--------·-0.00000016</para>
        /// <para>此边上有0值，但计算 (0-20)/(-0.00000016-20) == 1.0 </para>
        /// <para></para>
        /// <para>(2)当网格边上两端点上的值相差很悬殊时,比如追踪2值，遇到如下边：</para>
        /// <para>     1.70141E+038 ·--------·1</para>
        /// <para>此边上有2值，计算(2-1.70141E+038) / (1-1.70141E+038) == 1.0</para>
        /// <para></para>
        /// <para>网格边上有等值点时，理论上比例值不会等于0或1,但由于计算误差，我们在算法中判断时，认为0或1也是有等值点的</para>
        /// <para>所以xSide,ySide中存储的值是[0,1]的闭区间，不是(0,1)的开区间</para>
        /// </remarks>
        private Boolean InterpolateTracingValue()
        {
            int i, j;

            int rows = Grid.Height;
            int cols = Grid.Width;

            if (GridValue.GetLength(0) != rows || GridValue.GetLength(1) != cols)
                return false;

            // 追踪值
            double w = TraceValue;

            double z1, z2; //分别记录一条边的两个点上的数据值
            double shift = 0.001;  //修正值

            // 1.扫描并计算横边上的等值点,有rows*(cols-1)条横边需要扫描
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols - 1; j++)
                {
                    // 边的两端点值
                    z1 = GridValue[i, j];
                    z2 = GridValue[i, j + 1];

                    // 两个点上的数据值相等则该边上不存在等值点
                    if (z1 == z2)
                    {
                        m_xSide[i, j] = NotExistContourPoint;    // NotExistContourPoint 表示该边上不存在等值点
                        continue;	//下一个横边
                    }

                    //z1 != z2,判断横边是否存在等值点
                    z1 = (w == z1) ? z1 + shift : z1;	//Z1和要追踪的值w相等，加一小的偏移量修正
                    z2 = (w == z2) ? z2 + shift : z2;	//Z2和要追踪的值w相等，加一小的偏移量修正

                    double flag = (w - z1) * (w - z2);
                    if (flag > 0)       // 不存在等值点
                    {
                        m_xSide[i, j] = NotExistContourPoint;
                    }
                    else if (flag < 0)  // 存在等值点
                    {
                        //计算等值点位置并保存在m_xSide[i][j],该值应 >=0 && <=1 
                        m_xSide[i, j] = (w - z1) / (z2 - z1);
                    }

                }// next j

            }// next i

            // 2.扫描并计算纵边上等值点,有(rows-1)*cols条纵边需要扫描
            for (i = 0; i < rows - 1; i++)
            {
                for (j = 0; j < cols; j++)
                {
                    // 边的两端点值
                    z1 = GridValue[i, j];
                    z2 = GridValue[i + 1, j];

                    // 两个点上的数据值相等则该边上不存在等值点
                    if (z1 == z2)
                    {
                        m_ySide[i, j] = NotExistContourPoint;    // NotExistContourPoint 表示该边上不存在等值点
                        continue;	//下一个横边
                    }

                    //z1 != z2,判断横边是否存在等值点
                    z1 = (w == z1) ? z1 + shift : z1;	//Z1和要追踪的值w相等，加一小的偏移量修正
                    z2 = (w == z2) ? z2 + shift : z2;	//Z2和要追踪的值w相等，加一小的偏移量修正

                    double flag = (w - z1) * (w - z2);
                    if (flag > 0)       // 不存在等值点
                    {
                        m_ySide[i, j] = NotExistContourPoint;
                    }
                    else if (flag < 0)  // 存在等值点
                    {
                        //计算等值点位置并保存在m_ySide[i][j],该值应 >=0 && <=1 
                        m_ySide[i, j] = (w - z1) / (z2 - z1);
                    }

                }// next j

            }// next i

            return true;
        }

        /// <summary>
        /// 分4种情况追踪开路等值线
        /// </summary>
        /// <returns>成功返回 True,否则返回 False</returns>
        private Boolean TracingOpenedContour()
        {
            int i, j;

            int rows = Grid.Height;
            int cols = Grid.Width;

            // 1.搜索底边框m_xSide[0][j](自下而上追踪)	
            for (j = 0; j < cols - 1; j++)
            {
                double contourFlag = m_xSide[0, j];
                if (IsContourPoint(contourFlag))    // a2点
                {
                    //按自下向上的前进方向虚设前一点a1点的i,j
                    m_PreviousPoint.i = -1; // 假设前一点在-1行,这样下一点到当前点的前进方向就是自下向上的
                    m_PreviousPoint.j = j;
                    m_PreviousPoint.bHorV = true;	//横边

                    m_CurrentPoint.i = 0; //底边的行号为0，所以设置线头的i为0
                    m_CurrentPoint.j = j;
                    m_CurrentPoint.bHorV = true; //底边是横边

                    // 追踪一条开路等值线
                    TracingOneOpenedContour();
                }
            }

            // 2.搜索左边框m_ySide[i][0](自左而右追踪)
            for (i = 0; i < rows - 1; i++)
            {
                if (IsContourPoint(m_ySide[i, 0]))
                {
                    //按自左向右的前进方向虚设前一点a1的i,j
                    m_PreviousPoint.i = i;
                    m_PreviousPoint.j = -1; //假设前一点在-1列,使其满足左-->右的前进方向
                    m_PreviousPoint.bHorV = false;	//纵边

                    m_CurrentPoint.i = i;
                    m_CurrentPoint.j = 0; //左边框在第0列，所以设置线头的j为0
                    m_CurrentPoint.bHorV = false;		//纵边

                    // 追踪一条开路等值线
                    TracingOneOpenedContour();
                }
            }

            // 3.搜索上边框m_xSide[rows-1][j](自上而下追踪)
            double xinterval = Grid.IntervalX;
            double curPt_Col_X = Grid.MinX + m_CurrentPoint.j * xinterval; //当前等值点所在边(i,j)的所标识的j列的X坐标
            for (j = 0; j < cols - 1; j++)
            {
                if (IsContourPoint(m_xSide[rows - 1, j]))
                {
                    //虚设出由上向下追踪的条件
                    //  由上向下追踪的条件如下： 	 
                    //  1. Not( CurrentPoint.i > PreviousPoint.i )
                    //  2. Not( CurrentPoint.j > PreviousPoint.j )
                    //  3. CurrentPoint.x > m_gridDataInfo.xMin + CurrentPoint.j * deltX; 即:要求在横边上
                    //
                    m_PreviousPoint.i = rows - 1;
                    m_PreviousPoint.j = j;
                    m_PreviousPoint.bHorV = true;

                    m_CurrentPoint.i = rows - 1; //上边框的行号为rows-1
                    m_CurrentPoint.j = j;
                    m_CurrentPoint.bHorV = true; //使其符合第三个条件

                    // 追踪一条开路等值线
                    TracingOneOpenedContour();
                }
            }

            // 4.搜索右边框m_ySide[i][cols-1](自右而左追踪)
            for (i = 0; i < rows - 1; i++)
            {
                if (IsContourPoint(m_ySide[i, cols - 1]))
                {
                    //虚设出由右向左追踪的条件
                    //
                    //  由右向左追踪的条件如下： 	 
                    //  1. Not( CurrentPoint.i > PreviousPoint.i )
                    //  2. Not( CurrentPoint.j > PreviousPoint.j )
                    //  3. Not( CurrentPoint.x > m_gridDataInfo.xMin + CurrentPoint.j * deltX ); 要求在纵边上			
                    //
                    m_PreviousPoint.i = i;
                    m_PreviousPoint.j = cols - 1;
                    m_PreviousPoint.bHorV = false;

                    m_CurrentPoint.i = i;
                    m_CurrentPoint.j = cols - 1; //右边框在第cols-1列
                    m_CurrentPoint.bHorV = false;  //使其符合第三个条件

                    // 追踪一条开路等值线
                    TracingOneOpenedContour();
                }
            }

            return true;
        }

        /// <summary>
        /// 追踪闭路等值线
        /// </summary>
        /// <returns>成功返回 True,否则返回 False</returns>
        private Boolean TracingClosedContour()
        {
            int rows = Grid.Height;
            int cols = Grid.Width;

            //搜索所有的除了边框外的纵边,从左到右搜索每一列上的纵边，对于一列，从下到上搜索
            for (int j = 1; j < cols - 1; j++) //j从1开始
            {
                for (int i = 0; i < rows - 1; i++) //i从0开始
                {
                    if (IsContourPoint(m_ySide[i, j]))
                    {
                        // 追踪一条闭路等值线
                        TracingOneClosedContour(i, j);
                    }
                }//next i

            }//next j

            return true;
        }

        /// <summary>
        /// 追踪一组非闭合的等值线
        /// </summary>
        private void TracingOneOpenedContour()
        {
            int rows = Grid.Height;
            int cols = Grid.Width;

            //记录下线头所在边的i,j 和 横纵边标识
            int startPt_i = m_CurrentPoint.i;
            int startPt_j = m_CurrentPoint.j;
            bool startPt_bHoriz = m_CurrentPoint.bHorV;	// 横纵边标识

            // 验证线头在边界上
            if (startPt_i != 0              //线头在底边界上
                && startPt_i != rows - 1    //线头在上边界上
                && startPt_j != 0           //线头在左边界上
                && startPt_j != cols - 1    //线头在右边界上 
                )
                return;

            // 分配一条等值线
            Contour contour = new Contour();
            contour.Id = DateTime.Now.ToBinary().ToString();     // 曲线标识
            contour.Value = this.TraceValue;                  // 曲线的值

            // 添加一条曲线到等值线
            this.ContourPlex.Add(contour);

            // 计算出等值线头的坐标
            CalcOnePointCoord(startPt_i, startPt_j, startPt_bHoriz, ref m_CurrentPoint.x, ref m_CurrentPoint.y);

            // 保存头坐标
            contour.Add(new GeoPoint(m_CurrentPoint.x, m_CurrentPoint.y));

            //+++++++++++++++++++++++++++++++++++test
            int n = contour.Count;
            GeoPoint pt = contour[0] as GeoPoint;
            //+++++++++++++++++++++++++++++++++++

            // 表示已经追踪过
            if (startPt_bHoriz)	// 横边
            {
                m_xSide[startPt_i, startPt_j] = NotExistContourPoint;
            }
            else
            {
                m_ySide[startPt_i, startPt_j] = NotExistContourPoint;
            }

            // 追踪下一个点
            TracingNextPoint(ref contour);

            m_PreviousPoint = m_CurrentPoint;
            m_CurrentPoint = m_NextPoint;

            //遇到网格边界就结束追踪
            //------2004/03/09修改 by shenyc ------------------
            //为了不让浮点数计算的误差引起追踪结束条件的判断失误，
            //我们不能用『CurrentPoint.y <= yMin』来判断是否遇到底边框,
            //  也不能用『CurrentPoint.x <= xMin』来判断是否遇到左边框
            bool bIsFinish = (m_CurrentPoint.i == 0 && m_CurrentPoint.bHorV == true) ||  //遇到底边界,(注:不能仅仅用CurrentPoint.i == 0判断)
                (m_CurrentPoint.i == rows - 1) ||  //遇到上边界
                (m_CurrentPoint.j == 0 && m_CurrentPoint.bHorV == false) ||  //遇到左边界,(注:不能仅仅用CurrentPoint.j == 0判断)
                (m_CurrentPoint.j == cols - 1);                              //遇到右边界


            while (!bIsFinish)	//遇到边界
            {
                TracingNextPoint(ref contour);

                m_PreviousPoint = m_CurrentPoint;
                m_CurrentPoint = m_NextPoint;

                bIsFinish = (m_CurrentPoint.i == 0 && m_CurrentPoint.bHorV == true) ||
                    (m_CurrentPoint.i == rows - 1) ||
                    (m_CurrentPoint.j == 0 && m_CurrentPoint.bHorV == false) ||
                    (m_CurrentPoint.j == cols - 1);
            }

            return;
        }

        /// <summary>
        /// 追踪一条闭合等值线
        /// </summary>
        private void TracingOneClosedContour(int startPt_i, int startPt_j)
        {
            int rows = Grid.Height;
            int cols = Grid.Width;

            // 参数i,j是该闭曲线的第一个等值点的i,j

            // 虚设前一等值点的i,j，让其满足从左向右追踪的条件
            m_PreviousPoint.i = startPt_i;
            m_PreviousPoint.j = 0;
            m_PreviousPoint.bHorV = false;

            m_CurrentPoint.i = startPt_i;
            m_CurrentPoint.j = startPt_j;
            m_CurrentPoint.bHorV = false;           // 是FALSE，因为是在纵边上

            // 分配一条等值线
            Contour contour = new Contour();
            contour.Id = DateTime.Now.ToBinary().ToString();     // 曲线标识
            contour.Value = this.TraceValue;                  // 曲线的值

            // 添加一条等值线
            this.ContourPlex.Add(contour);

            // 计算出等值线头的坐标
            // FALSE => 线头在纵边上
            CalcOnePointCoord(startPt_i, startPt_j, false, ref m_CurrentPoint.x, ref m_CurrentPoint.y);

            // 保存头坐标
            contour.Add(new GeoPoint(m_CurrentPoint.x, m_CurrentPoint.y));

            // 追踪下一个等值点
            TracingNextPoint(ref contour);

            m_PreviousPoint = m_CurrentPoint;
            m_CurrentPoint = m_NextPoint;

            bool bColsed = false;
            while (!bColsed)
            {
                TracingNextPoint(ref contour);

                m_PreviousPoint = m_CurrentPoint;
                m_CurrentPoint = m_NextPoint;

                //------2004/03/09修改 by shenyc------------------
                //用等值点所在边的标识i,j来判断曲线是否封闭，不用x、y坐标来比较，因为浮点数计算会出现误差
                bColsed = (m_CurrentPoint.i == startPt_i) &&
                    (m_CurrentPoint.j == startPt_j) &&
                    (m_CurrentPoint.bHorV == false);

            }

            m_ySide[startPt_i, startPt_j] = NotExistContourPoint;   // 已经追踪过

            return;
        }

        /// <summary>
        /// 是否等值线点
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private Boolean IsContourPoint(Double flag)
        {
            return (flag < 0.0 || flag > 1.0) ? false : true;
        }

        /// <summary>
        /// 计算出等值线头的坐标
        /// </summary>
        /// <param name="i">行</param>
        /// <param name="j">列</param>
        /// <param name="bHorizon">水平边/垂直边</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CalcOnePointCoord(int i, int j, Boolean bHorizon, ref double x, ref double y)
        {
            double xmin = Grid.MinX;
            double ymin = Grid.MinY;
            double xinterval = Grid.IntervalX;
            double yinterval = Grid.IntervalY;

            if (bHorizon)	//在横边上
            {
                x = xmin + (j + m_xSide[i, j]) * xinterval;
                y = ymin + i * yinterval;
            }
            else	        //在纵边上
            {
                x = xmin + j * xinterval;
                y = ymin + (i + m_ySide[i, j]) * yinterval;
            }

            return;
        }

        /// <summary>
        /// 追踪下一个等值点 a3
        /// </summary>
        /// <param name="curve"></param>
        /// <remarks>
        /// <para></para>
        /// <para>1.先确定出等值线的前进方向(自下向上、由左向右、自上向下、由右向左，其中之一)</para>
        /// <para>2.再追踪下一个等值点</para>
        /// <para>前进方向可以如下判定：</para>
        /// <para>if( 当前点.行号 > 前一点.行号 ) { 下---->上 }</para>
        /// <para>if( 当前点.列号 > 前一点.列号 ) { 左---->右 }</para>
        /// <para>if( 当前点在横边上 ) { 上---->下 }</para>
        /// <para>else { 右---->左 }</para>
        /// <para></para>
        /// </remarks>
        private void TracingNextPoint(ref Contour curve)
        {
            // 1.先确定出等值线的前进方向(自下向上、由左向右、自上向下、由右向左，其中之一)
            // 2.再追踪下一个等值点

            if (m_CurrentPoint.i > m_PreviousPoint.i)
            {
                //TRACE0("下--->上\n");
                if (_TraceVersion == 2)
                    FromBottom2TopTracingV2();
                if (_TraceVersion == 1)
                    FromBottom2TopTracing();
            }
            else if (m_CurrentPoint.j > m_PreviousPoint.j)
            {
                //TRACE0("左--->右\n");
                if (_TraceVersion == 2)
                    FromLeft2RightTracingV2();
                else
                    FromLeft2RightTracing();
            }
            else if (m_CurrentPoint.bHorV == true)  //curPt_Col_X < CurrentPoint.x
            {//在横边上。CurrentPoint.bHorizon == TRUE 和 curPt_Col_X < CurrentPoint.x ，这两个条件等价

                //Assert( m_CurrentPoint.i <= m_PreviousPoint.i &&
                //        m_CurrentPoint.j <= m_PreviousPoint.j );

                //TRACE0("上--->下\n");
                if (_TraceVersion == 2)
                    FromTop2BottomTracingV2();
                else
                    FromTop2BottomTracing();
            }
            else
            {
                //Assert(m_CurrentPoint.bHorV==FALSE);//在纵边上

                //Assert( m_CurrentPoint.i <= m_PreviousPoint.i &&
                //        m_CurrentPoint.j <= m_PreviousPoint.j );

                //TRACE0("右--->左\n");
                if (_TraceVersion == 2)
                    FromRight2LeftTracingV2();
                else
                    FromRight2LeftTracing();

            }

            // 添加下一点
            curve.Add(new GeoPoint(m_NextPoint.x, m_NextPoint.y));

            return;
        }

        /// <summary>
        /// 自下向上追踪等值线
        /// </summary>
        /// <remarks>
        /// <para></para>
        /// <para>等值线自下向上前进时，网格单元的情况如下：</para>
        /// <para></para>
        ///                    横边(i+1,j)
        ///                    xSide[i+1][j]
        /// 
        ///                         ↑
        ///              (i+1,j)    ∣    (i+1,j+1)
        ///                  |-----------|
        ///                  |           |
        ///  纵边(i,j)---→  |           | ←----纵边(i,j+1)
        ///  ySide[i][j]	 |           |       ySide[i][j+1]
        ///                  |           |
        ///                  |-----·----|    
        ///              (i,j)      ↖     (i,j+1)
        ///                           ＼
        ///                             等前等值点(P2)
        /// <para></para>
        /// <para>当前等值点(用P2表示)在网格单元的底边上，那么下一等值点(用P3表示)所在的位置有三种情况：</para>
        /// <para>1.在纵边(i,j)上</para>
        /// <para>2.在纵边(i,j+1)上</para>
        /// <para>3.在横边(i+1,j)上</para>
        /// <para>但实际追踪时只能选择其中之一,程序判断的依据即是：ySide[i][j] 、 ySide[i][j+1] 、xSide[i+1][j] </para>
        /// <para>假设纵边(i,j)上存在等值点P31, 纵边(i,j+1)上存在等值点P33, 横边(i+1,j)上存在等值点P32</para>
        /// <para>选择的次序如下：</para>
        /// <para>1.当P31，P33都存在时，选择靠近网格底边者为P3(比较ySide[i][j]、ySide[i][j+1])</para>
        /// <para>2.若P31,P33靠近底边的距离相同，则选择与P2点距离近者为P3</para>
        /// <para>3.当P31,P33中只有一个存在时，则存在点即为P3</para>
        /// <para>4.当无P31,P33存在时，对边必定存在P32作为P3</para>
        /// <para></para>
        /// </remarks>
        private void FromBottom2TopTracing()
        {
            /*
						   横边(i+1,j)
						  xSide[i+1][j]

							   ↑
					(i+1,j)    ∣    (i+1,j+1)
						|-----------|
						|           |
		  纵边(i,j)---→|           |←----纵边(i,j+1)
		  ySide[i][j]	|           |     ySide[i][j+1]
						|           |
						|-----·----|    
					(i,j)      ↖     (i,j+1)
								 ＼
								   等前等值点(P2)
             */

            // 当前点的行号 > 前一点的行号
            if (!(m_CurrentPoint.i > m_PreviousPoint.i))
                return;

            // 横边
            if (!m_CurrentPoint.bHorV)
                return;

            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            if (IsContourPoint(m_ySide[i, j]) && IsContourPoint(m_ySide[i, j + 1]))
            {
                if (m_ySide[i, j] < m_ySide[i, j + 1])
                {
                    HandlingAfterNextPointFounded(i, j, false);// 在纵边(i,j)上
                }
                else if (m_ySide[i, j] == m_ySide[i, j + 1])
                {
                    // 注意:这里需要重新计算xSide[i][j]，不能直接用xSide[i][j],因为在上一次已经被置为-2.0
                    double xSideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i, j + 1] - GridValue[i, j]);
                    if (xSideIJ <= 0.5f)
                    {
                        //TRACE3("m_xSide[%d][%d]=%f,左-->右----遇到两边r值相等\n",i,j,*(m_xSide+i*m_xcols+j));
                        HandlingAfterNextPointFounded(i, j, false);//在纵边(i,j)上
                    }
                    else
                    {
                        HandlingAfterNextPointFounded(i, j + 1, false);//在纵边(i,j+1)上
                    }
                }
                else
                {
                    HandlingAfterNextPointFounded(i, j + 1, false);//在纵边(i,j+1)上
                }
            }
            else
            {
                if (IsContourPoint(m_ySide[i, j]))
                {
                    HandlingAfterNextPointFounded(i, j, false);//在纵边(i,j)上
                }
                else if (IsContourPoint(m_ySide[i, j + 1]))
                {
                    HandlingAfterNextPointFounded(i, j + 1, false);//在纵边(i,j+1)上
                }
                else if (IsContourPoint(m_xSide[i + 1, j]))
                {//两纵边上都没有
                    HandlingAfterNextPointFounded(i + 1, j, true);//在横边(i+1,j)上
                }
                else
                {//三边上都没有,数据插值出现错误
                    //Assert(FALSE);
                }
            }

            return;
        }
        /// <summary>
        /// 自下向上追踪等值线 Version 2
        /// </summary>
        private void FromBottom2TopTracingV2()
        {
            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            //Assert(m_CurrentPoint.i > m_PreviousPoint.i);   //当前点的行号 > 前一点的行号
            //Assert(m_CurrentPoint.bHorV == TRUE);

            if (m_ySide[i, j] < m_ySide[i, j + 1])
            {
                if (m_ySide[i, j] > 0)
                    HandlingAfterNextPointFounded(i, j, false);
                else
                    HandlingAfterNextPointFounded(i, j + 1, false);
            }
            else if (m_ySide[i, j] == m_ySide[i, j + 1])
            {
                if (m_ySide[i, j] < 0)
                {
                    HandlingAfterNextPointFounded(i + 1, j, true);
                }
                else
                {
                    double xSideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i, j + 1] - GridValue[i, j]);
                    if (xSideIJ <= 0.5f)
                    {
                        HandlingAfterNextPointFounded(i, j, false);
                    }
                    else
                    {
                        HandlingAfterNextPointFounded(i, j + 1, false);
                    }
                }
            }
            else if (m_ySide[i, j] > m_ySide[i, j + 1])
            {
                if (m_ySide[i, j + 1] > 0)
                    HandlingAfterNextPointFounded(i, j + 1, false);
                else
                    HandlingAfterNextPointFounded(i, j, false);
            }

            return;
        }

        /// <summary>
        /// 由上向下追踪等值线
        /// </summary>
        private void FromTop2BottomTracing()
        {
            /*   
                                   等前等值点(P2)
                                      ∣
                                      ∣
                            (i,j)     ↓    (i,j+1)
                                |-----·----|
                                |           |
                纵边(i-1,j)---→|           |←----纵边(i-1,j+1)
                ySide[i-1][j]	|           |     ySide[i-1][j+1]
                                |           |
                                |-----------|    
                         (i-1,j)      ↑     (i-1,j+1)
                                      ∣   
                                    横边(i-1,j)
                                   xSide[i-1][j]
	            
            */

            //Assert(m_CurrentPoint.bHorV == TRUE);

            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            if (IsContourPoint(m_ySide[i - 1, j]) && IsContourPoint(m_ySide[i - 1, j + 1]))
            {
                if (m_ySide[i - 1, j] < m_ySide[i - 1, j + 1])
                {
                    HandlingAfterNextPointFounded(i - 1, j, false);//在纵边(i-1,j)上
                }
                else if (m_ySide[i - 1, j] == m_ySide[i - 1, j + 1])
                {
                    //注意:这里需要重新计算xSide[i][j]，因为在上一次已经被置为-2.0
                    double xSideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i, j + 1] - GridValue[i, j]);
                    if (xSideIJ <= 0.5f)
                    {
                        //TRACE0("xSide[i][j] <= 0.5f\n");
                        HandlingAfterNextPointFounded(i - 1, j, false);//在纵边(i-,j)上
                    }
                    else
                    {
                        HandlingAfterNextPointFounded(i - 1, j + 1, false);//在纵边(i-1,j+1)上
                    }
                }
                else
                {
                    HandlingAfterNextPointFounded(i - 1, j + 1, false);//在纵边(i-1,j+1)上
                }
            }
            else
            {
                if (IsContourPoint(m_ySide[i - 1, j]))
                {
                    HandlingAfterNextPointFounded(i - 1, j, false);//在纵边(i-1,j)上
                }
                else if (IsContourPoint(m_ySide[i - 1, j + 1]))
                {
                    HandlingAfterNextPointFounded(i - 1, j + 1, false);//在纵边(i-1,j+1)上
                }
                else if (IsContourPoint(m_ySide[i - 1, j]))
                {//两纵边上都没有
                    HandlingAfterNextPointFounded(i - 1, j, true);//在横边(i-1,j)上
                }
                else
                {//三边上都没有
                    //Assert(FALSE);
                }
            }

            return;
        }
        /// <summary>
        /// 由上向下追踪等值线 Version 2
        /// </summary>
        private void FromTop2BottomTracingV2()
        {
            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            //比较：ySide[i-1][j]      ySide[i-1][j+1]
            //                 
            //               xSide[i-1][j] 

            if (m_ySide[i - 1, j] < m_ySide[i - 1, j + 1])
            {
                if (m_ySide[i - 1, j] > 0)
                    HandlingAfterNextPointFounded(i - 1, j, false);
                else
                    HandlingAfterNextPointFounded(i - 1, j + 1, false);
            }
            else if (m_ySide[i - 1, j] == m_ySide[i - 1, j + 1])
            {
                if (m_ySide[i - 1, j] < 0)
                {
                    HandlingAfterNextPointFounded(i - 1, j, true);//下一点在对面的横边上
                }
                else
                {
                    //注意:这里需要重新计算xSide[i][j]，因为在上一次已经被置为-2.0
                    double xSideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i, j + 1] - GridValue[i, j]);
                    if (xSideIJ <= 0.5f)
                    {
                        HandlingAfterNextPointFounded(i - 1, j, false);
                    }
                    else
                    {
                        HandlingAfterNextPointFounded(i - 1, j + 1, false);
                    }
                }
            }
            else if (m_ySide[i - 1, j] > m_ySide[i - 1, j + 1])
            {
                if (m_ySide[i - 1, j + 1] > 0)
                    HandlingAfterNextPointFounded(i - 1, j + 1, false);
                else
                    HandlingAfterNextPointFounded(i - 1, j, false);
            }
            else
            {
                // no body
            }

            return;
        }

        /// <summary>
        /// 由左向右追踪等值线
        /// </summary>
        private void FromLeft2RightTracing()
        {
            /*   
                                   横边(i+1,j)
                                  xSide[i+1][j]
                                       ∣ 
                                       ∣
                            (i+1,j)    ↓    (i+1,j+1)
                                |-----------|
                                |           |
             等前等值点(P2)--→·           |←----纵边(i,j+1)
                                |           |     ySide[i][j+1]
                                |           |
                                |-----------|    
                            (i,j)     ↑   (i,j+1)
                                      ∣   
                                      横边(i,j)
                                     xSide[i][j]	   
            */

            //Assert(m_CurrentPoint.j > m_PreviousPoint.j);
            //Assert(m_CurrentPoint.bHorV == FALSE);

            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            if (IsContourPoint(m_xSide[i, j]) && IsContourPoint(m_xSide[i + 1, j]))
            {
                if (m_xSide[i, j] < m_xSide[i + 1, j])
                {
                    HandlingAfterNextPointFounded(i, j, true);//在横边(i,j)上
                }
                else if (m_xSide[i, j] == m_xSide[i + 1, j])
                {
                    //注意:这里需要重新计算ySide[i][j]，因为在上一次已经被置为-2.0
                    double ySideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i + 1, j] - GridValue[i, j]);
                    if (ySideIJ <= 0.5f)
                    {
                        //TRACE3("ySide[%d][%d]=%f,左-->右----遇到两边r值相等\n",i,j,*(m_ySide+i*m_ycols+j));
                        HandlingAfterNextPointFounded(i, j, true);//在横边(i,j)上
                    }
                    else
                    {
                        HandlingAfterNextPointFounded(i + 1, j, true);//在横边(i+1,j)上
                    }
                }
                else
                {
                    HandlingAfterNextPointFounded(i + 1, j, true);//在横边(i+1,j)上
                }
            }
            else
            {
                if (IsContourPoint(m_xSide[i, j]))
                {
                    HandlingAfterNextPointFounded(i, j, true);//在横边(i,j)上
                }
                else if (IsContourPoint(m_xSide[i + 1, j]))
                {
                    HandlingAfterNextPointFounded(i + 1, j, true);//在横边(i+1,j)上
                }
                else if (IsContourPoint(m_xSide[i, j + 1]))
                {//两横边上都没有
                    HandlingAfterNextPointFounded(i, j + 1, false);//在纵边(i,j+1)上
                }
                else
                {//三边上都没有
                    //Assert(FALSE);
                }
            }

            return;
        }
        /// <summary>
        /// 由左向右追踪等值线 Version 2
        /// </summary>
        private void FromLeft2RightTracingV2()
        {
            //Assert(m_CurrentPoint.j > m_PreviousPoint.j);
            //Assert(m_CurrentPoint.bHorV == FALSE);

            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            if (m_xSide[i, j] < m_xSide[i + 1, j])
            {
                if (m_xSide[i, j] > 0)
                    HandlingAfterNextPointFounded(i, j, true);
                else
                    HandlingAfterNextPointFounded(i + 1, j, true);
            }
            else if (m_xSide[i, j] == m_xSide[i + 1, j])
            {
                if (m_xSide[i, j] < 0)
                {
                    HandlingAfterNextPointFounded(i, j + 1, false);
                }
                else
                {
                    double ySideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i + 1, j] - GridValue[i, j]);
                    if (ySideIJ <= 0.5f)
                        HandlingAfterNextPointFounded(i, j, true);
                    else
                        HandlingAfterNextPointFounded(i + 1, j, true);
                }
            }
            else if (m_xSide[i, j] > m_xSide[i + 1, j])
            {
                if (m_xSide[i + 1, j] > 0)
                    HandlingAfterNextPointFounded(i + 1, j, true);
                else
                    HandlingAfterNextPointFounded(i, j, true);
            }

            return;
        }

        /// <summary>
        /// 由右向左追踪等值线
        /// </summary>
        private void FromRight2LeftTracing()
        {
            /*   
                                   横边(i+1,j-1)
                                  xSide[i+1][j-1]
                                       ∣ 
                                       ∣
                          (i+1,j-1)    ↓    (i+1,j)
                                |-----------|
                                |           |
                纵边(i,j-1)--→ |           ·←----等前等值点(P2)
               ySide[i][j-1]    |           |     
                                |           |
                                |-----------|    
                          (i,j-1)     ↑   (i,j)
                                      ∣   
                                      横边(i,j-1)
                                     xSide[i][j-1]	   
            */

            //Assert(m_CurrentPoint.bHorV == FALSE);

            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            if (IsContourPoint(m_xSide[i, j - 1]) && IsContourPoint(m_xSide[i + 1, j - 1]))
            {
                if (m_xSide[i, j - 1] < m_xSide[i + 1, j - 1])
                {
                    HandlingAfterNextPointFounded(i, j - 1, true);//在横边(i,j-1)上
                }
                else if (m_xSide[i, j - 1] == m_xSide[i + 1, j - 1])
                {
                    //注意:这里需要重新计算ySide[i][j]，因为在上一次已经被置为-2.0
                    double ySideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i + 1, j] - GridValue[i, j]);
                    if (ySideIJ <= 0.5f)
                    {
                        HandlingAfterNextPointFounded(i, j - 1, true);//在横边(i,j-1)上
                    }
                    else
                    {
                        HandlingAfterNextPointFounded(i + 1, j - 1, true);//在横边(i+1,j-1)上
                    }
                }
                else
                {
                    HandlingAfterNextPointFounded(i + 1, j - 1, true);//在横边(i+1,j-1)上
                }
            }
            else
            {
                if (IsContourPoint(m_xSide[i, j - 1]))
                {
                    HandlingAfterNextPointFounded(i, j - 1, true);//在横边(i,j-1)上
                }
                else if (IsContourPoint(m_xSide[i + 1, j - 1]))
                {
                    HandlingAfterNextPointFounded(i + 1, j - 1, true);//在横边(i+1,j-1)上
                }
                else if (IsContourPoint(m_ySide[i, j - 1]))
                {//两横边上都没有
                    HandlingAfterNextPointFounded(i, j - 1, false);//在纵边(i,j-1)上
                }
                else
                {//三边上都没有
                    //Assert(FALSE);
                }
            }

            return;
        }
        /// <summary>
        /// 由右向左追踪等值线 Version 2
        /// </summary>
        private void FromRight2LeftTracingV2()
        {
            /*   
                               横边(i+1,j-1)
                              xSide[i+1][j-1]
                                   ∣
                      (i+1,j-1)    ↓    (i+1,j)
                            |-----------|
                            |           |
            纵边(i,j-1)--→ |           ·←----等前等值点(P2)
           ySide[i][j-1]    |           |     
                            |           |
                            |-----------|    
                      (i,j-1)     ↑   (i,j)
                                  ∣   
                                  横边(i,j-1)
                                 xSide[i][j-1]	   
            */

            //Assert(m_CurrentPoint.bHorV == FALSE);

            int i = m_CurrentPoint.i;
            int j = m_CurrentPoint.j;

            if (m_xSide[i, j - 1] < m_xSide[i + 1, j - 1])
            {
                if (m_xSide[i, j - 1] > 0)
                    HandlingAfterNextPointFounded(i, j - 1, true);
                else
                    HandlingAfterNextPointFounded(i + 1, j - 1, true);
            }
            else if (m_xSide[i, j - 1] == m_xSide[i + 1, j - 1])
            {
                if (m_xSide[i, j - 1] < 0)
                {
                    HandlingAfterNextPointFounded(i, j - 1, false);
                }
                else
                {
                    double ySideIJ = (this.TraceValue - GridValue[i, j]) / (GridValue[i + 1, j] - GridValue[i, j]);
                    if (ySideIJ <= 0.5f)
                        HandlingAfterNextPointFounded(i, j - 1, true);
                    else
                        HandlingAfterNextPointFounded(i + 1, j - 1, true);
                }
            }
            else if (m_xSide[i, j - 1] > m_xSide[i + 1, j - 1])
            {
                if (m_xSide[i + 1, j - 1] > 0)
                    HandlingAfterNextPointFounded(i + 1, j - 1, true);
                else
                    HandlingAfterNextPointFounded(i, j - 1, true);
            }

            return;
        }

        /// <summary>
        /// 当下一个等值点找到后做相应的处理
        /// </summary>
        /// <param name="i">等值点所在边的编号(行)</param>
        /// <param name="j">等值点所在边的编号(列)</param>
        /// <param name="bHorizon">指明所在边是横边还是纵边</param>
        /// <remarks>
        /// <para>当下一个等值点找到后做相应的处理,如下：</para>
        /// <para>1.记录该等值点的i,j</para>
        /// <para>2.计算并保存该等值点的坐标</para>
        /// <para>3.标志该等值点所在边的已经搜索过</para>
        /// </remarks>
        private void HandlingAfterNextPointFounded(int i, int j, Boolean bHorizon)
        {
            int rows = Grid.Height;
            int cols = Grid.Width;

            //验证i∈[0,rows-1], j∈[0,cols-1]
            if (!(i >= 0 && i <= rows - 1 && j >= 0 && j <= cols - 1))
                return;

            //1.记录该等值点的i,j
            m_NextPoint.i = i;
            m_NextPoint.j = j;
            m_NextPoint.bHorV = bHorizon;

            //2.计算并保存该等值点的坐标
            CalcOnePointCoord(i, j, bHorizon, ref m_NextPoint.x, ref m_NextPoint.y);

            // 3.标志该等值点所在边的已经搜索过
            if (m_NextPoint.bHorV)
            {
                m_xSide[i, j] = NotExistContourPoint;   // 已经追踪过
            }
            else
            {
                m_ySide[i, j] = NotExistContourPoint;   // 已经追踪过
            }

            return;
        }

        #endregion
    }
}
