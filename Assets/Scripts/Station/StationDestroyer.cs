using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StationDestroyer : MonoBehaviour
{
    [Header("Destruction")]
    public float destroyRadius = 2;
    [SerializeField] private GameObject[] blocks;

    [Header("Timer")]
    public float respawnTime = 3f;

    private class DeactivatedBlockInfo
    {
        public GameObject block;
        public float deactivationTime;

        public DeactivatedBlockInfo(GameObject block, float time)
        {
            this.block = block;
            this.deactivationTime = time;
        }
    }

    private List<DeactivatedBlockInfo> deactivatedBlocks = new List<DeactivatedBlockInfo>();

    void Update()
    {
        //float currentTime = Time.time;

        //for (int i = deactivatedBlocks.Count - 1; i >= 0; i--)
        //{
        //    DeactivatedBlockInfo info = deactivatedBlocks[i];

        //    if (currentTime - info.deactivationTime >= respawnTime)
        //    {
        //        if (info.block != null)
        //        {
        //            info.block.SetActive(true);
        //        }

        //        deactivatedBlocks.RemoveAt(i);
        //    }
        //}
    }

    internal void BlockDestroyed(Transform transform)
    {
        foreach (var block in blocks) 
        { 
            if(Vector3.Distance(block.transform.position, transform.position)  < destroyRadius)
            {
                if (block.activeSelf)
                {
                    block.SetActive(false);

                    //bool alreadyInList = false;
                    //foreach (var info in deactivatedBlocks)
                    //{
                    //    if (info.block == block)
                    //    {
                    //        alreadyInList = true;
                    //        break;
                    //    }
                    //}

                    //if (!alreadyInList)
                    //{
                    //    deactivatedBlocks.Add(new DeactivatedBlockInfo(block, Time.time));
                    //}
                }
            }
        }
    }
}