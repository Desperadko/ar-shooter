using Game.Gameplay.Systems;
using Game.Shop;
using System;
using UnityEngine;

[RequireComponent(typeof(Elemental), typeof(MeshRenderer), typeof(TrailRenderer))]
public class ProjectileSkinApplier : MonoBehaviour
{
    [SerializeField] private Elemental elemental;
    [SerializeField] private MeshRenderer projectileMaterial;
    [SerializeField] private TrailRenderer projectileTrailMaterial;
    [SerializeField] private SkinCatalog availableSkins;

    private void Awake()
    {
        elemental = elemental != null ? elemental : GetComponent<Elemental>();
        projectileMaterial = projectileMaterial != null ? projectileMaterial : GetComponent<MeshRenderer>();
        projectileTrailMaterial = projectileTrailMaterial != null ? projectileTrailMaterial : GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        ApplySkin();
    }

    private void ApplySkin()
    {
        if (elemental == null) return;

        var equippedSkin = availableSkins.GetEquippedSkin(elemental.Type, InventoryManager.Instance);

        foreach (var skin in availableSkins.Skins)
        {
            if (skin == null || skin.id != equippedSkin.id)
                continue;

            if (equippedSkin.projectileMaterial != null)
                projectileMaterial.material = equippedSkin.projectileMaterial;

            if (equippedSkin.trailMaterial != null)
                projectileTrailMaterial.material = equippedSkin.trailMaterial;

            return;
        }
    }
}
