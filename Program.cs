using System;
using static System.Console;

namespace lab1
{
    class Program
    {
        static void ProcessDemonstrateBaseConstructorsAndVirtualMethods()
        {
            Defender def = new Defender();
            while(true)
            {
                WriteLine("Enter a pace for def: ");
                string paceValue = ReadLine();
                int pace = 0;
                if(int.TryParse(paceValue, out pace))
                {
                    pace = int.Parse(paceValue);
                    def.Pace = pace;
                }
                if(def.Pace != -1)
                {
                    break;
                }
            }
            def.Defending = 97;
            def.Jumping = 81;
            WriteLine(def.GetInfo());
            def.DoAction("tackle");
            Attack att = new Attack();
            att.Dribbling = 84;
            att.Finishing = 91;
            WriteLine(att.GetInfo());
            att.DoAction("fint");
            Goalkeeper gk = new Goalkeeper();
            gk.Reflexes = 45;
            gk.Diving = 45;
            WriteLine(gk.GetInfo());
            gk.DoAction("reflexes");
            Forward fw = new Forward();
            fw.Longshot = 84;
            fw.Penalty = 76;
            WriteLine(fw.GetInfo());
            fw.DoAction("goal");
            def.PossibleTackle();
            att.PossibleGoal();
            gk.PossibleSave();
            WriteLine($"Difference of balls between two teams is: {Footballer.goaldifference}");
            Defender def1 = new Defender(45, 34);
            WriteLine(def1.GetInfo());
            def1.PossibleSubstitute();
        }
        static void Main(string[] args)
        {
            ProcessDemonstrateBaseConstructorsAndVirtualMethods();
        }
    }
    public abstract class Footballer
    {
        protected string name;
        protected int pace;
        protected int kicking;
        protected int physical;
        protected int timeOnField;
        public static readonly int genTime = 90;
        public static int goaldifference;
        protected Footballer(int timeOnField)
        {
            this.timeOnField = timeOnField;
        }
        static Footballer()
        {
            WriteLine("static constructor was called");
            goaldifference = 0;
        }
        public Footballer(int timeOnField, int physical)
        {
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
            WriteLine("it's going to be interesting");
        }
        public virtual string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking}, physical: {physical}";
        }
        public string Name
        {
            get 
            {
                return name;
            }
            set
            {
                if(char.IsUpper(name[0]))
                {
                    name = value;
                }
                else
                {
                    name = "";
                }
            }
        }
        public int Pace
        {
            get 
            {
                return pace;
            }
            set
            {
                if(value < 0 || value > 100)
                {
                    pace = -1;
                }
                else
                {
                    pace = value;
                }
            }
        }
        public int Kicking
        {
            get 
            {
                return kicking;
            }
            set
            {
                if(value < 0 || value > 100)
                {
                    kicking = -1;
                }
                else
                {
                    kicking = value;
                }
            }
        }
        public int Physical
        {
            get 
            {
                return physical;
            }
            set
            {
                if(value < 0 || value > 100)
                {
                    physical = -1;
                }
                else
                {
                    physical = value;
                }
            }
        }
        public void PossibleSubstitute()
        {
            Random random = new Random();
            int num = random.Next(1, 90);
            WriteLine($"it is the {num} minute of the match");
            if(num > this.timeOnField && this.timeOnField < genTime)
            {
                WriteLine("This player is tired, he may be substituted");
            }
            else
            {
                WriteLine("I suppose, he can be on the field all 90 minutes!");
            }
        }
    }
    public class Defender : Footballer
    {
        protected int defending;
        protected int jumping;
        public Defender(int timeOnField, int physical) : base(timeOnField, physical){}
        public Defender() : base(){}
        public override string GetInfo()
        {
            return $"Name: {name}, pace: {pace}, kicking: {kicking},\r\nphysical: {physical}, timeOnField: {timeOnField} defending: {defending}, jumping: {jumping}";
        }
        public override void DoAction(string action)
        {
            WriteLine($"What a wonderful {action} from {nameof(Defender)}");
        }
        public int Defending
        {
            get 
            {
                return defending;
            }
            set
            {
                if(value < 0 || value > 100)
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
                if(value < 0 || value > 100)
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
            return (jumping + defending)/2;
        }
        public void PossibleTackle()
        {
            if(GetProbabilityOfTackle()> 50)
            {
                WriteLine("DF: Tackle was successful");
            }
            else
            {
                WriteLine("DF: It is going to be a goal from opponent side");
                goaldifference -=1;
            }
        }
    }
    public class Goalkeeper : Defender
    {
        private int diving;
        private int reflexes;
        public Goalkeeper(int timeOnField, int physical) : base(timeOnField, physical){}
        public Goalkeeper() : base(){}
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
                if(value < 0 || value > 100)
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
                if(value < 0 || value > 100)
                {
                    reflexes = -1;
                }
                else
                {
                    reflexes = value;
                }
            }
        }
        private double GetProbabilityOfSave()
        {
            return (diving + reflexes)/2;
        }
        public void PossibleSave()
        {
            if(GetProbabilityOfSave() > 50)
            {
                WriteLine("GK: Save was successful");
            }
            else
            {
                WriteLine("GK: WHAT A MISS!!! It's a goal");
                goaldifference -=1;
            }
        }
    }
    public class Attack : Footballer
    {
        protected int dribbling;
        protected int finishing;
        public Attack(int timeOnField, int physical) : base(timeOnField, physical){}
        public Attack() : base(){}
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
                if(value < 0 || value > 100)
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
                if(value < 0 || value > 100)
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
            return (dribbling + finishing)/2;
        }
        public void PossibleGoal()
        {
            if(GetProbabilityOfGoal() > 75)
            {
                WriteLine("AT:What a wonderful goal");
                goaldifference +=1;
            }
            else if(GetProbabilityOfGoal() > 50)
            {
                WriteLine("What a shot");
            }
            else
            {
                WriteLine("OMG, how he missed from that position...");
            }
        }
    }
    public class Forward : Attack
    {
        private int penalty;
        private int longshot;
        public Forward(int timeOnField, int physical) : base(timeOnField, physical){}
        public Forward() : base(){}
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
                if(value < 0 || value > 100)
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
                if(value < 0 || value > 100)
                {
                    longshot = -1;
                }
                else
                {
                    longshot = value;
                }
            }
        }
        public void ScoreGoal()
        {
            if(penalty > 80 && longshot > 80 && GetProbabilityOfGoal() > 80)
            {
                WriteLine("FW: What a shot, it may be the greatest goal ever");
                goaldifference +=1;
            }
        }
    }
}