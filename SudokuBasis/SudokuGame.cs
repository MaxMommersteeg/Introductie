using Sudoku;
using System.Collections.Generic;
using System.Linq;

namespace SudokuBasis {
    public class SudokuGame
    {
        private IGame game;

        /// <summary>
        /// Constructor and initializing
        /// </summary>
        public SudokuGame() {
            game = new Game();
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        public void NewGame() {
            game.create();
        }

        /// <summary>
        /// Check if value can be set
        /// </summary>
        /// <param name="position">Value with location that the user wants to set</param>
        /// <returns></returns>
        public bool SetValue(Position position) {
            int success;
            game.set(position.X, position.Y, position.Value, out success);
            return success == 1;
        }

        /// <summary>
        /// Get current value for given position
        /// </summary>
        /// <param name="position"></param>
        public void GetValue(Position position) {
            short val;
            game.get(position.X, position.Y, out val);
            position.Value = val;
        }

        /// <summary>
        /// Validate to check if the current filled numbers are correct
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            int isValid;
            game.isValid(out isValid);
            return isValid == 1;
        }

        public bool IsCompleted() {
            //Check if all current filled in values are correct
            if (!IsValid())
                return false;
            return GetBoard().Where(x => x.Value > 0).Count() == 81;
        }

        #region NotUsed

        /// <summary>
        /// Not sure when to use this
        /// </summary>
        /// <returns></returns>
        public bool Read() {
            int succeeded;
            game.read(out succeeded);
            return succeeded == 1;
        }

        /// <summary>
        /// Not sure when to use this
        /// </summary>
        /// <returns></returns>
        public bool Write() {
            int succeeded;
            game.write(out succeeded);
            return succeeded == 1;
        }

        #endregion

        /// <summary>
        /// GetBoard for getting a complete list with all coordinates and values
        /// </summary>
        /// <returns></returns>
        public List<Position> GetBoard() {
            var positions = new List<Position>(81);
            for (short x = 1; x <= 9; x++) {
                for (short y = 1; y <= 9; y++) {
                    var p = new Position();
                    p.X = x;
                    p.Y = y;
                    positions.Add(p);
                    GetValue(p);
                }
            }
            return positions;
        }

        public Position GetHint() {
            short x, y, value;
            int succeeded;
            game.hint(out x, out y, out value, out succeeded);
            //We didn't succeed to retrieve a value
            if (succeeded != 1)
                return null;

            return new Position() {
                X = x,
                Y = y,
                Value = value
            };
        }
    }
}
