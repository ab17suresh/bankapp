using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;
using bankapp.DAO;
using bankapp.Models;

namespace bankapp.Service
{
    public class Bank
    {
        AccountDAO accountdao;
        public Bank()
        {
            this.accountdao = new AccountDAO();
        }
        public User MainBank(int userid,int pin)
        {
            User User1 = this.accountdao.Login(userid, pin);
            Console.WriteLine("login");
            if (User1.Name != "null")
            {
                Console.WriteLine("hi " + User1.Name + "...!");
                
            }
            else
            {
                Console.WriteLine("Invalid PIN.");
            }
            return User1;
        }

        public Account Deposit(int amount,int userid)
        {
            Account account2=this.accountdao.Deposit(userid,amount);
            Console.Beep();
            Console.WriteLine("Thank you for using C ATM Bank. ");
            return account2;
        }

        public Account Withdraw(int amount,int userid)
        {
            Account account2=this.accountdao.Withdraw(userid,amount);
            Console.Beep();
            Console.WriteLine("Thank you for using C ATM Bank. ");
            return account2;
        }

        public Account Transfer(int sendingAmount,int userid,int accountno)
        {
            Account account4=this.accountdao.Transfer(userid,accountno,sendingAmount);
            Console.Beep();
            Console.WriteLine("Thank you for using C ATM Bank. ");
            return account4;
        }

        public int CheckBalance(int userid)
        {
            int account2=this.accountdao.CheckBalance(userid);
            Console.Beep();
            Console.WriteLine("Thank you for using C ATM Bank. ");
            return account2;
        }

        public Transaction[] Transaction(int userid)
        {
            Transaction[] Transaction1=this.accountdao.Transactions(userid);
            Console.Beep();
            Console.WriteLine("Thank you for using C ATM Bank. ");
            return Transaction1;
        }

        public User ChangePin(int newpin,int userid)
        {
            User User2=this.accountdao.ChangePin(userid,newpin);
            Console.Beep();
            Console.WriteLine("Thank you for using C ATM Bank. ");
            return User2;
        }
    }
}