using System;
using System.Collections.Generic;

namespace JeuAllumette
{
    class Program
    {
        class Matches
        {
            List<string> AlumList = new List<string>();
            Random rd = new Random();
            int turn;
            int nbMatches;
            int matchesToPlayWith;

            public void PrintBoard(List<string> AlumList)
            {
                for (int i = 0; i < AlumList.Count; i++)
                {
                    Console.Write(AlumList[i]);
                }
                Console.WriteLine();
            }

            public int PlayerInputs()
            {
                string Line = Console.ReadLine();
                if(int.TryParse(Line, out int result))
                {
                    return result;
                }
                return -1;
            }

            public bool CheckInput(int result)
            {
                switch (result)
                {
                    case 1:
                    case 2:
                    case 3:
                        return true;
                    default:
                        Console.WriteLine("Veuillez selectionner un nombre entre 1 et 3 \n");
                        return false;
                }
            }

            public int WhoseStarting()
            {
                Console.Write("Qui commence ? choississez 0 pour le joueur et 1 pour L'IA : ");

                string Line1 = Console.ReadLine();
                if (int.TryParse(Line1, out int turn))
                {
                    return turn;
                }
                return -1;
            }

            public bool CheckWhoseStarting(int turn)
            {
                switch (turn)
                {
                    case 0:
                    case 1:
                        return true;
                    default:
                        Console.WriteLine("Veuillez selectionner un nombre entre 0 et 1 \n");
                        return false;
                }
            }


            public void TakeMatchesOut(int result, List<string> AlumList)
            {
                for (int i = 0; i < result; i++)
                {
                    AlumList.Remove(" | ");
                }
            }

            public bool IsGameOver(List<string> AlumList, bool playerTurn)
            {
                if(AlumList.Count == 0)
                {
                    if(playerTurn)
                    {
                        Console.WriteLine("L'IA a retirer la dernière allumette,le joueur a gagner !!");
                    }
                    else
                    {
                        Console.WriteLine("Le joueur a retirer la dernière allumette, l'IA a gagner !!");
                    }
                    return true; 
                }
                else
                {
                    return false;
                }
            }

            public bool PlayerTurn(int turn)
            {
                if (turn % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            public void AIturn(List<string> AlumList, Random rd)
            {
                //genere un nb aleatoire
                int nB;

                if(AlumList.Count % 4 == 0)
                {
                    nB = 3;
                }
                else if (AlumList.Count % 6 == 0)
                {
                    nB = 1;
                }
                else if (AlumList.Count % 7 == 0)
                {
                    nB = 2;
                }
                else
                {
                    nB = rd.Next(1, 3);
                }

                //utilise la fonction TakeMatchesOut
                switch (AlumList.Count)
                {
                    case 4:
                        TakeMatchesOut(3, AlumList);
                        Console.WriteLine("L'IA a choisi de retirer " + 3 + " allumettes");
                        break;
                    case 3:
                        TakeMatchesOut(2, AlumList);
                        Console.WriteLine("L'IA a choisi de retirer " + 2 + " allumettes");
                        break;
                    case 2:
                        TakeMatchesOut(1, AlumList);
                        Console.WriteLine("L'IA a choisi de retirer " + 1 + " allumettes");
                        break;
                    default:
                        TakeMatchesOut(nB, AlumList);
                        Console.WriteLine("L'IA a choisi de retirer " + nB + " allumettes");
                        break;
                }
            }
            
            public int NbOfMatchesToPlayWith()
            {
                Console.Write("Avec combien d'allumette voulez vous jouer ? : ");

                string Line = Console.ReadLine();
                if (int.TryParse(Line, out int matchesToPlayWith))
                {
                    return matchesToPlayWith;
                }
                return -1;
            }

            public bool ChechNbOfMatchesToPlayWith(int matchesToPlayWith)
            {
                if (matchesToPlayWith > 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Veuillez selectionner un nombre superieur a 1 \n");
                    return false;
                }
            }

            public void StartGame()
            {
                do
                {
                   turn = WhoseStarting();

                } while (!CheckWhoseStarting(turn));

                Console.WriteLine();

                do
                {
                    matchesToPlayWith = NbOfMatchesToPlayWith();

                } while (!ChechNbOfMatchesToPlayWith(matchesToPlayWith));


                for (int i = 0; i < matchesToPlayWith; i++)
                {
                    AlumList.Add(" | ");
                }

                PrintBoard(AlumList);
                Console.WriteLine();

                do
                {
                    bool playerTurn = PlayerTurn(turn);

                    if(playerTurn)
                    {
                        do
                        {
                            Console.Write("Joueur, choissisez le nombre d'allumette à retirer : ");
                            nbMatches = PlayerInputs();

                        } while (!CheckInput(nbMatches));

                        TakeMatchesOut(nbMatches, AlumList);

                        Console.WriteLine("il reste " + AlumList.Count + " allumettes");

                        PrintBoard(AlumList);
                        Console.WriteLine();
                    }
                    else
                    {
                        AIturn(AlumList, rd);
                        Console.WriteLine("il reste " + AlumList.Count + " allumettes");
                        PrintBoard(AlumList);
                        Console.WriteLine();
                    }

                    turn++;
                    Console.WriteLine();

                } while (!IsGameOver(AlumList, PlayerTurn(turn)));
                Console.WriteLine("Merci d'avoir jouer ! \n");

                Replay();
            }

            public void Replay()
            {
                Console.WriteLine("Pour rejouer entrer le mot 'yes' sinon appuyer sur n'importe quel touche");
                string Line = Console.ReadLine();
                Console.WriteLine();
                if (Line == "yes" || Line == "Yes" || Line == "YES")
                    StartGame();
                else
                    return;
            }
        }
        static void Main(string[] args)
        {
            Matches matches = new Matches();

            matches.StartGame();
        }
    }
}
