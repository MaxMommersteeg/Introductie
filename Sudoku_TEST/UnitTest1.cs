using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuBasis;

namespace Sudoku_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSetValue()
        {
            SudokuGame game = new SudokuGame();

            game.NewGame();

            /*Assert.AreEqual(true, game.SetValue(new Position()
            {
                X = 1,
                Y = 1,
                Value = 5
            }));*/


            while (!game.IsCompleted())
            {
                game.SetValue(game.GetHint());
            }

            Assert.AreEqual(true, game.IsValid());

            int count = 0;
            foreach (Position p in game.GetBoard())
            {
                if (p.Value > 0)
                {
                    count++;
                }
            }

            Assert.AreEqual(81, count);
        }
    }
}
