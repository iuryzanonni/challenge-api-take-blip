using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Models
{
    public class Repositories
    {
        public List<Repository> repositories { get; set; }

        public Repositories()
        {            
            repositories = new List<Repository>();
        }       

        public async Task<List<Repository>> GetAllRepositories(Organization p_organization)
        {
            int page = 1;
            int per_page = 0;

            while (p_organization.public_repos > 0)
            {
                if (p_organization.public_repos > 100)
                {
                    per_page = 100;
                    p_organization.public_repos -= per_page;
                }
                else
                {
                    p_organization.public_repos = 0;
                }

                HttpResponseMessage response = await p_organization.client.GetAsync($"orgs/{p_organization.username}/repos?per_page={per_page}&page={page}");

                if (response.IsSuccessStatusCode)
                {

                    var results = response.Content.ReadAsStringAsync().Result;
                    this.repositories.AddRange(JsonConvert.DeserializeObject<List<Repository>>(results));

                }
                page++;
            }

            return this.repositories;
        }

        public async Task<List<Repository>> GetRepositories(Organization p_organization, string p_languageType, int p_numRepository)
        {
            if(p_languageType.ToLower() == "cs")
            {
                p_languageType = "C#";
            }
            List<Repository> repositories = await GetAllRepositories(p_organization);

            List<Repository> results = repositories.Where(r => r.language == p_languageType).OrderBy( r => r.created_at).Take<Repository>(p_numRepository).ToList();

            foreach(Repository r in results)
            {
                r.avatar_url = p_organization.avatar_url;
            }

            return results;
        }
    }
}
