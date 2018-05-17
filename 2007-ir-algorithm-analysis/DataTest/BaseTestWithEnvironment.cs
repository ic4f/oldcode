using System;
using System.Collections;
using System.IO;
using NUnit.Framework;

namespace Giggle.DataTest
{
	public abstract class BaseTestWithEnvironment
	{
		[SetUp]
		public virtual void SetUp()
		{
			eb = new EnvironmentBuilder();
		}

		[TearDown]
		public void TearDown()
		{
			eb.Close();
		}

		private EnvironmentBuilder eb;
	}
}
