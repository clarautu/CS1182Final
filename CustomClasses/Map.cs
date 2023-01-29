using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomClasses {
    /// <summary>
    /// CustomClasses - Map
    /// Autumn Clark
    /// CS 1182
    /// Professor
    /// Class that instantiates a Map
    /// </summary>
    [Serializable]
    public class Map {
        #region Class Level Variables
        private MapCell[,] _GameBoard;
        private List<Item> _Items;
        private List<Monster> _Monsters;
        private Hero _Hero;
        private Boss _Boss;
        #endregion

        #region Properties
        /// <summary>
        /// Property that gets _Gameboard
        /// </summary>
        /// <remarks> Read Only </remarks>
        public MapCell[,] GameBoard {
            get {
                return _GameBoard;
            }
        }

        /// <summary>
        /// Private property that gets _Items
        /// </summary>
        /// <remarks> Read Only </remarks>
        private List<Item> Items {
            get {
                return _Items;
            }
        }

        /// <summary>
        /// Private property that gets _Monsters
        /// </summary>
        /// <remarks> Read Only </remarks>
        private List<Monster> Monsters {
            get {
                return _Monsters;
            }
        }

        /// <summary>
        /// Property that gets and sets _Hero
        /// </summary>
        public Hero Hero {
            get {
                return _Hero;
            }
            set {
                _Hero = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _Boss
        /// </summary>
        public Boss Boss {
            get {
                return _Boss;
            }
            set {
                _Boss = value;
            }
        }

        /// <summary>
        /// Property that gets the highest current HP of Monsters
        /// </summary>
        /// <remarks> Read Only </remarks>
        public int HighestMonsterHP {
            get {
                var maxMonsterHP = Monsters.Max(x => x.HP);
                return maxMonsterHP;
            }
        }

        /// <summary>
        /// Property that gets the lowest AffectValue of the Weapons in Items
        /// </summary>
        /// <remarks> Read Only </remarks>
        public int WeakestWeaponDamage {
            get {
                var weapons = Items.Where(x => x.GetType() == typeof(Weapon));
                var weakestWeaponDamage = weapons.Min(x => x.AffectValue);
                return weakestWeaponDamage;
            }
        }

        /// <summary>
        /// Property that gets the average AffectValue of the Potions in Items
        /// </summary>
        /// <remarks> Read Only </remarks>
        public double AveragePotionAffectValue {
            get {
                var potions = Items.Where(x => x.GetType() == typeof(Potion));
                var averagePotionAffectValue = potions.Average(x => x.AffectValue);
                return averagePotionAffectValue;
            }
        }

        /// <summary>
        /// Property that gets the number of Monsters on GameBoard
        /// </summary>
        /// <remarks> Read Only </remarks>
        public int CountMonstersOnMap {
            get {
                int countMonsters = 0;
                foreach (MapCell x in GameBoard) {
                    if (x.ContainsMonster) {
                        countMonsters++;
                    }
                }
                return countMonsters;
            }
        }

        /// <summary>
        /// Property that gets the number of Items on GameBoard
        /// </summary>
        /// <remarks> Read Only </remarks>
        public int CountItemsOnMap {
            get {
                int countItems = 0;
                foreach (MapCell x in GameBoard) {
                    if (x.ContainsItem) {
                        countItems++;
                    }
                }
                return countItems;
            }
        }

        /// <summary>
        /// Property that gets the percentage of how much of GameBoard has been discovered
        /// </summary>
        /// <remarks> Read Only </remarks>
        public double PercentMapDiscovered {
            get {
                double countDiscoveredMapCells = 0;
                foreach (MapCell x in GameBoard) {
                    if (x.IsDiscovered) {
                        countDiscoveredMapCells++;
                    }
                }
                return countDiscoveredMapCells / GameBoard.Length;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Overloaded constructor that instantiates a Map
        /// </summary>
        /// <param name="numOfRows"> Number of rows </param>
        /// <param name="numOfColumns"> Number of columns </param>
        public Map(int numOfRows, int numOfColumns) {
            FillMap(numOfRows, numOfColumns);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Private method to assign _GameBoard and fill it
        /// </summary>
        /// <param name="numOfRows"> Number of rows </param>
        /// <param name="numOfColumns"> Number of Columns </param>
        private void FillMap(int numOfRows, int numOfColumns) {
            //Declare _GameBoard size and instantiate
            _GameBoard = new MapCell[numOfRows, numOfColumns];
            //Random number generator for determining if a cell is filled and if so, what goes in it
            Random random = new Random();
            //Variables for tracking Door and DoorKey placement and instantiation
            bool placedBoss = false;
            bool placedDoor = false;
            string code = "Ocarina of Time";
            int countEmptyCells = 0;

            //Fill _Items, _Monsters, and _Boss
            FillPotions();
            FillWeapons();
            FillMonsters();
            CreateBoss(code);

            for (int i = 0; i < _GameBoard.GetLength(0); i++) {
                for (int j = 0; j < _GameBoard.GetLength(1); j++) {
                    //Set the location of each MapCell and instantiate it
                    int[] location = { i, j };
                    _GameBoard[i, j] = new MapCell(location);

                    //40% chance to fill each MapCell with an Item or Monster
                    int filledOrNot = random.Next(5);
                    if (filledOrNot == 0 || filledOrNot == 1) {
                        //If filled, about 66% chance to be an Item, otherwise a Monster
                        int whichItem = random.Next(3);
                        //Fill with an Item
                        if (whichItem == 0 || whichItem == 1) {
                            Item tempItem = Items[random.Next(Items.Count)];

                            //If a Potion, cast it and then create a deep copy for the MapCell
                            if (tempItem.GetType() == typeof(Potion)) {
                                Potion potion = (Potion)tempItem;
                                _GameBoard[i, j].Item = potion.CreateCopy();

                                //If a Weapon, cast it and then create a deep copy for the MapCell
                            } else {
                                Weapon weapon = (Weapon)tempItem;
                                _GameBoard[i, j].Item = weapon.CreateCopy();
                            }
                            //Fill with a Monster
                        } else {
                            _GameBoard[i, j].Monster = Monsters[random.Next(Monsters.Count)].CreateCopy();

                        }
                        //MapCells that weren't filled
                    } else {
                        countEmptyCells++;
                        //Place the Boss after a certain number of empty MapCells - make sure the Boss and Door aren't right next to each other
                        if (countEmptyCells == (int)(numOfColumns * numOfRows / 10) && placedBoss == false) {
                            _GameBoard[i, j].Monster = Boss.CreateCopy();
                            placedBoss = true;
                        }
                        //Place a Door after a certain number of empty MapCells - make sure the key and door aren't right next to each other
                        if (countEmptyCells == (int)(numOfColumns * numOfRows / 6) && placedDoor == false) {
                            _GameBoard[i, j].Item = new Door("Door", 0, code);
                            placedDoor = true;
                        }
                    }
                }
            }

            //If the door or key weren't placed, try populating again - shouldn't happen, but just in case
            if (placedDoor == false || placedBoss == false) {
                FillMap(numOfRows, numOfColumns);
            }
        }

        /// <summary>
        /// Private method to add Potion options to _Items
        /// </summary>
        private void FillPotions() {
            //Instantiate _Items
            _Items = new List<Item>();
            //List of all possible Potions to be found in the game
            List<Potion> potionOptions = new List<Potion> {new Potion("Red Potion", 40, Potion.Colors.Red), new Potion("Blue Potion", 60, Potion.Colors.Blue),
                new Potion("Magic Potion", 80, Potion.Colors.White), new Potion("Weak Potion", 20, Potion.Colors.Black)};

            //Add a copy of each Potion to _Items
            foreach (Potion potion in potionOptions) {
                Items.Add(potion.CreateCopy());
            }
        }

        /// <summary>
        /// Private method to add Weapon options to _Items
        /// </summary>
        /// <remarks> _Items was instantiated in FillPotions, so no need to instantiate it </remarks>
        private void FillWeapons() {
            //List of all possible Weapons to be found in the game
            List<Weapon> weaponOptions = new List<Weapon> { new Weapon("Master Sword", 40, 10), new Weapon("Small Club", 10, 10), new Weapon("Hidden Blade", 30, 1),
                new Weapon("Bow", 20, 7), new Weapon("Dagger", 15, 2)};

            //Add a copy of each Weapon to _Items
            foreach (Weapon weapon in weaponOptions) {
                Items.Add(weapon.CreateCopy());
            }
        }

        /// <summary>
        /// Private method to put Monsters in _Monsters
        /// </summary>
        private void FillMonsters() {
            //Instantiate _Monsters
            _Monsters = new List<Monster>();
            //List of all possible Monsters to be found in the game
            int[] placeHolder = { 0, 0 };
            List<Monster> monsterOptions = new List<Monster> {new Monster("Slime", "the Slimy", 30, 1, placeHolder, 1), new Monster("Orcy", "of Barcelona", 80, 10, placeHolder, 15),
                new Monster("Skully", "the Bone Seeker", 20, 25, placeHolder, 30)};

            //Add a copy of each Monster to _Monsters
            foreach (Monster monster in monsterOptions) {
                Monsters.Add(monster.CreateCopy());
            }
        }

        /// <summary>
        /// Private method that creates the boss
        /// </summary>
        private void CreateBoss(string code) {
            int[] placeHolder = { 0, 0 };
            Boss grawgnog = new Boss("Grawgnog", "the Indifferent", 400, 5, placeHolder, 30, new DoorKey("Key", 0, code));
            Boss = grawgnog.CreateCopy();
        }

        /// <summary>
        /// Method to return the MapCell where the Hero is
        /// </summary>
        /// <returns> The MapCell with matching position of the Hero. If not on _GameBoard, return a new MapCell with no location </returns>
        public MapCell HeroPosition() {
            //Hero's map position
            int[] heroPosition = _Hero.MapPosition;

            //Loop through every MapCell in _GameBoard
            for (int i = 0; i < _GameBoard.GetLength(0); i++) {
                for (int j = 0; j < _GameBoard.GetLength(1); j++) {
                    //If MapCell Location matches heroPosition, return matching MapCell
                    if (_GameBoard[i, j].Location == heroPosition) {
                        return _GameBoard[i, j];
                    }
                }
            }
            //Hero is not on the board, return a new MapCell
            return new MapCell();
        }

        /// <summary>
        /// Method to move the Hero and mark current MapCell as discovered. Validates direction won't move Hero off the board, then passes it to Hero.Move method.
        /// </summary>
        /// <param name="direction"> Actor.Directions enum </param>
        /// <returns> True if the Hero needs to react, false if he doesn't </returns>
        public bool MoveHero(Actor.Directions direction) {
            if (direction == Actor.Directions.Up && Hero.MapPosition[0] - 1 > -1) {
                Hero.Move(Actor.Directions.Up);
            }
            if (direction == Actor.Directions.Down && Hero.MapPosition[0] + 1 < GameBoard.GetLength(0)) {
                Hero.Move(Actor.Directions.Down);
            }
            if (direction == Actor.Directions.Right && Hero.MapPosition[1] + 1 < GameBoard.GetLength(1)) {
                Hero.Move(Actor.Directions.Right);
            }
            if (direction == Actor.Directions.Left && Hero.MapPosition[1] - 1 > -1) {
                Hero.Move(Actor.Directions.Left);
            }

            //Get Hero's new MapPosition and assign it to x and y coordinate variables
            int xCoordinate = Hero.MapPosition[0];
            int yCoordinate = Hero.MapPosition[1];
            GameBoard[xCoordinate, yCoordinate].IsDiscovered = true;
            //If MapCell at Hero.MapPosition contains an Item or Monster, return true; false otherwise
            if (GameBoard[xCoordinate, yCoordinate].ContainsItem || GameBoard[xCoordinate, yCoordinate].ContainsMonster) {
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }
}
