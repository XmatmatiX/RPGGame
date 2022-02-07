using RPGGame.Domains.Entity;

namespace RPGGame.App.Concrete
{
    public class ConsumerItemService
    {
        private readonly ConsumerItem[] Items = new ConsumerItem[8];

        public ConsumerItemService()
        {
            BuildTable();
        }
        private void BuildTable()
        {
            Items[0] = new ConsumerItem()
            {
                ItemID = 1,
                Name = "Surowe mięso",
                HPRestore = 10,
                SPRestore = 5,
                Type = ConsumerType.of_consumer
            };

            Items[1] = new ConsumerItem()
            {
                ItemID = 2,
                Name = "Drewno",
                SPRestore = 0,
                Type = ConsumerType.neutral
            };

            Items[2] = new ConsumerItem()
            {
                ItemID = 3,
                Name = "Kamień",
                SPRestore = 0,
                Type = ConsumerType.neutral
            };

            Items[3] = new ConsumerItem()
            {
                ItemID = 4,
                Name = "żelazo",
                SPRestore = 0,
                Type = ConsumerType.neutral
            };

            Items[4] = new ConsumerItem()
            {
                ItemID = 5,
                Name = "Owoce",
                SPRestore = 5,
                Type = ConsumerType.of_consumer
            };

            Items[5] = new ConsumerItem()
            {
                ItemID = 6,
                Name = "Skóra",
                SPRestore = 0,
                Type = ConsumerType.neutral
            };

            Items[6] = new ConsumerItem()
            {
                ItemID = 7,
                Name = "Butelka wody",
                SPRestore = 15,
                Type = ConsumerType.of_consumer
            };

            Items[7] = new ConsumerItem()
            {
                ItemID = 8,
                Name = "Ugotowane mięso",
                SPRestore = 15,
                Type = ConsumerType.of_consumer
            };
        }
        public ConsumerItem GetTemplateItem(int ID, int quantity)
        {
            ConsumerItem consumerItem = Items[ID - 1];
            consumerItem.Quantity = quantity;
            return consumerItem;
        }
    }
}
