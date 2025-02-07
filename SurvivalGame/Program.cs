namespace SurvivalGame;
internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        game.level = 10;
        game.StartGame();
    }

}