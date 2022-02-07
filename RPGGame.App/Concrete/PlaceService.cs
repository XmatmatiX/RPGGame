using RPGGame.Domains.Entity;
using RPGGame.Domains.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RPGGame.App.Concrete
{
    public class PlaceService
    {
        private readonly Place[] places = new Place[5];
        readonly ConsumerItemService consumerItemService;

        public PlaceService()
        {
            consumerItemService = new ConsumerItemService();
            BuildTable();
        }

        private void BuildTable()
        {
            places[0] = new Place()
            {
                Name = "Wielkie stawy",
                StaminaUse = 5,
                IsMine = false,
                IsPlant = true,
                IsWater = true,
                IsWood = false,

            };
            places[1] = new Place()
            {
                Name = "Górska przełęcz",
                StaminaUse = 10,
                IsMine = true,
                IsPlant = false,
                IsWater = false,
                IsWood = true,

            };
            places[2] = new Place()
            {
                Name = "Bagna",
                StaminaUse = 20,
                IsMine = false,
                IsPlant = true,
                IsWater = true,
                IsWood = false,

            };
            places[3] = new Place()
            {
                Name = "Serce Lasu",
                StaminaUse = 15,
                IsMine = false,
                IsPlant = true,
                IsWater = false,
                IsWood = true,

            };
            places[4] = new Place()
            {
                Name = "Mroczna jaskinia",
                StaminaUse = 15,
                IsMine = true,
                IsPlant = false,
                IsWater = false,
                IsWood = false,
            };
        }

        public Place[] GetPlaceData()
        {
            return places;
        }
        public bool StaminaCheck(int PlaceID, int PlayerStamina, out int StaminaUse)
        {
            if (PlayerStamina < places[PlaceID - 1].StaminaUse)
            {
                StaminaUse = 0;

                return false;
            }
            else
            {
                StaminaUse = places[PlaceID - 1].StaminaUse;

                return true;
            }
        }

        public int GetTravellingTime(int PlaceID)
        {
            PlaceID--;
            return places[PlaceID].StaminaUse * 5;
        }

        public List<ConsumerItem> GetMaterials(int PlaceID, Multiplier Multipliers)
        {
            PlaceID--;
            List<ConsumerItem> MaterialsGet = new List<ConsumerItem>();
            Random random = new Random();

            if (places[PlaceID].IsMine)
            {
                MaterialsGet.Add(consumerItemService.GetTemplateItem(3,
                    1 + (int)(random.NextDouble() * 8 * Multipliers.MiningMultiplier)));
                MaterialsGet.Add(consumerItemService.GetTemplateItem(4,
                    1 + (int)(random.NextDouble() * 3 * Multipliers.MiningMultiplier)));
            }
            if (places[PlaceID].IsPlant)
            {
                MaterialsGet.Add(consumerItemService.GetTemplateItem(5,
                    1 + (int)(random.NextDouble() * 5 * Multipliers.GatheringMultiplier)));
            }
            if (places[PlaceID].IsWater)
            {
                MaterialsGet.Add(consumerItemService.GetTemplateItem(6,
                    1 + (int)(random.NextDouble() * 4 * Multipliers.GatheringMultiplier)));
            }
            if (places[PlaceID].IsWood)
            {
                MaterialsGet.Add(consumerItemService.GetTemplateItem(2,
                    1 + (int)(random.NextDouble() * 7 * Multipliers.GatheringMultiplier)));
            }


            return MaterialsGet;
        }
    }
}
