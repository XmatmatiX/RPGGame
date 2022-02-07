using RPGGame.App.Concrete;
using RPGGame.App.Managers;
using System;

namespace RPGGame.Game
{
    public class GameScreen
    {
        readonly GameManager gameService;

        public GameScreen()
        {
            gameService = new GameManager();
        }

        private bool isParsed = false;
        private bool endGame = false;


        public void Start()
        {
            //pokazuje napisy początkowe i wprowadzenie do gry
            do
            {
                Console.WriteLine("Witaj w świecie wspaniałych przygód i wielu niebezpieczeństw.");
                Console.WriteLine("Mam nadzieję, że nie zginiesz tak łatwo!");

                Console.WriteLine();

                Console.Write("Podaj swoje imie poszukiwaczu: ");
                gameService.CreatePlayer();
                MainScreen();

                Console.Clear();

            } while (!isParsed);
            Console.Clear();
            Console.WriteLine("Dzięki za zagranie w moją gre! Mam nadzieję, że Ci się podobała");
            Console.ReadKey();

        }
        public void MainScreen()
        {
            //podstawowe okno dialogowe z wyborem akcji do wykonania oraz podstawowymi danymi danymi
            while (!endGame)
            {
                gameService.ShowPlayerData();

                Console.WriteLine($"A więc co planujesz teraz zrobić?");
                Console.WriteLine("1) Wyrusz na przygode");
                Console.WriteLine("2) Rozwiń swoją kryjówke");
                Console.WriteLine("3) Odpocznij");
                Console.WriteLine("4) Zobacz do plecaka");
                Console.WriteLine("5) wyjdź z gry");

                switch (GameManager.GetIntKeyDown(1, 5, out isParsed))
                {
                    case 1:
                        gameService.Travel();
                        break;
                    case 2:
                        gameService.Build();
                        break;
                    case 3:
                        gameService.Rest();
                        break;
                    case 4:
                        gameService.OpenBackpack();
                        break;
                    case 5:
                        endGame = gameService.EndGame();
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
