using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    public class XmlStorage :  Storage
    {
        String storageDirectory;

        public XmlStorage() 
        {
        }
        public String StorageLocation
        {
            get { return storageDirectory; }
            set { storageDirectory = value; }
        }
        public void storeGame(GameSettings lGame, String lName)
        {

        }
    }


}
