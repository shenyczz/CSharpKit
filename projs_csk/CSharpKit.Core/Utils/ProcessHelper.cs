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

namespace CSharpKit.Utils
{
    /// <summary>
    /// ProcessHelper - 进程助手
    /// </summary>
    public sealed class ProcessHelper
    {
        ProcessHelper() { }

        public static string[] ExecCommand(string commands)
        {
            //msg[0]执行结果;msg[1]错误结果  
            string[] msg = new string[2];
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();

                proc.StandardInput.WriteLine(commands);
                proc.StandardInput.WriteLine("exit");

                //执行结果  
                msg[0] = proc.StandardOutput.ReadToEnd();
                proc.StandardOutput.Close();

                //出错结果  
                msg[1] = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();

                //超时等待  
                int maxWaitCount = 10;
                while (proc.HasExited == false && maxWaitCount > 0)
                {
                    proc.WaitForExit(1000);
                    maxWaitCount--;
                }
                if (maxWaitCount == 0)
                {
                    msg[1] = "操作执行超时";
                    proc.Kill();
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg[1] = "进程创建失败:";
                msg[1] += ex.Message.ToString();
                msg[1] += ex.StackTrace.ToString();
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return msg;
        }

        //@EndOf(ProcessHelper)
    }
}
