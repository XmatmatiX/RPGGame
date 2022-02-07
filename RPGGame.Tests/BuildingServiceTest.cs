using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using RPGGame.App.Concrete;
using RPGGame.Domains.Entity;
using RPGGame.Domains.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RPGGame.Tests
{
    public class BuildingServiceTest
    {

        [Fact]
        public void Build()
        {

            var buildMoq = new Mock<BuildingService>();
            Requirement req = new Requirement(100, 100, 100, 100);

            var resultZero = buildMoq.Object.Build(req, 1);

            req = new Requirement(0, 0, 0, 0);

            var resultOne = buildMoq.Object.Build(req, 1);

            resultZero.Should().NotBeNull();
            resultOne.Should().NotBeNull();

            resultZero.RequirementWood.Should().Be(35);
            resultZero.RequirementStone.Should().Be(20);
            resultZero.RequirementIron.Should().Be(5);
            resultZero.RequirementWater.Should().Be(10);

            resultOne.IsZero().Should().BeTrue();

        }

    }
}
