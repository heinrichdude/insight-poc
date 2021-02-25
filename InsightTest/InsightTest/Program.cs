using System;
using System.IO;
using Insight.Database.Schema;
using System.Data.SqlClient;
using System.Reflection;

namespace InsightTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fileStream;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            var filename = string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd HHmmss"));
            var directory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location);

            fileStream = new FileStream(Path.Combine(directory, filename), FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(fileStream);

            Console.SetOut(writer);

            try
            {
                var connectionString = "Server='localhost';"
                    + "Initial Catalog='CCDB_MASTER_TEST';"
                    + "Integrated Security=false;"
                    + "User ID='sa';"
                    + "Password='yourStrong(!)Password'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SchemaObjectCollection schema = new SchemaObjectCollection();
                    schema.Load(System.Reflection.Assembly.GetExecutingAssembly());

                    connection.Open();
                    SchemaInstaller installer = new SchemaInstaller(connection);

                    new SchemaEventConsoleLogger().Attach(installer);

                    installer.Install("dbo", schema);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Schema installer encountered an error ...");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine();
            }
            finally
            {
                Console.SetOut(oldOut);
                writer.Close();
                fileStream.Close();

                Console.WriteLine("SchemaInstaller completed ...");
                Console.WriteLine("View " + filename + " for output results.");
                Console.WriteLine("Press a key to continue ...");
                Console.ReadKey();
            }

        }
    }
}
