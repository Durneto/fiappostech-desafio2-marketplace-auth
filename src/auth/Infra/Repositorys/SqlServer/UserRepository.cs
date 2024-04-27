using AuthApi.Domains.Interfaces;
using AuthApi.Domains.Models;
using AuthApi.Infra.Repositorys.Base;
using Dapper;
using System.Net;

namespace AuthApi.Infra.Repositorys.SqlServer
{
    public class UserRepository : IUserRepository
    {
        private readonly IUow _uow;

        public UserRepository(IUow uow)
        {
            _uow = uow;
        }

        public async Task CreateAsync(User user)
        {
            await _uow.Connection.ExecuteAsync("INSERT INTO TB_USER (Id, Login, Password, [Authorization], DataCriacao) VALUES (@Id, @Login, @Password, @Authorization, @CreateDate)", 
                //new 
                //{ 
                //    user.Id, 
                //    user.Login, 
                //    user.Password,
                //    Authorization = (int)user.Authorization,
                //    user.CreateDate
                //}, 
                user,
                _uow.Transaction);
        }

        public async Task DeleteAsync(string id)
        {
            await _uow.Connection.ExecuteAsync("DELETE FROM TB_USER WHERE Id = @Id", new { Id = id }, _uow.Transaction);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return (await _uow.Connection.QueryAsync<User>("SELECT Id, Login, Password, [Authorization], DataCriacao AS CreateDate FROM TB_USER", null, _uow.Transaction)).ToList();
        }

        public async Task<User> GetAsync(string id)
        {
            return await _uow.Connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM TB_USER WHERE Id = @Id", new { Id = id }, _uow.Transaction);
        }

        public async Task<User> GetLoginAsync(string login)
        {
            return await _uow.Connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM TB_USER WHERE Login = @Login", new { Login = login }, _uow.Transaction);
        }
    }
}
