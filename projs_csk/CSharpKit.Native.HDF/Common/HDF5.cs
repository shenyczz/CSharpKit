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
using System.Diagnostics;
using System.Runtime.InteropServices;
using CSharpKit;
using CSharpKit.Utils;

namespace CSharpKit.Native.HDF
{
    public sealed class HDF5
    {
        HDF5() { }
        public static readonly HDF5 Instance = new HDF5();


        #region NativeByteOrder - 本地字节序

        public static H5T.order_t NativeByteOrder => getNativeByteOrder();
        private static H5T.order_t getNativeByteOrder()
        {
            var mem_type = H5T.NATIVE_INT;
            var native_type = H5T.get_native_type(mem_type, H5T.direction_t.DEFAULT);
            var native_order = H5T.get_order(native_type);
            var status = H5T.close(native_type);
            return native_order;
        }

        #endregion



        /// <summary>
        /// 取得对象属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objId">对象标识</param>
        /// <param name="attr_name">属性名称</param>
        /// <param name="result">属性名称对应的属性值</param>
        /// <returns></returns>
        public bool GetAttribValue<T>(long objId, string attr_name, out T result)
        {
            bool ok = false;

            try
            {
                result = default;

                // 数组类型的字节大小
                //int tsize = Marshal.SizeOf(typeof(T));

                long attr = H5A.open(objId, attr_name, H5P.DEFAULT);    // 属性ID
                long atype = H5A.get_type(attr);                        // 属性数据类型
                long aspace = H5A.get_space(attr);                      // 属性数据空间

                H5T.order_t aorder = H5T.get_order(atype);              // 属性数据字节序
                H5T.class_t tclass = H5T.get_class(atype);              // 属性数据类型
                IntPtr psize = H5T.get_size(atype);                     // 数据类型字节大小

                // get attribute rank, and dimensions.
                ulong[] dims = new ulong[64];
                int rank = H5S.get_simple_extent_ndims(aspace);             // 数据空间维度
                int ret = H5S.get_simple_extent_dims(aspace, dims, null);   // 每个维度的长度

                if (tclass == H5T.class_t.INTEGER || tclass == H5T.class_t.FLOAT)
                {
                    long npoints = H5S.get_simple_extent_npoints(aspace);
                    T[] vars_array = new T[npoints];
                    ret = H5A.read(attr, atype, KConverter.ToIntPtr(vars_array));
                    result = (aorder == NativeByteOrder)
                        ? vars_array[0]
                        : vars_array[0].SwapByteOrder();

                    ok = true;
                }
                else if (tclass == H5T.class_t.STRING)
                {
                    long npoints = H5S.get_simple_extent_npoints(aspace);
                    H5T.str_t tstr = H5T.get_strpad(atype);
                    H5T.cset_t cset = H5T.get_cset(atype);
                    byte[] byte_array = new byte[(int)psize];
                    ret = H5A.read(attr, atype, KConverter.ToIntPtr(byte_array));
                    string str = KConverter.Bytes2String(byte_array);
                    result = (T)Convert.ChangeType(str, typeof(T));

                    ok = true;
                }

                // 关闭句柄
                H5T.close(atype);
                H5S.close(aspace);
                H5A.close(attr);

            }
            catch (Exception ex)
            {
                ok = false;
                result = default;
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return ok;
        }






        /// <summary>
        /// 取得对象属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objId">对象标识</param>
        /// <param name="attr_name">属性名称</param>
        /// <param name="result">属性名称对应的属性值数组</param>
        /// <returns></returns>
        public bool GetAttribValue<T>(long objId, string attr_name, out T[] result)
        {
            bool ok = false;

            try
            {
                result = default;

                // 数组类型的字节大小
                int tsize = Marshal.SizeOf(typeof(T));

                // get ids
                long attr = H5A.open(objId, attr_name, H5P.DEFAULT);       // 属性ID
                long atype = H5A.get_type(attr);                            // 属性的数据类型
                long aspace = H5A.get_space(attr);                          // 属性的数据空间
                H5T.order_t aorder = H5T.get_order(atype);

                // get attribute rank, and dimensions.
                ulong[] sdim = new ulong[64];
                int rank = H5S.get_simple_extent_ndims(aspace);             // 数据空间维度
                int ret = H5S.get_simple_extent_dims(aspace, sdim, null);   // 每个维度的长度

                // 属性类型
                H5T.class_t tclass = H5T.get_class(atype);
                if (tclass == H5T.class_t.INTEGER || tclass == H5T.class_t.FLOAT)
                {
                    long npoints = H5S.get_simple_extent_npoints(aspace);
                    T[] vars_array = new T[npoints];
                    ret = H5A.read(attr, atype, KConverter.ToIntPtr(vars_array));
                    result = vars_array;

                    for (int i = 0; i < npoints; i++)
                    {
                        result[i] = (aorder == NativeByteOrder)
                            ? result[i]
                            : result[i].SwapByteOrder();
                    }

                    ok = true;
                }
                else if (tclass == H5T.class_t.STRING)
                {
                    // waiting...
                }

                // 关闭句柄
                H5T.close(atype);
                H5S.close(aspace);
                H5A.close(attr);

            }
            catch (Exception)
            {
                result = default;
                ok = false;
            }

            return ok;
        }



        public void ReadDataset1D(long did, out float[] datas)
        {
            datas = null;

            try
            {
                int status;

                // datatype
                long dtype = H5D.get_type(did);

                H5T.class_t dclass = H5T.get_class(dtype);
                H5T.order_t dorder = H5T.get_order(dtype);
                IntPtr psize = H5T.get_size(dtype);

                // plist
                long plist = H5D.get_create_plist(did);
                int numfilt = H5P.get_nfilters(plist);  // 滤镜数量

                // dataspace
                long dspace = H5D.get_space(did);
                int rank = H5S.get_simple_extent_ndims(dspace);

#if DEBUG
                Debug.Assert(rank > 0);
#endif

                if (rank > 0)
                {
                    ulong[] dims = new ulong[rank];
                    H5S.get_simple_extent_dims(dspace, dims, null);

                    // 读取数据
                    float[] readbuf = new float[dims[0]];
                    status = H5D.read(
                        did,
                        H5T.NATIVE_FLOAT,
                        H5S.ALL,
                        H5S.ALL,
                        H5P.DEFAULT,
                        KConverter.ToIntPtr(readbuf));

                    datas = readbuf;
                }
            }
            catch (Exception)
            {
            }

        }
        public void ReadDataset1D(long did, out double[] datas)
        {
            datas = null;

            try
            {
                int status;

                // datatype
                long dtype = H5D.get_type(did);

                H5T.class_t dclass = H5T.get_class(dtype);
                H5T.order_t dorder = H5T.get_order(dtype);
                IntPtr psize = H5T.get_size(dtype);

                // plist
                long plist = H5D.get_create_plist(did);
                int numfilt = H5P.get_nfilters(plist);  // 滤镜数量

                // dataspace
                long dspace = H5D.get_space(did);
                int rank = H5S.get_simple_extent_ndims(dspace);

#if DEBUG
                Debug.Assert(rank > 0);
#endif

                if (rank > 0)
                {
                    ulong[] dims = new ulong[rank];
                    H5S.get_simple_extent_dims(dspace, dims, null);

                    // 读取数据
                    double[] readbuf = new double[dims[0]];
                    status = H5D.read(
                        did,
                        H5T.NATIVE_DOUBLE,
                        H5S.ALL,
                        H5S.ALL,
                        H5P.DEFAULT,
                        KConverter.ToIntPtr(readbuf));

                    datas = readbuf;
                }
            }
            catch (Exception)
            {
            }

        }


        public void ReadDataset2D(long did, out short[,] datas)
        {
            datas = null;

            try
            {
                int status;
                if (did < 0) return;

                // datatype
                long dtype = H5D.get_type(did);

                // 检查
                H5T.class_t dclass = H5T.get_class(dtype);  // 数据类型
                H5T.order_t dorder = H5T.get_order(dtype);  // 数据字节序
                IntPtr psize = H5T.get_size(dtype);         // 数据类型大小 1,2,4,8

                // plist
                long plist = H5D.get_create_plist(did);
                int numfilt = H5P.get_nfilters(plist);  // 滤镜数量

                // dataspace
                long dspace = H5D.get_space(did);
                int rank = H5S.get_simple_extent_ndims(dspace);

#if DEBUG
                Debug.Assert(rank > 0);
#endif

                if (rank > 0)
                {
                    ulong[] dims = new ulong[rank];
                    H5S.get_simple_extent_dims(dspace, dims, null);

                    // 读取数据
                    var readbuf = new short[dims[0], dims[1]];
                    status = H5D.read(
                        did,
                        H5T.NATIVE_INT16,
                        H5S.ALL,
                        H5S.ALL,
                        H5P.DEFAULT,
                        KConverter.ToIntPtr(readbuf));

                    datas = readbuf;
                }

            }
            catch (Exception)
            {
            }

        }
        public void ReadDataset2D(long did, out ushort[,] datas)
        {
            datas = null;

            try
            {
                int status;
                if (did < 0) return;

                // datatype
                long dtype = H5D.get_type(did);

                // 检查
                H5T.class_t dclass = H5T.get_class(dtype);  // 数据类型
                H5T.order_t dorder = H5T.get_order(dtype);  // 数据字节序
                IntPtr psize = H5T.get_size(dtype);         // 数据类型大小 1,2,4,8

                // plist
                long plist = H5D.get_create_plist(did);
                int numfilt = H5P.get_nfilters(plist);  // 滤镜数量

                // dataspace
                long dspace = H5D.get_space(did);
                int rank = H5S.get_simple_extent_ndims(dspace);

#if DEBUG
                Debug.Assert(rank > 0);
#endif

                if (rank > 0)
                {
                    ulong[] dims = new ulong[64];
                    H5S.get_simple_extent_dims(dspace, dims, null);

                    // 读取数据
                    ushort[,] readbuf = new ushort[dims[0], dims[1]];
                    status = H5D.read(
                        did,
                        H5T.NATIVE_UINT16,
                        H5S.ALL,
                        H5S.ALL,
                        H5P.DEFAULT,
                        KConverter.ToIntPtr(readbuf));

                    datas = readbuf;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

        }
        public void ReadDataset2D(long did, out float[,] datas)
        {
            datas = null;

            try
            {
                int status;
                if (did < 0) return;

                // 数据类型标识
                long dtype = H5D.get_type(did);

                // 检查
                H5T.class_t dclass = H5T.get_class(dtype);  // 数据类型
                H5T.order_t dorder = H5T.get_order(dtype);  // 数据字节序
                IntPtr psize = H5T.get_size(dtype);         // 数据类型大小 1,2,4,8

                // plist
                long plist = H5D.get_create_plist(did);
                int numfilt = H5P.get_nfilters(plist);  // 滤镜数量

                // dataspace
                long dspace = H5D.get_space(did);
                int rank = H5S.get_simple_extent_ndims(dspace);

#if DEBUG
                Debug.Assert(rank > 0);
#endif

                if (rank > 0)
                {
                    ulong[] dims = new ulong[rank];
                    H5S.get_simple_extent_dims(dspace, dims, null);

                    // 读取数据
                    var readbuf = new float[dims[0], dims[1]];
                    status = H5D.read(
                        did,
                        H5T.NATIVE_FLOAT,
                        H5S.ALL,
                        H5S.ALL,
                        H5P.DEFAULT,
                        KConverter.ToIntPtr(readbuf));

                    datas = readbuf;
                }
            }
            catch (Exception)
            {
            }

        }
        public void ReadDataset2D(long did, out double[,] datas)
        {
            datas = null;

            try
            {
                int status;
                if (did < 0) return;

                // 数据类型标识
                long dtype = H5D.get_type(did);

                // 检查
                H5T.class_t dclass = H5T.get_class(dtype);  // 数据类型
                H5T.order_t dorder = H5T.get_order(dtype);  // 数据字节序
                IntPtr psize = H5T.get_size(dtype);         // 数据类型大小 1,2,4,8

                // plist
                long plist = H5D.get_create_plist(did);
                int numfilt = H5P.get_nfilters(plist);  // 滤镜数量

                // dataspace
                long dspace = H5D.get_space(did);
                int rank = H5S.get_simple_extent_ndims(dspace);

#if DEBUG
                Debug.Assert(rank > 0);
#endif

                if (rank > 0)
                {
                    ulong[] dims = new ulong[rank];
                    H5S.get_simple_extent_dims(dspace, dims, null);

                    // 读取数据
                    var readbuf = new double[dims[0], dims[1]];
                    status = H5D.read(
                        did,
                        H5T.NATIVE_DOUBLE,
                        H5S.ALL,
                        H5S.ALL,
                        H5P.DEFAULT,
                        KConverter.ToIntPtr(readbuf));

                    datas = readbuf;
                }
            }
            catch (Exception)
            {
            }

        }


















        // 下面是测试







        /// <summary>
        /// Integer 
        /// </summary>
        /// <param name="obj_id"></param>
        /// <param name="attr_name"></param>
        /// <returns></returns>
        public int GetAttribInteger(long obj_id, string attr_name)
        {
            int result = int.MaxValue;

            try
            {
                // get ids
                long attr = H5A.open(obj_id, attr_name, H5P.DEFAULT);       // 属性id
                long atype = H5A.get_type(attr);                            // 属性类型
                long aspace = H5A.get_space(attr);                          // 属性空间

                // get attribute rank, and dimensions.
                ulong[] sdim = new ulong[64];
                int rank = H5S.get_simple_extent_ndims(aspace);             // 数据空间维度
                int ret = H5S.get_simple_extent_dims(aspace, sdim, null);   // 每个维度的长度

                // 属性类型
                H5T.class_t tclass = H5T.get_class(atype);
                if (H5T.class_t.INTEGER == tclass)
                {
                    long npoints = H5S.get_simple_extent_npoints(aspace);
                    Int32[] int_array = new Int32[npoints];
                    ret = H5A.read(attr, atype, KConverter.ToIntPtr(int_array));
                    for (int i = 0; i < npoints; i++)
                    {
                        Debug.WriteLine(string.Format("\t{0}", int_array[i]));
                    }

                    result = int_array[0];
                }

                H5T.close(atype);
                H5S.close(aspace);
                H5A.close(attr);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
            }


            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj_id"></param>
        /// <param name="attr_name"></param>
        /// <returns></returns>
        public float GetAttribFloat(long obj_id, string attr_name)
        {
            float result = float.NaN;

            try
            {
                // get ids
                long attr = H5A.open(obj_id, attr_name, H5P.DEFAULT);       // 属性id
                long atype = H5A.get_type(attr);                            // 属性类型
                long aspace = H5A.get_space(attr);                          // 属性空间

                // get attribute rank, and dimensions.
                ulong[] sdim = new ulong[64];
                int rank = H5S.get_simple_extent_ndims(aspace);             // 数据空间维度
                int ret = H5S.get_simple_extent_dims(aspace, sdim, null);   // 每个维度的长度

                // 属性类型
                H5T.class_t tclass = H5T.get_class(atype);

                if (H5T.class_t.FLOAT == tclass)
                {
                    long npoints = H5S.get_simple_extent_npoints(aspace);
                    float[] float_array = new float[npoints];
                    ret = H5A.read(attr, atype, KConverter.ToIntPtr(float_array));
                    for (int i = 0; i < npoints; i++)
                    {
                        Debug.WriteLine(string.Format("\t{0:F3}", float_array[i]));
                    }

                    result = float_array[0];
                }

                H5T.close(atype);
                H5S.close(aspace);
                H5A.close(attr);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
            }


            return result;
        }
        /// <summary>
        /// 读取对象字符串属性值
        /// </summary>
        /// <param name="obj_id">对象id</param>
        /// <param name="attr_name">属性名称</param>
        /// <returns>属性值</returns>
        public string GetAttribString(long obj_id, string attr_name)
        {
            string result = string.Empty;

            try
            {
                // get ids
                long attr = H5A.open(obj_id, attr_name, H5P.DEFAULT);       // 属性id
                long atype = H5A.get_type(attr);                            // 属性类型
                long aspace = H5A.get_space(attr);                          // 属性空间

                // get attribute rank, and dimensions.
                ulong[] sdim = new ulong[64];
                int rank = H5S.get_simple_extent_ndims(aspace);             // 数据空间维度
                int ret = H5S.get_simple_extent_dims(aspace, sdim, null);   // 每个维度的长度

                // 属性类型
                H5T.class_t tclass = H5T.get_class(atype);
                if (tclass == H5T.class_t.STRING)
                {
                    long npoints = H5S.get_simple_extent_npoints(aspace);
                    IntPtr psize = H5T.get_size(atype);
                    H5T.str_t tstr = H5T.get_strpad(atype);
                    byte[] byte_array = new byte[(int)psize];
                    ret = H5A.read(attr, atype, KConverter.ToIntPtr(byte_array));
                    result = KConverter.Bytes2String(byte_array);
                }

                H5T.close(atype);
                H5S.close(aspace);
                H5A.close(attr);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Debug.WriteLine(err);
            }


            return result;
        }



        public void AttribInfo(long loc_id, string attr_name, out object attr_value)
        {
            attr_value = null;

            // Open the attribute using its name.
            long attr = H5A.open(loc_id, attr_name, H5P.DEFAULT);
            Debug.WriteLine(string.Format("Attribute name: {0}", attr_name));

            // Get attribute datatype, dataspace, rank, and dimensions.
            //
            long atype = H5A.get_type(attr);
            long aspace = H5A.get_space(attr);

            ulong[] sdim = new ulong[64];
            int rank = H5S.get_simple_extent_ndims(aspace);
            int ret = H5S.get_simple_extent_dims(aspace, sdim, null);

            // Display rank and dimension sizes for the array attribute.
            //
            if (rank > 0)
            {
#if DEBUG
                Debug.WriteLine(string.Format("\tRank =  {0}", rank));
                Debug.Write(string.Format("\tDimension sizes: "));
#endif

                for (int i = 0; i < rank; i++)
                {
                    if (i == 0)
                        Debug.Write(string.Format("\t"));

                    Debug.Write(string.Format("{0}", (int)sdim[i]));

                    if (i < rank - 1)
                        Debug.Write(string.Format("{0}", ","));
                }

#if DEBUG
                Debug.Assert(true);
#endif
            }

            // Read array attribute and display its type and values.
            //
            H5T.class_t tclass = H5T.get_class(atype);
            Debug.WriteLine(string.Format("\tType: {0}", tclass));

            //H5T.get_order()

            // INTEGER、FLOAT、TIME、STRING、
            // BITFIELD、OPAQUE、COMPOUND、REFERENCE、
            // ENUM、VLEN、ARRAY、NCLASSES
            if (H5T.class_t.INTEGER == tclass)
            {
                long npoints = H5S.get_simple_extent_npoints(aspace);
                Int32[] int_array = new Int32[npoints];
                ret = H5A.read(attr, atype, KConverter.ToIntPtr(int_array));
                for (int i = 0; i < npoints; i++)
                {
                    Debug.WriteLine(string.Format("\t{0}", int_array[i]));
                }

                attr_value = int_array;
            }

            if (H5T.class_t.FLOAT == tclass)
            {
                long npoints = H5S.get_simple_extent_npoints(aspace);
                float[] float_array = new float[npoints];
                ret = H5A.read(attr, atype, KConverter.ToIntPtr(float_array));
                for (int i = 0; i < npoints; i++)
                {
                    Debug.WriteLine(string.Format("\t{0:F3}", float_array[i]));
                }

                attr_value = float_array;
            }

            if (H5T.class_t.STRING == tclass)
            {
                long npoints = H5S.get_simple_extent_npoints(aspace);
                IntPtr psize = H5T.get_size(atype);
                H5T.str_t tstr = H5T.get_strpad(atype);
                byte[] byte_array = new byte[(int)psize];
                ret = H5A.read(attr, atype, KConverter.ToIntPtr(byte_array));
                string str = KConverter.Bytes2String(byte_array);
                Debug.WriteLine(string.Format("\t{0}", str));

                attr_value = str;
            }

            H5T.close(atype);
            H5S.close(aspace);
            H5A.close(attr);

            return;
        }





        //}}@@@
    }



}
