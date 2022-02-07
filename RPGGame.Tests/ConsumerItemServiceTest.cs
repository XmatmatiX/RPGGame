using FluentAssertions;
using Moq;
using RPGGame.App.Concrete;
using RPGGame.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RPGGame.Tests
{
    public class ConsumerItemServiceTest
    {

        [Fact]
        public void GetTemplateItem()
        {
            var itemServiceMoq = new Mock<ConsumerItemService>();

            ConsumerItem item = new ConsumerItem()
            {
                ItemID = 1,
                Name = "Surowe mięso",
                HPRestore = 10,
                SPRestore = 5,
                Type = ConsumerType.of_consumer,
                Quantity = 5
            };

            var newItem = itemServiceMoq.Object.GetTemplateItem(1, 5);

            newItem.Should().NotBeNull();
            Assert.Equal(item.ItemID, newItem.ItemID);
            Assert.Equal(item.Quantity, newItem.Quantity);

        }

    }
}
