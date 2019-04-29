using System;
using System.Collections.Generic;
using System.Collections;

namespace Entity
{
    public class EnclosureList : List<Enclosure>
    {
        private List<Enclosure> enclosurelist;


        public List<Enclosure> EncloseList
        {
            get { return enclosurelist; }
            set { enclosurelist = value; }

        }
        public List<string> AnimalList()
        {
            List<string> temp = new List<string>();

            foreach(var close in EncloseList)
            {
                temp.Add(close.Animal_Type);
            }
            return temp;
        }
        
    }
}
