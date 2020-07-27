namespace bankapp.Models
{
    public class User
    {
        //[MaxLength]
       //[MinLength]
       //[Required(ErrorMessage="enter Pin")] 
       public int Pin{get;set;}
      
       //[Required(ErrorMessage="enter user id")] 
       public int UserID{get;set;}
       public string Name{get;set;}
    }
}