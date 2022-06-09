using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase
{
    internal class Vehicle
    {
        public Vehicle(int Id, string Type, string Color, string Plat) => (this.Id, this.Type, this.Color, this.Plat) = (Id, Type, Color, Plat);
        public int Id { get; set; }
        public virtual string Type { get; set; }
        public string Color { get; set; }
        public string Plat { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public bool IsOdd()
        {
            var temp = this.Plat.Split('-');
            return int.Parse(temp[1]) % 2 == 1;
        }

    }

    internal class Mobil : Vehicle
    {
        public Mobil(int Id, string Color, string Plat) : base(Id, "Mobil", Color, Plat) => (base.Id, base.Type, base.Color, base.Plat) = (Id, Type, Color, Plat);
    }
    internal class Motor : Vehicle
    {
        public Motor(int Id, string Color, string Plat) : base(Id, "Motor", Color, Plat) => (base.Id, base.Type, base.Color, base.Plat) = (Id, Type, Color, Plat);
    }
}
