using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine;


namespace 数据库操作
{
    public class Database3: MonoBehaviour
    {
        //远程连接
        //  string connectionString= "User ID = ; Password =.; Host =; Port =3306;Database = ;Charset = ";
        //本地连接
        public static string connectionString = "User ID = root ; Password = 12345678 ; Host = 127.0.0.1 ; Port = 3306;Database =database ;Charset =  ";

        public static MySqlConnection dbConnection;

        void Start()
        {
  
            try
            {
                OpenSqlConnection(connectionString);
                //mySqlConnection.Open();
                Debug.Log("连接成功");
            }
            
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Debug.Log("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        Debug.Log("Invalid username/password, please try again");
                        break;
                }
            }


            string type ="select 车型 from database.unity where 列车编号=12341;";
            string start = "select 始发站 from database.unity where 列车编号=12341;";
            string reach= "select 终点站 from database.unity where 列车编号=12341;";
            //string formname = "车辆基本信息";
            Select(type);
            Select(start);
            Select(reach);
            //string sqlString = " INSERT INTO `firstdatabase`.`users` (`username`, `password`) VALUES ('7788', '12345678');";
            //GetDataSet(sqlString);
            CloseConnection();


        }



        //打开数据库链接
        static void OpenSqlConnection(string connectionString)
        {
            dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();
        }

        //关闭数据库连接
        static void CloseConnection()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
                dbConnection = null;
            }
        }

        //保存数据
        public static DataSet GetDataSet(string sqlString)
        {
            DataSet ds = new DataSet();
            try
            {
                //用于检索和保存数据
                //Fill(填充)能改变DataSet中的数据以便于数据源中数据匹配
                //Update(更新)能改变数据源中的数据以便于DataSet中的数据匹配

                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
                da.Fill(ds);
                Debug.Log(sqlString);

            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "\n" + ee.Message.ToString());
            }
            return ds;

        }

        //增 insert
        static void Add()
        {
            OpenSqlConnection(connectionString);
            string sqlstring = "insert into userinformation(name,password,tel) values();";//保证sql语句的正确性
            GetDataSet(sqlstring);
            CloseConnection();
        }

        //删 delete
        static void Delete()
        {
            OpenSqlConnection(connectionString);
            string sqlstring = "delete from 表名;";
            GetDataSet(sqlstring);
            CloseConnection();
        }

        //改 update
        static void Update()
        {
            OpenSqlConnection(connectionString);
            string sqlstring = "update 表名 set 字段=值 where 条件;";
            GetDataSet(sqlstring);
            CloseConnection();

        }

        //查 select
        static void Select(string sqlSelect)
        {
            OpenSqlConnection(connectionString);

            MySqlCommand mysqlcommand = new MySqlCommand(sqlSelect, dbConnection);
            MySqlDataReader reader = mysqlcommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        string result = reader.GetString(0);
                        Debug.Log(result);
                        //reader.getstring(0)/getint(0).....
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("failed to select");

            }
            finally
            {
                reader.Close();
            }

            CloseConnection();
        }
    }
}