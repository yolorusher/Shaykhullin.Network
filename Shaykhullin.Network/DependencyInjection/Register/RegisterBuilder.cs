﻿namespace Shaykhullin.Network.Core
{
	internal class RegisterBuilder<TRegister> : IRegisterBuilder<TRegister>
		where TRegister : class
	{
		private readonly DependencyDto dependency;

		public RegisterBuilder(DependencyDto dependency)
		{
			this.dependency = dependency;
		}

		public IImplementedByBuilder ImplementedBy<TImplemented>()
			where TImplemented : class, TRegister
		{
			dependency.Implemented = typeof(TImplemented);
			return new ImplementedByBuilder(dependency);
		}

		public void As<TLifetime>()
			where TLifetime : ILifecycle
		{
			new ImplementedByBuilder(dependency)
				.As<TLifetime>();
		}
	}
}
