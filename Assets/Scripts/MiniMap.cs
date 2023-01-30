using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public float minimapScale = 0.1f;
    public Vector2 minimapPos = new Vector2(10, 10);
    public Vector2 minimapSize = new Vector2(200, 200);
    public Texture textureCircle;

    void OnGUI()
    {
        // Définir l'emplacement et la taille de la minimap
        GUI.BeginGroup(new Rect(minimapPos.x, minimapPos.y, minimapSize.x, minimapSize.y));

        // Dessiner un fond pour la minimap
        GUI.Box(new Rect(0, 0, minimapSize.x, minimapSize.y), "");

        // Dessiner les objets de la scène sur la minimap
        DrawMapObjects();

        // Dessiner la caméra sur la minimap
        DrawCamera();

        GUI.EndGroup();
    }

    void DrawMapObjects()
    {
        // Récupérer tous les objets de la scène
        GameObject[] objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        // Pour chaque objet, dessiner un cube sur la minimap
        foreach (GameObject go in objects)
        {
            // Convertir les coordonnées mondiales en coordonnées de minimap
            Vector3 minimapPos = WorldToMinimapCoord(go.transform.position);

            // Dessiner un cube rouge à l'emplacement de l'objet
            GUI.color = Color.red;
            GUI.Box(new Rect(minimapPos.x, minimapPos.y, 5, 5), "");
        }
    }

    void DrawCamera()
    {
        // Convertir les coordonnées mondiales de la caméra en coordonnées de minimap
        Vector3 minimapPos = WorldToMinimapCoord(Camera.main.transform.position);

        // Dessiner un cercle vert autour de la caméra
        GUI.color = Color.green;
        GUI.DrawTexture(new Rect(minimapPos.x - 5, minimapPos.y - 5, 10, 10), textureCircle);
    }

    Vector3 WorldToMinimapCoord(Vector3 worldPos)
    {
        // Convertir les coordonnées mondiales en coordonnées de minimap
        float x = (worldPos.x / 100) * minimapScale;
        float y = (worldPos.z / 100) * minimapScale;
        return new Vector3(x, y, 0);
    }
}
