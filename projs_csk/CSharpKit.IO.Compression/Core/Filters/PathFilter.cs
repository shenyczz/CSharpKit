using System.IO;

namespace CSharpKit.IO.Compression.Core
{
	/// <summary>
	/// 路径过滤器<br/>
	/// 路径过滤器使用一种形式的正则表达式通过完整的路径名过滤目录和文件。
	/// </summary>
	public class PathFilter : IScanFilter
	{
		#region Constructors

		/// <summary>
		/// Initialise a new instance of <see cref="PathFilter"></see>.
		/// </summary>
		/// <param name="filter">The <see cref="NameFilter">filter</see> expression to apply.</param>
		public PathFilter(string filter)
		{
			nameFilter_ = new NameFilter(filter);
		}

		#endregion Constructors

		#region IScanFilter Members

		/// <summary>
		/// Test a name to see if it matches the filter.
		/// </summary>
		/// <param name="name">The name to test.</param>
		/// <returns>True if the name matches, false otherwise.</returns>
		/// <remarks><see cref="Path.GetFullPath(string)"/> is used to get the full path before matching.</remarks>
		public virtual bool IsMatch(string name)
		{
			bool result = false;

			if (name != null)
			{
				string cooked = (name.Length > 0) ? Path.GetFullPath(name) : "";
				result = nameFilter_.IsMatch(cooked);
			}
			return result;
		}

		private readonly

		#endregion IScanFilter Members

		#region Instance Fields

		NameFilter nameFilter_;

		#endregion Instance Fields
	}



}
