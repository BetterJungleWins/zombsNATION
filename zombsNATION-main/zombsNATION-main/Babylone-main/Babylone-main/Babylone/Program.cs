using System;
using System.Collections.Generic;
using System.Windows;
using MyProgram.Entities;

namespace MyProgram {

    class Program {
        public static bool verif = false;
        public static int sleepTime = 1500;
        public static Character joueur1;
        public static Character joueur2;
        public static List < Character > ListePersonnages = new List < Character > ();

        public static bool BetweenRanges(int a, int b, int number) {
            return (a <= number && number <= b);
        }

        public static int Combat(Character whoStarts, Character whoFollows, int _whoStarts, int _whoFollows) {
            int whoStartsSaignement;
            int whoStartsDealSaignement;
            bool whoStartsStun = false;
            int whoStartsDealStun;
            bool whoStartsFlash = false;
            int whoStartsDealFlash = 0;
            int whoFollowsSaignement;
            int whoFollowsDealSaignement;
            bool whoFollowsStun = false;
            int whoFollowsDealStun;
            bool whoFollowsFlash = false;
            int whoFollowsDealFlash = 0;
            int winner = 0;
            bool fight = true;
            int choixAction = 0;
            int attaqueChoisie;
            bool hit = false;
            Random random = new Random();

            Console.WriteLine("----------");
            Console.WriteLine("C'est le Joueur " + _whoStarts + " qui commence !");

            while (fight) {
                Console.WriteLine("----------");
                Console.WriteLine("Rappel des stats :");
                Console.WriteLine("----------");
                RappelStats(whoStarts, _whoStarts);
                Console.WriteLine("----------");
                RappelStats(whoFollows, _whoFollows);

                Thread.Sleep(Program.sleepTime);

                // Le joueur qui commence attaque

                if (whoStarts.energy > 0 && !whoStartsStun) {
                    if (!whoStarts.isTransfo && whoStarts.transformationPoints > 2) {
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoStarts + " (" + whoStarts.name + "), que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Transformation");
                            Console.WriteLine("3. Se reposer");
                            Console.WriteLine("----------");
                            choixAction = VerifSaisie(choixAction, 1, 3);

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoStarts, _whoStarts);
                                    Console.WriteLine("----------");
                                    if (whoStartsFlash) {
                                        Console.WriteLine(whoStarts.name + " est aveuglé, chances de toucher réduites.");
                                    }
                                    hit = Character.Attaque(whoStarts, whoFollows, attaqueChoisie, _whoStarts,whoStartsFlash, whoStartsDealFlash);
                                    switch (whoStarts.ListeAttaques[attaqueChoisie].attackType) {
                                        case "saignement":
                                            if (hit) {
                                                whoFollowsSaignement = random.Next(20,30);
                                                whoFollowsDealSaignement = random.Next(1,500);

                                                if (whoFollowsDealSaignement > 250) {
                                                    Console.WriteLine(whoStarts.name + " inflige : " + whoStarts.ListeAttaques[attaqueChoisie].attackType + " ! (-" + whoFollowsSaignement + " hp)");
                                                    whoFollows.health -= whoFollowsSaignement;
                                                }
                                            }
                                            break;

                                        case "stun":
                                            if (hit) {
                                                whoFollowsDealStun = random.Next(1,500);
                                                if (whoFollowsDealStun > 250) {
                                                    whoFollowsStun = true;
                                                }
                                                whoFollowsFlash = false;
                                            }
                                            break;
                                        
                                        case "flash":
                                            if (hit) {
                                                whoFollowsDealFlash = random.Next(90,110);
                                                if (whoFollowsDealFlash > 100) {
                                                    whoFollowsFlash = true;
                                                }
                                                whoFollowsStun = false;
                                            }
                                            break;
                                    }

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(Program.sleepTime);

                                    break;

                                case 2:
                                    Console.WriteLine("----------");
                                    Character.Transformation(whoStarts, _whoStarts);

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    whoStarts.isTransfo = true;
                                    Thread.Sleep(sleepTime);
                                    break;

                                case 3:
                                    whoStarts.Repos(whoStarts);

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(sleepTime);
                                    break;
                            }

                        } else {
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoStarts + " (" + whoStarts.name + "), que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Se reposer");
                            Console.WriteLine("----------");
                            choixAction = VerifSaisie(choixAction, 1, 2);

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoStarts, _whoStarts);
                                    if (whoStartsFlash) {
                                        Console.WriteLine(whoStarts.name + " est aveuglé, chances de toucher réduites.");
                                    }
                                    hit = Character.Attaque(whoStarts, whoFollows, attaqueChoisie, _whoStarts,whoStartsFlash, whoStartsDealFlash);
                                    switch (whoStarts.ListeAttaques[attaqueChoisie].attackType) {
                                        case "saignement":
                                            if (hit) {
                                                whoFollowsSaignement = random.Next(20,30);
                                                whoFollowsDealSaignement = random.Next(1,500);

                                                if (whoFollowsDealSaignement > 250) {
                                                    Console.WriteLine(whoStarts.name + " inflige : " + whoStarts.ListeAttaques[attaqueChoisie].attackType + " ! (-" + whoFollowsSaignement + " hp)");
                                                    whoFollows.health -= whoFollowsSaignement;
                                                }
                                            }
                                            break;

                                        case "stun":
                                            if (hit) {
                                                whoFollowsDealStun = random.Next(1,500);
                                                if (whoFollowsDealStun > 250) {
                                                    whoFollowsStun = true;
                                                }
                                                whoFollowsFlash = false;
                                            }
                                            break;
                                        
                                        case "flash":
                                            if (hit) {
                                                whoFollowsDealFlash = random.Next(90,110);
                                                if (whoFollowsDealFlash > 100) {
                                                    whoFollowsFlash = true;
                                                }
                                                whoFollowsStun = false;
                                                }
                                            break;
                                    }   

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(sleepTime);

                                    break;

                                case 2:
                                    whoStarts.Repos(whoStarts);

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(sleepTime);                            
                                    break;
                            }                        
                    }

                }
                else {
                    Console.WriteLine("----------");
                    if (whoStarts.energy == 0) {
                        Console.WriteLine(whoStarts.name + ": énergie insuffisante pour attaquer.");
                    }
                    if (whoStartsStun) {
                        Console.WriteLine(whoStarts.name + " est étourdi.");
                    }
                    Console.WriteLine("----------");
                    Console.WriteLine("Joueur " + _whoStarts + " (" + whoStarts.name + "),que souhaitez-vous faire ?");
                    Console.WriteLine("1. Se reposer");
                    Console.WriteLine("----------");
                    choixAction = VerifSaisie(choixAction, 1, 1);

                    Console.Clear();

                    if (choixAction == 1) {
                        whoStarts.Repos(whoStarts);

                        if (whoStarts.health < 1) {
                            winner = _whoFollows;
                            fight = false;
                        }
                        if (whoFollows.health < 1) {
                            winner = _whoStarts;
                            fight = false;
                        }

                        Thread.Sleep(Program.sleepTime);
                    }

                }

                if (whoStarts.health < 1) {
                    winner = _whoFollows;
                    fight = false;
                }
                if (whoFollows.health < 1) {
                    winner = _whoStarts;
                    fight = false;
                }

                // Le joueur qui suit attaque

                Console.WriteLine("----------");
                Thread.Sleep(sleepTime);

                Console.WriteLine("Rappel des stats :");
                Console.WriteLine("----------");
                RappelStats(whoStarts, _whoStarts);
                Console.WriteLine("----------");
                RappelStats(whoFollows, _whoFollows);

                if (whoFollows.energy > 0 && !whoFollowsStun) {
                    if (!whoFollows.isTransfo && whoFollows.transformationPoints > 2) {
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoFollows + " (" + whoFollows.name + "), que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Transformation");
                            Console.WriteLine("3. Se reposer");
                            choixAction = VerifSaisie(choixAction, 1, 3);

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoFollows, _whoFollows);
                                    Console.WriteLine("----------");
                                    if (whoFollowsFlash) {
                                        Console.WriteLine(whoFollows.name + " est aveuglé, chances de toucher réduites.");
                                    }
                                    hit = Character.Attaque(whoFollows, whoStarts, attaqueChoisie, _whoFollows,whoFollowsFlash, whoFollowsDealFlash);
                                    switch (whoFollows.ListeAttaques[attaqueChoisie].attackType) {
                                        case "saignement":
                                            if (hit) {
                                                whoStartsSaignement = random.Next(20,30);
                                                whoStartsDealSaignement = random.Next(1,500);

                                                if (whoStartsDealSaignement > 250) {
                                                    Console.WriteLine(whoFollows.name + " inflige : " + whoFollows.ListeAttaques[attaqueChoisie].attackType + " ! (-" + whoStartsSaignement + " hp)");
                                                    whoStarts.health -= whoStartsSaignement;
                                                }
                                            }
                                            break;

                                        case "stun":
                                            if (hit) {
                                                whoStartsDealStun = random.Next(1,1000);
                                                if (whoStartsDealStun > 250) {
                                                    whoStartsStun = true;
                                                }
                                                whoStartsFlash = false;
                                            }
                                            break;
                                        
                                        case "flash":
                                            if (hit) {
                                                whoStartsDealFlash = random.Next(90,110);
                                                if (whoStartsDealFlash > 100) {
                                                    whoStartsFlash = true;
                                                }
                                                whoStartsStun = false;
                                            }
                                            break;
                                    }

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(Program.sleepTime);

                                    break;

                                case 2:
                                    Console.WriteLine("----------");
                                    Character.Transformation(whoFollows, _whoFollows);

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    whoFollows.isTransfo = true;
                                    Thread.Sleep(sleepTime);
                                    break;

                                case 3:
                                    whoFollows.Repos(whoFollows);

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(sleepTime);                            
                                    break;
                            }
                    } else {
                            Console.WriteLine("----------");
                            Console.WriteLine("Joueur " + _whoFollows + " (" + whoFollows.name + "), que voulez-vous faire ?");

                            Console.WriteLine("1. Attaquer");
                            Console.WriteLine("2. Se reposer");
                            choixAction = VerifSaisie(choixAction, 1, 2);

                            Console.Clear();

                            switch (choixAction) {
                                case 1:
                                    attaqueChoisie = choixAttaque(whoFollows, _whoFollows);
                                    if (whoFollowsFlash) {
                                        Console.WriteLine(whoFollows.name + " est aveuglé, chances de toucher réduites.");
                                    }
                                    hit = Character.Attaque(whoFollows, whoStarts, attaqueChoisie, _whoFollows, whoFollowsFlash, whoFollowsDealFlash);
                                    switch (whoFollows.ListeAttaques[attaqueChoisie].attackType) {
                                        case "saignement":
                                            if (hit) {
                                                whoStartsSaignement = random.Next(20,30);
                                                whoStartsDealSaignement = random.Next(1,500);

                                                if (whoStartsDealSaignement > 250) {
                                                    Console.WriteLine(whoFollows.name + " inflige : " + whoFollows.ListeAttaques[attaqueChoisie].attackType + " ! (-" + whoStartsSaignement + " hp)");
                                                    whoStarts.health -= whoStartsSaignement;
                                                }
                                            }
                                            break;

                                        case "stun":
                                        if (hit) {
                                            whoStartsDealStun = random.Next(1,500);
                                            if (whoStartsDealStun > 250) {
                                                whoStartsStun = true;
                                            }
                                            whoStartsFlash= false;
                                        }
                                            break;
                                        
                                        case "flash":
                                            if (hit) {
                                                whoStartsDealFlash = random.Next(90,110);
                                                if (whoStartsDealFlash > 100) {
                                                    whoStartsFlash = true;
                                                }
                                                whoStartsStun = false;
                                            }      
                                            break;
                                    }
                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(sleepTime);

                                    break;

                                case 2:
                                    whoFollows.Repos(whoFollows);

                                    if (whoStarts.health < 1) {
                                        winner = _whoFollows;
                                        fight = false;
                                    }
                                    if (whoFollows.health < 1) {
                                        winner = _whoStarts;
                                        fight = false;
                                    }

                                    Thread.Sleep(sleepTime);                            
                                    break;
                            }                    
                    }

                }
                else {
                    Console.WriteLine("----------");
                    if (whoFollows.energy == 0) {
                        Console.WriteLine(whoFollows.name + ": énergie insuffisante pour attaquer.");
                    }
                    if (whoFollowsStun) {
                        Console.WriteLine(whoFollows.name + " est étourdi.");
                    }
                    Console.WriteLine("----------");

                    Console.WriteLine(" Joueur " + _whoFollows + " (" + whoFollows.name + "),que souhaitez-vous faire ?");
                    Console.WriteLine("1. Se reposer");
                    choixAction = VerifSaisie(choixAction, 1, 1);

                    Console.Clear();

                    if (choixAction == 1) {
                        whoFollows.Repos(whoFollows);

                        if (whoStarts.health < 1) {
                            winner = _whoFollows;
                            fight = false;
                        }
                        if (whoFollows.health < 1) {
                            winner = _whoStarts;
                            fight = false;
                        }

                        Thread.Sleep(Program.sleepTime);
                    }

                    Console.Clear();

                }

                if (whoStarts.health < 1) {
                    winner = _whoFollows;
                    fight = false;
                }
                if (whoFollows.health < 1) {
                    winner = _whoStarts;
                    fight = false;
                }
            }
            
            return winner;
        }

        public static void VerificationDeSaisie(string value) {
            int number;

            bool success = int.TryParse(value, out number);
            if (!success || !(int.TryParse(value, out number) || value == "")) {
                Console.WriteLine("\nSaisie incorrecte, recommencez.");
            }
            else {
                Console.Clear();
            }
        }

        public static int choixAttaque(Character joueur, int numeroJoueur) {
            int attaqueChoisie = 0;

            Console.WriteLine("----------");

            switch (joueur.isTransfo) {
                case true :
                    Console.WriteLine("Avec quoi attaquez-vous ? (" + joueur.energy + " énergie et " + joueur.health + " pv restants)");
                    for (int i = 0; i < 5; i++) {
                        if (joueur.energy < joueur.ListeAttaques[i].attackEnergyCost) {
                            Console.WriteLine(i+1 + " : " + joueur.ListeAttaques[i].attackName + " : Pas assez d'énergie.");
                        }
                        else {
                            Console.WriteLine(i+1 + " : " + joueur.ListeAttaques[i].attackName + " (" + joueur.ListeAttaques[i].attackEnergyCost + " énergie et " + joueur.ListeAttaques[i].percentHealthCostUnderTransformation*joueur.health/100 + " pv). Spécial : " + joueur.ListeAttaques[i].attackType);
                        }
                    }
                    break;

                case false :
                    Console.WriteLine("Avec quoi attaquez-vous ? (" + joueur.energy + " énergie restante)");
                    for (int i = 0; i < 5; i++) {
                        if (joueur.energy < joueur.ListeAttaques[i].attackEnergyCost) {
                            Console.WriteLine(i+1 + " : " + joueur.ListeAttaques[i].attackName + " : Pas assez d'énergie.");
                        }
                        else {
                            Console.WriteLine(i+1 + " : " + joueur.ListeAttaques[i].attackName + " (" + joueur.ListeAttaques[i].attackEnergyCost + " énergie). Spécial : " + joueur.ListeAttaques[i].attackType);
                        }
                    }
                    break;
            }

            attaqueChoisie = VerifSaisie(attaqueChoisie, 1, 5);
            attaqueChoisie--;

            Console.Clear();

            return attaqueChoisie;
        }

        public static void RappelStats(Character joueur, int numeroJoueur) {
            Console.WriteLine("| Joueur " + numeroJoueur);
            Console.WriteLine("| " + joueur.name);
            Console.WriteLine("| Points de vie : " + joueur.health);
            Console.WriteLine("| Énergie restante : " + joueur.energy + " |");
            Console.WriteLine("| Points de transformation : " + joueur.transformationPoints + " |");
        }

        public static int VerifSaisie(int saisie, int borneInf, int borneSup) {
            verif = false;

            while (!verif) {
                saisie = Convert.ToInt32(Console.ReadLine());
                if (borneInf <= saisie && saisie <= borneSup){
                    verif = true;
                }
                else {
                    Console.WriteLine("Saisie incorrecte, recommencez.");
                }
            }

            return saisie;
        }

        static void AddPersonnage(Character joueur, string name, string transformation, int transformationPoints, bool canTransfo, int energy, int health) {
            joueur.name = name;
            joueur.transformation = transformation;
            joueur.transformationPoints = transformationPoints;
            joueur.canTransfo = canTransfo;
            joueur.energy = energy;
            joueur.health = health;

            ListePersonnages.Add(joueur);
        }

        static void Main(string[] args) {
            int personnageChoisiJoueur1 = 0;
            int personnageChoisiJoueur2 = 0;
            string winnerName = "";
            int winFight = 0;
            bool jeu = true;
            Random random = new Random();

            // Début du jeu

            while (jeu) {
                Console.WriteLine("----------");
                Console.WriteLine("Choix des personnages");

                Thread.Sleep(Program.sleepTime);

                joueur1 = new Character("");
                joueur2 = new Character("");

                Console.WriteLine("----------");
                Console.WriteLine("Joueur 1");
                Console.WriteLine("----------");

                Thread.Sleep(sleepTime);

                Console.WriteLine("Choisissez votre personnage :");

                Console.WriteLine("1. Monkey D. Luffy");
                Console.WriteLine("2. Uzumaki Naruto");
                Console.WriteLine("3. Son Goku"); /*
                Console.WriteLine("4. Izuku Midoriya");
                Console.WriteLine("5. Kirua Zoldyck"); */

                personnageChoisiJoueur1 =  VerifSaisie(personnageChoisiJoueur1, 1, 3);              

                if (personnageChoisiJoueur1 == 1) { // Luffy
                    Attacks joueur1attaque1 = new Attacks("Monkey D. Luffy", "Pistol", "saignement", 6, 10, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Monkey D. Luffy", "Stamp", "saignement", 8, 14, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Monkey D. Luffy", "Bazooka", "flash", 11, 15, 3, 470, 2, 0);
                    Attacks joueur1attaque4 = new Attacks("Monkey D. Luffy", "Rocket", "flash", 12, 14, 3, 460, 3, 0);
                    Attacks joueur1attaque5 = new Attacks("Monkey D. Luffy", "Gatling Gun", "stun", 17, 23, 3, 450, 4, 0);
                    joueur1.ListeAttaques.Add(joueur1attaque1);
                    joueur1.ListeAttaques.Add(joueur1attaque2);
                    joueur1.ListeAttaques.Add(joueur1attaque3);
                    joueur1.ListeAttaques.Add(joueur1attaque4);
                    joueur1.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Monkey D. Luffy", "Gear Third", 0, false, 5, 170);
                }
     
                else if (personnageChoisiJoueur1 == 2) { // Naruto
                    Attacks joueur1attaque1 = new Attacks("Uzumaki Naruto", "Naruto Rendan", "saignement", 6, 9, 3, 500, 2, 0);
                    Attacks joueur1attaque2 = new Attacks("Uzumaki Naruto", "Invocation : Gama kichi", "saignement", 10, 16, 4, 480, 3, 0);
                    Attacks joueur1attaque3 = new Attacks("Uzumaki Naruto", "Multiclonage", "flash", 10, 14, 4, 470, 2, 0);
                    Attacks joueur1attaque4 = new Attacks("Uzumaki Naruto", "Invocation : Gama Bunta", "flash", 12, 14, 3, 460, 3, 0);
                    Attacks joueur1attaque5 = new Attacks("Uzumaki Naruto", "Rasengan", "stun", 15, 18, 3, 450, 5, 0);
                    joueur1.ListeAttaques.Add(joueur1attaque1);
                    joueur1.ListeAttaques.Add(joueur1attaque2);
                    joueur1.ListeAttaques.Add(joueur1attaque3);
                    joueur1.ListeAttaques.Add(joueur1attaque4);
                    joueur1.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Uzumaki Naruto", "Mode Baryon", 0, false, 8, 220);
                }

                else if (personnageChoisiJoueur1 == 3) { // Goku
                    Attacks joueur1attaque1 = new Attacks("Son Goku", "Finger Beam", "saignement", 5, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Son Goku", "Vague d'énergie", "saignement", 8, 12, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Son Goku", "Kaioken", "flash", 11, 13, 3, 470, 2, 0);
                    Attacks joueur1attaque4 = new Attacks("Son Goku", "Kiai", "flash", 12, 16, 4, 460, 3, 0);
                    Attacks joueur1attaque5 = new Attacks("Son Goku", "Kamehameha", "stun", 17, 22, 3, 450, 5, 0);
                    joueur1.ListeAttaques.Add(joueur1attaque1);
                    joueur1.ListeAttaques.Add(joueur1attaque2);
                    joueur1.ListeAttaques.Add(joueur1attaque3);
                    joueur1.ListeAttaques.Add(joueur1attaque4);
                    joueur1.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Son Goku", "Ultra instinct", 0, false, 9, 200);
                }

                /* else if (personnageChoisiJoueur1 == 4) { // Deku
                    Attacks joueur1attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur1attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur1attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    joueur1.ListeAttaques.Add(joueur1attaque1);
                    joueur1.ListeAttaques.Add(joueur1attaque2);
                    joueur1.ListeAttaques.Add(joueur1attaque3);
                    joueur1.ListeAttaques.Add(joueur1attaque4);
                    joueur1.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                else if (personnageChoisiJoueur1 == 5) { // Kirua
                    Attacks joueur1attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur1attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur1attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur1attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur1attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    joueur1.ListeAttaques.Add(joueur1attaque1);
                    joueur1.ListeAttaques.Add(joueur1attaque2);
                    joueur1.ListeAttaques.Add(joueur1attaque3);
                    joueur1.ListeAttaques.Add(joueur1attaque4);
                    joueur1.ListeAttaques.Add(joueur1attaque5);
                    AddPersonnage(joueur1, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                } */

                Console.Clear();

                Console.WriteLine("----------");
                Console.WriteLine("Joueur 2");
                Console.WriteLine("----------");

                Thread.Sleep(sleepTime);

                Console.WriteLine("Choisissez votre personnage :");

                Console.WriteLine("1. Monkey D. Luffy");
                Console.WriteLine("2. Uzumaki Naruto");
                Console.WriteLine("3. Son Goku"); /*
                Console.WriteLine("4. Izuku Midoriya");
                Console.WriteLine("5. Kirua Zoldyck"); */

                personnageChoisiJoueur2 = VerifSaisie(personnageChoisiJoueur1, 1, 3);

                if (personnageChoisiJoueur2 == 1) { // Luffy
                    Attacks joueur2attaque1 = new Attacks("Monkey D. Luffy", "Gum Gum Pistol", "saignement", 6, 10, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Monkey D. Luffy", "Gum Gum Stamp", "saignement", 10, 16, 4, 480, 1, 0);
                    Attacks joueur2attaque3 = new Attacks("Monkey D. Luffy", "Gum Gum Bazooka", "flash", 11, 15, 3, 470, 2, 0);
                    Attacks joueur2attaque4 = new Attacks("Monkey D. Luffy", "Gum Gum Rocket", "flash", 12, 14, 3, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Monkey D. Luffy", "Gum Gum Gatling Gun", "stun", 17, 23, 3, 450, 3, 0);
                    joueur2.ListeAttaques.Add(joueur2attaque1);
                    joueur2.ListeAttaques.Add(joueur2attaque2);
                    joueur2.ListeAttaques.Add(joueur2attaque3);
                    joueur2.ListeAttaques.Add(joueur2attaque4);
                    joueur2.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Monkey D. Luffy", "Gear Third", 0, false, 5, 170);
                }

                else if (personnageChoisiJoueur2 == 2) { // Naruto
                    Attacks joueur2attaque1 = new Attacks("Uzumaki Naruto", "Naruto Rendan", "saignement", 6, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Uzumaki Naruto", "Invocation : Gama kichi", "saignement", 10, 16, 4, 480, 2, 0);
                    Attacks joueur2attaque4 = new Attacks("Uzumaki Naruto", "Multiclonage", "flash", 10, 14, 4, 470, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Uzumaki Naruto", "Invocation : Gama Bunta", "flash", 12, 14, 3, 460, 3, 0);
                    Attacks joueur2attaque5 = new Attacks("Uzumaki Naruto", "Rasengan", "stun", 15, 18, 3, 450, 3, 0);
                    joueur2.ListeAttaques.Add(joueur2attaque1);
                    joueur2.ListeAttaques.Add(joueur2attaque2);
                    joueur2.ListeAttaques.Add(joueur2attaque3);
                    joueur2.ListeAttaques.Add(joueur2attaque4);
                    joueur2.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Uzumaki Naruto", "Mode Baryon", 0, false, 10, 220);
                }

                else if (personnageChoisiJoueur2 == 3) { // Goku
                    Attacks joueur2attaque1 = new Attacks("Son Goku", "Finger Beam", "saignement", 5, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Son Goku", "Vague d'énergie", "saignement", 10, 14, 4, 480, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Son Goku", "Kaioken", "flash", 11, 13, 3, 470, 1, 0);
                    Attacks joueur2attaque4 = new Attacks("Son Goku", "Kiai", "flash", 12, 16, 4, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Son Goku", "Kamehameha", "stun", 15, 20, 3, 450, 3, 0);
                    joueur2.ListeAttaques.Add(joueur2attaque1);
                    joueur2.ListeAttaques.Add(joueur2attaque2);
                    joueur2.ListeAttaques.Add(joueur2attaque3);
                    joueur2.ListeAttaques.Add(joueur2attaque4);
                    joueur2.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Son Goku", "Ultra instinct", 0, false, 8, 200);
                }

                /* else if (personnageChoisiJoueur2 == 4) { // Deku
                    Attacks joueur2attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur2attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    joueur2.ListeAttaques.Add(joueur2attaque1);
                    joueur2.ListeAttaques.Add(joueur2attaque2);
                    joueur2.ListeAttaques.Add(joueur2attaque3);
                    joueur2.ListeAttaques.Add(joueur2attaque4);
                    joueur2.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                }

                else if (personnageChoisiJoueur2 == 5) { // Kirua
                    Attacks joueur2attaque1 = new Attacks("Son Goku", "Finger Beam", 5, 9, 3, 500, 1, 0);
                    Attacks joueur2attaque2 = new Attacks("Son Goku", "Vague d'énergie", 10, 14, 4, 480, 2, 0);
                    Attacks joueur2attaque3 = new Attacks("Son Goku", "Kaioken", 11, 13, 3, 470, 1, 0);
                    Attacks joueur2attaque4 = new Attacks("Son Goku", "Kiai", 12, 16, 4, 460, 2, 0);
                    Attacks joueur2attaque5 = new Attacks("Son Goku", "Kamehameha", 15, 20, 3, 450, 3, 0);
                    joueur2.ListeAttaques.Add(joueur2attaque1);
                    joueur2.ListeAttaques.Add(joueur2attaque2);
                    joueur2.ListeAttaques.Add(joueur2attaque3);
                    joueur2.ListeAttaques.Add(joueur2attaque4);
                    joueur2.ListeAttaques.Add(joueur2attaque5);
                    AddPersonnage(joueur2, "Son Goku", "Ultra instinct", 0, false, 8, 200, 3);
                } */

                Console.Clear();

                Thread.Sleep(sleepTime);
                Console.WriteLine("----------");
                Console.WriteLine("Très bons chois !");
                Console.WriteLine(joueur1.name + " VS " + joueur2.name + " !");
                Console.WriteLine("----------");
                Thread.Sleep(sleepTime);
                Console.Clear();

                int whoStarts = random.Next(1,100);
                if (whoStarts < 50) {
                    whoStarts = 1;
                    int whoFollows = 2;
                    winFight = Combat(joueur1, joueur2, 1, 2);
                }
                else {
                    whoStarts = 2;
                    int whoFollows = 1;
                    winFight = Combat(joueur2, joueur1, 2, 1);
                }

                jeu = false;
            }

            if (joueur1.health < 1) {
                    winnerName = joueur2.name;
                }
                else {
                    winnerName = joueur1.name;
            }

            // Avant de quitter

            Console.WriteLine("----------");

            Console.WriteLine("Gagnant : " + winnerName + " !");

            Console.WriteLine("----------");
            Console.WriteLine("Appuyez sur une touche pour quitter");
            Console.ReadKey();
        }
    }
}