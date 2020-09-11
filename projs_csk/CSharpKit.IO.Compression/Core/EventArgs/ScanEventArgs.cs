using System;

namespace CSharpKit.IO.Compression.Core
{
    /// <summary>
    /// Event arguments for scanning.
    /// </summary>
    public class ScanEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initialise a new instance of <see cref="ScanEventArgs"/>
		/// </summary>
		/// <param name="name">The file or directory name.</param>
		public ScanEventArgs(string name)
		{
			name_ = name;
		}

		#endregion Constructors

		/// <summary>
		/// The file or directory name for this event.
		/// </summary>
		public string Name
		{
			get { return name_; }
		}

		/// <summary>
		/// Get set a value indicating if scanning should continue or not.
		/// </summary>
		public bool ContinueRunning
		{
			get { return continueRunning_; }
			set { continueRunning_ = value; }
		}

		#region Instance Fields

		private string name_;
		private bool continueRunning_ = true;

		#endregion Instance Fields
	}


}
