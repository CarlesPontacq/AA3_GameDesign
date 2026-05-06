using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SpaceInvaders/Formation From Tilemap")]
public class FormationFromTilemap : ScriptableObject
{
    public int width;
    public int height;

    public EnemyTypeData[] grid;
}