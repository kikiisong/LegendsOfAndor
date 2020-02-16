using System;
using Photon.Pun;
using Routines;
public abstract class GameUnit: MonoBehaviourPun
{

    int WP;
    int SP;

    public void setWP(int pWP) {
        this.WP = pWP;
    }

    public void setSP(int pSP) {
        this.SP = pSP;
    }

    public int getWP() {
        return this.WP;
    }

    public int getSP() {
        return this.SP;
    }

}

