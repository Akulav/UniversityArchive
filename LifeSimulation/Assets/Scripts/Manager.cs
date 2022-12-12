using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public bool isStarted=false;    // it is required to prevent multiple function calling, once the game is started the "Start" button has no impact later in code

    

    public int button=0;      // the last pressed button (that sets the type on selecting an object)
    float interval= 0.5f;   //generation interval
    float maxintensitySquare=1.0f;    // intensity of color on square
    float maxintensityCircle=0.04f;    // same for circle
    /////////////////////////////////////////
    public GameObject Grid;        // this is the variable that is linked with the prefab in the inspector 

    public GameObject[,] grid;    //array of objects (our grid object)
    int Vertical,Horizontal,Columns,Rows;   //vertical are ortographicsize camera, horizontal the same...    ROWS AND COLUMNS are the dimensions of our array

    // Start is called before the first frame update
    void Start()
    {
        Vertical=(int)Camera.main.orthographicSize;
        Horizontal=Vertical*(Screen.width/Screen.height);
        Columns=(int)(Horizontal*3.0);                              //there are 5 colums per camera unit
        Rows=(int)(Vertical*3.0);
        grid=new GameObject[Columns,Rows];
        for (int i=0;i<Rows;i++){
            for (int j = 0; j < Rows; j++)
            {
               grid[i,j]=Spawn(i,j);    //spawn the object
            }
        }
        
        
    }
    /////////////////////////////////////////////////////////////////////////////////////////////
    public void StartGame()
    {
        for (int i=0;i<100;i++){
                int a=Random.Range(0,Columns);
                int b=Random.Range(0,Rows);
               grid[a,b].GetComponent<GridScript>().circleNew=2;    //spawn the object
               grid[a,b].GetComponent<GridScript>().Render();
        }
        if (!isStarted){
            isStarted=true;
            InvokeRepeating("NewGenerationUpdate", interval, interval);     //invoking the periodical function "NewGeneration"
        }
    }
    public void NewGenerationUpdate(){
                                       //apply rules and changes the new array
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {  
                grid[i,j].GetComponent<GridScript>().Render();    //drow current state
            }
        }
        ApplyRules();  
        
    }
    public void ApplyRules(){
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                int sameNeighbours= CountSameNeighbours(i,j);
                if(sameNeighbours!=0)
                Debug.Log("Element "+i+','+j+" has neighbours:"+sameNeighbours);
                if (sameNeighbours == 3){
                    grid[i, j].GetComponent<GridScript>().circleNew =2;
                }else if(sameNeighbours == 2 && grid[i, j].GetComponent<GridScript>().circleOld != 0){
                    grid[i, j].GetComponent<GridScript>().circleNew =2;
                }
                else{
                    grid[i, j].GetComponent<GridScript>().circleNew =0;
                }
            }
        }
    }
    public int  CountSameNeighbours(int x ,int y){
        int result = 0;
        int oldType = grid[x,y].GetComponent<GridScript>().circleOld;
        int iterationType;
       
        for (int i = x-1; i < x+2; i++)
        {
            for (int j = y-1; j < y+2; j++)
            {
                try{
                iterationType=grid[i,j].GetComponent<GridScript>().circleOld;
                if(i==x && j==y) continue;
                
                    if(iterationType!=0){
                        result++; 
                    }
                }
            catch{}
        }
       }
        
        return result;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////
    private GameObject Spawn(int x, int y){
        GameObject g1 = Instantiate(Grid);     //new Object
        g1.GetComponent<GridScript>().x=x;     //sets the x and y coordinate as a property
        g1.GetComponent<GridScript>().y=y;
        g1.transform.localScale=new Vector3((float)Horizontal/Columns*2,(float)Vertical/Rows*2,1);      //rescale the object dimensions


        //DONT even try to undestand this function, this is a piece of art that works correct, so does not need to change or to use it
        g1.transform.position= new Vector3((x-(float)Rows/2+0.5f)*g1.transform.localScale.x,(y-(float)Columns/2+0.5f)*g1.transform.localScale.y,-1);
        
        return g1;    // returns in the array this object, so it can be accesed after creation, so array element is no more null
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0)) {   //listen mouse leftclick
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);   // shut with a ray and the first object is the 'hit' object
            if (hit && (button>3 || isStarted==false)) {
                ChangeType(button,hit);    //changes the type(last pressed button) of the hit object
                
                
            }else{
                Debug.Log("nothing");   //there is no colision with an object
            }
            
        }
        System.Random rnd = new System.Random();
        for (int x = 0; x<Rows;x++){
            for (int y = 0; y<Columns;y++){
        int a = rnd.Next(100);
        int b = rnd.Next(100);
        toChange(a,b);
        }
        }
    
    }
    public void ChangeType(int type,RaycastHit2D hit){
        var parent=hit.collider.gameObject.transform.parent;     // the parent of the square/circle   the parent grid Object
        var square=parent.GetChild(0); // the square child of the grid
        var circle=parent.GetChild(1);//circle
            if(button==1 || button==2 || button==3){  //RGB circle buttons 
                parent.GetComponent<GridScript>().circleNew=button;
                    if (button==1)
                    {
                        
                        circle.GetComponent<SpriteRenderer>().color=new Color(0.8f,0.1f,0.1f);
                    }
                 else if(button==2){
                    circle.GetComponent<SpriteRenderer>().color=new Color(0.1f,0.8f,0.1f);
                } else if(button==3)
                {
                    circle.GetComponent<SpriteRenderer>().color=new Color(0.1f,0.1f,0.8f);
            }     
                }else if(button==4 || button==5 || button==6)   //RGB square button
                {
                    parent.GetComponent<GridScript>().squareOld=button-3;   // 4-3   5-3   6-3   => 1, 2, 3  type of color
                    var script=parent.GetComponent<GridScript>();   //gets the script from the parent object that stores ALL information about the grid
                    var x=script.x;   // the x position of the grid that was hit by the Ray
                    var y=script.y;
                    for (int i = x-2; (i <= x+2 && i<=Rows); i++)
                    {
                        for(int j=y-2; (j <= y+2 && j<=Columns);j++){     /// will try to change the type of the grids, but there can be an issue with indexError, that is why we use try
                            try
                            {
                                grid[i,j].GetComponent<GridScript>().squareOld = button - 3;
                                if(button==4){
                               grid[i,j].transform.GetChild(0).GetComponent<SpriteRenderer>().color=new Color(167f/255f, 94f/255f, 94f/255f);
                               }
                               else if(button==5){
                                    grid[i,j].transform.GetChild(0).GetComponent<SpriteRenderer>().color=new Color(208f/255f, 230f/255f, 21f/255f); 
                               }
                               else if(button==6){
                                    grid[i,j].transform.GetChild(0).GetComponent<SpriteRenderer>().color= new Color(15f/255f, 182f/255f, 149f/255f); 
                               }
                               
                            }
                            catch(System.Exception){

                            }
                            
                        }
                    }
                }
    }
    private void trytoChange(int a,int b, int c, int d){   // try to change one type to other
        try
        {
            
                var old=grid[a,b].GetComponent<GridScript>().circleOld;   //old type of the cell
                var newType=grid[c,d].GetComponent<GridScript>().circleNew;    //new type of the cell
                if (old==newType-1 || old==3 && newType==1 || old==0)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(grid[c,d].GetComponent<GridScript>().circleOld);   //changes the type
            
            
        }
        catch (System.Exception)
        {

        }
    }
    
        private void toChange(int a,int b){   // try to change one type to other
        try
        {
            
                var old=grid[a,b].GetComponent<GridScript>().circleOld;   //old type of the cell
                //var newType=grid[c,d].GetComponent<GridScript>().circleNew;    //new type of the cell
                if (old==3)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(2);   //changes the type
            if (old==2)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(1);   //changes the type
            if (old==1)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(0);   //changes the type
            // switch (grid[a, b].GetComponent<GridScript>().circleOld)    
            //     {                                 //increase the score depending of the new type
                    
            //     case 1:
            //      score[1]++;
            //      ScoreRed.text="Score Red:"+score[1];
            //      break;
            //     case 2:
            //      score[2]++; 
            //      ScoreGreen.text="Score Green:"+score[2];
            //      break;
            //      case 3:
            //      score[3]++;
            //      ScoreBlue.text="Score Blue:"+score[3];
            //      break;
            //      default:
            //      break;
            //     }
            
        }
        catch (System.Exception)
        {

        }
    }
    
           private void dontChange(int a,int b){   // try to change one type to other
        try
        {
            
                var old=grid[a,b].GetComponent<GridScript>().circleOld;   //old type of the cell
                //var newType=grid[c,d].GetComponent<GridScript>().circleNew;    //new type of the cell
                if (old==3)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(3);   //changes the type
            if (old==2)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(2);   //changes the type
            if (old==1)   //                                    Red<Blue<Green<RED
            grid[a,b].GetComponent<GridScript>().ChangeCircle(1);   //changes the type
        }
        catch (System.Exception)
        {

        }
    }
}
