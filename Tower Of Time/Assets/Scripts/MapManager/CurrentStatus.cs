using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CurrentStatus
{
    public static class Current
    {
        public static int level => Map.Level;
        public static Map Map { get; set; }
        public static int x { get; set; }
        public static int y { get; set; }
        public const float HalfWidth = 25;
        public const float HalfHeight = 12; 


        public static List<GameObject> LivingEnemies { get; set; } 
        public static bool Cleared => LivingEnemies.All<GameObject>(gameObject => gameObject == null);

        public static GameObject[] Boss()
        {
            switch (level)
            {
                case 1:
                    return new []{Resources.Load("Medusa") as GameObject};

                case 2:
                    return new []{Resources.Load("Minotaure") as GameObject};

                case 3:
                    return new []{Resources.Load("Charybde") as GameObject,
                                  Resources.Load("Scylla") as GameObject};

                case 4:
                    return new []{Resources.Load("Cronos") as GameObject};

                default : 
                    throw new System.Exception("Invalid level index");
            }
        }

        public static GameObject[] Minions() // /!\ A COMPLETER QUAND DES MONSTRES SONT AJOUTES
        {
            switch (level)
            {
                case 1:
                    return new []{Resources.Load("Serpent1") as GameObject};

                case 2:
                    return new []{Resources.Load("Minotaure-petit") as GameObject};

                case 3:
                    return new GameObject[]{};

                case 4:
                    return new GameObject[]{};

                default : 
                    throw new System.Exception("Invalid level index");
            }
        }
    }
}

