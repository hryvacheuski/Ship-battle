using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shipbattle
{
    
    class Field {
        public char[,] _myField = new char[14, 14];
        public char[,] _enemyField = new char[14, 14];
        char[,] _ship;
        
        public char[,] GetMyField()
        {
            return _myField;
        }
        public char[,] GetEnemyField()
        {
            return _enemyField;
        }

        public Field()
        {
            _myField = Fields(_myField);
            _enemyField = Fields(_enemyField);
        }
        private char[,] Fields(char[,] myField)
        {
            int letter = 64;
            int capitalLetter = 96;
            for (int i = 0; i < myField.GetLength(0); i++)
            {
                for (int j = 0; j < myField.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    {
                        myField[i, j] = ' ';
                    }
                    else
                    if (i == 0 && j > 0 && j < _myField.GetLength(1) - 1)
                    {
                        letter++;
                        myField[i, j] = Convert.ToChar(letter);

                    }
                    else
                    if (j == 0 && i > 0 && i < myField.GetLength(0) - 1)
                    {
                        capitalLetter++;
                        myField[i, j] = Convert.ToChar(capitalLetter);

                    }
                    else
                    if (i == 11 || j == 11)
                    {
                        myField[i, j] = '.';
                    }
                    else
                    {
                        myField[i, j] = '.';
                    }

                }
            }
            return myField;
        }
        public void ShowFields()
        {
            Console.WriteLine("====MY FIELD====");

            for (int i = 0; i < _myField.GetLength(0)-3; i++)
            {
                for (int j = 0; j < _myField.GetLength(1)-3; j++)
                {
                    Console.Write(_myField[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("====ENEMY FIELD====");
            for (int i = 0; i < _enemyField.GetLength(0)-3; i++)
            {
                for (int j = 0; j < _enemyField.GetLength(1)-3; j++)
                {
                    Console.Write(_enemyField[i, j]);
                }
                Console.WriteLine();
            }

        }

    }
    class Logic {

        Ship ship;
        Field f;
        Field e;
        Field dbf;
        Field dbfe;

        int numOfOneCelledShip = 0;
        int numOfTwoCelledShip = 0;
        int numOfThreeCelledShip = 0;
        int numOfFourCelledShip = 0;
        int numOfShipOnField = 0;
        int deadShips;

        public Logic(Field f, Field e)
        {
            this.f = f;
            this.e = e;
            dbf = f;
            dbfe = e;
        }
        /*public Logic(Field dbf, Field dbfe)
        {
            this.dbf = dbf;
            this.dbfe = dbfe;
            deadShips = 0;

        }*/

        public void Attack()
        {
            ;
            bool strikeStreak = true;
            int x, y, temp;
            x = 0;
            y = 0;
            while (strikeStreak)
            {
                Console.WriteLine("Enter X coordinate of attack");
                temp = Convert.ToInt32(Console.ReadLine());
                if (temp >= 1 && temp <= 10)
                {
                    x = temp;
                }
                else
                {
                    Console.WriteLine("Wrong X coordinate");
                }
                Console.WriteLine("Enter Y coordinate of attack");
                temp = Convert.ToInt32(Console.ReadLine());
                if (temp >= 1 && temp <= 10)
                {
                    y = temp;
                }
                else
                {
                    Console.WriteLine("Wrong Y coordinate");
                }

                if (dbfe._myField[x, y] == 'S')
                {

                    if (dbfe._myField[x, y + 1] == 'S' || dbfe._myField[x, y - 1] == 'S' || dbfe._myField[x + 1, y] == 'S' || dbfe._myField[x - 1, y] == 'S')
                    {
                        Console.WriteLine("Hitted!");
                        dbf._enemyField[x, y] = 'H';
                        dbfe._myField[x, y] = 'H';

                        continue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("DEAD");
                        deadShips++;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        //Point kill on my field
                        dbf._enemyField[x, y] = 'x';
                        dbfe._myField[x, y] = 'x';
                        for (int i = 0; i < dbf._enemyField.GetLength(0); i++)
                        {
                            for (int j = 0; j < dbf._enemyField.GetLength(1); j++)
                            {
                                if (dbf._enemyField[i, j] == 'H')
                                {
                                    dbf._enemyField[i, j] = 'x';
                                    dbfe._myField[i, j] = 'x';
                                }
                            }
                        }
                        dbf.ShowFields();
                        if (deadShips==10)
                        {
                            for (int i = 0; i < 100000; i++)
                            {
                                Console.WriteLine("WIN");
                                
                            }
                        }
                        continue;
                    }
                    
                }
                else
                {
                    strikeStreak = false;
                }


            }
        }
        public void PutShip()
        {
            //Length of ship
            Ship ship = null;
            char[,] shipChar = null;
            bool shipsOnField = false;
            while(!shipsOnField)
            {
                if (numOfFourCelledShip+numOfOneCelledShip+numOfThreeCelledShip+numOfTwoCelledShip == 11)
                {
                    shipsOnField = true;
                }

                int x, y, temp,cell;
                char or;
                bool isShipCanBeCreated = false;
                bool isCoordinateOfXCorrect = false;
                bool isCoordintaeOfYCorrect = false;
                cell = 0;

                Console.Write("Enter length of ship : ");
                temp = Convert.ToInt32(Console.ReadLine());
                if (temp <=4 )
                {
                    cell = temp;
                }

                ship = new Ship(cell);  //Create of given length
                switch (cell)
                {
                    case 1:
                        numOfOneCelledShip++;
                        break;
                    case 2:
                        numOfTwoCelledShip++;
                        break;
                    case 3:
                        numOfThreeCelledShip++;
                        break;
                    case 4:
                        numOfFourCelledShip++;
                        break;
                }
                shipChar = ship.GetShip(); // Get ship of given legnth
               

                if (cell == 1 && numOfOneCelledShip <= 4 )
                {
                    numOfShipOnField++;
                    ship = new Ship(cell);
                    shipChar = ship.GetShip();
                    isShipCanBeCreated = true;
                }
                if (cell == 2 && numOfTwoCelledShip <= 3)
                {
                    numOfShipOnField++;
                    ship = new Ship(cell);
                    shipChar = ship.GetShip();
                    isShipCanBeCreated = true;

                }
                if (cell == 3 && numOfThreeCelledShip <= 2)
                {
                    numOfShipOnField++;
                    ship = new Ship(cell);
                    shipChar = ship.GetShip();
                    isShipCanBeCreated = true;

                }
                if (cell == 4 && numOfFourCelledShip <= 1)
                {
                    numOfShipOnField++;
                    ship = new Ship(cell);
                    shipChar = ship.GetShip();
                    isShipCanBeCreated = true;
                }
                if(!isShipCanBeCreated)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ship cannot be created! Limit of {0} celled ships", cell);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    continue;
                }
                //Coorinates of ship
                x = 1;
                y = 1;
                //Check is coordintaes on field
                do
                {
                    Console.Write("Enter coordinate X of ship (capital letter): ");
                    temp = Convert.ToInt32(Console.ReadLine());
                    if (temp <= 10)
                    {
                        x = temp;
                        isCoordinateOfXCorrect = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ship cannot be created!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                } while (!isCoordinateOfXCorrect);
                do
                {
                    temp = 0;
                    Console.Write("Enter coordinate Y of ship (small letter): ");
                    temp = Convert.ToInt32(Console.ReadLine());
                    if (temp <= 10)
                    {
                        y = temp;
                        isCoordintaeOfYCorrect = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ship cannot be created!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                } while (!isCoordintaeOfYCorrect);

                //Orientation of ship 
                Console.Write("Enter orientation of ship: ");
                or = Convert.ToChar(Console.ReadLine());
                if (x + cell >= 12 || y + cell >= 12 || 
                    f._myField[x,y] == 'S'||
                    f._myField[x-1, y] == 'S' ||
                    f._myField[x+1, y] == 'S' ||
                    f._myField[x, y - 1] == 'S' ||
                    f._myField[x - 1, y - 1] == 'S' ||
                    f._myField[x + 1, y - 1] == 'S' ||
                    f._myField[x, y + 1] == 'S' ||
                    f._myField[x - 1, y + 1 ] == 'S' ||
                    f._myField[x + 1, y + 1 ] == 'S'
                    )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ship cannot be created!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (cell==1)
                    {
                        numOfOneCelledShip--;
                    }
                    if (cell == 2)
                    {
                        numOfTwoCelledShip--;
                    }
                    if (cell == 3)
                    {
                        numOfThreeCelledShip--;
                    }
                    if (cell == 4)
                    {
                        numOfFourCelledShip--;
                    }
                    continue;
                }
                else {
                    if (or == 'V' || or == 'v')
                    {
                        for (int i = 0; i < shipChar.GetLength(0); i++)
                        {
                            for (int j = 0; j < shipChar.GetLength(1); j++)
                            {
                                f._myField[i + y, x] = shipChar[i, j]; // BUG HERE! When ship rotates (x,y) transposes (y,x)
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < shipChar.GetLength(0); i++)
                        {
                            for (int j = 0; j < shipChar.GetLength(1); j++)
                            {
                                f._myField[x, i + y] = shipChar[i, j]; // BUG HERE ALL SHIPS ARE 4x4 and if you spawn 1celled ship at 9x9 will be an expetion
                            }
                        }
                    }
                }
                
                //Put ship on a field
                f.ShowFields();
            }
        }
    }
    class Ship
    {
        public int numberOfCells;
        public bool isAlive;
        public int numberOfHits;
        public int numOfOneCelledShip;
        public int numOfTwoCelledShip;
        public int numOfThreeCelledShip;
        public int numOfFourCelledShip;
        public int numOfShipOnField;
         private char[,] _ship = new char[4, 4] {
             {'.','.','.','.'},
             {'.','.','.','.'},
             {'.','.','.','.'},
             {'.','.','.','.'},
         };
        public bool Damage()
        {
            if (numberOfHits < numberOfCells)
            {
                numberOfHits++;
                isAlive = true;
                return false;
            }
            else
            {
                isAlive = false;
                return true;
            }

        }
        private void SetShip(int length)
        {
            const int yLenght= 3;

            for (int i = 0; i < length; i++)
            {
                _ship[i, yLenght] = 'S';
            }
        }
        public char[,] GetShip()
        {
            return _ship;
        }
        public char[,] RotateShip(char[,] ship)
        {
            char[,] newShip = new char[4, 4] {
            {'.','.','.','.'},
            {'.','.','.','.'},
            {'.','.','.','.'},
            {'.','.','.','.'},

           };

            for (int i = 0; i < _ship.GetLength(0); i++)
            {
                for (int j = 0; j < _ship.GetLength(1); j++)
                {
                    newShip[j, i] = ship[i, j];
                }
            }
            return newShip;
            
        }

        public Ship(int numberOfCells)
        {
            bool ShipLimit = true;
            this.numberOfCells = numberOfCells;
            isAlive = true;
            numberOfHits = 0;
            numOfOneCelledShip = 0;
            numOfTwoCelledShip = 0;
            numOfThreeCelledShip = 0;
            numOfFourCelledShip = 0;
            SetShip(numberOfCells);
        }

    }
    class DebugField
    {
        public char[,] _myField = new char[14, 14] {
            {'.','A','B','C','D','E','F','G','I','J','K','.','.','.'},
            {'A','S','.','.','.','.','.','.','.','.','S','.','.','.'},
            {'B','.','.','.','.','S','S','.','.','.','.','.','.','.'},
            {'C','.','.','.','.','.','.','.','.','.','.','.','.','.'},
            {'D','.','.','S','.','S','.','S','.','.','.','.','.','.'},
            {'E','.','.','S','.','S','.','S','.','.','.','.','.','.'},
            {'F','.','.','S','.','S','.','S','.','.','.','.','.','.'},
            {'G','.','.','.','.','S','.','.','.','.','.','.','.','.'},
            {'I','.','.','.','.','.','.','S','S','.','.','.','.','.'},
            {'J','.','.','S','S','.','.','.','.','.','.','.','.','.'},
            {'K','S','.','.','.','.','.','.','.','.','S','.','.','.'},
            {'.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
            {'.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
            {'.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
        };
        public char[,] _enemyField = new char[14, 14]{
            {'.','A' ,'B','C','D','E','F','G','I','J','K','.','.','.'},
            {'A','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'B','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'C','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'D','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'E','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'F','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'G','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'I','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'J','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'K','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'.','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'.','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
            {'.','.' ,'.','.','.','.','.','.','.','.','.','.','.','.'},
        };
        char[,] _ship;

        public char[,] GetMyField()
        {
            return _myField;
        }
        public char[,] GetEnemyField()
        {
            return _enemyField;
        }

        public DebugField()
        {
        }
        public void ShowFields()
        {
            Console.WriteLine("====MY FIELD====");

            for (int i = 0; i < _myField.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < _myField.GetLength(1) - 3; j++)
                {
                    Console.Write(_myField[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("====ENEMY FIELD====");
            for (int i = 0; i < _enemyField.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < _enemyField.GetLength(1) - 3; j++)
                {
                    Console.Write(_enemyField[i, j]);
                }
                Console.WriteLine();
            }

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FOR CREATING SHIP TYPE SIZE (X,Y) AND ORIENTATION H=HORIZONTAL, V=VERTICAL \nEXAMPLE \n4 A a H \n");

            bool isWin = false;
            Field field = new Field();
            Field field1 = new Field();
            Logic logic = new Logic(field, field1);
            Logic logic1 = new Logic(field1,field);
            //DEBUG
            DebugField testField1 = new DebugField();
            DebugField testField2 = new DebugField();
            Logic testLogic1 = new Logic(field, field1);
            Logic testLogic2 = new Logic(field1 ,field);

            //testField1.ShowFields();
            field.ShowFields();
            logic.PutShip();
            field.ShowFields();
            Console.Clear();
            field1.ShowFields();
            logic1.PutShip();
            field1.ShowFields();
            Console.Clear();

            while (!isWin)
            {
                Console.Clear();
                Console.WriteLine("1st player");
                field.ShowFields();
                logic.Attack();
                Console.Clear();
                Console.WriteLine("2nd player");
                field1.ShowFields();
                logic1.Attack();

            }

        }
    }
}
