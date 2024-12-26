using System.Drawing;

namespace Singleton
{
    class Singleton_
    {
        private static Singleton_ inst;
        private string name;
        private string path;
        private Singleton_(string path)
        {
            name = "TheOnlyObject";
            this.path = path;
        }
        public static Singleton_ getInst(string path = null) // Значение path по умолчанию будет NULL,
                                                            // с помощью этого можно будет при создании объекта
                                                            // игнорировать параметр path 
        {
            if (inst == null)
            {
                if (path == null) { inst = new Singleton_("Не указан"); }
                else { inst = new Singleton_(path); }
            }
            return inst;
        }
        public string getName() { return name; }
        public string getPath() { return path; }
        public override string ToString()
        {
            return $"Имя объекта - {name}; путь объекта - '{path}'!\n";
        }
    }
    class Program 
    { 
        public static void Singleton()
        {
            // !РЕАЛИЗАЦИЯ паттерна Singleton!
            Singleton_ a = Singleton_.getInst(); // В качестве параметра можно указать string path для указания путя объекта,
                                               // если оставить пустым, то путь будет "Не указан"

            // Singleton a1 = Singleton.getInst("C:"); a1 будет копией a, новый объект не создатся.                                           
            // Singleton b = new Singleton("C:"); Ошибка! Из-за уровня доступа. 
            // Singleton b = new Singleton(); Ошибка! Так как конструктор без параметров не предусмотрен.
            // Таким образом можно избежать создания еще одного объекта и создание будет единоразовым с помощью getInst(),
            // а далее эта функция будет возвращать этот единственный объект.
            Console.WriteLine(a);
            Console.WriteLine("Путь объекта {0} -- {1}", a.getName(), a.getPath());
        }
    }
}

namespace FactoryMethod
{
    abstract class Products
    {
        protected int count;
        protected string name;
        protected int pricePerOne;
        public abstract string SellAll();
    }
    class Milk : Products
    {
        public Milk(string name, int count, int price)
        {
            this.name = name; this.count = count; pricePerOne = price;
        }
        public override string SellAll() { return $"\nВсе продукты {name} можно продать на сумму - {count * pricePerOne} руб."; }
    }
    class Meat : Products
    {
        public Meat(string name, int count, int price)
        {
            this.name = name; this.count = count; pricePerOne = price;
        }
        public override string SellAll() { return $"\nВсе продукты {name} можно продать на сумму - {count * pricePerOne} руб."; }
    }
    class Bread : Products
    {
        public Bread(string name, int count, int price)
        {
            this.name = name; this.count = count; pricePerOne = price;
        }
        public override string SellAll() { return $"\nВсе продукты {name} можно продать на сумму - {count * pricePerOne} руб."; }
    }
    abstract class FactoryProducts
    {
        public abstract Products createProduct(string name, int count, int price);
        public void ShowSell(string name, int count, int price)
        {
            Products product = createProduct(name, count, price);
            Console.WriteLine(product.SellAll());
        }
    }
    class FactoryMilk : FactoryProducts
    {
        public override Milk createProduct(string name, int count, int price)
        {
            return new Milk(name, count, price);
        }
    }
    class FactoryMeat : FactoryProducts
    {
        public override Meat createProduct(string name, int count, int price)
        {
            return new Meat(name, count, price);
        }
    }
    class FactoryBread : FactoryProducts
    {
        public override Bread createProduct(string name, int count, int price)
        {
            return new Bread(name, count, price);
        }
    }
    class Program
    {
        public static void FactoryMethod()
        {
            // !РЕАЛИЗАЦИЯ паттерна Factory method! 
            FactoryProducts f;     // создаём фабрику объектов (продуктов)
            f = new FactoryMilk(); // фабрика будет создавать объекты типа Milk
            Products product1 = f.createProduct("Молоко", 150, 99);
            Console.WriteLine(product1.SellAll());

            f = new FactoryMeat(); // теперь фабрика будет создавать объекты типа Meat
            f.ShowSell("Курица", 20, 145); // сразу создадим объект типа Мясо и выведем инфо о возможной продажи
            Products product2 = f.createProduct("Говядина", 50, 300); // создадим и присвоим еще один объект через фабрику Мяса
            Console.WriteLine(product2.SellAll());

            f = new FactoryBread(); // фабрика будет создавать объекты типа Milk
            Products product3 = f.createProduct("Хлеб насущный", 1050, 49);
            Console.WriteLine(product3.SellAll());
        }
    }
}

namespace AbstractFactory
{
    interface Car
    {
        void drive();
        void sell();
    }
    class RusLada : Car
    {
        private int price; private int speed;
        public RusLada(int price, int speed)
        {
            this.price = price; this.speed = speed;
        }
        public void drive() => Console.WriteLine($"\nМашина едет с максимальной скоростью: {speed}");
        public void sell() => Console.WriteLine($"\nМашину можно продать за {price} руб.");
    }
    class GerMaybach : Car 
    {
        private int price; private int speed;
        public GerMaybach(int price, int speed)
        {
            this.price = 100 * price; this.speed = 2 * speed;
        }
        public void drive() => Console.WriteLine($"\nШедевр машиностроения летит со скоростью: {speed}");
        public void sell() => Console.WriteLine($"\nМожно стать пожизненно богатым, продав это за {price} руб.");
    }

    interface Phone
    {
        void use();
        void sell();
    }
    class RusUnk : Phone
    {
        private int price; private int batery;
        public RusUnk(int price, int batery)
        {
            this.price = price; this.batery = batery - 1;
        }
        public void use() => Console.WriteLine($"\nМаксимальное время работы батареи: {batery} часов");
        public void sell() => Console.WriteLine($"\nТелефон можно продать за {price} руб.");
    }
    class GerIPhone : Phone
    {
        private int price; private int batery;
        public GerIPhone(int price, int batery)
        {
            this.price = 1000 * price; this.batery = batery + 20;
        }
        public void use() => Console.WriteLine($"\nШедевр всех телефонов держится вечность: {batery} часов");
        public void sell() => Console.WriteLine($"\nШейхи оторвут с руками и ногами за {price} руб.");
    }


    interface AFabric
    {
        Phone createPhone(int price, int batery);
        Car createCar(int price, int speed);
    }
    class RusFactory : AFabric
    {
        public Phone createPhone(int price, int batery)
        {
            return new RusUnk(price, batery);
        }
        public Car createCar(int price, int speed)
        {
            return new RusLada(price, speed);
        }
    }
    class GerFactory : AFabric
    {
        public Phone createPhone(int price, int batery)
        {
            return new GerIPhone(price, batery);
        }
        public Car createCar(int price, int speed)
        {
            return new GerMaybach(price, speed);
        }
    }

    class Client
    {
        private Phone p;
        private Car c;
        public Client(AFabric a, int pricePhone, int battery, int priceCar, int speed)
        {
            p = a.createPhone(pricePhone, battery);
            c = a.createCar(priceCar, speed);
        }
        public void CarF() { c.drive(); c.sell(); }
        public void PhoneF() { p.use(); p.sell(); }
        public void All() { CarF(); PhoneF(); }
    }

    class Program
    {
        public static void AbstractFactory()
        {
            Client c1 = new Client(new GerFactory(), 100000, 10, 3500000, 100);
            Client c2 = new Client(new RusFactory(), 5000, 3, 50000, 60);
            c1.All();
            Console.WriteLine("\n---------");
            c2.All();
            Console.WriteLine("\n---------");
            c1.PhoneF();
            Console.WriteLine("\n---------");
            c2.PhoneF();
        }
    }
}

namespace Builder
{
    class Human
    {
        private Color eyes;
        private int iq;
        private string rasa;
        public void chooseEyes(Color eyes) => this.eyes = eyes;
        public void chooseIQ(int iq) => this.iq = iq;
        public void chooseRasa(string rasa) => this.rasa = rasa;
        public override string ToString()
        {
            return $"\nПриветствуйте нового человека у которого глаза цвета {eyes}, интеллект равен {iq} IQ, и принадлежит он '{rasa}'!";
        }
    }
    interface HumanBuilder
    {
        public void eyes();
        public void iq();
        public void rasa();
        public Human human();
    }
    class RusFemale : HumanBuilder
    {
        private Human h;
        public RusFemale()
        {
            h = new Human();
        }
        public void eyes() { h.chooseEyes(Color.Aqua); }
        public void iq() { h.chooseIQ(100); }
        public void rasa() { h.chooseRasa("European"); }
        public Human human() { return h; }
    }
    class AfricMale : HumanBuilder
    {
        private Human h;
        public AfricMale()
        {
            h = new Human();
        }
        public void eyes() { h.chooseEyes(Color.Beige); }
        public void iq() { h.chooseIQ(50); }
        public void rasa() { h.chooseRasa("African"); }
        public Human human() { return h; }
    }
    class God
    {
        private HumanBuilder h;
        public God(HumanBuilder h)
        {
            this.h = h;
        }
        public void born() 
        {
            h.iq(); h.rasa(); h.eyes();
        }
    }
    class Program
    { 
        public static void Builder()
        {
            HumanBuilder builder1 = new AfricMale();
            God g1 = new God(builder1);
            g1.born();
            Human h1 = builder1.human();
            Console.WriteLine(h1);
            Console.WriteLine("\n---------");

            HumanBuilder builder2 = new RusFemale();
            God g2 = new God(builder2);
            g2.born();
            Human h2 = builder2.human();
            Console.WriteLine(h2);
            Console.WriteLine("\n---------");
        }
    }

}



class Program
{
    private static void Main()
    {
        Singleton.Program.Singleton();
        Console.WriteLine("\n--------------------------------------------------------------------------------------------------------");
        FactoryMethod.Program.FactoryMethod();
        Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
        AbstractFactory.Program.AbstractFactory();
        Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
        Builder.Program.Builder();
        Console.ReadKey();
    }
}