using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiskCheck.Domain;
using System.Collections.Generic;
using Shouldly;
using Microsoft.Extensions.Caching.Memory;
using FakeItEasy;
using RiskCheck.Application;
using System.Threading.Tasks;

namespace RiskCheck.Tests
{
    [TestClass]
    public class CoordinateDistanceCheckerTests
    {
        [TestMethod]
        public void Check_HD75HY_With_HD74EZ_ShouldBe_1770m()
        {
            var start = new Coordinate(53.617699485619916, -1.8890033797334795);
            var end = new Coordinate(53.62771821593212, -1.8681711951473778);
            var distance = CoordinateDistanceChecker.Check(start, end);
            distance.ShouldBe(1770);
        }

        [TestMethod]
        public void Check_HD75HY_With_HD75HY_ShouldBe_Equal()
        {
            var start = new Coordinate(53.617699485619916, -1.8890033797334795);
            var end = new Coordinate(53.617699485619916, -1.8890033797334795);
            var distance = CoordinateDistanceChecker.Check(start, end);
            start.Equals(end).ShouldBeTrue();
        }

        [TestMethod]
        public void Check_HD75HY_With_HD75HY_ShouldBe_0()
        {
            var start = new Coordinate(53.617699485619916, -1.8890033797334795);
            var end = new Coordinate(53.617699485619916, -1.8890033797334795);
            var distance = CoordinateDistanceChecker.Check(start, end);
            distance.ShouldBe(0);
        }
    }

    [TestClass]
    public class OccupationServiceTests
    {
        [TestMethod]
        public void CheckXML_Occupation_For_Chef_ShouldBe_Defer()
        {
            var service = new OccupationService();
            var action = service.GetActionForOccupation("Chef");
            action.Wait();
            action.Result.ShouldBe(ActionEnum.Refer);
        }

        [TestMethod]
        public void CheckXML_Occupation_For_Footballer_ShouldBe_Accept()
        {
            var service = new OccupationService();
            var action = service.GetActionForOccupation("Footballer");
            action.Wait();
            action.Result.ShouldBe(ActionEnum.Accept);
        }

        [TestMethod]
        public void CheckXML_Occupation_For_IT_Contractor_ShouldBe_Accept()
        {
            var service = new OccupationService();
            var action = service.GetActionForOccupation("IT Contractor");
            action.Wait();
            action.Result.ShouldBe(ActionEnum.Decline);
        }

    }

    [TestClass]
    public class RiskCheckerServiceTests
    {
        [TestMethod]
        public void TestRiskChecker()
        {
            var risk = new Risk()
            {
                Name = "Tom",
                Address = new Address() { Address1 = "The Road", Postcode = "HD7 5HY" },
                KeptPostcode = "HD7 4EZ",
                Occupation = "Chef"
            };
            var check = A.Fake<IRiskCheckerService>();
            A.CallTo(() => check.GetOverallRisk(risk)).Returns(ActionEnum.Refer);
            check.GetOverallRisk(risk).ShouldBe(ActionEnum.Refer);
        }
    }

    [TestClass]
    public class VehicleKeptCheckCalculationTests
    {
        [TestMethod]
        public void CalculateRisk_5m_ShouldBe_Accepted()
        {
            var risk = new Risk()
            {
                Name = "Tom",
                Address = new Address() { Address1 = "The Road", Postcode = "HD7 5HY" },
                KeptPostcode = "HD7 4EZ",
                Occupation = "Chef"
            };

            var postCodeService = A.Fake<IPostCodeDistanceService>();
            var calc = A.Fake<VehicleKeptCheckCalculation>();

            A.CallTo(() => postCodeService.GetDistance(risk.Address.Postcode, risk.KeptPostcode)).Returns(Task.FromResult(5));

            var res = calc.CalculateRisk(risk);

            res.Result.ShouldBe(ActionEnum.Accept);
        }
    }
}
