using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
internal class Program
{
    private static void Main(string[] args)
    {

        var configration = new ConfigurationBuilder()
           .AddJsonFile("appsitting.json")
           .Build();


        //read from user input
        var walletToInsert = new Wallet
        {
            Holder = "Menaa",
            Balance = 5500
        };



        SqlConnection connction = new SqlConnection(configration.GetSection("constr").Value);
        var sql = "INSERT INTO WALLETS (Holder ,Balance) VALUES " +
            $"(@Holder , @Balance)";

        SqlParameter holderParameter = new SqlParameter
        {
            ParameterName ="@Holder",
            SqlDbType = SqlDbType.VarChar,
            Value = walletToInsert.Holder,
            Direction = ParameterDirection.Input,

        };


        SqlParameter balanceParameter = new SqlParameter
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Value = walletToInsert.Balance,
            Direction = ParameterDirection.Input,

        };


        SqlCommand cmd = new SqlCommand(sql, connction);

        cmd.Parameters.Add(holderParameter);
        cmd.Parameters.Add(balanceParameter);
        cmd.CommandType = CommandType.Text;

        connction.Open();

        if (cmd.ExecuteNonQuery() > 0)
        {
            Console.WriteLine($"wallet for {walletToInsert.Holder} added successfuly ");
        }
        else {

            Console.WriteLine($"Error : wallet for  {walletToInsert.Holder}");
        }
      
        connction.Close();

    }
}