using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {
    public enum Terrain { Sand, Mountain, Grass, Hills, Water}
    Terrain terrain;
    SpriteRenderer sr;
    public static float X_SIZE = 0.64f;
    public static float Y_SIZE = 0.20f;
    public static float ODD_ROW_OFFSET = 0.32f;
    // Use this for initialization
    void Start () {
	}
    int x = 0;
    int y = 0;
    public void Initialize(int x, int y) {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x * X_SIZE + (y % 2) * ODD_ROW_OFFSET, y * Y_SIZE);
        sr = GetComponent<SpriteRenderer>();
        terrain = (Terrain)Random.Range(0, 5);
        sr.color = GetColor();
    }
    Color GetColor() {
        Color color = Color.white;
        switch (terrain) {
            case Terrain.Grass:
                color = Color.green;
                break;
            case Terrain.Hills:
                color = new Color(173f / 255, 1, 47f / 255);
                break;
            case Terrain.Mountain:
                color = Color.gray;
                break;
            case Terrain.Sand:
                color = Color.yellow;
                break;
            case Terrain.Water:
                color = Color.blue;
                break;
        }
        return color;
    }
    public void Illusion(int max_width, int direction) {
        var shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;
        var child_sr = shadow.AddComponent<SpriteRenderer>();
        child_sr.color = GetColor();
        child_sr.sprite = sr.sprite;
        shadow.transform.localPosition = new Vector3(max_width * X_SIZE * direction,0,0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
