using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Module_10_Deep_OOP.Lessons
{
    abstract class Hero
    {
        byte level;
        uint hitPoint;
        uint maxHitPoint;

        static uint defIndexName;
        static Random randomize = new Random();
        static List<string> dbNames;

        public abstract void Motto();

        public string Name { get; set; }
        public byte Level { get { return this.level; } }
        public uint HitPoint { get { return this.hitPoint; } }
        public Hero() : this("", 1, 0)
        {

        }
        static Hero()
        {
            defIndexName = 1;
            dbNames = new List<string>();
        }
        public Hero(string Name, byte Level, uint HitPoint)
        {
            if (Name == String.Empty || Hero.dbNames.Contains(Name))
            {
                Name = $"{Guid.NewGuid().ToString().Substring(0, 5)} #{Hero.defIndexName++}";
            }
            this.Name = Name;
            Hero.dbNames.Add(Name);
            this.level = Level;

            HitPoint = HitPoint != 0 ? HitPoint : (uint)Hero.randomize.Next(100, 400);
            this.hitPoint = HitPoint;
            this.maxHitPoint = HitPoint;
        }
        public void Treatment(uint Hp = 10)
        {
            if (this.hitPoint == 0)
            {
                Console.WriteLine($"Лечение не возможно {this.Name}  в таверне");
            }
            else
            {
                this.hitPoint = this.hitPoint + Hp <= this.maxHitPoint ? this.hitPoint + Hp : this.maxHitPoint;
            }
        }
        public uint Attack()
        {
            return 10;
        }
        private void Die()
        {
            Console.WriteLine($"У {this.Name} Критический запас здоровья");
            this.Tavern();
        }
        private void Tavern()
        {
            Console.WriteLine($"Герой {this.Name}в таверне");
        }
        public void Attacked(uint Damage)
        {
            if (this.hitPoint == 0)
            {
                this.Tavern();
            }
            else
            {
                if (this.hitPoint - Damage <= 0)
                {
                    this.hitPoint = 0;
                    this.Die();
                }
                else
                {
                    this.hitPoint -= Damage;
                }
            }
        }
        public string HeroInformation()
        {
            return String.Format("Name:{0, 10} |  level: {1, 4} | HitPoint{2, 6} | Tape: {3, 12}",
                this.Name,
                this.Level,
                this.HitPoint,
                this.GetType().Name
                );

        }
    }
    class Druid : Hero
    {
        public override void Motto()
        {
            Console.WriteLine($"{this.Name} Druid forward");
        }
        public Druid(string Name, byte Level, uint HitPoint) : base(Name, Level, HitPoint)
        {

        }
        public Druid() : this("", 1, 0)
        {

        }
        public new void Treatment(uint Hp = 10)
        {
            Hp = (uint)(Hp * 1.5);
            base.Treatment(Hp);
        }
        public void Attack(Hero Target)
        {
            if (Target != this)
            {
                Target.Attacked(10);
            }
        }
        public void DruidHeal()
        {

        }
    }
    class Warrior : Hero, IRampage
    {
        public override void Motto()
        {
            Console.WriteLine($"{this.Name} Warrior forward");
        }
        public Warrior(string Name, byte Level, uint HitPoint) : base(Name, Level, HitPoint)
        {

        }
        public Warrior() : this("", 1, 0)
        {

        }
        public int Charge { get; set; }
        public void Rampage()
        {
            this.Charge = 5;
        }
        public void UltraAttack(Hero Target)
        {
            for (int i = 0; i < this.Charge; i++)
            {
                Target.Attacked(10);
            }
            this.Charge = 0;
        }
        public new void Attacked(uint Damage)
        {
            base.Attacked(Damage / 2);
        }
    }
    class Hunter : Hero, IRampage
    {
        public override void Motto()
        {
            Console.WriteLine($"{this.Name} Hunter forward");
        }
        public Hunter(string Name, byte Level, uint HitPoint) : base(Name, Level, HitPoint)
        {

        }
        public Hunter() : this("", 1, 0)
        {

        }
        public int Charge { get; set; }
        public void Rampage()
        {
            this.Charge = 5;
        }
        public void UltraAttack(Hero Target)
        {
            for (int i = 0; i < this.Charge; i++)
            {
                Target.Attacked(10);
            }
            this.Charge = 0;
        }
        public new void Attacked(uint Damage)
        {
            base.Attacked(Damage / 2);
        }
    }
    interface IRampage
    {
        int Charge { get; set; }
        void Rampage();
        void UltraAttack(Hero Target);
    }
}