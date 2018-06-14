using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RiMonitoring
{
    class DataManager
    {
        private string m_ConnectionString;
        public string m_TableName; //Edit by art 5/02/57
        private int m_ObjectID;

        private MongoClient Client = null;
        private IMongoDatabase DB = null;
        public string ConnnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }

        public int ObjectID
        {
            get
            {
                return m_ObjectID;
            }
            set
            {
                m_ObjectID = (int)value;
            }
        }


        public DataManager(string ConnectionString)
        {
            m_TableName = "data_logger";
            m_ConnectionString = ConnectionString;

            Client = new MongoClient(ConnectionString);
            DB = Client.GetDatabase("dbpiya");
        }


        //  **** frmConfigAlarm
        public void ConfigColor(int _BackNormal, int _ForeNormal, int _BackAlarm, int _ForeAlarm)
        {
            try
            {
                using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
                {
                    string Sql = @"update code_read set 
                                   backcolor_normal=@BackNormal, 
                                   forecolor_normal=@ForeNormal,
                                   backcolor_alarm=@BackAlarm,
                                   forecolor_alarm=@ForeAlarm
                                   where code='R10'";
                    Sql = Sql.Replace("@BackNormal", _BackNormal.ToString());
                    Sql = Sql.Replace("@ForeNormal", _ForeNormal.ToString());
                    Sql = Sql.Replace("@BackAlarm", _BackAlarm.ToString());
                    Sql = Sql.Replace("@ForeAlarm", _ForeAlarm.ToString());
                    MySqlCommand cmd = new MySqlCommand(Sql, Conn);
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        private DataTable bsonDocDatatbl(List<BsonDocument> bd)
        {
            try
            {
                DataTable table = new DataTable();
                foreach (var n in bd[0].Elements)
                {
                    table.Columns.Add(n.Name);//, typeof(string));
                }

                DataRow dr = null;
                foreach (var item in bd)
                {
                    dr = table.NewRow();
                    foreach (var n in item.Elements) //(BsonElement elm in doc.Elements)
                    {
                        dr[n.Name] = n.Value;
                    }
                    table.Rows.Add(dr);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataRow ReadConfigColor()
        {
            try
            {
                var collection = DB.GetCollection<BsonDocument>("CodeRead");
                var builder = Builders<BsonDocument>.Filter;
                var filt = builder.Eq("code", "R10");// & builder.Eq("ProductName", "WH-208");
                var resultDoc = collection.Find(filt).ToList();
                DataTable table = new DataTable();
                table = bsonDocDatatbl(resultDoc);
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            /*
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = "SELECT * FROM code_read where code='R10'";
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0].Rows[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            */
        }
        public DataTable CodeReadAlarm()
        {
            /*
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string _wherecode = "and (a.code='R14' OR a.code='R91' OR a.code='R92' OR a.code='R93' OR a.code='R121' OR a.code='R122' OR a.code='R128')"; // Create By art 19/02/57
                string txtSql = @"SELECT a.code,a.description, alarm_min,alarm_max FROM code_read a
                                    inner join code_addr b on addr_id=b.code where alarm_active='Y' and b.active='Y' " + _wherecode + " "; // Edit By art 18/02/57
                try
                {
                    //Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            */
            try
            {
                var collection = DB.GetCollection<BsonDocument>("CodeRead");
                var builder = Builders<BsonDocument>.Filter;
                var filt = "{code:{$in:['R14','R91','R92','R93','R121','R122','R128']},alarm_active:{$eq:'Y'},active:{$eq:'Y'}}";// builder.Eq("code", "R10");


                var resultDoc = collection.Find(filt).ToList();

                DataTable table = new DataTable();
                table = bsonDocDatatbl(resultDoc);
                return table;//.Rows[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public DataTable CodeConstant()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT code,desc1,val from code_constant";
                try
                {
                    //Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public DataTable GetTimeSwap()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT val FROM code_constant WHERE code = 01";
                try
                {
                    //Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public void SaveConstant(string _Code, float _val)
        {
            try
            {
                using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
                {
                    string Sql = @"update code_constant set 
                                   val=@val 
                                   where code='@Code'";
                    Sql = Sql.Replace("@Code", _Code.ToString());
                    Sql = Sql.Replace("@val", _val.ToString());
                    MySqlCommand cmd = new MySqlCommand(Sql, Conn);
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public int GetLineColor(string _MapColumn,string _AddrID)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT color_graph 
                                FROM `code_read` 
                                WHERE map_column='"+_MapColumn+"' and addr_id='"+_AddrID+"' and active='Y'";

                try
                {
                    Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public string GetDescription(string _AddrID)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT description 
                                FROM `code_addr` 
                                WHERE code='" + _AddrID + "' and active='Y'";

                try
                {
                    Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    return Convert.ToString(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        


        // **** frmAdjustAlarm
        public void ConfigAlarm(string _Code, float _AlarmMin, float _AlarmMax)
        {
            try
            {
                using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
                {
                    string Sql = @"update code_read set 
                                   alarm_min=@AlarmMin, 
                                   alarm_max=@AlarmMax
                                   where code='@Code'";
                    Sql = Sql.Replace("@Code", _Code.ToString());
                    Sql = Sql.Replace("@AlarmMin", _AlarmMin.ToString());
                    Sql = Sql.Replace("@AlarmMax", _AlarmMax.ToString());
                    MySqlCommand cmd = new MySqlCommand(Sql, Conn);
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public DataTable GetCodeRead()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = "select * from code_read where Active='Y' order by ord";
                //string txtSql = "select * from code_read where code='11' order by ord";
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        // *** frmMonitor
        public DataTable RetriveNowData()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT create_date,addr_id,control_code,power,voltage,current,pf,frequency,temperature1,temperature2,humid1,humid2,irr1,watts_hour FROM data_logger a
                                inner join data_raw b on a.data_raw_id=b.data_raw_id
                                where a.data_raw_id= ((select max( data_raw_id ) FROM data_logger where addr_id='91') )";

                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    if (DS.Tables[0].Rows.Count > 0) return DS.Tables[0];//.Rows[0];
                    else return null;
                    //return DS.Tables[0].Rows[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public DataRow RetriveNowData(string _AddrID)
        {
            var collection = DB.GetCollection<BsonDocument>("tbDataHttpGet");
            var resultDoc = collection.Find(new BsonDocument()).Sort("{_id:-1}").Limit(2).ToList();

            DataTable table = new DataTable();
            table = bsonDocDatatbl(resultDoc);
            return table.Rows[0];

            /*
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT create_date,addr_id,control_code,power,voltage,current,pf,frequency,temperature1,temperature2,humid1,humid2,irr1,watts_hour FROM data_logger a
                                inner join data_raw b on a.data_raw_id=b.data_raw_id
                                where a.data_raw_id= ((select max( data_raw_id ) FROM data_logger where addr_id='91') ) and addr_id='" + _AddrID + "'";

                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    if (DS.Tables[0].Rows.Count > 0) return DS.Tables[0].Rows[0];
                    else return null;
                    //return DS.Tables[0].Rows[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            */
        }

        public void RepairDataLogger()
        {
            try
            {
                using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
                {
                    string Sql = @"repair table data_logger;
                                   repair table data_raw_detail;
                                   repair table data_raw;";
                    MySqlCommand cmd = new MySqlCommand(Sql, Conn);
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void DeleteRawDetail()
        {
            try
            {
                using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
                {
                    string Sql = @"delete from data_raw_detail where
                                   data_raw_id < ((select max(data_raw_id) from data_raw)-3000);OPTIMIZE TABLE `data_raw_detail`;"; //flush table data_raw_detail
                    MySqlCommand cmd = new MySqlCommand(Sql, Conn);
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // *** frmGraph2
        public DateTime GetLastData()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"select max( create_date ) FROM data_logger a
                                inner join data_raw b on a.data_raw_id=b.data_raw_id where control_code=0"; 


                //string txtSql = @"SELECT max(create_date) 
                                //FROM `data_raw`";

                try
                {
                    Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    object obj = cmd.ExecuteScalar();
                    if (obj != DBNull.Value)
                        return Convert.ToDateTime(obj);
                    else
                        return DateTime.Now;

                    //return Convert.ToDateTime(cmd.ExecuteScalar());//.ToString();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public DataTable RetriveNowDataAll()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT addr_id,power,voltage,current,temperature1,temperature2,humid1,irr1 
                                  FROM data_logger where data_raw_id= (select max( data_raw_id ) FROM data_logger where control_code=0 and addr_id='91' )";
                try
                {
                    //Conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    //cmd.Parameters.AddWithValue("@ObjectID", mObjectID);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        //***  frmGraph
        public DataRow GetCodeAddr(string _AddrID)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"SELECT * 
                                FROM `code_addr` 
                                WHERE code='" + _AddrID + "' and active='Y'";

                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    if (DS.Tables[0].Rows.Count > 0) return DS.Tables[0].Rows[0];
                    else return null;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public DataTable Getdata_Logger(string _date, string _CodeAddr)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string _WhereClause = " where date_format(create_date,'%x-%m-%d')='" + _date + "'";//control_code=0 and 
                _WhereClause += (_CodeAddr == "10") ? "" : " and addr_id='" + _CodeAddr + "'";
                string txtSql = @"select logger_id,create_date,
                                  (Hour( create_date )*60 + MINUTE( create_date ))/3 x,(Hour( create_date )*60 + MINUTE( create_date ))/3.5 x1,
                                  control_code,temperature1,temperature2,lux,power,voltage,current,remark from data_logger a
                                  inner join data_raw b on a.data_raw_id=b.data_raw_id" + _WhereClause;// where addr_id='" + _CodeAddr + "'";// and logger_id between " + b.ToString() + " and " + e.ToString();
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

        }

        public DataTable Getdata_GraphCurve(string _date, string _CodeAddr,string _Val)
        {
//            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
//            {
//                string _WhereClause = " where date_format(create_date,'%x-%m-%d')='" + _date + "'";//control_code=0 and 
//                _WhereClause += " and addr_id='" + _CodeAddr + "'";
//                string txtSql = @"select logger_id,create_date,
//                                  (Hour( create_date )*60 + MINUTE( create_date ))/3 x,(Hour( create_date )*60 + MINUTE( create_date ))/3.5 x1,
//                                  " + _Val+((_CodeAddr== "09")?"*1000":"*1")+ @" val from data_logger a
//                                  inner join data_raw b on a.data_raw_id=b.data_raw_id" + _WhereClause;// where addr_id='" + _CodeAddr + "'";// and logger_id between " + b.ToString() + " and " + e.ToString();
//                try
//                {
//                    MySqlDataAdapter adapter = new MySqlDataAdapter();
//                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
//                    adapter.SelectCommand = cmd;
//                    DataSet DS = new DataSet();
//                    adapter.Fill(DS);
//                    return DS.Tables[0];
//                }
//                catch (Exception ex)
//                {
//                    throw (ex);
//                }
//            }

            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString)) // Create By art 17/02/57
            {
                string _WhereClause = " where date_format(create_date,'%x-%m-%d')='" + _date + "'";//control_code=0 and 
                _WhereClause += " and addr_id='" + _CodeAddr + "'";
                string txtSql = @"select logger_id,create_date,
                                  (Hour( create_date )*60 + MINUTE( create_date ))/3 x,(Hour( create_date )*60 + MINUTE( create_date ))/3.5 x1,
                                  " + _Val + (((_CodeAddr == "90" || _CodeAddr == "91") && _Val == "temperature1") ? "*1 + 15" : "*1") + @" val from data_logger a
                                  inner join data_raw b on a.data_raw_id=b.data_raw_id" + _WhereClause;// where addr_id='" + _CodeAddr + "'";// and logger_id between " + b.ToString() + " and " + e.ToString();
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

        }

        public DataTable Getdata_GraphCurvePre3(string _date, string _CodeAddr, string _Val)
        {

            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString)) // Create By art 17/02/57
            {
                string _WhereClause = " where date_format(create_date,'%x-%m-%d')='" + _date + "'";//control_code=0 and 
                _WhereClause += " and addr_id='" + _CodeAddr + "'";
                string txtSql = @"select logger_id,create_date,
                                  (Hour( create_date )*60 + MINUTE( create_date ))/6 x,(Hour( create_date )*60 + MINUTE( create_date ))/6 x1,
                                  " + _Val + (((_CodeAddr == "90" || _CodeAddr == "91") && _Val == "temperature1") ? "*1 + 15" : "*1") + @" val from data_logger a
                                  inner join data_raw b on a.data_raw_id=b.data_raw_id" + _WhereClause;// where addr_id='" + _CodeAddr + "'";// and logger_id between " + b.ToString() + " and " + e.ToString();
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    adapter.SelectCommand = cmd;
                    DataSet DS = new DataSet();
                    adapter.Fill(DS);
                    return DS.Tables[0];
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

        }
    }


}
