using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldConsole
{
    internal class Game
    {
        readonly Board board = new();

        /// <summary>
        /// Setup the board and play the game while the user wants to play
        /// </summary>
        public void SetupAndPlay()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("WELCOME TO MINEFIELD!");
            Console.WriteLine("---------------------");
            var userWantsToPlay = true;
            while (userWantsToPlay)
            {
                board.SetupBoard();
                var difficulty = GetDifficultyFromUser();

                Console.WriteLine("Planting mines...");
                board.PlantMines(difficulty * 5);

                Console.WriteLine("Ready!");
                PlayGame();

                userWantsToPlay = DoesUserWantToPlayAgain();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prompt user for their move and make the move
        /// </summary>
        private void PlayGame()
        {
            var numberOfLives = 5;
            var currentScore = 0;
            var currentPosition = 0;

            while (numberOfLives > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Current position: [{board.GetSquareCoordinates(currentPosition)}]");
                Console.WriteLine($"Number of lives: {numberOfLives}");
                Console.WriteLine("Make your move... (Up, Down, Left, Right keys)" );

                var key = Console.ReadKey();
                var validMove = board.IsMoveValid(currentPosition, key.Key);

                if (validMove)
                {
                    currentScore++;
                    currentPosition = board.GetNewPosition(currentPosition, key.Key);

                    if (board.IsSquareMined(currentPosition))
                    {
                        numberOfLives--;
                        WriteColouredText($"BANG!! You hit a mine. New position: [{board.GetSquareCoordinates(currentPosition)}]", ConsoleColor.Red, false);
                        board.ClearMine(currentPosition);

                        if (board.IsGameOver(currentPosition, numberOfLives))
                        {
                            WriteColouredText("Bad luck!! YOU LOOSE!!", ConsoleColor.Red);
                            break;
                        }
                    }
                    else
                    {
                        WriteColouredText($"Phew, you made it!! New position: [{board.GetSquareCoordinates(currentPosition)}]", ConsoleColor.Green, false);

                        if (board.IsGameOver(currentPosition, numberOfLives))
                        {
                            WriteColouredText($"Congratulations!! YOU WON!!  Your score is: {currentScore}", ConsoleColor.Green, true);
                            break;
                        }
                    }
                }
                else
                {
                    WriteColouredText("Oops, you cant make that move.", ConsoleColor.Blue, false);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prompt user to select difficulty.  This will determine the number of mines in the game later
        /// </summary>
        /// <returns></returns>
        private static int GetDifficultyFromUser()
        {
            var result = 1;
            var difficultySelected = false;
            while (!difficultySelected)
            {
                Console.Write("Choose your difficuly level - (1) Easy, (2) Medium, (3) Hard: ");
                var difficultyInput = Console.ReadLine();

                if (Int32.TryParse(difficultyInput, out result))
                {
                    switch (result)
                    {
                        case 1:
                        case 2:
                        case 3:
                            difficultySelected = true;
                            break;
                        default:
                            continue;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Prompt user to find out if they want to play again
        /// </summary>
        /// <returns></returns>
        private static bool DoesUserWantToPlayAgain()
        {
            var result = false;
            var playAgainValid = false;
            while (!playAgainValid)
            {
                Console.WriteLine();
                Console.Write("Do you want to play again? (Y / N): ");
                var playAgain = Console.ReadLine();

                if (!string.IsNullOrEmpty(playAgain))
                {
                    if (playAgain.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        result = true;
                        playAgainValid = true;
                    }
                    else if (playAgain.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                    {
                        result = false;
                        playAgainValid = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Writes text to the console with a colour
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="includeBlankLine"></param>
        private static void WriteColouredText(string text, ConsoleColor color, bool includeBlankLine = true)
        {
            if (includeBlankLine)
                Console.WriteLine();

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
