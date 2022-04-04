using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Toggler_Service;

namespace Toggler_Service_Tests
{
    [TestClass]
    public class ToggleTests
    {
        [TestMethod]
        public void ValidateToggle_True()
        {
            string name = "Test";

            var toggleDTO = new Toggler_Service.DTOs.ToggleDTO { Name = name};

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggle(toggleDTO);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateToggle_False_NameNull()
        {
            string name = null;

            var toggleDTO = new Toggler_Service.DTOs.ToggleDTO { Name = name };

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggle(toggleDTO);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateToggle_False_NameEmpty()
        {
            string name = "";

            var toggleDTO = new Toggler_Service.DTOs.ToggleDTO { Name = name };

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggle(toggleDTO);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateToggleService_True()
        {
            var inclusion = new Toggler_Service.DTOs.ServiceVersionDTO { Identifier = "Test", Version = "0.0.1" };

            var inclusions = new List<Toggler_Service.DTOs.ServiceVersionDTO>();

            inclusions.Add(inclusion);

            var dto = new Toggler_Service.DTOs.ToggleServiceDTO { IncludeAllServices = false, Inclusions = inclusions, Exclusions = new List<Toggler_Service.DTOs.ServiceVersionDTO>() };

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggleService(dto);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateToggleService_False_ObjectNull()
        {
            Toggler_Service.DTOs.ToggleServiceDTO dto = null;

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggleService(dto);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateToggleService_False_InclusionsNull()
        {
            var dto = new Toggler_Service.DTOs.ToggleServiceDTO { Inclusions = null, Exclusions = null};

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggleService(dto);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateToggleService_False_InclusionsAndExclusions()
        {
            var inclusion = new Toggler_Service.DTOs.ServiceVersionDTO { Identifier = "Test", Version = "0.0.1" };
            var exclusion = new Toggler_Service.DTOs.ServiceVersionDTO { Identifier = "Test2", Version = "0.0.1" };

            var inclusions = new List<Toggler_Service.DTOs.ServiceVersionDTO>();
            var exclusions = new List<Toggler_Service.DTOs.ServiceVersionDTO>();
             
            inclusions.Add(inclusion);
            exclusions.Add(exclusion);

            var dto = new Toggler_Service.DTOs.ToggleServiceDTO { Inclusions = inclusions, Exclusions = exclusions };

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggleService(dto);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateToggleService_False_InclusionsOrExclusionsAndIncludeAll()
        {
            var inclusion = new Toggler_Service.DTOs.ServiceVersionDTO { Identifier = "Test", Version = "0.0.1" };

            var inclusions = new List<Toggler_Service.DTOs.ServiceVersionDTO>();

            inclusions.Add(inclusion);

            var dto = new Toggler_Service.DTOs.ToggleServiceDTO { IncludeAllServices = true, Inclusions = inclusions, Exclusions = new List<Toggler_Service.DTOs.ServiceVersionDTO>() };

            var isValid = Toggler_Service.Validators.ToggleValidator.ValidateToggleService(dto);

            Assert.IsFalse(isValid);
        }
    }
}