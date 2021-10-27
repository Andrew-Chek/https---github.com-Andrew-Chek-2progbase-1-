using System;
using static System.Console;

namespace lab1
{
    interface IFootballer
    {
        void DoAction(string action);
        string GetInfo();
        void PossibleSubstitute();
        int GetCost();
    }
    interface IDefender
    {
        void PossibleTackle();
        void Defend();
    }
    interface IAttack
    {
        void PossibleGoal();
    }
    interface IGoalkeeper
    {
        void Defend();
    }
    interface IForward
    {
        double GetAccuracy();
    }
    class Program
    {
        static void ProcessDemonstrateBaseConstructorsAndVirtualMethods()
        {
            WriteLine("----------------------Checking constructors---------------------------");
            WriteLine("----------------------Checking defender info---------------------------");
            Defender def = new Defender();
            def.Defending = 97;
            def.Jumping = 81;
            WriteLine(def.GetInfo());
            WriteLine($"The cost of this def player is: {def.GetCost()}$");
            WriteLine($"The rating of this def is: {def.GetAverageRating()}");
            def.DoAction("tackle");
            WriteLine("----------------------Checking attack info---------------------------");
            Attack att = new Attack();
            att.Dribbling = 84;
            att.Finishing = 91;
            WriteLine(att.GetInfo());
            WriteLine($"The cost of this att player is: {att.GetCost()}$");
            att.DoAction("fint");
            WriteLine("--------------------Checking goalkeeper info-------------------------");
            Goalkeeper gk = new Goalkeeper();
            gk.Reflexes = 45;
            gk.Diving = 45;
            WriteLine(gk.GetInfo());
            gk.DoAction("reflexes");
            WriteLine("-----------------Checking difference of balls-------------------------");
            def.PossibleTackle();
            att.PossibleGoal();
            gk.possibleSave(def, gk.GetProbabilityOfSave());
            WriteLine($"Difference of balls between two teams is: {Footballer.goaldifference}");
            gk.Diving = 84;
            def.PossibleTackle();
            att.PossibleGoal();
            gk.possibleSave(def, gk.GetProbabilityOfSave());
            WriteLine($"Difference of balls between two teams is: {Footballer.goaldifference}");
            att.PossibleGoal();
            WriteLine($"Difference of balls between two teams is: {Footballer.goaldifference}");
            WriteLine("-----------------------Checking substitute----------------------------");
            Defender def1 = new Defender(45, 34);
            WriteLine(def1.GetInfo());
            def1.PossibleSubstitute();
            WriteLine("-----------------Checking private constructor-------------------------");
            att.Dribbling = 95;
            att.Finishing = 89;
            WriteLine(att.GetInfo());
            Forward fw = Forward.GreatInform(89, 89);
            WriteLine(fw.GetInfo());
            WriteLine("--------------------------Checking over-------------------------------");
        }
        static void ProcessDemonstrateGC()
        {
            Defender def = new Defender();

            WriteLine("---------------------Checking GC Generations--------------------------");

            WriteLine($"Generation of {nameof(def)}: {GC.GetGeneration(def)}");
            WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");

            GC.Collect(0);

            GC.WaitForPendingFinalizers();

            WriteLine();

            WriteLine($"Generation of {nameof(def)}: {GC.GetGeneration(def)}");
            WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");

            GC.Collect(2);

            WriteLine();

            WriteLine($"Generation of {nameof(def)}: {GC.GetGeneration(def)}");
            WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");
            WriteLine("--------------------------Checking over-------------------------------");
            WriteLine("-------------------Checking GC of many elements-----------------------");
            for (int i = 0; i < 20; i++)
            {
                Defender def1 = new Defender();
                def1 = null;
            }
            Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");
            Console.WriteLine("Collecting");
            GC.Collect();

            GC.WaitForPendingFinalizers();
            Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");
            Console.WriteLine("Collecting");
            GC.Collect();

            GC.WaitForPendingFinalizers();
            Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)} bytes");
            WriteLine("--------------------------Checking over-------------------------------");
        }
        static void Main(string[] args)
        {
            //ProcessDemonstrateBaseConstructorsAndVirtualMethods();
            //ProcessDemonstrateGC();
            Goalkeeper gk = new Goalkeeper();
            gk.Reflexes = 45;
            gk.Diving = 45;
            gk.Name = "Mike";
            WriteLine("----------------Checking interface EI-------------------");
            ((IGoalkeeper)gk).Defend();
            ((IDefender)gk).Defend();
            WriteLine("-----------Checking delegates and events----------------");
            Defender def = new Defender();
            def.Name = "Jake";
            def.Defending = 97;
            def.Jumping = 81;
            def.saveOfGK = gk.possibleSave(def, gk.GetProbabilityOfSave());;
            def.TeamWork();
            gk.Reflexes = 98;
            gk.Diving = 96;
            def.saveOfGK = gk.possibleSave(def, gk.GetProbabilityOfSave());
            def.TeamWork();
            WriteLine("----------------Checking mistakes-----------------------");
            gk.Name = "mike";
            gk.Physical = 1231;
            gk.Name = "Mike";
            gk.Physical = 18;
            WriteLine("----------------Checking extension-----------------------");
            def.GetAverageRating();
            WriteLine("----------------Checking over-----------------------");
        }
    }
    public static class MyExtension
    {
        public static int GetAverageRating(this Defender def)
        {
            return (def.Defending + def.Jumping + def.Physical) / 3;
        }
    }
    public static class MyException
    {
        public static bool CheckParam(int value)
        {
            if (value <= 100 && value >= 0)
            {
                return true;
            }
            throw new Exception("Wrong param(should be from 0 to 100): ");
        }
        public static bool CheckName(string name)
        {
            if (char.IsUpper(name[0]))
            {
                return true;
            }
            throw new Exception("Wrong name: first letter isn't upper");
        }
    }
    public static class ArgCollection
    {
        public static string GetCheckPh(int physical)
        {
            try
            {
                if (MyException.CheckParam(physical))
                {
                    return $"Checking of {nameof(physical)} was successful";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + nameof(physical);
            }
        }
        public static string GetCheckName(string name)
        {
            try
            {
                if (MyException.CheckName(name))
                {
                    return "Checking of name was successful";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
    public delegate void DefConnect(Defender defender, bool checkSave);
    public abstract class Footballer : IDisposable, IFootballer
    {
        protected string name;
        protected int pace;
        protected int kicking;
        protected int physical;
        protected int timeOnField;
        private bool disposed = false;
        public static readonly int genTime = 90;
        public static int goaldifference;
        static Footballer()
        {
            WriteLine("static constructor was called");
            goaldifference = 0;
        }
        public Footballer(int timeOnField, int physical)
        {
            WriteLine("Public constructor with parameters is called");
            this.name = "FB Player1";
            this.pace = 87;
            this.kicking = 71;
            this.physical = physical;
            this.timeOnField = timeOnField;
        }
        protected Footballer()
        {
            WriteLine("Entering data from base class");
            this.name = "FB Player";
            this.pace = 65;
            this.kicking = 44;
            this.physical = 100;
            this.timeOnField = 90;
        }
        public virtual void DoAction(string action)
        {
            WriteLine("Some info");
        }
        public virtual string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking}, physical: {physical}";
        }
        public int Physical
        {
            get
            {
                return physical;
            }
            set
            {
                if(ArgCollection.GetCheckPh(value) == "Checking of name was successful")
                {
                    this.physical = value;
                }
                else
                {
                    this.physical = 0;
                }
                WriteLine(ArgCollection.GetCheckPh(value));
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if(ArgCollection.GetCheckName(value) == "Checking of name was successful")
                {
                    this.name = value;
                }
                else
                {
                    this.name = "Undefined";
                }
                WriteLine(ArgCollection.GetCheckName(value));
            }
        }
        public void PossibleSubstitute()
        {
            Random random = new Random();
            int num = random.Next(1, genTime);
            WriteLine($"it is the {num} minute of the match");
            if (num > this.timeOnField && this.timeOnField < genTime)
            {
                WriteLine("This player is tired, he may be substituted");
            }
            else
            {
                WriteLine($"I suppose, he can be on the field at least {timeOnField - num} minutes!");
            }
        }
        public abstract int GetCost();

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void CleanUp(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("Releasing managed resources in base class");
                    this.name = null;
                    this.pace = 0;
                    this.physical = 0;
                    this.kicking = 0;
                    this.timeOnField = 0;
                }
                Console.WriteLine("Releasing unmanaged resources in base class");
                disposed = true;
            }
        }
        ~Footballer()
        {
            CleanUp(false);
            WriteLine("main destructor called");
        }
    }
    public class Defender : Footballer, IFootballer, IDefender
    {
        protected int defending;
        protected int jumping;
        public bool saveOfGK;
        public event DefConnect GiveFiveEvent;
        public Defender(int timeOnField, int physical) : base(timeOnField, physical) { }
        public Defender() : base() { }
        public override int GetCost()
        {
            return (defending + jumping) * 10000;
        }
        public override string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking},\r\nphysical: {physical}, timeOnField: {timeOnField} defending: {defending}, jumping: {jumping}";
        }
        public override void DoAction(string action)
        {
            WriteLine($"What a wonderful {action} from {nameof(Defender)}");
        }
        void IDefender.Defend()
        {
            WriteLine("I'm defending, as a defender");
        }
        public void TeamWork()
        {
            WriteLine("Will defender apploud a goalkeeper for his actions?");
            if (GiveFiveEvent != null)
            {
                GiveFiveEvent((Defender)this, saveOfGK);
            }
        }
        private bool disposed = false;
        protected override void CleanUp(bool disposing)
        {
            if (disposing && !disposed)
            {
                defending = 0;
                jumping = 0;
            }
            base.CleanUp(disposing);
        }
        public int Defending
        {
            get
            {
                return defending;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    defending = -1;
                }
                else
                {
                    defending = value;
                }
            }
        }
        public int Jumping
        {
            get
            {
                return jumping;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    jumping = -1;
                }
                else
                {
                    jumping = value;
                }
            }
        }
        private double GetProbabilityOfTackle()
        {
            return (jumping + defending) / 2;
        }
        public void PossibleTackle()
        {
            if (GetProbabilityOfTackle() > 50)
            {
                WriteLine("DF: Tackle was successful");
            }
            else
            {
                WriteLine("DF: It is going to be a goal from opponent side");
                goaldifference -= 1;
            }
        }
        ~Defender()
        {
            CleanUp(false);
            WriteLine("child destructor called: DF");
        }
    }
    public class Goalkeeper : Defender, IFootballer, IDefender, IGoalkeeper
    {
        private int diving;
        private int reflexes;
        public Goalkeeper(int timeOnField, int physical) : base(timeOnField, physical) { }
        public Goalkeeper() : base() { }
        private bool disposed = false;
        protected override void CleanUp(bool disposing)
        {
            if (disposing && !disposed)
            {
                diving = 0;
                reflexes = 0;
            }
            base.CleanUp(disposing);
        }
        void IGoalkeeper.Defend()
        {
            WriteLine("I'm saving a ball, as a goalkeeper");
        }
        public override void DoAction(string action)
        {
            WriteLine($"What a wonderful save and {action} from {nameof(Goalkeeper)}");
        }
        public override string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking}, physical: {physical},\r\ndefending: {defending}, jumping: {jumping}, diving: {diving}, reflexes: {reflexes}";
        }
        public int Diving
        {
            get
            {
                return diving;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    diving = -1;
                }
                else
                {
                    diving = value;
                }
            }
        }
        public int Reflexes
        {
            get
            {
                return reflexes;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    reflexes = -1;
                }
                else
                {
                    reflexes = value;
                }
            }
        }
        public double GetProbabilityOfSave()
        {
            return (diving + reflexes) / 2;
        }
        public Func<Defender, double, bool> possibleSave = (def, prOfSave) => 
        {
            def.GiveFiveEvent -= new DefConnect(applaud);
            def.GiveFiveEvent += new DefConnect(applaud);
            if (prOfSave > 50)
            {
                WriteLine("GK: Save was successful");
                return true;
            }
            else
            {
                WriteLine("GK: WHAT A MISS!!! It's a goal");
                goaldifference -= 1;
                return false;
            }
        };
        public static Action<Defender, bool> applaud = (def, save) => 
        {
            if (save)
            {
                WriteLine("DF: It was a great save! `Giving five`");
            }
            else
            {
                WriteLine("DF: Today is not goalkeeper's day");
            }
        };
        ~Goalkeeper()
        {
            CleanUp(false);
            WriteLine("child destructor called: GK");
        }
    }
    public class Attack : Footballer, IFootballer, IAttack
    {
        protected int dribbling;
        protected int finishing;
        public Attack(int timeOnField, int physical) : base(timeOnField, physical) { }
        public Attack() : base() { }
        private bool disposed = false;
        public override int GetCost()
        {
            return (dribbling + finishing) * 10000;
        }
        protected override void CleanUp(bool disposing)
        {
            if (disposing && !disposed)
            {
                dribbling = 0;
                finishing = 0;
            }
            base.CleanUp(disposing);
        }
        public override void DoAction(string action)
        {
            WriteLine($"What a wonderful shot and {action} from {nameof(Attack)} player");
        }
        public override string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking}, physical: {physical}, dribbling: {dribbling}, finishing: {finishing}";
        }
        public int Dribbling
        {
            get
            {
                return dribbling;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    dribbling = -1;
                }
                else
                {
                    dribbling = value;
                }
            }
        }
        public int Finishing
        {
            get
            {
                return finishing;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    finishing = -1;
                }
                else
                {
                    finishing = value;
                }
            }
        }
        protected double GetProbabilityOfGoal()
        {
            return (dribbling + finishing) / 2;
        }
        public void PossibleGoal()
        {
            if (GetProbabilityOfGoal() > 75)
            {
                WriteLine("AT:What a wonderful goal");
                goaldifference += 1;
            }
            else if (GetProbabilityOfGoal() > 50)
            {
                WriteLine("What a shot");
            }
            else
            {
                WriteLine("OMG, how he missed from that position...");
            }
        }
        ~Attack()
        {
            CleanUp(false);
            WriteLine("child destructor called: AT");
        }
    }
    public class Forward : Attack, IFootballer, IAttack, IForward
    {
        private int penalty;
        private int longshot;
        private double accuracy;
        private Forward(int longshot, int penalty)
        {
            WriteLine("Private constructor called");
            this.accuracy = 100;
            this.name = "Excelent player";
            this.pace = 100;
            this.physical = 100;
            this.timeOnField = 100;
            this.kicking = 100;
            this.dribbling = 100;
            this.finishing = 100;
            this.penalty = penalty;
            this.longshot = longshot;
        }
        private bool disposed = false;
        protected override void CleanUp(bool disposing)
        {
            if (disposing && !disposed)
            {
                penalty = 0;
                longshot = 0;
            }
            base.CleanUp(disposing);
        }
        public override void DoAction(string action)
        {
            WriteLine($"What a wonderful shot and {action} from {nameof(Forward)}");
        }
        public override string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking}, physical: {physical},\r\ndribbling: {dribbling}, finishing: {finishing}, penalty: {penalty}, longshot: {longshot}";
        }
        public int Penalty
        {
            get
            {
                return penalty;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    penalty = -1;
                }
                else
                {
                    penalty = value;
                }
            }
        }
        public int Longshot
        {
            get
            {
                return longshot;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    longshot = -1;
                }
                else
                {
                    longshot = value;
                }
            }
        }
        public double GetAccuracy()
        {
            this.accuracy = (finishing + kicking + penalty + longshot) / 4;
            return this.accuracy;
        }
        public static Forward GreatInform(int longshot, int penalty)
        {
            if (penalty <= 80 && longshot <= 80)
            {
                throw new Exception("Too low stats for inform player");
            }
            return new Forward(longshot, penalty);
        }
        ~Forward()
        {
            CleanUp(false);
            WriteLine("child destructor called: FW");
        }
    }
}