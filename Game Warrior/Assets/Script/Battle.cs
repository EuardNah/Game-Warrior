using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private List <Warrior>  _warriorsList = new List<Warrior>()
    {
        new Kinght(100, 20, DamageType.Melee, 30),
        new Archer(60,15,0.7f),
        new Dragon(200,30,DamageType.Range)
    };
    [SerializeField] private List<Text> _warriorsTexts;
    void Start()
    {
        StartCoroutine(Fight());

        print(_warriorsList[0].GetInfo());
        _warriorsList[0].TakeDamage(_warriorsList[1].Damage);
        print(_warriorsList[0].GetInfo());
        _warriorsList[0].TakeDamage(_warriorsList[1].Damage);
        print(_warriorsList[0].GetInfo());
        
        
    }

    private IEnumerator Fight()
    {
        for (int i = 0; i < _warriorsList.Count; i++)
        {
            _warriorsTexts[i].text = _warriorsList[i].GetType() + "\n" + _warriorsList[i].GetInfo();
        }
        yield return new WaitForSeconds(2f);

        _warriorsList[0].TakeDamage(_warriorsList[1].Damage);

        for (int i = 0; i < _warriorsList.Count; i++)
        {
            _warriorsTexts[i].text = _warriorsList[i].GetType() + "\n" + _warriorsList[i].GetInfo();
        }
    }

}

public enum DamageType { Melee, Range, Magic};

public class Warrior
{
    protected int _health;
    protected DamageType  DmgType;

    public virtual int Damage {get; protected set;}

    public bool isAlive {get { return _health >0;}}

    public Warrior( int health, int damage, DamageType dmgType)
    {
        _health = health;
        Damage = damage;
        DmgType = dmgType;
    }

    public virtual void TakeDamage(int damage)
    {

        _health -= damage;

    }

    public string GetInfo()
    {
        return "Hp: " + _health +"\n" + "Dmg: " + Damage;
    }
}


public class Kinght : Warrior 
{
    private int _armor;

    public Kinght (int health,int damage,DamageType dmgType, int armor) : base(health,damage,dmgType)
    {
        _armor = armor;
    }

    public override void TakeDamage(int damage)
    {
        damage -= _armor/4;
        if (damage > 0)
            _health -= damage;
    }
}

public class Archer : Warrior
{

    private float _criticalChance;

    public override int Damage 
    {
        get
        {
            float rand = Random.Range(0f,1f);
            if (rand <= _criticalChance)
                return base.Damage*2;
            else
                return base.Damage;
        }

    }
    public Archer(int health, int damage, float criticalChance) : base(health, damage, DamageType.Range)
    {
        _criticalChance = criticalChance;
    }

}

public class Dragon : Warrior
{
    public Dragon(int health, int damage, DamageType dmgType) : base(health, damage, dmgType){}
    public void Heal(int health)
    {
        _health += health;
    }
}
