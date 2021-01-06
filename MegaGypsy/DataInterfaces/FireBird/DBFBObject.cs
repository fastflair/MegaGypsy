using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Predictality {
   public class DBFBObject {
      // Private Variables
      //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      #region Private Variables
      protected FbConnection myConnection;
      private String myConnectionString;
      private FbDataAdapter FbDA;
      private FbTransaction myTransaction;
      #endregion

      // Constructors
      //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      #region Constructors
      /// <summary>
      /// A parameterized constructor, it allows us to take a connection string
      /// as a constructor argument, automatically instantiating a new connection
      /// </summary>
      public DBFBObject(string pNewConnectionString) {
         myConnectionString = pNewConnectionString;
         myConnection = new FbConnection(myConnectionString);
      }

      #endregion

      // Methods
      //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      #region Methods

      /// <summary>
      /// Runs a stored procedure, and it returns an integer indicating the return value of the
      /// stored procedure, and also returns the value of the RowsAffected aspect
      /// of the stored procedure that is returned by the ExecuteNonQuery method.
      /// </summary>
      public int RunProcedure(
              String storedProcName,
              IDataParameter[] parameters,
              out int rowsAffected) {
         int result;

         openConnection();
         FbCommand command = BuildIntCommand(storedProcName, parameters);
         command.CommandType = CommandType.StoredProcedure;
         rowsAffected = command.ExecuteNonQuery();
         result = (int)(command.Parameters["ReturnValue"].Value);
         closeConnection();
         return result;
      }

      /// <summary>
      /// Will run a stored procedure, can only be called by those classes
      /// deriving from this base. It returns a FbDataReader containing the
      /// result of the stored procedure.
      /// </summary>
      public FbDataReader RunProcedure(
          String storedProcName,
          IDataParameter[] parameters) {
         FbDataReader returnReader;

         openConnection();
         FbCommand command = BuildQueryCommand(storedProcName, parameters);
         command.CommandType = CommandType.StoredProcedure;

         returnReader = command.ExecuteReader();
         //Connection.Close();
         return returnReader;
      }

      /// <summary>
      /// Creates a DataSet by running the stored procedure and placing
      /// the results of the query/proc into the given tablename.
      /// </summary>
      public DataSet RunProcedure(
          String storedProcName,
          IDataParameter[] parameters,
          String tableName) {
         DataSet myDataSet = new DataSet();

         openConnection();
         FbDataAdapter FbDA = new FbDataAdapter();
         FbDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
         FbDA.Fill(myDataSet, tableName);
         closeConnection();
         return myDataSet;
      }

      /// <summary>
      /// Takes an -existing- dataset and fills the given table name
      /// with the results of the stored procedure.
      /// </summary>
      public void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet pDataSet, string tableName) {
         openConnection();
         FbDataAdapter FbDA = new FbDataAdapter();
         FbDA.SelectCommand = BuildIntCommand(storedProcName, parameters);
         FbDA.Fill(pDataSet, tableName);
         closeConnection();
      }

      public DataSet bindTable(
         String dbTableName,
         IDataParameter[] parameters,
         String tableName, string clause) {

         DataSet myDataSet = new DataSet();
         openConnection();
         try {
            FbDA = BuildQueryCommandTable(dbTableName, parameters, tableName, clause);
            FbCommandBuilder FbCB = new FbCommandBuilder(FbDA);
            FbDA.UpdateCommand = FbCB.GetUpdateCommand();
            FbDA.InsertCommand = FbCB.GetInsertCommand();
            FbDA.DeleteCommand = FbCB.GetDeleteCommand();
            FbDA.Fill(myDataSet, tableName);
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
            MessageBox.Show("Error accessing database caused commonly by MS Windows corruption, reboot and try again.");
         }
         closeConnection();
         return myDataSet;
      }

      public void UpdateFbDA(DataSet ds, string tableName) {
         try {
            openConnection();
            FbDA.Update(ds, tableName);
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         } finally {
            closeConnection();
         }
      }

      public string[] runQueryString(string query) {
         List<string> Lstring = new List<string>();

         openConnection();
         try {
            myTransaction = myConnection.BeginTransaction();
            FbCommand command = new FbCommand(query, myConnection);
            command.CommandType = CommandType.TableDirect;
            command.Transaction = myTransaction;
            using (IDataReader reader = command.ExecuteReader()) {
               while (reader.Read()) {
                  Lstring.Add(reader.GetString(0));
               }
            }
            myTransaction.Commit();
            command.Dispose();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         } finally {
            closeConnection();
         }
         return Lstring.ToArray();
      }
      public string[] runQueryString(string query, int index) {
         List<string> Lstring = new List<string>();

         openConnection();
         try {
            myTransaction = myConnection.BeginTransaction();
            FbCommand command = new FbCommand(query, myConnection);
            command.CommandType = CommandType.TableDirect;
            command.Transaction = myTransaction;
            using (IDataReader reader = command.ExecuteReader()) {
               while (reader.Read()) {
                  Lstring.Add(reader.GetString(index));
               }
            }
            myTransaction.Commit();
            command.Dispose();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         } finally {
            closeConnection();
         }
         return Lstring.ToArray();
      }
      public string[] runQueryStringMeterA(string query) {
         List<string> Lstring = new List<string>();

         openConnection();
         try {
            myTransaction = myConnection.BeginTransaction();
            FbCommand command = new FbCommand(query, myConnection);
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            command.Transaction = myTransaction;
            using (IDataReader reader = command.ExecuteReader()) {
               while (reader.Read()) {
                  Lstring.Add(Convert.ToString(reader.GetDecimal(0)));
               }
            }
            myTransaction.Commit();
            command.Dispose();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         } finally {
            closeConnection();
         }
         return Lstring.ToArray();
      }
      public Guid[] runQueryGuid(string query) {
         List<Guid> Lguid = new List<Guid>();

         openConnection();
         myTransaction = myConnection.BeginTransaction();
         FbCommand command = new FbCommand(query, myConnection);
         command.CommandType = CommandType.TableDirect;
         command.Transaction = myTransaction;
         using (IDataReader reader = command.ExecuteReader()) {
            while (reader.Read()) {
               Lguid.Add(new Guid(reader.GetString(0)));
            }
         }
         myTransaction.Commit();
         command.Dispose();
         closeConnection();
         return Lguid.ToArray();
      }
      public double[] runQueryDouble(string query) {
         List<double> Ldouble = new List<double>();

         openConnection();
         myTransaction = myConnection.BeginTransaction();
         FbCommand command = new FbCommand(query, myConnection);
         command.CommandType = CommandType.TableDirect;
         command.Transaction = myTransaction;
         using (IDataReader reader = command.ExecuteReader()) {
            while (reader.Read()) {
               Ldouble.Add(reader.GetDouble(0));
            }
         }
         myTransaction.Commit();
         command.Dispose();
         closeConnection();
         return Ldouble.ToArray();
      }

      public DateTime runQueryDateTime(string query, DateTime dtNow) {
         DateTime dt = DateTime.Now;
         openConnection();
         FbCommand command = new FbCommand(query, myConnection);
         command.CommandType = CommandType.TableDirect;
         using (IDataReader reader = command.ExecuteReader()) {
            while (reader.Read()) {
               try {
                  dt = reader.GetDateTime(0);
               } catch (Exception) {
                  dt = dtNow;
               }
            }
         }
         command.Dispose();
         closeConnection();
         return dt;
      }
      public DateTime[] runQueryDateTime(string query) {
         List<DateTime> Ldt = new List<DateTime>();
         openConnection();
         try {
            myTransaction = myConnection.BeginTransaction();
            FbCommand command = new FbCommand(query, myConnection);
            command.CommandType = CommandType.TableDirect;
            command.Transaction = myTransaction;
            using (IDataReader reader = command.ExecuteReader()) {
               while (reader.Read()) {
                  try {
                     Ldt.Add(reader.GetDateTime(0));
                  } catch (Exception) {
                     Ldt.Add(DateTime.Now);
                  }
               }
            }
            myTransaction.Commit();
            command.Dispose();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         }
         closeConnection();
         return Ldt.ToArray();
      }
      public DateAndString[] runQueryDateTimeAndString(string query) {
         List<DateAndString> Ldt = new List<DateAndString>();
         DateAndString ds;
         openConnection();
         try {
            myTransaction = myConnection.BeginTransaction();
            FbCommand command = new FbCommand(query, myConnection);
            command.CommandType = CommandType.TableDirect;
            command.Transaction = myTransaction;
            using (IDataReader reader = command.ExecuteReader()) {
               while (reader.Read()) {
                  ds = new DateAndString();
                  try {
                     ds.dt = reader.GetDateTime(0);
                     ds.s = reader.GetString(1);
                  } catch (Exception ex) {
                     Console.WriteLine(ex.Message);
                  }
                  Ldt.Add(ds);
               }
            }
            myTransaction.Commit();
            command.Dispose();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         }
         closeConnection();
         return Ldt.ToArray();
      }

      public int[] runQueryInt(string query) {
         int[] results = runQueryInt(query, true);
         return results;
      }
      public int[] runQueryInt(string query, bool dataReturned) {
         List<int> Lint = new List<int>();

         openConnection();
         myTransaction = myConnection.BeginTransaction();
         FbCommand command = new FbCommand(query, myConnection);
         command.CommandType = CommandType.TableDirect;
         command.Transaction = myTransaction;
         using (IDataReader reader = command.ExecuteReader()) {
            while (reader.Read()) {
               Lint.Add((int)reader.GetInt16(0));
            }
         }
         myTransaction.Commit();
         command.Dispose();
         closeConnection();
         // clean up the query result array because the FetchSize
         // returns more than the actual number of rows with useful data
         return Lint.ToArray();
      }


      public bool runQuery(string query) {
         openConnection();
         myTransaction = myConnection.BeginTransaction();
         FbCommand command = new FbCommand(query, myConnection);
         command.CommandType = CommandType.Text;
         command.Transaction = myTransaction;
         try {
            command.ExecuteScalar();
         } catch (Exception ex) {
            // exception, query did not run
            if (Program.debug) Console.Out.WriteLine(ex.ToString());
            myTransaction.Commit();
            command.Dispose();
            closeConnection();
            return false;
         }
         myTransaction.Commit();
         command.Dispose();
         closeConnection();
         return true;
      }
      public bool runQuery(string query, bool showException) {
         myTransaction = myConnection.BeginTransaction();
         FbCommand command = new FbCommand(query, myConnection);
         command.CommandType = CommandType.Text;
         command.Transaction = myTransaction;
         try {
            command.ExecuteScalar();
         } catch (Exception ex) {
            // exception, query did not run
            if (Program.debug && showException) Console.Out.WriteLine(ex.ToString());
            myTransaction.Commit();
            command.Dispose();
            return false;
         }
         myTransaction.Commit();
         command.Dispose();
         return true;
      }

      public void openConnection() {
         try {
            myConnection.Open();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         }
      }
      public void closeConnection() {
         try {
            myConnection.Close();
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         }
      }

      public DataTable getComboTable(string query) {
         DataTable dt_addcombo = new System.Data.DataTable();

         openConnection();
         try {
            FbDataAdapter da_addcombo = new FbDataAdapter(query, myConnection);

            da_addcombo.Fill(dt_addcombo);
         } catch (Exception ex) {
            Console.Out.WriteLine(ex.Message);
         }
         closeConnection();

         return dt_addcombo;
      }
      #endregion

      // Properties
      //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      #region Properties
      /// <summary>
      /// Protected property that exposes the connection string
      /// to inheriting classes. Read-Only.
      /// </summary>
      protected string ConnectionString {
         get {
            return myConnectionString;
         }
      }
      #endregion

      // Private Code
      //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      #region Private Code

      /// <summary>
      /// Private routine allowed only by this base class, it automates the task
      /// of building a SqlCommand object designed to obtain a return value from
      /// the stored procedure.
      /// </summary>
      /// <param name="storedProcName">Name of the stored procedure in the DB, eg. sp_DoTask</param>
      /// <param name="parameters">Array of IDataParameter objects containing parameters to the stored proc</param>
      /// <returns>Newly instantiated FbCommand instance</returns>
      public FbCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters) {
         FbCommand command = BuildQueryCommand(storedProcName, parameters);

         command.Parameters.Add(new FbParameter(
             "ReturnValue",
             FbDbType.Integer,
             4, /* Size */
             ParameterDirection.ReturnValue,
             false, /* is nullable */
             0, /* byte precision */
             0, /* byte scale */
             string.Empty,
             DataRowVersion.Default,
             ""));


         return command;
      }


      /// <summary>
      /// Builds a SqlCommand designed to return a SqlDataReader, and not
      /// an actual integer value.
      /// </summary>
      /// <param name="storedProcName">Name of the stored procedure</param>
      /// <param name="parameters">Array of IDataParameter objects</param>
      /// <returns></returns>
      private FbCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters) {
         FbCommand command = new FbCommand(storedProcName, myConnection);
         command.CommandType = CommandType.StoredProcedure;

         foreach (FbParameter parameter in parameters) {
            command.Parameters.Add(parameter);
         }

         return command;

      }

      private FbDataAdapter BuildQueryCommandTable(string dbTableName, IDataParameter[] parameters, string tableName, string clause) {
         string myQuery = "select * from " + dbTableName + " " + clause;
         FbDataAdapter da = new FbDataAdapter(myQuery, myConnection);

         return da;
      }
      #endregion

      // Binding Routines
      #region FirebirdDB Binding
      public static DataGridView AddCombo(string ColumnName, string HeaderText, string SourceTable, string DisplayColumn, string ValueMember, string SWhere, DataGridView dataGridView1, DBFBObject myDBObject, bool distinct) {
         // Build a string of comma delimited column names so we know which column to replace with a combo box
         string m_DataColumns = "";
         int count = dataGridView1.Columns.GetColumnCount(DataGridViewElementStates.Visible);
         for (int index = 0; index < count; index++) {
            if (index < count - 1) {
               m_DataColumns += dataGridView1.Columns[index].Name + ",";
            } else {
               m_DataColumns += dataGridView1.Columns[index].Name;
            }
         }
         string Sdistinct = "";
         if (distinct) Sdistinct = "distinct ";

         // remove the existing column
         dataGridView1.Columns.Remove(ColumnName);
         // this sql statement is the one used to build the combo box cross reference to display in the datagridview
         string query = "Select " + Sdistinct + DisplayColumn + " as LB," + ValueMember + " as VL from " + SourceTable + " " + SWhere;
         // create a new comboboxcolumn for use by the datagrid view and fill it with the query results
         DataGridViewComboBoxColumn column_addcombo = new DataGridViewComboBoxColumn();
         column_addcombo.DataSource = myDBObject.getComboTable(query);
         column_addcombo.DisplayMember = "LB";
         column_addcombo.ValueMember = "VL";
         column_addcombo.DataPropertyName = ColumnName;
         column_addcombo.Name = ColumnName;
         column_addcombo.HeaderText = HeaderText;
         // Add the new combo box column to the datagridview
         dataGridView1.Columns.Add(column_addcombo);
         // This section is used to display the added combo box column in the location of the
         // column that was originally replaced
         int j, i;
         string[] k;
         char a;
         a = ',';
         k = m_DataColumns.Split(a);
         i = k.Length - 1;
         j = 0;
         while ((j < i) && (k[j].IndexOf(ColumnName) == -1)) {
            j = j + 1;
         }
         dataGridView1.Columns[i].DisplayIndex = j;
         return dataGridView1;
      }
      public static DataGridView AddCombo(string ColumnName, string HeaderText, string SourceTable, string DisplayColumn, string ValueMember, string SWhere, DataGridView dataGridView1, DBFBObject myDBObject) {
         // Build a string of comma delimited column names so we know which column to replace with a combo box
         string m_DataColumns = "";
         int count = dataGridView1.Columns.GetColumnCount(DataGridViewElementStates.Visible);
         for (int index = 0; index < count; index++) {
            if (index < count - 1) {
               m_DataColumns += dataGridView1.Columns[index].Name + ",";
            } else {
               m_DataColumns += dataGridView1.Columns[index].Name;
            }
         }

         // remove the existing column
         dataGridView1.Columns.Remove(ColumnName);
         // this sql statement is the one used to build the combo box cross reference to display in the datagridview
         string query = "Select " + DisplayColumn + " as LB," + ValueMember + " as VL from " + SourceTable + " " + SWhere;
         // create a new comboboxcolumn for use by the datagrid view and fill it with the query results
         DataGridViewComboBoxColumn column_addcombo = new DataGridViewComboBoxColumn();
         column_addcombo.DataSource = myDBObject.getComboTable(query);
         column_addcombo.DisplayMember = "LB";
         column_addcombo.ValueMember = "VL";
         column_addcombo.DataPropertyName = ColumnName;
         column_addcombo.Name = ColumnName;
         column_addcombo.HeaderText = HeaderText;
         // Add the new combo box column to the datagridview
         dataGridView1.Columns.Add(column_addcombo);
         // This section is used to display the added combo box column in the location of the
         // column that was originally replaced
         int j, i;
         string[] k;
         char a;
         a = ',';
         k = m_DataColumns.Split(a);
         i = k.Length - 1;
         j = 0;
         while ((j < i) && (k[j].IndexOf(ColumnName) == -1)) {
            j = j + 1;
         }
         dataGridView1.Columns[i].DisplayIndex = j;
         return dataGridView1;
      }
      public static DataGridView AddCombo(string ColumnName, string HeaderText, string query, string SWhere, DataGridView dataGridView1, DBFBObject myDBObject) {
         // Build a string of comma delimited column names so we know which column to replace with a combo box
         string m_DataColumns = "";
         int count = dataGridView1.Columns.GetColumnCount(DataGridViewElementStates.Visible);
         for (int index = 0; index < count; index++) {
            if (index < count - 1) {
               m_DataColumns += dataGridView1.Columns[index].Name + ",";
            } else {
               m_DataColumns += dataGridView1.Columns[index].Name;
            }
         }

         // remove the existing column
         dataGridView1.Columns.Remove(ColumnName);
         // this sql statement is the one used to build the combo box cross reference to display in the datagridview
         // create a new comboboxcolumn for use by the datagrid view and fill it with the query results
         DataGridViewComboBoxColumn column_addcombo = new DataGridViewComboBoxColumn();
         column_addcombo.DataSource = myDBObject.getComboTable(query);
         column_addcombo.DisplayMember = "LB";
         column_addcombo.ValueMember = "VL";
         column_addcombo.DataPropertyName = ColumnName;
         column_addcombo.Name = ColumnName;
         column_addcombo.HeaderText = HeaderText;
         // Add the new combo box column to the datagridview
         dataGridView1.Columns.Add(column_addcombo);
         // This section is used to display the added combo box column in the location of the
         // column that was originally replaced
         int j, i;
         string[] k;
         char a;
         a = ',';
         k = m_DataColumns.Split(a);
         i = k.Length - 1;
         j = 0;
         while ((j < i) && (k[j].IndexOf(ColumnName) == -1)) {
            j = j + 1;
         }
         dataGridView1.Columns[i].DisplayIndex = j;
         return dataGridView1;
      }
      public static string FBdt(DateTime dt) {
         string date = dt.Day + "." + dt.Month + "." + dt.Year + " 00:00:00.000";
         return date;
      }
      #endregion


   }
}
