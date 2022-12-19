using Cura.Notification.Core;
using Cura.Notification.Service.Plugin;
using NSubstitute;
using NUnit.Framework;
using System.Reflection;
using System.Threading.Tasks;

namespace Cura.Notification.Service.Plugin.UnitTest
{
	[TestFixture]
	public class CuraNotificationServiceIntializeTests
	{


		[SetUp]
		public void SetUp()
		{

		}

		private CuraNotificationServiceIntializeCommand CuraNotificationServiceIntializeCommand()
		{
			
			return new CuraNotificationServiceIntializeCommand();
		}

		[Test]
		public void Execute_LoadPlugin_NotificationsPluginLoaded()
		{
			// Arrange
			var curaNotificationServiceIntialize = this.CuraNotificationServiceIntializeCommand();

			// Act
			var result = curaNotificationServiceIntialize.Execute();

			// Assert
			Assert.Fail();
		}

		[Test]
		public async Task ExecuteAsync_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			var curaNotificationServiceIntialize = this.CuraNotificationServiceIntializeCommand();

			// Act
			var result = await curaNotificationServiceIntialize.ExecuteAsync();

			// Assert
			Assert.Fail();
		}
	}
}
