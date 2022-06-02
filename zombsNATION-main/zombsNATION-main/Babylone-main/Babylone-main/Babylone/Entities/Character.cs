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
        public int damages;
        public bool hit;
        public List < Attacks > ListeAttaques;
        public static Attacks attaque;

        public Character(string _name) {
            name = _name;
            this.ListeAttaques = new List < Attacks > ();
        }

        public static bool Attaque(Character joueurAttaque, Character joueurDefend, int attaqueChoisie, int numeroJoueur, bool isFlash, int dealFlash) {
            Random random = new Random();
            int transfoHpLoss = 0;
            int hit;
            bool _hit;

            Console.WriteLine("----------");

            if (joueurAttaque.ListeAttaques[attaqueChoisie].percentHealthCostUnderTransformation > 0) {
                transfoHpLoss = joueurAttaque.health*joueurAttaque.ListeAttaques[attaqueChoisie].percentHealthCostUnderTransformation/100;
                joueurAttaque.health -= transfoHpLoss;

                Console.WriteLine(joueurAttaque.name + " Attaque avec " + joueurAttaque.ListeAttaques[attaqueChoisie].attackName + " ! " + "-" + joueurAttaque.ListeAttaques[attaqueChoisie].attackEnergyCost + " points d'énergie et -" + transfoHpLoss + " points de vie");
            }
            else {
                Console.WriteLine(joueurAttaque.name + " Attaque avec " + joueurAttaque.ListeAttaques[attaqueChoisie].attackName + " ! " + "-" + joueurAttaque.ListeAttaques[attaqueChoisie].attackEnergyCost + " points d'énergie");
            }
            
            joueurAttaque.energy -= joueurAttaque.ListeAttaques[attaqueChoisie].attackEnergyCost;
            Thread.Sleep(Program.sleepTime);
            switch (isFlash) {
                case true:
                    hit = random.Next(1, dealFlash); // Détermine si l'attaque touche ou non (flash)
                    break;

                case false:
                    hit = random.Next(1, joueurAttaque.ListeAttaques[attaqueChoisie].attackHitChances); // Détermine si l'attaque touche ou non
                    break;
            }
            
            if (hit > 50) { // Si ça touche
                joueurAttaque.hit = true;
                int damagesDealt = random.Next(joueurAttaque.ListeAttaques[attaqueChoisie].attackDamagesMin, joueurAttaque.ListeAttaques[attaqueChoisie].attackDamagesMax) * joueurAttaque.ListeAttaques[attaqueChoisie].damagesMultiplicator;
                Console.WriteLine("Touché ! " + joueurAttaque.name + " inflige " + damagesDealt + " points de dégâts.");

                _hit = true;
                joueurDefend.health -= damagesDealt; // Retire les hp de l'ennemi  
                joueurAttaque.transformationPoints++; // Gagne des points de transformation     
            }
            else {
                _hit = false;
                Console.WriteLine("Loupé !");
            }

            return _hit;
        }

        public void Repos(Character joueur) {
            Random random = new Random();

            int gainedEnergy = random.Next(2,7);
            joueur.energy += gainedEnergy;
            
            int healOrNot = random.Next(1,3);
            int gainedHealth = random.Next(1,70);

            Console.WriteLine("----------");

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

        public static void Transformation(Character joueurAttaque, int numeroJoueur) {
            joueurAttaque.isTransfo = true;
            int lostHealth = joueurAttaque.health/4;
            int dodgeChancesUnderTransformation = joueurAttaque.dodgeChances*3/2;

            switch (joueurAttaque.name) {
                case "Monkey D. Luffy":
                    Console.WriteLine("Luffy active le Gear Second ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    joueurAttaque.ListeAttaques[0].attackName = "Gum Gum no Jet Pistol";
                    joueurAttaque.ListeAttaques[1].attackName = "Gum Gum no Jet Stamp";
                    joueurAttaque.ListeAttaques[2].attackName = "Gum Gum no Jet Bazooka";
                    joueurAttaque.ListeAttaques[3].attackName = "Gum Gum no Jet Rocket";
                    joueurAttaque.ListeAttaques[4].attackName = "Gum Gum no Jet Gatling Gun";

                    break;

                case "Uzumaki Naruto":
                    Console.WriteLine("Naruto active le Mode Baryon ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    joueurAttaque.ListeAttaques[0].attackName = "Naruto Nisen Rendan";
                    joueurAttaque.ListeAttaques[1].attackName = "Gamakichi : Boule de feu suprême";
                    joueurAttaque.ListeAttaques[2].attackName = "Multiclonage Supra";
                    joueurAttaque.ListeAttaques[3].attackName = "Gama Bunta : Ittoryuu Iai";
                    joueurAttaque.ListeAttaques[4].attackName = "Senpo : Biju Rasenshuriken";

                    break;

                case "Son Goku":
                    Console.WriteLine("Goku active l'Ultra Instinct ! (-" + lostHealth + " points de vie)");

                    // Changer le nom des attaques
                    
                    joueurAttaque.ListeAttaques[0].attackName = "Twin Dragon Shot";
                    joueurAttaque.ListeAttaques[1].attackName = "Super Kamehameha";
                    joueurAttaque.ListeAttaques[2].attackName = "Kaioken Kamehameha";
                    joueurAttaque.ListeAttaques[3].attackName = "Ranbu Gekimetsu";
                    joueurAttaque.ListeAttaques[4].attackName = "Kamehameha Divin";

                    break;
            }
            
            // Perte de vie due à la transfo

            joueurAttaque.health -= lostHealth;

            // Self harming à chaque attaque

            joueurAttaque.ListeAttaques[0].percentHealthCostUnderTransformation = 5;
            joueurAttaque.ListeAttaques[1].percentHealthCostUnderTransformation = 5;
            joueurAttaque.ListeAttaques[2].percentHealthCostUnderTransformation = 10;
            joueurAttaque.ListeAttaques[3].percentHealthCostUnderTransformation = 10;
            joueurAttaque.ListeAttaques[4].percentHealthCostUnderTransformation = 15;

            // Augmenter les chances d'esquive

            joueurAttaque.dodgeChances = dodgeChancesUnderTransformation;

            // Augmenter les dégâts des attaques

            joueurAttaque.ListeAttaques[0].damagesMultiplicator = joueurAttaque.ListeAttaques[0].damagesMultiplicator*2;
            joueurAttaque.ListeAttaques[1].damagesMultiplicator = joueurAttaque.ListeAttaques[1].damagesMultiplicator*2;
            joueurAttaque.ListeAttaques[2].damagesMultiplicator = joueurAttaque.ListeAttaques[2].damagesMultiplicator*2;
            joueurAttaque.ListeAttaques[3].damagesMultiplicator = joueurAttaque.ListeAttaques[3].damagesMultiplicator*2;
            joueurAttaque.ListeAttaques[4].damagesMultiplicator = joueurAttaque.ListeAttaques[4].damagesMultiplicator*2;              

            // Augmenter le coût des attaques

            for (int i = 0; i < 5; i++) {
                joueurAttaque.ListeAttaques[i].attackEnergyCost++;
            }
        }
    }      
}