using Microsoft.Data.SqlClient;
using SharedLibrary.Enum;
using System.Reflection;
using System.Text;

namespace RepositoryLayer.Helper
{
	internal static class BaseRepositoryHelper
	{
		public static StringBuilder SetUpdatingParametersInQuery(StringBuilder updateQuery, Dictionary<string, object> updatingParameters)
		{
			if (updatingParameters.Count > 0)
			{
				var enumerator = updatingParameters.GetEnumerator();
				var hasMoreElement = enumerator.MoveNext();

				var currentElement = enumerator.Current;

				updateQuery.Append($"{currentElement.Key} = @{currentElement.Key} ");

				hasMoreElement = enumerator.MoveNext();

				while (hasMoreElement)
				{
					currentElement = enumerator.Current;

					updateQuery.Append($", {currentElement.Key} = @{currentElement.Key} ");
					hasMoreElement = enumerator.MoveNext();
				}
			}

			return updateQuery;
		}

		public static void GenerateParametersValues(Dictionary<string, object> parameters, SqlCommand sqlCommand)
		{
			foreach (var parameter in parameters)
			{
				sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
			}
		}

		public static string[] GetPropertyNames<TResult>()
		{
			return typeof(TResult).GetProperties(BindingFlags.Public | BindingFlags.Instance)
								  .Select(p => p.Name)
								  .ToArray();
		}
		
	}
}
