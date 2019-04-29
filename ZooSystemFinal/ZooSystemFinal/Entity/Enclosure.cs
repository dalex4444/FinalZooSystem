using System;

namespace Entity
{
    public class Enclosure
    {
        public int EnclosureNo { get; set; }
        public string Animal_Type { get; set; }
        public int Num_Of_Animal { get; set; }
        public bool[,] Enclosure_Schedule { get; set; } = new bool[10, 7];
    } //end class 
}//end namespace

