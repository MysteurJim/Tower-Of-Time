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

        public static string[] Boss()
        {
            switch (level)
            {
                case 1:
                    return new []{"Méchant/Level 1/Medusa"};

                case 2:
                    return new []{"Méchant/Level 2/Minotaure"};

                case 3:
                    return new []{"Méchant/Level 3/Charybde",
                                  "Méchant/Level 3/Scylla"};

                case 4:
                    return new []{"Méchant/Level 4/Cronos"};

                default : 
                    throw new System.Exception("Invalid level index");
            }
        }

        public static string[] Minions() // /!\ A COMPLETER QUAND DES MONSTRES SONT AJOUTES
        {
            switch (level)
            {
                case 1:
                    return new []{"Méchant/Level 1/Anaconda",
                                  "Méchant/Level 1/Boa",
                                  "Méchant/Level 1/Cobra"};

                case 2:
                    return new []{"Méchant/Level 2/Puberté",
                                  "Méchant/Level 2/Superman",
                                  "Méchant/Level 2/Taureau"};

                case 3:
                    return new []{"Méchant/Level 3/Hippo",
                                  "Méchant/Level 3/Kraken",
                                  "Méchant/Level 3/Sirène"};

                case 4:
                    return new []{"Méchant/Level 4/Titan 1",
                                  "Méchant/Level 4/Titan 2",
                                  "Méchant/Level 4/Titan 3"};

                default : 
                    throw new System.Exception("Invalid level index");
            }
        }
    }
}

