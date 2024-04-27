using Dapper;

namespace AuthApi.Infra.Repositorys.SqlServer.DapperConfig
{
    public static class DapperCollections
    {
        public static void AddDapperMapper(this IServiceCollection services)
        {
            // Registrando o manipulador de tipo customizado do Dapper para enums
            //SqlMapper.AddTypeHandler(new TypeAuthorizationHandler());
        }
    }
}
