namespace MyProgram.Entities {

    public class Ennemy {
        Random random = new Random();
        public string name;
        public string technique;
        public int hitPersonnage;
        public bool hit;
        public int health;
        public int damagesMultiplicator;

        public Ennemy(string _name, string _technique) {
            name = _name;
            technique = _technique;
        }

        public static int Attaque(Ennemy ennemi1, Character personnage, int targetHealth, int damagesMultiplicator) {
            Random random = new Random();
            
            Console.WriteLine("----------");
            Console.WriteLine(ennemi1.name + " Attaque avec " + ennemi1.technique + " !");
            Thread.Sleep(Program.sleepTime);

            int hit = random.Next(0, personnage.dodgeChances); // Détermine si l'attaque touche ou non

            if (hit < 50) { // Si ça touche
                int damagesDealt = random.Next(8, 15) * damagesMultiplicator;
                Console.WriteLine("Touché ! " + ennemi1.name + " inflige " + damagesDealt + " points de dégâts.");         
                Thread.Sleep(Program.sleepTime);
                targetHealth -= damagesDealt; // Retire les hp du personnage
            }
            else {
                Console.WriteLine("Loupé !");
            }
            
            return targetHealth;
        }
    }
}