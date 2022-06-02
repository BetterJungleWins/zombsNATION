namespace MyProgram.Entities
{
    public class Attacks {
        public string attackUser;
        public string attackName;
        public string attackType;
        public int attackDamagesMin;
        public int attackDamagesMax;
        public int attackEnergyCost;
        public int damagesMultiplicator;
        public int attackHitChances;
        public int percentHealthCostUnderTransformation;

        public Attacks(string _attackUser, string _attackName, string _attackType, int _attackDamagesMin, int _attackDamagesMax, int _damagesMultiplicator, int _attackHitChances, int _attackEnergyCost, int _percentHealthCostUnderTransformation) {
            attackUser = _attackUser;
            attackName = _attackName;
            attackType = _attackType;
            attackDamagesMin = _attackDamagesMin;
            attackDamagesMax = _attackDamagesMax;
            damagesMultiplicator = _damagesMultiplicator;
            attackHitChances = _attackHitChances;
            attackEnergyCost = _attackEnergyCost;
            percentHealthCostUnderTransformation = _percentHealthCostUnderTransformation;
        }
    }
}