using MinefieldConsole;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSetupBoard()
        {
            Board board = new Board();

            board.SetupBoard();

            Assert.IsTrue(board.Squares.Count() == 64);
        }

        [TestMethod]
        [DataRow(12, ConsoleKey.DownArrow)]
        [DataRow(64, ConsoleKey.DownArrow)]
        [DataRow(1, ConsoleKey.UpArrow)]
        [DataRow(7, ConsoleKey.UpArrow)]
        public void TestIsMoveValid(int position, ConsoleKey key)
        {
            Board board = new Board();
            board.SetupBoard();
            var result = board.IsMoveValid(position - 1, key);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow(1, ConsoleKey.DownArrow)]
        [DataRow(9, ConsoleKey.DownArrow)]
        [DataRow(1, ConsoleKey.LeftArrow)]
        [DataRow(60, ConsoleKey.RightArrow)]
        [DataRow(56, ConsoleKey.UpArrow)]
        [DataRow(64, ConsoleKey.RightArrow)]
        public void TestIsMoveIsInvalid(int position, ConsoleKey key)
        {
            Board board = new Board();
            board.SetupBoard();
            var result = board.IsMoveValid(position - 1, key);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetNewPosition()
        {
            Board board = new Board();
            board.SetupBoard();
            var result = board.GetNewPosition(2, ConsoleKey.DownArrow);

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void TestPlantMines()
        {
            Board board = new Board();
            board.SetupBoard();
            board.PlantMines(10);
            var countMines = board.Squares.Count(x => x.IsMined);
            Assert.IsTrue(board.Squares.Count(x => x.IsMined) == 10);
        }

        [TestMethod]
        [DataRow(8, 1)]
        [DataRow(6, 0)]
        public void TestIsGameOver(int position, int livesLeft)
        {
            Board board = new Board();
            board.SetupBoard();
            var result = board.IsGameOver(position - 1, livesLeft);

            Assert.IsTrue(result);
        }
    }
}