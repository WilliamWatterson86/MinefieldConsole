using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldConsole
{
    public class Board
    {
        public List<Square> Squares { get; set; }

        public Board()
        {
            Squares = [];
        }

        /// <summary>
        /// Creates the board - 8 columns (A - H) and 8 rows (1 - 8)
        /// </summary>
        public void SetupBoard()
        {
            this.Squares.Clear();
            var columns = new[] { "A", "B", "C", "D", "E", "F", "G", "H" };

            foreach (var column in columns)
            {
                for (int x = 1; x <= 8; x++)
                {
                    Squares.Add(new Square(column, x));
                }
            }
        }

        /// <summary>
        /// Checks if the move is valid - eg when in column A you shouldn't be able to go left, when in row 1 you shouldnt be able to go down etc
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsMoveValid(int currentPosition, ConsoleKey key)
        {
            var result = false;
            var currentSquare = Squares[currentPosition];

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (currentSquare.Row < 8)
                    {
                        result = true;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (currentSquare.Row > 1)
                    {
                        result = true;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentSquare.Column != "A")
                    {
                        result = true;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentSquare.Column != "H")
                    {
                        result = true;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets the new position based on the keyboard key pressed
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetNewPosition(int currentPosition, ConsoleKey key)
        {
            var result = currentPosition;
            var square = this.Squares[currentPosition];
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (square.Row < 8)
                    {
                        result++;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (square.Row > 1)
                    {
                        result--;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (square.Column != "A")
                    {
                        result -= 8;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (square.Column != "H")
                    {
                        result += 8;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// Plants a definable number of mines at random places in the board
        /// </summary>
        /// <param name="numberOfMinesToPlant"></param>
        public void PlantMines(int numberOfMinesToPlant)
        {
            var randomNumbers = new List<int>();
            var rnd = new Random();
            for (int x = 0; x < numberOfMinesToPlant; x++)
            {
                int squareToMine;
                
                // random number may be the same, keep generating random numbers until it is not present in the list
                do
                {
                    squareToMine = rnd.Next(2, 63);
                } while (randomNumbers.Contains(squareToMine));

                randomNumbers.Add(squareToMine);
                this.PlantMine(squareToMine);
            }
        }

        /// <summary>
        /// Checks if square is mined based on position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsSquareMined(int position)
        {
            return this.Squares[position].IsMined;
        }

        /// <summary>
        /// Clears a mine that a user has already stepped on
        /// </summary>
        /// <param name="position"></param>
        public void ClearMine(int position)
        {
            this.Squares[position].IsMined = false;
        }

        /// <summary>
        /// Gets the square coordinates for display
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public string GetSquareCoordinates(int position)
        {
            var square = this.Squares[position];
            return $"{square.Column},{square.Row}";
        }

        /// <summary>
        /// Check if game is over based on current position and lives left
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="livesLeft"></param>
        /// <returns></returns>
        public bool IsGameOver(int currentPosition, int livesLeft)
        {
            return livesLeft == 0 || this.IsLastRow(currentPosition);
        }

        /// <summary>
        /// Plants a mine at a specific position on the board
        /// </summary>
        /// <param name="index"></param>
        private void PlantMine(int index)
        {
            var square = Squares[index];
            square.IsMined = true;
        }

        /// <summary>
        /// Checks if the position is in the last row of the board
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool IsLastRow(int position)
        {
            return this.Squares[position].Row == 8;
        }
    }
}
