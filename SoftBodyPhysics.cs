using System;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))] //make sure there is a mesh for simulation

public class SoftBodyPhysics : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    public float softness = 1f; // max boingness

    [Range(0.01f, 1f)]
    public float damping = 0.1f; // reduce boingness gradually

    public float stiffness = 1f; // controls deformation    

    private void Start()
    {
        CreateSoftBodyPhysics(); // create physx
    }

    void CreateSoftBodyPhysics() //how the physx works:
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>(); // get mesh (needs cloth)

        if (skinnedMeshRenderer == null) //make sure there is one
        {
            Debug.Log("No skinned mesh renderer found"); // error message
            return; // exit
        }

        Cloth cloth = gameObject.AddComponent<Cloth>(); //cloth use (for the texture of shirts usully)
        cloth.damping = damping; // set mmovement slow
        cloth.bendingStiffness = stiffness;  // set bending stiffness

        cloth.coefficients = GenerateClothCoefficients(skinnedMeshRenderer.sharedMesh.vertices.Length); //max lenght of the vertex (of triangles of cloth) movement
    }

    private ClothSkinningCoefficient[] GenerateClothCoefficients(int vertexCount) // generate a list of coefficients for the simulation
    {
        ClothSkinningCoefficient[] coefficients = new ClothSkinningCoefficient[vertexCount]; //array of coefficients for each vertex of triangles

        for (int i = 0; i < vertexCount; i++) // list of triangle vertex and...
        {
            coefficients[i].maxDistance = softness; // apply how much it can boing based on softness
            coefficients[i].collisionSphereDistance = 0f; // but it's still a sphere
        }
    return coefficients;
    }

}
