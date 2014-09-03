using System;
using System.Collections.Generic;
using Escrutinador.Extensions.KissSpecifications;
using NUnit.Framework;

namespace Escrutinador.UnitTests.Extensions.KissSpecifications
{
    [TestFixture]
    public class MustComplyWithMetadataSpecificationTest
    {
        [Test]
        public void IsSatisfiedBy_MetatadaNotRespect_False()
        {
            var target = new MustComplyWithMetadataSpecification<EntityStub>();

            // Check if Order property is respected.
            Assert.IsFalse(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = null,
                Password = "1"
            }));
            Assert.AreEqual("The minimum length to Password is 6.", target.NotSatisfiedReason);

            Assert.IsFalse(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = null,
                Password = "123456"
            }));
            Assert.AreEqual("The UserName is required.", target.NotSatisfiedReason);

            Assert.IsFalse(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = "12",
                Password = "123456"
            }));
            Assert.AreEqual("The minimum length to UserName is 3.", target.NotSatisfiedReason);

            Assert.IsFalse(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = "".PadRight(101, 'a'),
                Password = "123456"
            }));
            Assert.AreEqual("The maximum length to UserName is 100.", target.NotSatisfiedReason);

            Assert.IsFalse(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = "1234",
            }));
            Assert.AreEqual("The Password is required.", target.NotSatisfiedReason);

            Assert.IsFalse(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = "123",
                Password = "123456",
                OtherId = 0
            }));

            Assert.AreEqual("The OtherId is required.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_MetatadaNotRespectWithComplexProperty_False()
        {
            var target = new MustComplyWithMetadataSpecification<OtherEntityStub>();

            Assert.IsFalse(target.IsSatisfiedBy(new OtherEntityStub()
            {
                Parent = null
            }));

            Assert.AreEqual("The Parent is required.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_MetatadaNotRespectWithListProperty_False()
        {
            var target = new MustComplyWithMetadataSpecification<OtherEntityStub>();

            Assert.IsFalse(target.IsSatisfiedBy(new OtherEntityStub()
            {
                Parent = new EntityStub(),
                Lines = null
            }));

            Assert.AreEqual("The Lines is required.", target.NotSatisfiedReason);

            Assert.IsFalse(target.IsSatisfiedBy(new OtherEntityStub()
            {
                Parent = new EntityStub(),
                Lines = new List<string>() { "1" }
            }));

            Assert.AreEqual("The minimum length to Lines is 2.", target.NotSatisfiedReason);
        }


        [Test]
        public void IsSatisfiedBy_MetatadaInvalidUrl_False()
        {
            var target = new MustComplyWithMetadataSpecification<OtherEntityStub>();

            Assert.IsFalse(target.IsSatisfiedBy(new OtherEntityStub()
            {
                Parent = new EntityStub(),
                Lines = new List<string>() { "1", "2" },
                Url = "teste"
            }));

            Assert.AreEqual("The Url is an invalid URL.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_MetatadaValidUrl_True()
        {
            var target = new MustComplyWithMetadataSpecification<OtherEntityStub>();

            Assert.IsTrue(target.IsSatisfiedBy(new OtherEntityStub()
            {
                Parent = new EntityStub(),
                Lines = new List<string>() { "1", "2" },
                Url = "http://www.diegogiacomelli.com.br"
            }));
        }

        [Test]
        public void IsSatisfiedBy_MetatadaRespectWithListProperty_True()
        {
            var target = new MustComplyWithMetadataSpecification<OtherEntityStub>();

            Assert.IsTrue(target.IsSatisfiedBy(new OtherEntityStub()
            {
                Parent = new EntityStub(),
                Lines = new List<string>() { "1", "2" }
            }));

            Assert.IsNullOrEmpty(target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_NoMetadata_True()
        {
            var target = new MustComplyWithMetadataSpecification<DateTime>();
            Assert.IsTrue(target.IsSatisfiedBy(DateTime.Now));
        }

        [Test]
        public void IsSatisfiedBy_MetadataRespect_True()
        {
            var target = new MustComplyWithMetadataSpecification<EntityStub>();
            Assert.IsTrue(target.IsSatisfiedBy(new EntityStub()
            {
                UserName = "1234",
                Password = "123456",
                OtherId = 1,
                ThatOrderId = 2,
                DateKind = DateTimeKind.Unspecified
            }));

            Assert.IsNullOrEmpty(target.NotSatisfiedReason);
        }
    }
}
