using RPGGame.Domains.Entity;
using RPGGame.Domains.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RPGGame.App.Concrete
{
    public class PlayerService
    {
        private readonly Player player;
        private readonly BuildingService buildingService = new BuildingService();

        public PlayerService()
        {
            player = new Player()
            {
                HP = 100,
                ArmorPoints = 10,
                AtackPoints = 15,
                StaminaPoints = 100,
                Multipliers = new Multiplier()
                {
                    GatheringMultiplier = 1.0,
                    HuntingMultipier = 1.0,
                    MiningMultiplier = 1.0
                },
                ConsumerBackpack = new List<ConsumerItem>()
            };
        }

        public void SetName(string name)
        {
            player.Name = name;
        }

        public int[] GetBasicData()
        {
            return new int[] { player.HP, player.StaminaPoints };
        }

        public int GetStamina()
        {
            return player.StaminaPoints;
        }
        public int GetHP()
        {
            return player.HP;
        }

        public void UseStamina(int StaminaUse)
        {
            player.StaminaPoints -= StaminaUse;
            if (player.StaminaPoints<0)
            {
                player.StaminaPoints = 0;
            }
        }

        public Multiplier GetMultipliers()
        {
            Multiplier playerMultiplier = player.Multipliers;
            Multiplier buildingMultiplier = buildingService.GetBuildingsMultipliers();

            playerMultiplier.GatheringMultiplier += buildingMultiplier.GatheringMultiplier;
            playerMultiplier.HuntingMultipier += buildingMultiplier.HuntingMultipier;
            playerMultiplier.MiningMultiplier += buildingMultiplier.MiningMultiplier;


            return playerMultiplier;
        }

        public void AddMaterialsToBackpack(List<ConsumerItem> MaterialsGet)
        {
            int quantity;
            bool isOwned;
            var backpack = player.ConsumerBackpack;
            foreach (var getMaterial in MaterialsGet)
            {
                if (getMaterial.Quantity < 1)
                {
                    continue;
                }
                isOwned = false;
                foreach (var backpackItem in player.ConsumerBackpack)
                {
                    if (backpackItem.ItemID == getMaterial.ItemID)
                    {
                        quantity = getMaterial.Quantity;
                        backpackItem.Quantity += quantity;
                        isOwned = true;
                        break;
                    }
                }
                if (!isOwned)
                {
                    backpack.Add(getMaterial);
                }
            }
            player.ConsumerBackpack = player.ConsumerBackpack.OrderBy(p => p.ItemID).ToList();
        }

        public List<ConsumerItem> GetBackpack()
        {
            return player.ConsumerBackpack.Where(i=>i.Quantity > 0).ToList();
        }

        public int Dead()
        {
            player.HP = 1;

            return player.HP;
        }

        public void UseItem(int id)
        {
            foreach (var item in player.ConsumerBackpack)
            {
                if (item.ItemID == id && item.Quantity > 0 && (item.SPRestore > 0 || item.HPRestore > 0))
                {
                    AddHP(item.HPRestore);
                    AddStamina(item.SPRestore);
                    item.Quantity--;
                }
            }
        }

        public int GetSleepTime(int option)
        {
            return option switch
            {
                1 => 15,
                2 => 30,
                3 => 45,
                4 => 100,
                _ => 0,
            };
        }

        public void AddStamina(int stamina)
        {
            player.StaminaPoints += stamina;
            if (player.StaminaPoints>100)
            {
                player.StaminaPoints = 100;
            }
        }

        public void AddHP(int hp)
        {
            player.HP += hp;
            if (player.HP > 100)
            {
                player.HP = 100;
            }
        }

        public void ShowBuilding()
        {
            buildingService.ShowBuildings();
        }

        public Requirement Build(int id)
        {
            Requirement sources = getSources();
            Requirement usedSources = buildingService.Build(sources, id);

            return usedSources;
        }

        public bool MeetAnimal()
        {
            Random random = new Random();

            if (random.Next(0, 100) > 75)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public BattleData GetBattleData()
        {
            BattleData battleData = new BattleData()
            {
                Name = player.Name,
                ArmorPoints = player.ArmorPoints,
                AtackPoints = player.AtackPoints,
                HP = player.HP
            };
            return battleData;
        }

        public void SaveBattleData(BattleData battleData)
        {
            player.HP = battleData.HP;
            AddMaterialsToBackpack(battleData.ConsumerLoot);
        }

        public Requirement getSources()
        {
            Requirement req = new Requirement(0, 0, 0, 0);
            foreach (var item in player.ConsumerBackpack)
            {
                if (item.ItemID == 2)
                {
                    req.RequirementWood = item.Quantity;
                }
                else if (item.ItemID == 3)
                {
                    req.RequirementStone = item.Quantity;
                }
                else if (item.ItemID == 4)
                {
                    req.RequirementIron = item.Quantity;
                }
                else if (item.ItemID == 7)
                {
                    req.RequirementWater = item.Quantity;
                }
            }
            return req;
        }

        public void UseSources(Requirement requirement)
        {
            foreach (var item in player.ConsumerBackpack)
            {
                if (item.Name == "Drewno")
                {
                    item.Quantity -= requirement.RequirementWood;
                }
                if (item.Name == "Kamień")
                {
                    item.Quantity -= requirement.RequirementStone;
                }
                if (item.Name == "żelazo")
                {
                    item.Quantity -= requirement.RequirementIron;
                }
                if (item.Name == "Butelka wody")
                {
                    item.Quantity -= requirement.RequirementWater;
                }
            }
        }
    }
}
