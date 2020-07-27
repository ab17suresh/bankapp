using Microsoft.AspNetCore.Mvc;
using bankapp.DAO;
using bankapp.Models;
using bankapp.Service;
using Microsoft.AspNetCore.Http;
using System;
namespace bankapp.Controllers
{
    public class HomeController : Controller
    {
     Bank bank1;
     public HomeController()
     {
         this.bank1= new Bank();
         
     }
        public ActionResult Index()
        {           
            
            return View();
        }
        [HttpPost]        
         public ActionResult login([FromForm]int uid ,int pw)
        {
            //this.HttpContext.Session.SetInt32("UserId",uid);
            User user2=this.bank1.MainBank(uid,pw);           
                if (user2.Name == "null")
            {
                string er = "Incorrect UserID or PIN";
                this.HttpContext.Session.SetString("Error",er);
                var Errmsg = this.HttpContext.Session.GetString("Error");
                ViewData["Error"] = Errmsg;
                return View("Index");
            }         
            else
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(30);
                option.SameSite = SameSiteMode.Strict;
                string UserID1 = Convert.ToString(uid);
                Response.Cookies.Append("Cookie1",UserID1,option);
                return View("Mainmenu");            
            }
                       
        }  
        [HttpGet]        
         public ActionResult Depositpage()
        {
           
            return View();           
        }  

        [HttpPost]        
         public ActionResult deposit([FromForm]int amount)
        {
            //int? userid=this.HttpContext.Session.GetInt32("UserId");
            //Console.WriteLine(userid);
            //int userid=1;
            var IsCookieAvail = Request.Cookies.ContainsKey("Cookie1");
            string value;
            Request.Cookies.TryGetValue("Cookie1", out value);
            //if (userid.hasValue )
            //{
            int userid =Convert.ToInt32(value);
            Console.WriteLine(amount);
            Console.WriteLine("ur id is "+userid);
            Account Account1=this.bank1.Deposit(amount,userid);           
            return View("Result");  
            //}    
           // else
            //{
                
            //}     
        }  

        [HttpGet]        
         public ActionResult Withdrawpage()
        {
           
            return View();           
        }  

        [HttpPost]        
         public ActionResult withdraw([FromForm]int wamount )
        {
            var IsCookieAvail = Request.Cookies.ContainsKey("Cookie1");
            string value;
            Request.Cookies.TryGetValue("Cookie1", out value);
            int userid =Convert.ToInt32(value);
            int item1 = this.bank1.CheckBalance(userid);
            //
            this.HttpContext.Session.SetInt32("Balance", item1);
            var Bal = this.HttpContext.Session.GetInt32("Balance");
            if (wamount <= Bal)
            {
                this.bank1.Withdraw(wamount,userid);
                return View("Result");
            }
            else
            {
                string error1  = "insufficient Balance";
                this.HttpContext.Session.SetString("st1", error1);
                var withdrawmsg = this.HttpContext.Session.GetString("st1");
                ViewData["errormsg1"] = withdrawmsg;
                return View("Withdrawpage");
            }
              
        } 

        
        [HttpGet]        
         public ActionResult Transferpage()
        {
           
            return View();           
        } 

        [HttpPost]        
         public ActionResult transfer([FromForm]int tamount,int acccno )
        {

            var IsCookieAvail = Request.Cookies.ContainsKey("Cookie1");
            string value;
            Request.Cookies.TryGetValue("Cookie1", out value);
            int userid =Convert.ToInt32(value);            
            int item1 = this.bank1.CheckBalance(userid);
            this.HttpContext.Session.SetInt32("Balance", item1);
            var Bal = this.HttpContext.Session.GetInt32("Balance");
            if (tamount <= Bal)
            {
                this.bank1.Transfer(tamount,userid,acccno);
                return View("Result");
            }
            else {
                string error2 = "insufficient balance in your account!";
                this.HttpContext.Session.SetString("st2", error2);
                var transfermsg = this.HttpContext.Session.GetString("st2");
                ViewData["errormsg2"] = transfermsg;
                return View("Transferpage");
            }          
        } 

        

        [HttpGet]        
         public ActionResult Balancecheck( )
        {
            var IsCookieAvail = Request.Cookies.ContainsKey("Cookie1");
            string value;
            Request.Cookies.TryGetValue("Cookie1", out value);
            int userid =Convert.ToInt32(value);
            Console.WriteLine(userid);
            int Account4=this.bank1.CheckBalance(userid);  
            this.HttpContext.Session.SetInt32("Balance", Account4);
            Console.WriteLine("balance");
            Console.WriteLine(Account4);
            var Bal = this.HttpContext.Session.GetInt32("Balance");
            ViewData["Balance"] = Bal;         
            return View();           
        } 
        
       
        [HttpGet]        
         public ActionResult Transactionlog( )
        {
            var IsCookieAvail = Request.Cookies.ContainsKey("Cookie1");
            string value;
            Request.Cookies.TryGetValue("Cookie1", out value);
            int userid =Convert.ToInt32(value);
            Console.WriteLine(userid);
            Transaction[] Transaction2=this.bank1.Transaction(userid);                
            ViewData["TranLog"] = Transaction2;
            int i=1;
            while( i < Transaction2.Length)
            {   
                Console.WriteLine( "  "+Transaction2[i].TranID+"     "+Transaction2[i].AccountNO+"    "+ Transaction2[i].TranType+"      "+Transaction2[i].TranAmount+"      "+ Transaction2[i].Balance+"      "+ Transaction2[i].TranDate );
                i++;
            }
            Console.WriteLine("end");
            return View(Transaction2);           
 
       } 
 
        [HttpGet]        
         public ActionResult ChangePinpage()
        {
           
            return View();           
        } 
   
        [HttpPost]        
         public ActionResult ChangePin([FromForm]int newpin )
        {
            var IsCookieAvail = Request.Cookies.ContainsKey("Cookie1");
            string value;
            Request.Cookies.TryGetValue("Cookie1", out value);
            int userid =Convert.ToInt32(value);
            User user2=this.bank1.ChangePin(newpin,userid);           
            return View("Result");           
        } 
        [HttpGet]        
         public ActionResult Result()
        {           
            return View();           
        } 
         
               
         

    }
}