using System;
using bankapp.Models;
using Microsoft.Data.Sqlite;
namespace bankapp.DAO
{
    public class AccountDAO
    {
        //string DataSource="./bank.db";    
        private readonly SqliteConnection conn;
        public AccountDAO()
        {
            /*IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            var section = Configuration.GetSection("ConnectionString");
            myConnectionString = section.Value;*/
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();;
            connectionStringBuilder.DataSource = "./bank.db";
            connectionStringBuilder.Mode=SqliteOpenMode.ReadWriteCreate;
            conn = new SqliteConnection(connectionStringBuilder.ConnectionString);
        }
        public User Login(int UserID,int Pin)
        {

            string selectQuery = "SELECT Name FROM Login where UserID = "+UserID + " AND Pin = " + Pin;
            SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            User user1=new User();
            reader.Read();
            //user1.UserID=reader.GetInt32(0);
            
            try
            {
                user1.Name=reader.GetString(0);
                return user1;           

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                 user1.Name= "null";
                 return user1;
            }
            finally
            {
                conn.Close();
            }
            
            
        }

        public User ChangePin(int UserID,int newpin)
        {
            string changeQuery = "UPDATE  Login SET Pin="+newpin+" where UserID = "+ UserID;
            SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
            conn.Open();
            updateCommand.ExecuteScalar();
            Console.WriteLine( " your pin has been changed sucessfully");
            return null;
        }

        public Account Deposit(int UserID,int amt)
        {
            string selectQuery = "SELECT  Balance,AccountNO FROM  Account Where UserID = "+ UserID;
            SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            Account account2=new Account();
            while (reader.Read())
            {
                account2.Balance=reader.GetInt32(0);
                account2.AccountNO=reader.GetInt32(1);
            }
            int Totamount=account2.Balance+ amt;
            string changeQuery = "UPDATE Account SET Balance=" + Totamount+" WHERE UserID = "+ UserID;
            SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
            updateCommand.ExecuteNonQuery();
            DateTime now=DateTime.Now;
            string insertQuery = "INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+UserID+","+account2.AccountNO+",'D','"+now+"',"+amt+","+Totamount+")";
            SqliteCommand insertCommand = new SqliteCommand(insertQuery, conn);
            insertCommand.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine( " your transaction is sucessfully");
            return account2;
        }

        public Account Withdraw(int UserID,int wamt)
        {
            conn.Open();
                string selectQuery = "SELECT  Balance,AccountNO FROM  Account Where UserID = "+ UserID;
                SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
                SqliteDataReader reader = selectCommand.ExecuteReader();
                Account account3=new Account();
                reader.Read();
                account3.Balance=reader.GetInt32(0);
                account3.AccountNO=reader.GetInt32(1);
                if(account3.Balance >= wamt)
                {
                    int NewBalance= account3.Balance - wamt;           
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "PRAGMA foreign_keys = ON";
                    cmd.ExecuteNonQuery();
                    string changeQuery = "UPDATE Account SET Balance=" + NewBalance+" WHERE UserID = "+ UserID;
                    SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
                    updateCommand.ExecuteNonQuery();
                    DateTime now=DateTime.Now;
                    string insertQuery ="INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+UserID+","+account3.AccountNO+",'D','"+now+"',"+wamt+","+NewBalance+")";
                    SqliteCommand insertCommand = new SqliteCommand(insertQuery, conn);
                    insertCommand.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("low balance");
                }
                conn.Close();
                //wdprocess.Commit();
                Console.WriteLine("transaction is Completed!!! ");
                return account3;

        }

        public Account Transfer(int UserID,int sact,int samt)
        {
            conn.Open();
                string selectQuery = "SELECT  AccountNO,Balance FROM  Account Where UserID = "+ UserID;
                SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
                SqliteDataReader reader = selectCommand.ExecuteReader();
                Account account3=new Account();
                reader.Read();
                account3.AccountNO=reader.GetInt32(0);
                account3.Balance=reader.GetInt32(1);
                string selectQuery1 = "SELECT  UserID,Balance FROM  Account Where AccountNO = "+ sact;
                SqliteCommand selectCommand1 = new SqliteCommand(selectQuery1, conn);
                SqliteDataReader reader4 = selectCommand1.ExecuteReader();
                Account account4=new Account();
                reader4.Read();
                account4.UserID= reader4.GetInt32(0);
                account4.Balance= reader4.GetInt32(1);
                if(account3.Balance >= samt)
                    {
                        int SenderBalance= account3.Balance - samt;
                        string changeQuery = "UPDATE Account SET Balance=" + SenderBalance+" WHERE UserID = "+ UserID;
                        SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
                        updateCommand.ExecuteScalar();
                        DateTime now=DateTime.Now;
                        string insertQuery ="INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+UserID+","+account3.AccountNO+",'D','"+now+"',"+samt+","+SenderBalance+")";
                        SqliteCommand insertCommand1 = new SqliteCommand(insertQuery, conn);
                        insertCommand1.ExecuteNonQuery();
                        int ReciverBalance= account4.Balance + samt;
                        string changeQuery1 = "UPDATE Account SET Balance=" + ReciverBalance+" WHERE AccountNO = "+ sact;
                        SqliteCommand updateCommand1 = new SqliteCommand(changeQuery1, conn);
                        updateCommand1.ExecuteScalar();
                        string insertQuery2 ="INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+account4.UserID+","+sact+",'C','"+now+"',"+samt+","+ReciverBalance+")";
                        SqliteCommand insertCommand2 = new SqliteCommand(insertQuery2, conn);
                        insertCommand2.ExecuteNonQuery();
                    }
                else
                    {
                        Console.WriteLine("low balance");
                    }
                conn.Close();
                //wdprocess.Commit();
                Console.WriteLine("transaction is Completed!!! ");
                return account3;

            //}
            //catch(Exception e)
            //{
             //   Console.WriteLine(e);
            //    wdprocess.Rollback();
             //   
             //   Console.WriteLine("transaction failed!!! ");

            //}

        }

        public int CheckBalance(int UserID)
        {
            string checkQuery = "SELECT Balance FROM Account where UserID = "+ UserID;
            SqliteCommand selectCommand = new SqliteCommand(checkQuery, conn);
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            int[] balance =new int[1];
            reader.Read();
            balance[0]= reader.GetInt32(0);
            Console.WriteLine("balance"+balance[0]);
            conn.Close();
            return balance[0];
        }

/*
        public User[] TransLog(int UserID)
        {
            int i = 0;
            string countTransQuery = "SELECT COUNT(*) FROM Trans WHERE UserID = " + UserID;
            MySqlCommand countCommand = new MySqlCommand(countTransQuery, conn);
            conn.Open();
            Int64 n = (Int64)countCommand.ExecuteScalar();
            User[] Tran1 = new User[n];
            string TransLogQuery = "SELECT TransID, CD, Amount, AccountNo, UserID, Dated FROM Trans WHERE UserID = " + UserID;
            MySqlCommand selectCommand = new MySqlCommand(TransLogQuery, conn);
            MySqlDataReader reader = selectCommand.ExecuteReader();
            Console.WriteLine("  TranID     CD     Amount      AccountNo      Date");
            while (reader.Read())
            {
                User Tran = new User();
                Tran.TransID = reader.GetInt32(0);
                Tran.CD = reader.GetString(1);
                Tran.Amount = reader.GetInt32(2);
                Tran.AccountNo = reader.GetInt32(3);
                Tran.Dated = reader.GetString(5);
                Tran1[i] = Tran;
       //         Console.WriteLine("  " + Tran.TransID + "      " + Tran.CD + "      " + Tran.Amount + "        " + Tran.AccountNo+"       "+Tran.Dated);
                i++;
            }
            conn.Close();
            return Tran1;
        }
*/
         public Transaction[] Transactions(int UserID)
        {
            string countQuery = "SELECT  COUNT(*) FROM Transactions WHERE UserID = "+ UserID;
            SqliteCommand countCommand = new SqliteCommand(countQuery, conn);
            conn.Open();
            Int64 num2=(Int64)countCommand.ExecuteScalar();
            Transaction[] transaction1= new Transaction[num2];
            string tranQuery = "SELECT  TranID,AccountNO,TranType,TranAmount,Balance,TranDate FROM Transactions WHERE UserID = "+ UserID;
            SqliteCommand selectCommand = new SqliteCommand(tranQuery, conn);
            SqliteDataReader reader1 = selectCommand.ExecuteReader();
            int i =0;
            Console.WriteLine(" tranID AccountNO Trantype TranAmount   Balance     date");
            while (reader1.Read())
                {
                Transaction tran2 = new Transaction
                {
                    TranID = reader1.GetInt32(0),
                    AccountNO = reader1.GetInt32(1),
                    TranType = reader1.GetString(2),
                    TranAmount = reader1.GetInt32(3),
                    Balance = reader1.GetInt32(4),
                    TranDate = reader1.GetString(5),
                };
                transaction1[i] = tran2;
                    //Console.WriteLine( "  "+tran2.TranID+"     "+tran2.AccountNO+"    "+ tran2.TranType+"      "+tran2.TranAmount+"      "+ tran2.Balance+"      "+ tran2.TranDate );
                    i++;
                }
                conn.Close();
                return transaction1;
        }
    }
}