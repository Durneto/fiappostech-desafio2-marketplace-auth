using AuthApi.Domains.Models.Enum;
using Dapper;
using System.Data;

namespace AuthApi.Infra.Repositorys.SqlServer.DapperConfig
{
    public class TypeAuthorizationHandler : SqlMapper.TypeHandler<TypeAuthorization>
    {
        public override TypeAuthorization Parse(object value)
        {
            return (TypeAuthorization)Enum.Parse(typeof(TypeAuthorization), value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, TypeAuthorization value)
        {
            parameter.Value = (int)value;
        }
    }
}
