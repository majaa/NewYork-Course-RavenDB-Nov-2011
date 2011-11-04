using System;
using System.Configuration;
using Raven.Client;
using Raven.Client.Document;

namespace RavenDB.Course.NewYork._2011.Controllers
{
	public static class DocumentStoreHolder
	{
		private static IDocumentStore documentStore;

		public static IDocumentStore DocumentStore
		{
			get
			{
				if(documentStore != null)
					return documentStore;

				lock (typeof(DocumentStoreHolder))
				{
					if (documentStore != null)
						return documentStore;

					documentStore = new DocumentStore
					{
						ConnectionStringName = ConnectionStringName
					};


					documentStore.Initialize();

				}

				return documentStore;
			}
		}

		private static string ConnectionStringName
		{
			get
			{
				var customConnection = ConfigurationManager.ConnectionStrings[Environment.MachineName] != null;
				var connectionStringName = customConnection ? Environment.MachineName : "RavenDB";
				return connectionStringName;
			}
		}
	}
}