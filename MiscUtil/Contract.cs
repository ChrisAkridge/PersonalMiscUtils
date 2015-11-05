// Written by Chris Akridge
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiscUtil
{
	public static class Contract
	{
		// Code Contracts look really cool but they also are, well, a bit overkill.
		// Full runtime and static checking is nice and all, but it seems using
		// Code Contracts does add some dependencies or something (ccrewrite comes to
		// mind). But I like the simple syntax.

		// This class contains a bunch of methods similar to the standard Contract class.
		// These methods are basically just wrappers around the if(something is wrong) throw
		// something; method of validating parameters and return values. No fancy static checking,
		// but it does augment one key thing: code readability. Contracts are now explicit to people
		// reading through the code and it's clear what parameters, etc. should be.

		private static void Throw<TException>(string message)
		{
			throw Activator.CreateInstance(typeof(TException), message);
		}

		private static void ThrowContractException(string message)
		{
			Throw<ContractException>(message);
		}

		public void Assert(bool predicate)
		{
			if (!predicate)
			{
				ThrowContractException("An assertion failed.");
			}
		}
	}


	[Serializable]
	public class ContractException : Exception
	{
		public ContractException() { }
		public ContractException(string message) : base(message) { }
		public ContractException(string message, Exception inner) : base(message, inner) { }
		protected ContractException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{ }
	}
}
