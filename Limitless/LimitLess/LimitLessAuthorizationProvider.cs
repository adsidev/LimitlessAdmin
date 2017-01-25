using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace LimitLess
{
    public class LimitLessAuthorizationProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var Identity = new ClaimsIdentity(context.Options.AuthenticationType);
            string UserName = context.UserName;
            string Password = context.Password;
            DataSet dsUser = new DataSet();
            string ConnectionString = ConfigurationManager.ConnectionStrings["LimitLess"].ToString();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Usp_ValidateUser",con);
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = UserName;
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = Password;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsUser);
            }
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                if (context.UserName == "admin" && context.Password == "admin")
                {
                    Identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                    Identity.AddClaim(new Claim("username", "admin"));
                    Identity.AddClaim(new Claim(ClaimTypes.Name, "rajesh"));
                    context.Validated(Identity);
                }
                else if (context.UserName == UserName && context.Password == Password)
                {
                    Identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    Identity.AddClaim(new Claim("username", UserName));
                    Identity.AddClaim(new Claim(ClaimTypes.Name, UserName));
                    context.Validated(Identity);
                }
                else
                {
                    context.SetError("Invalid User", "Provide valid user name and password.");
                }
            }
            else
            {
                context.SetError("Invalid User", "Provide valid user name and password.");
            }
        }
    }
}