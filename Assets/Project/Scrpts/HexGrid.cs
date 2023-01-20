using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Project.Scrpts
{
    public class HexGrid : MonoBehaviour
    {
        public int width = 6;
        public int height = 6;

        // attach cell prefab to component - the prefab we are using as a cell (one triangle)
        public HexCell cellPrefab;
        HexCell[] cells;

        // attach the Hex cell label prefab to the component ( text )
        public TextMeshPro cellLabelPrefab;
        // the canvas vhere the text cells are drawn upon (one cell contains 6 triangles)
        Canvas gridCanvas;
        // the hex mesh where the map is drawn
        HexMesh hexMesh;
       

        private void Awake()
        {
            gridCanvas = GetComponentInChildren<Canvas>();
            hexMesh= GetComponentInChildren<HexMesh>();

            // cache the cells on the awake lifecycle
            cells = new HexCell[height * width];
            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        private void Start()
        {
            // triangulate the cells - one cell has 6 triangles aka hexagon
            // TODO - this should be handled as an event maybe ?
            hexMesh.Triangulate(cells);
        }

        /**
         * As the default planes are 10 by 10 units, offset each cell by that amount.
         */
        void CreateCell (int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);
            
            // clone the object and transform it with the new position
            HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            // set the coordinates (handle the offset)
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
            // create the label text ( helper )
            CreateCoordinatesText(cell, cellLabelPrefab, gridCanvas, position);
        }


        // helper method that creates coordinate text on the cell
        void CreateCoordinatesText(HexCell cell, TextMeshPro cellLabelPrefab, Canvas gridCanvas, Vector3 position)
        {
            TextMeshPro label = Instantiate<TextMeshPro>(cellLabelPrefab);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(position.x, position.z);
            label.text = cell.coordinates.ToStringOnSeparateLines();        
        }




    }
}