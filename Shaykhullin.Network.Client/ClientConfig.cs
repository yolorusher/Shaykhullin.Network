﻿using Shaykhullin.Network.Core;

namespace Shaykhullin.Network
{
	public class ClientConfig : IClientConfig
	{
		public ISerializerBuilder UseSerializer<TSerializer>() where TSerializer : ISerializer
		{
			throw new System.NotImplementedException();
		}

		public IRegisterBuilder<TRegister> Register<TRegister>() where TRegister : class
		{
			throw new System.NotImplementedException();
		}

		public IHandlerConfig<TEvent> When<TEvent>() where TEvent : IEvent<object>
		{
			throw new System.NotImplementedException();
		}

		public IClient Create(string host, int port)
		{
			throw new System.NotImplementedException();
		}
	}
}