using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlipNormal : MonoBehaviour
{
    //노말에 뒤집는 방식으로 안쪽에서 텍스처가 보이게 한다.
    //유니티의 랜더링 특징(오른쪽으로 그린다)를 이용한다.
    //직접적으로 왼쪽으로 그리게 할 수 없으니 메쉬의 버텍스(점)의 위치를 바꿔서 반대로 그리는 효과를 주는 트릭

    [SerializeField] private MeshFilter meshfilter;
    // Start is called before the first frame update
    void Start()
    {
        //메쉬필터 컴포넌트의 mesh를 할당한다.
        Mesh mesh = meshfilter.mesh;
        if(mesh != null)
        {
            Vector3[] normals = mesh.normals;
            for(int i=0; i<normals.Length; i++)
            {
                Debug.Log($"normals{i}=>{normals[i]}");
                normals[i] *= -1;
            }

            mesh.normals = normals;

            for(int i=0; i<mesh.subMeshCount; i++)
            {
                //매쉬의 트라이앵글을 가져온다.(3개의 점)
                int[] test = mesh.GetTriangles(i);
                for(int t=0; t<test.Length; t+=3)
                {
                    //점의 순서를 바꾼다. A값과 B값을 바꾸는 방법? A=5,B=3 
                    int temp = test[t];
                    //n번은 n+1값으로,n+1값은 n값으로 서로 바꾼다.
                    test[t] = test[t + 1];
                    test[t + 1] = temp;
                }

                mesh.SetTriangles(test, i);
            }
        }
    }
}

