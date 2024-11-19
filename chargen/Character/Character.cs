using System.ComponentModel.DataAnnotations;

namespace chargen.Character
{
    public class Character
    {
        public Character() 
        {
            this. Metatype= new Metatype();   
            this.Origin = new CharacterOrigin();      
   
        }
        #region health
       
                public int LebenspunkteMax { get; set; }
        public int LebenpunkteCurrent { get; set; }

        public int ErschöpfungspunkteMax { get; set; }
        public int ErschöpfungspunkteCurrent { get; set; }
        #endregion

        public int Movement { get; set; }
        [Range(0, 100)]
        public double Essence { get; set; }
        public Metatype Metatype { get; set; }

        public double Prominence { get; set; }
        public double Finances { get; set; }

        public CharacterOrigin Origin { get; set; }

        public string Name { get; set; }

    }
}