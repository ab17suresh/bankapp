namespace bankapp.Models
{
    public class Transaction
    {
       public int TranID{get;set;}
       public int UserID{get;set;}
       public int AccountNO{get;set;}
       public string TranType{get;set;}
       public string TranDate{get;set;}
       public int TranAmount{get;set;}
       public int Balance{get;set;}
    }
}