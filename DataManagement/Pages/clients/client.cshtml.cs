using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace TestApp.Pages.Shared.clients
{
    public class clientModel : PageModel
    {
       public List<clientInfo> clients = new List<clientInfo>();
       private IConfiguration _configuration;
       private ILogger<clientModel> _logger;

        public clientModel(IConfiguration configuration, ILogger<clientModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public void OnGet()
        {
            try
            {

                string conn = _configuration.GetConnectionString("myDb");
                _logger.LogInformation("client connection..!!", conn);
                string sql = "select * from clients";
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    _logger.LogInformation("client loading..!!");
                    connection.Open();
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                clientInfo info = new clientInfo();
                                info.id = ""+reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.phone = reader.GetString(3);
                                info.address = reader.GetString(4);
                                clients.Add(info);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Error!!", ex.Message);
                _logger.LogError("client loading error!!", ex.Message);
            }

        }

        public class clientInfo
        {
            public string id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address { get; set; }

        }
    }
}
