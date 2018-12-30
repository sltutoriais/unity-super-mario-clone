using UnityEngine;
using System.Collections;

public class PathDefinitions : MonoBehaviour {

	public Transform[] points;//contem os pontos por onde a plataforma vai passar
	int  index = 0;
	
	//desenha linhas para que fique visivel ao desenvolvedor o caminho que  a plataforma vai seguir
	void OnDrawGizmosSelected() {
		
		
		if (points != null && points.Length>=2) {
			Gizmos.color = Color.blue;
			Transform p1,p2;
			for(int i = 1; i< points.Length; i = i+1)
			{
				p1 = (Transform) points[i-1];
				p2 = (Transform) points[i];
				Gizmos.DrawLine(p1.position, p2.position);
			}
		}
		
		
	}
	
	public  Transform[] getPoints() {
		return points;
	}
	
	public  Transform getPoint(int index) {
		return (Transform)points[index];
	}
}
