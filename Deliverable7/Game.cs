using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomClasses;

namespace Deliverable7 {
    public static class Game {
        #region Class Level Variables
        private static Map _Map;
        private static State _GameState = State.Running;
        private static int _Height;
        private static int _Width;
        private static bool _IsFirstCombat = true;
        public enum State { Running, Lost, Won}
        #endregion

        #region Properties
        /// <summary>
        /// Property that gets and sets _Map
        /// </summary>
        public static Map Map {
            get {
                return _Map;
            }
            set {
                _Map = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _GameState. If Map.Hero is dead, _GameState is Lost
        /// </summary>
        public static State GameState {
            get {
                if(Map.Hero.IsAlive == false) {
                    _GameState = State.Lost;
                }
                return _GameState;
            }
            set {
                _GameState = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _Height
        /// </summary>
        public static int Height {
            get {
                return _Height;
            }
            set {
                _Height = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _Width
        /// </summary>
        public static int Width {
            get {
                return _Width;
            }
            set {
                _Width = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _IsFirstCombat
        /// </summary>
        public static bool IsFirstCombat {
            get {
                return _IsFirstCombat;
            }
            set {
                _IsFirstCombat = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Overloaded method to reset the game. Instantiates new Map and Hero classes and sets Hero at a random, unoccupied spot
        /// </summary>
        /// <param name="numOfRows"> Number of rows in gameboard </param>
        /// <param name="numOfColumns"> Number of columns in gameboard </param>
        public static void ResetGame(int numOfRows, int numOfColumns) {
            //Set Height and Width
            Height = numOfRows;
            Width = numOfColumns;
            //Set GameState
            GameState = State.Running;
            _Map = new Map(Height, Width);
            //Outsourced unoccupied logic to private method RandomHeroPosition
            int[] mapPosition = RandomHeroPosition();
            _Map.Hero = new Hero("Bob", "the Unbroken", 400, 15, mapPosition);
            //Mark the starting MapCell as discovered
            _Map.GameBoard[mapPosition[0], mapPosition[1]].IsDiscovered = true;
        }

        /// <summary>
        /// Overloaded method to reset the game. Instantiates new Map and Hero classes and sets Hero at a random, unoccupied spot
        /// Defaults to a 10 x 10 gameboard
        /// </summary>
        public static void ResetGame() {
            Height = 10;
            Width = 10;
            ResetGame(Height, Width);
        }

        /// <summary>
        /// Private method to set a new Hero at a random position on the board that is unoccupied.
        /// </summary>
        /// <returns> An int[] that is an unoccupied position on _Map.GameBoard </returns>
        private static int[] RandomHeroPosition() {
            //Complie a list of empty MapCells
            List<MapCell> emptyCells = new List<MapCell>();
            for(int i = 0; i < _Map.GameBoard.GetLength(0); i++) {
                for(int j = 0; j < _Map.GameBoard.GetLength(1); j++) {
                    if(_Map.GameBoard[i, j].ContainsItem != true && _Map.GameBoard[i, j].ContainsMonster != true) {
                        emptyCells.Add(_Map.GameBoard[i, j]);
                    }
                }
            }
            //Select a random empty MapCell and return its coordinates
            Random random = new Random();
            int x = random.Next(emptyCells.Count);
            int xCoordinate = emptyCells[x].Location[0];
            int yCoordinate = emptyCells[x].Location[1];
            int[] heroPosition = { xCoordinate, yCoordinate };
            return heroPosition;
        }
        #endregion
    }
}
