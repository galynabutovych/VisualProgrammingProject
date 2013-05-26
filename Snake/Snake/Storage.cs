using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    public interface Storage
    {
        //String StorageLocation
        //{
        //    get;
        //    set;
        //}
        void storeGame(GameSettings lGame, String lName);
        GameSettings loadGame(String lName);
    }

    

}
