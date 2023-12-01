using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RESTful_API_test.Parameter;
using System.Data;
using System.Web.Http.Cors;
using Intercom.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Xml.Linq;

namespace RESTful_API_test.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("API")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        /// <summary>
        /// SQL資料集合
        /// </summary>
        public MemberController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        List<MemberParametercs> memberlist = new List<MemberParametercs>();
        /// <summary>
        /// 查詢資料庫所有會員列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllMembers")]
        public List<MemberParametercs> Get()
        {
            //List<MemberParametercs> memberlist = new List<MemberParametercs>();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("API_connection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT *  FROM [API_TEST].[dbo].[API_TEST]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for(int i= 0; i < dt.Rows.Count; i++)
                {
                    MemberParametercs member = new MemberParametercs();
                    member.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    member.Name = dt.Rows[i]["Name"].ToString();
                    member.Age = dt.Rows[i]["Age"].ToString();
                    member.email = dt.Rows[i]["e-mail"].ToString();
                    member.address = dt.Rows[i]["address"].ToString();
                    member.work = dt.Rows[i]["work"].ToString();
                    member.datetime = dt.Rows[i]["datetime"].ToString();
                    memberlist.Add(member);
                }
            }
            return memberlist;
        }
        /// <summary>
        /// 查詢會員
        /// </summary>
        /// <param name="id">會員編號</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMembers/{id}")]
        public List<MemberParametercs> Get([FromRoute] int id)
        {
            //List<MemberParametercs> memberlist = new List<MemberParametercs>();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("API_connection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT *  FROM [API_TEST].[dbo].[API_TEST] WHERE id="+id, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MemberParametercs member = new MemberParametercs();
                    member.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    member.Name = dt.Rows[i]["Name"].ToString();
                    member.Age = dt.Rows[i]["Age"].ToString();
                    member.email = dt.Rows[i]["e-mail"].ToString();
                    member.address = dt.Rows[i]["address"].ToString();
                    member.work = dt.Rows[i]["work"].ToString();
                    member.datetime = dt.Rows[i]["datetime"].ToString();
                    memberlist.Add(member);
                }
            }
            return memberlist;
        }
        ///// <summary>
        ///// 新增會員
        ///// </summary>
        ///// <param name="parameter">會員參數</param>
        ///// <returns></returns>
        [HttpPost]
        [Route("addMember")]
        public IActionResult Insert([FromForm, BindRequired] string name, [FromForm, BindRequired] int age, [FromForm, BindRequired] string email, [FromForm, BindRequired] string address, [FromForm, BindRequired] string work)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("API_connection").ToString());
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[API_TEST]([name], [age], [e-mail],[address],[work]) VALUES ('" + name + "', '" + age + "', '" + email + "','" + address + "','" + work + "')");
            command.Connection = conn;
            int check_num = command.ExecuteNonQuery();
            conn.Close();
            if(check_num > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        ///// <summary>
        ///// 更新會員
        ///// </summary>
        ///// <param name="id">會員編號</param>
        ///// <param name="parameter">會員參數</param>
        ///// <returns></returns>
        [HttpPut]
        [Route("UpdateMember/{id}")]
        public IActionResult Update(
        [FromRoute] int id, [FromForm,BindRequired] string name, [FromForm, BindRequired] int age, [FromForm, BindRequired] string email, [FromForm, BindRequired] string address, [FromForm, BindRequired] string work)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("API_connection").ToString());
            conn.Open();
            SqlCommand command = new SqlCommand("UPDATE [dbo].[API_TEST] SET [name]='" + name + "', [age]=" + age + ", [e-mail]='" + email + "',[address]='" + address + "',[work]='" + work + "' WHERE (id = '"+ id + "')");
            command.Connection = conn;
            int check_num = command.ExecuteNonQuery();
            conn.Close();
            if (check_num != 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        ///// <summary>
        ///// 刪除會員
        ///// </summary>
        ///// <param name="id">會員編號</param>
        ///// <returns></returns>
        [HttpDelete]
        [Route("DeleteMember/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("API_connection").ToString());
            conn.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[API_TEST] WHERE (id = '" + id + "')");
            command.Connection = conn;
            int check_num = command.ExecuteNonQuery();
            conn.Close();
            if (check_num != 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
