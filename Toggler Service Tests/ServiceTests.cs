using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toggler_Service;

namespace Toggler_Service_Tests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void ValidateService_True()
        {
            string identifier = "Test";
            string version = "0.0.1";

            var serviceDTO = new Toggler_Service.DTOs.ServiceDTO { Identifier = identifier, Version = version };

            var isValid = Toggler_Service.Validators.ServiceValidator.ValidateService(serviceDTO);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateService_False_IdentifierNull()
        {
            string identifier = null;
            string version = "0.0.1";

            var serviceDTO = new Toggler_Service.DTOs.ServiceDTO { Identifier = identifier, Version = version };

            var isValid = Toggler_Service.Validators.ServiceValidator.ValidateService(serviceDTO);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateService_False_IdentifierEmpty()
        {
            string identifier = null;
            string version = "0.0.1";

            var serviceDTO = new Toggler_Service.DTOs.ServiceDTO { Identifier = identifier, Version = version };

            var isValid = Toggler_Service.Validators.ServiceValidator.ValidateService(serviceDTO);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateService_False_VersionNull()
        {
            string identifier = "Test";
            string version = null;

            var serviceDTO = new Toggler_Service.DTOs.ServiceDTO { Identifier = identifier, Version = version };

            var isValid = Toggler_Service.Validators.ServiceValidator.ValidateService(serviceDTO);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateService_False_VersionEmpty()
        {
            string identifier = "Test";
            string version = "";

            var serviceDTO = new Toggler_Service.DTOs.ServiceDTO { Identifier = identifier, Version = version };

            var isValid = Toggler_Service.Validators.ServiceValidator.ValidateService(serviceDTO);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateService_False_VersionInvalid()
        {
            string identifier = "Test";
            string version = "test";

            var serviceDTO = new Toggler_Service.DTOs.ServiceDTO { Identifier = identifier, Version = version };

            var isValid = Toggler_Service.Validators.ServiceValidator.ValidateService(serviceDTO);

            Assert.IsFalse(isValid);
        }
    }
}