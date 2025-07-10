using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RR_Coronet.CustomMonoScripts
{
    public class HideMesh : MonoBehaviour
    {
        private MeshRenderer Renderer;


        public void Start()
        {
            if (Renderer == null)
            {
                Renderer = base.GetComponent<MeshRenderer>();
            }

            if (base.transform.parent.name == base.name)
            {
                Renderer.enabled = true;
            }
            else 
            {
                Renderer.enabled = false;
            }
        }

        public void Toggle(bool Enable) 
        {
            Renderer.enabled = Enable;
        }

    }
}

