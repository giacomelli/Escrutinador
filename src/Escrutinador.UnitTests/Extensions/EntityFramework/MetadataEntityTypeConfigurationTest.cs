using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Escrutinador.UnitTests.Extensions.EntityFramework
{
    [TestFixture]
    public class MetadataEntityTypeConfigurationTest
    {
        [Test]
        public void AutoConfigure_NoArgs_ConfiguredByMetadata()
        {
            var target = new EntityStubMap();
            Assert.IsNotNull(target);
            var targetType = target.GetType();

            var configuration = targetType.GetProperty("Configuration", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(target);
            var configuredProperties = configuration.GetType()
                .GetProperty("ConfiguredProperties", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .GetValue(configuration) as IEnumerable<object>;

            Assert.AreEqual(3, configuredProperties.Count());
        }
    }
}
