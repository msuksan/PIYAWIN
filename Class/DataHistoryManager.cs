using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace RiMonitoring
{
    class DataHistoryManager
    {
        private string m_ConnectionString;
        //private string m_TableName;
        private int m_ObjectID;

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


        public DataHistoryManager(string ConnectionString)
        {
            //m_TableName = "data_testsystem_weather";
            m_ConnectionString = ConnectionString;
        }

        public string GetModuleName(string _CodeModule)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string sql = "SELECT description FROM code_module WHERE code = '" + _CodeModule + "' AND Active='Y'";
                MySqlCommand cmd = new MySqlCommand(sql, Conn);
                try
                {
                    Conn.Open();
                    return  cmd.ExecuteScalar().ToString();;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public string GetAddrName(string _CodeAddr)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string sql = "SELECT description FROM code_addr WHERE code = '" + _CodeAddr + "' AND Active='Y'";
                MySqlCommand cmd = new MySqlCommand(sql, Conn);
                try
                {
                    Conn.Open();
                    return cmd.ExecuteScalar().ToString(); ;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
       
        
        public DataTable ReadAddrName()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"select '01' code,'All' description
                                union
                                SELECT code,description FROM code_addr WHERE Active='Y'";
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

        public DataTable GetdataMonth_Eff(string _date1, string _time1, string _time2)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {

//                string txtSql = @"select date_format(create_date,'%x-%m-%d') date_data,
//                                max(dc_watts_hour)-min(dc_watts_hour) dc_wh, max(watts_hour)-min(watts_hour) ac_wh
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='01'),2) pay
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='02'),2) co2
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='03'),2) diesel
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='04'),4) tree
//                                 from 
//                                (
//                                    select * from 
//                                    (
//                                      select aa.logger_id,aa.data_raw_id,dc_voltage,dc_current,dc_power,dc_watts_hour,voltage,current,power,watts_hour,pf,frequency from 
//                                      (
//		                                select logger_id,data_raw_id,voltage dc_voltage,current dc_current,power dc_power,watts_hour dc_watts_hour from data_logger where addr_id='02') aa
//                                        inner join (select * from data_logger where addr_id='09') bb on aa.data_raw_id=bb.data_raw_id
//                                     ) 
//                                    a
//                                 ) a
//                              inner join data_raw d on a.data_raw_id=d.data_raw_id where date_format(create_date,'%x-%m') = '" + _date1 + "' group by date_format(create_date,'%x-%m-%d')";

                string _WhereClause = " where date_format(create_date,'%x-%m') = '" + _date1 + "' and addr_id<'90' and b.description like '%1'";
                string txtSql = @"SELECT date_format(create_date,'%x-%m-%d') date_data,substr(b.description,1,6) description,max(watts_hour)-min(watts_hour) ac_wh
                                FROM data_logger a
                                inner join code_addr b on a.addr_id=b.code
                                inner join data_raw d on a.data_raw_id=d.data_raw_id" + _WhereClause + " group by date_format(create_date,'%x-%m-%d'),addr_id";       


                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    cmd.CommandTimeout = 300;
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
        public DataTable GetdataYear_Eff(string _date1, string _time1, string _time2)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {

                string _WhereClause = " where date_format(create_date,'%x') = '" + _date1 + "' and addr_id<'90' and b.description like '%1'";
                string txtSql = @"SELECT date_format(create_date,'%x-%m')date_data,substr(b.description,1,6) description,max(watts_hour)-min(watts_hour) ac_wh
                                FROM data_logger a
                                inner join code_addr b on a.addr_id=b.code
                                inner join data_raw d on a.data_raw_id=d.data_raw_id" + _WhereClause + " group by date_format(create_date,'%x-%m'),addr_id";
//                string _WhereClause = " where date_format(create_date,'%x') = '" + _date1 + "'";
//                string txtSql = @"SELECT date_format(create_date,'%x-%m') date_data, max(dc_watts_hour)-min(dc_watts_hour) dc_wh, max(watts_hour)-min(watts_hour) ac_wh
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='01'),2) pay
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='02'),2) co2
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='03'),2) diesel
//                                ,round((max(watts_hour)-min(watts_hour))*(select val from code_constant where code='04'),4) tree
//                                 from 
//                                (
//                                    select * from 
//                                    (
//                                      select aa.logger_id,aa.data_raw_id,dc_voltage,dc_current,dc_power,dc_watts_hour,voltage,current,power,watts_hour,pf,frequency from 
//                                      (
//		                                select logger_id,data_raw_id,voltage dc_voltage,current dc_current,power dc_power,watts_hour dc_watts_hour from data_logger where addr_id='02') aa
//                                        inner join (select * from data_logger where addr_id='09') bb on aa.data_raw_id=bb.data_raw_id
//                                     ) 
//                                    a
//                                 ) a
//                               inner join data_raw d on a.data_raw_id=d.data_raw_id" + _WhereClause + " group by date_format(create_date,'%x-%m')";

                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(txtSql, Conn);
                    cmd.CommandTimeout = 300;
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


        public DataTable GetdataMain_Logger(string _date, string _CodeAddr,string _Desc)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                _CodeAddr = _CodeAddr.Replace("'99'", "'90'");
                string _WhereClause = " where date_format(create_date,'%x-%m-%d')='" + _date + "'" ;
                //_WhereClause += (_CodeAddr=="10")?"":" and addr_id in(" + _CodeAddr + ")";
//                string txtSql = @"select description,create_date,control_code,temperature1,temperature2,irr1,if(a.addr_id='02',power/1000,power) power,voltage,current,remark,addr_id from data_logger a
//                                  inner join data_raw b on a.data_raw_id=b.data_raw_id
//                                  inner join code_addr c on a.addr_id=c.code" + _WhereClause + " order by a.data_raw_id " + _Desc + ",addr_id";// where addr_id='" + _CodeAddr + "'";// and logger_id between " + b.ToString() + " and " + e.ToString();

                _WhereClause +=  " and addr_id in(" + _CodeAddr + ")";
                string txtSql = @"select description,create_date,humid1,temperature1,
                                  (select temperature1 from data_logger where data_raw_id=a.data_raw_id and addr_id='91') temperature2,irr1,if(a.addr_id='02',power/1000,power) power,voltage,current,pf,frequency,watts_hour,addr_id from data_logger a
                                  inner join data_raw b on a.data_raw_id=b.data_raw_id
                                  inner join code_addr c on a.addr_id=c.code" + _WhereClause + " order by a.data_raw_id "+_Desc+",addr_id";// where addr_id='" + _CodeAddr + "'";// and logger_id between " + b.ToString() + " and " + e.ToString();
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

        public DataTable GetdataMain_Logger(string _WhereClause,int _every)//, string _date2, string _time1, string _time2, string _CodeAddr)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string txtSql = @"select a.logger_id,a.addr_id,description,create_date,control_code,power,(watts_hour/1) watts_hour,voltage,current,
                                e.temperature1,
                                (select temperature1 from data_logger where data_raw_id=a.data_raw_id and addr_id='91') temperature2,
                                e.irr1,e.humid1,
                                c.constant_a,remark from 
                                (
                                    select * from data_logger where
                                    data_raw_id in (
                                        SELECT data_raw_id FROM `data_raw` 
                                        where   (minute(create_date) mod " + _every + @")<2
                                    )
                                 ) a
                                inner join code_addr c on a.addr_id = c.code
                                inner join (select data_raw_id,temperature1,temperature2,irr1,humid1 from data_logger where addr_id='90') e on a.data_raw_id = e.data_raw_id
                                inner join data_raw d on a.data_raw_id=d.data_raw_id" + _WhereClause + " order by addr_id,logger_id";

//                string txtSql = @"select logger_id,a.data_raw_id,create_date
//                                ,dc_power, dc_watts_hour,dc_voltage,dc_current,power, watts_hour,voltage,current,pf,frequency
//                                ,round(watts_hour*(select val from code_constant where code='01'),2) pay
//                                ,round(watts_hour*(select val from code_constant where code='02'),2) co2
//                                ,round(watts_hour*(select val from code_constant where code='03'),2) diesel
//                                ,round(watts_hour*(select val from code_constant where code='04'),4) tree
//                                 from 
//                                (
//                                    select * from (select aa.logger_id,aa.data_raw_id,dc_voltage,dc_current,dc_power,dc_watts_hour,voltage,current,power,watts_hour,pf,frequency from 
//                                    (select logger_id,data_raw_id,voltage dc_voltage,current dc_current,power dc_power,watts_hour dc_watts_hour from data_logger where addr_id='02') aa
//                                        inner join (select * from data_logger where addr_id='09') bb on aa.data_raw_id=bb.data_raw_id) a
//		                                where
//                                            data_raw_id in (
//                                                SELECT data_raw_id FROM `data_raw` 
//                                                where   (minute(create_date) mod " + _every + @")<2
//                                        )
//                                 ) a
//                              inner join data_raw d on a.data_raw_id=d.data_raw_id " + _WhereClause + " order by logger_id";

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
        // *** for Delete data
        public long GetAllRecords()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string sql = "SELECT count(*) FROM data_raw";
                MySqlCommand cmd = new MySqlCommand(sql, Conn);
                try
                {
                    Conn.Open();
                    return  Convert.ToInt64(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public void DeleteAllRecords()
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string sql = @"truncate table data_logger;
                               truncate table data_raw_detail;
                               truncate table data_raw;";
                MySqlCommand cmd = new MySqlCommand(sql, Conn);
                try
                {
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public long GetRecords(string _Date1, string _Date2)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                string sql = "SELECT count(*) FROM data_raw where date_format(create_date,'%x-%m-%d') between '" + _Date1 + "' and '" + _Date2 + "'";
                MySqlCommand cmd = new MySqlCommand(sql, Conn);
                try
                {
                    Conn.Open();
                    return Convert.ToInt64(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public void DeleteRecords(string _Date1,string _Date2)
        {
            using (MySqlConnection Conn = new MySqlConnection(m_ConnectionString))
            {
                //string _WhereClause = "select data_raw_id from data_raw where date_format(create_date,'%x-%m-%d') between '" + _Date1 + "' and '" + _Date2 + "'";
                string _WhereClause = " where date_format(create_date,'%x-%m-%d') between '" + _Date1 + "' and '" + _Date2 + "'";
                string sql = "delete from data_logger where data_raw_id in(select data_raw_id from data_raw" + _WhereClause + ");";
                       sql += "delete from data_raw_detail where data_raw_id in(select data_raw_id from data_raw" + _WhereClause + ");";
                       sql += "delete from data_raw" + _WhereClause;
                MySqlCommand cmd = new MySqlCommand(sql, Conn);
                try
                {
                    Conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
       

      
    }
}
