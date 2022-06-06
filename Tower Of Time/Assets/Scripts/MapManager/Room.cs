using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using CurrentStatus;
using Photon.Pun;

using static System.Math;
using System;

public class Room
{
    protected Map map;
    protected (int, int) pos;
    protected RoomType type;
    protected int level;
    protected List<(string, float, float, Quaternion)> entities; // Saves entities as (name, pos.x, pos.y, rotation) as required by PhotonNetwork.Instantiate :unamused:

    public Map Map => map;
    public (int, int) Pos => pos;
    public RoomType Type => type;
    public int Level => level;
    public List<(string, float, float, Quaternion)> Entities => entities;
    public int x
    {
        get => pos.Item1;
        protected set => pos = (value, pos.Item2);
    }
    public int y
    {
        get => pos.Item2;
        protected set => pos = (pos.Item1, value);
    }
    public string sceneName => "Salle " + 
                                type switch { RoomType.BossRoom => "Boss ", _ => "Int "} + 
                                level switch { 1 => "Medusa", 2 => "Minotaure", 3 => "Charybde et Scylla", _ => "Cronos"};

    public Room(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate)
    {
        this.map = map;
        this.map[x, y] = this;
        pos = (x, y);
        this.level = level;
        this.type = type;
        entities = new List<(string, float, float, Quaternion)>();
        Debug.Log(level);
    }

    public void SetType(RoomType type) => this.type = type;

    protected int minDepth => level * level * level - 7 * level * level + 16 * level - 6;
    protected int maxDepth => - level * level + 7 * level + 2;
    public virtual void Generate(int depth = 0){}

    public bool isFull => false;

    public override string ToString()
    {
        return type switch {RoomType.StartRoom => "S", RoomType.Intermediate => "I", RoomType.BossRoom => "B", _ => "?"};
    }

    public void Goto(MonoBehaviour mono)
    {
        Current.LivingEnemies = new List<GameObject>();
        SceneManager.LoadScene(sceneName);
        mono.StartCoroutine(WaitForClear());
    }

    IEnumerator WaitForClear()
    {
        yield return null;

        Spawn();

        while (!Current.Cleared)
        {
            string Living = "";
            foreach (GameObject enemy in Current.LivingEnemies)
            {
                Living += (enemy?.name).ToString() + " ";
            }
            Debug.Log(Living);

            yield return null;
        }

        Debug.Log("Cleared");

        entities.Where<(string, float, float, Quaternion)>(entity => entity.Item1.Substring(0, Min(entity.Item1.Length, "Méchant".Length)) == "Méchant")
                .ToList<(string, float, float, Quaternion)>()
                .ForEach(enemy => entities.Remove(enemy));
    }

    private int PseudoNormalDistrib(int min, int avg) // genere un nombre aleatoire entre min et 2 * avg - min
    {
        float sum = min;

        for (int i = 0; i < 2 * (avg - min); i++) sum += UnityEngine.Random.value;

        return (int)sum;
    }

    public void Populate()
    {
        if (map[x, y - 1] != null)
            entities.Add(("Portes/Porte Haut", 0, Current.HalfHeight, Quaternion.identity));

        if (map[x, y + 1] != null)
            entities.Add(("Portes/Porte Bas", 0, -Current.HalfHeight, Quaternion.AngleAxis(180, Vector3.forward)));

        if (map[x - 1, y] != null)
            entities.Add(("Portes/Porte Gauche", -Current.HalfWidth, 0, Quaternion.AngleAxis(90, Vector3.forward)));

        if (map[x + 1, y] != null)
            entities.Add(("Portes/Porte Droite", Current.HalfWidth, 0, Quaternion.AngleAxis(270, Vector3.forward)));


        if (type == RoomType.BossRoom)
            System.Array.ForEach<string>(Current.Boss(), x => entities.Add((x, 0, 0, Quaternion.identity)));

        if (type == RoomType.Intermediate)
        {
            int toAdd = PseudoNormalDistrib(3, 5);
            string[] minions = Current.Minions();

            for (int i = 0; i < toAdd; i++)
                entities.Add((minions[UnityEngine.Random.Range(0, minions.Length)],
                              UnityEngine.Random.Range(0, Current.HalfWidth) - Current.HalfWidth / 2,
                              UnityEngine.Random.Range(0, Current.HalfHeight) - Current.HalfHeight / 2,
                              Quaternion.identity));
        }
    }

    public void Spawn()
    {
        Debug.Log("Spawning");
        entities.ForEach(entity => 
        {
            Debug.Log(entity.Item1);
            GameObject curr = PhotonNetwork.InstantiateRoomObject(entity.Item1, new Vector2(entity.Item2, entity.Item3), entity.Item4);
            if (curr.tag == "Ennemi") Current.LivingEnemies.Add(curr);
        });
        
    }
}