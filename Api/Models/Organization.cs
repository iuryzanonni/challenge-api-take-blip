using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Models
{
    public class Organization
    {
        public int id { get; set; }
        public int public_repos { get; set; }
        public string name { get; set; }
        public string avatar_url { get; set; }
        public string username { get; set; }
        public Repositories repositories { get; set; }
        public HttpClient client { get; set; }

        public Organization(){ }

        public static async Task<Organization> InitalizeOrganizations(string p_orgName)
        {
            Organization organization = new Organization();
            organization.username = p_orgName;
            organization.repositories = new Repositories();
      
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Api", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"orgs/{p_orgName}");           

            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<Organization>(results);

                organization.id = model.id;
                organization.name = model.name;
                organization.avatar_url = model.avatar_url;
                organization.public_repos = model.public_repos;
                organization.client = client;
            }

            return organization;
        }

        public async Task<List<Repository>> GetAllRepositories()
        {            

            return await this.repositories.GetAllRepositories(this);
        }
    }
}
