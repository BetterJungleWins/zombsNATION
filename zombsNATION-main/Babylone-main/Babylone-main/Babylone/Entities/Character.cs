namespace MyProgram.Entities {
    
    public class Character {
        public string name;
        public string transformation;
        public int transformationPoints;
        public bool canTransfo;
        public bool isTransfo;
        public float experience;
        public int energy;
        public int dodgeChances;
        public int health;
        public int damagesMultiplicator;
        public int damages;
        public bool hit;
        public static List<Attacks> ListeAttaques = new List<Attacks>();
        public static Attacks attaque;

        public Character(string _name) {
            name = _name;
        }

        public static void Attaque(Character joueurAttaque, Character joueurDefend, List<Attacks> ListeAttaques, int attaqueChoisie, int numeroJoueur) {
            Random random = new Random();
            int transfoHpLoss = 0;

            if (ListeAttaques[attaqueChoisie].percentHealthCostUnderTransformation > 0) {
                transfoHpLoss = joueurAttaque.health*ListeAttaques[attaqueChoisie].percentHealthCostUnderTransformation/100;
                joueurAttaque.health -= transfoHpLoss;

                switch (numeroJoueur) {
                    case 1:
                        Console.WriteLine(joueurAttaque.name + " Attaque avec " + ListeAttaques[attaqueChoisie].attackName + " ! " + "-" + ListeAttaques[attaqueChoisie].attackEnergyCost + " points d'énergie et -" + transfoHpLoss + " points de vie");
                        break;

                    case 2:
                        Console.WriteLine(joueurAttaque.name + " Attaque avec " + ListeAttaques[attaqueChoisie+5].attackName + " ! " + "-" + ListeAttaques[attaqueChoisie+5].attackEnergyCost + " points d'énergie et -" + transfoHpLoss + " points de vie");
                        break;
                }
            }
            else {
                switch (numeroJoueur) {
                    case 1:
                        Console.WriteLine(joueurAttaque.name + " Attaque avec " + ListeAttaques[attaqueChoisie].attackName + " ! " + "-" + ListeAttaques[attaqueChoisie].attackEnergyCost + " points d'énergie");
                        break;

                    case 2:
                        Console.WriteLine(joueurAttaque.name + " Attaque avec " + ListeAttaques[attaqueChoisie+5].attackName + " ! " + "-" + ListeAttaques[attaqueChoisie+5].attackEnergyCost + " points d'énergie");
                        break;
                }
            }
            
            joueurAttaque.energy -= ListeAttaques[attaqueChoisie].attackEnergyCost;
            Thread.Sleep(Program.sleepTime);
            int hit = random.Next(1, ListeAttaques[attaqueChoisie].attackHitChances); // Détermine si l'attaque touche ou non

            if (hit > 50) { // Si ça touche
                int damagesDealt = random.Next(ListeAttaques[attaqueChoisie].attackDamagesMin, ListeAttaques[attaqueChoisie].attackDamagesMax) * ListeAttaques[attaqueChoisie].damagesMultiplicator;
                Console.WriteLine("Touché ! " + joueurAttaque.name + " inflige " + damagesDealt + " points de dégâts.");

                joueurDefend.health -= damagesDealt; // Retire les hp de l'ennemi  
                joueurAttaque.transformationPoints++; // Gagne des points de transformation     
            }
            else {
                Console.WriteLine("Loupé !");
            }

        }

        public void Repos(Character joueur) {
            Random random = new Random();

            int gainedEnergy = random.Next(2,7);
            joueur.energy += gainedEnergy;
            
            int healOrNot = random.Next(1,3);
            int gainedHealth = random.Next(1,70);

            switch (healOrNot) {
                case 1:
                    Console.WriteLine(name + " se repose et gagne " + gainedEnergy + " points d'énergie");
                    break;

                case 2:
                    joueur.health += gainedHealth;
                    Console.WriteLine(joueur.name + " se repose et gagne " + gainedEnergy + " points d'énergie et " + gainedHealth + " points de vie.");
                    break;
            }
        }

        public static void Transformation(Character joueurAttaque, List < Attacks > ListeAttaques, int numeroJoueur) {
            joueurAttaque.isTransfo = true;
            int lostHealth = joueurAttaque.health/4;
            int dodgeChancesUnderTransformation = joueurAttaque.dodgeChances*3/2;

            switch (joueurAttaque.name) {
                case "Monkey D. Luffy":
                    Console.WriteLine("Luffy active le Gear Second ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    ListeAttaques[0].attackName = "Gum Gum no Jet Pistol";
                    ListeAttaques[1].attackName = "Gum Gum no Jet Stamp";
                    ListeAttaques[2].attackName = "Gum Gum no Jet Bazooka";
                    ListeAttaques[3].attackName = "Gum Gum no Jet Rocket";
                    ListeAttaques[4].attackName = "Gum Gum no Jet Gatling Gun";

                    break;

                case "Uzumaki Naruto":
                    Console.WriteLine("Naruto active le Mode Baryon ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    ListeAttaques[0].attackName = "Naruto Nisen Rendan";
                    ListeAttaques[1].attackName = "Gamakichi : Boule de feu suprême";
                    ListeAttaques[2].attackName = "Multiclonage Supra";
                    ListeAttaques[3].attackName = "Gama Bunta : Ittoryuu Iai";
                    ListeAttaques[4].attackName = "Senpo : Biju Rasenshuriken";

                    break;

                case "Son Goku":
                    Console.WriteLine("Goku active l'Ultra Instinct ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    ListeAttaques[0].attackName = "Twin Dragon Shot";
                    ListeAttaques[1].attackName = "Super Kamehameha";
                    ListeAttaques[2].attackName = "Kaioken Kamehameha";
                    ListeAttaques[3].attackName = "Ranbu Gekimetsu";
                    ListeAttaques[4].attackName = "Kamehameha Divin";

                    break;
            }
            
            // Perte de vie due à la transfo

            joueurAttaque.health -= lostHealth;

            // Self harming à chaque attaque

            switch (numeroJoueur) {
                case 1:
                    ListeAttaques[0].percentHealthCostUnderTransformation = 5;
                    ListeAttaques[1].percentHealthCostUnderTransformation = 5;
                    ListeAttaques[2].percentHealthCostUnderTransformation = 10;
                    ListeAttaques[3].percentHealthCostUnderTransformation = 10;
                    ListeAttaques[4].percentHealthCostUnderTransformation = 15;

                    // Augmenter les chances d'esquive

                    joueurAttaque.dodgeChances = dodgeChancesUnderTransformation;

                    // Augmenter les dégâts des attaques

                    ListeAttaques[0].damagesMultiplicator = ListeAttaques[0].damagesMultiplicator*2;
                    ListeAttaques[1].damagesMultiplicator = ListeAttaques[1].damagesMultiplicator*2;
                    ListeAttaques[2].damagesMultiplicator = ListeAttaques[2].damagesMultiplicator*2;
                    ListeAttaques[3].damagesMultiplicator = ListeAttaques[3].damagesMultiplicator*2;
                    ListeAttaques[4].damagesMultiplicator = ListeAttaques[4].damagesMultiplicator*2;
                    break;
                
                case 2:
                    ListeAttaques[5].percentHealthCostUnderTransformation = 5;
                    ListeAttaques[6].percentHealthCostUnderTransformation = 5;
                    ListeAttaques[7].percentHealthCostUnderTransformation = 10;
                    ListeAttaques[8].percentHealthCostUnderTransformation = 10;
                    ListeAttaques[9].percentHealthCostUnderTransformation = 15;

                    // Augmenter les chances d'esquive

                    joueurAttaque.dodgeChances = dodgeChancesUnderTransformation;

                    // Augmenter les dégâts des attaques

                    ListeAttaques[5].damagesMultiplicator = ListeAttaques[5].damagesMultiplicator*2;
                    ListeAttaques[6].damagesMultiplicator = ListeAttaques[6].damagesMultiplicator*2;
                    ListeAttaques[7].damagesMultiplicator = ListeAttaques[7].damagesMultiplicator*2;
                    ListeAttaques[8].damagesMultiplicator = ListeAttaques[8].damagesMultiplicator*2;
                    ListeAttaques[9].damagesMultiplicator = ListeAttaques[9].damagesMultiplicator*2;
                    break;
            }
                    
            

        }
    }      
}