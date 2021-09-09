using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class CreateGrid : MonoBehaviour
    {
        public Camera MainCamera;

        public GameObject GridPrefab;

        public Vector2 GridPrefabSize;

        private int LastSize = 0;

        // Start is called before the first frame update
        void Start()
        {
            var rect = GridPrefab.GetComponent<SpriteRenderer>().sprite.rect;
            GridPrefabSize = new Vector2(rect.width, rect.height);
            //Debug.Log("Grid prefab size: " + GridPrefabSize);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            var size = MainCamera.orthographicSize * 2;
            var sizeOfGridImageWidth = 34;
            var sizeOfGridImageHeight = 17;
            //var pixelWidth = 2031;
            //var pixelHeight = 1017;
            var pixelWidth = GridPrefabSize.x;
            var pixelHeight = GridPrefabSize.y;
            var realSizeX = Mathf.Ceil(size / sizeOfGridImageHeight);
            var realSizeY = Mathf.Ceil(size / sizeOfGridImageWidth);
            //var blocksX = 19.75f; //pixelWidth / sizeOfGridImageWidth;
            //var blocksY = 9.6f;// pixelHeight / sizeOfGridImageHeight;
            var blocksX = sizeOfGridImageWidth * pixelWidth / 256.0f;
            var blocksY = sizeOfGridImageHeight * pixelHeight / 256.0f;

            var blocksOnScreenX = MainCamera.pixelWidth / 256.0f;
            var blocksOnScreenY = MainCamera.pixelHeight / 256.0f;

            //transform.localScale = new Vector3(256.0f / blocksX, 256.0f / blocksY, 0.0f);
            //Debug.Log("Blocks: " + blocksX + "," + blocksY + ": " + blocksOnScreenX + "," + blocksOnScreenY);
            //Debug.Log("Scaled Pixel Width: " + MainCamera.scaledPixelWidth);
            //Debug.Log("Scaled Pixel Width: " + MainCamera.scaledPixelWidth / blocksX);
            //Debug.Log("Pixel Width to Screen" + MainCamera.WorldToScreenPoint(new Vector3(blocksX, blocksY, 0.0f)));
            if (LastSize < (int) Mathf.Max(realSizeX, realSizeY))
            {
                LastSize = (int) Mathf.Max(realSizeX, realSizeY);
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                for (var x = -realSizeX; x <= realSizeX; x++)
                {
                    for (var y = -realSizeY; y <= realSizeY; y++)
                    {
                        var obj = Instantiate(GridPrefab, transform);
                        obj.transform.localPosition = new Vector3(x * 4.0f, y * 2.0f, 0.0f);
                    }
                }
            }

            //transform.position =
            //    new Vector3(
            //        -(MainCamera.gameObject.transform.position.x - Mathf.Floor(MainCamera.gameObject.transform.position.x)),
            //        -(MainCamera.gameObject.transform.position.y - -Mathf.Floor(MainCamera.gameObject.transform.position.y)),
            //        0.5f);
            //transform.position =
            //    new Vector3(
            //        Mathf.Floor(MainCamera.gameObject.transform.position.x),
            //        Mathf.Floor(MainCamera.gameObject.transform.position.y),
            //        0.5f);
            //transform.localScale
        }
    }
}
