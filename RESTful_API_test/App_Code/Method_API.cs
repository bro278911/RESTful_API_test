using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Data.SqlClient;
/// <summary>
/// Method_API 的摘要描述
/// </summary>
public class Method_API
{
    public readonly IConfiguration _configuration;
    SqlConnection conn;
    public Method_API(IConfiguration configuration)
    {
        _configuration = configuration;
        conn = new SqlConnection(_configuration.GetConnectionString("API_connection").ToString());
    }
    

}