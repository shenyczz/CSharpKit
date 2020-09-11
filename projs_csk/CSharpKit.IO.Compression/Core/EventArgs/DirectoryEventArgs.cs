namespace CSharpKit.IO.Compression.Core
{
    /// <summary>
    /// Event arguments for directories.
    /// </summary>
    public class DirectoryEventArgs : ScanEventArgs
	{
		#region Constructors

		/// <summary>
		/// Initialize an instance of <see cref="DirectoryEventArgs"></see>.
		/// </summary>
		/// <param name="name">The name for this directory.</param>
		/// <param name="hasMatchingFiles">Flag value indicating if any matching files are contained in this directory.</param>
		public DirectoryEventArgs(string name, bool hasMatchingFiles)
			: base(name)
		{
			hasMatchingFiles_ = hasMatchingFiles;
		}

		#endregion Constructors

		/// <summary>
		/// Get a value indicating if the directory contains any matching files or not.
		/// </summary>
		public bool HasMatchingFiles
		{
			get { return hasMatchingFiles_; }
		}

		private readonly

		#region Instance Fields

		bool hasMatchingFiles_;

		#endregion Instance Fields
	}



}
