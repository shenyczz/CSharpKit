using System;

namespace CSharpKit.IO.Compression.Core
{
    /// <summary>
    /// Arguments passed when scan failures are detected.
    /// </summary>
    public class ScanFailureEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initialise a new instance of <see cref="ScanFailureEventArgs"></see>
		/// </summary>
		/// <param name="name">The name to apply.</param>
		/// <param name="e">The exception to use.</param>
		public ScanFailureEventArgs(string name, Exception e)
		{
			name_ = name;
			exception_ = e;
			continueRunning_ = true;
		}

		#endregion Constructors

		/// <summary>
		/// The applicable name.
		/// </summary>
		public string Name
		{
			get { return name_; }
		}

		/// <summary>
		/// The applicable exception.
		/// </summary>
		public Exception Exception
		{
			get { return exception_; }
		}

		/// <summary>
		/// Get / set a value indicating whether scanning should continue.
		/// </summary>
		public bool ContinueRunning
		{
			get { return continueRunning_; }
			set { continueRunning_ = value; }
		}

		#region Instance Fields

		private string name_;
		private Exception exception_;
		private bool continueRunning_;

		#endregion Instance Fields
	}


}
