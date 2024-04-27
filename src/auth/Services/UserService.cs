using AuthApi.Domains.Base;
using AuthApi.Domains.Dtos.User;
using AuthApi.Domains.Interfaces;
using AuthApi.Domains.Models;
using AuthApi.Infra.Repositorys.Base;
using System.Data;

namespace AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUow _uow;
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, IUow uow)
        {
            _uow = uow;
            _repository = repository;
        }

        public async Task<ApiResult<UserDto>> CreateAsync(UserCreateDto dto)
        {
            var result = new ApiResult<UserDto>();
            try
            {
                _uow.Open();

                var userRep = await _repository.GetLoginAsync(dto.Login);

                if (userRep != null)
                    result.Erros.Add("Usuário já cadastrado");

                if (!result.HasError)
                {
                    _uow.BeginTransaction();

                    var model = new User(dto);
                    
                    if (model == null)
                        result.Erros.Add("O usuário deve ser informado");

                    if (model?.Login == string.Empty)
                        result.Erros.Add("O login deve ser informado");

                    if (model?.Password == string.Empty)
                        result.Erros.Add("A senha deve ser informado");

                    if (!result.HasError)
                    {
                        model.Id = Guid.NewGuid().ToString();                        
                        model.CreateDate = DateTime.Now;

                        await _repository.CreateAsync(model);
                        _uow.Commit();

                        result.Data = model.ToDto();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Erros.Add(ex.ToString());
                _uow.Rollback();
            }
            finally
            {
                _uow.Dispose();
            }

            return result;
        }

        public async Task<ApiResult> DeleteAsync(string id)
        {
            var result = new ApiResult();
            try
            {
                _uow.Open();
                _uow.BeginTransaction();

                var model = await _repository.GetAsync(id);

                if (model == null)
                    result.Erros.Add("O produto não foi localizado");

                if (!result.HasError)
                {
                    await _repository.DeleteAsync(id);
                    _uow.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Erros.Add(ex.ToString());
                _uow.Rollback();
            }
            finally
            {
                _uow.Dispose();
            }

            return result;
        }

        public async Task<ApiResult<List<UserDto>>> GetAllAsync()
        {
            var result = new ApiResult<List<UserDto>>();
            try
            {
                _uow.Open();

                var list = await _repository.GetAllAsync();
                var dtoList = list.Select(p => p.ToDto()).ToList();
                result.Data = dtoList;
            }
            catch (Exception ex)
            {
                result.Erros.Add(ex.ToString());
            }
            finally
            {
                _uow.Dispose();
            }

            return result;
        }
    }
}
