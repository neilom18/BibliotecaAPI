using BibliotecaAPI.Models;
using System;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public class AddressService
    {
        public AddressService() { }

        public async Task<Address> GetAddressAsync(string cep, int retryCount)
        {

            var client = new System.Net.Http.HttpClient();
            var response = string.Empty;
            Address address = null;
            var retry = false;
            var retryIndex = 0;

            do
            {
                retry = false;
                Console.WriteLine("Fazendo requisicao {0} de {1}", retryIndex, retryCount);
                var rs = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json");
                if (!rs.IsSuccessStatusCode)
                {
                    retry = true;
                    retryIndex++;
                }
                if(rs.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response = await rs.Content.ReadAsStringAsync();
                    address = Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(response);
                }
            } while (retry && retryIndex <= retryCount);
            return address;
        }
    }
}
