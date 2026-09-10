using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    public Tilemap placementTilemap;
    public Tilemap NonPlacementTilemap;
    public GameObject ghostPrefab;
    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>();
    private GameObject ghostInstance;

    void Update()
    {
        HandlePlacementHover();
        HandlePlacementClick();
    }

    void HandlePlacementHover()
    {
        // Als er geen tower is geselecteerd, verwijder de ghost
        if (TowerSelectionUI.SelectedTowerPrefab == null)
        {
            if (ghostInstance != null)
            {
                Destroy(ghostInstance);
                ghostInstance = null; // Zorg dat de referentie ook echt leeg is
            }
            return;
        }

        // Maak een ghost aan als die er nog niet is
        if (ghostInstance == null)
        {
            ghostInstance = Instantiate(ghostPrefab);
            ghostInstance.GetComponent<SpriteRenderer>().sprite = TowerSelectionUI.SelectedTowerPrefab.GetComponent<SpriteRenderer>().sprite;
        }

        // Update de positie van de ghost ELK frame (buiten de null check!)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPos = placementTilemap.WorldToCell(mouseWorldPos);

        Vector3 worldCenter = placementTilemap.GetCellCenterWorld(cellPos);
        worldCenter.z = 0;

        ghostInstance.transform.position = worldCenter + new Vector3(0, placementTilemap.cellSize.y * 0.25f);

        // Controleer of de tegel geldig is om op te bouwen
        bool valid = placementTilemap.HasTile(cellPos) 
                     && !occupiedTiles.Contains(cellPos) 
                     && (NonPlacementTilemap == null || !NonPlacementTilemap.HasTile(cellPos));

        ghostInstance.GetComponent<GhostTower>().SetValid(valid);
    }

    void HandlePlacementClick()
    {
        // Alleen iets doen als er DAADWERKELIJK geklikt wordt (met uitroepteken !)
        if (!Input.GetMouseButtonDown(0)) return;
        
        // Geen tower geselecteerd? Doe niks.
        if (TowerSelectionUI.SelectedTowerPrefab == null) return;

        // Negeer kliks op UI elementen (knopen)
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPos = placementTilemap.WorldToCell(mouseWorldPos);

        // Controleer of er een tegel is en of deze niet al bezet is
        if (!placementTilemap.HasTile(cellPos)) return;
        if (occupiedTiles.Contains(cellPos)) return;
        if (NonPlacementTilemap != null && NonPlacementTilemap.HasTile(cellPos)) return;

        // Plaats de toren
        Instantiate(TowerSelectionUI.SelectedTowerPrefab, ghostInstance.transform.position, Quaternion.identity);

        // Reset de selectie en ruim de ghost op
        TowerSelectionUI.SelectedTowerPrefab = null;
        if (ghostInstance != null)
        {
            Destroy(ghostInstance);
            ghostInstance = null;
        }

        occupiedTiles.Add(cellPos);
    }
}