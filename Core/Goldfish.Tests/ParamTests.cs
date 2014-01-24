using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldfish.Tests
{
	/// <summary>
	/// Integration tests for the param repository.
	/// </summary>
	[TestClass]
	public class ParamTests
	{
		public ParamTests() {
			App.Init();
		}

		[TestMethod, TestCategory("Repositories")]
		public void ParamRepository() {
			var id = Guid.Empty;

			// Insert param
			using (var api = new Api()) {
				var param = new Models.Param() {
					InternalId = "MY_PARAM",
					Name = "My param",
					Value = "23"
				};
				api.Params.Add(param);

				Assert.AreEqual(param.Id.HasValue, true);
				Assert.IsTrue(api.SaveChanges() > 0);

				id = param.Id.Value;
			}

			// Get by internal id
			using (var api = new Api()) {
				var param = api.Params.GetByInternalId("MY_PARAM");

				Assert.IsNotNull(param);
				Assert.AreEqual(param.Value, "23");
			}

			// Update param
			using (var api = new Api()) {
				var param = api.Params.GetByInternalId("MY_PARAM");

				Assert.IsNotNull(param);

				param.Value = "24";
				api.Params.Add(param);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Get by id
			using (var api = new Api()) {
				var param = api.Params.GetById(id);

				Assert.IsNotNull(param);
				Assert.AreEqual(param.Value, "24");
			}

			// Remove param
			using (var api = new Api()) {
				var param = api.Params.GetByInternalId("MY_PARAM");

				api.Params.Remove(param);

				Assert.IsTrue(api.SaveChanges() > 0);
			}

			// Remove system param
			using (var api = new Api()) {
				var param = api.Params.GetByInternalId("ARCHIVE_PAGE_SIZE");

				try {
					api.Params.Remove(param);

					// The above statement should throw an exception and this
					// this assertion should not be reached.
					Assert.IsTrue(false);
				} catch (Exception ex) {
					Assert.IsTrue(ex is UnauthorizedAccessException);
				}
			}
		}
	}
}
