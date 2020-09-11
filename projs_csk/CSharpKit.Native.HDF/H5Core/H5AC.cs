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

#region using - LIBHDF

using haddr_t = System.UInt64;
using hbool_t = System.UInt32;
using herr_t = System.Int32;
using htri_t = System.Int32;
using size_t = System.IntPtr;
using ssize_t = System.IntPtr;
using hsize_t = System.UInt64;
using hssize_t = System.Int64;
using time_t = System.UInt64;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;
using off_t = System.Int64;

using H5O_msg_crt_idx_t = System.UInt32;

#if HDF5_VER1_10
using hid_t = System.Int64;
#else
using hid_t = System.Int32;
#endif

#endregion

namespace CSharpKit.Native.HDF
{
    /// <summary>
    /// H5AC - ??
    /// </summary>
    public sealed class H5AC : H5Common
    {
        static H5AC() { H5.open(); }

        public const int CURR_CACHE_CONFIG_VERSION = 1;

        public const int MAX_TRACE_FILE_NAME_LEN = 1024;

        public enum metadata_write_strategy_t : int
        {
            PROCESS_0_ONLY = 0,
            DISTRIBUTED = 1
        }


        /// <summary>
        /// Cache configuration struct used by H5F.[get,set]_mdc_config()
        /// </summary>
        public unsafe struct cache_config_t
        {
            /* general configuration fields: */
            public int version;

            public hbool_t rpt_fcn_enabled;

            public hbool_t open_trace_file;
            public hbool_t close_trace_file;

            public fixed char trace_file_name[MAX_TRACE_FILE_NAME_LEN + 1]; 

            public hbool_t evictions_enabled;

            public hbool_t set_initial_size;
            public size_t initial_size;

            public double min_clean_fraction;

            public size_t max_size;
            public size_t min_size;

            public long epoch_length;

            /* size increase control fields: */
            public H5C.cache_incr_mode incr_mode;

            public double lower_hr_threshold;

            public double increment;

            public hbool_t apply_max_increment;
            public size_t max_increment;

            public H5C.cache_flash_incr_mode flash_incr_mode;
            public double flash_multiple;
            public double flash_threshold;

            /* size decrease control fields: */
            public H5C.cache_decr_mode decr_mode;

            public double upper_hr_threshold;

            public double decrement;

            public hbool_t apply_max_decrement;
            public size_t max_decrement;

            public int epochs_before_eviction;

            public hbool_t apply_empty_reserve;
            public double empty_reserve;


            /* parallel configuration fields: */
            public int dirty_bytes_threshold;
            public int metadata_write_strategy;

            public cache_config_t(int cache_config_version)
            {
                version = cache_config_version;

                rpt_fcn_enabled = 0;

                open_trace_file = 0;
                close_trace_file = 0;

                evictions_enabled = 0;

                set_initial_size = 0;
                initial_size = IntPtr.Zero;

                min_clean_fraction = 0.0;

                max_size = IntPtr.Zero;
                min_size = IntPtr.Zero;

                epoch_length = 0;

                incr_mode = H5C.cache_incr_mode.OFF;

                lower_hr_threshold = 0.0;

                increment = 0.0;

                apply_max_increment = 0;
                max_increment = IntPtr.Zero;

                flash_incr_mode = H5C.cache_flash_incr_mode.OFF;
                flash_multiple = 0.0;
                flash_threshold = 0.0;

                decr_mode = H5C.cache_decr_mode.OFF;

                upper_hr_threshold = 0.0;

                decrement = 0.0;

                apply_max_decrement = 0;
                max_decrement = IntPtr.Zero;

                epochs_before_eviction = 0;

                apply_empty_reserve = 0;
                empty_reserve = 0.0;

                dirty_bytes_threshold = 0;
                metadata_write_strategy = 0;
            }
        }

        public const int CURR_CACHE_IMAGE_CONFIG_VERSION = 1;

        public const int CACHE_IMAGE__ENTRY_AGEOUT__NONE = -1;

        public const int CACHE_IMAGE__ENTRY_AGEOUT__MAX = 100;

        /// <summary>
        /// Cache image configuration struct used by
        /// H5F.[get,set]_mdc_image_config()
        /// </summary>
        public struct cache_image_config_t
        {
            public int version;

            public hbool_t generate_image;

            public hbool_t save_resize_status;

            public int entry_ageout;
        }

        //}}@@@
    }

}
