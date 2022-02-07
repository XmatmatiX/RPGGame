using RPGGame.App.Concrete;
using RPGGame.Domains.Entity;
using RPGGame.Domains.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RPGGame.App.Managers
{
    public class GameManager
    {
        private readonly PlaceService placeService = new PlaceService();
        private readonly PlayerService playerService = new PlayerService();
        private readonly BattleService battleService = new BattleService();
        private readonly BuildingService buildingService = new BuildingService();

        private bool isParsed;
        private bool endAction;

        public static int GetIntKeyDown(int min, int max, out bool isParsed)
        {
            int choice;
            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out choice))
            {
                if (choice >= min && choice <= max)
                {
                    isParsed = true;
                }
                else
                {
                    isParsed = false;
                    choice = 0;
                }
            }
            else
            {
                isParsed = false;
                choice = 0;
            }
            Console.Clear();
            return choice;
        }

        public void CreatePlayer()
        {
            string name = Console.ReadLine();
            playerService.SetName(name);
        }

        public void ShowPlayerData()
        {
            int[] data = playerService.GetBasicData();
            Console.Clear();
            Console.WriteLine($"HP: {data[0]}   Stamina: {data[1]}");
            Console.WriteLine();
        }

        public void Travel()
        {
            int StaminaUse;
            int Choice;
            bool meetAnimal;

            do
            {
                ShowPlayerData();

                Console.WriteLine("Wybierz cel swojej podróży:");
                Console.WriteLine();

                var places = placeService.GetPlaceData();

                int id = 1;
                foreach (var item in places)
                {
                    Console.WriteLine($"{id}) {item.Name}" +
                        $"   Zużycie wytrzymałości: {item.StaminaUse}");
                    id++;
                    Console.WriteLine();
                }

                Console.WriteLine("6) Wróć do menu");

                Choice = GetIntKeyDown(1, 6, out isParsed);
                if (Choice >= 1 && Choice <= 5)
                {

                    if (placeService.StaminaCheck(Choice, playerService.GetStamina(), out StaminaUse))
                    {
                        playerService.UseStamina(StaminaUse);

                        meetAnimal = playerService.MeetAnimal();

                        if (meetAnimal)
                        {
                            BattleData battleData = playerService.GetBattleData();
                            Enemy enemy = battleService.GetEnemy();

                            Random random = new Random();
                            bool isParsed = false;
                            Choice = 0;
                            int atack;


                            Console.Clear();
                            Console.WriteLine($"Podczas swojej podróży napotykasz się na {enemy.Name}");
                            Console.WriteLine("Chyba nie jest zbyt zadowolony twoją obecnością.  Lepiej na siebie uważaj");
                            Console.ReadKey();
                            while (!isParsed)
                            {
                                Console.Clear();
                                Console.WriteLine("1)Walka");
                                Console.WriteLine("2)Ucieczka");
                                Choice = GameManager.GetIntKeyDown(1, 2, out isParsed);
                            }
                            switch (Choice)
                            {
                                case 1:
                                    while (battleData.HP > 0 && enemy.HP > 0)
                                    {

                                        atack = (int)(random.Next(50, 200) / 100 * battleData.AtackPoints)
                                                - (int)(random.NextDouble() * enemy.ArmorPoints / 2);
                                        Console.WriteLine($"{enemy.Name} otrzymuje {atack} obrażeń");
                                        enemy.HP -= atack;
                                        Thread.Sleep(1500);
                                        if (enemy.HP > 0)
                                        {
                                            atack = (int)(random.Next(50, 200) / 100 * enemy.AtackPoints)
                                                    - (int)(random.NextDouble() * battleData.ArmorPoints / 2);
                                            Console.WriteLine($"{battleData.Name} otrzymuje {atack} obrażeń");
                                            battleData.HP -= atack;
                                            Thread.Sleep(1500);
                                        }
                                    }
                                    break;
                                case 2:
                                    if (enemy.CanRunAway)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Tym razem udało ci się uciec.");
                                        Console.ReadKey();
                                    }
                                    else
                                    {

                                        Console.WriteLine("Biegłeś ile sił w nogach ale niestety");
                                        Console.WriteLine("byłeś zbyt wolny... musisz walczyć!");
                                        Console.ReadKey();
                                        while (battleData.HP > 0 && enemy.HP > 0)
                                        {
                                            Console.Clear();
                                            atack = (int)(random.Next(50, 200) / 100 * enemy.AtackPoints)
                                                    - (int)(random.NextDouble() * battleData.ArmorPoints / 2);
                                            Console.WriteLine($"{battleData.Name} otrzymuje {atack} obrażeń");
                                            battleData.HP -= atack;
                                            Thread.Sleep(1500);
                                            atack = (int)(random.Next(50, 200) / 100 * battleData.AtackPoints)
                                                    - (int)(random.NextDouble() * enemy.ArmorPoints / 2);
                                            Console.WriteLine($"{enemy.Name} otrzymuje {atack} obrażeń");
                                            enemy.HP -= atack;
                                            Thread.Sleep(1500);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            if (battleData.HP > 0)
                            {
                                Console.WriteLine($"Gratulacje udało ci się pokonać {enemy.Name}");
                                battleData.ConsumerLoot = enemy.Loot.ToList();
                            }

                            Console.ReadKey();
                            playerService.SaveBattleData(battleData);

                            if (playerService.GetHP() > 0)
                            {
                                int time = placeService.GetTravellingTime(Choice);
                                
                                Console.Clear();
                                for (int i = time; i > 0; i--)
                                {
                                    Console.WriteLine($"Dotrzesz do celu podróży za: {i}");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                }
                                playerService.AddMaterialsToBackpack(placeService.GetMaterials(Choice,
                                    playerService.GetMultipliers()));
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"przykro mi {playerService.GetBattleData().Name}...");
                                Console.ReadKey();
                                Console.WriteLine("Niestety nie udało ci się wygrać swojej walki i zostałeś zjedzony");
                                Console.WriteLine("przez zwierzęta... Jedyne co moge dla ciebie zrobić to cofnąć czas");
                                Console.WriteLine("do momentu zanim wyruszyłeś na przygodę");

                                Console.ReadKey();
                                playerService.Dead();
                            }
                        }
                        else
                        {
                            int time = placeService.GetTravellingTime(Choice);

                            Console.Clear();
                            for (int i = time; i > 0; i--)
                            {
                                Console.WriteLine($"Dotrzesz do celu podróży za: {i}");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            playerService.AddMaterialsToBackpack(placeService.GetMaterials(Choice,
                                playerService.GetMultipliers()));
                        }



                    }
                    else
                    {
                        ShowPlayerData();
                        Console.WriteLine("Nie możesz się tam udać jesteś zbyt zmęczony!");
                        Console.ReadKey();
                        isParsed = false;
                    }
                }
                else if (Choice == 6)
                {
                    isParsed = true;
                }

            } while (!isParsed);
        }

        public void Build()
        {
            int Choice;
            isParsed = false;
            while (!isParsed)
            {
                ShowPlayerData();
                var buildings = buildingService.GetBuildingsTable();

                foreach (var item in buildings)
                {
                    Console.WriteLine($"{item.BuildingID}) {item.Name}   kamień: {item.Requirement.RequirementStone}" +
                           $"   drewno: {item.Requirement.RequirementWood}  ");
                    Console.WriteLine($"            woda: {item.Requirement.RequirementWater}" +
                        $"   żelazo: {item.Requirement.RequirementIron}   Level: {item.Level}");
                    Console.WriteLine();
                }
                Console.WriteLine("4) powrót");

                Choice = GetIntKeyDown(1, 4, out isParsed);
                if (Choice < 4)
                {
                    var req = playerService.Build(Choice);
                    if (req.IsZero())
                    {
                        Console.Clear();
                        Console.WriteLine("Nie masz wystarczająco dużo surowców");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Udało ci się rozbudować budynek");
                        Console.ReadKey();
                        playerService.UseSources(req);
                    }

                }
                else if (Choice == 4)
                {
                    isParsed = true;
                }
            }

        }

        public void Rest()
        {
            int Choice;
            isParsed = false;
            while (!isParsed)
            {
                ShowPlayerData();

                Console.WriteLine("1) drzemka (+5 SP   15sek)");
                Console.WriteLine("2) krótki sen (+10 SP   30sek)");
                Console.WriteLine("3) sen (+20 SP   45sek)");
                Console.WriteLine("4) Długi sen (+100 SP   100sek)");
                Console.WriteLine("5) powrót");
                Choice = GetIntKeyDown(1, 5, out isParsed);
                if (Choice < 5)
                {
                    int time = playerService.GetSleepTime(Choice);

                    for (int i = time; i > 0; i--)
                    {
                        Console.Clear();
                        Console.WriteLine($"Pozostały czas snu: {i}");
                        Thread.Sleep(1000);
                    }
                    switch (Choice)
                    {
                        case 1:
                            playerService.AddStamina(5);
                            break;
                        case 2:
                            playerService.AddStamina(10);
                            break;
                        case 3:
                            playerService.AddStamina(20);
                            break;
                        case 4:
                            playerService.AddStamina(100);
                            break;
                        default:
                            break;
                    }

                }
                else if (Choice == 5)
                {
                    isParsed = true;
                }
            }
        }

        public void OpenBackpack()
        {

            int Choice;

            while (!endAction)
            {
                ShowPlayerData();
                List<ConsumerItem> backpack = playerService.GetBackpack();

                foreach (var item in backpack)
                {
                    Console.Write($"{item.ItemID}) {item.Name}   ");
                    if (item.SPRestore > 0)
                    {
                        Console.Write($"SP: {item.SPRestore}   ");
                    }
                    if (item.HPRestore> 0)
                    {
                        Console.Write($"HP: {item.HPRestore}   ");
                    }
                    Console.WriteLine($"Ilość: {item.Quantity}");
                }
                Console.WriteLine("9) powrót");
                Choice = GetIntKeyDown(0, 9, out isParsed);

                if (Choice > 0 && Choice < 9)
                {
                    playerService.UseItem(Choice);
                }
                else if (Choice == 9)
                {
                    endAction = true;
                }


            }
            endAction = false;

        }

        public bool EndGame()
        {
            bool endGame = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Czy napewno chcesz zakończyć grę?");
                Console.WriteLine("1)Tak");
                Console.WriteLine("2)Nie");
                if (GetIntKeyDown(1, 2, out isParsed) == 1)
                {
                    endGame = true;
                }
            } while (!isParsed);
            return endGame;
        }
    }
}
