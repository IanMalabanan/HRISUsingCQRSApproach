using HRIS.Application.Common.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Infrastructure.Services
{
	public class MongoKeyGenerator : IKeyGenerator
	{
		/// <summary>
		/// Generate a unique identifier using MongoDB's ObjectId generator.
		/// </summary>
		public string NewStringKey()
		{
			return ObjectId.GenerateNewId().ToString();
		}
	}
}
