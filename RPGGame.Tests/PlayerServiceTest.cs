using FluentAssertions;
using Moq;
using RPGGame.App.Concrete;
using RPGGame.Domains.Entity;
using RPGGame.Domains.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RPGGame.Tests
{
    public class PlayerServiceTest
    {
        [Fact]
        public void SaveBattleData()
        {
            //Arrange
            BattleData battleData = new BattleData() {
                HP = 50,
                ArmorPoints = 10,
                AtackPoints = 10 ,
                ConsumerLoot = new List<ConsumerItem>(),
                Name = "Asd"
            };
            var playerMoq = new Mock<PlayerService>();
            //Act
            playerMoq.Object.SaveBattleData(battleData);
            var getHP = playerMoq.Object.GetHP();
            //Assert
            Assert.Equal(battleData.HP, getHP);
        }

        [Fact]
        public void UseStamina()
        {
            //Arrange
            var playerMoq = new Mock<PlayerService>();
            //Act
            playerMoq.Object.UseStamina(5);
            //Assert
            Assert.Equal(95, playerMoq.Object.GetStamina());

            playerMoq.Object.UseStamina(100);

            Assert.Equal(0, playerMoq.Object.GetStamina());
        }

        [Fact]
        public void Sleep()
        {
            var playerMoq = new Mock<PlayerService>();

            playerMoq.Object.UseStamina(100);

            playerMoq.Object.Sleep(1);
            Assert.Equal(5, playerMoq.Object.GetStamina());

            playerMoq.Object.UseStamina(100);

            playerMoq.Object.Sleep(2);

            int result = playerMoq.Object.GetStamina();
            Assert.Equal(10, result);

            playerMoq.Object.UseStamina(100);

            playerMoq.Object.Sleep(3);
            Assert.Equal(20, playerMoq.Object.GetStamina());

            playerMoq.Object.UseStamina(100);

            playerMoq.Object.Sleep(4);
            Assert.Equal(100, playerMoq.Object.GetStamina());
        }

        [Fact]
        public void AddMaterialToBackpack()
        {
            var playerMoq = new Mock<PlayerService>();

            List<ConsumerItem> Loot1 = new List<ConsumerItem>();
            List<ConsumerItem> Loot2 = new List<ConsumerItem>();

            Loot1.Add(new ConsumerItem() { ItemID = 1, Quantity = 5});
            Loot1.Add(new ConsumerItem() { ItemID = 1, Quantity = 3});
            Loot2.Add(new ConsumerItem() { ItemID = 2, Quantity = 5 });
            Loot2.Add(new ConsumerItem() { ItemID = 2, Quantity = 1 });

            playerMoq.Object.AddMaterialsToBackpack(Loot1);
            playerMoq.Object.AddMaterialsToBackpack(Loot2);

            List<ConsumerItem> backpack = new List<ConsumerItem>();
            backpack.Add(new ConsumerItem() { ItemID = 1, Quantity = 8 });
            backpack.Add(new ConsumerItem() { ItemID = 2, Quantity = 6 });

            var expected = backpack.ToArray();

            var result = playerMoq.Object.GetBackpack().ToArray();

            Assert.Equal(expected[0].Quantity, result[0].Quantity);
            Assert.Equal(expected[0].ItemID, result[0].ItemID);

            Assert.Equal(expected[1].Quantity, result[1].Quantity);
            Assert.Equal(expected[1].ItemID, result[1].ItemID);
        }

        [Fact]
        public void UseItem()
        {
            var playerMoq = new Mock<PlayerService>();

            List<ConsumerItem> backpack = new List<ConsumerItem>();

            backpack.Add(new ConsumerItem() { ItemID = 1, Quantity = 5, SPRestore = 5 });

            playerMoq.Object.UseStamina(10);
            playerMoq.Object.AddMaterialsToBackpack(backpack);

            playerMoq.Object.UseItem(1);

            var resultStamina = playerMoq.Object.GetStamina();

            var newBackpack = playerMoq.Object.GetBackpack().ToArray();
            var resultQuantity = newBackpack[0].Quantity; 

            Assert.Equal(95, resultStamina);
            Assert.Equal(4, resultQuantity);

        }

    }
}
