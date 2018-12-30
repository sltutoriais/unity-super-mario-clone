using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour {



	public enum FollowTipe
	{
		moveTowards,//move de ponto em ponto
		Lerp //move vagarosamente ate chegar em um ponto
	}
	
	public Transform[] points;
	public PathDefinitions path;
	int index ;
	int cont = 0;
	int direction = 1;
	
	public FollowTipe Type = FollowTipe.moveTowards;
	
	
	public float speed = 1;
	public float MaxDistanceToGo = .1f;

	private Transform  _lastPoint;
	private Transform  _currentPoint;
	public Vector3 lastPosition;
	public Vector3 currentPosition;
	public Vector3 deltaMovement;
	private BoxCollider2D myboxcollider2D;
	public bool goToRight;
	
	// Use this for initialization
	void Start () {

		myboxcollider2D = this.GetComponent<BoxCollider2D> ();

		if(path == null)
		{
			return;
		}
		
		
		
		if (_currentPoint == null)
		{
			return;
		}
		
		
	}
	
	
	public void SetPathPoints(Transform[] _points, PathDefinitions _path)
	{
	  points = _points;
	  path = _path;
	  points = path.getPoints ();
	  _currentPoint = points[0];//o ponto atual recebe o primeiro ponto definido como Start la no unity e o indice zero do vetor points
		
		transform.position =  _currentPoint.position; // o inimigo recebe a posição do primeiro ponto no caso points[0] ou Start
		
	}
	// Update is called once per frame
	void Update () {
		

		if (_currentPoint == null)
		{
			
			return;
		}
		
		// se o tipo de movimento for moveTowards
		if (Type == FollowTipe.moveTowards) {
			lastPosition = transform.position ;//marca a ultima posição como a posição atual da inimigo pois ja vamos para o proximo ponto
			transform.position = Vector3.MoveTowards (transform.position,_currentPoint.position, Time.deltaTime * speed);//desloca o inimigo para currentPoint que já e o proximo ponto
			currentPosition = transform.position;//atualiza a posição atual na variavel currentPosition
		} 
		else if (Type == FollowTipe.Lerp)// se o tipo de movimento for Lerp
		{
			transform.position = Vector3.Lerp(transform.position, _currentPoint.position, Time.deltaTime * speed);//desloca o inimigo para currentPoint que já e o proximo ponto   
		}
		
		var distanceSquared = (transform.position - _currentPoint.position).sqrMagnitude;//calcula a distancia
		
		if (distanceSquared < MaxDistanceToGo * MaxDistanceToGo)// se a distancia for menor que MaxDistanceToGo ao quadrado
		{
			if(cont <= 0)// se cont chegou a -1 presisamos inverter o movimento do inimigo pois ela ja chegou no ponto  Start 
			{
				direction = 1;//inverte o sentido do movimento

			}
			
			if(cont >= points.Length - 1)// se o valor de cont for maior que o numero de pontos significa que o inimigo já chegou no ultimo ponto End
			{
				direction = -1;//inverte o sentido do movimento

			}
			_lastPoint = _currentPoint;//marca o ponto atual como sendo o ultimo visitado
			_currentPoint = path.getPoint(cont);//invoca o metodo getPathEnumerator() do script PathDefinitions que devolve o proximo ponto do caminho usando como calculo  a variavel cont
			
			cont = cont + direction;//incrementa ou diminui cont dependendo se o inimigo está indo ou voltando


				
		}

	} 

    //metodo que devolve o ponto atual
	public Transform  GetcurrentPoint()
	{
		return  _currentPoint;
	}
	//devolve ultima posição visitada
	public Transform  GetlastPoint()
	{
		return  _lastPoint;
	}
	
	//metodo apenas para testes não interfere em nada!
	public Vector3 getDeltaMovement()
	{
		float dx,dy,dz;
		dx = currentPosition.x - lastPosition.x;
		dy = currentPosition.y - lastPosition.y;
	    deltaMovement = new Vector3(dx,dy,0);
		return deltaMovement;
	}
   //metodo apenas para testes não interfere em nada!
	public float GetSpeed()
	{
		return speed;
	}

	public int GetCont()
	{
		return cont;
	}
	
     //metodo apenas para testes não interfere em nada!
	public float GetHeight()
	{
		var height = myboxcollider2D.size.y/2;
		return height;
	}


	void OnTriggerEnter2D(Collider2D colisor)
	{
		

		if (colisor.gameObject.name == "P1") {

			transform.eulerAngles = new Vector2(0,180);



		}

		if (colisor.gameObject.name == "P3") {

			transform.eulerAngles = new Vector2(0,0);


		}


	}

	void OnCollisionEnter2D(Collision2D colisor)
	{
		if (colisor.gameObject.tag == "Player") {

			GameManager.instance.GameOver ();
			GameManager.instance.StartGame ();



		}
	}



}
