using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public Tile tilePrefab;

    Tile[][] tiles;
    public int WIDTH = 30;
    public int HEIGHT = 10;
    public int PAGE_WIDTH = 12;

    // Use this for initialization
	void Start () {
        tiles = Enumerable.Range(0, WIDTH).Select(x => Enumerable.Range(0, HEIGHT).Select(y => {
            Tile tile = Instantiate(tilePrefab);
            tile.Initialize(x, y);
            if(x<= PAGE_WIDTH) {
                tile.Illusion(WIDTH, 1);
            }
            if (x >= WIDTH - PAGE_WIDTH) {
                tile.Illusion(WIDTH, -1);
            }
            return tile;
        }).ToArray()).ToArray();
	}

    float SCROLL_SPEED = 10;
	// Update is called once per frame
	void Update () {
        var d = 1-Input.GetAxis("Mouse ScrollWheel");
        if (d != 1) {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize * d, 0.25f, 3.2f);
        }
        HandleMouseDrag();
    }
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private Vector3 start;
    void HandleMouseDrag() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            start = Camera.main.transform.position;
            return;
        }
        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed);

        Camera.main.transform.position = start-move*Camera.main.orthographicSize;

        var new_pos = Camera.main.transform.position;
        var new_x = new_pos.x % (WIDTH * Tile.X_SIZE);
        if (new_x < 0) {
            new_x += (WIDTH * Tile.X_SIZE);
        }
        Camera.main.transform.position = new Vector3(new_x, Mathf.Clamp(new_pos.y,0,HEIGHT*Tile.Y_SIZE),-10);


    }
}
