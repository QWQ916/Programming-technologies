abstract class GameObject
{
    protected int id; protected int x; protected int y;
    protected string name; 
    public string getName() { return name; }
    public int getX() { return x; }
    public int getY() { return y; }
    public int getID() { return id; }
    protected void setXY(int x, int y) { this.y = y; this.x = x; }
}
class Building : GameObject
{
    protected float hp = 1000;
    public bool isBuilt() 
    {
        if (hp > 0) { return true; } 
        else { return false; }
    }
    public void receiveDamage(float damage)
    {
        if (hp > damage) { hp -= damage; }
        else { hp = 0; }
    }
    public float getHP() { return hp; }
}
class Fort : Building
{
    public Fort(int id, string name, int x, int y, float damage)
    {
        this.id = id; this.name = name; this.x = x; this.y = y; this.damage = damage;
    }
    float damage;
    public void attack(Building building)
    {
        if (building.isBuilt()) { building.receiveDamage(damage); }
    }
}
class MobileHouse : Building
{
    public MobileHouse(int id, string name, int x, int y)
    {
        this.id = id; this.name = name; this.x = x; this.y = y;
    }
    public void move(int x0, int y0) { setXY(x0, y0); }
}

class Unit : GameObject
{
    protected float hp = 100; 
    public float getHP() { return hp; }
    public bool isAlive()
    {
        if (hp > 0)
        { return true; }
        else { return false; }
    }
    public void receiveDamage(float damage)
    {
        if (hp > damage) { hp -= damage; }
        else { hp = 0; }
    }
}
class Archer : Unit
{
    
}
class Attacker : Archer
{
    float damage; 
    public Attacker(int id, string name, int x, int y, float damage)
    {
        this.id = id; this.name = name; this.x = x; this.y = y; this.damage = damage;
    }
    public void attack(Unit unit)
    {
        if (unit.isAlive()) { unit.receiveDamage(damage); }
    }
}
class Moveable : Archer
{
    public Moveable(int id, string name, int x, int y)
    {
        this.id = id; this.name = name; this.x = x; this.y = y;
    }
    public void move(int x0, int y0) { setXY(x0, y0); }
}


class Program
{
    private static void Main()
    {
        Attacker unit1 = new Attacker(1, "Fighter", 50, 60, 15);
        Moveable unit2 = new Moveable(2, "Runner", 10, 20);
        Attacker unit3 = new Attacker(5, "Avenger", 30, 40, 75);
        Fort house1 = new Fort(3, "Fortress", 25, 35, 345);
        MobileHouse house2 = new MobileHouse(3, "HouseOnFoot", 10, 50);

        Console.WriteLine($"Персонаж {unit1.getName()} ({unit1.getHP()} HP) был ранен другим персонажем {unit3.getName()} ({unit3.getHP()} HP)!");
        unit3.attack(unit1);
        Console.WriteLine($"У персонажа {unit1.getName()} осталось {unit1.getHP()} HP!");
        Console.WriteLine($"\n\nПерсонаж {unit1.getName()} ({unit1.getHP()} HP) атакует другого персонажа {unit2.getName()} ({unit2.getHP()} HP)!");
        unit1.attack(unit2);
        Console.WriteLine($"У другого персонажа {unit2.getName()} осталось {unit2.getHP()} HP!");
        Console.Write($"Персонаж {unit2.getName()} был на месте атаки (X: {unit2.getX()}, Y: {unit2.getY()}), ");
        unit2.move(350, 400);
        Console.WriteLine($"а после атаки убежал в другое место (X: {unit2.getX()}, Y: {unit2.getY()})");

        Console.WriteLine($"\n\nЗдание {house1.getName()} ({house1.getHP()} HP) атакует здание {house2.getName()} ({house2.getHP()} HP)!");
        house1.attack(house2);
        Console.WriteLine($"У здания {house2.getName()} осталось {house2.getHP()} HP!");
        Console.Write($"Здание {house2.getName()} было на месте атаки (X: {house2.getX()}, Y: {house2.getY()}), ");
        house2.move(250, 250);
        Console.WriteLine($"а после атаки уехало в другое место (X: {house2.getX()}, Y: {house2.getY()})");


        Console.ReadKey();
    }
}