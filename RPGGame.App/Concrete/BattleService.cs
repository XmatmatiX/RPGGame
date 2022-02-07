using RPGGame.App.Managers;
using RPGGame.Domains.Entity;
using RPGGame.Domains.Helpers;
using System;
using System.Linq;
using System.Threading;

namespace RPGGame.App.Concrete
{
    public class BattleService
    {
        private readonly Enemy[] enemies = new Enemy[6];
        private readonly ConsumerItemService consumerService = new ConsumerItemService();

        public BattleService()
        {
            BuildTable();
        }

        private void BuildTable()
        {
            ConsumerItem[] consumerItem = new ConsumerItem[2];
            Random random = new Random();
            consumerItem[0] = consumerService.GetTemplateItem(1, random.Next(0, 4));
            consumerItem[1] = consumerService.GetTemplateItem(6, random.Next(0, 5));
            enemies[0] = new Enemy
            {
                Name = "Wilk",
                HP = 25,
                ArmorPoints = 5,
                AtackPoints = 10,
                Loot = consumerItem,
                CanRunAway = false
            };
            consumerItem[0] = consumerService.GetTemplateItem(1, random.Next(0, 3));
            consumerItem[1] = consumerService.GetTemplateItem(6, random.Next(0, 5));
            enemies[1] = new Enemy
            {
                Name = "Dziki pies",
                HP = 15,
                ArmorPoints = 5,
                AtackPoints = 5,
                Loot = consumerItem,
                CanRunAway = true
            };
            consumerItem[0] = consumerService.GetTemplateItem(1, random.Next(0, 6));
            consumerItem[1] = consumerService.GetTemplateItem(6, random.Next(0, 6));
            enemies[2] = new Enemy
            {
                Name = "Niedźwiedź",
                HP = 40,
                ArmorPoints = 15,
                AtackPoints = 15,
                Loot = consumerItem,
                CanRunAway = true
            };
            consumerItem[0] = consumerService.GetTemplateItem(1, random.Next(0, 4));
            consumerItem[1] = consumerService.GetTemplateItem(6, random.Next(0, 5));
            enemies[3] = new Enemy
            {
                Name = "Owca",
                HP = 15,
                ArmorPoints = 0,
                AtackPoints = 5,
                Loot = consumerItem,
                CanRunAway = true
            };
            consumerItem[0] = consumerService.GetTemplateItem(1, random.Next(0, 2));
            consumerItem[1] = consumerService.GetTemplateItem(6, random.Next(0, 5));
            enemies[4] = new Enemy
            {
                Name = "pancernik",
                HP = 15,
                ArmorPoints = 30,
                AtackPoints = 5,
                Loot = consumerItem,
                CanRunAway = true
            };
            consumerItem[0] = consumerService.GetTemplateItem(1, random.Next(0, 8));
            consumerItem[1] = consumerService.GetTemplateItem(6, random.Next(0, 7));
            enemies[5] = new Enemy
            {
                Name = "Król Lasu",
                HP = 50,
                ArmorPoints = 25,
                AtackPoints = 30,
                Loot = consumerItem,
                CanRunAway = false
            };
        }

        public Enemy GetEnemy()
        {
            Random random = new Random();
            Enemy enemy = enemies[random.Next(0, 5)];

            return enemy;
        }
    }
}
