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
using System.Timers;

namespace CSharpKit.Utils
{
    /// <summary>
    /// TimeKeeper
    /// </summary>
    public sealed class TimeKeeper
    {
        #region Constructors

        private TimeKeeper() { }

        #endregion

        #region Fields

        private static Timer _timer = null;
        private static float _interval = 15;

        #endregion

        #region Properties

        private static bool _Enabled = false;
        /// <summary>
        /// 使能
        /// </summary>
        public static bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled == value) return;
                _Enabled = value;
                if (_Enabled) Start();
                else Stop();
                return;
            }
        }

        private static DateTime _CurrentTimeUtc = DateTime.Now.ToUniversalTime();
        /// <summary>
        /// 当前 UTC 时间
        /// </summary>
        public static DateTime CurrentTimeUtc
        {
            get { return _CurrentTimeUtc; }
            set { _CurrentTimeUtc = value; }
        }

        static float _TimeMultiplier = 1.0f;
        /// <summary>
        /// 
        /// </summary>
        public static float TimeMultiplier
        {
            get { return _TimeMultiplier; }
            set { _TimeMultiplier = value; }
        }

        #endregion

        #region Events

        public static event System.Timers.ElapsedEventHandler Elapsed;

        #endregion

        #region Public Functions

        /// <summary>
        /// 开始
        /// </summary>
        public static void Start()
        {
            _Enabled = true;
            if (_timer == null)
            {
                _timer = new System.Timers.Timer(_interval);
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
            }
            _timer.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            _Enabled = false;
            if (_timer != null)
                _timer.Stop();
        }

        #endregion

        #region Private Functions

        private static void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _CurrentTimeUtc += System.TimeSpan.FromMilliseconds(_interval * _TimeMultiplier);

            if (Elapsed != null)
                Elapsed(sender, e);

            return;
        }

        #endregion
    }



}

