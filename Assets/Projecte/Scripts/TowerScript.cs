using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TowerType {MAGE_TOWER, ARCHER_TOWER, WARRIOR_TOWER, GOLD_TOWER, SPEED_TOWER, COUNT};
public struct TowerStates
{
   public int health;
    public float area;
    public int damage;
    public int range;
    public float attackSpeed;
    public int moneyPerSecond;
};

public class TowerScript : MonoBehaviour
{
    GameObject player;// jugador el que controla
    TowerStates state;
    private float speed;
    TowerType type;
    GameObject objective;  //Al que atacara
    Vector2 posMouse;
    bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        TowerStates(1500, 10, 30, 1.6f, 12, 10);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void TowerStates(int h, int v, int dny, float dps, int r, int mps)
    {
        state.health = h;
        state.area = v;
        state.damage = dny;
        state.attackSpeed = dps;
        state.range = r;
        state.moneyPerSecond = mps;

    }
    /* private:
    //Mira si hi ha algú per atacar
    GameObject AnyoneToAttack()
    {
        GameObject enemyToAttack;

        return enemyToAttack;
    } */

    //Si segueix en rang enemic
    bool StillInRange()
    {
       

        return true;
    }
    
    //Atacar l'enemic.
    void AttackEnemy()
    {
        //
    }

    //Al clickar
    private void OnMouseDown()
    {
        if (isClicked)
        {
            isClicked = false;
        }
        else
        {
            isClicked = true;
        }
    }
}
