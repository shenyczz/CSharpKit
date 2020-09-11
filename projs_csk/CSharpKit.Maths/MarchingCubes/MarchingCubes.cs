using System;
using System.Linq;
using System.Collections.Generic;

namespace CSharpKit.Maths
{
    public sealed class MarchingCubes
    {
        public MarchingCubes()
        {
            _Positions = new List<Double3>();
            _Normals = new List<Double3>();
            _Indices = new List<UInt32>();
            _VertexScalars = new List<Double>();
        }


        #region Fields - Static

        static readonly int[] CASE_MASK = new int[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
        static readonly int[][] EDGES = new int[12][]
        {
                new int[2]{ 0,1 }, new int[2]{ 1,2 }, new int[2]{ 3,2 }, new int[2]{ 0,3 }, // 0 - 3
                new int[2]{ 4,5 }, new int[2]{ 5,6 }, new int[2]{ 7,6 }, new int[2]{ 4,7 }, // 4 - 7
                new int[2]{ 0,4 }, new int[2]{ 1,5 }, new int[2]{ 3,7 }, new int[2]{ 2,6 }, // 8 - 11
        };

        #endregion


        #region Positions - 顶点位置
    
        private List<Double3> _Positions;
        /// <summary>
        /// 顶点位置
        /// </summary>
        public List<Double3> Positions => _Positions;

        #endregion

        #region Normals - 顶点法线

        private List<Double3> _Normals;
        /// <summary>
        /// 顶点法线
        /// </summary>
        public List<Double3> Normals => _Normals;

        #endregion
        
        #region Indices - 顶点索引
    
        private readonly List<UInt32> _Indices;
        /// <summary>
        /// 顶点索引
        /// </summary>
        public List<UInt32> Indices => _Indices;

        #endregion

        #region VertexScalars - 顶点标量

        private List<Double> _VertexScalars;
        /// <summary>
        /// 顶点标量值
        /// </summary>
        public List<double> VertexScalars => _VertexScalars;

        #endregion


        /// <summary>
        /// MarchingCube Parameter
        /// </summary>
        public class MCP
        {
            /// <summary>
            /// 三维立方体网格标量数据
            /// </summary>
            public IEnumerable<double> Scalars { get; set; }
            /// <summary>
            /// 三维立方体网格维度
            /// </summary>
            public IEnumerable<int> Dimensions { get; set; }
            /// <summary>
            /// 等值面值
            /// </summary>
            public IEnumerable<double> IsoValues { get; set; }
            /// <summary>
            /// 原点
            /// </summary>
            public Double3 Origin { get; set; }
            /// <summary>
            /// 间隔
            /// </summary>
            public Double3 Spacing { get; set; }

            /// <summary>
            /// 对齐原点
            /// </summary>
            public bool AllignOrigin { get; set; }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_scalars">标量数据集</param>
        /// <param name="_dims">数据维度</param>
        /// <param name="_origin">原点</param>
        /// <param name="_spacing">间距</param>
        /// <param name="_values">等值面值集合</param>
        /// <remarks>
        /// 立方体：8个顶点、6个面、12条边、12个三角形、36个顶点索引
        ///
        ///      z
        ///      |
        ///      |
        ///      4_______5
        ///     /|      /|
        ///    7_|_____6 |
        ///    | 0_ _ _|_1____x
        ///    | /     |/
        ///    |3______2
        ///    /
        ///   / 
        ///  y   
        ///            
        /// </remarks>
        public void Building(IEnumerable<double> _scalars, IEnumerable<int> _dims, Double3 _origin, Double3 _spacing, IEnumerable<double> _values)
        {
            // 清除老数据
            Clear();

            // 标量数据集
            double[] scalars = _scalars.ToArray();
            // 数据维度
            int[] dims = _dims.ToArray();
            // 原点
            double[] origin = _origin.ToArray();
            // 间距
            double[] spacing = _spacing.ToArray();
            // 等值面值集合
            double[] values = _values.ToArray();

            // 小方块数量
            int iCubeNX = dims[0];
            int iCubeNY = dims[1];
            int iCubeNZ = dims[2];

            // 小方块大小
            double dCubeCX = spacing[0];
            double dCubeCY = spacing[1];
            double dCubeCZ = spacing[2];

            // 大方块大小
            double BigCubeCX = dCubeCX * iCubeNX;
            double BigCubeCY = dCubeCY * iCubeNY;
            double BigCubeCZ = dCubeCZ * iCubeNZ;

            // 
            double x0 = origin[0];
            double y0 = origin[1];
            double z0 = origin[2];

            // 小立方体8个顶点要素值
            double[] s = new double[8];

            // 小立方体8个顶点坐标、梯度
            double[][] pts = new double[8][]
            {
                new double[3], new double[3], new double[3], new double[3],
                new double[3], new double[3], new double[3], new double[3],
            };

            // 小立方体8个顶点梯度
            double[][] gradients = new double[8][]
            {
                new double[3], new double[3], new double[3], new double[3],
                new double[3], new double[3], new double[3], new double[3],
            };

            // 切片数量和尺寸
            int sliceNums = dims[2];
            int sliceSize = dims[0] * dims[1];

            // ?
            int[] ptIds1 = new int[3];         // 顶点id
            double[] xyz = new double[3];     // 坐标
            double[] n = new double[3];       // 梯度

            //vtkInformation *inInfo = self->GetExecutive()->GetInputInformation(0, 0);
            //inInfo->Get(vtkStreamingDemandDrivenPipeline::WHOLE_EXTENT(), extent);
            int[] extent=new int[6];

            // 求等高线最大最小值
            int numValues = values.Length;
            if (numValues < 1)
                return;

            double min = values[0];
            double max = values[0];
            for (int i = 1; i < numValues; i++)
            {
                if (values[1] < min)
                {
                    min = values[1];
                }
                if (values[i] > max)
                {
                    max = values[i];
                }
            }

            // 遍历所有的体素细胞，使用MC算法生成三角形和点梯度
            for (int k = 0; k < iCubeNZ - 1; k++)
            {
                // 面偏移
                int kOffset = k * sliceSize;

                // z坐标
                //pts[0][2] = origin[2] + (k+extent[4]) * spacing[2];
                pts[0][2] = origin[2] + k * spacing[2];
                double zp = pts[0][2] + spacing[2];

                for (int j = 0; j < iCubeNY - 1; j++)
                {
                    // 行偏移
                    int jOffset = j * dims[0];

                    // y坐标
                    //pts[0][1] = origin[1] + (j + extent[2]) * spacing[1];
                    pts[0][1] = origin[1] + j * spacing[1];
                    double yp = pts[0][1] + spacing[1];

                    for (int i = 0; i < iCubeNX - 1; i++)
                    {
                        // 取得小立方体的8个顶点的标量值
                        int idx = i + jOffset + kOffset;

                        s[0] = scalars[idx];
                        s[1] = scalars[idx + 1];
                        s[2] = scalars[idx + dims[0] + 1];
                        s[3] = scalars[idx + dims[0]];

                        s[4] = scalars[idx + sliceSize];
                        s[5] = scalars[idx + sliceSize + 1];
                        s[6] = scalars[idx + sliceSize + dims[0] + 1];
                        s[7] = scalars[idx + sliceSize + dims[0]];

                        // 要素值不在范围内
                        if ((s[0] < min && s[1] < min && s[2] < min && s[3] < min &&
                            s[4] < min && s[5] < min && s[6] < min && s[7] < min) ||
                            (s[0] > max && s[1] > max && s[2] > max && s[3] > max &&
                                s[4] > max && s[5] > max && s[6] > max && s[7] > max))
                        {
                            continue;
                        }

                        //create voxel points
                        //pts[0][0] = origin[0] + (i + extent[0]) * spacing[0];
                        pts[0][0] = origin[0] + i * spacing[0];
                        double xp = pts[0][0] + spacing[0];

                        pts[1][0] = xp;
                        pts[1][1] = pts[0][1];
                        pts[1][2] = pts[0][2];

                        pts[2][0] = xp;
                        pts[2][1] = yp;
                        pts[2][2] = pts[0][2];

                        pts[3][0] = pts[0][0];
                        pts[3][1] = yp;
                        pts[3][2] = pts[0][2];

                        pts[4][0] = pts[0][0];
                        pts[4][1] = pts[0][1];
                        pts[4][2] = zp;

                        pts[5][0] = xp;
                        pts[5][1] = pts[0][1];
                        pts[5][2] = zp;

                        pts[6][0] = xp;
                        pts[6][1] = yp;
                        pts[6][2] = zp;

                        pts[7][0] = pts[0][0];
                        pts[7][1] = yp;
                        pts[7][2] = zp;

                        // 计算梯度 - gradients[8]
                        bool needGradients = true;
                        if (needGradients)
                        {
                            ComputePointGradient(i, j, k, sliceSize, scalars, dims, spacing, ref gradients[0]);
                            ComputePointGradient(i + 1, j, k, sliceSize, scalars, dims, spacing, ref gradients[1]);
                            ComputePointGradient(i + 1, j + 1, k, sliceSize, scalars, dims, spacing, ref gradients[2]);
                            ComputePointGradient(i, j + 1, k, sliceSize, scalars, dims, spacing, ref gradients[3]);
                            ComputePointGradient(i, j, k + 1, sliceSize, scalars, dims, spacing, ref gradients[4]);
                            ComputePointGradient(i + 1, j, k + 1, sliceSize, scalars, dims, spacing, ref gradients[5]);
                            ComputePointGradient(i + 1, j + 1, k + 1, sliceSize, scalars, dims, spacing, ref gradients[6]);
                            ComputePointGradient(i, j + 1, k + 1, sliceSize, scalars, dims, spacing, ref gradients[7]);
                        }

                        // 等值线数量
                        double svalue = values[0]; //标量值
                        for (int contNum = 0; contNum < numValues; contNum++)
                        {
                            svalue = values[contNum];

                            // 根据8个顶点来计算要用256种情况的哪一种
                            int index = 0;
                            for (int iv = 0; iv < 8; iv++)
                            {
                                if (s[iv] >= svalue)
                                {
                                    index |= CASE_MASK[iv];
                                }
                            }

                            //这两个case是不会生成任何三角形
                            if (index == 0 || index == 255)
                            {
                                continue;
                            }

                            TriangleCase triCase = MarchingCubesTriangleCases.TriangleCases[index];
                            int[] edges = triCase.Edges;

                            // 
                            for (int loop = 0; edges[loop] > -1; loop += 3)
                            {
                                // insert triangle
                                for (int tri = 0; tri < 3; tri++)
                                {
                                    // 线性插值计算坐标 LERP
                                    int[] vert = EDGES[edges[tri+ loop]];
                                    double t = (svalue - s[vert[0]]) / (s[vert[1]] - s[vert[0]]);

                                    // 计算坐标
                                    double[] p1 = pts[vert[0]];
                                    double[] p2 = pts[vert[1]];
                                    xyz[0] = p1[0] + t * (p2[0] - p1[0]);
                                    xyz[1] = p1[1] + t * (p2[1] - p1[1]);
                                    xyz[2] = p1[2] + t * (p2[2] - p1[2]);


                                    // 靠近中心点
                                    double x, y, z;
                                    x = xyz[0];
                                    y = xyz[1];
                                    z = xyz[2];
                                    //x = xyz[0] - BigCubeCX / 2;
                                    //y = xyz[1] - BigCubeCY / 2;
                                    //z = xyz[2] - BigCubeCZ / 2;

                                    // 标准化处理
                                    x /= BigCubeCX;
                                    y /= BigCubeCY;
                                    z /= BigCubeCZ;

                                    // 保存
                                    Double3 pos = new Double3(x, y, z);
                                    _Positions.Add(pos);

                                    // 计算梯度
                                    double[] n1 = gradients[vert[0]];
                                    double[] n2 = gradients[vert[1]];
                                    n[0] = n1[0] + t * (n2[0] - n1[0]);
                                    n[1] = n1[1] + t * (n2[1] - n1[1]);
                                    n[2] = n1[2] + t * (n2[2] - n1[2]);

                                    // 法线
                                    Double3 nom = new Double3(n[0], n[1], n[2]);
                                    nom.Normalize();
                                    _Normals.Add(nom);

                                    // 顶点标量
                                    _VertexScalars.Add(svalue);

                                }// for tri

                                // TODO: 20191216 - 构造索引

                                //
                                // check for degenerate triangle(检查退化三角形)
                                //if (ptIds[0] != ptIds[1] &&
                                //	ptIds[0] != ptIds[2] &&
                                //	ptIds[1] != ptIds[2])
                                //{
                                //	newPolys->InsertNextCell(3, ptIds);
                                //}


                            }////for each triangle
                        }//for all contours
                    }//for i
                }// for j
            }//for k



            // TODO:[20191216,syc]临时索引
            for (uint i = 0; i < _Positions.Count; i++)
            {
                _Indices.Add(i);
            }

            //sw.WriteLine(string.Format("Triangles {0}", mc.Positions.Count / 3));
            //for (int i = 0; i < mc.Positions.Count; i += 3)
            //{
            //    string s = string.Format("{0:D} {1:D} {2:D}", i + 0, i + 1, i + 2);
            //    sw.WriteLine(s);
            //}





            return;
        }




        /// <summary>
        /// 用中心差分法计算点梯度，输出 n[3]
        /// </summary>
        /// <param name="i">x</param>
        /// <param name="j">y</param>
        /// <param name="k">z</param>
        /// <param name="sliceSize">切片大小</param>
        /// <param name="scalars">标量数据</param>
        /// <param name="dims">维度</param>
        /// <param name="spacing">间隔</param>
        /// <param name="n"></param>
        private void ComputePointGradient(int i, int j, int k, int sliceSize,
            double[] scalars, int[] dims, double[] spacing, ref double[] n)
        {
            double[] s = scalars;
            double sp, sm;

            // x-direction
            if (i == 0)
            {
                sp = s[i + 1 + j * dims[0] + k * sliceSize];
                sm = s[i + 0 + j * dims[0] + k * sliceSize];
                n[0] = (sm - sp) / spacing[0];
            }
            else if (i == (dims[0] - 1))
            {
                sp = s[i + j * dims[0] + k * sliceSize];
                sm = s[i - 1 + j * dims[0] + k * sliceSize];
                n[0] = (sm - sp) / spacing[0];
            }
            else
            {
                sp = s[i + 1 + j * dims[0] + k * sliceSize];
                sm = s[i - 1 + j * dims[0] + k * sliceSize];
                n[0] = 0.5 * (sm - sp) / spacing[0];
            }

            // y-direction
            if (j == 0)
            {
                sp = s[i + (j + 1) * dims[0] + k * sliceSize];
                sm = s[i + j * dims[0] + k * sliceSize];
                n[1] = (sm - sp) / spacing[1];
            }
            else if (j == (dims[1] - 1))
            {
                sp = s[i + j * dims[0] + k * sliceSize];
                sm = s[i + (j - 1) * dims[0] + k * sliceSize];
                n[1] = (sm - sp) / spacing[1];
            }
            else
            {
                sp = s[i + (j + 1) * dims[0] + k * sliceSize];
                sm = s[i + (j - 1) * dims[0] + k * sliceSize];
                n[1] = 0.5 * (sm - sp) / spacing[1];
            }

            // z-direction
            if (k == 0)
            {
                sp = s[i + j * dims[0] + (k + 1) * sliceSize];
                sm = s[i + j * dims[0] + k * sliceSize];
                n[2] = (sm - sp) / spacing[2];
            }
            else if (k == (dims[2] - 1))
            {
                sp = s[i + j * dims[0] + k * sliceSize];
                sm = s[i + j * dims[0] + (k - 1) * sliceSize];
                n[2] = (sm - sp) / spacing[2];
            }
            else
            {
                sp = s[i + j * dims[0] + (k + 1) * sliceSize];
                sm = s[i + j * dims[0] + (k - 1) * sliceSize];
                n[2] = 0.5 * (sm - sp) / spacing[2];
            }

            return;
        }





        /// <summary>
        /// 清除数据数据
        /// </summary>
        public void Clear()
        {
            Positions.Clear();
            Normals.Clear();
            Indices.Clear();
            VertexScalars.Clear();
        }













        /*

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scalars">三维格点标量值</param>
        /// <param name="dims">x,y,z方向维度</param>
        /// <param name="origin">中心点坐标？</param>
        /// <param name="spacing">小方块跨度：x,y,z</param>
        /// <param name="isovalues">要追踪的等值面</param> isovalues
        /// <remarks>
        /// 立方体：8个顶点、6个面、12条边、12个三角形、36个顶点索引
        ///
        ///      z
        ///      |
        ///      |
        ///      4_______5
        ///     /|      /|
        ///    7_|_____6 |
        ///    | 0_ _ _|_1_____x
        ///    | /     |/
        ///    |3______2
        ///    /
        ///   / 
        ///  y   
        ///            
        /// </remarks>
        public void Building_1(double[] scalars,
            TVector3<int> dims,
            TVector3<double> origin,
            TVector3<double> spacing,
            double[] isovalues)
        {
            // 小方块数量
            int iCubeNX = dims.X;
            int iCubeNY = dims.Y;
            int iCubeNZ = dims.Z;

            // 小方块大小
            double dCubeCX = spacing.X;
            double dCubeCY = spacing.Y;
            double dCubeCZ = spacing.Z;

            // 大方块大小
            double BigCubeCX = dCubeCX * iCubeNX;
            double BigCubeCY = dCubeCY * iCubeNY;
            double BigCubeCZ = dCubeCZ * iCubeNZ;

            // 中心点坐标
            //double x0 = origin.X;
            //double y0 = origin.Y;
            //double z0 = origin.Z;

            // 小立方体8个顶点要素值
            double[] s = new double[8];

            // 小立方体8个顶点坐标、梯度
            TVector3<double>[] pts = new TVector3<double>[8];

            // 小立方体8个顶点梯度
            TVector3<double>[] gradients = new TVector3<double>[8];

            // 切片数量和尺寸
            int sliceNums = dims.Z;
            int sliceSize = dims.X * dims.Y;

            // ?
            //int ptIds[3];   // 顶点id

            //TVector3<double> xyz = new TVector3<double>();  // 坐标

            //int extent[6];
            //vtkInformation *inInfo = self->GetExecutive()->GetInputInformation(0, 0);
            //inInfo->Get(vtkStreamingDemandDrivenPipeline::WHOLE_EXTENT(), extent);

            TriangleCase[] triCases = MarchingCubesTriangleCases.TriangleCases;

            // 求等高线最大最小值
            int numValues = isovalues.Length;
            if (numValues < 1)
                return;

            double min = isovalues[0];
            double max = isovalues[0];
            for (int i = 1; i < numValues; i++)
            {
                if (isovalues[i] < min)
                {
                    min = isovalues[i];
                }
                if (isovalues[i] > max)
                {
                    max = isovalues[i];
                }
            }

            // 遍历所有的体素细胞，使用MC算法生成三角形和点梯度
            for (int k = 0; k < iCubeNZ - 1; k++)
            {
                // 面偏移
                int kOffset = k * sliceSize;

                // z坐标
                pts[0].Z = origin.Z + k * spacing.Z;
                double zp = pts[0].Z + spacing.Z;

                for (int j = 0; j < iCubeNY; j++)
                {
                    // 行偏移
                    int jOffset = j * dims.X;

                    // y坐标
                    pts[0].Y = origin.Y + j * spacing.Y;
                    double yp = pts[0].Y + spacing.Y;

                    for (int i = 0; i < iCubeNX; i++)
                    {
                        // 取得小立方体的8个顶点的标量值
                        int idx = kOffset + jOffset + i;

                        s[0] = scalars[idx];
                        s[1] = scalars[idx + 1];
                        s[2] = scalars[idx + dims.X + 1];
                        s[3] = scalars[idx + dims.X];

                        s[4] = scalars[idx + sliceSize];
                        s[5] = scalars[idx + sliceSize + 1];
                        s[6] = scalars[idx + sliceSize + dims.X + 1];
                        s[7] = scalars[idx + sliceSize + dims.X];

                        // 要素值不在范围内
                        if ((s[0] < min && s[1] < min && s[2] < min && s[3] < min &&
                            s[4] < min && s[5] < min && s[6] < min && s[7] < min) ||
                            (s[0] > max && s[1] > max && s[2] > max && s[3] > max &&
                                s[4] > max && s[5] > max && s[6] > max && s[7] > max))
                        {
                            continue;
                        }

                        // x坐标
                        pts[0].X = origin.X + i * spacing.X;
                        double xp = pts[0].X + spacing.X;

                        pts[1].X = xp;
                        pts[1].Y = pts[0].Y;
                        pts[1].Z = pts[0].Z;

                        pts[2].X = xp;
                        pts[2].Y = yp;
                        pts[2].Z = pts[0].Z;

                        pts[3].X = pts[0].X;
                        pts[3].Y = yp;
                        pts[3].Z = pts[0].Z;

                        pts[4].X = pts[0].X;
                        pts[4].Y = pts[0].Y;
                        pts[4].Z = zp;

                        pts[5].X = xp;
                        pts[5].Y = pts[0].Y;
                        pts[5].Z = zp;

                        pts[6].X = xp;
                        pts[6].Y = yp;
                        pts[6].Z = zp;

                        pts[7].X  = pts[0].X;
                        pts[7].Y  = yp;
                        pts[7].Z = zp;

                        // 计算梯度 - gradients[8]
                        bool needGradients = true;
                        if (needGradients)
                        {
                            ComputePointGradient(i, j, k, sliceSize, scalars, dims, spacing, out gradients[0]);
                            ComputePointGradient(i + 1, j, k, sliceSize, scalars, dims, spacing, out gradients[1]);
                            ComputePointGradient(i + 1, j + 1, k, sliceSize, scalars, dims, spacing, out gradients[2]);
                            ComputePointGradient(i, j + 1, k, sliceSize, scalars, dims, spacing, out gradients[3]);
                            ComputePointGradient(i, j, k + 1, sliceSize, scalars, dims, spacing, out gradients[4]);
                            ComputePointGradient(i + 1, j, k + 1, sliceSize, scalars, dims, spacing, out gradients[5]);
                            ComputePointGradient(i + 1, j + 1, k + 1, sliceSize, scalars, dims, spacing, out gradients[6]);
                            ComputePointGradient(i, j + 1, k + 1, sliceSize, scalars, dims, spacing, out gradients[7]);
                        }

                        // 等值面追踪
                        double svalue = isovalues[0]; //标量值
                        for (int contNum = 0; contNum < numValues; contNum++)
                        {
                            svalue = isovalues[contNum];

                            // 根据8个顶点来计算要用256种情况的哪一种
                            int index = 0;
                            for (int ii = 0; ii < 8; ii++)
                            {
                                if (s[ii] >= svalue)
                                {
                                    index |= CASE_MASK[ii]; // 1<<ii
                                }
                            }

                            //这两个case是不会生成任何三角形，直接跳过
                            if (index == 0 || index == 255)
                            {
                                continue;
                            }

                            // 保存值
                            _VertexValues.Add(svalue);

                            TriangleCase triCase = MarchingCubesTriangleCases.TriangleCases[index];
                            int[] edge = triCase.Edges;

                            int loop = 0;
                            while (edge[loop] > -1)
                            {
                                // insert triangle
                                for (int tri = 0; tri < 3; tri++)
                                {
                                    // 线性插值计算坐标 LERP
                                    int[] vert = EDGES[edge[tri]];  // 两个顶点
                                    double t = (svalue - s[vert[0]]) / (s[vert[1]] - s[vert[0]]);

                                    // 计算顶点坐标
                                    TVector3<double> pos = new TVector3<double>();
                                    TVector3<double> p1 = pts[vert[0]];
                                    TVector3<double> p2 = pts[vert[1]];
                                    pos.X = p1.X + t * (p2.X - p1.X);
                                    pos.Y = p1.Y + t * (p2.Y - p1.Y);
                                    pos.Z = p1.Z + t * (p2.Z - p1.Z);

                                    // 靠近中心点
                                    pos.X -= BigCubeCX / 2;
                                    pos.Y -= BigCubeCY / 2;
                                    pos.Z -= BigCubeCZ / 2;

                                    // 保存顶点坐标
                                    _Positions.Add(pos);


                                    // 计算梯度
                                    Vector3 normal = new Vector3();	// 法线
                                    TVector3<double> n1 = gradients[vert[0]];
                                    TVector3<double> n2 = gradients[vert[1]];
                                    normal.X = n1.X + t * (n2.X - n1.X);
                                    normal.Y = n1.Y + t * (n2.Y - n1.Y);
                                    normal.Z = n1.Z + t * (n2.Z - n1.Z);

                                    // 标准化
                                    double len = Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y + normal.Z * normal.Z);
                                    if (len > 0)
                                    {
                                        normal.X /= len;
                                        normal.Y /= len;
                                        normal.Z /= len;
                                    }

                                    // 保存法线
                                    _Normals.Add(normal);  

                                }//for tri

                                // check for degenerate triangle(检查退化三角形)
                                //if (ptIds[0] != ptIds[1] &&
                                //	ptIds[0] != ptIds[2] &&
                                //	ptIds[1] != ptIds[2])
                                //{
                                //	newPolys->InsertNextCell(3, ptIds);
                                //}

                                // next loop
                                loop += 3;
                            }//for each triangle
                        }//for all contours
                    }//for i
                }//for j
            }//for k


            return;
        }

        private void ComputePointGradient(int i, int j, int k, int sliceSize,
            double[] scalars, TVector3<int> dims, TVector3<double> spacing, out TVector3<double> n)
        {
            n.X = 0; n.Y = 0; n.Z = 0;

            double[] s = scalars;
            double sp, sm;

            // x-方向
            if (i == 0)
            {
                sp = s[i + 1 + j * dims.X + k * sliceSize];
                sm = s[i + 0 + j * dims.X + k * sliceSize];
                n.X = (sm - sp) / spacing.X;
            }
            else if (i == (dims.X - 1))
            {
                sp = s[i + j * dims.X + k * sliceSize];
                sm = s[i - 1 + j * dims.X + k * sliceSize];
                n.X = (sm - sp) / spacing.X;
            }
            else
            {
                sp = s[i + 1 + j * dims.X + k * sliceSize];
                sm = s[i - 1 + j * dims.X + k * sliceSize];
                n.X = 0.5 * (sm - sp) / spacing.X;
            }

            // y-direction
            if (j == 0)
            {
                sp = s[i + (j + 1) * dims.X + k * sliceSize];
                sm = s[i + j * dims.X + k * sliceSize];
                n.Y = (sm - sp) / spacing.Y;
            }
            else if (j == (dims.Y - 1))
            {
                sp = s[i + j * dims.X + k * sliceSize];
                sm = s[i + (j - 1) * dims.X + k * sliceSize];
                n.Y = (sm - sp) / spacing.Y;
            }
            else
            {
                sp = s[i + (j + 1) * dims.X + k * sliceSize];
                sm = s[i + (j - 1) * dims.X + k * sliceSize];
                n.Y = 0.5 * (sm - sp) / spacing.Y;
            }

            // z-direction
            if (k == 0)
            {
                sp = s[i + j * dims.X + (k + 1) * sliceSize];
                sm = s[i + j * dims.X + k * sliceSize];
                n.Z = (sm - sp) / spacing.Z;
            }
            else if (k == (dims.Z - 1))
            {
                sp = s[i + j * dims.X + k * sliceSize];
                sm = s[i + j * dims.X + (k - 1) * sliceSize];
                n.Z = (sm - sp) / spacing.Z;
            }
            else
            {
                sp = s[i + j * dims.X + (k + 1) * sliceSize];
                sm = s[i + j * dims.X + (k - 1) * sliceSize];
                n.Z = 0.5 * (sm - sp) / spacing.Z;
            }

            return;
        }


        */



        //@@@
    }




}


