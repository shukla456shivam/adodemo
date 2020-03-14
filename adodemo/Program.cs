using System;
using System.Data;  //Namespace import
using System.Data.SqlClient;
/*using System.Data;
* */

namespace AdoDemo	 //Pascal
{
	/// <summary>
	/// This program is demonstration of a dataset
	/// </summary>
	class Program	 //Pascal
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			DataSet organizationDataSet = new DataSet();
			DataTable employeeTable = new DataTable("Employee");
			organizationDataSet.Tables.Add(employeeTable);
			DataTable addressTable = new DataTable("Address");
			organizationDataSet.Tables.Add(addressTable);

			employeeTable.Columns.Add("Id");
			employeeTable.Columns.Add("Name");
			employeeTable.Columns.Add("Age");

			addressTable.Columns.Add("Id");
			addressTable.Columns.Add("EmpId");
			addressTable.Columns.Add("City");
			addressTable.Columns.Add("State");
			addressTable.Columns.Add("Zip");

			//Add Columns Id, Name, Age, City, State, Zip
			while (true)
			{
				Console.WriteLine("Do you want to exist y/n");
				string willExit =  Console.ReadLine();
				if (willExit == "y")
					break;

				Console.WriteLine("Please enter  Name, Age, City, State, Zip");
				var userInput = Console.ReadLine();
				var values = userInput.Split(',');
				string name = values[0].Trim();
				int age = Convert.ToInt32( values[1].Trim());
				string city = values[2].Trim();
				string state = values[3].Trim();
				string zip	 = values[4].Trim();

				var employeeRow = employeeTable.NewRow();    //camelCasing 
				employeeRow["Id"] = employeeTable.Rows.Count + 1 ;
				employeeRow["name"] = name;
				employeeRow["age"] = age;

				var addressRow = addressTable.NewRow();
				addressRow["Id"] = employeeTable.Rows.Count + 1;
				addressRow["EmpId"] = employeeRow["Id"];
				addressRow["City"] = city;
				addressRow["State"] = state;
				addressRow["Zip"] = zip;

				employeeTable.Rows.Add(employeeRow);
				addressTable.Rows.Add(addressRow);
			}

			foreach (DataRow row in organizationDataSet.Tables[0].Rows)
			{
				var currentId = row["Id"];
				var addressRow = organizationDataSet.Tables[1].Select( "empId =" + currentId)[0];
				Console.WriteLine("Name is {0} Age is {1} City is {2} State is {3} Zip is {4}",
					row["name"], row["age"], addressRow["city"], addressRow["state"], addressRow["zip"]);
			}

			Console.WriteLine("Press any key to exit");

			Console.ReadLine();

			SqlConnection sqlConnection = new SqlConnection("");
			SqlCommand command = new SqlCommand("Select * from Employee",sqlConnection);

			SqlDataAdapter da = new SqlDataAdapter(command);

			SqlCommandBuilder builder = new SqlCommandBuilder(da);

			da.Update(organizationDataSet.Tables[0]);
		}
	}
}
