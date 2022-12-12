using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript: MonoBehaviour
{
    //////////////////////////////////////////This CLASS contains all properies of the grid
    public Transform circle;  // The circle object (our cell)
    public Transform square;  //    square(enviroment)
    //////////////////////////////COLORS FOR DIFERENT TYPE WHITE < RED  >  GREEN  > BLUE   > RED


    public float[,] Colors={{1.0f,1.0f,1.0f},{0.9f,0.1f,0.1f},{0.1f,0.9f,0.1f},{0.1f,0.1f,0.9f}};
   public float[,] SquareColors={{1.0f,1.0f,1.0f},{1.0f,1.0f,1.0f},{1.0f,1.0f,1.0f},{1.0f,1.0f,1.0f}};
    //white,red,green,blue  maps type 0,1,2,3!!! each type has its own color
    
    
    /////////////////////////////////////////
    public int squareOld;    // old type of the square 
    public int squareNew;   //new type of Square
    public int circleOld;   //old type of circle
    public int circleNew;   //new type of the circle
    public int x;   // the position in the grid
    public int y;// same

    public void ChangeCircle(int type){     //this function is used to change the type of the cell(circle)
        this.circleNew=type; 
    
    }
    public void ChangeSquare(int type){     //this is for enviroment(square)
        this.squareNew=type;
    }
    private void Start() {
        this.square=this.transform.GetChild(0);  //now we can access object square in code
        this.circle=this.transform.GetChild(1); //same
        
    }
    public void Render(){
          
                circleOld=circleNew;     //during render we just move the new state as old, and on next iteration can be used to compare
                squareOld = squareNew;
        circle.GetComponent<SpriteRenderer>().color= new Color(Colors[circleNew,0],Colors[circleNew,1],Colors[circleNew,2]);  //changes the color of the object
        square.GetComponent<SpriteRenderer>().color= new Color(SquareColors[squareNew,0],SquareColors[squareNew,1],SquareColors[squareNew,2]);
    }


    
}
