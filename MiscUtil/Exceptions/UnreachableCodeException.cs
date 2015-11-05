// Added by Chris Akridge
using System;
using System.Runtime.Serialization;

namespace MiscUtil.Exceptions
{
	/// <summary>
	/// An exception to use in places that are unreachable but the C# compiler does not think they are.
	/// Seeing this is a "world-is-seriously-broken" failure.
	/// </summary>
	// see also http://stackoverflow.com/a/16966572/2709212
	[Serializable]
	public class UnreachableCodeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		public UnreachableCodeException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public UnreachableCodeException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public UnreachableCodeException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected UnreachableCodeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
