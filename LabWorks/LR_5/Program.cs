namespace Proxy
{
    interface School
    {
        void CancelClasses();
        void defaultPhrase();
        public string[] Names { get; }
    }
    public class Teachers : School
    {
        public Teachers(string name, int age)
        {
            this.name = name; this.age = age;
        }
        private string name;
        private string[] names = new string[5] { "Alexei", "Danil", "Maria", "Klava", "Santa"};
        private int age;
        public string[] Names { get => names; }
        public void CancelClasses()
        {
            Console.WriteLine("Урок окончен! Все свободны!"); 
        }
        public void defaultPhrase()
        {
            Console.WriteLine("Звонок для учителя, всем сидеть!");
        }
    }
    public class Pupils : School
    {
        private string name; private int age;
        private string[] names = new string[6] { "Cattrine", "Boris", "Chris", "Melody", "Klancy", "Man" };
        public string[] Names { get => names; }
        public Pupils(string name, int age)
        {
            this.name = name; this.age = age;
        }
        public void CancelClasses()
        {
            Console.WriteLine("Отказано! Недостаточно прав!");
        }
        public void defaultPhrase()
        {
            Console.WriteLine("Да зачем мне эта ваша школа?!");
        }
    }
    public class ProxySchool : School
    {
        private School p; public string[] Names { get; }
        private int age; private string name;
        private bool NameInNames()
        {
            foreach (var v in p.Names)
            {
                if (v == name) return true;
            }
            return false;
        }
        public ProxySchool(string name, int age)
        {
            if (age > 20)
            {
                p = new Teachers(name, age);
            }
            else
            {
                p = new Pupils(name, age);
            }
            this.age = age; this.name = name;
        }
        public void CancelClasses()
        {
            if (NameInNames() || age > 20) { p.CancelClasses(); }
            else { Console.WriteLine("Отказано! Недостаточно прав!"); }
        }
        public void defaultPhrase()
        {
            p.defaultPhrase();
        }
    }
    class ProxyTest
    {
        public static void Proxy()
        {
            School p1 = new ProxySchool("Jordan", 10);
            p1.defaultPhrase();
            p1.CancelClasses();
            Console.WriteLine();
            School p2 = new ProxySchool("Boris", 30);
            p2.defaultPhrase();
            p2.CancelClasses();
            Console.WriteLine();
            School p3 = new ProxySchool("Alexei", 21);
            p3.defaultPhrase();
            p3.CancelClasses();
        }
    }
}

namespace Adapter
{
    public class English
    {
        string[] words; string[] t = new string[3] { "Кошка", "Собака", "Красный" };
        public void translate(string[] s)
        {
            for (int i = 0; i < s.Length; i++) 
            {
                Console.WriteLine($"{s[i]} переводится как {t[i]}");
            }
            Console.WriteLine();
        }
    }
    interface Spanish
    {
        void traducir(string[] palabras); // перевести, слова на испанском соответственно
    }
    public class LangAdapter : Spanish
    {
        private English en;
        public LangAdapter(English en)
        {
            this.en = en;
        }
        public void traducir(string[] palabras)
        {
            en.translate(palabras);
        }
    }
    class AdapterTest
    {
        public static void Adapter()
        {
            English e1 = new English();
            LangAdapter a1 = new LangAdapter(e1);
            a1.traducir(new string[3] { "cat", "dog", "red" });
        }
    }
}

namespace Bridge
{
    public interface Telephones
    {
        void phone(string ph);
    }
    class Android : Telephones
    {
        public void phone(string ph)
        {
            Console.WriteLine($"Пытаемся дозво... [Server Error] ниться до... [404] абонента EEERROR... Ошибка была отправлена Вам: \n{ph}");
            Console.WriteLine();
        }
    }
    class IPhone : Telephones
    {
        public void phone(string ph)
        {
            Console.WriteLine($"Мы уже дозваниваемся до абонента; для повышения качества обслуживания с вас 10000$, счёт уже отправлен вам: \n{ph}");
            Console.WriteLine();
        }
    }
    public abstract class Message
    {
        protected Telephones phone;
        public Message(Telephones p)
        {
            phone = p;
        }
        public abstract void send(string message);
    }
    public class EmailMessage : Message
    {
        public EmailMessage(Telephones p) : base(p) { }
        public override void send(string email)
        {
            phone.phone("Send to your email: " + email);
        }
    }
    public class WhatsAppMessage : Message
    {
        public WhatsAppMessage(Telephones p) : base(p) { }
        public override void send(string acc)
        {
            phone.phone("Send to your WhatsApp account: " + acc);
        }
    }
    class BridgeTest
    {
        public static void Bridge()
        {
            Telephones IPhone_17_ProMaxUltraSuperDuperPuper = new IPhone();
            Telephones Xiaomi_0 = new Android();

            EmailMessage mess1 = new EmailMessage(Xiaomi_0);
            EmailMessage mess2 = new EmailMessage(IPhone_17_ProMaxUltraSuperDuperPuper);

            mess1.send("lalatopola@misis.ru");
            mess2.send("pupintop1@redsquare.com");

            WhatsAppMessage mess3 = new WhatsAppMessage(Xiaomi_0);
            WhatsAppMessage mess4 = new WhatsAppMessage(IPhone_17_ProMaxUltraSuperDuperPuper);

            mess3.send("89871233421");
            mess4.send("+79457777777");
        }
    }
}

namespace Program
{
    class Program
    {
        private static void Main()
        {
            Proxy.ProxyTest.Proxy();
            Console.WriteLine("\n---------------------------------------------------------------------------------\n");
            Adapter.AdapterTest.Adapter();
            Console.WriteLine("\n---------------------------------------------------------------------------------\n");
            Bridge.BridgeTest.Bridge();
            Console.WriteLine("\n---------------------------------------------------------------------------------\n");
            Console.ReadKey();
        }
    }
}