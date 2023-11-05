using LetsShopping.Domain.Enums;
using LetsShopping.Domain.Models.Price;
using LetsShopping.Service.Dtos.Price;
namespace LetsShopping.DataAccess.Repositories.PriceRepositories
{
    public class PriceRepository : BaseRepository, IPriceRepository
    {
        public async ValueTask<int> CreateAsync(PriceDto model)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"Insert into Price(price,Status,CreatedAt) Values({model.Price},{(int)Status.Created},Getdate());";
                int created = await _connection.ExecuteAsync(query, model);
                return created;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }

        }


        public async ValueTask<int> DeleteAsync(int Id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"EXEC DeleteById \'Prices\' , {Id}";
                int deleted = await _connection.ExecuteAsync(query);
                return deleted;

            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async ValueTask<IList<Price>> GetAllAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Exec GetAll 'Prices'";
                var get = (await _connection.QueryAsync<Price>(query)).ToList();
                return get;
            }
            catch
            {
                return new List<Price>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async ValueTask<Price> GetByIdAsync(int Id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"EXEC GetAllById 'Prices' , {Id};";
                var price = await _connection.QueryFirstOrDefaultAsync<Price>(query);
                return price;
            }
            catch
            {
                return new Price();

            }
            finally
            {
                await _connection.CloseAsync();
            }

        }

        public async ValueTask<int> UpdateAsync(int Id, PriceDto model)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"Update Price Set Price = {model.Price},Status = {(int)Status.Updated},UpdatedAt = GetDate() Where Id = {Id}";
                int updated = await _connection.ExecuteAsync(query);
                return updated;

            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


    }
}
