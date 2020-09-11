using System;
using System.Collections.Generic;
using System.Drawing;

namespace CSharpKit
{
    public  class AreaGrowSmall
    {
        public  byte[,] grayValue;
        double[,] _dbz;
       
        int height;
        int width;
        int areaNum = 0;
        int mindbz = 10;
        
        public CAllAreaInfo[] AreaGrow(double[,] dbz,int _mindbz)
        {
            List<CAllAreaInfo> list = new List<CAllAreaInfo>();

            mindbz = _mindbz;
            _dbz = dbz.Clone() as double[,];
            height = dbz.GetLength(1);
            width = dbz.GetLength(0);
            grayValue = new byte[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height ; j++)
                {
                    if (_dbz[i, j] <= mindbz)//只要区域点的灰度不是0就不能成为种子点 -- 需要修改
                    {
                        continue;
                    }
                    CAllAreaInfo call = new CAllAreaInfo();
                    Grow(i, j, ref call);//fasle表示这个区域不符合要求

                    if (call.Points.Count > 30)
                    {
                        list.Add(call);
                    }
                 
                }
            }
            return list.ToArray();

        }
        void Grow(int seedx, int seedy, ref CAllAreaInfo tempCall)
        {
            byte[,] bianyuan = new byte[width, height];
            //8领域  x--水平  y--垂直
            //int[] dx = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            //int[] dy = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };

            //4邻域
            int[] dx = new int[] { -1, 0, 0, 1 };
            int[] dy = new int[] { 0, -1, 1, 0 };
            double[,] _dbz2 = new double[width, height];

            //阈值  与种子灰度值相差为3
            int standardValue = 3;//需要修改

            //堆栈
            Stack<int> SeedX = new Stack<int>();
            Stack<int> SeedY = new Stack<int>();

            //当前正在处理的像素点
            int currentPixel_x, currentPixel_y;

            //标记领域的循环变量
            int k = 0;

            //int areaNum = 0;//区域标号

            //----求重心
            int seedxSum = 0;
            int seedySum = 0;

            //--------
            int max_x = 0, min_x = 0;
            int max_y = 0, min_y = 0;

            //处理邻域
            int xx, yy;

            //标记种子
            grayValue[seedx, seedy] = 1;

            areaNum++;//区域标号

            //种子进栈
            SeedX.Push(seedx);
            SeedY.Push(seedy);
            //----
            //计算最大矩形
            max_x = min_x = seedx;
            max_y = min_y = seedy;
            //----

            //加上种子坐标
            seedxSum = seedx;
            seedySum = seedy;

            //SeedPoint.Add(tempPoint);                    
            int temp = 1;

            while (SeedX.Count > 0 || SeedY.Count > 0)//两个栈均为空时 退出
            {
                currentPixel_x = SeedX.Pop();
                currentPixel_y = SeedY.Pop();

               // for (k = 0; k < 8; k++)
                for (k = 0; k < 4; k++)
                {

                    xx = currentPixel_x + dx[k];
                    yy = currentPixel_y + dy[k];

                    //判断点是否在图像区域内
                    if ((xx < height && xx >= 0) && (yy < width && yy >= 0))
                    {
                        //判断是否检查过了
                        if (_dbz[xx, yy] >= mindbz && grayValue[xx, yy] != 1)//没检查
                        {
                            //要生长的区域为黑色区域则grayValue[xx, yy]=0，而 grayValue[seedx, seedy]=1
                            if (Math.Abs(grayValue[xx, yy] - grayValue[seedx, seedy]) <= standardValue)
                            {
                                _dbz2[xx, yy] = _dbz[xx, yy];
                                bianyuan[xx, yy] = 1;
                                grayValue[xx, yy] = 1;
                                _dbz[xx, yy] = 0;
                                SeedX.Push(xx);
                                SeedY.Push(yy);
                                temp++;

                                //重心
                                seedxSum += xx;
                                seedySum += yy;
                                {
                                    if (xx > max_x)
                                        max_x = xx;
                                    if (xx < min_x)
                                        min_x = xx;
                                    if (yy > max_y)
                                        max_y = yy;
                                    if (yy < min_y)
                                        min_y = yy;
                                }
                               
                                //Point p1 = new Point(xx, yy);
                                //tempCall.Points.Add(p1);
                            }
                           
                        }
                     
                    }
                }
            }//while
            
            //保存信息
            tempCall.AreaNum = areaNum;
            Point p = new Point( seedx, seedy);
            tempCall.x = seedx;
            tempCall.y = seedy;
            tempCall.PixelNumber = temp;
            tempCall.Points = Edge(bianyuan);
            int cx, cy;
            Pointxy cp = new Pointxy();
            if (tempCall.Points.Count > 30)
            {
                CenterOfMass(_dbz2, out cx, out cy);
               
                //重心
                cp.x = cx;
                cp.y = cy;
              
            }
            tempCall.CenterPoint = cp;
        }

        public void CenterOfMass(double[,] bianyuan, out int x, out int y)
        {
            x = 0; y = 0;
            //int minx = 99999;
            //int miny = 99999;
            int w = bianyuan.GetLength(0);
            int h = bianyuan.GetLength(1);

            int sum = 0, sumxI = 0, sumyI = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    sumxI += i * (int)bianyuan[i,j];
                    sumyI += j * (int)bianyuan[i, j];
                    sum += (int)bianyuan[i, j];

                }
            }
            x = sumxI / sum;
            y = sumyI / sum;

        }

        /// <summary>
        /// 质心计算
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        public List<Pointxy> Edge(byte[,] bp)
        {
            List<Pointxy> list = new List<Pointxy>();
            byte[,] image = bp;
            int[,] bianyuan = new int[bp.GetLength(0), bp.GetLength(1)];
            int M = bp.GetLength(0);
            int N = bp.GetLength(1);
            int i, j;

            for (i = 1; i < M - 1; i++)
            {
                for (j = 1; j < N - 1; j++)
                {
                    int t = image[i - 1, j] + image[i + 1, j] + image[i, j - 1] + image[i, j + 1];
                    if (t > 0 && t < 4 && image[i, j] == 1)/*周围4个像素值介于1~3之间，*/
                        bianyuan[i, j] = 1;             /*且当前像素为物体，则其必为边界*/
                }
            }

            for (i = 0; i < M; i++)
            {
                for (j = 0; j < N; j++)
                {
                    if (bianyuan[i, j] == 1)
                    {
                        Pointxy p1 = new Pointxy();
                        p1.x = i;
                        p1.y = j;
                        list.Add(p1);
                    }
                }
            }
            return list;
        }


        //@EndOf(AreaGrowSmall)
    }

    public class CAllAreaInfo
    {
        public int x;
        public int y;

        public int AreaNum { get;   set; }
        public int PixelNumber { get;   set; }
        public Pointxy CenterPoint { get;   set; }

        public List<Pointxy> Points = new List<Pointxy>();
    }

    public class Pointxy
    {
        public int x;
        public int y;
 
    }
}
