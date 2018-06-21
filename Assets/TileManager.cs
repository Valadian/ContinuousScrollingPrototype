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
    public Camera main;
    public Camera foreground;
    public Camera left;
    public Camera right;

    // Use this for initialization
	void Start () {
        left.transform.localPosition = -transform.right * Tile.X_SIZE * WIDTH;
        right.transform.localPosition = transform.right * Tile.X_SIZE * WIDTH;
        tiles = Enumerable.Range(0, WIDTH).Select(x => Enumerable.Range(0, HEIGHT).Select(y => {
            Tile tile = Instantiate(tilePrefab);
            tile.Initialize(x, y);
            //if(x<= PAGE_WIDTH) {
            //    tile.Illusion(WIDTH, 1);
            //}
            //if (x >= WIDTH - PAGE_WIDTH) {
            //    tile.Illusion(WIDTH, -1);
            //}
            return tile;
        }).ToArray()).ToArray();
	}

    float SCROLL_SPEED = 10;
	// Update is called once per frame
	void Update () {
        var d = 1-Input.GetAxis("Mouse ScrollWheel");
        if (d != 1) {
            var orthographicSize = Mathf.Clamp(main.orthographicSize * d, 0.25f, 3.2f);
            main.orthographicSize = orthographicSize;
            foreground.orthographicSize = orthographicSize;
            left.orthographicSize = orthographicSize;
            right.orthographicSize = orthographicSize;
        }
        HandleMouseDrag();
    }
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private Vector3 start;
    void HandleMouseDrag() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            start = main.transform.position;
            return;
        }
        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed);

        main.transform.position = start-move* main.orthographicSize;

        var new_pos = main.transform.position;
        var new_x = new_pos.x % (WIDTH * Tile.X_SIZE);
        if (new_x < 0) {
            new_x += (WIDTH * Tile.X_SIZE);
        }
        main.transform.position = new Vector3(new_x, Mathf.Clamp(new_pos.y,0,HEIGHT*Tile.Y_SIZE),-10);


    }
}
