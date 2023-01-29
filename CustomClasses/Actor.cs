using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomClasses {
    ///<summary>
    /// CustomClasses - Actor
    /// Autumn Clark
    /// CS 1182
    /// Professor Holmes
    /// Class that instantiates an Actor
    /// </summary>
    [Serializable]
    public abstract class Actor {
        #region Class Level Variables
        private string _Name;
        private string _Title;
        private int _HP;
        private bool _IsAlive = true;
        private int _AttackSpeed;
        private int[] _MapPosition = new int[2];
        private int _MaxHP;
        public enum Directions { Up, Down, Right, Left}
        /*Citation - Word lists were found at https://grammar.yourdictionary.com/capitalization/rules-for-capitalization-in-titles.html
         * https://www.englishclub.com/grammar/prepositions-list.htm
         * https://www.english-grammar-revolution.com/list-of-conjunctions.html
         * I also added some myself that were missing from the lists I found
         */
        private string[] _NonCasedWords = { "for", "and", "nor", "but", "or", "yet", "so", "a", "an", "some", "the", "aboard", "about", "above", "across", "after",
            "against", "along", "amid", "among", "anti", "around", "as", "at", "before", "behind", "below", "beneath", "beside", "besides", "between",
            "beyond", "but", "by", "concerning", "considering", "despite", "down", "during", "except", "excepting", "excluding", "following", "for",
            "from", "in", "inside", "into", "like", "minus", "near", "of", "off", "on", "onto", "opposite", "outside", "over", "past", "per", "plus",
            "regarding", "round", "save", "since", "than", "through", "to", "toward", "towards", "under", "underneath", "unlike", "until", "up",
            "upon", "versus", "via", "with", "within", "without" };
        #endregion Class Level Variables

        #region Properties
        /// <summary>
        /// Property that gets and sets _Name
        /// </summary>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        /// <summary>
        /// Property that gets and sets _Title
        /// </summary>
        public string Title {
            get {
                return _Title;
            }
            set {
                _Title = value;
            }
        }

        /// <summary>
        /// Property that gets _Name and _Title
        /// </summary>
        /// <remarks> Read only </remarks>
        public string NameAndTitle {
            get {
                return _Name + " " + _Title;
            }
        }

        /// <summary>
        /// Property that gets _HP
        /// </summary>
        /// <remarks> Read only </remarks>
        public int HP {
            get {
                return _HP;
            }
        }

        /// <summary>
        /// Property that gets _IsAlive
        /// </summary>
        /// <remarks> Read Only </remarks>
        public bool IsAlive {
            get {
                return _IsAlive;
            }
        }

        /// <summary>
        /// Property that gets and sets _AttackSpeed
        /// </summary>
        /// <remarks> Read only </remarks>
        public virtual int AttackSpeed {
            get {
                return _AttackSpeed;
            }
        }

        /// <summary>
        /// Property that gets _MapPosition
        /// </summary>
        /// <remarks> Read only </remarks>
        public int[] MapPosition {
            get {
                return _MapPosition;
            }
        }

        /// <summary>
        /// Property that gets _MaxHP
        /// </summary>
        /// <remarks> Read only </remarks>
        public int MaxHP {
            get {
                return _MaxHP;
            }
        }
        #endregion Properties

        #region Constructor
        /// <summary>
        /// Overloaded constructor to instantiate an Actor
        /// </summary>
        /// <param name="name"> Name of the actor </param>
        /// <param name="title"> Title of the actor </param>
        /// <param name="hP"> Health points of the actor (must be > 0) </param>
        /// <param name="attackSpeed"> Attack speed of the actor </param>
        /// <param name="mapPosition"> X and Y coordinates of the actor (must be >= 0 and only have two indexes) </param>
        public Actor(string name, string title, int hP, int attackSpeed, int[] mapPosition) {
            Name = CaseString(name);
            Title = CaseString(title);
            if (hP > 0) {
                _HP = hP;
                _MaxHP = hP;
            }
            //Set _AttackSpeed if greater than zero
            //Set through private variable, as it shouldn't change
            if (attackSpeed > 0) {
                _AttackSpeed = attackSpeed;
            }
            if (mapPosition[0] >= 0 && mapPosition[1] >= 0 && mapPosition.Length == 2) {
                _MapPosition = mapPosition;
            }
        }
        #endregion Constructor

        #region Methods
        /// <summary>
        /// Method to move the actor
        /// </summary>
        /// <param name="direction"> Direction the actor is moving </param>
        /// <remarks> Validation for moving is present on Map.MoveHero method </remarks>
        public virtual void Move(Directions direction) {
            if( direction == Directions.Up) {
                _MapPosition[0] -= 1;
            }
            if (direction == Directions.Down) {
                _MapPosition[0] += 1;
            }
            if (direction == Directions.Right) {
                _MapPosition[1] +=1;
            }
            if (direction == Directions.Left) {
                _MapPosition[1] -= 1;
            }
        }

        /// <summary>
        /// Method to determine if the actor attacks first
        /// </summary>
        /// <returns> True if this actor attacks first </returns>
        public bool DoIAttackFirst() {
            bool doIAttackFirst = false;

            return doIAttackFirst;
        }

        /// <summary>
        /// Method for the actor to gain HP
        /// </summary>
        /// <param name="pointValue"> HP to be gained </param>
        public void GainHP(int pointValue) {
            _HP += pointValue;
            if(HP > MaxHP) {
                _HP = MaxHP;
            }
        }

        /// <summary>
        /// Method for the actor to lose HP
        /// </summary>
        /// <param name="pointValue"></param>
        public void LoseHP(int pointValue) {
            _HP -= pointValue;
            if(HP <= 0) {
                //Accesses the private variable as property is read only
                _IsAlive = false;
                _HP = 0;
            }
        }

        /// <summary>
        /// Method to case a string
        /// </summary>
        /// <param name="nameOrTitle"> String to be cased </param>
        /// <returns> The string fully cased </returns>
        public string CaseString(string nameOrTitle) {
            string casedString = "";
            string[] temp = nameOrTitle.ToLower().Split();
            for(int i = 0; i < temp.Length; i++) {
                bool isCasable = true;
                for (int j = 0; j < _NonCasedWords.Length; j++) {
                    if(temp[i] == _NonCasedWords[j]) {
                        isCasable = false;
                    }
                }
                if (isCasable) {
                    casedString += temp[i][0].ToString().ToUpper() + temp[i].Substring(1) + " ";
                } else {
                    casedString += temp[i] + " ";
                }
            }
            return casedString.Trim();
        }
        #endregion Methods
    }
}
