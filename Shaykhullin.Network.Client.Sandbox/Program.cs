﻿using Shaykhullin.Network;
using System.Threading.Tasks;

namespace Client.Sandbox
{
	class Program
	{
		static void Main()
		{
			var config = new ClientConfig();
			
			config.UseSerializer<JsonSerializer>()
				.UseCompression<GZipCompression>()
				.UseEncryption<AesEncryption>()
				.UseProtocol<TcpProtocol>()
				.UseDependencyContainer<DefaultContainer>();
			
			config.Register<BaseService>()
				.ImplementedBy<DerivedService>()
				.As<Singleton>();
			
			config.When<Connection>()
				.Call<ConnectConfig>();

			var connection = config
				.Create("127.0.0.1", 4000)
				.Connect();

			connection.Send("Hello")
				.To<MessageEvent>()
				.Wait();
		}
		
		class ConnectConfig : IConfig<Connection>
		{
			public void Configure(Connection @event)
			{
				@event.Context
					.When<MessageEvent>()
					.Call<MessageHandler>();
			}
		}
	
		public class MessageEvent : IEvent<string>
		{
			public MessageEvent(IConnection connection, string data)
			{
				Connection = connection;
				Data = data;
			}

			public IConnection Connection { get; }
			public string Data { get; }
		}
	
		public class MessageHandler : IHandler<MessageEvent>
		{
			private readonly BaseService service;

			public MessageHandler(BaseService service)
			{
				this.service = service;
			}
		
			public async Task Handle(MessageEvent @event)
			{
				await @event.Connection 
					.Send(service.GetSomeString())
					.To<MessageEvent>();
			}
		}
	
		public class BaseService
		{
			public string GetSomeString() => "";
		}

		public class DerivedService : BaseService
		{
		}
	}
}