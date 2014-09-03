using NUnit.Framework;

namespace Escrutinador.UnitTests
{
    [TestFixture]
    public class DataAnnotationsMetadataProviderTest
    {
        [Test]
        public void Property_StringProperty_Metadata()
        {
            var target = new DataAnnotationsMetadataProvider();
            var actual = target.Property<DataStub>(d => d.Name);
            Assert.IsNotNull(actual);
            Assert.AreEqual(10, actual.MinLength);
            Assert.AreEqual(50, actual.MaxLength);
            Assert.AreEqual(2, actual.Order);
            Assert.IsTrue(actual.Required);

            actual = target.Property<DataStub>(d => d.Description);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.MinLength);
            Assert.AreEqual(int.MaxValue, actual.MaxLength);
            Assert.AreEqual(1, actual.Order);
            Assert.IsFalse(actual.Required);

            actual = target.Property<DataStub>(d => d.Text);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.MinLength);
            Assert.AreEqual(2, actual.MaxLength);
            Assert.AreEqual(int.MaxValue, actual.Order);
            Assert.IsTrue(actual.Required);
        }

        [Test]
        public void Property_IntProperty_Metadata()
        {
            var target = new DataAnnotationsMetadataProvider();
            var actual = target.Property<DataStub>(d => d.OtherId);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.MinLength);
            Assert.AreEqual(10, actual.MaxLength);
            Assert.AreEqual(int.MaxValue, actual.Order);
            Assert.IsFalse(actual.Required);
        }

        [Test]
        public void Property_UrlProperty_Metadata()
        {
            var target = new DataAnnotationsMetadataProvider();
            var actual = target.Property<DataStub>(d => d.Url);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.MinLength);
            Assert.AreEqual(int.MaxValue, actual.MaxLength);
            Assert.IsFalse(actual.Required);
            Assert.IsTrue(actual.IsUrl);
        }

        [Test]
        public void Property_EnumProperty_Metadata()
        {
            var target = new DataAnnotationsMetadataProvider();
            var actual = target.Property<DataStub>(d => d.DateKind);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.MinLength);
            Assert.AreEqual(int.MaxValue, actual.MaxLength);
            Assert.IsTrue(actual.Required);
            Assert.IsFalse(actual.IsUrl);
        }

        [Test]
        public void Property_IListProperty_Metadata()
        {
            var target = new DataAnnotationsMetadataProvider();
            var actual = target.Property<DataStub>(d => d.Lines);
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.MinLength);
            Assert.AreEqual(int.MaxValue, actual.MaxLength);
            Assert.AreEqual(int.MaxValue, actual.Order);
            Assert.True(actual.Required);
        }

        [Test]
        public void Properties_NoArgs_AllPropertiesMetadata()
        {
            var target = new DataAnnotationsMetadataProvider();
            var actual = target.Properties<DataStub>();
            Assert.IsNotNull(actual);
            Assert.AreEqual(7, actual.Count);
        }
    }
}
