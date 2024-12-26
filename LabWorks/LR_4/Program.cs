namespace Strategy 
{ 
    interface QuadroEquation
    {
        public void Solve();
    }

    class Discriminant : QuadroEquation
    {
        int a, b, c;
        public Discriminant(int a, int b, int c)
        {
            this.a = a; this.b = b; this.c = c;
        }
        public void Solve()
        {
            double D = b * b - 4 * a * c;
            if (D > 0)
            {
                double x1 = ((-b + Math.Sqrt(D)) / (2 * a));
                double x2 = ((-b - Math.Sqrt(D)) / (2 * a));
                Console.WriteLine($"\nРешением уравнения через дискриминант являются два корня: х1 = {x1, 4:f2} и x2 = {x2, 4:f2}.");
            }
            else if (D == 0) 
            {
                double x = (-b / 2 * a);
                Console.WriteLine($"\nРешением уравнения через дискриминант является один корень: х = {x, 4:f2}.");
            }
            else { Console.WriteLine("\nДанное уравнение через дискриминант не имеет решений"); }
        }
    }

    class Vieta : QuadroEquation
    {
        int a, b, c;
        public Vieta(int a, int b, int c)
        {
            this.a = a; this.b = b; this.c = c;
        }
        public void Solve()
        {
            bool brek = false;
            for (int x1 = -10; x1 <= 10; x1++) 
            {
                if (!brek)
                {
                    for (int x2 = -10; x2 <= 10; x2++)
                    {
                        if (x1 * x2 == c && x1 + x2 == -b && x1 != x2)
                        {
                            Console.WriteLine($"\nРешением уравнения по теореме Виета являются два корня: х1 = {x1, 4:f2} и x2 = {x2, 4:f2}.");
                            brek = true;
                            break;
                        }
                        else if (x1 * x2 == c && x1 + x2 == -b && x1 == x2)
                        {
                            brek = true;
                            Console.WriteLine($"\nРешением уравнения по теореме Виета является один корень: х = {x1, 4:f2}.");
                            break;
                        }
                    }
                }
                else break;
            }
            if (!brek) Console.WriteLine($"\nДанное уравнение по теореме Виета не имеет решений");
        }
    }

    class Enumerate : QuadroEquation
    {
        int a, b, c;
        public Enumerate(int a, int b, int c)
        {
            this.a = a; this.b = b; this.c = c;
        }
        public void Solve()
        {
            bool brek = false;
            for (int x1 = -100; x1 <= 100; x1++)
            {
                if (!brek && (a * x1 * x1 + b * x1 + c == 0))
                {
                    for (int x2 = -100; x2 <= 100; x2++)
                    {
                        if (a * x2 * x2 + b * x2 + c == 0 && x1 != x2)
                        {
                            brek = true;
                            Console.WriteLine($"\nРешением уравнения перебором корней являются два корня: х1 = {x1, 4:f2} и x2 = {x2, 4:f2}.");
                            break;
                        }
                        else if (a * x2 * x2 + b * x2 + c == 0 && x1 == x2)
                        {
                            brek = true;
                            Console.WriteLine($"\nРешением уравнения перебором корней является один корень: х = {x1, 4:f2}.");
                            break;
                        }
                    }
                }
                else if (brek) { break; }
            }
            if (!brek) Console.WriteLine($"\nДанное уравнение перебором корней не имеет решений.");
        }
    }

    class Teacher
    {
        private QuadroEquation way;
        public Teacher(QuadroEquation q)
        {
            way = q;
        }
        public void Solution() 
        {
            way.Solve();
        }
    }

    class Program
    {
        public static void Strategy()
        {
            Teacher t1 = new Teacher(new Vieta(1, 2, 1));
            t1.Solution();

            Teacher t2 = new Teacher(new Discriminant(1, -5, 6));
            t2.Solution();

            Teacher t3 = new Teacher(new Enumerate(5, 2, 1));
            t3.Solution();
        }
    }
}

namespace ResponsibilityChain
{
    interface Access
    {
        void Log_in(Users user);
        void LowerAccess(Access access);
    }
    class Owner : Access
    {
        private Access a;
        private string Ownerpassword = "123Hello";
        private int Ownerlogin = 1;
        private string name = "Иван";
        public void Log_in(Users user)
        {
            if (user.password == Ownerpassword && user.login == Ownerlogin)
            {
                Console.WriteLine($"\nДобро Пожаловать Владелец {name}! (login: {Ownerlogin})");
            }
            else if (a != null)
            {
                a.Log_in(user);
            }
        }
        public void LowerAccess(Access access)
        {
            a = access;
        }
    }

    class Admins : Access
    {
        private Access a;
        Dictionary<int, string[]> admins = new Dictionary<int, string[]>() { 
                                                                           { 2, new string[2]{"kishmish", "Алексей Петрович" } }, 
                                                                           { 3, new string[2] { "000boom", "Пётр Алексеевич" } }, 
                                                                           { 4, new string[2] { "want5forsubject", "Добрыня Никитич" } } };
        public void Log_in(Users user)
        {
            bool brek = false;
            foreach (var k in admins)
            {
                if (k.Key == user.login && k.Value[0] == user.password)
                {
                    Console.WriteLine($"\nДобро пожаловать Админ {k.Value[1]}! (login: {k.Key})");
                    brek = true; break;
                }
            }
            if (a != null && brek == false)
            {
                a.Log_in(user);
            }
        }
        public void LowerAccess(Access access)
        {
            a = access;
        }
    }

    class Workers : Access
    {
        private Access a;
        Dictionary<int, string[]> workers = new Dictionary<int, string[]>() {
                                                                           { 5, new string[2]{"omygoood123", "Николай II" } },
                                                                           { 6, new string[2] { "000boom", "Роман" } },
                                                                           { 7, new string[2] { "plsdontdoit", "Евгения" } } };
        public void Log_in(Users user)
        {
            bool brek = false;
            foreach (var k in workers)
            {
                if (k.Key == user.login && k.Value[0] == user.password)
                {
                    Console.WriteLine($"\nДобро пожаловать Работник {k.Value[1]}! (login: {k.Key})");
                    brek = true; break; 
                }
            }
            if (!brek) Console.WriteLine("\nПользователь с такими данными не найден!");
        }
        public void LowerAccess(Access access)
        {
            a = access;
        }
    }

    class Users
    {
        public int login;
        public string password;
        public Users(int login, string password)
        {
            this.login = login; this.password = password;
        }
    }

    class Program
    {
        public static void ResponsibilityChain()
        {
            Access User1 = new Owner();
            Access User2 = new Admins();
            Access User3 = new Workers();

            User1.LowerAccess(User2);
            User2.LowerAccess(User3);

            Users n1 = new Users(1, "boo123");
            Users n2 = new Users(1, "123Hello");
            Users n3 = new Users(3, "000boom");
            Users n4 = new Users(6, "000boom");
            Users n5 = new Users(4, "want5forsubject");
            Users n6 = new Users(5, "omygoood123");
            Users n7 = new Users(9, "omygoood123");

            User1.Log_in(n1);
            User1.Log_in(n2);
            User1.Log_in(n3);
            User1.Log_in(n4);
            User1.Log_in(n5);
            User1.Log_in(n6);
            User1.Log_in(n7);
        }
    }
}

namespace Iterator 
{
    interface Iterator<T>
    {
        bool hasNext();
        T next();
    }
    class ArrayIterator<T> : Iterator<T> 
    {
        private T[] items;
        private int position;
        public ArrayIterator(T[] items)
        {
            this.items = items;
            position = 0;
        }
        public bool hasNext()
        {
            return position < items.Count();
        }
        public T next()
        {
            if (hasNext())
            {
                return items[position++];
            }
            throw new IndexOutOfRangeException();
        }
    }
    class Program
    {
        public static void Iterator()
        {
            int[] a = new int[5] { 1, 10, 98, 6, 72 };
            Iterator<int> iterator = new ArrayIterator<int>(a);
            for (int i = 0; i < 5; i++) // если ставить больше длины списка, то после 5 элемента выдаст ошибку, указанную в коде, и завершит работу
            {
                Console.WriteLine($"Элемент массива: {iterator.next()}");
            }
        }
    }
}

namespace Main
{
    class Program
    {
        private static void Main()
        {
            Console.WriteLine("------------------------------------- Реализация паттерна Strategy ---------------------------------------\n");
            Strategy.Program.Strategy();
            Console.WriteLine("\n\n------------------------------------- Реализация паттерна Responsibility Chain ---------------------------------------\n");
            ResponsibilityChain.Program.ResponsibilityChain();
            Console.WriteLine("\n\n------------------------------------- Реализация паттерна Iterator ---------------------------------------\n");
            Iterator.Program.Iterator();
            Console.WriteLine("\n\n------------------------------------- The End! ---------------------------------------\n");
            Console.ReadKey();
        }
    }
}