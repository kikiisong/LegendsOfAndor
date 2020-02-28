using System;
public class Monster: GameUnit
{
    int maxWP;
    int maxSP;
    int redDice;
    int blackDice;
    int regionLabel;
    public Monster(MonsterType mt, int regionLabel)
    {
        switch (mt)
        {
            //TOOD: Fix them in correct name
            case MonsterType.Gor:
                maxWP =4;
                maxSP =6;
                redDice = 2;
                blackDice = 0;
                break;
            case MonsterType.Skral:
                maxWP = 4;
                maxSP = 6;
                redDice = 2;
                blackDice = 0;
                break;
            case MonsterType.Troll:

                maxWP = 4;
                maxSP = 6;
                redDice = 2;
                blackDice = 0;
                break;
        }

        this.regionLabel = regionLabel;

    }

    public int calculateAttack(int dice) {
        return dice + maxSP;
    }

    public int getMaxSP() {
        return this.maxSP;
    }

    public int getMaxWP() {
        return this.maxWP;
    }

    public int getRedDice() {
        return this.redDice;
    }

    public int getBlackDice() {
        return this.blackDice;
    }

    public int getRegionLabel() {
        return this.regionLabel;
            }

    public void setRegionLabel(int newPosition) {
        this.regionLabel = newPosition;
    }


}
