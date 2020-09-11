using System;
using System.Configuration;
using System.IO;
using System.Security;
using System.Diagnostics;
using System.Reflection;

namespace CSharpKit.Native.HDF
{
    /// <summary>
    /// H5本地依赖 
    /// </summary>
    public sealed class H5NativeDependency : NativeDependency
    {
        H5NativeDependency() {}


        #region Fields - Constant

        /// <summary>
        /// 本地依赖库绝对路径
        /// </summary>
        /// <remarks>
        /// 在App.config中设置
        ///     <appSettings>
        ///     <add key = "NativeDependencyAbsolutePath" value="d:\bin" />
        ///     </appSettings>
        /// </remarks>
        private const string NativePathSetting = "NativeDependencyAbsolutePath";

        // 定义三方库相对路径，用于添加环境路径（PATH）
        //
        private const string _lib_name = "HDF5";
        private const string _lib_version = "1.10.5.2";
        private const string _lib_path = _lib_dir + "\\" + _lib_name + "\\" + _lib_version + "\\";
                
        private const string LIB_PATH_X86 = _lib_path + "x86" + "\\";
        private const string LIB_PATH_X64 = _lib_path + "x64" + "\\";

        public const string Version = _lib_version;

        // Path.DirectorySeparatorChar

        #endregion


        #region Public Functions

        /// <summary>
        /// 外部依赖库相对路径
        /// </summary>
        public static void ResolvePathToExternalDependencies()
        {
            // 取得 Dll 路径
            if (!GetDllPathFromAppConfig(out string nativeDllPath))
            {
                GetDllPathFromAssembly(out nativeDllPath);
            }

            // 添加路径到系统环境中
            AddPathStringToEnvironment(nativeDllPath);

#if DEBUG
            Debug.WriteLine(string.Format("NativeDllPath = {0}", nativeDllPath));
#endif
        }

        #endregion


        #region Private Functions

        /// <summary>
        /// 从 AppConfig 取得库路径
        /// </summary>
        /// <param name="dllPath"></param>
        /// <returns></returns>
        private static bool GetDllPathFromAppConfig(out string dllPath)
        {
            dllPath = string.Empty;
            return false;
            /*
            dllPath = string.Empty;

            try
            {
                if (ConfigurationManager.AppSettings.Count <= 0)
                    return false;

                string pathFromAppSettings = ConfigurationManager.AppSettings[NativePathSetting].ToString();
                if (string.IsNullOrEmpty(pathFromAppSettings))
                {
                    return false;
                }

                foreach (var c in Path.GetInvalidPathChars())
                {
                    if (pathFromAppSettings.Contains(new string(c, 1)))
                    {
                        return false;
                    }
                }

                dllPath = pathFromAppSettings;
                return true;
            }
            catch
            {
                return false;
            }
            */
        }

        /// <summary>
        /// 从程序集取得库路径
        /// </summary>
        /// <param name="dllPath"></param>
        private static void GetDllPathFromAssembly(out string dllPath)
        {
            //Environment.Is64BitOperatingSystem;
            switch (IntPtr.Size)
            {
                case 4: // 32位
                    dllPath = Path.Combine(Path.GetDirectoryName(GetAssemblyName()), LIB_PATH_X86);
                    break;

                case 8: // 64位
                    dllPath = Path.Combine(Path.GetDirectoryName(GetAssemblyName()), LIB_PATH_X64);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 取得程序集名称
        /// </summary>
        /// <returns></returns>
        private static string GetAssemblyName()
        {
            // 程序集绝对路径
            string myPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            // 转换为非转义表示形式
            myPath = Uri.UnescapeDataString(myPath);
            return myPath;
        }

        /// <summary>
        /// 添加路径到环境
        /// </summary>
        /// <param name="aPath">库路径</param>
        private static void AddPathStringToEnvironment(string aPath)
        {
            try
            {
                // 环境变量 - PATH
                string envPath = Environment.GetEnvironmentVariable("PATH");
                if (!envPath.Contains(aPath))
                {
                    Environment.SetEnvironmentVariable("PATH", aPath + ";" + envPath);
                }
            }
            catch (SecurityException)
            {
                System.Diagnostics.Debug.WriteLine("Changing PATH not allowed");
            }
        }

        #endregion

        //}}@@@
    }

}
